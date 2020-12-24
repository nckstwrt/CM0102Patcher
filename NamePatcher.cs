using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;

namespace CM0102Patcher
{
    public class NamePatcher
    {
        string exeFile;
        string dataDir;
        Patcher patcher;
        byte[] exeBytes;


        int freePos = (0x6DC000 + 0x200000) - 0x20000; // last 128kb can be used for renaming

        public NamePatcher(string exeFile, string dataDir)
        {
            this.exeFile = exeFile;
            this.dataDir = dataDir;
            this.patcher = new Patcher();
            this.exeBytes = null;
        }

        public void RunPatch()
        {
            // Change game name :)
            YearChanger yearChanger = new YearChanger();
            var currentYear = yearChanger.GetCurrentExeYear(exeFile);
            var newGameName1 = currentYear.ToString().Substring(2) + "/" + (currentYear + 1).ToString().Substring(2);
            var newGameName2 = currentYear.ToString() + "/" + (currentYear+1).ToString().Substring(2);
            ByteWriter.WriteToFile(exeFile, 0x5cd33d, newGameName1 + "\0");  // Window Title
            ByteWriter.WriteToFile(exeFile, 0x68029d, newGameName2 + "\0");  // Main Menu Screen

            // Add Transfer Window Patch (from Saturn's v3 Patches)
            patcher.ApplyPatch(exeFile, patcher.patches["transferwindowpatch"]);

            // Expand the EXE
            patcher.ExpandExe(exeFile);

            // Make better error messages to help me debug
            ByteWriter.WriteToFile(exeFile, 0x20d7e2, patcher.HexStringToBytes("641fde"));

            // Patch League Select Items
            PatchExeString("Regional Divisions", "3. Liga", 0x5Eb02c);
            PatchExeString("Second Division", "Division 1", 0x5Eb02c);
            PatchExeString("Conference Division<%s - COMMENT - English Conference>", "National Leagues", 0x5Eb02c);
            // PatchExeString("Serie C2 A, B, C", "Lega Pro", 0x5Eb02c);

            // Stupidly complex code for Second Division B for Spain and Portugal
            // 0066A076 |.  68 3CB09E00 PUSH OFFSET 009EB03C; / Format = "Second Division B"
            // 0066A07B |.  50            PUSH EAX; Arg1 = 19FFCC
            // Trying to make this generic is overkill. Although learnt the trick of absolute jumping
            // PUSH offset-to-jmp-to RET
            int portugalText, segundaDivisionText, leagueSelectCodePos;
            var textSelectionBytes = patcher.HexStringToBytes("0000003B0D88F49C007407683CB09E00EB05683CB09E00687BA06600C3000000");
            portugalText = freePos;
            freePos += ByteWriter.WriteToFile(exeFile, freePos, "Campeonato de Portugal\0");
            segundaDivisionText = freePos;
            freePos += ByteWriter.WriteToFile(exeFile, freePos, "Segunda División B\0");
            leagueSelectCodePos = freePos;
            BitConverter.GetBytes(portugalText + 0x70B000).ToArray().CopyTo(textSelectionBytes, 12);
            BitConverter.GetBytes(segundaDivisionText + 0x70B000).ToArray().CopyTo(textSelectionBytes, 19);
            freePos += ByteWriter.WriteToFile(exeFile, freePos, textSelectionBytes);
            var jumpBytes = new byte[5] { 0xe9, 0x00, 0x00, 0x00, 0x00 };
            BitConverter.GetBytes(((leagueSelectCodePos+ 0x70b000) - (0x26A076 + 0x400000)) - 5 + 3).ToArray().CopyTo(jumpBytes, 1); // - 5 for the length of the jmp, + 3 for prefix 00s
            ByteWriter.WriteToFile(exeFile, 0x26A076, jumpBytes);
            
            // Patch Holland
            PatchHolland();

            // Patch Nation Comps
            PatchNationComp("European Football Championship", "UEFA European Championship");
            PatchNationComp("European Championship Qualifying", "UEFA European Championship Qualifying", "Euro Ch'ship Quals", "European Championship Qlf");
            PatchNationComp("Copa America", "Copa América", "Copa America", "Copa América");
            PatchNationComp("Oceania Nations Cup", "OFC Nations Cup", "OFC Nations Cup", "OFC Nations Cup");
            PatchNationComp("Asian Cup", "AFC Asian Cup", "Asian Cup", "Asian Cup");
            PatchNationComp("Confederations Cup", "FIFA Confederations Cup", "Confederations Cup", "Confederations Cup");

            // Patch Club Comps
            PatchClubComp("Asian Club Championship", "AFC Champions League", "Club Championship", "Champions League");
            PatchClubComp("CONCACAF Champions Cup", "CONCACAF Champions League", "Champions Cup", "Champions League");
            PatchClubComp("European Champions Cup", "UEFA Champions League", "Champions Cup", "Champions League");
            PatchClubComp("European Super Cup", "UEFA Super Cup", "Super Cup", "Super Cup");
            PatchClubComp("FIFA Club World Championship", "FIFA Club World Cup", "World Championship", "Club World Cup");
            PatchClubComp("Inter-American Cup", "Copa Interamericana", "Inter-American Cup", "Copa Interamericana");
            PatchClubComp("Inter-Toto Cup", "UEFA Europa League Qualifying", "Inter-Toto Cup", "Europa League Qualifying");
            PatchClubComp("Oceania Champions Cup", "OFC Champions League", "OFC Champions Cup", "Champions League");
            PatchClubComp("South American Copa Libertadores", "Copa Libertadores de América", "Copa Libertadores", "Copa Libertadores");
            PatchClubComp("South American Copa Mercosur", "Copa Sudamericana", "Copa Mercosur", "Copa Sudamericana");
            PatchClubComp("UEFA Cup", "UEFA Europa League", "UEFA Cup", "Europa League");
            PatchClubComp("African Champions League", "CAF Champions League", "African Champions League", "CAF Champions League");
            PatchClubComp("African Super Cup", "CAF Super Cup", "African Super Cup", "CAF Super Cup");
            PatchClubComp("Arab Club Champions Cup", "Arab World Club Cup", "Arab Champions Cup", "Arab World Club Cup");
            PatchClubComp("CONCACAF Cup Winners Cup", "Cup Winners Cup", "Cup Winners Cup", "Cup Winners Cup");
            PatchClubComp("European Cup Winners Cup", "Cup Winners' Cup", "Cup Winners Cup", "Cup Winners' Cup");
            PatchClubComp("Gulf Club Champions Cup", "GCC Champions League", "Gulf Club Champions Cup", "GCC Champions League");
            PatchClubComp("South American CONMEBOL Cup", "Copa CONMEBOL", "CONMEBOL Cup", "Copa CONMEBOL");
            PatchClubComp("South American Recopa", "Recopa Sudamericana", "Recopa", "Recopa Sudamericana");
            PatchClubComp("South American Super Cup", "Supercopa Libertadores", "Super Cup", "Supercopa Libertadores");

            // America
            PatchClubComp("American Major League", "Major League Soccer", "Major League", "Major League Soccer", "MLS");
            PatchClubComp("American A-League", "American A-League",  "A-League", "NASL", "NSL");
            PatchClubComp("American D3-Pro League", "American D3-Pro League", "D3-Pro League", "United Soccer League", "USL");
            PatchClubComp("American MLS All-Stars", "MLS All-Stars", "MLS All - Stars", "MLS All-Stars", "MLS");
            PatchClubComp("US Open Cup", "Lamar Hunt U.S. Open Cup", "Open Cup", "U.S. Open Cup");

            // Argentina
            PatchClubComp("Argentine Premier Division", "Argentine Superliga", "Premier Division", "Superliga", "SUP");
            PatchClubComp("Argentine Second Division", "Argentine Primera B Nacional", "Second Division", "Primera B Nacional", "D2");
            PatchClubComp("Argentine Third Division", "Argentine Third Division", "Third Division", "Torneo Federal A", "D3A");
            PatchClubComp("Argentine Metropolitan Zone", "Argentine Metropolitan Zone", "Metropolitan Zone", "Primera B Metropolitana", "D3B");
            PatchClubComp("Argentine Interior Zone", "Argentine Interior Zone", "Interior Zone", "Primera C", "D4");

            // Australia
            PatchClubComp("Australian National Soccer League", "Australian A-League", "National Soccer League", "A-League", "A-L");
            PatchClubComp("New South Wales State League", "New South Wales State League", "NSW Winter League", "NPL Northern NSW", "NSW");
            PatchClubComp("New South Wales Super League", "New South Wales Super League", "NSW Super League", "NPL NSW", "NSW");
            PatchClubComp("Queensland Premier League", "Queensland Premier League", "Qld Premier League", "NPL Queensland", "QNS");
            PatchClubComp("South Australian Premier League", "South Australian Premier League", "SA Premier League", "NPL South Australia", "STH");
            PatchClubComp("South Australian State League", "South Australian State League", "SA State League", "NPL ACT", "ACT");
            PatchClubComp("Victorian Premier League", "Victorian Premier League", "Vic Premier League", "NPL Victoria", "VIC");
            PatchClubComp("Victorian State League", "Victorian State League", "Vic State League", "NPL Northern Australia", "NTH");
            PatchClubComp("Western Australian Premier League", "Western Australian Premier League", "WA Premier League", "NPL Western Australia", "WST");
            PatchClubComp("Western Australian State League", "Western Australian State League", "WA State League", "NPL Tasmania", "TAS");

            // Austrian
            PatchClubComp("Austrian Premier Division", "Austrian Premier Division", "Premier Division", "Bundesliga", "B");
            PatchClubComp("Austrian First Division", "Austrian First Division", "First Division", "Erste Liga", "EL");
            PatchClubComp("Austrian Lower Division", "Austrian Lower Division", "Lower Division", "Regionalliga", "Reg");

            // Belgian
            PatchClubComp("Belgian First Division", "Belgian First Division A", "First Division", "First Division A", "D1A");
            PatchClubComp("Belgian Second Division", "Belgian First Division B", "Second Division", "First Division B", "D1B");
            PatchClubComp("Belgian Third Division", "Belgian First Amateur Division", "Third Division", "First Amateur Division", "AMA");
            PatchClubComp("Belgian Third Division A", "Belgian First Amateur Division A", "Third Division A", "First Amateur Division A", "FAA");
            PatchClubComp("Belgian Third Division B", "Belgian First Amateur Division B", "Third Division B", "First Amateur Division B", "FAB");
            PatchClubComp("Belgian Cup", "Coupe de Belgique", "Belgian Cup", "Coupe de Belgique");
            PatchClubComp("Belgian Super Cup", "Supercoupe de Belgique", "Super Cup", "Supercoupe");
            PatchClubComp("Belgian League Cup", "Belgian League Cup", "League Cup", "Coupe de la Ligue Pro");

            // Brazil
            PatchClubComp("Brazilian National First Division", "Brazilian Campeonato Série A", "National First Division", "Série A", "A");
            PatchClubComp("Brazilian National Second Division", "Brazilian Campeonato Série B", "National Second Division", "Série B", "B");
            PatchClubComp("Brazilian National Third Division", "Brazilian Campeonato Série C", "National Third Division", "Série C", "C");
            PatchClubComp("Brazilian Bahia State Championship", "Brazilian Campeonato Baiano", "Bahia State C'ship", "Campeonato Baiano", "BA1");
            PatchClubComp("Brazilian Central State Championship", "Brazilian Campeonato Central", "Central State C'ship", "Campeonato Central", "CE");
            PatchClubComp("Brazilian Gaúcho State Championship", "Brazilian Campeonato Gaúcho", "Gaúcho State C'ship", "Campeonato Gaúcho", "RS");
            PatchClubComp("Brazilian Goiás State Championship", "Brazilian Campeonato Goiano", "Goiás State C'ship", "Campeonato Goiano", "GO");
            PatchClubComp("Brazilian Minas Gerais State Championship", "Brazilian Campeonato Mineiro", "Minas State C'ship", "Campeonato Mineiro", "MG");
            PatchClubComp("Brazilian North State Championship", "Brazilian Campeonato Norte", "North State C'ship", "Campeonato Norte", "NO");
            PatchClubComp("Brazilian Northeast State Championship", "Brazilian Campeonato Nordeste", "Northeast State C'ship", "Campeonato Nordeste", "NE");
            PatchClubComp("Brazilian Paraná State Championship", "Brazilian Campeonato Paranaense", "Paraná State C'ship", "Campeonato Paranaense", "PR");
            PatchClubComp("Brazilian Pernambuco State Championship", "Brazilian Campeonato Pernambucano", "Pernambuco State C'ship", "Campeonato Pernambucano", "PE");
            PatchClubComp("Brazilian Rio de Janeiro State Championship", "Brazilian Campeonato Carioca", "Rio State C'ship", "Campeonato Carioca", "RJ");
            PatchClubComp("Brazilian Santa Catarina State Championship", "Brazilian Campeonato Catarinense", "Sta Catarina State C'ship", "Campeonato Catarinense", "SC");
            PatchClubComp("Brazilian São Paulo State Championship", "Brazilian Campeonato Paulista", "São Paulo State C'ship", "Campeonato Paulista", "SP");
            PatchClubComp("Brazilian Cup", "Copa do Brasil", "Brazilian Cup", "Copa do Brasil");
            PatchClubComp("Brazilian Champions Cup", "Brazilian Copa dos Campeões", "Brazilian Champions Cup", "Copa dos Campeões");
            PatchClubComp("Brazilian Central Regional Cup", "Brazilian Central Regional Cup", "Central Cup", "Copa Central");
            PatchClubComp("Brazilian North Regional Cup", "Brazilian North Regional Cup", "North Cup", "Copa Verde");
            PatchClubComp("Brazilian Northeast Regional Cup", "Brazilian Northeast Regional Cup", "Northeast Cup", "Copa do Nordeste");
            PatchClubComp("Brazilian Rio-São Paulo Tournement", "Brazilian Rio-São Paulo Tournement", "Rio-SP Tournement", "Torneio Rio-São Paulo");
            PatchClubComp("Brazilian South-Minas Regional Cup", "Brazilian South-Minas Regional Cup", "South-Minas Cup", "Copa Sul-Minas");
            PatchClubComp("Brazilian Taça Brazil", "Brazilian Taça Brazil", "Taça Brazil", "Taça Brasil");

            // China
            PatchClubComp("Chinese First Division A", "Chinese Super League", "First Division A", "Super League", "CSL");
            PatchClubComp("Chinese First Division B", "Chinese League One", "First Division B", "League One", "CL1");
            PatchClubComp("Chinese Second Division", "Chinese League Two", "Second Division", "League Two", "CL2");
            PatchClubComp("Chinese Lower Division", "Chinese Lower Division", "Lower Division", "Lower Division", "CLD");

            // Croatian
            PatchClubComp("Croatian First Division", "Croatian 1. HNL", "First Division", "1. HNL", "1L");
            PatchClubComp("Croatian Second Division", "Croatian 2. HNL", "Second Division", "2. HNL", "2L");
            PatchClubComp("Croatian Second Division North", "Croatian 2. HNL North", "Second Division North", "2. HNL North", "2LN");
            PatchClubComp("Croatian Second Division South", "Croatian 2. HNL South", "Second Division South", "2. HNL South", "2LS");
            PatchClubComp("Croatian Third Division Central", "Croatian Third Division Central", "Croatian Third Division C", "3. HNL Central", "3LC");
            PatchClubComp("Croatian Third Division East", "Croatian Third Division East", "Croatian Third Division E", "3. HNL East", "3LE");
            PatchClubComp("Croatian Third Division North", "Croatian Third Division North", "Croatian Third Division N", "3. HNL North", "3LN");
            PatchClubComp("Croatian Third Division South", "Croatian Third Division South", "Croatian Third Division S", "3. HNL South", "3LS");
            PatchClubComp("Croatian Third Division West", "Croatian Third Division West", "Croatian Third Division W", "3. HNL West", "3LW");
            PatchClubComp("Croatian Lower Division", "Croatian Lower Division", "Lower Division", "1. ZNL", "ZNL");
            PatchClubComp("Croatian Cup", "Hrvatski Kup", "Croatian Cup", "Hrvatski Kup");
            PatchClubComp("Croatian Super Cup", "Croatian Super Cup", "Super Cup", "Superkup");

            // Czech
            PatchClubComp("Czech First Division", "Czech First Division", "First Division", "First League", "CFL");
            PatchClubComp("Czech Second Division", "Czech Second Division", "Second Division", "National League", "CNL");
            PatchClubComp("Czech Third Division CFL", "Czech Third Division CFL", "Third Division CFL", "Bohemian League", "CBL");
            PatchClubComp("Czech Third Division MSFL", "Czech Third Division MSFL", "Third Division MSFL", "Moravian-Silesian League", "CML");

            // Danish
            PatchClubComp("Danish Premier Division", "Danish Superliga", "Premier Division", "Superliga", "DSL");
            PatchClubComp("Danish First Division", "Danish 1st Division", "First Division", "1st Division", "D1");
            PatchClubComp("Danish Second Division", "Danish 2nd Division", "Second Division", "2nd Division", "D2");
            PatchClubComp("Danish Kvalifikationsrækken", "Danish Kvalifikationsrækken", "Danish Kvalifikationsrækk", "Denmark Series", "D3");
            PatchClubComp("Danish Cup", "DBU Pokalen", "Danish Cup", "DBU Pokalen");

            // Dutch
            PatchClubComp("Dutch Premier Division", "Dutch Eredivisie", "Premier Division", "Eredivisie", "ERE");
            PatchClubComp("Dutch First Division", "Dutch Eerste Divisie", "First Division", "Eerste Divisie", "ED");
            PatchClubComp("Dutch Cup", "KNVB Beker", "Dutch Cup", "KNVB Beker");
            PatchClubComp("Dutch Super Cup", "Johan Cruijff Schaal", "Super Cup", "Johan Cruijff Schaal");

            // English
            PatchClubComp("English Premier Division", "English Premier League", "Premier Division", "Premier League", "EPL");
            PatchClubComp("English First Division", "English Football League Championship", "First Division", "Championship", "FLC");
            PatchClubComp("English Second Division", "English Football League One", "Second Division", "League One", "FL1");
            PatchClubComp("English Third Division", "English Football League Two", "Third Division", "League Two", "FL2");
            PatchClubComp("English Conference", "English National League", "Conference", "National League", "ENL");
            PatchClubComp("English Football EFL Cup", "English Football League Cup", "League Cup", "EFL Cup");
            PatchClubComp("English Vans Trophy", "English Football League Trophy", "Vans Trophy", "Football League Trophy");
            PatchClubComp("English Charity Shield", "English FA Community Shield", "Charity Shield", "FA Community Shield");
            PatchClubComp("English Conference Cup", "Conference League Cup", "Conference Cup", "Conference League Cup");

            // English Awards
            PatchStaffAward("English Players Player of the Year", "English PFA Players' Player of the Year");
            PatchStaffAward("English Players Young Player of the Year", "English PFA Young Player of the Year");
            PatchStaffAward("English Premier Division Team of the Week", "English Premier League Team of the Week");
            PatchStaffAward("English Premier Division Manager of the Month", "English Premier League Manager of the Month");
            PatchStaffAward("English Premier Division Player of the Month", "English Premier League Player of the Month");
            PatchStaffAward("English Premier Division Young Player of the Month", "English Premier League Young Player of the Month");
            PatchStaffAward("English Premier Division Manager of the Year", "English Premier League Manager of the Year");
            PatchStaffAward("English Players Premier Division Select", "English Premier League Team of the Year");
            PatchStaffAward("English First Division Team of the Week", "EFL Championship Team of the Week");
            PatchStaffAward("English First Division Manager of the Month", "EFL Championship Manager of the Month");
            PatchStaffAward("English First Division Player of the Month", "EFL Championship Player of the Month");
            PatchStaffAward("English First Division Young Player of the Month", "EFL Championship Young Player of the Month");
            PatchStaffAward("English First Division Manager of the Year", "EFL Championship Manager of the Year");
            PatchStaffAward("English Players First Division Select", "EFL Championship Team of the Year");
            PatchStaffAward("English Second Division Team of the Week", "EFL One Team of the Week");
            PatchStaffAward("English Second Division Manager of the Month", "EFL One Manager of the Month");
            PatchStaffAward("English Second Division Player of the Month", "EFL One Player of the Month");
            PatchStaffAward("English Second Division Young Player of the Month", "EFL One Young Player of the Month");
            PatchStaffAward("English Second Division Manager of the Year", "EFL One Manager of the Year");
            PatchStaffAward("English Players Second Division Select", "EFL One Team of the Year");
            PatchStaffAward("English Third Division Team of the Week", "EFL Two Team of the Week");
            PatchStaffAward("English Third Division Manager of the Month", "EFL Two Manager of the Month");
            PatchStaffAward("English Third Division Player of the Month", "EFL Two Player of the Month");
            PatchStaffAward("English Third Division Young Player of the Month", "EFL Two Young Player of the Month");
            PatchStaffAward("English Third Division Manager of the Year", "EFL Two Manager of the Year");
            PatchStaffAward("English Players Third Division Select", "EFL Two Team of the Year");
            PatchStaffAward("English Conference Team of the Week", "English National League Team of the Week");
            PatchStaffAward("English Conference Manager of the Month", "English National League Manager of the Month");
            PatchStaffAward("English Conference Player of the Month", "English National League Player of the Month");
            PatchStaffAward("English Conference Young Player of the Month", "English National League Young Player of the Month");
            PatchStaffAward("English Conference Manager of the Year", "English National League Manager of the Year");
            PatchStaffAward("English Players Conference Select", "English National League Team of the Year");

            // Scotland
            PatchClubComp("Scottish Premier Division", "Scottish Premiership", "Premier Division", "Premiership", "PRM");
            PatchClubComp("Scottish First Division", "Scottish Championship", "First Division", "Championship", "C");
            PatchClubComp("Scottish Second Division", "Scottish League One", "Second Division", "League One", "L1");
            PatchClubComp("Scottish Third Division", "Scottish League Two", "Third Division", "League Two", "L2");

            // Scotland Awards
            PatchStaffAward("Scottish Player of the Year", "PFA Scotland Players' Player of the Year");
            PatchStaffAward("Scottish Young Player of the Year", "PFA Scotland Young Player of the Year");
            PatchStaffAward("Scottish Premier Division Team of the Week", "SPFL Premiership Team of the Week");
            PatchStaffAward("Scottish Premier Division Manager of the Month", "SPFL Premiership Manager of the Month");
            PatchStaffAward("Scottish Premier Division Player of the Month", "SPFL Premiership Player of the Month");
            PatchStaffAward("Scottish Premier Division Young Player of Month", "SPFL Premiership Young Player of the Month");
            PatchStaffAward("Scottish Premier Division Manager of the Year", "SPFL Premiership Manager of the Year");
            PatchStaffAward("Scottish Premier Division Team of the Year", "SPFL Premiership Team of the Year");
            PatchStaffAward("Scottish First Division Team of the Week", "SPFL Championship Team of the Week");
            PatchStaffAward("Scottish First Division Manager of the Month", "SPFL Championship Manager of the Month");
            PatchStaffAward("Scottish First Division Player of the Month", "SPFL Championship Player of the Month");
            PatchStaffAward("Scottish First Division Young Player of Month", "SPFL Championship Young Player of the Month");
            PatchStaffAward("Scottish First Division Manager of the Year", "SPFL Championship Manager of the Year");
            PatchStaffAward("Scottish First Division Team of the Year", "SPFL Championship Team of the Year");
            PatchStaffAward("Scottish Second Division Team of the Week", "SPFL League One Team of the Week");
            PatchStaffAward("Scottish Second Division Manager of the Month", "SPFL League One Manager of the Month");
            PatchStaffAward("Scottish Second Division Player of the Month", "SPFL League One Player of the Month");
            PatchStaffAward("Scottish Second Division Young Player of Month", "SPFL League One Young Player of the Month");
            PatchStaffAward("Scottish Second Division Manager of the Year", "SPFL League One Manager of the Year");
            PatchStaffAward("Scottish Second Division Team of the Year", "SPFL League One Team of the Year");
            PatchStaffAward("Scottish Third Division Team of the Week", "SPFL League Two Team of the Week");
            PatchStaffAward("Scottish Third Division Manager of the Month", "SPFL League Two Manager of the Month");
            PatchStaffAward("Scottish Third Division Player of the Month", "SPFL League Two Player of the Month");
            PatchStaffAward("Scottish Third Division Young Player of Month", "SPFL League Two Young Player of the Month");
            PatchStaffAward("Scottish Third Division Manager of the Year", "SPFL League Two Manager of the Year");
            PatchStaffAward("Scottish Third Division Team of the Year", "SPFL League Two Team of the Year");

            // Spanish
            PatchClubComp("Spanish First Division", "Spanish La Liga", "First Division", "La Liga", "LL");
            PatchClubComp("Spanish Second Division", "Spanish Segunda División", "Second Division", "Segunda División", "SD");
            PatchClubComp("Spanish Second Division B", "Spanish Segunda División B", "Second Division B", "Segunda División B", "SDB");
            PatchClubComp("Spanish Second Division B1", "Spanish Segunda División B1", "Second Division B1", "Segunda División B1", "SDB");
            PatchClubComp("Spanish Second Division B2", "Spanish Segunda División B2", "Second Division B2", "Segunda División B2", "SDB");
            PatchClubComp("Spanish Second Division B3", "Spanish Segunda División B3", "Second Division B3", "Segunda División B3", "SDB");
            PatchClubComp("Spanish Second Division B4", "Spanish Segunda División B4", "Second Division B4", "Segunda División B4", "SDB");
            PatchClubComp("Spanish Lower Division", "Tercera División", "Lower Division", "Tercera División", "TD");
            PatchClubComp("Spanish Cup", "Spanish Copa del Rey", "Spanish Cup", "Copa del Rey");
            PatchClubComp("Spanish Super Cup", "Supercopa de España", "Super Cup", "Supercopa");

            // Germany
            PatchClubComp("German First Division", "German Bundesliga", "First Division", "Bundesliga", "BUN");
            PatchClubComp("German Second Division", "German 2. Bundesliga", "Second Division", "2. Bundesliga", "2B");
            PatchClubComp("German Regional", "German 3. Liga", "Regional", "3. Liga", "3L");
            PatchClubComp("German Regional Division East", "German 3. Liga Osten", "Regional Division East", "3. Liga Osten", "3LO");
            PatchClubComp("German Regional Division North", "German 3. Liga Nord", "Regional Division North", "3. Liga Nord", "3LN");
            PatchClubComp("German Regional Division South", "German 3. Liga Süd", "Regional Division South", "3. Liga Süd", "3LS");
            PatchClubComp("German Regional Division West/Southwest", "German 3. Liga West", "Regional Division West", "3. Liga West", "3LW");
            PatchClubComp("German Cup", "German DFB-Pokal", "German Cup", "DFB-Pokal");
            PatchClubComp("German League Cup", "German DFB-Ligapokal", "German League Cup", "DFB-Ligapokal");

            // Germany Awards
            PatchStaffAward("German First Division Team of the Week", "German Bundesliga Team of the Week");
            PatchStaffAward("German First Division Player of the Month", "German Bundesliga Player of the Month");
            PatchStaffAward("German First Division Manager of the Year", "German Bundesliga Manager of the Year");
            PatchStaffAward("German First Division Top Goalscorer", "German Bundesliga Top Goalscorer");
            PatchStaffAward("German Second Division Team of the Week", "German 2. Bundesliga Team of the Week");
            PatchStaffAward("German Second Division Player of the Month", "German 2. Bundesliga Player of the Month");
            PatchStaffAward("German Second Division Manager of the Year", "German 2. Bundesliga Manager of the Year");
            PatchStaffAward("German Second Division Top Goalscorer", "German 2. Bundesliga Top Goalscorer");

            // Portugal
            PatchClubComp("Portuguese Premier League", "Portuguese Primeira Liga", "Premier League", "Primeira Liga", "PRM");
            PatchClubComp("Portuguese Second League", "Portuguese LigaPro", "Second League", "LigaPro", "LP");
            PatchClubComp("Portuguese Second Division B", "Campeonato de Portugal", "Second Division B", "Campeonato de Portugal", "CAM");
            PatchClubComp("Portuguese Second Division B Central", "Campeonato de Portugal Central", "Second Division B Central", "Campeonato Central", "D3C");
            PatchClubComp("Portuguese Second Division B North", "Campeonato de Portugal North", "Second Division B North", "Campeonato North", "D3N");
            PatchClubComp("Portuguese Second Division B South", "Campeonato de Portugal South", "Second Division B South", "Campeonato South", "D3S");
            PatchClubComp("Portuguese Third Division", "Campeonato Distrital", "Third Division", "Campeonato Distrital", "D4");
            PatchClubComp("Portuguese Cup", "Taça de Portugal", "Portuguese Cup", "Taça de Portugal");
            PatchClubComp("Portuguese Super Cup", "Supertaça Cândido de Oliveira", "Portuguese Super Cup", "Supertaça");

            // Italy
            PatchClubComp("Italian Cup", "Coppa Italia", "Italian Cup", "Coppa Italia");
            PatchClubComp("Italian Serie C Cup", "Coppa Italia Lega Pro", "Serie C Cup", "Coppa Italia Lega Pro");
            PatchClubComp("Italian Super Cup", "Supercoppa Italiana", "Super Cup", "Supercoppa");
            PatchClubComp("Italian C1 Super Cup", "Supercoppa di Lega Pro", "C1 Super Cup", "Supercoppa di Lega Pro");

            // Italy Awards
            PatchStaffAward("Italian Serie A Manager of the Year", "Italian Serie A Panchina d'Oro");
            PatchStaffAward("Italian Serie A Top Goalscorer", "Italian Serie A Capocannoniere");
            PatchStaffAward("Italian Serie B Manager of the Year", "Italian Serie B Panchina d'Argento");

            // Poland
            PatchClubComp("Polish First Division", "Polish Ekstraklasa", "First Division", "Ekstraklasa", "EKS");
            PatchClubComp("Polish Second Division", "Polish I Liga", "Second Division", "I Liga", "LI");
            PatchClubComp("Polish Lower Division", "II Liga", "Lower Division", "II Liga", "IIL");
            PatchClubComp("Polish FA Cup", "Puchar Polski", "Polish FA Cup", "Puchar Polski");
            PatchClubComp("Polish League Cup", "Puchar Ekstraklasa", "League Cup", "Puchar Ekstraklasa");
            PatchClubComp("Polish Super Cup", "SuperPuchar Polski", "Super Cup", "SuperPuchar");
            
            // France
            PatchClubComp("French First Division", "French Ligue 1", "First Division", "Ligue 1", "L1");
            PatchClubComp("French Second Division", "French Ligue 2", "Second Division", "Ligue 2", "L2");
            PatchClubComp("French National", "French Championnat National", "National", "National", "NAT");
            PatchClubComp("French Lower Division", "French Lower Division", "Lower Division", "CFA2");
            PatchClubComp("French Cup", "Coupe de France", "French Cup", "Coupe de France");
            PatchClubComp("French League Cup", "Coupe de la Ligue", "League Cup", "Coupe de la Ligue");
            PatchClubComp("French Champions Trophy", "Trophée des Champions", "Champions Trophy", "Trophée des Champions");

            // France Awards
            PatchStaffAward("French First Division Team of the Week", "French Ligue 1 Team of the Week");
            PatchStaffAward("French First Division Team of the Year", "French Ligue 1 Team of the Year");
            PatchStaffAward("French Players First Division Player of the Year", "French Ligue 1 Players' Player of the Year");
            PatchStaffAward("French First Division Player of the Year", "French Ligue 1 Player of the Year");
            PatchStaffAward("French First Division Goalkeeper of the Year", "French Ligue 1 Goalkeeper of the Year");
            PatchStaffAward("French Second Division Team of the Week", "French Ligue 2 Team of the Week");
            PatchStaffAward("French Second Division Team of the Year", "French Ligue 2 Team of the Year");
            PatchStaffAward("French Players Second Division Player of the Year", "French Ligue 2 Players' Player of the Year");
            PatchStaffAward("French Second Division Player of the Year", "French Ligue 2 Player of the Year");
            PatchStaffAward("French Second Division Goalkeeper of the Year", "French Ligue 2 Goalkeeper of the Year");
            PatchStaffAward("French Players National Player of the Year", "French National Players' Player of the Year");

            // World Player Awards
            PatchStaffAward("FIFA World Player of the Year", "Ballon d'Or");
            PatchStaffAward("World Footballer Of The Year", "Best FIFA Men's Player", true, true);
            PatchStaffAward("European Footballer of the Year", "UEFA Men's Player of the Year");
            PatchStaffAward("South American Footballer of the Year", "Rey del Fútbol de América");
            PatchStaffAward("Oceania Player of the Year", "Oceania Footballer of the Year");
        }

        public void PatchWelshWithNorthernLeague()
        {
            patcher.ExpandExe(exeFile);
            PatchClubComp("English Northern Premier League Premier Division", "English National League North", "Northern Premier", "National League North", "NLN");
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatch"]);
            ByteWriter.WriteToFile(exeFile, 0x6d56b8, "English National League North" + "\0");

            PatchStaffAward("Welsh Team of the Week",           "English National League North Team of the Week", false);
            PatchStaffAward("Welsh Player of the Year",         "English National League North Player of the Year", false);
            PatchStaffAward("Welsh Young Player of the Year",   "English National League North Youth of the Year", false);
            PatchStaffAward("Welsh Top Goalscorer",             "English National League North Top Goalscorer", false);
            PatchStaffAward("Welsh Manager of the Year",        "English National League North Manager of the Year", false);
            PatchStaffAward("Welsh Manager of the Month",       "English National League North Manager of the Month", false);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthawards"]);
            patcher.ApplyPatch(exeFile, patcher.patches["tapanispacemaker"]);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatchrelegation"]);
        }

        public void PatchWelshWithSouthernLeague()
        {
            patcher.ExpandExe(exeFile);

            // Apply the standard north patch first
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatch"]);

            var cm = new ClubMover();
            cm.LoadClubAndComp(Path.Combine(dataDir, "club_comp.dat"), Path.Combine(dataDir, "club.dat"));
            var southernTeams = cm.CountSouthernTeams();

            // Patch the number of teams
            ByteWriter.WriteToFile(exeFile, 0x525B3C, BitConverter.GetBytes(southernTeams * 59));
            ByteWriter.WriteToFile(exeFile, 0x525B46, new byte[] { ((byte)southernTeams) });

            PatchClubComp("English Southern League Premier Division", "English National League South", "Southern Premier", "National League South", "NLS");
            ByteWriter.WriteToFile(exeFile, 0x6d56b8, "English National League South" + "\0");

            PatchStaffAward("Welsh Team of the Week", "English National League South Team of the Week", false);
            PatchStaffAward("Welsh Player of the Year", "English National League South Player of the Year", false);
            PatchStaffAward("Welsh Young Player of the Year", "English National League South Youth of the Year", false);
            PatchStaffAward("Welsh Top Goalscorer", "English National League South Top Goalscorer", false);
            PatchStaffAward("Welsh Manager of the Year", "National League South Manager of the Year", false);
            PatchStaffAward("Welsh Manager of the Month", "National League South Manager of the Month", false);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguesouthawards"]);
            patcher.ApplyPatch(exeFile, patcher.patches["tapanispacemaker"]);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatchrelegation"]);

            patcher.ApplyPatch(exeFile, 0x1751ff, "9c");
        }

        public void PatchWelshWithSouthernPremierCentral()
        {
            patcher.ExpandExe(exeFile);

            // Apply the standard north patch first
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatch"]);
            
            var cm = new ClubMover();
            cm.LoadClubAndComp(Path.Combine(dataDir, "club_comp.dat"), Path.Combine(dataDir, "club.dat"));
            var southernTeams = cm.SetupEnglishSouthernLeague();
            
            // Patch the number of teams
            ByteWriter.WriteToFile(exeFile, 0x525B3C, BitConverter.GetBytes(southernTeams*59));
            ByteWriter.WriteToFile(exeFile, 0x525B46, new byte[] { ((byte)southernTeams) });

            //ByteWriter.WriteToFile(exeFile, 0x6d56b8, "English Southern League Premier Division" + "\0");

            PatchClubComp("English Southern League Premier Division", "English Southern Premier Central", "Southern Premier", "Southern Premier", "SPC");
            ByteWriter.WriteToFile(exeFile, 0x6d56b8, "English Southern Premier Central" + "\0");
            
            PatchStaffAward("Welsh Team of the Week",           "English Southern Premier Team of the Week");
            PatchStaffAward("Welsh Player of the Year",         "English Southern Premier Player of the Year");
            PatchStaffAward("Welsh Young Player of the Year",   "English Southern Premier Youth of the Year");
            PatchStaffAward("Welsh Top Goalscorer",             "English Southern Central Premier Top Goalscorer");
            PatchStaffAward("Welsh Manager of the Year",        "English Southern Premier Manager of the Year");
            PatchStaffAward("Welsh Manager of the Month",       "English Southern Premier Manager of the Month");
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguesouthawards"]);
            patcher.ApplyPatch(exeFile, patcher.patches["tapanispacemaker"]);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatchrelegation"]);

            // Let's allow more loans seeing as we don't get many players
            patcher.ApplyPatch(exeFile, 0x179e5B, "07");
            patcher.ApplyPatch(exeFile, 0x179f17, "06");

            /*
            005751F8  |> \A1 FCADAD00   MOV EAX,DWORD PTR DS:[0ADADFC]
            005751FD  |.  8BB8 A0050000 MOV EDI,DWORD PTR DS:[EAX+5A0]
            00575203  |.  8BB0 74010000 MOV ESI,DWORD PTR DS:[EAX+174]

            0x5A0 = 0x168 * 4 = Team 0x168 which is the Northern Premier League
            0x167 is the southern, so we need 0x167 * 4 = 0x59c
            */
            patcher.ApplyPatch(exeFile, 0x1751ff, "9c");
        }

        // https://champman0102.co.uk/showthread.php?t=8267&highlight=Netherlands
        // Going to stick things at 009861d0 (005861d0 = in binary)
        // 0060E100  |> 68 34159B00 PUSH OFFSET 009B1534                     ; /Arg2 = ASCII "Holland"
        // 0060E100     68 D0619800 PUSH OFFSET 009861D0
        void PatchHolland()
        {
            PatchExeString("Holland", "Netherlands", 0x5b1534);

            // nation.dat
            var nationDatFilename = Path.Combine(dataDir, "nation.dat");
            var nationDat = ByteWriter.LoadFile(nationDatFilename);
            var pos = ByteWriter.SearchBytes(nationDat, "Holland");
            if (pos != -1)
            {
                ByteWriter.WriteToFile(nationDatFilename, pos, "Netherlands", 52);
                ByteWriter.WriteToFile(nationDatFilename, pos + 52, "Netherlands", 27);
                ByteWriter.WriteToFile(nationDatFilename, pos + 52 + 27, "NED");
            }
            // nat_club.dat
            ByteWriter.BinFileReplace(Path.Combine(dataDir, "nat_club.dat"), "Holland", "Netherlands");
            // euro.cfg
            ByteWriter.TextFileReplace(Path.Combine(dataDir, "euro.cfg"), "Holland", "Netherlands");
            // eng.lng
            var engLng = ByteWriter.LoadFile(Path.Combine(dataDir, "eng.lng"));
            var engLngHollandBytes = ByteWriter.SearchBytesForAll(engLng, Encoding.ASCII.GetBytes("Holland"));
            if (engLngHollandBytes.Contains(0x109FA1) && engLngHollandBytes.Contains(0x109FD5))
            {
                ByteWriter.WriteToFile(Path.Combine(dataDir, "eng.lng"), 0x109FA1, "Netherlands");
                ByteWriter.WriteToFile(Path.Combine(dataDir, "eng.lng"), 0x109FD5, "Netherlands");
            }
        }

        public static void PatchCompAcronym(string fileName, int startPos, string acronym)
        {
            ByteWriter.WriteToFile(fileName, startPos + 79, acronym, 3);
        }

        public void PatchStaffAward(string oldName, string newName, bool patchExe = true, bool ignoreCase = false)
        {
            var staff_comp = Path.Combine(dataDir, "staff_comp.dat");
            oldName = AddTerminator(oldName);
            newName = AddTerminator(newName);
            ByteWriter.BinFileReplace(staff_comp, oldName, newName, 0, 0, ignoreCase);
            if (patchExe)
                PatchExeString(oldName, newName, 0);
            PatchEngLng(oldName, newName);
        }

        public void PatchClubComp(string oldName, string newName, string oldShortName = null, string newShortName = null, string newAcronym = null)
        {
            PatchComp("club_comp.dat", oldName, newName, oldShortName, newShortName, newAcronym);
        }

        public void PatchNationComp(string oldName, string newName, string oldShortName = null, string newShortName = null, string newAcronym = null)
        {
            PatchComp("nation_comp.dat", oldName, newName, oldShortName, newShortName, newAcronym);
        }

        public void PatchComp(string fileName, string oldName, string newName, string oldShortName, string newShortName, string newAcronym = null)
        {
            oldName = AddTerminator(oldName);
            newName = AddTerminator(newName);
            oldShortName = AddTerminator(oldShortName);
            newShortName = AddTerminator(newShortName);

            int compChangePos = PatchComp(fileName, oldName, newName);
            if (compChangePos != -1)
            {
                if (oldShortName != null && newShortName != null)
                    PatchComp(fileName, oldShortName, newShortName, compChangePos + newName.Length, -1);
                if (newAcronym != null)
                    PatchCompAcronym(Path.Combine(dataDir, fileName), compChangePos, newAcronym);
                PatchEngLng(oldName, newName, oldShortName, newShortName, newAcronym);
            }
        }

        void LoadExeBytes()
        {
            if (exeBytes == null)
                exeBytes = ByteWriter.LoadFile(exeFile);
        }

        public int PatchComp(string fileName, string fromComp, string toComp, int clubCompStartPos = 0, int exeStartPos = 0x5d9e30)
        {
            var club_comp = Path.Combine(dataDir, fileName);

            fromComp = AddTerminator(fromComp);
            toComp = AddTerminator(toComp);

            int compChangePos = ByteWriter.BinFileReplace(club_comp, fromComp, toComp, clubCompStartPos, clubCompStartPos != 0 ? 1 : 0);

            if (exeStartPos != -1)
                PatchExeString(fromComp, toComp, exeStartPos);

            return compChangePos;
        }

        public void PatchExeString(string from, string to, int exeStartPos)
        {
            LoadExeBytes();

            from = AddTerminator(from);
            to = AddTerminator(to);

            // Find where the string is held
            var pos = ByteWriter.SearchBytes(exeBytes, from, exeStartPos);

            // Check for lower case version
            if (pos == -1)
                pos = ByteWriter.SearchBytes(exeBytes, from, exeStartPos, true);

            // Convert the position of the current string, to a PUSH statement in the exe
            var searchBytes = new byte[5] { 0x68, 0x00, 0x00, 0x00, 0x00 };
            BitConverter.GetBytes(pos + 0x400000).ToArray().CopyTo(searchBytes, 1);

            // Find the PUSH Statement in the EXE to this string
            var positions = ByteWriter.SearchBytesForAll(exeBytes, searchBytes, 0);
            foreach (var position in positions)
            {
                // Get the next free position of text and convert to a PUSH
                BitConverter.GetBytes(freePos + 0x70B000).ToArray().CopyTo(searchBytes, 1);

                // Write the new PUSH statement to the free pos
                ByteWriter.WriteToFile(exeFile, position, searchBytes);

                // Write the new string to the free pos and increment the free pos
                freePos += ByteWriter.WriteToFile(exeFile, freePos, to);
            }
        }

        private void PatchEngLng(string oldName, string newName, string oldShortName = null, string newShortName = null, string newAcronym = null)
        {
            var engLng = Path.Combine(dataDir, "eng.lng");
            
            newName = AddTerminator(newName);
            oldName = AddTerminator(oldName);
            newShortName = AddTerminator(newShortName);
            oldShortName = AddTerminator(oldShortName);

            int changePos = ByteWriter.BinFileReplace(engLng, oldName, newName, 0, 1);
            if (oldShortName != null)
            {
                ByteWriter.BinFileReplace(engLng, oldShortName, newShortName, changePos + newName.Length, 1);
            }
            if (newAcronym != null && changePos != -1)
                PatchCompAcronym(engLng, changePos, newAcronym);
        }

        private string AddTerminator(string inString)
        {
            if (!string.IsNullOrEmpty(inString))
            {
                if (inString[inString.Length - 1] != '\0')
                    inString += "\0";
            }
            return inString;
        }
    }
}

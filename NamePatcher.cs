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
            var newGameName = currentYear.ToString() + "/" + (currentYear+1).ToString().Substring(2);
            ByteWriter.WriteToFile(exeFile, 0x68029d, newGameName);

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

            // Poland
            PatchClubComp("Polish First Division", "Polish Ekstraklasa", "First Division", "Ekstraklasa", "EKS");
            PatchClubComp("Polish Second Division", "Polish I Liga", "Second Division", "I Liga", "LI");
            PatchClubComp("Polish Lower Division", "II Liga", "Lower Division", "II Liga", "IIL");
            PatchClubComp("Polish FA Cup", "Puchar Polski", "Polish FA Cup", "Puchar Polski");
            PatchClubComp("Polish League Cup", "Puchar Ekstraklasa", "League Cup", "Puchar Ekstraklasa");
            PatchClubComp("Polish Super Cup", "SuperPuchar Polski", "Super Cup", "SuperPuchar");
        }

        public void PatchWelshWithNorthernLeague()
        {
            PatchClubComp("English Northern Premier League Premier Division", "English National League North", "Northern Premier", "National League North", "NLN");
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatch"]);
            ByteWriter.WriteToFile(exeFile, 0x6d56b8, "English National League North" + "\0");

            PatchStaffAward("Welsh Team of the Week",           "English National League North Team of the Week");
            PatchStaffAward("Welsh Player of the Year",         "English National League North Player of the Year");
            PatchStaffAward("Welsh Young Player of the Year",   "English National League North Youth of the Year");
            PatchStaffAward("Welsh Top Goalscorer",             "English National League North Top Goalscorer");
            PatchStaffAward("Welsh Manager of the Year",        "English National League North Manager of the Year");
            PatchStaffAward("Welsh Manager of the Month",       "English National League North Manager of the Month");
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthawards"]);
            patcher.ApplyPatch(exeFile, patcher.patches["tapanispacemaker"]);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatchrelegation"]);
        }

        public void PatchWelshWithSouthernLeague()
        {
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

            PatchStaffAward("Welsh Team of the Week", "English National League South Team of the Week");
            PatchStaffAward("Welsh Player of the Year", "English National League South Player of the Year");
            PatchStaffAward("Welsh Young Player of the Year", "English National League South Youth of the Year");
            PatchStaffAward("Welsh Top Goalscorer", "English National League South Top Goalscorer");
            PatchStaffAward("Welsh Manager of the Year", "National League South Manager of the Year");
            PatchStaffAward("Welsh Manager of the Month", "National League South Manager of the Month");
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguesouthawards"]);
            patcher.ApplyPatch(exeFile, patcher.patches["tapanispacemaker"]);
            patcher.ApplyPatch(exeFile, patcher.patches["englishleaguenorthpatchrelegation"]);

            patcher.ApplyPatch(exeFile, 0x1751ff, "9c");
        }

        public void PatchWelshWithSouthernPremierCentral()
        {
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

        public void PatchStaffAward(string oldName, string newName, bool patchExe = false, bool ignoreCase = false)
        {
            var staff_comp = Path.Combine(dataDir, "staff_comp.dat");
            oldName = AddTerminator(oldName);
            newName = AddTerminator(newName);
            ByteWriter.BinFileReplace(staff_comp, oldName, newName, 0, 0, ignoreCase);
            if (patchExe)
                ByteWriter.BinFileReplace(exeFile, oldName, newName, 0, 0, ignoreCase);
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
            if (compChangePos != -1 && oldShortName != null && newShortName != null)
            {
                PatchComp(fileName, oldShortName, newShortName, compChangePos, -1);
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
                ByteWriter.BinFileReplace(engLng, oldShortName, newShortName, changePos, 1);
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

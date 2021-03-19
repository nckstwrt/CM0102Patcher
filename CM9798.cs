using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class CM9798
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Player  // 215 bytes
        {
            public int _UniqueID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)] public byte[] _FirstName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _SecondName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Nationality;
            public byte NationalCaps;
            public byte NationalGoals;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Team;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _DateJoined;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _ContractEnds;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)] public byte[] _BirthDate;
            public byte Age;
            public byte Goalkeeper;
            public byte Sweeper;
            public byte Defence;
            public byte Anchor;
            public byte Midfield;
            public byte Support;
            public byte Attack;
            public byte RightSided;
            public byte LeftSided;
            public byte CentralSided;
            public short Ability;           // First byte = 1, then add 128
            public short Potential;
            public short Reputation;
            public byte Adaptability;
            public byte Aggression;
            public byte BigOccasion;
            public byte Character;
            public byte Consistency;
            public byte Creativity;
            public byte Determination;
            public byte Dirtyness;
            public byte Dribbling;
            public byte Flair;
            public byte Heading;
            public byte Influence;
            public byte InjProne;
            public byte Marking;
            public byte OffTheBall;
            public byte Pace;
            public byte Passing;
            public byte Positioning;
            public byte SetPieces;
            public byte Shooting;
            public byte Stamina;
            public byte Strength;
            public byte Tackling;
            public byte Technique;

            public int UniqueID
            {
                get { return CM2.ConvertLongToNormalFormat(_UniqueID); }
                set { _UniqueID = CM2.ConvertLongToCM2Format(value); }
            }

            public string FirstName
            {
                get { return MiscFunctions.GetTextFromBytes(_FirstName); }
                set { _FirstName = MiscFunctions.GetBytesFromText(value, 30); }
            }
            public string SecondName
            {
                get { return MiscFunctions.GetTextFromBytes(_SecondName); }
                set { _SecondName = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string Nationality
            {
                get { return MiscFunctions.GetTextFromBytes(_Nationality); }
                set { _Nationality = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string Team
            {
                get { return MiscFunctions.GetTextFromBytes(_Team); }
                set { _Team = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string DateJoined
            {
                get { return MiscFunctions.GetTextFromBytes(_DateJoined); }
                set { _DateJoined = MiscFunctions.GetBytesFromText(value, 10); }
            }
            public string ContractEnds
            {
                get { return MiscFunctions.GetTextFromBytes(_ContractEnds); }
                set { _ContractEnds = MiscFunctions.GetBytesFromText(value, 10); }
            }
            public string BirthDate
            {
                get { return MiscFunctions.GetTextFromBytes(_BirthDate); }
                set { _BirthDate = MiscFunctions.GetBytesFromText(value, 13); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Team
        {
            /* 1 */  public int _UniqueID;
            /* 2 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKLongName;
            /* 3 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKShortName;
            /* 4 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKDescription;
            /* 5 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITALongName;
            /* 6 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITAShortName;
            /* 7 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITADescription;
            /* 8 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERLongName;
            /* 9 */  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERShortName;
            /* 10 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERDescription;
            /* 11 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPALongName;
            /* 12 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPAShortName;
            /* 13 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPADescription;
            /* 14 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FRELongName;
            /* 15 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FREShortName;
            /* 16 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FREDescription;
            /* 17 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORLongName;
            /* 18 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORShortName;
            /* 19 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORDescription;
            /* 20 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTLongName;
            /* 21 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTShortName;
            /* 22 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTDescription;
            /* 23 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Nation;
            /* 24 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Region;
            /* 25 */ public byte Developed;
            /* 26 */ public byte XCoord;
            /* 27 */ public byte YCoord;
            /* 28 */ public byte EEC;
            /* 29 */ public int TCoef8893;
            /* 30 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] City;
            /* 31 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Stadium;
            /* 32 */ public int Capacity;
            /* 33 */ public int Seating;
            /* 34 */ public byte Following;
            /* 35 */ public byte Reputation;
            /* 36 */ public byte Blend;
            /* 37 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Formation;
            /* 38 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Style;
            /* 39 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] HomeTextCol;
            /* 40 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] HomeBackCol;
            /* 41 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] AwayTextCol;
            /* 42 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] AwayBackCol;
            /* 43 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] _Division;
            /* 44 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] _LastDivision;
            /* 45 */ public byte LastPosition;
            /* 46 */ public int Cash;
            /* 47 */ public byte LeagueStandard;
            /* 48 */ public byte Under21;
            /* 49 */ public byte BTeam;
            /* 50 */ public byte Essential;
            /* 51 */ public int TransferRecord;
            /* 52 */ public byte ItalianGender;
            /* 53 */ public byte FrenchGender;
            /* 54 */ public byte PortugueseGender;

            public int UniqueID
            {
                get { return CM2.ConvertLongToNormalFormat(_UniqueID); }
                set { _UniqueID = CM2.ConvertLongToCM2Format(value); }
            }

            public string LongName
            {
                get { return MiscFunctions.GetTextFromBytes(UKLongName); }
                set { UKLongName = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string ShortName
            {
                get { return MiscFunctions.GetTextFromBytes(UKShortName); }
                set { UKShortName = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string Nation
            {
                get { return MiscFunctions.GetTextFromBytes(_Nation); }
                set { _Nation = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string Region
            {
                get { return MiscFunctions.GetTextFromBytes(_Region); }
                set { _Region = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string Division
            {
                get { return MiscFunctions.GetTextFromBytes(_Division); }
                set { _Division = MiscFunctions.GetBytesFromText(value, 15); }
            }
            public string LastDivision
            {
                get { return MiscFunctions.GetTextFromBytes(_LastDivision); }
                set { _LastDivision = MiscFunctions.GetBytesFromText(value, 15); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Manager
        {
            public int _UniqueID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] _FirstName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _SecondName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Nationality;
            public byte YearsInGame;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Favoured;
            public short MotivatingAbility;
            public short Judgement;
            public short Reputation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Formation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Style;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] CurrentClub;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] DateJoined;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] NationalJob;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] DateStarted;
            public byte PlayerManager;
            public byte BoardConfidence;

            public int UniqueID
            {
                get { return CM2.ConvertLongToNormalFormat(_UniqueID); }
                set { _UniqueID = CM2.ConvertLongToCM2Format(value); }
            }
            public string FirstName
            {
                get { return MiscFunctions.GetTextFromBytes(_FirstName); }
            }
            public string SecondName
            {
                get { return MiscFunctions.GetTextFromBytes(_SecondName); }
            }
            public string Team
            {
                get { return MiscFunctions.GetTextFromBytes(CurrentClub); }
            }
        }

        const int TeamDataStartPos = 895;
        const int PlayerDataStartPos = 666;
        const int ManagerDataStartPos = 268;

        public static void Test()
        {
            // CM0102 Load
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\2020_orig\Championship Manager 01-02\Data\index.dat");
            
            // To speed things up, make a ID -> StaffHistoryMap + Club ID -> ClubMap
            Dictionary<int, List<TStaffHistory>> staffHistoryMap = new Dictionary<int, List<TStaffHistory>>();
            foreach (var h in hl.staff_history)
            {
                if (!staffHistoryMap.ContainsKey(h.StaffID))
                    staffHistoryMap[h.StaffID] = new List<TStaffHistory>();
                staffHistoryMap[h.StaffID].Add(h);
            }
            Dictionary<int, TClub> clubMap = new Dictionary<int, TClub>();
            foreach (var c in hl.club)
            {
                clubMap[c.ID] = c;
            }

            var s = CM2.ConvertShortToCM2Format(12696);
            var s2 = CM2.ConvertLongToCM2Format(1146892696);
            var s3 = CM2.ConvertLongToNormalFormat(1146892696);
            var s4 = CM2.ConvertShortToNormalFormat(12696);
            var s5 = CM2.ConvertShortToNormalFormat(17500);

            // CM2 Load
            int CM9798PlayerSize = Marshal.SizeOf(typeof(CM9798Player));
            var tmdata = MiscFunctions.ReadFile<CM9798Team>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\ORIG\TMDATA.DB1", TeamDataStartPos);
            var pldata = MiscFunctions.ReadFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\ORIG\PLAYERS.DB1", PlayerDataStartPos);
            var mgdata = MiscFunctions.ReadFile<CM9798Manager>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\ORIG\MGDATA.DB1", ManagerDataStartPos);
            var plhist = new List<CM2.CM2History>();

            //WriteTeamDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM2\ORIG\TMDATA.CSV", tmdata);
            //WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM2\ORIG\PLAYERS.CSV", pldata);

            var pldataTest = MiscFunctions.ReadFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLAYERS.DB1", PlayerDataStartPos);
            var maxidxxx = pldata.Max(x => x.UniqueID);

            // Remove all Player Managers
            foreach (var manager in mgdata.Where(x => x.PlayerManager == 1))
                manager.PlayerManager = 0;

            List<TClub> cm0102clubs = new List<TClub>();

            bool UpdateEnglishLeagues = true;
            bool UpdateItalianLeagues = true;
            bool UpdateSpanishLeagues = true;
            bool UpdateFrenchLeagues = true;
            bool UpdateGermanLeagues = true;
            bool UpdateScottishLeagues = true;

            List<string> cm2premTeams, cm2firstDivTeams, cm2secondDivTeams, cm2thirdDivTeams, cm2fourthDivTeams;
            List<string> cm2SerieATeams, cm2SerieBTeams, cm2SerieCTeams;
            List<string> cm2SpainFirstTeams, cm2SpainSecondTeams, cm2SpainNationalTeams;
            List<string> cm2FranceFirstTeams, cm2FranceSecondTeams, cm2FranceNationalTeams;
            List<string> cm2GermanFirstTeams, cm2GermanSecondTeams, cm2GermanNationalTeams;
            List<string> cm2ScotlandPremierTeams, cm2ScotlandFirstTeams, cm2ScotlandSecondTeams, cm2ScotlandThirdTeams, cm2ScotlandNationalTeams;

            if (UpdateEnglishLeagues)
            {
                // English CM0102 Teams
                var cm0102premTeams = CM2.ReadCM0102League(hl, "English Premier Division", cm0102clubs); // 20
                var cm0102firstDivTeams = CM2.ReadCM0102League(hl, "English First Division", cm0102clubs); // 24
                var cm0102secondDivTeams = CM2.ReadCM0102League(hl, "English Second Division", cm0102clubs); // 24
                var cm0102thirdDivTeams = CM2.ReadCM0102League(hl, "English Third Division", cm0102clubs); // 24

                // English CM9798 Teams
                cm2premTeams = tmdata.Where(x => x.Division == "EPR").Select(x => x.LongName).ToList();         // 20
                cm2firstDivTeams = tmdata.Where(x => x.Division == "ED1").Select(x => x.LongName).ToList();     // 24
                cm2secondDivTeams = tmdata.Where(x => x.Division == "ED2").Select(x => x.LongName).ToList();    // 24
                cm2thirdDivTeams = tmdata.Where(x => x.Division == "ED3").Select(x => x.LongName).ToList();     // 24
                cm2fourthDivTeams = tmdata.Where(x => x.Division == "ENL").Select(x => x.LongName).ToList();    // 52
            }

            if (UpdateItalianLeagues)
            {
                // Italian CM0102 Teams
                var cm0102SerieATeams = CM2.ReadCM0102League(hl, "Italian Serie A", cm0102clubs); // 18
                var cm0102SerieBTeams = CM2.ReadCM0102League(hl, "Italian Serie B", cm0102clubs); // 20
                var cm0102SerieC1ATeams = CM2.ReadCM0102League(hl, "Italian Serie C1/A", cm0102clubs); // 18
                var cm0102SerieC1BTeams = CM2.ReadCM0102League(hl, "Italian Serie C1/B", cm0102clubs); // 18

                // Italian CM9798 Teams
                cm2SerieATeams = tmdata.Where(x => x.Division == "ISA").Select(x => x.LongName).ToList();       // 18
                cm2SerieBTeams = tmdata.Where(x => x.Division == "ISB").Select(x => x.LongName).ToList();       // 20
                cm2SerieCTeams = tmdata.Where(x => x.Division == "ISC").Select(x => x.LongName).ToList();       // 48
            }

            if (UpdateSpanishLeagues)
            {
                // Spanish CM0102
                var cm0102SpainFirstTeams = CM2.ReadCM0102League(hl, "Spanish First Division", cm0102clubs);        // 20
                var cm0102SpainSecondTeams = CM2.ReadCM0102League(hl, "Spanish Second Division", cm0102clubs);      // 22

                // Spanish CM9798 Teams
                cm2SpainFirstTeams = tmdata.Where(x => x.Division == "SP1").Select(x => x.LongName).ToList();        // 20
                cm2SpainSecondTeams = tmdata.Where(x => x.Division == "SP2").Select(x => x.LongName).ToList();       // 22
                cm2SpainNationalTeams = tmdata.Where(x => x.Division == "SPN").Select(x => x.LongName).ToList();     // 66
            }

            if (UpdateFrenchLeagues)
            {
                // French CM0102
                var cm0102FrenchFirstTeams = CM2.ReadCM0102League(hl, "French First Division", cm0102clubs);        // 18
                var cm0102FrenchSecondTeams = CM2.ReadCM0102League(hl, "French Second Division", cm0102clubs);      // 20

                // French CM9798 Teams
                cm2FranceFirstTeams = tmdata.Where(x => x.Division == "FD1").Select(x => x.LongName).ToList();        // 18
                cm2FranceSecondTeams = tmdata.Where(x => x.Division == "FD2").Select(x => x.LongName).ToList();       // 22
                cm2FranceNationalTeams = tmdata.Where(x => x.Division == "FNL").Select(x => x.LongName).ToList();     // 26
            }

            if (UpdateGermanLeagues)
            {
                // German CM0102
                var cm0102GermanFirstTeams = CM2.ReadCM0102League(hl, "German First Division", cm0102clubs);        // 18
                var cm0102GermanSecondTeams = CM2.ReadCM0102League(hl, "German Second Division", cm0102clubs);      // 18

                // German CM9798 Teams
                cm2GermanFirstTeams = tmdata.Where(x => x.Division == "GD1").Select(x => x.LongName).ToList();        // 18
                cm2GermanSecondTeams = tmdata.Where(x => x.Division == "GD2").Select(x => x.LongName).ToList();       // 18
                cm2GermanNationalTeams = tmdata.Where(x => x.Division == "GNL").Select(x => x.LongName).ToList();     // 56
            }

            if (UpdateScottishLeagues)
            {
                // Scotland CM0102
                var cm0102ScotlandPremierTeams = CM2.ReadCM0102League(hl, "Scottish Premier Division", cm0102clubs);      // 12
                var cm0102ScotlandFirstTeams = CM2.ReadCM0102League(hl, "Scottish First Division", cm0102clubs);          // 10
                var cm0102ScotlandSecondTeams = CM2.ReadCM0102League(hl, "Scottish Second Division", cm0102clubs);        // 10
                var cm0102ScotlandThirdTeams = CM2.ReadCM0102League(hl, "Scottish Third Division", cm0102clubs);          // 10

                // Scotland CM9798 Teams
                cm2ScotlandPremierTeams = tmdata.Where(x => x.Division == "SPR").Select(x => x.LongName).ToList();    // 10
                cm2ScotlandFirstTeams = tmdata.Where(x => x.Division == "SD1").Select(x => x.LongName).ToList();      // 10
                cm2ScotlandSecondTeams = tmdata.Where(x => x.Division == "SD2").Select(x => x.LongName).ToList();     // 10
                cm2ScotlandThirdTeams = tmdata.Where(x => x.Division == "SD3").Select(x => x.LongName).ToList();      // 10
                cm2ScotlandNationalTeams = tmdata.Where(x => x.Division == "SNL").Select(x => x.LongName).ToList();   // 27
            }

            var EnglishLeagues = tmdata.Where(x => x.Nation == "England").Select(x => x.Division).Distinct().ToList();
            var ItalianLeagues = tmdata.Where(x => x.Nation == "Italy").Select(x => x.Division).Distinct().ToList();
            var SpanishLeagues = tmdata.Where(x => x.Nation == "Spain").Select(x => x.Division).Distinct().ToList();
            var FranceLeagues = tmdata.Where(x => x.Nation == "France").Select(x => x.Division).Distinct().ToList();
            var GermanLeagues = tmdata.Where(x => x.Nation == "Germany").Select(x => x.Division).Distinct().ToList();
            var ScotlandLeagues = tmdata.Where(x => x.Nation == "Scotland").Select(x => x.Division).Distinct().ToList();

            // Remove Divison from CM9798 Teams
            if (UpdateEnglishLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "ENL", "EPR", "ED1", "ED2", "ED3");
            if (UpdateItalianLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "ISC", "ISA", "ISB", "ISC");
            if (UpdateSpanishLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "SPN", "SP1", "SP2", "SPN");
            if (UpdateFrenchLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "FNL", "FD1", "FD2", "FNL");
            if (UpdateGermanLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "GNL", "GD1", "GD2", "GNL");
            if (UpdateScottishLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "SNL", "SPR", "SD1", "SD2", "SD3");

            var newID = pldata.Max(x => x.UniqueID) + 1;
            var newTeamID = tmdata.Max(x => x.UniqueID) + 1;

            foreach (var cm0102team in cm0102clubs)
            {
                var cm2team = GetTeamFromCM0102Team(tmdata, cm0102team, 1);
               // Console.WriteLine("Processing: {0} - Matched: {1}", MiscFunctions.GetTextFromBytes(cm0102team.Name), cm2team == null ? "DIDNT MATCH" : cm2team.LongName);

                if (cm2team == null)
                {
                    Console.WriteLine("CREATING UNKNOWN TEAM: {0} in DIVISION: {1}", MiscFunctions.GetTextFromBytes(cm0102team.Name), DivisionMapper(hl, cm0102team));
                    cm2team = CreateUnknownTeam(newTeamID++, MiscFunctions.GetTextFromBytes(cm0102team.Name), MiscFunctions.GetTextFromBytes(cm0102team.ShortName), (byte)(cm0102team.Reputation/500), MiscFunctions.GetTextFromBytes(hl.nation[cm0102team.Nation].Name));
                    tmdata.Add(cm2team);
                }

                if (cm2team != null)
                {
                    Console.WriteLine("{0} - Was {1} now {2}", cm2team.LongName, cm2team.Division, DivisionMapper(hl, cm0102team));
                    // Set Division
                    cm2team.Division = DivisionMapper(hl, cm0102team);
                    cm2team.LastDivision = DivisionMapper(hl, cm0102team, true);
                    cm2team.LastPosition = (cm2team.Division.Contains("3") || cm2team.Division.EndsWith("C")) ? (byte)0 : (byte)cm0102team.LastPosition;        // Weird oddity where Barrow in Division 3 is in the Champions League
                    
                    // Don't make a team non-essential if it was essential in the original game
                    if (cm2team.Essential != 1)
                        cm2team.Essential = (cm2team.Division.Contains("3") || cm2team.Division.EndsWith("C")) ? (byte)0 : (byte)1; // Don't make Division 3 teams essential

                    // Hack to ensure Newport and other clubs go into Division 3. Else you'll get team "Free Transfer"
                    if (cm2team.Division == "ED3")
                        cm2team.Nation = "England";

                    // Delete all the players from the CM2 Team (Don't use a fuzzy search with U23 or U19 teams as could delete all the players in the main team!
                    if (cm2team.LongName.Contains("U23") || cm2team.LongName.Contains("U19"))
                        pldata.RemoveAll(x => x.Team == cm2team.LongName);
                    else
                        pldata.RemoveAll(x => MiscFunctions.StringCompare(x.Team, cm2team.ShortName, cm2team.LongName));

                    // Find all the CM0102 Players of that team
                    var cm0102players = hl.staff.Where(x => x.ClubJob == cm0102team.ID && x.Player != -1).OrderByDescending(x => hl.players[x.Player].CurrentReputation).ToList();

                    int plCount = 0;
                    foreach (var cm0102player in cm0102players)
                    {
                        var newPlayer = CM0102PlayerTo9798(newID++, ref newTeamID, hl, cm0102player.ID, string.IsNullOrEmpty(cm2team.ShortName) ? cm2team.LongName : cm2team.ShortName, staffHistoryMap, clubMap, plhist, tmdata);
                        if (newPlayer != null)
                        {
                            pldata.Add(newPlayer);
                        }
                        plCount++;
                        if (plCount == 32)
                            break;
                    }
                }
                else
                    Console.WriteLine("COULD NOT FIND TEAM: {0}", MiscFunctions.GetTextFromBytes(cm0102team.Name));
            }

            if (UpdateEnglishLeagues)
            {
                // Count English Teams
                cm2premTeams = GetCM9798TeamNamesByDivision(tmdata, "EPR");          // 20
                cm2firstDivTeams = GetCM9798TeamNamesByDivision(tmdata, "ED1");      // 24
                cm2secondDivTeams = GetCM9798TeamNamesByDivision(tmdata, "ED2");     // 24
                cm2thirdDivTeams = GetCM9798TeamNamesByDivision(tmdata, "ED3");      // 24
                DivisionTeamCutOff(tmdata, "ENL", 52);
                cm2fourthDivTeams = GetCM9798TeamNamesByDivision(tmdata, "ENL");     // Should be 52
            }

            if (UpdateItalianLeagues)
            {
                // Count Italian Teams
                cm2SerieATeams = GetCM9798TeamNamesByDivision(tmdata, "ISA");        // 18
                cm2SerieBTeams = GetCM9798TeamNamesByDivision(tmdata, "ISB");        // 20
                DivisionTeamCutOff(tmdata, "ISC", 48);
                cm2SerieCTeams = GetCM9798TeamNamesByDivision(tmdata, "ISC");        // 48
            }

            if (UpdateSpanishLeagues)
            {
                // Count Spanish Teams
                cm2SpainFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "SP1");        // 18
                cm2SpainSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "SP2");       // 20
                DivisionTeamCutOff(tmdata, "SPN", 66);
                cm2SpainNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "SPN");     // 66
            }

            if (UpdateFrenchLeagues)
            {
                // Count French Teams
                cm2FranceFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "FD1");        // 18
                AddTeamsToLeague(tmdata, "FD2", "FNL", 2);
                cm2FranceSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "FD2");       // 22
                DivisionTeamCutOff(tmdata, "FNL", 26);
                cm2FranceNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "FNL");     // 26
            }

            if (UpdateGermanLeagues)
            {
                // Count German Teams
                cm2GermanFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "GD1");        // 18
                cm2GermanSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "GD2");       // 18
                DivisionTeamCutOff(tmdata, "GNL", 56);
                cm2GermanNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "GNL");     // 56
            }

            if (UpdateScottishLeagues)
            {
                // Count Scottish Teams
                DivisionTeamCutOff(tmdata, "SPR", 10, "SNL");
                cm2ScotlandPremierTeams = GetCM9798TeamNamesByDivision(tmdata, "SPR");          // 12 (should be 10)
                cm2ScotlandFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "SD1");      // 10
                cm2ScotlandSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "SD2");     // 10
                cm2ScotlandThirdTeams = GetCM9798TeamNamesByDivision(tmdata, "SD3");      // 10
                DivisionTeamCutOff(tmdata, "SNL", 27);
                cm2ScotlandNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "SNL");     // Should be 27
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "SPR", "SD1", "SD2", "SD3", "SNL");
            }

            // This is needed at least for the EPR as if you load the Scottish league without the English league, you can have errors where the European Cups will try and find the 2nd place team
            // In the EPR and not find it
            ListDivisionAndFixLastPositions(tmdata, "EPR");
            ListDivisionAndFixLastPositions(tmdata, "SPR");

            MiscFunctions.SaveFile<CM9798Team>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\TMDATA.DB1", tmdata, TeamDataStartPos);
            MiscFunctions.SaveFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLAYERS.DB1", pldata, PlayerDataStartPos, true);
            MiscFunctions.SaveFile<CM9798Manager>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\MGDATA.DB1", mgdata, ManagerDataStartPos);

            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM2\TMDATA.DB1", TeamDataStartPos-8, tmdata.Count, true);
            //CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM2\TMDATA.DB1", TeamDataStartPos-2, tmdata.Count);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLAYERS.DB1", 660, pldata.Count, false);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM2\MGDATA.DB1", ManagerDataStartPos-8, mgdata.Count, true);
            CM2.WriteCM2HistoryFile(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLHIST98.BIN", plhist, true);

            WriteTeamDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM2\TMDATA.CSV", tmdata);
            WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLAYERS.CSV", pldata);

            Console.WriteLine("Player Count: {0}", pldata.Count);
            Console.WriteLine("Team Count: {0}", tmdata.Count);
       }

        static void ListDivisionAndFixLastPositions(List<CM9798Team> tmdata, string division)
        {
            Console.WriteLine(division);
            Console.WriteLine("----------------");
            var cm2teams = tmdata.Where(x => x.Division == division).OrderBy(x => x.LastPosition).ToList();
            byte pos = 1;
            foreach (var cm2team in cm2teams)
            {
                Console.WriteLine("{0}. {1} (Last Division: {2})", cm2team.LastPosition, cm2team.LongName, cm2team.LastDivision);
                cm2team.LastPosition = pos++;
            }
        }

        static void CheckLeaguesTeamsHavePlayers(List<CM9798Team> tmdata, List<CM9798Player> pldata, params string[] divisions)
        {
            foreach (var division in divisions)
            {
                var cm2teams = tmdata.Where(x => x.Division == division).ToList();
                foreach (var cm2team in cm2teams)
                {
                    var players = pldata.Where(x => MiscFunctions.StringCompare(x.Team, cm2team.ShortName, cm2team.LongName)).ToList();
                    if (players.Count() == 0)
                        Console.WriteLine("************* NO PLAYERS IN TEAM {0} IN DIVISION {1} *************", cm2team.LongName, division);
                    if (players.Count() > 32)
                        Console.WriteLine("************* TOO MANY PLAYERS IN TEAM {0} IN DIVISION {1} *************", cm2team.LongName, division);
                }
            }
        }

        static List<string> GetCM9798TeamNamesByDivision(List<CM9798Team> tmdata, string division)
        {
            return tmdata.Where(x => x.Division == division).Select(x => x.LongName).ToList();
        }

        static void AddTeamsToLeague(List<CM9798Team> tmdata, string destinationDivision, string sourceDestination, int numberOfTeams)
        {
            var cm2Teams = tmdata.Where(x => x.Division == sourceDestination).OrderByDescending(x => x.Reputation).Take(numberOfTeams).ToList();
            foreach (var cm2team in cm2Teams)
                cm2team.Division = destinationDivision;
        }

        static void DivisionTeamCutOff(List<CM9798Team> tmdata, string division, int teams, string replacementDivision = "")
        {
            var cm2Teams = tmdata.Where(x => x.Division == division).OrderByDescending(x => x.Reputation).ToList();
            for (int i = 0; i < cm2Teams.Count; i++)
            {
                if (i >= teams)
                {
                    cm2Teams[i].Division = replacementDivision;
                    Console.WriteLine("Cutting team {0} from division {1} (replacing with: {2})", cm2Teams[i].LongName, division, replacementDivision);
                }
            }
        }

        static void RemoveTeamsAndPlayersFromDivision(List<CM9798Team> tmdata, List<CM9798Player> pldata, string defaultDivision, params string [] divisions)
        {
            foreach (var cm2team in tmdata.Where(x => divisions.Contains(x.Division) || x.Division == defaultDivision))
            {
                if (cm2team.Division != defaultDivision /*&& defaultDivision != null*/)
                {
                    cm2team.Division = defaultDivision;
                    cm2team.LastDivision = defaultDivision;
                    cm2team.LastPosition = 0;
                }
                //if (defaultDivision != null)
                pldata.RemoveAll(x => MiscFunctions.StringCompare(x.Team, cm2team.ShortName, cm2team.LongName));
            }
        }

        static string DivisionMapper(HistoryLoader hl, TClub cm0102club, bool mapLastDivisionInstead = false)
        {
            string ret = "";
            var cm0102division = MiscFunctions.GetTextFromBytes(hl.club_comp[mapLastDivisionInstead ? cm0102club.LastDivision : cm0102club.Division].Name);
            switch (cm0102division)
            {
                // England
                case "English Premier Division":
                    ret = "EPR";
                    break;
                case "English First Division":
                    ret = "ED1";
                    break;
                case "English Second Division":
                    ret = "ED2";
                    break;
                case "English Third Division":
                    ret = "ED3";
                    break;
                case "English Conference":
                    ret = "ENL";
                    break;

                // Italy
                case "Italian Serie A":
                    ret = "ISA";
                    break;
                case "Italian Serie B":
                    ret = "ISB";
                    break;
                case "Italian Serie C1/A":
                case "Italian Serie C1/B":
                case "Italian Serie C2/A":
                case "Italian Serie C2/B":
                case "Italian Serie C2/C":
                    ret = "ISC";
                    break;

                // Spain
                case "Spanish First Division":
                    ret = "SP1";
                    break;
                case "Spanish Second Division":
                    ret = "SP2";
                    break;
                case "Spanish Second Division B":
                case "Spanish Second Division B1":
                case "Spanish Second Division B2":
                case "Spanish Second Division B3":
                case "Spanish Second Division B4":
                case "Spanish Lower Division":
                    ret = "SPN";
                    break;

                // France
                case "French First Division":
                    ret = "FD1";
                    break;
                case "French Second Division":
                    ret = "FD2";
                    break;
                case "French National":
                case "French CFA":
                    ret = "FNL";
                    break;

                // German
                case "German First Division":
                    ret = "GD1";
                    break;
                case "German Second Division":
                    ret = "GD2";
                    break;
                case "German Regional":
                case "German Regional Division East":
                case "German Regional Division North":
                case "German Regional Division South":
                case "German Regional Division West/Southwest":
                    ret = "GNL";
                    break;

                // Scotland
                case "Scottish Premier Division":
                    ret = "SPR";
                    break;
                case "Scottish First Division":
                    ret = "SD1";
                    break;
                case "Scottish Second Division":
                    ret = "SD2";
                    break;
                case "Scottish Third Division":
                    ret = "SD3";
                    break;
                case "Scottish Conference":
                    ret = "SNL";
                    break;

                default:
                    Console.WriteLine("********* COULD NOT MAP DIVISION!! ({0})*********", cm0102division);
                    break;
            }
            return ret;
        }

        static CM9798Team GetTeamFromCM0102Team(List<CM9798Team> tmdata, TClub cm0102club, int calledFrom = 0)
        {
            var cm0102TeamName = MiscFunctions.GetTextFromBytes(cm0102club.Name);
            var cm0102ShortTeamName = MiscFunctions.GetTextFromBytes(cm0102club.ShortName);

            // Remove accents from team names
            cm0102TeamName = MiscFunctions.RemoveDiacritics(cm0102TeamName);
            cm0102ShortTeamName = MiscFunctions.RemoveDiacritics(cm0102ShortTeamName);

            // Make sure you do not match against a blank
            if (string.IsNullOrEmpty(cm0102ShortTeamName))
                cm0102ShortTeamName = "DONTMATCH";

            // Hack for "St. " vs "St."
            if (cm0102ShortTeamName.StartsWith("St."))
                cm0102ShortTeamName = cm0102ShortTeamName.Replace(" ", "");

            // Some special mappings between team names where CM2 and CM0102 differ
            var extraCheck = CM2.TeamMapper(cm0102TeamName);

            var returnTeam = tmdata.FirstOrDefault(x => x.Nation != "EXTINCT" && (x.LongName == cm0102TeamName || x.LongName == cm0102ShortTeamName || x.ShortName == cm0102ShortTeamName || x.LongName == extraCheck || x.ShortName == extraCheck));
            return returnTeam;
        }

        static CM9798Player CM0102PlayerTo9798(int newPlayerID, ref int newTeamID, HistoryLoader hl, int playerId, string setTeamTo, Dictionary<int, List<TStaffHistory>> staffHistoryMap, Dictionary<int, TClub> clubMap, List<CM2.CM2History> plhist, List<CM9798Team> tmdata, int yearModifier = -5)
        {
            var s = hl.staff[playerId];

            var firstName = MiscFunctions.GetTextFromBytes(hl.first_names[s.FirstName].Name);
            var secondName = MiscFunctions.GetTextFromBytes(hl.second_names[s.SecondName].Name);
            var commonName = MiscFunctions.GetTextFromBytes(hl.common_names[s.CommonName].Name);

            // Remove accents
            /*
            firstName = MiscFunctions.RemoveDiacritics(firstName);
            secondName = MiscFunctions.RemoveDiacritics(secondName);
            commonName = MiscFunctions.RemoveDiacritics(commonName);
            */

            // Have to cut to 20 letter (even though the max you get from CM0102 is 25)
            // Any more and you get addname 1 (maybe)
            firstName = firstName.Substring(0, Math.Min(39, firstName.Length));
            secondName = secondName.Substring(0, Math.Min(34, secondName.Length));
            commonName = commonName.Substring(0, Math.Min(34, commonName.Length));

            //if (s.Nation == -1)
            //    continue;

            var nation = MiscFunctions.GetTextFromBytes(hl.nation[s.Nation].Name);
            //nation = MiscFunctions.RemoveDiacritics(nation);

            var team = setTeamTo;
            DateTime dob;
            string birthdate = "";
            int age;
            if (s.DateOfBirth.Year != 0)
            {
                dob = TCMDate.ToDateTime(s.DateOfBirth).AddYears(yearModifier);
                birthdate = dob.ToString("d.M.yy");
                age = (2001 - dob.Year) + yearModifier;
            }
            else
            {
                age = 2001 - (s.YearOfBirth - yearModifier);
            }

            var p = hl.players[s.Player];

            var goalkeeper = CM2.ConvertPosition(p.Goalkeeper); ;
            var sweeper = CM2.ConvertPosition(p.Sweeper);
            var defence = CM2.ConvertPosition(p.Defender);
            var anchor = CM2.ConvertPosition(p.DefensiveMidfielder);
            var midfield = CM2.ConvertPosition(p.Midfielder);
            var support = CM2.ConvertPosition(p.AttackingMidfielder);
            var attack = p.Attacker == 20 ? 2 : 0;
            var rightsided = CM2.ConvertSide(p.RightSide);
            var leftsided = CM2.ConvertSide(p.LeftSide);
            var centralsided = CM2.ConvertSide(p.Central);

            var ability = p.CurrentAbility;
            var potential = ((short)p.PotentialAbility) < 0 ? -1 : p.PotentialAbility;
            var reputation = p.CurrentReputation;

            var Aggression = p.Aggression;
            var BigOccasion = p.ImportantMatches;
            var Character = s.Temperament;
            var Consistency = p.Consistency;
            var Creativity = p.Vision;
            var Determination = s.Determination;
            var Dirtiness = p.Dirtiness;
            var Dribbling = p.Dribbling;
            var Flair = p.Flair;
            var Heading = p.Heading;
            var Influence = p.Leadership;
            var InjProne = p.InjuryProneness;
            var Adaptability = p.Decisions;
            var Marking = p.Marking;
            var OffTheBall = p.Movement;
            var Pace = p.PlayerPace;
            var Passing = p.Passing;
            var Positioning = p.Positioning;
            var SetPieces = p.Crossing;
            var Shooting = p.Finishing;
            var Stamina = p.Stamina;
            var Strength = p.Strength;
            var Tackling = p.Tackling;
            var Technique = p.Technique;

            var DateJoinedClub = s.DateJoinedClub.Year == 0 ? "" : TCMDate.ToDateTime(s.DateJoinedClub).AddYears(yearModifier).ToString("M.yy");
            var ContractEnd = s.DateExpiresClub.Year == 0 ? "" : TCMDate.ToDateTime(s.DateExpiresClub).AddYears(yearModifier).ToString("M.yy");

            // Nation Mappings
            if (nation == "Serbia" || nation == "Bosnia-Herzegovina" || nation == "Montenegro" || nation == "North Macedonia" || nation == "Kosovo")
                nation = "Yugoslavia";
            if (nation == "Gabon")
                nation = "France";
            if (nation == "Democratic Republic of Congo" || nation == "Tanzania")
                nation = "Zaire";
            if (nation == "Mali")
                nation = "Senegal";
            if (nation == "Benin")
                nation = "Nigeria";
            if (nation == "Burkina Faso")
                nation = "Nigeria";
            if (nation == "Zaire" || nation == "The Congo")
                nation = "Republic of Congo";
            if (nation == "The Gambia")
                nation = "Gambia";
            if (nation == "Pays Basque")
                nation = "France";
            if (nation == "French Guiana")
                nation = "France";

            var nationsToBeMapped = new string[] {
                    "Curaçao",
                    "Cuba",
                    "Réunion",
                    "Namibia",
                    "Samoa",
                    "The Philippines",
                    "Montserrat",
                    "Surinam",
                    "Curaçao",
                    "Antigua & Barbuda",
                    "Cape Verde Islands",
                    "St Kitts & Nevis",
                    "Oman",
                    "Martinique",
                    "Sierra Leone",
                    "Saint Lucia",
                    "Indonesia",
                    "Pakistan",
                    "Sudan",
                    "Grenada",
                    "Guyana",
                    "Guinea-Bissau",
                    "St Kitts & Nevis",
                    "Gibraltar",
                    "Seville",
                    "Comoros"
                };
            if (nationsToBeMapped.Contains(nation))
                nation = "France";

            var newPlayer = new CM9798Player();
            newPlayer.UniqueID = newPlayerID;

            if (string.IsNullOrEmpty(commonName))
            {
                newPlayer.FirstName = firstName;
                newPlayer.SecondName = secondName;
            }
            else
                newPlayer.SecondName = commonName;
            newPlayer.Nationality = nation;
            newPlayer.Team = team;
            newPlayer.BirthDate = birthdate;
            newPlayer.Age = (byte)age;

            newPlayer.Goalkeeper = (byte)goalkeeper;
            newPlayer.Sweeper = (byte)sweeper;
            newPlayer.Defence = (byte)defence;
            newPlayer.Anchor = (byte)anchor;
            newPlayer.Midfield = (byte)midfield;
            newPlayer.Support = (byte)support;
            newPlayer.Attack = (byte)attack;
            newPlayer.RightSided = (byte)rightsided;
            newPlayer.LeftSided = (byte)leftsided;
            newPlayer.CentralSided = (byte)centralsided;

            newPlayer.Ability = CM2.ConvertShortToCM2Format((short)ability);
            newPlayer.Potential = CM2.ConvertShortToCM2Format((short)potential);
            newPlayer.Reputation = CM2.ConvertShortToCM2Format((short)reputation);

            newPlayer.Adaptability = (byte)Adaptability;
            newPlayer.Aggression = (byte)Aggression;
            newPlayer.BigOccasion = (byte)BigOccasion;
            newPlayer.Character = (byte)Character;
            newPlayer.Consistency = (byte)Consistency;
            newPlayer.Creativity = (byte)Creativity;
            newPlayer.Determination = (byte)Determination;
            newPlayer.Dirtyness = (byte)Dirtiness;
            newPlayer.Dribbling = (byte)Dribbling;
            newPlayer.Flair = (byte)Flair;
            newPlayer.Heading = (byte)Heading;
            newPlayer.Influence = (byte)Influence;
            newPlayer.InjProne = (byte)InjProne;
            newPlayer.Marking = (byte)Marking;
            newPlayer.OffTheBall = (byte)OffTheBall;
            newPlayer.Pace = (byte)Pace;
            newPlayer.Passing = (byte)Passing;
            newPlayer.Positioning = (byte)Positioning;
            newPlayer.SetPieces = (byte)SetPieces;
            newPlayer.Shooting = (byte)Shooting;
            newPlayer.Stamina = (byte)Stamina;
            newPlayer.Strength = (byte)Strength;
            newPlayer.Tackling = (byte)Tackling;
            newPlayer.Technique = (byte)Technique;

            newPlayer.DateJoined = DateJoinedClub;
            newPlayer.ContractEnds = ContractEnd;

            // Get History
            if (staffHistoryMap.ContainsKey(s.ID) && s.DateOfBirth.Year != 0)
            {
                var cm0102player_history = staffHistoryMap[s.ID].OrderBy(x => x.Year).ToList();
                var history = new CM2.CM2History();
                history.Name = newPlayer.SecondName + "," + newPlayer.FirstName;
                history.SetBirthDate(TCMDate.ToDateTime(s.DateOfBirth).AddYears(yearModifier));
                history.Nation = nation;
                foreach (var h in cm0102player_history)
                {
                    CM2.CM2History.CM2HistoryDetails detail = new CM2.CM2History.CM2HistoryDetails();
                    detail.Year = (byte)(((h.Year - 1900) + yearModifier) + 1);

                    if (detail.Year >= 99 /*|| detail.Year <= 94*/)
                        continue;

                    if (clubMap.ContainsKey(h.ClubID))
                    {
                        var cm2team = GetTeamFromCM0102Team(tmdata, clubMap[h.ClubID]);
                        
                        var cm0102TeamName = MiscFunctions.GetTextFromBytes(clubMap[h.ClubID].Name);
                        var cm0102ShortTeamName = MiscFunctions.GetTextFromBytes(clubMap[h.ClubID].ShortName);
                        /*
                        // Remove accents from team names
                        cm0102TeamName = MiscFunctions.RemoveDiacritics(cm0102TeamName);
                        cm0102ShortTeamName = MiscFunctions.RemoveDiacritics(cm0102ShortTeamName);

                        // Get CM2 Short Name
                        var CM2ShortTeamName = "DONTMATCH";
                        var cm2TeamViaShortName = tmdata.FirstOrDefault(x => x.ShortName == cm0102ShortTeamName);

                        if (cm2TeamViaShortName != null)
                            CM2ShortTeamName = cm2TeamViaShortName.ShortName;

                        // Some special mappings between team names where CM2 and CM0102 differ
                        var extraCheck = CM2.TeamMapper(cm0102TeamName);

                        // Get the original team name, let's try and keep that
                        var tmDataIndex = tmdata.FindIndex(x => x.LongName == cm0102TeamName || x.LongName == cm0102ShortTeamName || x.ShortName == CM2ShortTeamName || x.LongName == extraCheck || x.ShortName == extraCheck);
                        */

                        // Now check that team exists in CM2
                        if (cm0102TeamName != setTeamTo && cm2team == null)
                        {
                            if (tmdata.Count() < 2050)
                            {
                                tmdata.Insert(100, CreateUnknownTeam(newTeamID++, cm0102TeamName, cm0102ShortTeamName));
                                detail.Team = cm0102ShortTeamName;
                            }
                            else
                                detail.Team = "Unknown";
                        }
                        else
                        {
                            if (cm2team == null)
                                detail.Team = setTeamTo;
                            else
                                detail.Team = string.IsNullOrEmpty(cm2team.ShortName) ? cm2team.LongName : cm2team.ShortName;
                        }
                    }
                    else
                        detail.Team = "Unknown";

                    if (h.OnLoan != 0)
                        detail.Team = "*" + detail.Team;

                    detail.Goals = (byte)h.Goals;
                    detail.Apps = (byte)h.Apps;

                    // if already have the year, then skip it
                    if (history.Details.Where(x => x.Year == detail.Year).Count() == 0)
                        history.Details.Add(detail);
                }

                // Clean history of "Unknown" teams if at end of history
                int cutFrom = 0;
                for (int i = 0; i < history.Details.Count; i++)
                {
                    if (history.Details[i].Team == "Unknown")
                        cutFrom++;
                    else
                        break;
                }
                history.Details = history.Details.GetRange(cutFrom, history.Details.Count - cutFrom);

                plhist.Add(history);
            }


            return newPlayer;
        }

        public static CM9798Team CreateUnknownTeam(int uniqueID, string teamName = "Unknown", string teamShortName = "Unknown", byte Reputation = 7, string Nation = "Finland")
        {
            CM9798Team t = new CM9798Team();

            t.UniqueID = uniqueID;
            t.LongName = teamName;
            t.ShortName = teamShortName;
            t.Nation = Nation;
            t.Stadium = MiscFunctions.GetBytesFromText(teamShortName + " Stadium", 35);
            t.Capacity = CM2.ConvertLongToCM2Format(50000);
            t.Seating = CM2.ConvertLongToCM2Format(50000);
            t.Following = 10;
            t.Blend = 12; 
            t.Essential = 0;
            t.Reputation = Reputation;
            t.XCoord = 10;
            t.YCoord = 10;
            t.City = MiscFunctions.GetBytesFromText(teamShortName, 35);
            t.Style = MiscFunctions.GetBytesFromText("PASS", 10);
            t.HomeTextCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.HomeBackCol = MiscFunctions.GetBytesFromText("BLU", 15);
            t.AwayTextCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.AwayBackCol = MiscFunctions.GetBytesFromText("GRN", 15);
            t.Formation = MiscFunctions.GetBytesFromText("442N", 10);
            t.Division = "";
            t.LastDivision = "";
            t.LastPosition = 12;
            t.Cash = CM2.ConvertLongToCM2Format(100);

            return t;
        }

        static void WritePlayerDataToCSV(string fileName, List<CM9798Player> pldata)
        {
            using (var sw = new StreamWriter(fileName))
            {
                MiscFunctions.WriteCSVLine(sw, "FirstName", "SecondName", "Nationality", "NationalCaps", "NationalGoals", "Team", "BirthDate",
                      "Age", "Goalkeeper", "Sweeper", "Defence", "Anchor", "Midfield", "Support", "Attack", "RightSided", "LeftSided", "CentralSided", "Ability", "Potential", "Reputation",
                      "Adaptability", "Aggression", "BigOccasion", "Character", "Consistency", "Creativity", "Determination", "Dirtyness", "Dribbling", "Flair", "Heading", "Influence", "InjProne", "Marking", "OffTheBall", "Pace", "Passing",
                      "Positioning", "SetPieces", "Shooting", "Stamina", "Strength", "Tackling", "Technique");
                foreach (var p in pldata)
                    WritePlayer(sw, p);
            }
        }

        static void WriteTeamDataToCSV(string fileName, List<CM9798Team> tmdata)
        {
            using (var sw = new StreamWriter(fileName))
            {
                MiscFunctions.WriteCSVLine(sw, "LongName", "ShortName", "Nation", "Region", "Developed", "XCoord", "YCoord", "EEC", "TCoef8893",
                          "City", "Stadium", "Capacity", "Seating", "Following", "Reputation", "Blend", "Formation", "Style", "HomeTextCol", "HomeBackCol", "AwayTextCol", "AwayBackCol", "Division",
                          "LastDivision", "LastPosition", "Cash", "LeagueStandard", "Under21", "BTeam", "Essential", "TransferRecord");
                foreach (var t in tmdata)
                    WriteTeam(sw, t);
            }
        }

        static void WritePlayer(StreamWriter sw, CM9798Player player)
        {
            MiscFunctions.WriteCSVLine(sw, player.FirstName, player.SecondName, player.Nationality, player.NationalCaps, player.NationalGoals, player.Team, player.BirthDate,
                      player.Age, player.Goalkeeper, player.Sweeper, player.Defence, player.Anchor, player.Midfield, player.Support, player.Attack, player.RightSided, player.LeftSided, player.CentralSided, CM2.ConvertShortToNormalFormat(player.Ability), CM2.ConvertShortToNormalFormat(player.Potential), CM2.ConvertShortToNormalFormat(player.Reputation),
                      player.Adaptability, player.Aggression, player.BigOccasion, player.Character, player.Consistency, player.Creativity, player.Determination, player.Dirtyness, player.Dribbling, player.Flair, player.Heading, player.Influence, player.InjProne, player.Marking, player.OffTheBall, player.Pace, player.Passing,
                      player.Positioning, player.SetPieces, player.Shooting, player.Stamina, player.Strength, player.Tackling, player.Technique);
        }

        static void WriteTeam(StreamWriter sw, CM9798Team team)
        {
            MiscFunctions.WriteCSVLine(sw, team.LongName, team.ShortName, team.Nation, team.Region, team.Developed, team.XCoord, team.YCoord, team.EEC, team.TCoef8893,
                team.City, team.Stadium, CM2.ConvertLongToNormalFormat(team.Capacity), CM2.ConvertLongToNormalFormat(team.Seating), team.Following, team.Reputation, team.Blend,
                team.Formation, team.Style, team.HomeTextCol, team.HomeBackCol, team.AwayTextCol, team.AwayBackCol,
                team.Division, team.LastDivision, team.LastPosition, CM2.ConvertLongToNormalFormat(team.Cash) * 1000, team.LeagueStandard, team.Under21, team.BTeam, team.Essential, CM2.ConvertLongToNormalFormat(team.TransferRecord));
        }
    }
}

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
    /*0*/   public int _UniqueID;
    /*4*/   [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)] public byte[] _FirstName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _SecondName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Nationality;
            public byte NationalCaps;
            public byte NationalGoals;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Team;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _DateJoined;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _ContractEnds;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)] public byte[] _BirthDate;
    /*AE*/  public byte Age;
    /*AF*/  public byte Goalkeeper;
    /*B0*/  public byte Sweeper;
    /*B1*/  public byte Defence;
    /*B2*/  public byte Anchor;
    /*B3*/  public byte Midfield;
    /*B4*/  public byte Support;
    /*B5*/  public byte Attack;
    /*B6*/  public byte RightSided;
    /*B7*/  public byte LeftSided;
    /*B8*/  public byte CentralSided;
    /*B9*/  public short Ability;           // First byte = 1, then add 128
    /*BB*/  public short Potential;
    /*BD*/  public short Reputation;
    /*BF*/  public byte Adaptability;  // 23
            public byte Aggression;    // 24
            public byte BigOccasion;   // 25
            public byte Character;     // 26
            public byte Consistency;   // 27
            public byte Creativity;    // 28
            public byte Determination; // 29
            public byte Dirtyness;     // 30
            public byte Dribbling;     // 31 
            public byte Flair;         // 32
            public byte Heading;       // 33
            public byte Influence;     // 34
            public byte InjProne;      // 35
            public byte Marking;       // 36
            public byte OffTheBall;    // 37
            public byte Pace;          // 38
            public byte Passing;       // 39
            public byte Positioning;   // 40
            public byte SetPieces;     // 41
            public byte Shooting;      // 42
            public byte Stamina;       // 43
            public byte Strength;      // 44
            public byte Tackling;      // 45
            public byte Technique;     // 46

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
                    /* 1 */
            public int _UniqueID;
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
            /* 31 */ [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Stadium;
            /* 32 */ public int _Capacity;
            /* 33 */ public int _Seating;
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
            public string Stadium
            {
                get { return MiscFunctions.GetTextFromBytes(_Stadium); }
                set { _Stadium = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public int Seating
            {
                get { return CM2.ConvertLongToNormalFormat(_Seating); }
                set { _Seating = CM2.ConvertLongToCM2Format(value); }
            }
            public int Capacity
            {
                get { return CM2.ConvertLongToNormalFormat(_Capacity); }
                set { _Capacity = CM2.ConvertLongToCM2Format(value); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Manager
        {
            public int _UniqueID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] _FirstName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _SecondName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Nationality;
            public byte YearsInGame;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Favoured;
            public short _MotivatingAbility;
            public short _Judgement;
            public short _Reputation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _Formation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _Style;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] CurrentClub;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _DateJoined;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _NationalJob;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] _DateStarted;
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
                set { _FirstName = MiscFunctions.GetBytesFromText(value, 20); }
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
            public string Favoured
            {
                get { return MiscFunctions.GetTextFromBytes(_Favoured); }
                set { _Favoured = MiscFunctions.GetBytesFromText(value, 35); }
            }

            public short MotivatingAbility
            {
                get { return CM2.ConvertShortToNormalFormat(_MotivatingAbility); }
                set { _MotivatingAbility = CM2.ConvertShortToCM2Format(value); }
            }

            public short Judgement
            {
                get { return CM2.ConvertShortToNormalFormat(_Judgement); }
                set { _Judgement = CM2.ConvertShortToCM2Format(value); }
            }

            public short Reputation
            {
                get { return CM2.ConvertShortToNormalFormat(_Reputation); }
                set { _Reputation = CM2.ConvertShortToCM2Format(value); }
            }

            public string Formation
            {
                get { return MiscFunctions.GetTextFromBytes(_Formation); }
                set { _Formation = MiscFunctions.GetBytesFromText(value, 10); }
            }
            public string Style
            {
                get { return MiscFunctions.GetTextFromBytes(_Style); }
                set { _Style = MiscFunctions.GetBytesFromText(value, 10); }
            }
            public string Team
            {
                get { return MiscFunctions.GetTextFromBytes(CurrentClub); }
            }
            public string DateJoined
            {
                get { return MiscFunctions.GetTextFromBytes(_DateJoined); }
                set { _DateJoined = MiscFunctions.GetBytesFromText(value, 10); }
            }
            public string NationalJob
            {
                get { return MiscFunctions.GetTextFromBytes(_NationalJob); }
                set { _NationalJob = MiscFunctions.GetBytesFromText(value, 35); }
            }
            public string DateStarted
            {
                get { return MiscFunctions.GetTextFromBytes(_DateStarted); }
                set { _DateStarted = MiscFunctions.GetBytesFromText(value, 10); }
            }
        }

        public const int TeamDataStartPos = 895;
        public const int PlayerDataStartPos = 666;
        public const int ManagerDataStartPos = 268;

        public static List<CM9798Team> tmdata;
        public static List<CM9798Player> pldata;
        public static List<CM9798Manager> mgdata;

        public static void LoadCM9798DataFromDirectory(string dir)
        {
            tmdata = MiscFunctions.ReadFile<CM9798Team>(Path.Combine(dir, "TMDATA.DB1"), TeamDataStartPos);
            pldata = MiscFunctions.ReadFile<CM9798Player>(Path.Combine(dir, "PLAYERS.DB1"), PlayerDataStartPos);
            mgdata = MiscFunctions.ReadFile<CM9798Manager>(Path.Combine(dir, "MGDATA.DB1"), ManagerDataStartPos);
        }

        public static void Test()
        {
            //var pldata_new = MiscFunctions.ReadFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLAYERS.DB1", PlayerDataStartPos);
            //WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLAYERS_NEW.CSV", pldata_new);

            // CM0102 Load
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\April2022\Data\index.dat");

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

            var s1 = CM2.ConvertShortToCM2Format(12696);
            var s2 = CM2.ConvertLongToCM2Format(1146892696);
            var s3 = CM2.ConvertLongToNormalFormat(1146892696);
            var s4 = CM2.ConvertShortToNormalFormat(12696);
            var s5 = CM2.ConvertShortToNormalFormat(17500);

            // CM2 Load
            int CM9798PlayerSize = Marshal.SizeOf(typeof(CM9798Player));
            LoadCM9798DataFromDirectory(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG");
            var plhist = new List<CM2.CM2History>();

            //WriteTeamDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\TMDATA.CSV", tmdata);
            //WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\PLAYERS.CSV", pldata);
            //WriteManagerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\MGDATA.CSV", mgdata);

            // Remove all Player Managers
            foreach (var manager in mgdata.Where(x => x.PlayerManager == 1))
                manager.PlayerManager = 0;

            // Remove All Players
            pldata.Clear();

            // List of Managers we do touch
            List<CM9798Manager> convertedManagers = new List<CM9798Manager>();

            List<TClub> cm0102clubs = new List<TClub>();

            bool UpdateEnglishLeagues = true;
            bool UpdateItalianLeagues = true;
            bool UpdateSpanishLeagues = true;
            bool UpdateFrenchLeagues = true;
            bool UpdateGermanLeagues = true;
            bool UpdateScottishLeagues = true;
            bool UpdateDutchLeagues = true;
            bool UpdatePortugalLeagues = true;
            bool UpdateBelgiumLeagues = true;

            List<string> cm2premTeams, cm2firstDivTeams, cm2secondDivTeams, cm2thirdDivTeams, cm2fourthDivTeams;
            List<string> cm2SerieATeams, cm2SerieBTeams, cm2SerieCTeams;
            List<string> cm2SpainFirstTeams, cm2SpainSecondTeams, cm2SpainNationalTeams;
            List<string> cm2FranceFirstTeams, cm2FranceSecondTeams, cm2FranceNationalTeams;
            List<string> cm2GermanFirstTeams, cm2GermanSecondTeams, cm2GermanNationalTeams;
            List<string> cm2ScotlandPremierTeams, cm2ScotlandFirstTeams, cm2ScotlandSecondTeams, cm2ScotlandThirdTeams, cm2ScotlandNationalTeams;
            List<string> cm2DutchFirstTeams, cm2DutchSecondTeams, cm2DutchNationalTeams;
            List<string> cm2PortugalFirstTeams, cm2PortugalSecondTeams, cm2PortugalNationalTeams;
            List<string> cm2BelgiumFirstTeams, cm2BelgiumSecondTeams, cm2BelgiumNationalTeams;

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

            if (UpdateDutchLeagues)
            {
                // Dutch CM0102
                var cm0102DutchFirstTeams = CM2.ReadCM0102League(hl, "Dutch Premier Division", cm0102clubs);        // 18
                var cm0102DutchSecondTeams = CM2.ReadCM0102League(hl, "Dutch First Division", cm0102clubs);         // 18

                // Dutch CM9798 Teams
                cm2DutchFirstTeams = tmdata.Where(x => x.Division == "HD1").Select(x => x.LongName).ToList();        // 18
                cm2DutchSecondTeams = tmdata.Where(x => x.Division == "HD2").Select(x => x.LongName).ToList();       // 18
                cm2DutchNationalTeams = tmdata.Where(x => x.Division == "HNL").Select(x => x.LongName).ToList();     // 27
            }

            if (UpdatePortugalLeagues)
            {
                // Portugal CM0102
                var cm0102PortugalFirstTeams = CM2.ReadCM0102League(hl, "Portuguese Premier League", cm0102clubs);        // 18
                var cm0102PortugalSecondTeams = CM2.ReadCM0102League(hl, "Portuguese Second League", cm0102clubs);        // 18

                // Portugal CM9798 Teams
                cm2PortugalFirstTeams = tmdata.Where(x => x.Division == "PD1").Select(x => x.LongName).ToList();        // 18
                cm2PortugalSecondTeams = tmdata.Where(x => x.Division == "PD2").Select(x => x.LongName).ToList();       // 18
                cm2PortugalNationalTeams = tmdata.Where(x => x.Division == "PNL").Select(x => x.LongName).ToList();     // 94  ???
            }

            if (UpdateBelgiumLeagues)
            {
                // Belgian CM0102
                var cm0102BelgiumFirstTeams = CM2.ReadCM0102League(hl, "Belgian First Division", cm0102clubs);         // 18
                var cm0102BelgiumSecondTeams = CM2.ReadCM0102League(hl, "Belgian Second Division", cm0102clubs);       // 18

                // Belgian CM9798 Teams
                cm2BelgiumFirstTeams = tmdata.Where(x => x.Division == "BD1").Select(x => x.LongName).ToList();        // 18
                cm2BelgiumSecondTeams = tmdata.Where(x => x.Division == "BD2").Select(x => x.LongName).ToList();       // 18
                cm2BelgiumNationalTeams = tmdata.Where(x => x.Division == "BNL").Select(x => x.LongName).ToList();     // 93  ???
            }

            var EnglishLeagues = tmdata.Where(x => x.Nation == "England").Select(x => x.Division).Distinct().ToList();
            var ItalianLeagues = tmdata.Where(x => x.Nation == "Italy").Select(x => x.Division).Distinct().ToList();
            var SpanishLeagues = tmdata.Where(x => x.Nation == "Spain").Select(x => x.Division).Distinct().ToList();
            var FranceLeagues = tmdata.Where(x => x.Nation == "France").Select(x => x.Division).Distinct().ToList();
            var GermanLeagues = tmdata.Where(x => x.Nation == "Germany").Select(x => x.Division).Distinct().ToList();
            var ScotlandLeagues = tmdata.Where(x => x.Nation == "Scotland").Select(x => x.Division).Distinct().ToList();
            var DutchLeagues = tmdata.Where(x => x.Nation == "Holland").Select(x => x.Division).Distinct().ToList();
            var PortugalLeagues = tmdata.Where(x => x.Nation == "Portugal").Select(x => x.Division).Distinct().ToList();
            var BelgiumLeagues = tmdata.Where(x => x.Nation == "Belgium").Select(x => x.Division).Distinct().ToList();

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
            if (UpdateDutchLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "HNL", "HD1", "HD2", "HNL");
            if (UpdatePortugalLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "PNL", "PD1", "PD2", "PNL");
            if (UpdateBelgiumLeagues)
                RemoveTeamsAndPlayersFromDivision(tmdata, pldata, "BNL", "BD1", "BD2", "BNL");

            var newID = pldata.Count > 0 ? pldata.Max(x => x.UniqueID) + 1 : 0;
            var newTeamID = tmdata.Max(x => x.UniqueID) + 1;

            foreach (var cm0102team in cm0102clubs)
            {
                var currentCM0102Team = MiscFunctions.GetTextFromBytes(cm0102team.Name);
                var cm2team = GetTeamFromCM0102Team(tmdata, cm0102team, 1);
               // Console.WriteLine("Processing: {0} - Matched: {1}", MiscFunctions.GetTextFromBytes(cm0102team.Name), cm2team == null ? "DIDNT MATCH" : cm2team.LongName);

                if (cm2team == null)
                {
                    Console.WriteLine("CREATING UNKNOWN TEAM: {0} in DIVISION: {1}", MiscFunctions.GetTextFromBytes(cm0102team.Name), DivisionMapper(hl, cm0102team));
                    //cm2team = CreateUnknownTeam(newTeamID++, MiscFunctions.GetTextFromBytes(cm0102team.Name), MiscFunctions.GetTextFromBytes(cm0102team.ShortName), (byte)(cm0102team.Reputation/500), MiscFunctions.GetTextFromBytes(hl.nation[cm0102team.Nation].Name));
                    cm2team = CreateUnknownTeam2(newTeamID++, hl, cm0102team);
                    tmdata.Add(cm2team);
                }

                if (cm2team != null)
                {
                    // MANAGER HANDLING --- START
                    var cm9798mgr = mgdata.FirstOrDefault(x => MiscFunctions.StringCompare(x.Team, cm2team.ShortName, cm2team.LongName));
                    if (cm9798mgr != null)
                    {
                        var cm0102mgr = hl.staff.FindIndex(x => x.ID == cm0102team.Manager);
                        if (cm0102mgr != -1)
                        {
                            cm9798mgr.FirstName = MiscFunctions.GetTextFromBytes(hl.first_names[hl.staff[cm0102mgr].FirstName].Name);
                            cm9798mgr.SecondName = MiscFunctions.GetTextFromBytes(hl.second_names[hl.staff[cm0102mgr].SecondName].Name);

                            convertedManagers.Add(cm9798mgr);
                        }
                    }
                    else
                        Console.WriteLine("CANT FIND CM9798 Manager: " + cm2team.LongName);
                    // MANAGER HANDLING --- END

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

                    // Delete all the players from the CM2 Team (Don't use a fuzzy search with U23 or U19 or U21 teams as could delete all the players in the main team!
                    // Hacks for Dundee United vs United and Club Brugge vs Club Brugge II
                    if (cm2team.LongName.Contains("U23") || cm2team.LongName.Contains("U19") || cm2team.LongName.Contains("U21") || cm2team.LongName.EndsWith(" B") || cm2team.LongName.EndsWith(" II") || cm2team.LongName.StartsWith("Dundee") || cm2team.LongName.StartsWith("Club Brugge"))
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

            // Qatar patches (to try and make it a proper 2022 host)
            var qatar = tmdata.Find(x => x.LongName == "Qatar");
            qatar.Reputation = 18;
            qatar.Blend = 16;
            qatar.Developed = 1;
            qatar.Following = 30;
            qatar.Stadium = "Lusail Stadium";
            qatar.Capacity = 80000;
            qatar.Seating = 80000;

            var QatarNationId = hl.nation.Find(x => MiscFunctions.GetTextFromBytes(x.Name) == "Qatar").ID;
            var QatariPlayers = hl.staff.Where(x => x.Nation == QatarNationId && x.Player >= 0).OrderByDescending(x => hl.players[x.Player].CurrentReputation).ToList();
            var QatariPlayerCount = 0;
            for (int i = 0; i < QatariPlayers.Count; i++)
            {
                var s = QatariPlayers[i];
                var p = hl.players[s.Player];
                Console.WriteLine("{0} {1} - Team: {2}", MiscFunctions.GetTextFromBytes(hl.first_names[s.FirstName].Name), MiscFunctions.GetTextFromBytes(hl.second_names[s.SecondName].Name), s.ClubJob == -1 ? "NO CLUB" : MiscFunctions.GetTextFromBytes(hl.club[s.ClubJob].Name));
                var teamName = "Free Transfer";
                if (s.ClubJob != -1)
                {
                    CM9798Team cm2team = tmdata.FirstOrDefault(x => x.LongName == MiscFunctions.GetTextFromBytes(hl.club[s.ClubJob].Name));
                    if (cm2team == null)
                    {
                        //cm2team = CreateUnknownTeam(newTeamID++, MiscFunctions.GetTextFromBytes(hl.club[s.ClubJob].Name), MiscFunctions.GetTextFromBytes(hl.club[s.ClubJob].ShortName), (byte)(hl.club[s.ClubJob].Reputation/500), "Qatar");
                        //tmdata.Add(cm2team);
                        continue;
                    }
                    teamName = cm2team.LongName;
                }
                pldata.Add(CM0102PlayerTo9798(newID++, ref newTeamID, hl, s.ID, teamName, staffHistoryMap, clubMap, plhist, tmdata));
                
                QatariPlayerCount++;
                if (QatariPlayerCount > 35)
                    break;
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
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "EPR", "ED1", "ED2", "ED3");
            }

            if (UpdateItalianLeagues)
            {
                /*
                CHANGECLUBDIVISION: "FC Crotone" "Italian Serie A"
                CHANGECLUBDIVISION: "Spezia Calcio" "Italian Serie A"
                CHANGECLUBDIVISION: "LR Vicenza Virtus" "Italian Serie B"
                CHANGECLUBDIVISION: "AC Reggiana 1919" "Italian Serie B"
                */
                tmdata.Find(x => x.LongName == "FC Crotone").Division = "ISA";
                tmdata.Find(x => x.LongName == "Spezia Calcio").Division = "ISA";
                tmdata.Find(x => x.LongName == "Vicenza").Division = "ISB";
                tmdata.Find(x => x.LongName == "Reggiana").Division = "ISB";

                // Count Italian Teams
                cm2SerieATeams = GetCM9798TeamNamesByDivision(tmdata, "ISA");        // 18
                cm2SerieBTeams = GetCM9798TeamNamesByDivision(tmdata, "ISB");        // 20
                DivisionTeamCutOff(tmdata, "ISC", 48);
                cm2SerieCTeams = GetCM9798TeamNamesByDivision(tmdata, "ISC");        // 48
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "ISA", "ISB", "ISC");
            }

            if (UpdateSpanishLeagues)
            {
                // Count Spanish Teams
                cm2SpainFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "SP1");        // 18
                cm2SpainSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "SP2");       // 20
                DivisionTeamCutOff(tmdata, "SPN", 66);
                cm2SpainNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "SPN");     // 66
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "SP1", "SP2");
            }

            if (UpdateFrenchLeagues)
            {
                // Count French Teams
                cm2FranceFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "FD1");        // 18
                //AddTeamsToLeague(tmdata, "FD2", "FNL", 2);
                cm2FranceSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "FD2");       // 22
                DivisionTeamCutOff(tmdata, "FNL", 26);
                cm2FranceNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "FNL");     // 26
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "FD1", "FD2");
            }

            if (UpdateGermanLeagues)
            {
                // Count German Teams
                cm2GermanFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "GD1");        // 18
                cm2GermanSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "GD2");       // 18
                DivisionTeamCutOff(tmdata, "GNL", 56);
                cm2GermanNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "GNL");     // 56
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "GD1", "GD2");
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
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "SPR", "SD1", "SD2", "SD3");
            }

            if (UpdateDutchLeagues)
            {
                // Count Dutch Teams
                cm2DutchFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "HD1");        // 18
                cm2DutchSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "HD2");       // 18
                DivisionTeamCutOff(tmdata, "HNL", 27);
                cm2DutchNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "HNL");     // 27
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "HD1", "HD2");
            }

            if (UpdatePortugalLeagues)
            {
                // Count Portugal Teams
                cm2PortugalFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "PD1");        // 18
                cm2PortugalSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "PD2");       // 18
                DivisionTeamCutOff(tmdata, "PNL", 94);
                cm2PortugalNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "PNL");     // 94
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "PD1", "PD2");
            }

            if (UpdateBelgiumLeagues)
            {
                // Count Portugal Teams
                cm2BelgiumFirstTeams = GetCM9798TeamNamesByDivision(tmdata, "BD1");        // 18
                cm2BelgiumSecondTeams = GetCM9798TeamNamesByDivision(tmdata, "BD2");       // 18
                DivisionTeamCutOff(tmdata, "BNL", 93);
                cm2BelgiumNationalTeams = GetCM9798TeamNamesByDivision(tmdata, "BNL");     // 93
                CheckLeaguesTeamsHavePlayers(tmdata, pldata, "BD1", "BD2");
            }

            // This is needed at least for the EPR as if you load the Scottish league without the English league, you can have errors where the European Cups will try and find the 2nd place team
            // In the EPR and not find it
            ListDivisionAndFixLastPositions(tmdata, "EPR", true);
            ListDivisionAndFixLastPositions(tmdata, "SPR", true);
            ListDivisionAndFixLastPositions(tmdata, "ISA", true);
            ListDivisionAndFixLastPositions(tmdata, "GD1", true);
            ListDivisionAndFixLastPositions(tmdata, "FD1", true);
            ListDivisionAndFixLastPositions(tmdata, "SP1", true);
            ListDivisionAndFixLastPositions(tmdata, "HD1", true);
            ListDivisionAndFixLastPositions(tmdata, "PD1", true);
            ListDivisionAndFixLastPositions(tmdata, "BD1", true);

            // Add more players that did not belong to the leagues we added
            var cm0102bestPlayers = hl.staff.Where(x => x.Player != -1).OrderByDescending(x => hl.players[x.Player].CurrentReputation).ToList();
            foreach (var cm0102bestPlayer in cm0102bestPlayers)
            {
                if (cm0102clubs.FindIndex(x => x.ID == cm0102bestPlayer.ClubJob) == -1)
                {
                    // Add player as we won't have added him previously
                    CM9798Team cm2team;

                    if (cm0102bestPlayer.ClubJob >= 0)
                        cm2team = GetTeamFromCM0102Team(tmdata, hl.club[cm0102bestPlayer.ClubJob], 1);
                    else
                        cm2team = tmdata.Find(x => x.LongName == "Free Transfer");

                    if (cm2team != null)
                    {
                        var newPlayer = CM0102PlayerTo9798(newID++, ref newTeamID, hl, cm0102bestPlayer.ID, string.IsNullOrEmpty(cm2team.ShortName) ? cm2team.LongName : cm2team.ShortName, staffHistoryMap, clubMap, plhist, tmdata);
                        if (newPlayer != null)
                        {
                            // Console.WriteLine("Adding addtional player: {0} {1} at club {2} ({3})", newPlayer.FirstName, newPlayer.SecondName, cm2team.LongName, CM2.ConvertShortToNormalFormat(newPlayer.Reputation));
                            pldata.Add(newPlayer);
                        }
                    }
                }

                if (pldata.Count > 25000)
                    break;
            }

            mgdata = convertedManagers;

            MiscFunctions.SaveFile<CM9798Team>(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\TMDATA.DB1", tmdata, TeamDataStartPos);
            MiscFunctions.SaveFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLAYERS.DB1", pldata, PlayerDataStartPos, true);
            MiscFunctions.SaveFile<CM9798Manager>(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\MGDATA.DB1", mgdata, ManagerDataStartPos);

            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\TMDATA.DB1", TeamDataStartPos-8, tmdata.Count, true);
            //CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\TMDATA.DB1", TeamDataStartPos-2, tmdata.Count);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLAYERS.DB1", 660, pldata.Count, false);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\MGDATA.DB1", ManagerDataStartPos-8, mgdata.Count, true);
            CM2.WriteCM2HistoryFile(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLHIST98.BIN", plhist, true);

            WriteTeamDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\TMDATA.CSV", tmdata);
            WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLAYERS.CSV", pldata);
            WriteManagerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\MGDATA.CSV", mgdata);

            Console.WriteLine("Player Count: {0}", pldata.Count);
            Console.WriteLine("Team Count: {0}", tmdata.Count);
        }

        static void ListDivisionAndFixLastPositions(List<CM9798Team> tmdata, string division, bool quietly = false)
        {
            if (!quietly)
            {
                Console.WriteLine(division);
                Console.WriteLine("----------------");
            }
            var cm2teams = tmdata.Where(x => x.Division == division).OrderBy(x => x.LastPosition).ToList();
            byte pos = 1;
            foreach (var cm2team in cm2teams)
            {
                if (!quietly)
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
                    var players = pldata.Where(x => MiscFunctions.StringCompare(x.Team, cm2team.ShortName, cm2team.LongName, true)).ToList();
                    if (players.Count() == 0)
                        Console.WriteLine("************* NO PLAYERS IN TEAM {0} IN DIVISION {1} *************", cm2team.LongName, division);
                    if (players.Count() > 32)
                    {
                        Console.WriteLine("************* TOO MANY PLAYERS IN TEAM {0} IN DIVISION {1} *************", cm2team.LongName, division);
                        foreach (var player in players)
                            Console.WriteLine("{0} {1} in team {2}", player.FirstName, player.SecondName, player.Team);
                    }
                }
            }
        }

        static List<string> GetCM9798TeamNamesByDivision(List<CM9798Team> tmdata, string division)
        {
            var teamNames = tmdata.Where(x => x.Division == division).Select(x => x.LongName).ToList();
            Console.WriteLine("{0}: {1} Teams", division, teamNames.Count);
            return teamNames;
        }

        static void AddTeamsToLeague(List<CM9798Team> tmdata, string destinationDivision, string sourceDivision, int numberOfTeams)
        {
            var cm2Teams = tmdata.Where(x => x.Division == sourceDivision).OrderByDescending(x => x.Reputation).Take(numberOfTeams).ToList();
            foreach (var cm2team in cm2Teams)
            {
                Console.WriteLine("Adding team {0} to division {1} (from: {2})", cm2team.LongName, destinationDivision, sourceDivision);
                cm2team.Division = destinationDivision;
            }
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

            // Cope against a weird oddity that happens only with Belgium
            if (mapLastDivisionInstead && cm0102club.LastDivision == -1)
                return ret;

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

                // Dutch
                case "Dutch Premier Division":
                    ret = "HD1";
                    break;
                case "Dutch First Division":
                    ret = "HD2";
                    break;

                // Portugal
                case "Portuguese Premier League":
                    ret = "PD1";
                    break;
                case "Portuguese Second League":
                    ret = "PD2";
                    break;
                case "Portuguese Third Division":
                case "Portuguese Second Division B":
                case "Portuguese Second Division B Central":
                case "Portuguese Second Division B North":
                case "Portuguese Second Division B South":
                    ret = "PNL";
                    break;

                // Belgium
                case "Belgian First Division":
                    ret = "BD1";
                    break;
                case "Belgian Second Division":
                    ret = "BD2";
                    break;
                case "Belgian Third Division":
                case "Belgian Third Division A":
                case "Belgian Third Division B":
                case "Belgian Fourth Division  A":
                case "Belgian Fourth Division  B":
                case "Belgian Fourth Division  C":
                case "Belgian Fourth Division  D":
                    ret = "BNL";
                    break;

                // A Lower Division
                case "A Lower Division":
                    ret = "";
                    break;

                default:
                    Console.WriteLine("********* COULD NOT MAP DIVISION!! ({0})*********", cm0102division);
                    ret = null;
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

            // Hack for LB Chateauroux
            if (cm0102TeamName.StartsWith("LB "))
                cm0102TeamName = MiscFunctions.GetTextFromBytes(cm0102club.Name).Replace("LB ", "");    // Keep the Diacritics

            // Some special mappings between team names where CM2 and CM0102 differ
            var extraCheck = CM2.TeamMapper(cm0102TeamName);

            var returnTeam = tmdata.FirstOrDefault(x => x.Nation != "EXTINCT" && (x.LongName.ToLower() == cm0102TeamName.ToLower() || x.LongName.ToLower() == cm0102ShortTeamName.ToLower() ||x.ShortName.ToLower() == cm0102ShortTeamName.ToLower() || x.LongName.ToLower() == extraCheck.ToLower() || x.ShortName.ToLower() == extraCheck.ToLower()));
            return returnTeam;
        }

        static CM9798Player CM0102PlayerTo9798(int newPlayerID, ref int newTeamID, HistoryLoader hl, int playerId, string setTeamTo, Dictionary<int, List<TStaffHistory>> staffHistoryMap, Dictionary<int, TClub> clubMap, List<CM2.CM2History> plhist, List<CM9798Team> tmdata, int yearModifier = -4)
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
            if (nation.Contains("Principe"))
                nation = "France";
            if (nation == "Trinidad & Tobago")
                nation = "Venezuela";
            if (nation == "Eritrea")
                nation = "Ethiopia";
            if (nation == "Palestine")
                nation = "Israel";
            if (nation == "Belarus")
                nation = "Bielorussia";
            if (nation == "eSwatini")
                nation = "Swaziland";
            if (nation == "Chinese Taipei")
                nation = "China";
            if (nation == "Cambodia")
                nation = "Kampuchea";
            if (nation == "Guam" || nation == "US Virgin Islands" || nation == "Northern Mariana Islands")
                nation = "United States";
            if (nation == "Timor")
                nation = "Australia";
            if (nation == "South Sudan")
                nation = "Sudan";

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
                    "Comoros",
                    "New Caledonia",
                    "Maldives"
                };
            if (nationsToBeMapped.Contains(nation))
                nation = "France";

            if (tmdata.FirstOrDefault(x => x.LongName.ToLower() == nation.ToLower() || x.ShortName.ToLower() == nation.ToLower()) == null)
            {
                Console.WriteLine("********** NATION NOT KNOWN FOR PLAYER: {0} {1} - {2}", firstName, secondName, nation);
                nation = "France";
            }

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
            if (firstName == "Jadon" && secondName == "Sancho")
                Console.WriteLine("Found Jadon");
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
                    detail.Year = (byte)(((h.Year - 1900) + yearModifier));// + 1);

                    if (detail.Year > 100 /*|| detail.Year <= 94*/)
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
                            if (tmdata.Count() < 2000)
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

        public static CM9798Manager CreateManager(HistoryLoader hl, TClub club)
        {
            CM9798Manager cm2mgr = null;

            if (club.Manager >= 0)
            {
                var mgrIdx = hl.staff.FindIndex(x => x.ID == club.Manager);
                if (mgrIdx != -1)
                {
                    var cm0102mgr = hl.staff[mgrIdx];
                    if (cm0102mgr.NonPlayer >= 0)
                    {
                        var nonPlayerIdx = hl.nonPlayers.FindIndex(x => x.ID == cm0102mgr.NonPlayer);
                        if (nonPlayerIdx != -1)
                        {
                            var cm0102NonPlayer = hl.nonPlayers[nonPlayerIdx];
                            cm2mgr = new CM9798Manager();
                        }
                    }
                }
            }

            return cm2mgr;
        }

        public static CM9798Team CreateUnknownTeam2(int uniqueID, HistoryLoader hl, TClub cm0102Team)
        {
            CM9798Team t = new CM9798Team();
            var teamName = MiscFunctions.GetTextFromBytes(cm0102Team.Name);
            var teamShortName = MiscFunctions.GetTextFromBytes(cm0102Team.ShortName);
            var stadiumName = teamShortName + " Stadium";
            int capacity = 50000;
            int seating = 50000;
            if (cm0102Team.Stadium >= 0)
            {
                var stadium = hl.stadiums[cm0102Team.Stadium];
                stadiumName = MiscFunctions.GetTextFromBytes(stadium.Name);
                capacity = stadium.StadiumCapacity;
                seating = stadium.StadiumSeatingCapacity;
            }

            t.UniqueID = uniqueID;
            t.LongName = teamName;
            t.ShortName = teamShortName;
            t.Nation = MiscFunctions.GetTextFromBytes(hl.nation[cm0102Team.Nation].Name);
            t.Stadium = stadiumName;
            t.Capacity = capacity;
            t.Seating = seating;
            t.Following = 10;
            t.Blend = 12;
            t.Essential = 0;
            t.Reputation = (byte)(cm0102Team.Reputation/500);
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
            t.Cash = CM2.ConvertLongToCM2Format(cm0102Team.Cash/1000);

            return t;
        }
        
        public static CM9798Team CreateUnknownTeam(int uniqueID, string teamName = "Unknown", string teamShortName = "Unknown", byte Reputation = 7, string Nation = "Finland")
        {
            CM9798Team t = new CM9798Team();

            t.UniqueID = uniqueID;
            t.LongName = teamName;
            t.ShortName = teamShortName;
            t.Nation = Nation;
            t.Stadium = teamShortName + " Stadium";
            t.Capacity = 50000;
            t.Seating = 50000;
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

        public static void WritePlayerDataToCSV(string fileName, List<CM9798Player> pldata)
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

        public static void WriteManagerDataToCSV(string fileName, List<CM9798Manager> mgdata)
        {

            using (var sw = new StreamWriter(fileName))
            {
                MiscFunctions.WriteCSVLine(sw, "UniqueID", "FirstName", "SecondName", "Nationality", "YearsInGame", "Favoured", "MotivatingAbility",
                      "Judgement", "Reputation", "Formation", "Style", "CurrentClub", "DateJoined", "NationalJob", "DateStarted", "PlayerManager", "BoardConfidence");
                foreach (var m in mgdata)
                    WriteManager(sw, m);
            }
        }

        public static void WriteTeamDataToCSV(string fileName, List<CM9798Team> tmdata)
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

        static void WriteManager(StreamWriter sw, CM9798Manager mgr)
        {
            MiscFunctions.WriteCSVLine(sw, mgr.UniqueID, mgr.FirstName, mgr.SecondName, mgr.Nationality, mgr.YearsInGame, mgr.Favoured, mgr.MotivatingAbility, 
                mgr.Judgement, mgr.Reputation, mgr.Formation, mgr.Style, mgr.Team, mgr.DateJoined, mgr.NationalJob, mgr.DateStarted, mgr.PlayerManager, mgr.BoardConfidence);
        }

        static void WriteTeam(StreamWriter sw, CM9798Team team)
        {
            MiscFunctions.WriteCSVLine(sw, team.LongName, team.ShortName, team.Nation, team.Region, team.Developed, team.XCoord, team.YCoord, team.EEC, team.TCoef8893,
                team.City, team.Stadium, team.Capacity, team.Seating, team.Following, team.Reputation, team.Blend,
                team.Formation, team.Style, team.HomeTextCol, team.HomeBackCol, team.AwayTextCol, team.AwayBackCol,
                team.Division, team.LastDivision, team.LastPosition, CM2.ConvertLongToNormalFormat(team.Cash) * 1000, team.LeagueStandard, team.Under21, team.BTeam, team.Essential, CM2.ConvertLongToNormalFormat(team.TransferRecord));
        }

        public static void SavedPlayerCount(string PLDATA1_S16)
        {
            int count = 0;
            using (var f = File.Open(PLDATA1_S16, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var br = new BinaryReader(f))
            {
                var playerCount = br.ReadInt16();
                while (true)
                {
                    if (f.Position >= f.Length)
                        break;
                    var firstNameLen = br.ReadInt16();
                    var firstName = MiscFunctions.GetTextFromBytes(br.ReadBytes(firstNameLen));
                    var secondNameLen = br.ReadInt16();
                    string secondName = "";
                    if (secondNameLen > 0 && secondNameLen <= 35)
                        secondName = MiscFunctions.GetTextFromBytes(br.ReadBytes(secondNameLen));
                    else
                    {
                        secondNameLen = 22;
                    }

                    Console.WriteLine("{0} {1}", firstName, secondName);
                    f.Seek(360, SeekOrigin.Current);
                    count++;
                }
            }
        }

        static public void ASMParser(string inFile, string outFile)
        {
            using (var fin = File.Open(inFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var fout = File.Open(outFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var sr = new StreamReader(fin))
            using (var sw = new StreamWriter(fout))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;

                    if (line.StartsWith("                pushfw"))
                    {
                        line = line.Replace("                pushfw", "                dw 09c66h");
                    }
                    else
                    if (line.StartsWith("                retfw"))
                    {
                        line = line.Replace("                retfw", "                dw 0cb66h");
                    }
                    else
                    if (line.StartsWith("                neg     ["))
                    {
                        line = line.Replace("                neg     [", "                neg     dword ptr [");
                    }
                    else
                    if (line.StartsWith("                inc     ["))
                    {
                        line = line.Replace("                inc     [", "                inc     byte ptr [");
                    }
                    else
                    if (line.StartsWith("                inc     word"))
                    {
                        line = line.Replace("                inc     word", "                inc     word ptr word");
                    }
                    else
                    if (line.StartsWith("                inc     dword"))
                    {
                        line = line.Replace("                inc     dword", "                inc     dword ptr dword");
                    }
                    else
                    if (line.StartsWith("                inc     byte"))
                    {
                        line = line.Replace("                inc     byte", "                inc     byte ptr byte");
                    }
                    else
                    if (line.StartsWith("                dec     ["))
                    {
                        line = line.Replace("                dec     [", "                dec   byte ptr [");
                    }
                    else
                    if (line.StartsWith("                dec     word"))
                    {
                        line = line.Replace("                dec     word", "                dec     word ptr word");
                    }
                    else
                    if (line.StartsWith("                dec     dword"))
                    {
                        line = line.Replace("                dec     dword", "                dec     dword ptr dword");
                    }
                    else
                    if (line.StartsWith("                dec     byte"))
                    {
                        line = line.Replace("                dec     byte", "                dec     byte ptr byte");
                    }
                    else
                    if (line.StartsWith("                fstp "))
                    {
                        line = line.Replace("                fstp ", "                fstp dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fistp "))
                    {
                        line = line.Replace("                fistp ", "                fistp dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fdiv "))
                    {
                        line = line.Replace("                fdiv ", "                fdiv dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fmul "))
                    {
                        line = line.Replace("                fmul ", "                fmul dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fsubr "))
                    {
                        line = line.Replace("                fsubr ", "                fsubr dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fsub "))
                    {
                        line = line.Replace("                fsub ", "                fsub dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fdivr "))
                    {
                        line = line.Replace("                fdivr ", "                fdivr dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fst "))
                    {
                        line = line.Replace("                fst ", "                fst dword ptr ");
                    }
                    else
                    if (line.StartsWith("                imul "))
                    {
                        line = line.Replace("                imul ", "                imul dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fcomp "))
                    {
                        line = line.Replace("                fcomp ", "                fcomp dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fild "))
                    {
                        line = line.Replace("                fild ", "                fild dword ptr ");
                    }
                    else if (line.StartsWith("                fld "))
                    {
                        line = line.Replace("                fld ", "                fld dword ptr ");
                    }
                    else
                    if (line.StartsWith("                fadd "))
                    {
                        line = line.Replace("                fadd ", "                fadd dword ptr ");
                    }
                    else
                    if (line.StartsWith("                repne movsd"))
                    {
                        line = line.Replace("                repne movsd", "                dw 0a5f2h");
                    }
                    else
                    if (line.StartsWith("                repne movsb"))
                    {
                        line = line.Replace("                repne movsb", "                dw 0a4f2h");
                    }
                    else
                    if (line.StartsWith("                idiv    ["))
                    {
                        line = line.Replace("                idiv    [", "                idiv dword ptr [");
                    }


                    sw.WriteLine(line);
                }
            }
        }

        static public void TacticsViewer()
        {
            // 1st is ultra defensive
            var Formations = new string[]
            {
                "Ultra Defensive",
                "5-3-2 Defensive",
                "Sweeper Defensive",
                "4-4-2 Defensive",
                "4-5-1 Defensive",
                "Counter Attack",
                "5-3-2 Formation",
                "3-5-2 Sweeper",
                "3-5-2 Formation",
                "3-1-3-3 Formation",
                "4-2-2 Formation",
                "Christmas Tree",
                "Diamond Formation",
                "4-3-3 Formation",
                "5-3-2 Attacking",
                "4-4-2 Attacking",
                "4-3-3 Attacking",
                "4-2-4 Attacking",
                "All Out Attack"
            };

            using (var fs = File.Open(@"C:\ChampMan\cm9798\Fresh\Data\CM2\test.exe", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var grid = new byte[(5 * 6) + 1];
                var grid2 = new byte[(5 * 6) + 1];

                fs.Seek(0x153EE4, SeekOrigin.Begin);

                for (int formation = 0; formation < Formations.Length; formation++)
                {
                    Array.Clear(grid, 0, grid.Length);
                    Array.Clear(grid2, 0, grid2.Length);

                    for (int i = 0; i < 11; i++)
                    {
                        var pos = fs.ReadByte();

                        //Console.Write("{0:X2} ({0}): ", pos);

                        grid[pos] = (byte)(i + 1);

                        if (pos == 0)
                        {
                            //   Console.WriteLine("Goal Keeper");
                            continue;
                        }
                        /*
                        switch ((pos-1) / 5)
                        {
                            case 0:
                                Console.Write("SW ");
                                break;
                            case 1:
                                Console.Write("DF ");
                                break;
                            case 2:
                                Console.Write("DM ");
                                break;
                            case 3:
                                Console.Write("MF ");
                                break;
                            case 4:
                                Console.Write("AM ");
                                break;
                            case 5:
                                Console.Write("ST ");
                                break;
                        }
                        var place = pos % 5;
                        switch (place)
                        {
                            case 1:
                                Console.WriteLine("Far Left");
                                break;
                            case 2:
                                Console.WriteLine("Left");
                                break;
                            case 3:
                                Console.WriteLine("Centre");
                                break;
                            case 4:
                                Console.WriteLine("Right");
                                break;
                            case 0:
                                Console.WriteLine("Far Right");
                                break;
                        }*/
    }
    Console.WriteLine("***************************");
                    Console.WriteLine(Formations[formation]);

                    // Read Subs1
                    var subs = new byte[5];
                    fs.Read(subs, 0, 5);

                    // Grid 2
                    for (int i = 0; i < 11; i++)
                    {
                        var pos = fs.ReadByte();

                        grid2[pos] = (byte)(i + 1);
                    }

                    // Read Subs2
                    var subs2 = new byte[5];
                    fs.Read(subs2, 0, 5);

                    // Output Grid
                    for (int i = 5 * 6; i >= 0; i--)
                    {
                        if (grid[i] == 0)
                            Console.Write("   ");
                        else
                        {
                            if (i == 0)
                                Console.Write("      ");
                            Console.Write("{0:00} ", grid[i]);
                        }
                        if ((i - 1) % 5 == 0)
                            Console.WriteLine();

                    }
                    Console.WriteLine();
                    Console.WriteLine("---");

                    // Output Grid2
                    for (int i = 5 * 6; i >= 0; i--)
                    {
                        if (grid2[i] == 0)
                            Console.Write("   ");
                        else
                        {
                            if (i == 0)
                                Console.Write("      ");
                            Console.Write("{0:00} ", grid2[i]);
                        }
                        if ((i - 1) % 5 == 0)
                            Console.WriteLine();

                    }
                    Console.WriteLine();
                }

                /*
                    (L, CL, C, CR, R)  (maybe other way around??)

                    ST: 1A, 1B, 1C, 1D, 1E

                    AM: 15, 16, 17, 18, 19

                    MF: 10, 11, 12, 13, 14

                    DM: 0B, 0C, 0D, 0E, 0F

                    DF: 06, 07, 08, 09, 0A

                    SW: 01, 02, 03, 04, 05
            
                    GKL         00
                */
            }
        }
    }
}

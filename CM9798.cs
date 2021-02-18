using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class CM9798
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Player
        {
            public int UniqueID;
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
                get { return MiscFunctions.GetTextFromBytes(_Team); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Team
        {
            public int UniqueID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKLongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] UKDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITALongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITAShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ITADescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERLongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] GERDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPALongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPAShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SPADescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FRELongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FREShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] FREDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORLongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] PORDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTLongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] DUTDescription;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] _Nation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Region;
            public byte Developed;
            public byte XCoord;
            public byte YCoord;
            public byte EEC;
            public int TCoef8893;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] City;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Stadium;
            public int Capacity;
            public int Seating;
            public byte Following;
            public byte Reputation;
            public byte Blend;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Formation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Style;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] HomeTextCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] HomeBackCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] AwayTextCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] AwayBackCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] Division;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] LastDivision;
            public byte LastPosition;
            public int Cash;
            public byte LeagueStandard;
            public byte Under21;
            public byte BTeam;
            public byte Essential;
            public int TransferRecord;
            public byte ItalianGender;
            public byte FrenchGender;
            public byte PortugueseGender;

            public string LongName
            {
                get { return MiscFunctions.GetTextFromBytes(UKLongName); }
            }
            public string ShortName
            {
                get { return MiscFunctions.GetTextFromBytes(UKShortName); }
            }
            public string Nation
            {
                get { return MiscFunctions.GetTextFromBytes(_Nation); }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class CM9798Manager
        {
            public int UniqueID;
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
            var tmdata = MiscFunctions.ReadFile<CM9798Team>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\TMDATA.DB1", TeamDataStartPos);
            var pldata = MiscFunctions.ReadFile<CM9798Player>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\PLAYERS.DB1", PlayerDataStartPos);
            var mgdata = MiscFunctions.ReadFile<CM9798Manager>(@"C:\ChampMan\cm9798\Fresh\Data\CM2\MGDATA.DB1", ManagerDataStartPos);

            var man = pldata.Where(x => x.Team == "Man U").ToList();
            var mant = tmdata.Where(x => x.ShortName.StartsWith("Man U")).ToList();

            var manu_players = pldata.Where(x => MiscFunctions.StringCompare(x.Team, mant[0].ShortName, mant[0].LongName)).ToList();

            foreach (var p in manu_players)
            {
                Console.WriteLine("{0} {1} {2}", p.FirstName, p.SecondName, p.Team);
            }

            Console.WriteLine();
        }
    }
}

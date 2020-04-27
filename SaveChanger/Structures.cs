using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TCMDate
    {
        public short Day;
        public short Year;
        public int LeapYear;

        public TCMDate(byte[] data, int position)
        {
            Day = BitConverter.ToInt16(data, position);
            Year = BitConverter.ToInt16(data, position + 2);
            LeapYear = BitConverter.ToInt16(data, position + 4);
        }

        public static TCMDate FromDateTime(DateTime dt)
        {
            TCMDate date = new TCMDate();
            date.Day = (short)(dt.DayOfYear - 1);
            date.Year = (short)dt.Year;
            date.LeapYear = DateTime.IsLeapYear(date.Year) ? 1 : 0;
            return date;
        }

        public static DateTime ToDateTime(TCMDate date)
        {
            return new DateTime(date.Year, 1, 1).AddDays(date.Day);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TContract
    {
        public int ID;
        public int Club;
        public int Unknown;
        public int Wage;
        public int GoalBonus;
        public int AssistBonus;
        public int CleanSheetBonus;
        public byte NonPromotionRC;
        public byte MinimumFeeRC;
        public byte NonPlayingRC;
        public byte RelegationRC;
        public byte ManagerJobRC;
        public int ReleaseFee;
        public TCMDate DateStarted;
        public TCMDate ContractExpires;
        public byte ContractType;
        public UInt64 Unknown18_1;
        public UInt64 Unknown18_2;
        public short Unknown18_3;
        public byte Unknown18_4;
        public byte LeavingOnBosman;
        public int TransferArrangedFor;
        public byte TransferStatus;
        public byte SquadStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TStaff
    {
        /*0*/
        public int ID;
        /*4*/
        public int FirstName;
        /*8*/
        public int SecondName;
        /*C*/
        public int CommonName;
        /*10*/
        public TCMDate DateOfBirth;
        /*18*/
        public UInt16 YearOfBirth;
        /*1A*/
        public int Nation;
        /*1E*/
        public int SecondNation;
        /*22*/
        public byte IntApps;
        /*23*/
        public byte IntGoals;
        /*24*/
        public int NationalJob;
        /*28*/
        public byte JobForNation;
        /*29*/
        public TCMDate DateJoinedNation;
        /*31*/
        public TCMDate DateExpiresNation;
        /*39*/
        public int ClubJob;
        /*3D*/
        public byte JobForClub;
        /*3E*/
        public TCMDate DateJoinedClub;
        /*46*/
        public TCMDate DateExpiresClub;
        /*4E*/
        public int Wage;
        /*52*/
        public int Value;
        /*56*/
        public byte Adaptability;
        /*57*/
        public byte Ambition;
        /*58*/
        public byte Determination;
        /*59*/
        public byte Loyality;
        /*5A*/
        public byte Pressure;
        /*5B*/
        public byte Professionalism;
        /*5C*/
        public byte Sportsmanship;
        /*5D*/
        public byte Temperament;
        /*5E*/
        public byte PlayingSquad;
        /*5F*/
        public byte Classification;
        /*60*/
        public byte ClubValuation;
        /*61*/
        public int Player;
        /*65*/
        public int StaffPreferences;
        /*69*/
        public int NonPlayer;
        /*6D*/
        public byte SquadSelectedFor;
        /*Total: 0x6E (110)*/
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPlayer
    {
        /*0*/
        public int ID;
        /*4*/
        public byte SquadNumber;
        /*5*/
        public UInt16 CurrentAbility;
        /*7*/
        public UInt16 PotentialAbility;
        /*9*/
        public UInt16 HomeReputation;
        /*B*/
        public UInt16 CurrentReputation;
        /*D*/
        public UInt16 WorldReputation;
        /*F*/
        public sbyte Goalkeeper;
        /*10*/
        public sbyte Sweeper;
        /*11*/
        public sbyte Defender;
        /*12*/
        public sbyte DefensiveMidfielder;
        /*13*/
        public sbyte Midfielder;
        /*14*/
        public sbyte AttackingMidfielder;
        /*15*/
        public sbyte Attacker;
        /*16*/
        public sbyte WingBack;
        /*17*/
        public sbyte RightSide;
        /*18*/
        public sbyte LeftSide;
        /*19*/
        public sbyte Central;
        /*1A*/
        public sbyte FreeRole;
        /*1B*/
        public sbyte Acceleration;
        /*1C*/
        public sbyte Aggression;
        /*1D*/
        public sbyte Agility;
        /*1E*/
        public sbyte Anticipation;
        /*1F*/
        public sbyte Balance;
        /*20*/
        public sbyte Bravery;
        /*21*/
        public sbyte Consistency;
        /*22*/
        public sbyte Corners;
        /*23*/
        public sbyte Crossing;
        /*24*/
        public sbyte Decisions;
        /*25*/
        public sbyte Dirtiness;
        /*26*/
        public sbyte Dribbling;
        /*27*/
        public sbyte Finishing;
        /*28*/
        public sbyte Flair;
        /*29*/
        public sbyte FreeKicks;
        /*2A*/
        public sbyte Handling;
        /*2B*/
        public sbyte Heading;
        /*2C*/
        public sbyte ImportantMatches;
        /*2D*/
        public sbyte InjuryProneness;
        /*2E*/
        public sbyte Jumping;
        /*2F*/
        public sbyte Leadership;
        /*30*/
        public sbyte LeftFoot;
        /*31*/
        public sbyte LongShots;
        /*32*/
        public sbyte Marking;
        /*33*/
        public sbyte Movement;
        /*34*/
        public sbyte NaturalFitness;
        /*35*/
        public sbyte OneOnOnes;
        /*36*/
        public sbyte PlayerPace;
        /*37*/
        public sbyte Passing;
        /*38*/
        public sbyte Penalties;
        /*39*/
        public sbyte Positioning;
        /*3A*/
        public sbyte Reflexes;
        /*3B*/
        public sbyte RightFoot;
        /*3C*/
        public sbyte Stamina;
        /*3D*/
        public sbyte Strength;
        /*3E*/
        public sbyte Tackling;
        /*3F*/
        public sbyte Teamwork;
        /*40*/
        public sbyte Technique;
        /*41*/
        public sbyte ThrowIns;
        /*42*/
        public sbyte Versatility;
        /*43*/
        public sbyte Vision;
        /*44*/
        public sbyte WorkRate;
        /*45*/
        public byte PlayerMorale;
        /*Total: 0x46 (70)*/
    }

    // 68
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TNonPlayer
    {
        public int ID;
        public UInt16 CurrentAbility;
        public UInt16 PotentialAbility;
        public UInt16 HomeReputation;
        public UInt16 CurrentReputation;
        public UInt16 WorldReputation;
        public sbyte Attacking;
        public sbyte Business;
        public sbyte Coaching;
        public sbyte CoachingGks;
        public sbyte CoachingTechnique;
        public sbyte Directness;
        public sbyte Discipline;
        public sbyte FreeRoles;
        public sbyte Interference;
        public sbyte Judgement;
        public sbyte JudgingPotential;
        public sbyte ManHandling;
        public sbyte Marking;
        public sbyte Motivating;
        public sbyte Offside;
        public sbyte Patience;
        public sbyte Physiotherapy;
        public sbyte Pressing;
        public sbyte Resources;
        public sbyte Tactics;
        public sbyte Youngsters;
        public int Goalkeeper;
        public int Sweeper;
        public int Defender;
        public int DefensiveMidfielder;
        public int Midfielder;
        public int AttackingMidfielder;
        public int Attacker;
        public int WingBack;
        public sbyte Formation;
    }
    
    // 581
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TClub
    {
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public byte GenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ShortName;
        public byte ShortGenderName;
        public int Nation;
        public int Division;
        public int LastDivision;
        public byte LastPosition;
        public int ReserveDivision;
        public byte ProfessionalStatus;
        public int Cash;
        public int Stadium;
        public byte OwnStadium;
        public int ReserveStadium;
        public byte MatchDay;
        public int Attendance;
        public int MinAttendance;
        public int MaxAttendance;
        public byte Training;
        public UInt16 Reputation;
        public byte PLC;
        public int ForeColour1;
        public int BackColour1;
        public int ForeColour2;
        public int BackColour2;
        public int ForeColour3;
        public int BackColour3;
        public int FavStaff1;
        public int FavStaff2;
        public int FavStaff3;
        public int DisStaff1;
        public int DisStaff2;
        public int DisStaff3;
        public int Rival1;
        public int Rival2;
        public int Rival3;
        public int Chairman;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public int[] Directors;
        public int Manager;
        public int AssistantManager;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)] public int[] Squad;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] public int[] Coaches;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public int[] Scouts;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public int[] Physios;
        public int EuroFlag;
        public byte EuroSeeding;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public int[] TeamSelected;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public int[] TacticTraining;
        public int TacticSelected;
        public byte HasLinkedClub;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TComp
    {
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public byte GenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ShortName;
        public byte ShortGenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public byte[] ThreeLetterName;
        public sbyte ClubCompScope;
        public sbyte ClubCompSelected;
        public int ClubCompContinent;
        public int ClubCompNation;
        public int ClubCompForegroundColour;
        public int ClubCompBackgroundColour;
        public Int16 ClubCompReputation; // Version 0x02 - Changed char->short
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TStaffComp
    {
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public byte GenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ShortName;
        public sbyte GenderNameShort;
        public int Continent;
        public int Nation;
        public int ForegroundColour;
        public int BackgroundColour;
        public Int16 StaffCompReputation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TCompHistory
    {
        public int ID;
        public int Comp;
        public Int16 Year;
        public int Winners;
        public int RunnersUp;
        public int ThirdPlace;
        public int Host;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TStaffCompHistory
    {
        public int ID;
        public int Comp;
        public Int16 Year;
        public int WinnersFirstName;
        public int WinnersSecondName;
        public int WinnerID;
        public int WinnerInfo;
        public int RunnersUpFirstName;
        public int RunnersUpSecondName;
        public int RunnersUpID;
        public int RunnersUpInfo;
        public int ThirdPlaceFirstName;
        public int ThirdPlaceSecondName;
        public int ThirdPlaceID;
        public int ThirdPlaceInfo;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TStaffHistory
    {
        public int ID;
        public int StaffID;
        public Int16 Year;
        public int ClubID;
        public sbyte OnLoan;
        public sbyte Apps;
        public sbyte Goals;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TNames
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public int ID;
        public int Nation;
        public sbyte Count;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TNation
    {
        // original data
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public byte GenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ShortName;
        public byte ShortGenderName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public byte[] ThreeLetterName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] Nationality;
        public int Continent;
        public sbyte Region;
        public sbyte ActualRegion;
        public sbyte FirstLanguage;
        public sbyte SecondLanguage;
        public sbyte ThirdLanguage;
        public int CapitalCity;
        public sbyte StateOfDevelopment;
        public sbyte GroupMembership;
        public int NationalStadium;
        public sbyte GameImportance;
        public sbyte LeagueStandard;
        public Int16 NumberClubs;
        public int NumberStaff;
        public Int16 SeasonUpdateDay;
        public Int16 Reputation;
        public int ForegroundColour1;
        public int BackgroundColour1;
        public int ForegroundColour2;
        public int BackgroundColour2;
        public int ForegroundColour3;
        public int BackgroundColour3;
        public double FIFACoefficient;
        public double FIFACoefficient91;
        public double FIFACoefficient92;
        public double FIFACoefficient93;
        public double FIFACoefficient94;
        public double FIFACoefficient95;
        public double FIFACoefficient96;
        public double UEFACoefficient91;
        public double UEFACoefficient92;
        public double UEFACoefficient93;
        public double UEFACoefficient94;
        public double UEFACoefficient95;
        public double UEFACoefficient96;
        public int Rivals1;
        public int Rivals2;
        public int Rivals3;

        public sbyte NationLeagueSelected;
        public int NationShortlistOffset; // Version 0x02 - Added
        public sbyte NationGamesPlayed; // Version 0x02 - Moved to runtime
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TIndex
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public int FileType;
        public int Count;
        public int Offset;
        public int Version;
    }

    public class Block
    {
        public byte[] blockBuffer;
        public byte[] dataBuffer;
        public uint Position
        {
            get
            {
                return BitConverter.ToUInt32(blockBuffer, 0);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, blockBuffer, 0, 4);
            }
        }

        public uint Size
        {
            get
            {
                return BitConverter.ToUInt32(blockBuffer, 4);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, blockBuffer, 4, 4);
            }
        }

        public string Name
        {
            get
            {
                int idx = Array.IndexOf(blockBuffer, (byte)0, 8);
                return Encoding.ASCII.GetString(blockBuffer, 8, idx - 8);
            }
        }
    }
}

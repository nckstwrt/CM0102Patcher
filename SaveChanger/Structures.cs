using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TCMDate
    {
        public short Day;
        public short Year;
        public int LeapYear;

        public TCMDate()
        {
            Day = 1;
            Year = 1970;
            LeapYear = 0;
        }

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
            try
            {
                return new DateTime(date.Year, 1, 1).AddDays(date.Day);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TContract
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

    public enum StaffJob
    {
        JOB_INVALID_JOB,
        JOB_CHAIRMAN,
        JOB_MANAGING_DIRECTOR,
        JOB_GENERAL_MANAGER,
        JOB_DIRECTOR_OF_FOOTBALL,
        JOB_MANAGER,
        JOB_ASSISTANT_MANAGER,
        JOB_RESERVE_TEAM_MANAGER,
        JOB_COACH,
        JOB_SCOUT,
        JOB_PHYSIO,
        JOB_PLAYER,
        JOB_PLAYER_MANAGER,
        JOB_PLAYER_ASSISTANT_MANAGER,
        PLAYER_RESERVE_TEAM_MANAGER,
        JOB_PLAYER_COACH,
        JOB_PLAYER_RETIRED,
        JOB_MEDIA_PUNDIT
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TStaff
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
    public class TPreferences
    {
        public int StaffPreferencesID;
        public int StaffFavouriteClubs1;
        public int StaffFavouriteClubs2;
        public int StaffFavouriteClubs3;
        public int StaffDislikedClubs1;
        public int StaffDislikedClubs2;
        public int StaffDislikedClubs3;
        public int StaffFavouriteStaff1;
        public int StaffFavouriteStaff2;
        public int StaffFavouriteStaff3;
        public int StaffDislikedStaff1;
        public int StaffDislikedStaff2;
        public int StaffDislikedStaff3;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TPlayer
    {
        /*0*/
        public int ID;
        /*4*/
        public byte SquadNumber;
        /*5*/
        public UInt16 CurrentAbility;
        /*7*/
        public Int16 PotentialAbility;
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
    public class TNonPlayer
    {
        /* 0 */
        public int ID;
        /* 4 */
        public UInt16 CurrentAbility;
        /* 6 */
        public UInt16 PotentialAbility;
        /* 8 */
        public UInt16 HomeReputation;
        /* A */
        public UInt16 CurrentReputation;
        /* C */
        public UInt16 WorldReputation;
        /* E */
        public sbyte Attacking;
        /* F */
        public sbyte Business;
        /* 10 */
        public sbyte Coaching;
        /* 11 */
        public sbyte CoachingGks;
        /* 12 */
        public sbyte CoachingTechnique;
        /* 13 */
        public sbyte Directness;
        /* 14 */
        public sbyte Discipline;
        /* 15 */
        public sbyte FreeRoles;
        /* 16 */
        public sbyte Interference;
        /* 17 */
        public sbyte Judgement;
        /* 18 */
        public sbyte JudgingPotential;
        /* 19 */
        public sbyte ManHandling;
        /* 1A */
        public sbyte Marking;
        /* 1B */
        public sbyte Motivating;
        /* 1C */
        public sbyte Offside;
        /* 1D */
        public sbyte Patience;
        /* 1E */
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
    public class TClub
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
    public class TComp
    {
        /*0x0*/
        public int ID;
        /*0x4*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        /*0x37*/
        public byte GenderName;
        /*0x38*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ShortName;
        /*0x52*/
        public byte ShortGenderName;
        /*0x53*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public byte[] ThreeLetterName;
        /*0x57*/
        public sbyte ClubCompScope;
        /*0x58*/
        public sbyte ClubCompSelected;
        /*0x59*/
        public int ClubCompContinent;
        /*0x5D*/
        public int ClubCompNation;
        public int ClubCompForegroundColour;
        public int ClubCompBackgroundColour;
        public Int16 ClubCompReputation; // Version 0x02 - Changed char->short
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TStaffComp
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
    public class TCompHistory
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
    public class TStaffCompHistory
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
    public class TStaffHistory
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
    public class TNames
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        public int ID;
        public int Nation;
        public sbyte Count;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TStadiums
    {
        // 0x0
        public int ID;
        // 0x4
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)] public byte[] Name;
        // 0x37
        public byte StadiumGenderName;
        // 0x38
        public int StadiumCity;
        // 0x3C
        public int StadiumCapacity;
        // 0x40
        public int StadiumSeatingCapacity;
        // 0x44
        public int StadiumExpansionCapacity;
        public int StadiumNearbyStadium;
        public byte StadiumCovered;
        public byte StadiumUnderSoilHeating;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TCities
    {
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] Name;
        public char CityGenderName;
        public int CityNation;
        public double CityLatitude;
        public double CityLongitude;
        public char CityAttraction;
        public long CityWeather;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TNation
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
        /*75*/
        public sbyte Region;
        /*76*/
        public sbyte ActualRegion;
        /*77*/
        public sbyte FirstLanguage;
        /*78*/
        public sbyte SecondLanguage;
        /*79*/
        public sbyte ThirdLanguage;
        /*7A*/
        public int CapitalCity;
        /*7E*/
        public sbyte StateOfDevelopment;
        /*7F*/
        public sbyte GroupMembership;
        /*80*/
        public int NationalStadium;
        /*84*/
        public sbyte GameImportance;
        /*85*/
        public sbyte LeagueStandard;
        /*86*/
        public Int16 NumberClubs;
        /*88*/
        public int NumberStaff;
        /*8C*/
        public Int16 SeasonUpdateDay;
        /*8E*/
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
    public class TContinent
    {
        // 0x0
        public int ContinentID;
        // 0x4
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ContinentName;
        // 0x1E
        public char ContinentGenderName;
        // 0x1F
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public byte[] ContinentNameThreeLetter;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ContinentNameContinentality;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)] public byte[] ContinentFederationName;
        public char ContinentGenderFederationName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)] public byte[] ContinentFederationNameShort;
        public char ContinentGenderFederationNameShort;
        public double ContinentRegionalStrength;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class TIndex
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

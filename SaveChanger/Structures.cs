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
        public int ID;
        public int FirstName;
        public int SecondName;
        public int CommonName;
        public TCMDate DateOfBirth;
        public UInt16 YearOfBirth;
        public int Nation;
        public int SecondNation;
        public byte IntApps;
        public byte IntGoals;
        public int NationalJob;
        public byte JobForNation;
        public TCMDate DateJoinedNation;
        public TCMDate DateExpiresNation;
        public int ClubJob;
        public byte JobForClub;
        public TCMDate DateJoinedClub;
        public TCMDate DateExpiresClub;
        public int Wage;
        public int Value;
        public byte Adaptability;
        public byte Ambition;
        public byte Determination;
        public byte Loyality;
        public byte Pressure;
        public byte Professionalism;
        public byte Sportsmanship;
        public byte Temperament;
        public byte PlayingSquad;
        public byte Classification;
        public byte ClubValuation;
        public int Player;
        public int StaffPreferences;
        public int NonPlayer;
        public byte SquadSelectedFor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TPlayer
    {
        public int ID;
        public byte SquadNumber;
        public UInt16 CurrentAbility;
        public UInt16 PotentialAbility;
        public UInt16 HomeReputation;
        public UInt16 CurrentReputation;
        public UInt16 WorldReputation;
        public sbyte Goalkeeper;
        public sbyte Sweeper;
        public sbyte Defender;
        public sbyte DefensiveMidfielder;
        public sbyte Midfielder;
        public sbyte AttackingMidfielder;
        public sbyte Attacker;
        public sbyte WingBack;
        public sbyte RightSide;
        public sbyte LeftSide;
        public sbyte Central;
        public sbyte FreeRole;
        public sbyte Acceleration;
        public sbyte Aggression;
        public sbyte Agility;
        public sbyte Anticipation;
        public sbyte Balance;
        public sbyte Bravery;
        public sbyte Consistency;
        public sbyte Corners;
        public sbyte Crossing;
        public sbyte Decisions;
        public sbyte Dirtiness;
        public sbyte Dribbling;
        public sbyte Finishing;
        public sbyte Flair;
        public sbyte FreeKicks;
        public sbyte Handling;
        public sbyte Heading;
        public sbyte ImportantMatches;
        public sbyte InjuryProneness;
        public sbyte Jumping;
        public sbyte Leadership;
        public sbyte LeftFoot;
        public sbyte LongShots;
        public sbyte Marking;
        public sbyte Movement;
        public sbyte NaturalFitness;
        public sbyte OneOnOnes;
        public sbyte PlayerPace;
        public sbyte Passing;
        public sbyte Penalties;
        public sbyte Positioning;
        public sbyte Reflexes;
        public sbyte RightFoot;
        public sbyte Stamina;
        public sbyte Strength;
        public sbyte Tackling;
        public sbyte Teamwork;
        public sbyte Technique;
        public sbyte ThrowIns;
        public sbyte Versatility;
        public sbyte Vision;
        public sbyte WorkRate;
        public byte PlayerMorale;
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

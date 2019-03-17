using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM0102Scout
{
    public class Club
    {
        public int clubID;
        public string name;
        public byte genderName;
        public string shortName;
    }

    public class Nation
    {
        public int nationID;
        public string name;
        public byte genderName;
        public string shortName;
        public string threeLetterName;
        public string nationality;
        public int continent;
        public byte region;
        public byte actualRegion;
    }

    public class Staff
    {
        public int staffId;
        public int firstName;
        public int secondName;
        public int playerId;
        public int value;
        public DateTime dob;
        public short yearOfBirth;
        public int nationID;
        public int clubID;
    }

    public class Player
    {
        public int ID;
        public byte SquadNumber;
        public short CurrentAbility;
        public short PotentialAbility;
        public short HomeReputation;
        public short CurrentReputation;
        public short WorldReputation;
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

        public string ShortPosition()
        {
            string Result = "";

            if (Goalkeeper > 14)
                Result = "GK";
            if (Sweeper > 14)
            {
                if (Result != "")
                    Result = Result + "/SW";
                else
                    Result = "SW";
            }
            if (Defender > 14)
            {
                if (Result != "")
                    Result = Result + "/D";
                else
                    Result = "D";
            }
            if (DefensiveMidfielder > 14)
            {
                if (Result != "")
                    Result = Result + "/DM";
                else
                    Result = "DM";
            }
            if ((Midfielder > 14) && (DefensiveMidfielder <= 14) && (AttackingMidfielder <= 14))
            {
                if (Result != "")
                    Result = Result + "/M";
                else
                    Result = "M";
            }
            if (((AttackingMidfielder > 14) && (DefensiveMidfielder <= 14) && ((Attacker <= 14) || ((Attacker > 14) && (Midfielder > 14)))) || ((WingBack > 14) && (DefensiveMidfielder <= 14) && ((Attacker <= 14) || ((Attacker > 14) && (Midfielder > 14)))))
            {
                if (Result != "")
                    Result = Result + "/AM";
                else
                    Result = "AM";
            }
            if (Attacker > 14)
            {
                if ((AttackingMidfielder > 14) || (LeftSide > 14) || (RightSide > 14) || (FreeRole > 14))
                {
                    if (Result != "")
                        Result = Result + "/F";
                    else
                        Result = "F";
                }
                else
                {
                    if (Result != "")
                        Result = Result + "/S";
                    else
                        Result = "S";
                }
            }

            if (Goalkeeper < 15)
            {
                var strSites = "";
                if (RightSide > 14)
                    strSites = "R";
                if (LeftSide > 14)
                    strSites = strSites + "L";
                if (Central > 14)
                    strSites = strSites + "C";
                Result = Result + ' ' + strSites;
            }
            return Result;
        }

        public sbyte Convert(sbyte Attribute, bool goalKeeperRelevantAttribute = false, bool goalKeepingAttribute = false)
        {
            if (goalKeeperRelevantAttribute)
            {
                if (Goalkeeper >= 15)
                {
                    if (goalKeepingAttribute)
                        return HighConvert(Attribute, CurrentAbility);
                    else
                        return LowConvert(Attribute, CurrentAbility);
                }
                else
                {
                    if (goalKeepingAttribute)
                        return LowConvert(Attribute, CurrentAbility);
                    else
                        return HighConvert(Attribute, CurrentAbility);
                }
            }
            return HighConvert(Attribute, CurrentAbility);
        }

        sbyte HighConvert(sbyte Attribute, short Ability)
        {
            double dblTemp = (((double)Attribute) / 10.0) + (((double)Ability) / 20.0) + 10.0;
            double dblResult = (dblTemp * dblTemp / 30.0) + (dblTemp / 3.0) + 0.5;
            if (dblResult < 1)
                dblResult = 1;
            if (dblResult > 20)
                dblResult = 20;
            return (sbyte)dblResult;
        }

        sbyte LowConvert(sbyte Attribute, short Ability)
        {
            double dblTemp = (((double)Attribute) / 10.0) + (((double)Ability) / 200.0) + 10.0;
            double dblResult = (dblTemp * dblTemp / 30.0) + (dblTemp / 3.0) + 0.5;
            if (dblResult < 1)
                dblResult = 1;
            if (dblResult > 20)
                dblResult = 20;
            return (sbyte)dblResult;
        }

        public void PlayerConvert()
        {
            Anticipation = HighConvert(Anticipation, CurrentAbility);
            Decisions = HighConvert(Decisions, CurrentAbility);
            Heading = HighConvert(Heading, CurrentAbility);
            LongShots = HighConvert(LongShots, CurrentAbility);
            Passing = HighConvert(Passing, CurrentAbility);
            Penalties = HighConvert(Penalties, CurrentAbility);
            Positioning = HighConvert(Positioning, CurrentAbility);
            Tackling = HighConvert(Tackling, CurrentAbility);

            if (Goalkeeper >= 15)
            {
                Handling = HighConvert(Handling, CurrentAbility);
                Reflexes = HighConvert(Reflexes, CurrentAbility);
                OneOnOnes = HighConvert(OneOnOnes, CurrentAbility);

                Crossing = LowConvert(Crossing, CurrentAbility);
                Dribbling = LowConvert(Dribbling, CurrentAbility);
                Finishing = LowConvert(Finishing, CurrentAbility);
                Marking = LowConvert(Marking, CurrentAbility);
                Movement = LowConvert(Movement, CurrentAbility);
                ThrowIns = LowConvert(ThrowIns, CurrentAbility);
                Vision = LowConvert(Vision, CurrentAbility);
            }
            else
            {
                Handling = LowConvert(Handling, CurrentAbility);
                Reflexes = LowConvert(Reflexes, CurrentAbility);
                OneOnOnes = LowConvert(OneOnOnes, CurrentAbility);

                Crossing = HighConvert(Crossing, CurrentAbility);
                Dribbling = HighConvert(Dribbling, CurrentAbility);
                Finishing = HighConvert(Finishing, CurrentAbility);
                Marking = HighConvert(Marking, CurrentAbility);
                Movement = HighConvert(Movement, CurrentAbility);
                ThrowIns = HighConvert(ThrowIns, CurrentAbility);
                Vision = HighConvert(Vision, CurrentAbility);
            }
        }
    }

    public class TBlock
    {
        public static string GetName(byte[] block)
        {
            return Encoding.ASCII.GetString(block, 8, 260).TrimEnd('\0');
        }

        public static int GetPosition(byte[] block)
        {
            return BitConverter.ToInt32(block, 0);
        }

        public static int GetSize(byte[] block)
        {
            return BitConverter.ToInt32(block, 4);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public class CM2
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CM2Player
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)] public byte[] FirstName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SecondName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Nationality;
            public byte NationalCaps;
            public byte NationalGoals;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Team;
            public byte Unavailable;
            public byte DataSet;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)] public byte[] BirthDate;
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
            public byte Intelligence;
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
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CM2Team
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] LongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Nation;
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
            public byte Standing;
            public byte Blend;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Formation;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Style;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] FirstHomeCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] SecondHomeCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] FirstAwayCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] SecondAwayCol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] Division;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] LastDivision;
            public byte LastPosition;
            public int Cash;
            public byte LeagueStandard;
            public byte TransferSystem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public byte[] Wav;
        }

        public void ReadData()
        {
            var tmdata = MiscFunctions.ReadFile<CM2Team>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.DB1", 381);
            var pldata1 = MiscFunctions.ReadFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.DB1", 632);

            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\2019\Championship Manager 01-02\Data\index.dat");

            string[] teams = new string[] { "Manchester United",
                                            "Arsenal",
                                            "Aston Villa",
                                            "Blackburn Rovers",
                                            "Chelsea",
                                            "Coventry City",
                                            "Derby County",
                                            "Everton",
                                            "Leeds United",
                                            "Leicester City",
                                            "Liverpool",
                                            "Middlesbrough",
                                            "Newcastle United",
                                            "Nottingham Forest",
                                            "Sheffield Wednesday",
                                            "Southampton",
                                            "Sunderland",
                                            "Tottenham Hotspur",
                                            "West Ham United",
                                            "Wimbledon",
                                             };

            foreach (var team in teams)
            {
                var newPlayers = ReadCM0102Data(hl, team);

                // Get Short Name
                var shortTeamName = MiscFunctions.GetTextFromBytes(tmdata.Find(x => MiscFunctions.GetTextFromBytes(x.LongName) == team).ShortName);

                // Remove data first
                pldata1.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Team) == team || MiscFunctions.GetTextFromBytes(x.Team) == shortTeamName);

                newPlayers = newPlayers.OrderByDescending(x => ConvertShortToNormalFormat(x.Reputation)).ToList();
                foreach (var np in newPlayers)
                {
                    Console.WriteLine(MiscFunctions.GetTextFromBytes(np.FirstName) + " " + MiscFunctions.GetTextFromBytes(np.SecondName));
                }

                for (int i = 0; i < Math.Min(30, newPlayers.Count); i++)
                {
                    var name = MiscFunctions.GetTextFromBytes(newPlayers[i].FirstName) + " " + MiscFunctions.GetTextFromBytes(newPlayers[i].SecondName);
                    var rep = ConvertShortToNormalFormat(newPlayers[i].Reputation);
                    pldata1.Add(newPlayers[i]);
                }
            }

            var deans = pldata1.Where(x => MiscFunctions.GetTextFromBytes(x.FirstName) == "Dean" && MiscFunctions.GetTextFromBytes(x.SecondName) == "Henderson").ToList();

            MiscFunctions.SaveFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.DB1", pldata1, 632, true);

            using (var sw = new StreamWriter(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.csv"))
            {
                WriteLine(sw, "FirstName", "SecondName", "Nationality", "NationalCaps", "NationalGoals", "Team", "Unavailable", "DataSet", "BirthDate",
                      "Age", "Goalkeeper", "Sweeper", "Defence", "Anchor", "Midfield", "Support", "Attack", "RightSided", "LeftSided", "CentralSided", "Ability", "Potential", "Reputation",
                      "Aggression", "BigOccasion", "Character", "Consistency", "Creativity", "Determination", "Dirtyness", "Dribbling", "Flair", "Heading", "Influence", "InjProne", "Intelligence", "Marking", "OffTheBall", "Pace", "Passing",
                      "Positioning", "SetPieces", "Shooting", "Stamina", "Strength", "Tackling", "Technique");
                foreach (var p in pldata1)
                    WritePlayer(sw, p);
            }

            using (var sw = new StreamWriter(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.csv"))
            {
                WriteLine(sw, "LongName", "ShortName", "Nation", "Region", "Developed", "XCoord", "YCoord", "EEC", "TCoef8893",
                          "City", "Stadium", "Capacity", "Seating", "Following", "Standing", "Blend", "Formation", "Style", "FirstHomeCol", "SecondHomeCol", "FirstAwayCol", "SecondAwayCol", "Division",
                          "LastDivision", "LastPosition", "Cash", "LeagueStandard", "TransferSystem", "Wav");
                foreach (var t in tmdata)
                    WriteTeam(sw, t);
            }
        }

        public List<CM2Player> ReadCM0102Data(HistoryLoader hl, string teamName)
        {
            var club = hl.club.Find(x => MiscFunctions.GetTextFromBytes(x.ShortName) == teamName || MiscFunctions.GetTextFromBytes(x.Name) == teamName );

            var staff = hl.staff.FindAll(x => x.ClubJob == club.ID && x.Player != -1);

            List<CM2Player> players = new List<CM2Player>();
            foreach (var s in staff)
            {
                var firstName = MiscFunctions.GetTextFromBytes(hl.first_names[s.FirstName].Name);
                var secondName = MiscFunctions.GetTextFromBytes(hl.second_names[s.SecondName].Name);
                var commonName = MiscFunctions.GetTextFromBytes(hl.common_names[s.CommonName].Name);
                var nation = MiscFunctions.GetTextFromBytes(hl.nation[s.Nation].Name);
                var team = teamName; //MiscFunctions.GetTextFromBytes(hl.club[s.ClubJob].ShortName);
                var dob = TCMDate.ToDateTime(s.DateOfBirth).AddYears(-2);
                var birthdate = dob.ToString("dd.MM.yy");
                var age = 2001 - dob.Year;

                var p = hl.players[s.Player];

                var goalkeeper = ConvertPosition(p.Goalkeeper);;
                var sweeper = ConvertPosition(p.Sweeper);
                var defence = ConvertPosition(p.Defender);
                var anchor = ConvertPosition(p.DefensiveMidfielder);
                var midfield = ConvertPosition(p.Midfielder); 
                var support = ConvertPosition(p.AttackingMidfielder);
                var attack = p.Attacker == 20 ? 2 : 0;
                var rightsided = ConvertSide(p.RightSide);
                var leftsided = ConvertSide(p.LeftSide);
                var centralsided = ConvertSide(p.Central);

                var ability = p.CurrentAbility;
                var potential = p.PotentialAbility < 0 ? 0 : p.PotentialAbility;
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
                var Intelligence = p.Decisions;
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

                var newPlayer = new CM2Player();
                if (string.IsNullOrEmpty(commonName))
                {
                    newPlayer.FirstName = MiscFunctions.GetBytesFromText(firstName, 30);
                    newPlayer.SecondName = MiscFunctions.GetBytesFromText(secondName, 35);
                }
                else
                    newPlayer.SecondName = MiscFunctions.GetBytesFromText(commonName, 35);
                newPlayer.Nationality = MiscFunctions.GetBytesFromText(nation, 35);
                newPlayer.Team = MiscFunctions.GetBytesFromText(team, 35);
                newPlayer.BirthDate = MiscFunctions.GetBytesFromText(birthdate, 13);
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

                newPlayer.Ability = ConvertShortToCM2Format((short)ability);
                newPlayer.Potential = ConvertShortToCM2Format((short)potential);
                newPlayer.Reputation = ConvertShortToCM2Format((short)reputation);

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
                newPlayer.Intelligence = (byte)Intelligence;
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


                players.Add(newPlayer);
            }

            return players;
        }

        public int ConvertPosition(int pos)
        {
            if (pos >= 15)
                return 2;
            if (pos >= 10)
                return 1;
            return 0;
        }

        public int ConvertSide(int pos)
        {
            if (pos >= 15)
                return 2;
            return 0;
        }

        short ConvertShortToCM2Format(short val)
        {
            return  (short)(((val % 128) << 8) | (val / 128));
        }

        short ConvertShortToNormalFormat(short val)
        {
            return (short)(((val & 0xff) << 7) + (val >> 8));
        }

        int ConvertLongToCM2Format(int val)
        {
            var v3 = val / 16384;
            val -= v3 * 16384;
            var v2 = val / 128;
            val -= v2 * 128;

            return (v3 << 8) | (v2 << 16) | (val << 24);
        }

        int ConvertLongToNormalFormat(int val)
        {
            return (int)((((val & 0xff00) >> 8) * 16384) + (((val & 0xff0000) >> 16) * 128) + ((val & 0xff000000) >> 24));
        }

        DateTime? GetDate(byte[] date)
        {
            DateTime? ret = null;
            var textDate = MiscFunctions.GetTextFromBytes(date);
            if (!string.IsNullOrEmpty(textDate) && textDate.Count(x => x == '.') == 2)
            {
                var parts = textDate.Split('.');
                ret = new DateTime(1900 + int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
            }
            return ret;
        }

        byte [] ToCM2Date(DateTime? dt)
        {
            string dateText = "";
            if (dt != null)
            {
                dateText = string.Format("{0}.{1}.{2}", dt.Value.Day, dt.Value.Month, dt.Value.Year - 1900);
            }
            return MiscFunctions.GetBytesFromText(dateText, 13);
        }

        void WritePlayer(StreamWriter sw, CM2Player player)
        {
            WriteLine(sw, MiscFunctions.GetTextFromBytes(player.FirstName), MiscFunctions.GetTextFromBytes(player.SecondName), MiscFunctions.GetTextFromBytes(player.Nationality), player.NationalCaps, player.NationalGoals, MiscFunctions.GetTextFromBytes(player.Team), player.Unavailable, player.DataSet, MiscFunctions.GetTextFromBytes(player.BirthDate),
                      player.Age, player.Goalkeeper, player.Sweeper, player.Defence, player.Anchor, player.Midfield, player.Support, player.Attack, player.RightSided, player.LeftSided, player.CentralSided, ConvertShortToNormalFormat(player.Ability), ConvertShortToNormalFormat(player.Potential), ConvertShortToNormalFormat(player.Reputation),
                      player.Aggression, player.BigOccasion, player.Character, player.Consistency, player.Creativity, player.Determination, player.Dirtyness, player.Dribbling, player.Flair, player.Heading, player.Influence, player.InjProne, player.Intelligence, player.Marking, player.OffTheBall, player.Pace, player.Passing,
                      player.Positioning, player.SetPieces, player.Shooting, player.Stamina, player.Strength, player.Tackling, player.Technique);
        }

        void WriteTeam(StreamWriter sw, CM2Team team)
        {
            WriteLine(sw, MiscFunctions.GetTextFromBytes(team.LongName), MiscFunctions.GetTextFromBytes(team.ShortName), MiscFunctions.GetTextFromBytes(team.Nation), MiscFunctions.GetTextFromBytes(team.Region), team.Developed, team.XCoord, team.YCoord, team.EEC, team.TCoef8893,
                MiscFunctions.GetTextFromBytes(team.City), MiscFunctions.GetTextFromBytes(team.Stadium), ConvertLongToNormalFormat(team.Capacity), ConvertLongToNormalFormat(team.Seating), team.Following, team.Standing, team.Blend,
                MiscFunctions.GetTextFromBytes(team.Formation), MiscFunctions.GetTextFromBytes(team.Style), MiscFunctions.GetTextFromBytes(team.FirstHomeCol), MiscFunctions.GetTextFromBytes(team.SecondHomeCol), MiscFunctions.GetTextFromBytes(team.FirstAwayCol), MiscFunctions.GetTextFromBytes(team.SecondAwayCol),
                MiscFunctions.GetTextFromBytes(team.Division), MiscFunctions.GetTextFromBytes(team.LastDivision), team.LastPosition, ConvertLongToNormalFormat(team.Cash) * 1000, team.LeagueStandard, team.TransferSystem, MiscFunctions.GetTextFromBytes(team.Wav));
        }

        void WriteLine(StreamWriter sw, params object[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                sw.Write(fields[i].ToString());
                if (i != fields.Length - 1)
                    sw.Write(",");
            }
            sw.WriteLine();
        }

    }
}

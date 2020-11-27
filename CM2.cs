using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace CM0102Patcher
{
    public class CM2
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CM2Manager
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] FirstName;              // 0
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] SecondName;             // 20
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Nationality;            // 55
            public byte YearsInGame;                                                                    // 90
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] Favoured;               // 91
            public short Ability;                                                                       // 126
            public short Reputation;                                                                    // 128
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Formation;              // 130
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] Style;                  // 140
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ManagingClub;           // 150
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] AppointedClub;          // 185
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)] public byte[] ManagingInternational;  // 195
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public byte[] AppointedInternational; // 230
            public byte PlayerManager;                                                                  // 240
        }

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

        public class CM2History
        {
            public string Name;
            public string Nation;
            public string BirthDate;
            public List<CM2HistoryDetails> Details = new List<CM2HistoryDetails>();

            public class CM2HistoryDetails
            {
                public byte Year;
                public string Team;
                public byte Apps;
                public byte Goals;
            }

            public void AddDetail(int Year, string Team, int Apps, int Goals)
            {
                var detail = new CM2HistoryDetails();
                detail.Year = (byte)(Year - 1900);
                detail.Team = Team;
                detail.Apps = (byte)Apps;
                detail.Goals = (byte)Goals;
                Details.Add(detail);
            }

            public void SetBirthDate(DateTime dt)
            {
                BirthDate = dt.ToString("d.M.yy");
            }
        }

        public class PlayerAndHistory
        {
            public CM2Player player;
            public CM2History history = null;
        }

        public class CM0102Team
        {
            public string Name;
            public string ShortName;
            public int ID;
        }

        public void ReadData()
        {
            var playerYearModifier = -5;
            var playerSize = Marshal.SizeOf(typeof(CM2Player));
            var teamSize = Marshal.SizeOf(typeof(CM2Team));

            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\2020\Data\index.dat");

            var tmdata = MiscFunctions.ReadFile<CM2Team>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.DB1", 381);
            var pldata1 = MiscFunctions.ReadFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.DB1", 632);
            var mgdata = MiscFunctions.ReadFile<CM2Manager>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\MGDATA.DB1", 182);
            var pldata2 = MiscFunctions.ReadFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA2.DB1", 632);

            //var plhist = ReadCM2HistoryFile(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLHIST.BIN");
            var plhist = new List<CM2History>();
            
            // English
            var cm0102premTeams = ReadCM0102League(hl, "English Premier Division"); // 20
            var cm0102firstDivTeams = ReadCM0102League(hl, "English First Division"); // 24
            var cm0102secondDivTeams = ReadCM0102League(hl, "English Second Division"); // 24
            var cm0102thirdDivTeams = ReadCM0102League(hl, "English Third Division"); // 24

            // Scottish
            var cm0102ScotpremTeams = ReadCM0102League(hl, "Scottish Premier Division"); // 12
            var cm0102ScotfirstDivTeams = ReadCM0102League(hl, "Scottish First Division"); // 10
            var cm0102ScotsecondDivTeams = ReadCM0102League(hl, "Scottish Second Division"); // 10
            var cm0102ScotthirdDivTeams = ReadCM0102League(hl, "Scottish Third Division"); // 19

            var cm2premTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "EPR").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();         // 20
            var cm2firstDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "ED1").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();     // 24
            var cm2secondDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "ED2").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();    // 24
            var cm2thirdDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "ED3").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();     // 24
            var cm2ScotpremTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "SPR").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();         // 10
            var cm2ScotfirstDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "SD1").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();     // 10
            var cm2ScotsecondDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "SD2").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();    // 10
            var cm2ScotthirdDivTeams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "SD3").Select(x => MiscFunctions.GetTextFromBytes(x.LongName)).ToList();     // 10

            var cm0102teams = new List<List<string>> { cm0102premTeams, cm0102firstDivTeams, cm0102secondDivTeams, cm0102thirdDivTeams, cm0102ScotpremTeams, cm0102ScotfirstDivTeams, cm0102ScotsecondDivTeams, cm0102ScotthirdDivTeams };
            var cm2divs = new List<string> { "EPR", "ED1", "ED2", "ED3", "SPR", "SD1", "SD2", "SD3" };

            // Remove all player managers
            for (int i = 0; i < mgdata.Count; i++)
            {
                var mgr = mgdata[i];
                mgr.PlayerManager = 0;
                mgdata[i] = mgr;
            }

            // Add an Unknown Team
            tmdata.Insert(100, CreateUnknownTeam());

            // Add in Foeign Players
            var cm0102clubs = hl.club.Select(x => new CM0102Team { Name = x.Name.ReadString().RemoveDiacritics().ToLower(), ShortName = x.ShortName.ReadString().RemoveDiacritics().ToLower(), ID = x.ID }).ToList();
            var pldata2new = new List<CM2Player>();
            var foreignPlayerTeams = pldata2.Select(x => x.Team.ReadString()).Distinct().OrderBy(x => x).ToList();
            foreach (var foreignTeam in foreignPlayerTeams)
            {
                var teamIdx = FindTeamIndex(tmdata, foreignTeam);
                if (teamIdx != -1)
                {
                    var teamData = tmdata[teamIdx];
                    var teamShortName = teamData.ShortName.ReadString().ToLower();
                    var teamName = teamData.LongName.ReadString().ToLower();
                    var extraCheck = teamName;

                    extraCheck = extraCheck.Replace("fc ", "").Replace(" fc", "");

                    if (teamName == "paris st.germain fc")
                        extraCheck = "psg";

                    // Find how many players were in original DB
                    var playerCount = pldata2.Where(x => teamName.StartsWithIgnoreBlank(x.Team.ReadString()) || teamShortName.StartsWithIgnoreBlank(x.Team.ReadString())).Count();
                    //Console.WriteLine("{0} = {1}", teamName, playerCount);

                    // Find the CM0102 Club
                    var cm0102TeamIdx = cm0102clubs.FindIndex(x => x.Name == teamName || x.Name == teamShortName || x.ShortName == teamName || x.ShortName == teamShortName || x.Name == extraCheck || x.ShortName == extraCheck);
                    //var cm0102TeamIdx = hl.club.FindIndex(x => x.Name.ReadString().RemoveDiacritics().ToLower() == teamName.ToLower() || x.Name.ReadString().RemoveDiacritics().ToLower() == teamShortName.ToLower() || x.ShortName.ReadString().RemoveDiacritics().ToLower() == teamName.ToLower() || x.ShortName.ReadString().RemoveDiacritics().ToLower() == teamShortName.ToLower() || x.LongName.ReadString().RemoveDiacritics().ToLower() == extraCheck.ToLower() || x.ShortName.ReadString().RemoveDiacritics().ToLower() == extraCheck.ToLower());
                    if (cm0102TeamIdx == -1)
                    {
                        Console.WriteLine("CANT FIND CM0102 TEAM: " + teamName);
                    }
                    else
                    {
                        var cm0102fullName = hl.club.Find(x => x.ID == cm0102clubs[cm0102TeamIdx].ID).Name.ReadString();
                        //Console.WriteLine("Loading Players From: {0}", cm0102fullName);
                        var players = ReadCM0102Data(hl, cm0102fullName, tmdata, playerYearModifier).OrderByDescending(x => ConvertShortToNormalFormat(x.player.Reputation)).Take(playerCount).ToList();

                        foreach (var p in players)
                        {
                            // Set the right CM2 LongName
                            p.player.Team = MiscFunctions.GetBytesFromText(teamData.LongName.ReadString(), 35);

                            //Console.WriteLine("Adding Player: {0} {1}", p.player.FirstName.ReadString(), p.player.SecondName.ReadString());
                            pldata2new.Add(p.player);

                            // Add history if we have it
                            if (p.history != null)
                            {
                                plhist.Add(p.history);
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("CANT FIND CM2 TEAM: " + foreignTeam);
            }
            pldata2 = pldata2new;

            // Remove everyone domestic :)
            pldata1 = new List<CM2Player>();

            /*
            // Remove all Scottish people :)
            var scot_count = pldata1.Where(x => MiscFunctions.GetTextFromBytes(x.Nationality) == "Scotland").Count();
            var scots = pldata1.Where(x => MiscFunctions.GetTextFromBytes(x.Nationality) == "Scotland").Take(500).ToList();
            pldata1.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Nationality) == "Scotland");
            pldata1.AddRange(scots);

            // Remove all free transfers + Schoolboys
            pldata1.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Team) == "Free Transfer" || MiscFunctions.GetTextFromBytes(x.Team) == "Schoolboy");
            */

            // Remove all teams from English + Scottish Leagues by blanking out their Division (and their players)
            for (int i = 0; i < tmdata.Count; i++)
            {
                var team = tmdata[i];
                //if (MiscFunctions.GetTextFromBytes(team.Division) == "EPR" || MiscFunctions.GetTextFromBytes(team.Division) == "ED1" || MiscFunctions.GetTextFromBytes(team.Division) == "ED2" || MiscFunctions.GetTextFromBytes(team.Division) == "ED3")
                if (cm2divs.Contains(team.Division.ReadString()))
                {

                    team.Division = MiscFunctions.GetBytesFromText(team.Division.ReadString()[0] == 'E' ? "ENL" : "SNL", 15);
                    tmdata[i] = team;

                    // Remove their players
                    pldata1.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Team) == MiscFunctions.GetTextFromBytes(team.LongName) || MiscFunctions.GetTextFromBytes(x.Team) == MiscFunctions.GetTextFromBytes(team.ShortName));
                }
            }

            // Remove the existing players + add the new
            foreach (var divison in cm0102teams)
            {
                foreach (var team in divison)
                {
                    var newPlayers = ReadCM0102Data(hl, team, tmdata, playerYearModifier);

                    // Get CM2 Short Name
                    var shortTeamName = MiscFunctions.GetTextFromBytes(tmdata.Find(x => MiscFunctions.GetTextFromBytes(x.LongName) == team).ShortName);

                    if (string.IsNullOrEmpty(shortTeamName))
                        shortTeamName = "DONTMATCH";

                    // Some special mappings between team names where CM2 and CM0102 differ
                    var extraCheck = TeamMapper(team);

                    // Check if teams exist
                    var check = pldata1.Count(x => MiscFunctions.GetTextFromBytes(x.Team) == team || MiscFunctions.GetTextFromBytes(x.Team) == shortTeamName || MiscFunctions.GetTextFromBytes(x.Team) == extraCheck);
                    if (check == 0)
                        Console.WriteLine(team);

                    // Remove all their players too (if any exist)
                    pldata1.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Team) == team || MiscFunctions.GetTextFromBytes(x.Team) == shortTeamName || MiscFunctions.GetTextFromBytes(x.Team) == extraCheck);

                    // Get the original team name, let's try and keep that
                    var tmDataIndex = tmdata.FindIndex(x => MiscFunctions.GetTextFromBytes(x.LongName) == team || MiscFunctions.GetTextFromBytes(x.LongName) == shortTeamName || MiscFunctions.GetTextFromBytes(x.ShortName) == shortTeamName || MiscFunctions.GetTextFromBytes(x.LongName) == extraCheck || MiscFunctions.GetTextFromBytes(x.ShortName) == extraCheck);

                    // Get players ordered by reputation
                    newPlayers = newPlayers.OrderByDescending(x => ConvertShortToNormalFormat(x.player.Reputation)).ToList();

                    // Take first 30 of them
                    for (int i = 0; i < Math.Min(30, newPlayers.Count); i++)
                    {
                        var newPlayer = newPlayers[i];

                        // Make the player have the original Team name
                        if (tmDataIndex != -1)
                        {
                            newPlayer.player.Team = string.IsNullOrEmpty(MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].ShortName)) ? MiscFunctions.GetBytesFromText(MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].LongName), 35) : MiscFunctions.GetBytesFromText(MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].ShortName), 35);
                        }
                        else
                        {
                            // If team doesn't exist, it might be yet to be added - therefore check CM0102 doesn't have a short name for the same team
                            var cm0102club = hl.club.Find(x => MiscFunctions.GetTextFromBytes(x.Name) == team);
                            if (!string.IsNullOrEmpty(MiscFunctions.GetTextFromBytes(cm0102club.ShortName)))
                                newPlayer.player.Team = MiscFunctions.GetBytesFromText(MiscFunctions.GetTextFromBytes(cm0102club.ShortName), 35);
                        }

                        pldata1.Add(newPlayer.player);

                        // Add history if we have it
                        if (newPlayer.history != null)
                        {
                            plhist.Add(newPlayer.history);
                        }
                    }
                }
            }

            // Correct Teams
            for (int i = 0; i < cm2divs.Count; i++)
            {
                foreach (var team in cm0102teams[i])
                {
                    var extraCheck = TeamMapper(team);
                    var index = tmdata.FindIndex(x => MiscFunctions.GetTextFromBytes(x.LongName) == team || MiscFunctions.GetTextFromBytes(x.LongName) == extraCheck || MiscFunctions.GetTextFromBytes(x.ShortName) == team || MiscFunctions.GetTextFromBytes(x.ShortName) == extraCheck);
                    if (index == -1)
                    {
                        // Team doesn't exist - so add in
                        var newTeam = CreateCM2Team(hl, team, cm2divs[i]);
                        tmdata.Insert(100, newTeam);
                    }
                    else
                    {
                        var cm0102club = hl.club.Find(x => MiscFunctions.GetTextFromBytes(x.Name) == team);
                        var temp = tmdata[index];
                        temp.Division = MiscFunctions.GetBytesFromText(cm2divs[i], 15);
                        temp.LastPosition = cm0102club.LastPosition;
                        temp.LongName = tmdata[index].LongName;
                        temp.ShortName = tmdata[index].ShortName;

                        if (MiscFunctions.GetTextFromBytes(temp.Nation) == "EXTINCT")
                        {
                            Console.WriteLine("Correcting EXINCT team to English: {0}", temp.LongName.ReadString());
                            temp.Nation = MiscFunctions.GetBytesFromText("England", 35);
                        }

                        tmdata[index] = temp;
                    }
                }
            }

            // We may have more than 52 in the ENL + 27 in the SNL. If so, cut a few off
            var enl_teams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "ENL").OrderByDescending(x => x.Following).ToList();
            tmdata.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Division) == "ENL");
            tmdata.AddRange(enl_teams.Take(52));
            var snl_teams = tmdata.Where(x => MiscFunctions.GetTextFromBytes(x.Division) == "SNL").OrderByDescending(x => x.Following).ToList();
            tmdata.RemoveAll(x => MiscFunctions.GetTextFromBytes(x.Division) == "SNL");
            tmdata.AddRange(enl_teams.Take(27));

            // Cut Hamilton + Aberdeen from SPR
            pldata1.RemoveAll(x => x.Team.ReadString() == "Hamilton" || x.Team.ReadString() == "Aberdeen");
            tmdata.RemoveAll(x => x.ShortName.ReadString() == "Hamilton" || x.ShortName.ReadString() == "Aberdeen");

            MiscFunctions.SaveFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.DB1", pldata1, 632, true);
            MiscFunctions.SaveFile<CM2Player>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA2.DB1", pldata2, 632, true);
            MiscFunctions.SaveFile<CM2Team>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.DB1", tmdata, 381, true);

            ApplyCorrectCount(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.DB1", 630, pldata1.Count);
            ApplyCorrectCount(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA2.DB1", 630, pldata2.Count);
            ApplyCorrectCount(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.DB1", 379, tmdata.Count);

            MiscFunctions.SaveFile<CM2Manager>(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\MGDATA.DB1", mgdata, 182, true);
            WriteCM2HistoryFile(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLHIST.BIN", plhist);

            WritePlayerDataToCSV(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA1.csv", pldata1);
            WritePlayerDataToCSV(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\PLDATA2.csv", pldata2);
            WriteTeamDataToCSV(@"C:\ChampMan\CM2\CM2_9697\Data\CM2\TMDATA.csv", tmdata);
        }

        /*
        int FindManagerIndex(List<CM2Manager> mgdata, string team)
        {
            var extraCheck = TeamMapper(team);

            //return mgdata.FindIndex(x => x.ReadString().StartsWithIgnoreBlank(team) || x.ShortName.ReadString().StartsWithIgnoreBlank(team) || x.LongName.ReadString().StartsWithIgnoreBlank(extraCheck) || x.ShortName.ReadString().StartsWithIgnoreBlank(extraCheck));
        }*/

        int FindTeamIndex(List<CM2Team> tmdata, string team)
        {
            var extraCheck = TeamMapper(team);

            return tmdata.FindIndex(x => x.LongName.ReadString().StartsWithIgnoreBlank(team) || x.ShortName.ReadString().StartsWithIgnoreBlank(team) || x.LongName.ReadString().StartsWithIgnoreBlank(extraCheck) || x.ShortName.ReadString().StartsWithIgnoreBlank(extraCheck));
        }

        public void ApplyCorrectCount(string file, int position, int count)
        {
            using (var f = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var bw = new BinaryWriter(f))
            {
                bw.Seek(position, SeekOrigin.Begin);
                bw.Write(ConvertShortToCM2Format((short)count));
            }
        }

        public List<CM2History> ReadCM2HistoryFile(string fileName)
        {
            var histories = new List<CM2History>();
            using (var f = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(f))
            {
                Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");

                var count = br.ReadInt16();

                for (int i = 0; i < count; i++)
                {
                    var history = new CM2History();
                    history.Name = MiscFunctions.GetTextFromBytes(br.ReadBytes(br.ReadByte()), true);
                    history.Nation = MiscFunctions.GetTextFromBytes(br.ReadBytes(br.ReadByte()), true);
                    history.BirthDate = MiscFunctions.GetTextFromBytes(br.ReadBytes(br.ReadByte()), true);

                    string Team = "";
                    while (true)
                    {
                        var Year = br.ReadByte();
                        if (Year == 0xff)
                            break;
                        
                        var details = new CM2History.CM2HistoryDetails();
                        details.Year = Year;

                        var length = br.ReadByte();
                        if (length != 0)
                            Team = MiscFunctions.GetTextFromBytes(br.ReadBytes(length), true);
                        details.Team = Team;
                        details.Apps = br.ReadByte();
                        details.Goals = br.ReadByte();
                        history.Details.Add(details);
                    }
                    histories.Add(history);
                }
            }
            return histories;
        }

        void WriteCM2HistoryFile(string fileName, List<CM2History> histories)
        {
            using (var f = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var bw = new BinaryWriter(f))
            {
                Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");

                bw.Write((Int16)histories.Count);

                foreach (var history in histories)
                {
                    bw.Write((byte)history.Name.Length);
                    bw.Write(latin1.GetBytes(history.Name));
                    bw.Write((byte)history.Nation.Length);
                    bw.Write(latin1.GetBytes(history.Nation));
                    bw.Write((byte)history.BirthDate.Length);
                    bw.Write(latin1.GetBytes(history.BirthDate));

                    var Team = "";
                    foreach (var details in history.Details)
                    {
                        bw.Write((byte)details.Year);
                        if (details.Team == Team)
                        {
                            bw.Write((byte)0x00);
                        }
                        else
                        {
                            bw.Write((byte)details.Team.Length);
                            bw.Write(latin1.GetBytes(details.Team));
                            Team = details.Team;
                        }
                        bw.Write((byte)details.Apps);
                        bw.Write((byte)details.Goals);
                    }
                    bw.Write((byte)0xff);
                }
            }
        }

        public void WritePlayerDataToCSV(string fileName, List<CM2Player> pldata)
        {
            using (var sw = new StreamWriter(fileName))
            {
                WriteLine(sw, "FirstName", "SecondName", "Nationality", "NationalCaps", "NationalGoals", "Team", "Unavailable", "DataSet", "BirthDate",
                      "Age", "Goalkeeper", "Sweeper", "Defence", "Anchor", "Midfield", "Support", "Attack", "RightSided", "LeftSided", "CentralSided", "Ability", "Potential", "Reputation",
                      "Aggression", "BigOccasion", "Character", "Consistency", "Creativity", "Determination", "Dirtyness", "Dribbling", "Flair", "Heading", "Influence", "InjProne", "Intelligence", "Marking", "OffTheBall", "Pace", "Passing",
                      "Positioning", "SetPieces", "Shooting", "Stamina", "Strength", "Tackling", "Technique");
                foreach (var p in pldata)
                    WritePlayer(sw, p);
            }
        }

        public void WriteTeamDataToCSV(string fileName, List<CM2Team> tmdata)
        {
            using (var sw = new StreamWriter(fileName))
            {
                WriteLine(sw, "LongName", "ShortName", "Nation", "Region", "Developed", "XCoord", "YCoord", "EEC", "TCoef8893",
                          "City", "Stadium", "Capacity", "Seating", "Following", "Standing", "Blend", "Formation", "Style", "FirstHomeCol", "SecondHomeCol", "FirstAwayCol", "SecondAwayCol", "Division",
                          "LastDivision", "LastPosition", "Cash", "LeagueStandard", "TransferSystem", "Wav");
                foreach (var t in tmdata)
                    WriteTeam(sw, t);
            }
        }

        public CM2Team CreateCM2Team(HistoryLoader hl, string team, string division)
        {
            CM2Team t = new CM2Team();

            var cm0102club = hl.club.Find(x => MiscFunctions.GetTextFromBytes(x.Name) == team);
            var cm0102stadium = hl.stadiums.Find(x => x.ID == cm0102club.Stadium);

            t.LongName = MiscFunctions.GetBytesFromText(team, 35);
            var shortName = MiscFunctions.GetTextFromBytes(cm0102club.ShortName);

            if (!string.IsNullOrEmpty(shortName) && shortName != team)
            { 
                t.ShortName = MiscFunctions.GetBytesFromText(shortName, 35);
            }

            t.Nation = MiscFunctions.GetBytesFromText(division[0] == 'E' ? "England" : "Scotland", 35);
            t.Stadium = MiscFunctions.GetBytesFromText(MiscFunctions.GetTextFromBytes(cm0102stadium.Name), 35);
            t.Capacity = ConvertLongToCM2Format(cm0102stadium.StadiumCapacity);
            t.Seating = ConvertLongToCM2Format(cm0102stadium.StadiumSeatingCapacity);
            t.Following = 10;
            t.Standing = 7;
            t.XCoord = 10;
            t.YCoord = 10;
            t.Style = MiscFunctions.GetBytesFromText("PASS", 10);
            t.FirstHomeCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.SecondHomeCol = MiscFunctions.GetBytesFromText("BLU", 15);
            t.FirstAwayCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.SecondAwayCol = MiscFunctions.GetBytesFromText("GRN", 15);
            t.Division = MiscFunctions.GetBytesFromText(division, 15);
            t.LastDivision = MiscFunctions.GetBytesFromText(division, 15);
            t.LastPosition = cm0102club.LastPosition;
            t.Cash = ConvertLongToCM2Format(cm0102club.Cash / 1000);
            t.Wav = MiscFunctions.GetBytesFromText(/*"silent"*/"ROCHDAL1", 15);

            return t;
        }

        public CM2Team CreateUnknownTeam(string teamName = "Unknown", string teamShortName = "Uknown")
        {
            CM2Team t = new CM2Team();

            t.LongName = MiscFunctions.GetBytesFromText(teamName, 35);
            t.ShortName = MiscFunctions.GetBytesFromText(teamShortName, 35);
            t.Nation = MiscFunctions.GetBytesFromText("Spain", 35);
            t.Stadium = MiscFunctions.GetBytesFromText("Unknown Stadium", 35);
            t.Capacity = ConvertLongToCM2Format(1000);
            t.Seating = ConvertLongToCM2Format(10000);
            t.Following = 10;
            t.Standing = 7;
            t.XCoord = 10;
            t.YCoord = 10;
            t.Style = MiscFunctions.GetBytesFromText("PASS", 10);
            t.FirstHomeCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.SecondHomeCol = MiscFunctions.GetBytesFromText("BLU", 15);
            t.FirstAwayCol = MiscFunctions.GetBytesFromText("WHI", 15);
            t.SecondAwayCol = MiscFunctions.GetBytesFromText("GRN", 15);
            t.Division = MiscFunctions.GetBytesFromText("", 15);
            t.LastDivision = MiscFunctions.GetBytesFromText("", 15);
            t.LastPosition = 1;
            t.Cash = ConvertLongToCM2Format(100);
            t.Wav = MiscFunctions.GetBytesFromText("silent", 15);

            return t;
        }

        public string TeamMapper(string team)
        {
            string extraCheck = team;
            switch (team)
            {
                case "Brighton and Hove Albion":
                    extraCheck = "Brighton";
                    break;
                case "Sheffield United":
                    extraCheck = "Sheff U";
                    break;
                case "Cardiff City":
                    extraCheck = "Cardiff C";
                    break;
                case "Hull City":
                    extraCheck = "Hull C";
                    break;
                case "Bristol City":
                    extraCheck = "Bristol C";
                    break;
                case "Chester City":
                    extraCheck = "Chester C";
                    break;
                case "York City":
                    extraCheck = "York C";
                    break;
                case "Notts County":
                    extraCheck = "Notts C";
                    break;
                case "AFC Wimbledon":
                    extraCheck = "Wimbledon";
                    break;
                case "Oxford United":
                    extraCheck = "Oxford U";
                    break;
                case "Dundee United":
                    extraCheck = "Dundee U";
                    break;
                case "Cambridge United":
                    extraCheck = "Cambridge U";
                    break;
                case "Rochdale AFC":
                    extraCheck = "Rochdale";
                    break;
                case "Paris Saint-Germain":
                    extraCheck = "Paris St.Germain FC";
                    break;
                case "Borussia M'gladbach":
                    extraCheck = "Borussia Monchengladbach";
                    break;
                default:
                    extraCheck = team.Replace(" FC", "");
                    break;
            }
            return extraCheck;
        }

        public List<string> ReadCM0102League(HistoryLoader hl, string league)
        {
            var foundLeague = hl.club_comp.Find(x => MiscFunctions.GetTextFromBytes(x.Name) == league);
            var alLClubs = hl.club.FindAll(x => x.Division == foundLeague.ID);
            return alLClubs.Select(x => MiscFunctions.GetTextFromBytes(x.Name)).ToList();
        }

        int maxNameLength = 0;

        public List<PlayerAndHistory> ReadCM0102Data(HistoryLoader hl, string teamName, List<CM2Team> tmdata, int yearModifier = -2)
        {
            var club = hl.club.Find(x => MiscFunctions.GetTextFromBytes(x.ShortName) == teamName || MiscFunctions.GetTextFromBytes(x.Name) == teamName );
            var staff = hl.staff.FindAll(x => x.ClubJob == club.ID && x.Player != -1);

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

            List<PlayerAndHistory> players = new List<PlayerAndHistory>();
            foreach (var s in staff)
            {
                var firstName = MiscFunctions.GetTextFromBytes(hl.first_names[s.FirstName].Name);
                var secondName = MiscFunctions.GetTextFromBytes(hl.second_names[s.SecondName].Name);
                var commonName = MiscFunctions.GetTextFromBytes(hl.common_names[s.CommonName].Name);

                // Remove accents
                // firstName = MiscFunctions.RemoveDiacritics(firstName);
                // secondName = MiscFunctions.RemoveDiacritics(secondName);
                // commonName = MiscFunctions.RemoveDiacritics(commonName);

                maxNameLength = Math.Max(maxNameLength, firstName.Length);
                maxNameLength = Math.Max(maxNameLength, secondName.Length);
                maxNameLength = Math.Max(maxNameLength, commonName.Length);

                // Have to cut to 20 letter (even though the max you get from CM0102 is 25)
                // Any more and you get addname 1 (maybe)
                int maxNameSize = 25;
                firstName = firstName.Substring(0, Math.Min(maxNameSize, firstName.Length));
                secondName = secondName.Substring(0, Math.Min(maxNameSize, secondName.Length));
                commonName = commonName.Substring(0, Math.Min(maxNameSize, commonName.Length));

                if (s.Nation == -1)
                    continue;

                var nation = MiscFunctions.GetTextFromBytes(hl.nation[s.Nation].Name);
                var team = teamName;
                DateTime dob;
                string birthdate = "";
                int age;
                if (s.DateOfBirth.Year != 0)
                {
                    dob = TCMDate.ToDateTime(s.DateOfBirth).AddYears(yearModifier);
                    birthdate = dob.ToString("dd.MM.yy");
                    age = 2001 - dob.Year;
                }
                else
                {
                    age = 2001 - (s.YearOfBirth - yearModifier);
                }
                
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
                var potential = ((short)p.PotentialAbility) < 0 ? 0 : p.PotentialAbility;
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

                var nationsToBeMapped = new string[] {
                    "Iran",
                    "Curaçao",
                    "Cuba",
                    "Namibia",
                    "Samoa",
                    "Grenada",
                    "The Philippines",
                    "Curaçao",
                    "Montserrat",
                    "Surinam",
                    "Curaçao",
                    "Antigua & Barbuda",
                    "Cape Verde Islands",
                    "St Kitts & Nevis",
                    "Oman",
                    "Antigua & Barbuda",
                    "The Congo",
                    "St Kitts & Nevis",
                    "The Congo",
                    "Martinique",
                    "Curaçao",
                    "Sierra Leone",
                    "Antigua & Barbuda",
                    "Antigua & Barbuda",
                    "Saint Lucia",
                    "Guyana",
                    "Indonesia",
                    "Pakistan",
                    "Sudan",
                    "Grenada",
                    "Grenada",
                    "Grenada",
                    "Antigua & Barbuda",
                    "Guyana",
                    "Montserrat",
                    "Guinea-Bissau",
                    "Guyana",
                    "Antigua & Barbuda",
                    "St Kitts & Nevis",
                    "Gibraltar",
                    "Seville"
                };
                if (nationsToBeMapped.Contains(nation))
                    nation = "France";

                var PandH = new PlayerAndHistory();
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

                // Have to copy here at the end as its a struct not a pointer
                PandH.player = newPlayer;

                if ((firstName == "David" && secondName == "Luiz") || commonName == "David Luiz")
                    Console.WriteLine();

                // Get History
                if (staffHistoryMap.ContainsKey(s.ID)  && s.DateOfBirth.Year != 0)
                {
                    var cm0102player_history = staffHistoryMap[s.ID].OrderBy(x => x.Year).ToList();
                    var history = new CM2History();
                    history.Name = MiscFunctions.GetTextFromBytes(newPlayer.SecondName) + "," + MiscFunctions.GetTextFromBytes(newPlayer.FirstName);
                    history.SetBirthDate(TCMDate.ToDateTime(s.DateOfBirth).AddYears(yearModifier));
                    history.Nation = nation;
                    foreach (var h in cm0102player_history)
                    {
                        CM2History.CM2HistoryDetails detail = new CM2History.CM2HistoryDetails();
                        detail.Year = (byte)(((h.Year - 1900) + yearModifier)+1);

                        if (detail.Year >= 99 /*|| detail.Year <= 94*/)
                            continue;

                        if (clubMap.ContainsKey(h.ClubID))
                        {
                            var cm0102TeamName = MiscFunctions.GetTextFromBytes(clubMap[h.ClubID].Name);
                            var cm0102ShortTeamName = MiscFunctions.GetTextFromBytes(clubMap[h.ClubID].ShortName);

                            // Remove accents from team names
                            cm0102TeamName = MiscFunctions.RemoveDiacritics(cm0102TeamName);
                            cm0102ShortTeamName = MiscFunctions.RemoveDiacritics(cm0102ShortTeamName);

                            // Get CM2 Short Name
                            var CM2ShortTeamName = MiscFunctions.GetTextFromBytes(tmdata.Find(x => MiscFunctions.GetTextFromBytes(x.ShortName) == cm0102ShortTeamName).ShortName);

                            if (string.IsNullOrEmpty(CM2ShortTeamName))
                                CM2ShortTeamName = "DONTMATCH";

                            // Some special mappings between team names where CM2 and CM0102 differ
                            var extraCheck = TeamMapper(cm0102TeamName);

                            // Get the original team name, let's try and keep that
                            var tmDataIndex = tmdata.FindIndex(x => MiscFunctions.GetTextFromBytes(x.LongName) == cm0102TeamName || MiscFunctions.GetTextFromBytes(x.LongName) == cm0102ShortTeamName || MiscFunctions.GetTextFromBytes(x.ShortName) == CM2ShortTeamName || MiscFunctions.GetTextFromBytes(x.LongName) == extraCheck || MiscFunctions.GetTextFromBytes(x.ShortName) == extraCheck);

                            // Now check that team exists in CM2
                            if (cm0102TeamName != teamName && tmDataIndex == -1)
                            {
                                if (tmdata.Count < 1100)
                                {
                                    tmdata.Insert(100, CreateUnknownTeam(cm0102TeamName, cm0102ShortTeamName));
                                    detail.Team = cm0102ShortTeamName;
                                }
                                else
                                    detail.Team = "Unknown";
                            }
                            else
                            {
                                if (tmDataIndex == -1)
                                    detail.Team = teamName;
                                else
                                    detail.Team = string.IsNullOrEmpty(MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].ShortName)) ? MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].LongName) : MiscFunctions.GetTextFromBytes(tmdata[tmDataIndex].ShortName);
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

                    PandH.history = history;
                }

                players.Add(PandH);
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

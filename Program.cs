using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace CM0102Patcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //football_api api = new football_api("df11781692msh76c090b6812c8d2p13d983jsn3352c6239cf9");
            //api.GetLeagues(2021, true);
/*
            CM9798.ShiftPlayerAges(@"C:\ChampMan\cm9798\Fresh\Data\CM9798", -5);

            //CM9798.LoadCM9798DataFromDirectory(@"C:\ChampMan\cm9798\Fresh\Data\CM9798");
            //CM9798.LoadCM9798DataFromDirectory(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG");

            CM9798.LimitEXNMESTXT(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\EXNMES.TXT");

            CM9798.Test();
*/
            /*
            CM9798.WriteTeamDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\TMDATA.CSV", CM9798.tmdata);
            CM9798.WritePlayerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\PLAYERS.CSV", CM9798.pldata);
            CM9798.WriteManagerDataToCSV(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\ORIG\MGDATA.CSV", CM9798.mgdata);

            Console.WriteLine("{0:X} {1:X}", CM2.ConvertLongToCM2Format(CM9798.pldata.Count), CM2.ConvertShortToCM2Format((short)CM9798.pldata.Count));
            Console.WriteLine("{0:X} {1:X} ({2})", CM2.ConvertLongToCM2Format(CM9798.mgdata.Count), CM2.ConvertShortToCM2Format((short)CM9798.mgdata.Count), CM9798.mgdata.Count);

            var xx = CM9798.pldata.Max(x => x.UniqueID);
            var xx2 = CM2.ConvertShortToCM2Format((short)xx);
            */
            /*

            CM9798.LoadCM9798DataFromDirectory(@"C:\ChampMan\cm9798\Fresh\Data\CM9798");

            xx = CM9798.mgdata.Max(x => x.UniqueID);
            xx2 = CM2.ConvertShortToCM2Format((short)xx);


            var surnames = CM9798.mgdata.Select(x => x._SecondName).Distinct().ToList();
            foreach (var name in surnames)
            {
                var strName = name.ReadString();
                bool foundZero = false;
                int namelen = 0;
                foreach (var x in name)
                {
                    if (name[0] == ' ')
                        Console.WriteLine("Weird3");
                    if (name[29] != 0)
                        Console.WriteLine("Weird4");

                    if (x == 0)
                        foundZero = true;
                    else
                    {
                        namelen++;
                        if (foundZero)
                            Console.WriteLine("Weird");
                    }
                }
                if (namelen <= 1)
                    Console.WriteLine("Weird2");
            }

            //var xx = CM9798.pldata.Where(x => x.SecondName.ToList());
            Console.WriteLine();
            */
            
            //LinearExecutableFixUps.CheckFixups(@"C:\Development\DOSTEST\DOSTEST.EXE");
            /*
            Patcher p = new Patcher();
            p.CreateReversePatches(@"C:\ChampMan\Championship Manager 0102\cm0102 - Fresh - 3_9_68.exe");
            */
            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\April2022\Data\index.dat");
            var Italy = hl.nation.FirstOrDefault(x => x.Name.ReadString() == "Italy");
            var ItalianClubs = hl.club.Where(x => x.Nation == Italy.ID).ToList();
            foreach (var club in ItalianClubs)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", 
                    club.Name.ReadString(), 
                    club.ShortName.ReadString(), 
                    club.Division >= 0 ? hl.club_comp.First(x => x.ID == club.Division).Name.ReadString() : club.Division.ToString(), 
                    club.LastDivision >= 0 ? hl.club_comp.First(x => x.ID == club.LastDivision).Name.ReadString() : club.LastDivision.ToString(), 
                    club.LastPosition);
            }
            */
            /*
            TCMDate date = new TCMDate();
            date.Day = 0x62;
            date.Year = 2005;
            var dtt = TCMDate.ToDateTime(date);*/
            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Millwall\Data\index.dat");
            foreach (var staff in hl.staff)
            {
                if (hl.first_names[staff.FirstName].Name.ReadString() == "Cherno")
                    Console.WriteLine();
                if (staff.Player >= 0)
                {
                    var player = hl.players[staff.Player];
                    if (player.PotentialAbility == -2)
                    {
                        Console.WriteLine("{0} {1} {2}", hl.first_names[staff.FirstName].Name.ReadString(), hl.second_names[staff.SecondName].Name.ReadString(), player.PotentialAbility);
                    }
                }
            }*/

            /*
            CM9798.LoadCM9798DataFromDirectory(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798");
            var tmdata = CM9798.tmdata;
            var pldata = CM9798.pldata;
            var mgdata = CM9798.mgdata;
            CM9798.WritePlayerDataToCSV(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\pldata.csv", pldata);
            CM9798.WriteTeamDataToCSV(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\TMDATA.CSV", tmdata);
            CM9798.WriteManagerDataToCSV(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\MGDATA.CSV", mgdata);

            int cut_count = 0;
            List<CM9798.CM9798Player> newpldata = pldata;
            List<CM9798.CM9798Team> newtmdata = tmdata;
            //List<CM9798.CM9798Manager> newmgdata = mgdata;
            
            List<CM9798.CM9798Manager> newmgdata = new List<CM9798.CM9798Manager>();
            foreach (var mgr in mgdata)
            {
                if (cut_count <= 200)
                {
                    cut_count++;
                    continue;
                }
                newmgdata.Add(mgr);
            }*/
            /*
            List<CM9798.CM9798Player> newpldata = new List<CM9798.CM9798Player>();
            foreach (var player in pldata)
            {
                if (CM2.ConvertShortToNormalFormat(player.Ability) == 0 && cut_count <= 1500)
                {
                    cut_count++;
                    continue;
                }
                newpldata.Add(player);
            }*/
            /*
            List<CM9798.CM9798Team> newtmdata = new List<CM9798.CM9798Team>();
            int cut_count = 0;
            var aldershot = tmdata.Find(x => x.LongName == "Aldershot");
            foreach (var team in tmdata)
            {
                if (CM2.ConvertShortToNormalFormat(team.Reputation) == 0 && (team.Nation == "Portugal" || team.Nation == "Germany") && cut_count <= 200)
                {
                    cut_count++;
                    continue;
                }
                newtmdata.Add(team);
            }
            var aldershot2 = newtmdata.Find(x => x.LongName == "Aldershot");
            */
            /*
            var plhist = new List<CM2.CM2History>();

            MiscFunctions.SaveFile<CM9798.CM9798Team>(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\TMDATA.DB1", newtmdata, CM9798.TeamDataStartPos);
            MiscFunctions.SaveFile<CM9798.CM9798Player>(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\PLAYERS.DB1", newpldata, CM9798.PlayerDataStartPos, true);
            MiscFunctions.SaveFile<CM9798.CM9798Manager>(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\MGDATA.DB1", newmgdata, CM9798.ManagerDataStartPos);

            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\TMDATA.DB1", CM9798.TeamDataStartPos - 8, newtmdata.Count, true);
            //CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\TMDATA.DB1", TeamDataStartPos-2, tmdata.Count);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\PLAYERS.DB1", 660, newpldata.Count, false);
            CM2.ApplyCorrectCount(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\MGDATA.DB1", CM9798.ManagerDataStartPos - 8, newmgdata.Count, true);
            CM2.WriteCM2HistoryFile(@"C:\ChampMan\cm9798\V2beta1\CM9798\Data\cm9798\PLHIST98.BIN", plhist, true);

            Console.WriteLine();*/
            /*
            using (StreamReader sr = new StreamReader(@"C:\ChampMan\Notes\2020\2021\Derby County Minus 12 Points.patch"))
            {
                int firstAddr = -1;
                int lastAddr = -1;
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    var splits = line.Split(' ');
                    if (splits.Length >= 3)
                    {
                        var addrStr = splits[0].Substring(0, splits[0].Length - 1);
                        var addr = Convert.ToInt32(addrStr, 16);
                        if (addr != lastAddr + 1)
                            firstAddr = addr;
                        lastAddr = addr;
                        var newAddr = (addr - firstAddr) + 0x006DC000 + (0xDE7383 - 0xDE7000);
                        Console.WriteLine("{0}: {1} {2}", newAddr.ToString("X8"), splits[1], splits[2]);
                    }
                }
            }*/

            //CM9798.Test();
            /*
            CM2 cm2 = new CM2();
            cm2.ReadData();
            */
            //CM9798.SavedPlayerCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLDATA1.S16");
            /*
            using (var sr = new StreamReader(@"C:\ChampMan\Championship Manager 0102\TestQuick\3.9.68-Normal\Championship Manager 01-02\Data\euro.cfg"))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (line.StartsWith("*"))
                        Console.WriteLine(line.Substring(1));
                }
            }*/
            /*
            HistoryLoader hl = new HistoryLoader();
            HistoryLoader hl_orig = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\2004\Data\index.dat");
            hl_orig.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\3.9.68-Normal\Championship Manager 01-02\Data\index.dat", true);

            foreach (var cont in hl.continent)
            {
                Console.WriteLine(cont.ContinentName.ReadString());
            }

            var SouthAmericaID = hl_orig.continent.First(x => x.ContinentName.ReadString() == "North America");
            var so_am = hl_orig.nation.Where(x => x.Continent == SouthAmericaID.ContinentID).ToList();
            foreach (var country in so_am)
                Console.WriteLine(country.Name.ReadString() + "\t" + country.Region);
            Console.WriteLine();*/
            /*
            foreach (var staff in hl.staff.Where(x => x.JobForClub == 1 && x.NonPlayer != -1))
            {
                var name = hl.first_names[staff.FirstName].Name.ReadString() + " " + hl.second_names[staff.SecondName].Name.ReadString();
                var job = staff.ClubJob;
                var nonPlayerInfo = hl.nonPlayers.First(x => x.ID == staff.NonPlayer);
                if (name == "Joel Glazer")
                {
                    Console.WriteLine();
                }
            }
            */

            /*
            var latvia = hl.nation.First(x => x.Name.ReadString() == "Bolivia");
            foreach (var club in hl.club)
            {
                if (club.Nation == latvia.ID)
                {
                    Console.WriteLine("{0} - {1}", club.Name.ReadString(), club.ShortName.ReadString());
                }
            }*/

            /*
            var serie = hl.club_comp.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == "French Ligue 2");
            int zerorep = 0;
            //foreach (var club in hl.club)
            List<int> removeClubs = new List<int>();
            for (int i = 0; i < hl.club.Count; i++)
            {
                if (hl.club[i].Division == -1 && hl.club[i].Reputation > 0 && hl.club[i].Reputation <= 500 && hl.club[i].HasLinkedClub == 0)
                    removeClubs.Add(i);
                /*
                if (hl.club[i].Reputation <= 1000)
                {
                    var temp = hl.club[i];
                    temp.Reputation = 1;
                    hl.club[i] = temp;
                }*/

            //if (club.Reputation > 1000 && club.Division == -1)
            //  Console.WriteLine("Hello");
            /*6
            if (club.Division == serie.ID)
            {
                Console.WriteLine("{0} ----- {1}  ({2})", MiscFunctions.GetTextFromBytes(club.Name), MiscFunctions.GetTextFromBytes(club.ShortName), club.Reputation);
            }*/
            /*}
            removeClubs.Sort();
            removeClubs.Reverse();
            foreach (var remove in removeClubs)
                hl.club.RemoveAt(remove);
            */
            //  hl.Save(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2021\Data\index.dat", true);


            /*
            // FOR CAMF
            int forYear = 1998;
            string folderYear = (forYear - 1900).ToString();

            HistoryLoader hl = new HistoryLoader();
            hl.Load(string.Format(@"C:\ChampMan\Championship Manager 0102\TestQuick\CamDateFix\CM\{0}\Data\index.dat", folderYear));
            var players = hl.staff.FindAll(x => x.ClubJob != -1 && x.Player != -1 && x.JobForClub == 11);

            DateTime now = new DateTime(forYear, 7, 15);

            Random rand = new Random();
            using (var sw = new StreamWriter(string.Format(@"C:\ChampMan\Championship Manager 0102\TestQuick\CamDateFix\CM\{0}\player.csv", folderYear)))
            {
                foreach (var player in players)
                {
                    var player_hist = hl.staff_history.Where(x => x.StaffID == player.ID).OrderByDescending(x => x.Year).ToList();
                    var player_stats = hl.players[player.Player];
                    int years_at_club = 0;
                    foreach (var hist in player_hist)
                    {
                        if (hist.ClubID != player.ClubJob)
                            break;
                        years_at_club++;
                    }
                    var last_year = player_hist.Count == 0 ? 0 : player_hist[0].Year;
                    var dob = TCMDate.ToDateTime(player.DateOfBirth);
                    var playerJoined = TCMDate.ToDateTime(player.DateJoinedClub);
                    var playerExpires = TCMDate.ToDateTime(player.DateExpiresClub);
                    int age = dob.Year > 1900 ? (int)((now - dob).TotalDays/365) : 0;

                    DateTime newPlayerJoined = new DateTime(1900, 1, 1);
                    DateTime newPlayerExpires = new DateTime(1900, 1, 1);
                    if (age > 0 && age <= 19)
                    {
                        switch (age)
                        {
                            default:
                                newPlayerExpires = new DateTime(forYear + rand.Next(4, 6), 6, 1);  // under 17 gets 4 to 5 year deal
                                break;
                            case 18:
                                newPlayerExpires = new DateTime(forYear + rand.Next(3, 5), 6, 1);  // 18 gets 3 to 4 year deal
                                break;
                            case 19:
                                newPlayerExpires = new DateTime(forYear + rand.Next(player_stats.CurrentAbility >= 120 ? 2 : 1, 3), 6, 1);  // 19 gets 1 to 2 year deal
                                break;
                        }
                    }
                    else
                    {
                        if (player_hist.Count > 0)
                        {
                            newPlayerJoined = new DateTime(forYear - years_at_club, 6, 1);
                            if (age >= 32)
                            {
                                newPlayerExpires = new DateTime(forYear + rand.Next(age == 32 ? 2 : 1, 3), 6, 1);  // between 1 or 2 years if 32 or over
                            }
                            else
                            {
                                {
                                    if (years_at_club >= 4)
                                        newPlayerExpires = new DateTime(forYear + rand.Next(player_stats.CurrentAbility >= 120 ? 2 : 1, 4), 6, 1);  // between 1 to 3 years been at club for 4 years or more
                                    else
                                        newPlayerExpires = new DateTime(forYear + (4 - years_at_club) + rand.Next(player_stats.CurrentAbility >= 120 ? 1 : 0, 2), 6, 1);
                                }
                            }
                        }
                    }

                    MiscFunctions.WriteCSVLine(sw,
                                               player.ID,
                                               hl.first_names[player.FirstName].Name.ReadString(),
                                               hl.second_names[player.SecondName].Name.ReadString(),
                                               age,
                                               hl.club[player.ClubJob].ShortName.ReadString(),
                                               playerJoined.Year <= 1900 ? "" : playerJoined.ToShortDateString(),
                                               playerExpires.Year <= 1900 ? "" : playerExpires.ToShortDateString(),
                                               player_hist.Count > 0 ? years_at_club.ToString() : "",
                                               newPlayerJoined.Year <= 1900 ? "" : newPlayerJoined.ToShortDateString(),
                                               newPlayerExpires.Year <= 1900 ? "" : newPlayerExpires.ToShortDateString(),
                                               newPlayerExpires.Year <= 1900 ? "" : (newPlayerExpires.Year - forYear).ToString()
                                               );

                    if (newPlayerJoined.Year >= 1900)
                        player.DateJoinedClub = TCMDate.FromDateTime(newPlayerJoined);
                    if (newPlayerExpires.Year >= 1900)
                        player.DateExpiresClub = TCMDate.FromDateTime(newPlayerExpires);
                }
            }
            hl.Save(string.Format(@"C:\ChampMan\Championship Manager 0102\TestQuick\CamDateFix\CM\{0}\Data\index.dat", folderYear), false, true, false);
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PatcherForm());
        }

        public static bool RunningInMono()
        {
            return (Type.GetType("Mono.Runtime") != null);
        }
    }
}

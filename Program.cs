using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace CM0102Patcher
{
    static class Program
    {
        static void patch()
        {
            //int year = 1991;
            using (var stream = System.IO.File.Open(@"C:\ChampMan\Championship Manager 0102\Cam91_Issue\Attempt3\cm91.exe", FileMode.Open))
            using (var bw = new BinaryWriter(stream))
            {
                Patcher patcher = new Patcher();

                // Set to normally be a USA start
                bw.Seek(0x1F99A1, SeekOrigin.Begin);        // Normally 7CD (1997)
                bw.Write((short)(1993));
                bw.Seek(0x1F99BC, SeekOrigin.Begin);        // Normally 7CE (1998)
                bw.Write((short)(1994));

                // Make USA The Hosts Replacing France
                bw.Seek(0x1F99E9, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x8B, 0x0D, 0xf8, 0xf4, 0x9c, 0x00 }); // Make USA the hosts (replacing France)
                bw.Seek(0x52DD67, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xA1, 0x54, 0xf2, 0x9c, 0x00 }); // Make Bolivia replace USA in qualifiers so we don't get 2 USAs

                // This, like the Euros shunts everything along, so the 1998 World Cup would get hosted by S.Korea and Japan. Replace with just France.
                bw.Seek(0x1F9A21, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x8B, 0x15, 0x00, 0xf3, 0x9c, 0x00, 0x89, 0x51, 0x28, 0x8B, 0x06, 0xB9, 0xFF, 0xFF, 0xFF, 0xFF, 0x90 });

                // Make Germany = S.Korea + Japan
                bw.Seek(0x1F9A5D, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x84, 0xf4 });
                bw.Seek(0x1F9A64, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xC7, 0x40, 0x4E, 0x61, 0x00, 0x00, 0x00 });    /// <---- Put 61 in (which is Japan)

                // We have the space maker - so we can put in the reputation fix for when we don't have qualifiers
                // Currently, if we don't have qualifiers the world cup will pick teams in alphabetical order. This fixes that based on rep.
                patcher.ApplyPatch(stream, new[] { new Patcher.HexPatch(0x201a83, "90e84799f3ff80787f0a0f8d7ad8320083c404ff4c2414e982d832009090"), new Patcher.HexPatch(0x52f308, "e97727cd") });

                bw.Seek(0x1F9DD3, SeekOrigin.Begin);    // Switzerland -> Belgium
                bw.Write(new byte[] { 0xA1, 0x44, 0xf2, 0x9c, 0x00, 0x89, 0x82, 0xC0, 0x01, 0x00, 0x00, 0xC7, 0x81, 0xC4, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });

                // New Approach for Switzerland + Sweden:
                // Doesn't work either - or maybe it does if you make BL = FD rather than FE
                bw.Seek(0x1F9D04, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x44, 0xf2 });
                bw.Seek(0x1F9D0E, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xC7, 0x82, 0x66, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });

                bw.Seek(0x1F9D19, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x44, 0xf2 });
                bw.Seek(0x1F9D23, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xC7, 0x82, 0x6e, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });

                // Change BL to FD, (use EDX rather than EAX)
                bw.Seek(0x1F9D2D, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xC6, 0x82, 0x72, 0x01, 0x00, 0x00, 0xFD, 0x90 });
                bw.Seek(0x1F9D3c, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x8a });

                bw.Seek(0x1F9CE9, SeekOrigin.Begin); // Wales/Scotland (easier as already dual nation hosted)
                bw.Write(new byte[] { 0x44, 0xf2 });
                bw.Seek(0x1F9CF7, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x38, 0xf3 });

                // Make 2004 Portugal
                bw.Seek(0x1F9D37, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x34, 0xf4 });
                bw.Seek(0x1F9D4D, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x34, 0xf4 });
                bw.Seek(0x1F9D63, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x34, 0xf4 });

                /*

                // Wembley Fix
                bw.Seek(0x45b843, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xeb });
                bw.Seek(0x45c40e, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xeb });

                // Turn off transfer_manager..cpp 10691
                // This is a bad one - you need to do more than turn it off
                // This occurs when the staff member being transferred does not have a Player pointer at +61
                // So you need to eject! :)
                // bw.Seek(0x4CC7BB, SeekOrigin.Begin);
                // bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                bw.Seek(0x4CC76A, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xEB, 0xA4, 0x90, 0x90, });

                // Turn off match_eng..cpp 612
                bw.Seek(0x2B896E, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                // Turn off match_eng..cpp 652
                bw.Seek(0x2B8AC5, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                // Turn off Cup 1187 error
                bw.Seek(0x11A396, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                // Turn off comp_stats..CPP 1664.
                bw.Seek(0x0A25A3, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                // German Regional
                List<int> startYearMinus1_GerRegional = new List<int> { 0x001DCF10, 0x001DD7FF, 0x001DD9FE,  };
                foreach (var offset in startYearMinus1_GerRegional)
                {
                    bw.Seek(offset, SeekOrigin.Begin);
                    bw.Write(YearChanger.YearToBytes(year - 1));
                }

                
                // Scotland
                List<int> startYearMinus1_Scotland = new List<int> { 0x3EE026, 0x3EEE61, 0x3EEF79, 0x3F0413, 0x3F0C00, 0x3F0E95, 0x3F2831, 0x3F297E, 0x3F2A4E, 0x3F2A8D, 0x3F31D4, 0x3F3F8B, 0x3F4F3F };
                foreach (var offset in startYearMinus1_Scotland)
                {
                    bw.Seek(offset, SeekOrigin.Begin);
                    bw.Write(YearChanger.YearToBytes(year - 1));
                }
                */
                /*
                // Remove Eidos Logo Splash Screen
                bw.Seek(0x1CCFB6, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                bw.Seek(0x1CCFD1, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xeb });
                bw.Seek(0x1CCFF9, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                

                // Change the name
                var newGameName1 = year.ToString().Substring(2) + "/" + (year + 1).ToString().Substring(2);
                var newGameName2 = year.ToString() + "/" + (year + 1).ToString().Substring(2);
                ByteWriter.WriteToBinaryWriter(bw, 0x5cd33d, newGameName1 + "\0");  // Window Title
                ByteWriter.WriteToBinaryWriter(bw, 0x68029d, newGameName2 + "\0");  // Main Menu Screen
                */
                /*
                // Remove the non-playable leagues for the 93 update (with fix so Select All does not select unselectable leagues)
                // (These should be all one patch - but i'm lazy and pressed for time :) )
                patcher.ApplyPatch(stream, new Patcher.HexPatch[] { new Patcher.HexPatch(0x202f05, "0f8473ac21"), new Patcher.HexPatch(0x202f0b, "807d000b74f4807d002a74ee807d002f74e8807d004474e2807d004b74dc807d00aa74d6807d008074d0807d009474ca807d005c74c4807d009a74be807d00c074b8807d00cf74b2e915a8210090"), new Patcher.HexPatch(0x41d767, "e99957deff90") });
                patcher.ApplyPatch(stream, new Patcher.HexPatch[] { new Patcher.HexPatch(0x202f10, "47"), new Patcher.HexPatch(0x202f16, "41"), new Patcher.HexPatch(0x202f1c, "3b"), new Patcher.HexPatch(0x202f22, "35"), new Patcher.HexPatch(0x202f28, "2f"), new Patcher.HexPatch(0x202f2e, "29"), new Patcher.HexPatch(0x202f34, "23"), new Patcher.HexPatch(0x202f3a, "1d"), new Patcher.HexPatch(0x202f40, "17"), new Patcher.HexPatch(0x202f46, "11"), new Patcher.HexPatch(0x202f4c, "0b"), new Patcher.HexPatch(0x202f52, "05"), new Patcher.HexPatch(0x202f58, "c6851c010000"), new Patcher.HexPatch(0x202f5f, "e91aac210090") });
                patcher.ApplyPatch(stream, new Patcher.HexPatch[] { new Patcher.HexPatch(0x202f5f, "833d042dae00107e0ac705042dae001000"), new Patcher.HexPatch(0x202f71, "00e907ac21"), new Patcher.HexPatch(0x41d5e3, "10") });

                // Turn off Swedish Second Division too
                bw.Seek(0x26A096, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xE9, 0x3B, 0xFF, 0xFF, 0xFF, 0x90 });

                // Turn off Merconorte Cup
                bw.Seek(0x431856, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xEB });*/
            }
        }

        static string TeamConvert(string club)
        {
            if (club == "Sporting Lisbon")
                return "Sporting CP";
            if (club == "QPR")
                return "Queens Park Rangers";
            if (club == "Blackburn")
                return "Blackburn Rovers";
            if (club == "Inter Milan")
                club = "Internazionale";
            if (club == "Unattached")
                club = "NO CLUB";
            if (club == "Los Angeles FC")
                club = "Los Angeles";
            if (club == "Cambridge")
                club = "Cambridge City";
            if (club == "Lommel")
                club = "Lommel SK";
            if (club == "Bayer Leverkusen")
                club = "Bayer 04 Leverkusen";
            if (club == "Roda")
                club = "Roda JC Kerkrade";
            if (club == "Burton")
                club = "Burton Albion";
            if (club == "Hacken")
                club = "BK Haken";
            if (club == "Cray Valley")
                club = "Cray Wanderers FC";
            if (club == "Waterford")
                club = "Waterford Bohemians";
            if (club == "Derry")
                club = "Derry City";
            if (club == "Hammarby")
                club = "Hammarby TFF";
            if (club == "Partick")
                club = "Partick Thistle FC";
            if (club == "Twente")
                club = "Twente Enschede FC";
            if (club == "Sutton")
                club = "Sutton United";
            return club;
        }

        static void ParseBBCTransfers(string indexDatFile, string transfersSource, string transferOutput)
        {
            HistoryLoader hl = new HistoryLoader();
            hl.Load(indexDatFile, false);
            using (StreamWriter wr = new StreamWriter(transferOutput))
            using (StreamReader sr = new StreamReader(transfersSource))
            {
                wr.WriteLine("Transfer\tInfo\tGood");

                var staffNames = hl.staffNamesNoDiacritics.Values.ToList();
                var clubNames = hl.clubNames.Keys.ToList();
                var shortClubNames = hl.club.Select(x => new { ShortName = x.ShortName.ReadString(), LongName = x.Name.ReadString(), ID = x.ID }).ToList();
                var playerNames = hl.staff.Select(x => { string name, basicName; hl.StaffToName(x, out name, out basicName); return new { basicName = MiscFunctions.RemoveDiacritics(basicName), name = MiscFunctions.RemoveDiacritics(name), staffObj = x }; }).ToList();
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (line.Contains("add-ons"))
                        line = line.Replace("add-ons", "");
                    if (line.Contains("["))
                    {
                        var playerName = line.Substring(0, line.IndexOf('[')).Trim();
                        string sourceTeam, destTeam;
                        if (line.Contains(" - "))
                        {
                            sourceTeam = line.Substring(line.IndexOf('[') + 1, line.LastIndexOf(" - ") - (line.IndexOf('[') + 1)).Trim();
                            destTeam = line.Substring(line.LastIndexOf(" - ") + 3, line.IndexOf(']') - (line.LastIndexOf(" - ") + 3)).Trim();
                        }
                        else
                        {
                            sourceTeam = line.Substring(line.IndexOf('[') + 1, line.LastIndexOf('-') - (line.IndexOf('[') + 1)).Trim();
                            destTeam = line.Substring(line.LastIndexOf('-') + 1, line.IndexOf(']') - (line.LastIndexOf('-') + 1)).Trim();
                        }
                        bool isLoan = line.Contains(" Loan");
                        var playerObj = playerNames.FirstOrDefault(x => x.basicName == playerName); //hl.staffNamesNoDiacritics.FirstOrDefault(x => x.Value == playerName);

                        sourceTeam = TeamConvert(sourceTeam);
                        destTeam = TeamConvert(destTeam);

                        float bestSimilarity = 0;
                        string bestClubName = "", bestClubNameShort = "";
                        float shortSimilarity = 0;
                        float longSimilarity = 0;
                        int clubID = 0;

                        int bestDLDistance = 255;
                        string bestDLDClubName = "";

                        if (sourceTeam == "Lommel")
                            Console.WriteLine();
                        foreach (var club in shortClubNames)
                        {
                            if (sourceTeam == "NO CLUB")
                            {
                                bestClubName = "NO CLUB";
                                bestSimilarity = 1f;
                                clubID = -1;
                                break;
                            }

                            if (club.LongName.EndsWith(" B") || club.LongName.EndsWith(" C") || club.LongName.EndsWith(" 2") || club.LongName.Contains("U21") || club.LongName.Contains("U23") || club.LongName.Contains("U19"))
                                continue;

                            shortSimilarity = MiscFunctions.GetSimilarity(sourceTeam, club.ShortName);
                            longSimilarity = MiscFunctions.GetSimilarity(sourceTeam, club.LongName);

                            var dldist = MiscFunctions.DamerauLevenshteinDistance(sourceTeam, club.ShortName);
                            var dldist2 = MiscFunctions.DamerauLevenshteinDistance(sourceTeam, club.LongName.Substring(0, sourceTeam.Length < club.LongName.Length ? sourceTeam.Length : club.LongName.Length));

                            if (dldist < bestDLDistance)
                            {
                                bestDLDistance = dldist;
                                bestDLDClubName = club.LongName;
                            }
                            else
                            if (dldist2 < bestDLDistance)
                            {
                                bestDLDistance = dldist2;
                                bestDLDClubName = club.LongName;
                            }

                            if (sourceTeam == "Lommel" && club.LongName.Contains("Lommel"))
                                Console.WriteLine();

                            if (club.ShortName == sourceTeam)
                            {
                                bestSimilarity = 1f;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                            else
                            if (longSimilarity > bestSimilarity)
                            {
                                bestSimilarity = longSimilarity;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                            else
                            if (shortSimilarity > bestSimilarity)
                            {
                                bestSimilarity = shortSimilarity;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                        }
                        string bestSourceClubName = bestClubName;
                        float sourceSimilarity = bestSimilarity;
                        int sourceClubID = clubID;
                        if (bestClubName != bestDLDClubName && bestClubName != "NO CLUB")
                            Console.WriteLine("DLD DIFFERENCE!");

                        var teams = shortClubNames.Where(x => x.LongName.StartsWith(destTeam)).ToList();

                        bestSimilarity = 0;
                        foreach (var club in shortClubNames)
                        {
                            if (club.LongName.EndsWith(" B") || club.LongName.EndsWith(" C") || club.LongName.EndsWith(" 2") || club.LongName.Contains("U21") || club.LongName.Contains("U23") || club.LongName.Contains("U19"))
                                continue;

                            shortSimilarity = MiscFunctions.GetSimilarity(club.ShortName, destTeam);
                            longSimilarity = MiscFunctions.GetSimilarity(club.LongName, destTeam);

                            if (club.ShortName == destTeam)
                            {
                                bestSimilarity = 1f;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                            else
                            if (longSimilarity > bestSimilarity)
                            {
                                bestSimilarity = longSimilarity;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                            else
                            if (shortSimilarity > bestSimilarity)
                            {
                                bestSimilarity = shortSimilarity;
                                bestClubName = club.LongName;
                                bestClubNameShort = club.ShortName;
                                clubID = club.ID;
                            }
                        }
                        string bestDestClubName = bestClubName;
                        float destSimilarity = bestSimilarity;
                        int destClubID = clubID;

                        string storedName = "";
                        if (playerObj == null)
                        {
                            if (playerName == "Momo Diaby")
                                Console.WriteLine();
                            // Can't find the player - see if we can find the player in the source team
                            float nameSimilarity = 0;
                            if (sourceSimilarity > 0.7)
                            {
                                var clubPlayers = hl.staff.Where(x => x.ClubJob == sourceClubID).ToList();
                                foreach (var clubPlayer in clubPlayers)
                                {
                                    string name, basicName;
                                    hl.StaffToName(clubPlayer, out name, out basicName);
                                    var similiarity = MiscFunctions.GetSimilarity(playerName, basicName);
                                    if (similiarity > nameSimilarity)
                                    {
                                        nameSimilarity = similiarity;
                                        storedName = name;
                                    }
                                }
                            }

                            if (nameSimilarity < 0.6)
                            {
                                var playerNotFound = string.Format("Cannot find player: {0}\t\tFALSE", playerName);
                                Console.WriteLine(playerNotFound);
                                wr.WriteLine(playerNotFound);
                                continue;

                            }
                        }
                        else
                            storedName = playerObj.name;

                        var lineOut = string.Format("{0} ({1}) -> {2}", storedName, bestSourceClubName, bestDestClubName);
                        if (isLoan)
                            lineOut += " [LOAN]";
                        lineOut += string.Format("\tsrcSim: {0} {2}, dstSim: {1} {3}\t{4}", sourceSimilarity, destSimilarity, sourceTeam, destTeam, (sourceSimilarity == 1f && destSimilarity == 1f) ? "TRUE" : "FALSE");
                        Console.WriteLine(lineOut);
                        wr.WriteLine(lineOut);
                    }
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            // Count the Dublin Clubs
            HistoryLoader hl = new HistoryLoader();
            //hl.Load(@"C:\ChampMan\Championship Manager 0102\Cam84\Romania Names\Data\index.dat");
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Original\Data\index.dat");
            var dupes = hl.staff.GroupBy(x => x.ID).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            var dupes2 = hl.club.GroupBy(x => x.ID).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            var dupes3 = hl.players.GroupBy(x => x.ID).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            var dupes4 = hl.nonPlayers.GroupBy(x => x.ID).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            var dinamo = hl.club.Where(x => x.Name.ReadString().Contains("Dinamo Bucharest")).ToList();
            var bihor = hl.club.Where(x => x.Name.ReadString().Contains("Bihor")).ToList();
            var player_count = hl.players.Count;
            var nonplayer_count = hl.nonPlayers.Count;
            Console.WriteLine();
            */
            //ParseBBCTransfers(@"C:\ChampMan\Championship Manager 0102\TestQuick\April2023\Data\index.dat",  @"c:\downloads\Transfers_August_2023.txt", @"c:\downloads\Transfers_2023.txt");

            /*
            // Count the Dublin Clubs
            HistoryLoader hl = new HistoryLoader();
            //hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\April2023\Data\index.dat");
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\quick\Data\index.dat");
            int count = 1;
            //var first = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "League of Ireland First Division".ToLower());
            //var prem = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "League of Ireland Premier Division".ToLower());
            var first = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "Irish First Division".ToLower());
            var prem = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "Irish Premier Division".ToLower());
            var leinster1 = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "Irish Leinster Senior League Division One".ToLower());
            var leinster2 = hl.club_comp.FirstOrDefault(x => x.Name.ReadString().ToLower() == "Irish Leinster Senior League Premier".ToLower());
            foreach (var club in hl.club)
            {
                var clubName = club.Name.ReadString().ToLower(); ;
                if (club.Division == first.ID || club.Division == prem.ID )//|| club.Division == leinster1.ID || club.Division == leinster2.ID)
                {
                    if (club.Stadium > 0)
                    {
                        var stadium = hl.stadiums.FirstOrDefault(x => x.ID == club.Stadium);
                        if (stadium != null)
                        {
                            var stadiumName = stadium.Name.ReadString();
                            var city = hl.cities.FirstOrDefault(x => x.ID == stadium.StadiumCity);
                            if (city != null)
                            {
                                var cityName = city.Name.ReadString().ToLower();
                                if (cityName.Length >= 4 && cityName.Substring(0, 4) == "dubl")
                                {
                                    Console.WriteLine(count++ + ". " + club.Name.ReadString() + " " + hl.club_comp.First(x => x.ID == club.Division).Name.ReadString());
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
            */
            /*
            Logger.Log(@"C:\ChampMan\Championship Manager 0102\TestQuick\Leeds2022\cm0102_logger.exe", "Test Start String");
            Logger.Log(@"C:\ChampMan\Championship Manager 0102\TestQuick\Leeds2022\cm0102_logger.exe", "String 2");
            var strs = Logger.ReadStrings(@"C:\ChampMan\Championship Manager 0102\TestQuick\Leeds2022\cm0102_logger.exe");
            */
            //patch();
            /*
            // For match man 1225 errors! :)
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\Cam91_Issue\Attempt1\Data\index.dat");
            foreach (var club in hl.club)
            {
                var dupes = club.Squad.Where(x => x != -1).GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                if (dupes.Count > 0)
                {
                    foreach (var dupe in dupes)
                    {
                        string name = "Unknown";
                        hl.StaffToName(hl.staff[dupe], out name);
                        Console.WriteLine("Dupe Quad Found for Club {0} ({1}) - Player: {2}", club.Name.ReadString(), club.ShortName.ReadString(), name);
                    }
                }
            }
            */
            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2022_Test2\Data\index.dat");
            var Italy = hl.nation.Find(x => x.Name.ReadString() == "Italy");
            var allItalianClubs = hl.club.FindAll(x => x.Nation == Italy.ID);
            allItalianClubs.ForEach(x => {
                if (x.Name.ReadString().ToLower().Contains("brus") || x.ShortName.ReadString().ToLower().Contains("brus"))
                    Console.WriteLine("{0} {1}", x.Name.ReadString(), x.ShortName.ReadString());
            });
            */

            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2022_Test2\Data\index.dat");
            var Liechtenstein = hl.nation.Find(x => x.Name.ReadString() == "Liechtenstein");
            var SanMarino = hl.nation.Find(x => x.Name.ReadString() == "San Marino");
            int oldLichID = Liechtenstein.ID;
            int oldSanMarinoID = SanMarino.ID;
            Liechtenstein.ID = SanMarino.ID;
            SanMarino.ID = oldLichID;
            int lichIndex = hl.nation.IndexOf(Liechtenstein);
            int sanIndex = hl.nation.IndexOf(SanMarino);
            hl.nation[lichIndex] = SanMarino;
            hl.nation[sanIndex] = Liechtenstein;
            hl.nation_comp.ForEach(x => { if (x.ClubCompNation == oldLichID) x.ClubCompNation = oldSanMarinoID; else if (x.ClubCompNation == oldSanMarinoID) x.ClubCompNation = oldLichID; });
            hl.nat_club.ForEach(x => { if (x.Nation == oldLichID) x.Nation = oldSanMarinoID; else if (x.Nation == oldSanMarinoID) x.Nation = oldLichID; });
            hl.Save(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2022_Test2\Data\index.dat", false, false, true);
            */
            /*
            var now = new DateTime(2022, 1, 1);
            for (int i = 0; i < 365; i++)
            {
                var tcm = TCMDate.FromDateTime(now);
                Console.WriteLine("{0} {1} = {2:X}", now.ToString("MMMM", CultureInfo.InvariantCulture), now.Day, tcm.Day);
                now = now.AddDays(1);
            }
            */
            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\CM93 Swiss\Data\index.dat");
            int albcount = 0;
            foreach (var staff in hl.staff)
            {
                if (staff.SecondNation == 1)
                    staff.SecondNation = -1;
                if (staff.SecondNation < -1)
                    staff.SecondNation = -1;
            }
            hl.Save(@"C:\ChampMan\Championship Manager 0102\CM93 Swiss\Data\index.dat", false, true, false);
            */
            //football_api api = new football_api("");
            //api.GetLeagues(2022, false);
            //api.GetCM0102Leagues(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2022_Test1\Data\index.dat", 2022, false, true);
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

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
            catch { }
            Application.Run(new PatcherForm());
        }

        public static bool RunningInMono()
        {
            return (Type.GetType("Mono.Runtime") != null);
        }
    }
}

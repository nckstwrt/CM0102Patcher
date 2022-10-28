using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Threading;

namespace CM0102Patcher
{
    public class football_api
    {
        public string key;
        public string indexFile;

        public football_api(string key, string indexFile = null)
        {
            this.key = key;
            this.indexFile = indexFile;
        }

        public void DumpTeamsAndLeaguesToCSV()
        {
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\2020_orig\Championship Manager 01-02\Data\index.dat");

            using (var sw = new StreamWriter(@"c:\ChampMan\notes\Clubs.csv"))
            {
                MiscFunctions.WriteCSVLine(sw, "ID", "TeamName", "Division");
                foreach (var club in hl.club)
                {
                    MiscFunctions.WriteCSVLine(sw, club.ID, MiscFunctions.GetTextFromBytes(club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.FirstOrDefault(x => x.ID == club.Division).Name));
                }
            }
            using (var sw = new StreamWriter(@"c:\ChampMan\notes\ClubComps.csv"))
            {
                MiscFunctions.WriteCSVLine(sw, "ID", "CompName", "Nation");
                foreach (var comp in hl.club_comp)
                {
                    MiscFunctions.WriteCSVLine(sw, comp.ID, MiscFunctions.GetTextFromBytes(comp.Name), MiscFunctions.GetTextFromBytes(hl.nation.FirstOrDefault(x => x.ID == comp.ClubCompNation).Name));
                }
            }
        }

        public void GetCM0102Leagues(string indexDatFile, int year, bool runForItaly = true, bool runForFrance = true)
        {
            float bestSimilarity;
            HistoryLoader hl = new HistoryLoader();
            hl.Load(indexDatFile);

            if (runForItaly)
            {
                var serie_a_clubs = GetCM0102League(hl, "Italian Serie A");
                var api_serie_a_clubs = GetAPILeague("Italy", "Serie A", year);

                List<TeamMatch> teamMatches;

                teamMatches = MatchTeams(api_serie_a_clubs, serie_a_clubs);

                // Teams Missing
                var serie_a_team1 = teamMatches[0];
                var serie_a_team2 = teamMatches[1];

                Console.WriteLine("Serie A Team Missing From CM0102: {0} (1)", serie_a_team1.apiTeamName);
                Console.WriteLine("Serie A Team Missing From CM0102: {0} (2)", serie_a_team2.apiTeamName);

                // Get Serie B teams
                var api_serie_b_clubs = GetAPILeague("Italy", "Serie B", year);
                var serie_b_clubs = GetCM0102League(hl, "Italian Serie B");

                var serie_b_club_to_go_to_seriea_1 = FindTeamFromName(serie_b_clubs, teamMatches[0].apiTeamName, out bestSimilarity);
                var serie_b_club_to_go_to_seriea_2 = FindTeamFromName(serie_b_clubs, teamMatches[1].apiTeamName, out bestSimilarity);
                Console.WriteLine("Found Serie B Team To Move To Serie A: {0} (1)", MiscFunctions.GetTextFromBytes(serie_b_club_to_go_to_seriea_1.ShortName));
                Console.WriteLine("Found Serie B Team To Move To Serie A: {0} (2)", MiscFunctions.GetTextFromBytes(serie_b_club_to_go_to_seriea_2.ShortName));
                //hl.UpdateClubsDivision(serie_b_club_to_go_to_seriea_1.ID, serie_a_clubs[0].Division);
                //hl.UpdateClubsDivision(serie_b_club_to_go_to_seriea_2.ID, serie_a_clubs[0].Division);
                serie_b_clubs = GetCM0102League(hl, "Italian Serie B"); // Re-get the Serie B clubs now we've moved 2 into Serie A

                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_b_club_to_go_to_seriea_1.Name), "Italian Serie A");
                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_b_club_to_go_to_seriea_2.Name), "Italian Serie A");

                Console.WriteLine("Serie B");
                teamMatches = MatchTeams(api_serie_b_clubs, serie_b_clubs);

                // Teams Missing
                var serie_b_team1 = teamMatches[0];
                var serie_b_team2 = teamMatches[1];
                Console.WriteLine("Serie B Team Missing From CM0102: {0} 0x{1:X} (1)", serie_b_team1.apiTeamName, serie_b_team1.cm0102ClubID);
                Console.WriteLine("Serie B Team Missing From CM0102: {0} 0x{1:X} (2)", serie_b_team2.apiTeamName, serie_b_team2.cm0102ClubID);

                // Get Serie C1/A Teams
                var api_serie_c1a_clubs = GetAPILeague("Italy", "Serie C", year);
                var serie_c1a_clubs = GetCM0102League(hl, "Italian Serie C1/A");

                var serie_c1a_club_to_go_to_serieb_1 = FindTeamFromName(serie_c1a_clubs, serie_b_team1.apiTeamName, out bestSimilarity);
                var serie_c1a_club_to_go_to_serieb_2 = FindTeamFromName(serie_c1a_clubs, serie_b_team2.apiTeamName, out bestSimilarity);
                Console.WriteLine("Found Serie C1A Team To Move To Serie B: {0} (1)", MiscFunctions.GetTextFromBytes(serie_c1a_club_to_go_to_serieb_1.ShortName));
                Console.WriteLine("Found Serie C1A Team To Move To Serie B: {0} (2)", MiscFunctions.GetTextFromBytes(serie_c1a_club_to_go_to_serieb_2.ShortName));
                //hl.UpdateClubsDivision(serie_c1a_club_to_go_to_serieb_1.ID, serie_b_clubs[0].Division);
                //hl.UpdateClubsDivision(serie_c1a_club_to_go_to_serieb_2.ID, serie_b_clubs[0].Division);
                serie_c1a_clubs = GetCM0102League(hl, "Italian Serie C1/A"); // Re-get the Serie B clubs now we've moved 2 into Serie A

                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_c1a_club_to_go_to_serieb_1.Name), "Italian Serie B");
                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_c1a_club_to_go_to_serieb_2.Name), "Italian Serie B");

                Console.WriteLine("Serie C");
                teamMatches = MatchTeams(api_serie_c1a_clubs, serie_c1a_clubs);

                // Grab 2 teams from Serie D and put them in C1A
                var serie_d_clubs = GetCM0102League(hl, "Italian Serie D");
                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_d_clubs[0].Name), "Italian Serie C1/A");
                Console.WriteLine("CHANGECLUBDIVISION: \"{0}\" \"{1}\"", MiscFunctions.GetTextFromBytes(serie_d_clubs[1].Name), "Italian Serie C1/A");

                ////////////////////////////////////////////////////////////////////////////
                // Load all CM0102 Teams and use them to match to the API
                var serie_c1b_clubs = GetCM0102League(hl, "Italian Serie C1/B");
                var serie_c2a_clubs = GetCM0102League(hl, "Italian Serie C2/A");
                var serie_c2b_clubs = GetCM0102League(hl, "Italian Serie C2/B");
                var serie_c2c_clubs = GetCM0102League(hl, "Italian Serie C2/C");
                var allItalianClubs = new List<TClub>();
                allItalianClubs.AddRange(serie_a_clubs);
                allItalianClubs.AddRange(serie_b_clubs);
                allItalianClubs.AddRange(serie_c1a_clubs);
                allItalianClubs.AddRange(serie_c1b_clubs);
                allItalianClubs.AddRange(serie_c2a_clubs);
                allItalianClubs.AddRange(serie_c2b_clubs);
                allItalianClubs.AddRange(serie_c2c_clubs);
                allItalianClubs.AddRange(serie_d_clubs);

                Console.WriteLine("SERIE A");
                foreach (var club in api_serie_a_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allItalianClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("SERIE B");
                foreach (var club in api_serie_b_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allItalianClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("SERIE C1/A");
                foreach (var club in api_serie_c1a_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allItalianClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("SERIE C1/B");
                foreach (var club in api_serie_c1a_clubs.api.standings[1])
                {
                    var cm0102_club = FindTeamFromName(allItalianClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("SERIE C1/C");
                foreach (var club in api_serie_c1a_clubs.api.standings[2])
                {
                    var cm0102_club = FindTeamFromName(allItalianClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
            }

            if (runForFrance)
            {
                ////////////////////////////////////////////////////////////////////////
                /// Do Same for French Leagues
                /// 
                var allFrenchClubs = new List<TClub>();
                allFrenchClubs.AddRange(GetCM0102League(hl, "French First Division"));
                allFrenchClubs.AddRange(GetCM0102League(hl, "French Second Division"));
                allFrenchClubs.AddRange(GetCM0102League(hl, "French National"));
                allFrenchClubs.AddRange(GetCM0102League(hl, "French Lower Division"));
                var api_ligue_1_clubs = GetAPILeague("France", "Ligue 1", year);
                var api_ligue_2_clubs = GetAPILeague("France", "Ligue 2", year);
                var api_ligue_national_clubs = GetAPILeague("France", "National 1", year);
                Console.WriteLine("Ligue 1\r\nAPI Name\tCM0102 Name\tAPI Division\tMatch");
                foreach (var club in api_ligue_1_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allFrenchClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("\r\nLigue 2\r\nAPI Name\tCM0102 Name\tAPI Division\tMatch");
                foreach (var club in api_ligue_2_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allFrenchClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
                Console.WriteLine("\r\nNational\r\nAPI Name\tCM0102 Name\tAPI Division\tMatch");
                foreach (var club in api_ligue_national_clubs.api.standings[0])
                {
                    var cm0102_club = FindTeamFromName(allFrenchClubs, club.teamName, out bestSimilarity);
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", club.teamName, MiscFunctions.GetTextFromBytes(cm0102_club.Name), MiscFunctions.GetTextFromBytes(hl.club_comp.Find(x => x.ID == cm0102_club.Division).Name), bestSimilarity);
                }
            }
        }

        List<TeamMatch> MatchTeams(api_standings api_teams, List<TClub> cm0102Teams)
        {
            List<TeamMatch> teamMatches = new List<TeamMatch>();
            foreach (var club in api_teams.api.standings[0])
            {
                // Foreach API club compare it to the CM0102 team
                float bestSimilarity = 0;
                var club_id = 0;
                foreach (var cm0102club in cm0102Teams)
                {
                    // Hack for Nuovo Cosenza
                    club.teamName = club.teamName.Replace("Nuova ", "");

                    float similarityShort = MiscFunctions.GetSimilarity(MiscFunctions.GetTextFromBytes(cm0102club.ShortName).ToLower(), club.teamName.ToLower());
                    float similarityLong = MiscFunctions.GetSimilarity(MiscFunctions.GetTextFromBytes(cm0102club.Name).ToLower(), club.teamName.ToLower());

                    float similarity = similarityLong > similarityShort ? similarityLong : similarityShort;

                    if (similarity > bestSimilarity)
                    {
                        club_id = cm0102club.ID;
                        bestSimilarity = similarity;
                    }
                }

                var teamMatch = new TeamMatch();
                teamMatch.apiClubID = club.team_id.Value;
                teamMatch.cm0102ClubID = club_id;
                teamMatch.similarity = bestSimilarity;
                teamMatch.apiTeamName = club.teamName;
                teamMatch.cm0102LongTeamName = MiscFunctions.GetTextFromBytes(cm0102Teams.Find(x => x.ID == club_id).Name);
                teamMatches.Add(teamMatch);

                Console.WriteLine("Matched {0} to {1} ({2})", club.teamName, MiscFunctions.GetTextFromBytes(cm0102Teams.First(x => x.ID == club_id).Name), bestSimilarity);
            }

            teamMatches.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            return teamMatches;
        }

        TClub FindTeamFromName(List<TClub> clubs, string clubName, out float bestSimilarity)
        {
            bestSimilarity = 0;
            TClub ret = default;
            foreach (var cm0102club in clubs)
            {
                float similarityShort = MiscFunctions.GetSimilarity(MiscFunctions.GetTextFromBytes(cm0102club.ShortName).ToLower(), clubName.ToLower());
                float similarityLong = MiscFunctions.GetSimilarity(MiscFunctions.GetTextFromBytes(cm0102club.Name).ToLower(), clubName.ToLower());

                float similarity = similarityLong > similarityShort ? similarityLong : similarityShort;

                if (similarity > bestSimilarity)
                {
                    ret = cm0102club;
                    bestSimilarity = similarity;
                }
            }
            return ret;
        }

        List<TClub> GetCM0102League(HistoryLoader hl, string league_name)
        {
            var club_comp = hl.club_comp.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == league_name);
            return hl.club.FindAll(x => x.Division == club_comp.ID);
        }

        api_standings GetAPILeague(string country, string league_name, int year)
        {
            var api_leagues = GetJSON<api_leagues>(@"C:\ChampMan\Notes\api-football-leagues.txt" /*"https://api-football-v1.p.rapidapi.com/v2/leagues"*/);
            var api_standing = api_leagues.api.leagues.FindAll(x => x.country == country && (league_name == null ? true : (x.name == league_name)) && x.season == year);
            return GetJSON<api_standings>("https://api-football-v1.p.rapidapi.com/v2/leagueTable/" + api_standing[0].league_id, string.Format(@"C:\ChampMan\Notes\api-football-league-serie-a-{0}.txt", api_standing[0].league_id));
        }

        public void GetLeagues(int year = 2021, bool readFromFile = true)
        {
            string leaguesFile = @"C:\ChampMan\Notes\api-football-leagues.txt";
            api_leagues leagues;
            if (readFromFile)
                leagues = GetJSON<api_leagues>(leaguesFile);
            else
            {
                leagues = GetJSON<api_leagues>("https://api-football-v1.p.rapidapi.com/v2/leagues", leaguesFile);
            }
            var eng_prem = leagues.api.leagues.FirstOrDefault(x => x.name == "Premier League" && x.season == year && x.country == "England");
            var italy_leagues = leagues.api.leagues.FindAll(x => x.country == "Italy" && x.season == year);

            var selectedCountries = new List<string> { "Italy", "England", "France", "Sweden" };
            var selectedleagues = leagues.api.leagues.FindAll(x => selectedCountries.Contains(x.country) && x.season == year && !x.name.Contains("Women"));

            using (var sw = new StreamWriter(@"C:\ChampMan\Notes\api-football-league-data.txt"))
            {
                foreach (var league in selectedleagues)
                {
                    Thread.Sleep(5000);
                    try
                    {
                        var standings = GetJSON<api_standings>("https://api-football-v1.p.rapidapi.com/v2/leagueTable/" + league.league_id, string.Format(@"C:\ChampMan\Notes\api-football-league-{0}.txt", league.league_id));
                        foreach (var standing in standings.api.standings)
                        {
                            for (int i = 0; i < standing.Count; i++)
                            {
                                if (i == 0)
                                {
                                    string header = string.Format("{0} - {1} ({2})", standing[i].group, league.country, standing.Count);
                                    sw.WriteLine(header);
                                    for (int j = 0; j < header.Length; j++)
                                        sw.Write("-");
                                    sw.WriteLine();
                                }
                                sw.WriteLine(standing[i].teamName);
                            }
                            sw.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public T GetJSON<T>(string source, string dumpToFile = null)
        {
            // TLS 1.2 issue
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            var Serializer = new JavaScriptSerializer();
            Serializer.MaxJsonLength = Int32.MaxValue;

            if (source.ToLower().StartsWith("https://"))
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("X-RapidAPI-Key", key);
                    //client.Headers.Add("x-rapidapi-host", "api-football-v1.p.rapidapi.com");

                    using (var leagueStream = client.OpenRead(source))
                    {
                        using (var streamReader = new StreamReader(leagueStream))
                        {
                            var json = streamReader.ReadToEnd();

                            if (!string.IsNullOrEmpty(dumpToFile))
                            {
                                using (var sw = new StreamWriter(dumpToFile))
                                {
                                    sw.WriteLine(json);
                                }
                            }

                            return Serializer.Deserialize<T>(json);
                        }
                    }
                }
            }
            else
            {
                using (var streamReader = new StreamReader(source))
                {
                    var json = streamReader.ReadToEnd();
                    return Serializer.Deserialize<T>(json);
                }
            }
        }
    }

    public class api_leagues
    {
        public class apiClass
        {
            public int results;
            public List<league> leagues;
        }

        public class league
        {
            public int league_id;
            public string name;
            public string type;
            public string country;
            public string country_code;
            public int season;
            public string season_start;
            public string season_end;
            public string logo;
            public string flag;
            public int standings;
            public int is_current;
            public coverage coverage;
        }

        public class coverage
        {
            public bool standings;
            public fixtures fixtures;
            public bool players;
            public bool topScorers;
            public bool predictions;
            public bool odds;
        }

        public class fixtures
        {
            public bool events;
            public bool lineups;
            public bool statistics;
            public bool players_statistics;
        }

        public apiClass api;
    }

    public class api_standings
    {
        public class apiClass
        {
            public int results;
            public List<List<standing>> standings;
        }

        public class standing
        {
            public int rank;
            public int? team_id;
            public string teamName;
            public string logo;
            public string group;
            public string forme;
            public string status;
            public string description;
            public data all;
            public data home;
            public data away;
            public int goalsDiff;
            public int points;
            public string lastUpdate;
        }

        public class data
        {
            public int matchesPlayed;
            public int win;
            public int draw;
            public int lose;
            public int goalsFor;
            public int goalsAgainst;
        }

        public apiClass api;
    }

    public class TeamMatch
    {
        public int apiClubID;
        public int cm0102ClubID;
        public float similarity;
        public string apiTeamName;
        public string cm0102LongTeamName;
    }
}

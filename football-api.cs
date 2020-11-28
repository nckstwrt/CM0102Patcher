using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace CM0102Patcher
{
    public class football_api
    {
        public string key;

        public football_api(string key)
        {
            this.key = key;
        }

        public void GetLeagues()
        {
            var leagues = GetJSON<api_leagues>(@"C:\ChampMan\Notes\api-football-leagues.txt" /*"https://api-football-v1.p.rapidapi.com/v2/leagues"*/);
            var eng_prem = leagues.api.leagues.FirstOrDefault(x => x.name == "Premier League" && x.season == 2020 && x.country == "England");
            var italy_leagues = leagues.api.leagues.FindAll(x => x.country == "Italy" && x.season == 2020);

            using (var sw = new StreamWriter(@"C:\ChampMan\Notes\api-football-league-data.txt"))
            {
                foreach (var italy_league in italy_leagues)
                {
                    var standings = GetJSON<api_standings>("https://api-football-v1.p.rapidapi.com/v2/leagueTable/" + italy_league.league_id, string.Format(@"C:\ChampMan\Notes\api-football-league-{0}.txt", italy_league.league_id));
                    foreach (var standing in standings.api.standings)
                    {
                        for (int i = 0; i < standing.Count; i++)
                        {
                            if (i == 0)
                            {
                                sw.WriteLine(standing[i].group);
                                for (int j = 0; j < standing[i].group.Length; j++)
                                    sw.Write("-");
                                sw.WriteLine();
                            }
                            sw.WriteLine(standing[i].teamName);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

        public T GetJSON<T>(string source, string dumpToFile = null)
        {
            var Serializer = new JavaScriptSerializer();
            Serializer.MaxJsonLength = Int32.MaxValue;

            if (source.ToLower().StartsWith("https://"))
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("x-rapidapi-key", key);
                    client.Headers.Add("x-rapidapi-host", "api-football-v1.p.rapidapi.com");

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
}

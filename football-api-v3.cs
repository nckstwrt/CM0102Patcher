using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CM0102Patcher
{
    public class football_api_v3
    {
        public string key;
        public string cachePath = @"c:\champman\notes\apiv3";
        public int year;

        public football_api_v3(string key)
        {
            this.key = key;
            this.year = DateTime.Now.Year;
            //var leagues = GetJSON<Wrapper<League>>("https://v3.football.api-sports.io/leagues");
            //var teams = GetJSON<Wrapper<Team>>("https://v3.football.api-sports.io/teams?league=39&season=2023", true, "team_league39_season2023");
            //var players = GetJSON<Wrapper<Player>>("https://v3.football.api-sports.io/players?league=39&season=2023", true, "players_league39_season2023");

            var allPlayers = GetPlayers(39, 2023);

            Console.WriteLine();
        }

        public List<Player> GetPlayers(int league, int year)
        {
            List<Player> players = new List<Player>();
            int page = 1;
            while (true)
            {
                var playerPage = GetJSON<Wrapper<Player>>(string.Format("https://v3.football.api-sports.io/players?league={0}&season={1}&page={2}", league, year, page), true, string.Format("players_league{0}_season{1}_page{2}", league, year, page));
                if (playerPage.errors.Count > 0)
                {
                    Console.WriteLine("Error getting players");
                    break;
                }
                players.AddRange(playerPage.response);
                page = playerPage.paging.current + 1;
                if (page > playerPage.paging.total)
                    break;
            }
            return players;
        }

        public T GetJSON<T>(string source, bool useCache = true, string overrideCacheFileName = null)
        {
            string cacheFile = null;
            var lastSlash = source.LastIndexOf('/');
            if (string.IsNullOrEmpty(overrideCacheFileName))
            {
                if (lastSlash != -1 && lastSlash < source.Length - 1)
                    cacheFile = Path.Combine(cachePath, source.Substring(source.LastIndexOf('/') + 1) + ".txt");
            }
            else
                cacheFile = Path.Combine(cachePath, overrideCacheFileName + ".txt");

            if (File.Exists(cacheFile) && useCache)
                source = cacheFile;

            // TLS 1.2 issue
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            var Serializer = new JavaScriptSerializer();
            Serializer.MaxJsonLength = Int32.MaxValue;

            if (source.ToLower().StartsWith("https://"))
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("X-RapidAPI-Key", key);
                    client.Headers.Add("x-apisports-key", key);
                    //client.Headers.Add("x-rapidapi-host", "api-football-v1.p.rapidapi.com");

                    using (var leagueStream = client.OpenRead(source))
                    {
                        using (var streamReader = new StreamReader(leagueStream))
                        {
                            var json = streamReader.ReadToEnd();

                            if (!string.IsNullOrEmpty(cacheFile))
                            {
                                using (var sw = new StreamWriter(cacheFile))
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

    public class Player
    {
        public PlayerInfo player { get; set; }
        public List<Statistic> statistics { get; set; }
    }

    public class PlayerInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public Birth birth { get; set; }
        public string nationality { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public bool injured { get; set; }
        public string photo { get; set; }
    }

    public class Birth
    {
        public string date { get; set; }
        public string place { get; set; }
        public string country { get; set; }
    }

    public class Statistic
    {
        public TeamInfo team { get; set; }
        public LeagueInfo league { get; set; }
        public Games games { get; set; }
        public Substitutes substitutes { get; set; }
        public Shots shots { get; set; }
        public Goals goals { get; set; }
        public Passes passes { get; set; }
        public Tackles tackles { get; set; }
        public Duels duels { get; set; }
        public Dribbles dribbles { get; set; }
        public Fouls fouls { get; set; }
        public Cards cards { get; set; }
        public Penalty penalty { get; set; }
    }

    public class Games
    {
        public int? appearences { get; set; }
        public int? lineups { get; set; }
        public int? minutes { get; set; }
        public int? number { get; set; }
        public string position { get; set; }
        public string rating { get; set; }
        public bool captain { get; set; }
    }

    public class Substitutes
    {
        public int? @in { get; set; }
        public int? @out { get; set; }
        public int? bench { get; set; }
    }

    public class Shots
    {
        public int? total { get; set; }
        public int? on { get; set; }
    }

    public class Goals
    {
        public int? total { get; set; }
        public int? conceded { get; set; }
        public int? assists { get; set; }
        public int? saves { get; set; }
    }

    public class Passes
    {
        public int? total { get; set; }
        public int? key { get; set; }
        public int? accuracy { get; set; }
    }

    public class Tackles
    {
        public int? total { get; set; }
        public int? blocks { get; set; }
        public int? interceptions { get; set; }
    }

    public class Duels
    {
        public int? total { get; set; }
        public int? won { get; set; }
    }

    public class Dribbles
    {
        public int? attempts { get; set; }
        public int? success { get; set; }
        public int? past { get; set; }
    }

    public class Fouls
    {
        public int? drawn { get; set; }
        public int? committed { get; set; }
    }

    public class Cards
    {
        public int? yellow { get; set; }
        public int? yellowred { get; set; }
        public int? red { get; set; }
    }

    public class Penalty
    {
        public int? won { get; set; }
        public int? commited { get; set; }
        public int? scored { get; set; }
        public int? missed { get; set; }
        public int? saved { get; set; }
    }

    public class Team
    {
        public TeamInfo team { get; set; }
        public Venue venue { get; set; }
    }

    public class TeamInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string country { get; set; }
        public int founded { get; set; }
        public bool national { get; set; }
        public string logo { get; set; }
    }

    public class Venue
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int capacity { get; set; }
        public string surface { get; set; }
        public string image { get; set; }
    }
    
    public class League
    {
        public LeagueInfo league { get; set; }
        public Country country { get; set; }
        public List<Season> seasons { get; set; }
    }

    public class LeagueInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string logo { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
        public string flag { get; set; }
    }

    public class Season
    {
        public int year { get; set; }
        public string start { get; set; }   // JavaScriptSerializer maps to string first
        public string end { get; set; }     // safer than DateTime if formats vary
        public bool current { get; set; }
        public Coverage coverage { get; set; }
    }

    public class Coverage
    {
        public Fixtures fixtures { get; set; }
        public bool standings { get; set; }
        public bool players { get; set; }
        public bool top_scorers { get; set; }
        public bool top_assists { get; set; }
        public bool top_cards { get; set; }
        public bool injuries { get; set; }
        public bool predictions { get; set; }
        public bool odds { get; set; }
    }

    public class Fixtures
    {
        public bool events { get; set; }
        public bool lineups { get; set; }
        public bool statistics_fixtures { get; set; }
        public bool statistics_players { get; set; }
    }

    public class Wrapper<T>
    {
        public string get;
        public List<string> parameters;
        public List<string> errors;
        public int results;
        public Paging paging;
        public List<T> response;
    }

    public class Paging
    {
        public int current;
        public int total;
    }
}


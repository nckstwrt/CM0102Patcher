using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace CM0102Patcher
{
    public class TMAPI
    {
        public string root = "http://localhost:8000";
        public string cachePath = @"c:\champman\notes\TMAPI";

        public class Result
        {
            public string TMCompetitionID;
            public string CompetitionName;
            public string TMTeamID;
            public string TMTeamName;
            public string CMStaffID;
            public string TMPlayerID;
            public string TMName;
            public string CMName;
            public string DOB;
            public string Position;
            public string Value;
        }

        public void Test()
        {
            int yearModifier = 1996 - 1973;
            var comps = SearchCompetitions("Championship");

            var comp = "GB1";
            var clubID = "985";

            var clubs = GetCompetitionsClubs(comp);
            var players = GetPlayersFromClub(clubID);

            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2024\Data\index.dat", false, true);

            List<Result> results = new List<Result>();
            foreach (var player in players)
            {
                Result newResult = new Result();
                DateTime dob = DateTime.ParseExact(player.dateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                dob = dob.AddYears(-yearModifier);
                var matchingBirthdate = hl.staff.Where(x => TCMDate.ToDateTime(x.DateOfBirth) == dob).ToList();

                float bestSimilarity = 0;
                TStaff bestMatch = null;
                foreach (var matcher in matchingBirthdate)
                {
                    var similarity = MiscFunctions.GetSimilarity(player.name, hl.staffNames[matcher.ID]);
                    Console.WriteLine("Testing {0} against {1} - similarity: {2}", player.name, hl.staffNames[matcher.ID], similarity);
                    if (similarity > bestSimilarity)
                    {
                        bestMatch = matcher;
                        bestSimilarity = similarity;
                    }
                }
                var foundName = hl.staffNames[bestMatch.ID];

                newResult.TMCompetitionID = comp;
                //newResult.CompetitionName

                Console.WriteLine();
            }


            Console.WriteLine();
        }

        public List<Player> GetPlayersFromClub(string clubId)
        {
            var ret = new List<Player>();
            var players = GetJSON<PlayerWrapper>(string.Format("/clubs/{0}/players", clubId));
            ret.AddRange(players.players);
            return ret;
        }

        public List<Club> GetCompetitionsClubs(string compId, string season = null)
        {
            var ret = new List<Club>();
            var pageOfClubs = GetJSON<ClubWrapper>(string.Format("/competitions/{0}/clubs{1}", compId, season == null ? "" : string.Format("season_id={0}", season)));
            ret.AddRange(pageOfClubs.clubs);
            return ret;
        }

        public List<Competition> SearchCompetitions(string compName)
        {
            var ret = new List<Competition>();
            int page = 1;
            while (true)
            {
                var pageOfComps = GetJSON<CompetitionWrapper>(string.Format("/competitions/search/{0}?page_number={1}", compName, page++));
                ret.AddRange(pageOfComps.results);
                if (pageOfComps.pageNumber == pageOfComps.lastPageNumber)
                    break;
                Thread.Sleep(1000);
                break;
            }
            return ret;
        }

        public T GetJSON<T>(string source, bool useCache = true, string overrideCacheFileName = null)
        {
            string cacheFile = null;
            var lastSlash = source.LastIndexOf('/');
            if (string.IsNullOrEmpty(overrideCacheFileName))
            {
                var fileName = source + ".txt";
                fileName = fileName.Replace("?", "_").Replace("/", "_");
                cacheFile = Path.Combine(cachePath, fileName);
            }
            else
                cacheFile = Path.Combine(cachePath, overrideCacheFileName + ".txt");

            if (File.Exists(cacheFile) && useCache)
                source = cacheFile;

            // TLS 1.2 issue
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            var Serializer = new JavaScriptSerializer();
            Serializer.MaxJsonLength = Int32.MaxValue;

            if (source.ToLower().StartsWith("/"))
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("accept", "application/json, text/plain, */*");
                    using (var leagueStream = client.OpenRead(root + source))
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

                            Thread.Sleep(1000);     // limiter
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

        public class PlayerWrapper
        {
            public string updatedAt { get; set; }
            public string id { get; set; }
            public List<Player> players { get; set; }
        }

        public class Player
        {
            public string id { get; set; }
            public string name { get; set; }
            public string position { get; set; }
            public string dateOfBirth { get; set; }
            public int age { get; set; }
            public List<string> nationality { get; set; }
            public int height { get; set; }
            public string foot { get; set; }
            public string joinedOn { get; set; }
            public string signedFrom { get; set; }
            public string contract { get; set; }
            public long marketValue { get; set; }
            public string status { get; set; }   // optional, not always present
        }

        public class ClubWrapper
        {
            public string updatedAt { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string seasonId { get; set; }
            public List<Club> clubs { get; set; }
        }

        public class Club
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class CompetitionWrapper
        {
            public string updatedAt { get; set; }
            public string query { get; set; }
            public int pageNumber { get; set; }
            public int lastPageNumber { get; set; }
            public List<Competition> results { get; set; }
        }

        public class Competition
        {
            public string id { get; set; }
            public string name { get; set; }
            public string country { get; set; }
            public int clubs { get; set; }
            public int players { get; set; }
            public long totalMarketValue { get; set; }
            public long meanMarketValue { get; set; }
            public string continent { get; set; }
        }
    }
}

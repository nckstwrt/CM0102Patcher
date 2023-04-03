using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public class HistoryLoader
    {
        public List<TIndex> index;
        public TIndex staffDetails;
        public TIndex playerDetails;
        public TIndex nonPlayerDetails;
        public TIndex preferenceDetails;
        public List<TComp> nation_comp;
        public List<TComp> club_comp;
        public List<TStaffComp> staff_comp;
        public List<TClub> club;
        public List<TClub> nat_club;
        public List<TNation> nation;
        public List<TContinent> continent;
        public List<TCompHistory> nation_comp_history;
        public List<TCompHistory> club_comp_history;
        public List<TStaffCompHistory> staff_comp_history;
        public List<TStaff> staff;
        public List<TPreferences> preferences;
        public List<TStaffHistory> staff_history;
        public List<TPlayer> players;
        public List<TNonPlayer> nonPlayers;
        public List<TNames> first_names;
        public List<TNames> second_names;
        public List<TNames> common_names;
        public Dictionary<int, string> staffNames;
        public Dictionary<int, string> staffNamesNoDiacritics;
        public Dictionary<string, int> clubNames;
        public Dictionary<string, List<int>> staffNamesReverse;
        public List<TCities> cities;
        public List<TStadiums> stadiums;

        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");

        public string GetTextFromBytes(byte[] bytes)
        {
            var ret = "";
            if (bytes != null)
            {
                int length = Array.IndexOf(bytes, (byte)0);
                ret = latin1.GetString(bytes, 0, length);
            }
            return ret;
        }

        public void Load(string indexFile, bool quickLoad = true)
        {
            var dir = Path.GetDirectoryName(indexFile);
            
            index = MiscFunctions.ReadFile<TIndex>(indexFile, 8);

            foreach (var idx in index)
            {
                Console.WriteLine("{3}: {0} {1} {2}", idx.Name.ReadString(), idx.Count, idx.FileType, idx.Offset);
            }

            Application.DoEvents();

            nation_comp = MiscFunctions.ReadFile<TComp>(Path.Combine(dir, "nation_comp.dat"));
            club_comp = MiscFunctions.ReadFile<TComp>(Path.Combine(dir, "club_comp.dat"));
            club = MiscFunctions.ReadFile<TClub>(Path.Combine(dir, "club.dat"));
            nat_club = MiscFunctions.ReadFile<TClub>(Path.Combine(dir, "nat_club.dat"));

            Application.DoEvents();

            nation = MiscFunctions.ReadFile<TNation>(Path.Combine(dir, "nation.dat"));
            continent = MiscFunctions.ReadFile<TContinent>(Path.Combine(dir, "continent.dat"));
            nation_comp_history = MiscFunctions.ReadFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"));
            club_comp_history = MiscFunctions.ReadFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"));

            Application.DoEvents();

            staff_comp = MiscFunctions.ReadFile<TStaffComp>(Path.Combine(dir, "staff_comp.dat"));
            staff_comp_history = MiscFunctions.ReadFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"));

            staffDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 6);

            Application.DoEvents();

            if (staffDetails.Version == 1)
            {
                throw new Exception("This is a very old version of the data!\r\n\r\nLoad in the Champ Man Editor and then save it to update it before history editing!\r\n\r\n");
            }

            Application.DoEvents();
            playerDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 10);
            nonPlayerDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 9);

            staff = MiscFunctions.ReadFile<TStaff>(Path.Combine(dir, "staff.dat"), staffDetails.Offset, staffDetails.Count);
            staff_history = MiscFunctions.ReadFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"));

            players = MiscFunctions.ReadFile<TPlayer>(Path.Combine(dir, "staff.dat"), playerDetails.Offset, playerDetails.Count);
            nonPlayers = MiscFunctions.ReadFile<TNonPlayer>(Path.Combine(dir, "staff.dat"), nonPlayerDetails.Offset, nonPlayerDetails.Count); 

            preferenceDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 22);
            if (preferenceDetails != null)
                preferences = MiscFunctions.ReadFile<TPreferences>(Path.Combine(dir, "staff.dat"), preferenceDetails.Offset, preferenceDetails.Count);

            Application.DoEvents();

            first_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "first_names.dat"));
            second_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "second_names.dat"));
            common_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "common_names.dat"));

            staffNames = new Dictionary<int, string>();
            staffNamesNoDiacritics = new Dictionary<int, string>();
            staffNamesReverse = new Dictionary<string, List<int>>();

            if (!quickLoad)
            {
                foreach (var staffMember in staff)
                {
                    string name;
                    if (StaffToName(staffMember, out name))
                    {
                        staffNames[staffMember.ID] = name;
                        staffNamesNoDiacritics[staffMember.ID] = MiscFunctions.RemoveDiacritics(name);

                        if (!staffNamesReverse.ContainsKey(name))
                            staffNamesReverse[name] = new List<int>();
                        staffNamesReverse[name].Add(staffMember.ID);
                    }
                }
            }

            Application.DoEvents();

            clubNames = new Dictionary<string, int>();
            foreach (var clubObj in club)
            {
                clubNames[clubObj.Name.ReadString()] = clubObj.ID;
            }

            cities = MiscFunctions.ReadFile<TCities>(Path.Combine(dir, "city.dat"));
            stadiums = MiscFunctions.ReadFile<TStadiums>(Path.Combine(dir, "stadium.dat"));

            Application.DoEvents();
        }

        public bool StaffToName(TStaff staffMember, out string name)
        {
            if (staffMember.ID >= 0 && staffMember.FirstName >= 0 && staffMember.FirstName < first_names.Count && staffMember.SecondName >= 0 && staffMember.SecondName < second_names.Count)
            {
                if (staffMember.CommonName >= 0 && staffMember.CommonName < common_names.Count && GetTextFromBytes(common_names[staffMember.CommonName].Name).Trim() != "")
                    name = GetTextFromBytes(common_names[staffMember.CommonName].Name);
                else
                    name = GetTextFromBytes(second_names[staffMember.SecondName].Name) + ", " + GetTextFromBytes(first_names[staffMember.FirstName].Name);
                return true;
            }
            else
            {
                name = "";
                return false;
            }
        }
        
        void UpdateIndex<T>(string fileName, List<T> data)
        {
            int idx = index.FindIndex(x => GetTextFromBytes(x.Name) == fileName);
            var indexItem = index[idx];
            indexItem.Count = data.Count;
            index[idx] = indexItem;
        }

        public void UpdateClubsDivision(int clubId, int divisionId)
        {
            for (int i = 0; i < club.Count; i++)
            {
                if (club[i].ID == clubId)
                {
                    TClub temp = club[i];
                    temp.Division = divisionId;
                    club[i] = temp;
                }
            }
        }

        public void UpdateClubsLastDivision(int clubId, int divisionId)
        {
            for (int i = 0; i < club.Count; i++)
            {
                if (club[i].ID == clubId)
                {
                    TClub temp = club[i];
                    temp.LastDivision = divisionId;
                    club[i] = temp;
                }
            }
        }

        public void UpdateClubsLastPosition(int clubId, int lastPosition)
        {
            for (int i = 0; i < club.Count; i++)
            {
                if (club[i].ID == clubId)
                {
                    TClub temp = club[i];
                    temp.LastPosition = (byte)lastPosition;
                    club[i] = temp;
                }
            }
        }

        public void UpdateClubsNation(int clubId, int nationId)
        {
            for (int i = 0; i < club.Count; i++)
            {
                if (club[i].ID == clubId)
                {
                    TClub temp = club[i];
                    temp.Nation = nationId;
                    // This is what the editor suggests when reactivating a club
                    if (temp.Reputation == 0)
                        temp.Reputation = 2 * 500;
                    club[i] = temp;
                }
            }
        }

        public void UpdateNationCompName(int nationCompId, string compName, string compNameShort, int? newNationId = null)
        {
            for (int i = 0; i < nation_comp.Count; i++)
            {
                if (nation_comp[i].ID == nationCompId)
                {
                    var temp = nation_comp[i];
                    temp.Name = MiscFunctions.GetBytesFromText(compName, 51);
                    temp.ShortName = MiscFunctions.GetBytesFromText(compNameShort, 26);
                    if (newNationId != null)
                        temp.ClubCompContinent = newNationId.Value;
                    nation_comp[i] = temp;
                }
            }
        }

        public void UpdateNationCompColor(int nationCompId, int foregroundColorId, int backgroundColorId)
        {
            for (int i = 0; i < nation_comp.Count; i++)
            {
                if (nation_comp[i].ID == nationCompId)
                {
                    var temp = nation_comp[i];
                    temp.ClubCompForegroundColour = foregroundColorId;
                    temp.ClubCompBackgroundColour = backgroundColorId;
                    nation_comp[i] = temp;
                }
            }
        }

        public void ClearNationCompHistory(int nationCompId)
        {
            nation_comp_history.RemoveAll(x => x.Comp == nationCompId);
        }

        public void AddNationCompHistory(string nationCompName, int year, string winner, string runner_up, string host)
        {
            var tNationComp = nation_comp.FirstOrDefault(x => x.Name.ReadString() == nationCompName);
            var tNationWinner = nat_club.FirstOrDefault(x => x.Name.ReadString() == winner);
            var tNationRunnerUp = nat_club.FirstOrDefault(x => x.Name.ReadString() == runner_up);
            var tNationHost = nat_club.FirstOrDefault(x => x.Name.ReadString() == host);

            if (tNationComp.ID != 0 && tNationWinner.ID != 0 && tNationRunnerUp.ID != 0 && tNationHost.ID != 0)
            {
                TCompHistory newHistory = new TCompHistory();
                newHistory.ID = nation_comp_history.Max(x => x.ID) + 1;
                newHistory.Comp = tNationComp.ID;
                newHistory.Year = (short)year;
                newHistory.Winners = tNationWinner.ID;
                newHistory.RunnersUp = tNationRunnerUp.ID;
                newHistory.ThirdPlace = -1;
                newHistory.Host = tNationHost.ID;
                nation_comp_history.Add(newHistory);
            }
        }

        public void Save(string indexFile, bool saveClubData = false, bool saveStaffData = false, bool saveNationData = false)
        {
            var dir = Path.GetDirectoryName(indexFile);

            if (saveNationData)
            {
                UpdateIndex("nation_comp.dat", nation_comp);
                UpdateIndex("nation.dat", nation);
            }

            if (saveClubData)
            {
                UpdateIndex("club_comp.dat", club_comp);
                UpdateIndex("club.dat", club);
            }

            UpdateIndex("nation_comp_history.dat", nation_comp_history);
            UpdateIndex("club_comp_history.dat", club_comp_history);
            UpdateIndex("staff_comp_history.dat", staff_comp_history);
            UpdateIndex("staff_history.dat", staff_history);

            MiscFunctions.SaveFile<TIndex>(indexFile, index, 8);

            if (saveNationData)
            {
                MiscFunctions.SaveFile<TComp>(Path.Combine(dir, "nation_comp.dat"), nation_comp);
                MiscFunctions.SaveFile<TClub>(Path.Combine(dir, "nat_club.dat"), nat_club);
                MiscFunctions.SaveFile<TNation>(Path.Combine(dir, "nation.dat"), nation);
            }

            if (saveClubData)
            {
                MiscFunctions.SaveFile<TComp>(Path.Combine(dir, "club_comp.dat"), club_comp);
                MiscFunctions.SaveFile<TClub>(Path.Combine(dir, "club.dat"), club);
            }

            if (saveStaffData)
            {
                MiscFunctions.SaveFile<TStaff>(Path.Combine(dir, "staff.dat"), staff, staffDetails.Offset);
                MiscFunctions.SaveFile<TPreferences>(Path.Combine(dir, "staff.dat"), preferences, preferenceDetails.Offset);
            }

            MiscFunctions.SaveFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"), nation_comp_history);
            MiscFunctions.SaveFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"), club_comp_history);
            MiscFunctions.SaveFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"), staff_comp_history);
            MiscFunctions.SaveFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"), staff_history);
        }

        public void OutputTeamsAndDivisions()
        {
            Console.WriteLine("**** Teams ****");
            foreach (var c in club)
            {
                var teamName = MiscFunctions.GetTextFromBytes(c.Name);
                Console.WriteLine("\"{0}\"", teamName);
            }
            Console.WriteLine("**** Divisions ****");
            foreach (var c in club_comp)
            {
                var divisionName = MiscFunctions.GetTextFromBytes(c.Name);
                Console.WriteLine("\"{0}\"", divisionName);
            }
        }

        public int FindStaffIndex(string firstName, string secondName)
        {
            int ret = -1;
            var firstNameList = first_names.FindAll(x => MiscFunctions.GetTextFromBytes(x.Name) == firstName);
            var secondNameList = second_names.FindAll(x => MiscFunctions.GetTextFromBytes(x.Name) == secondName);
            // Need to try every combination as there are duplicates names
            foreach (var firstIdx in firstNameList)
            {
                foreach (var secondIdx in secondNameList)
                {
                    ret = staff.FindIndex(x => x.FirstName == firstIdx.ID && x.SecondName == secondIdx.ID);
                    if (ret != -1)
                        break;
                }
                if (ret != -1)
                    break;
            }
            return ret;
        }

        public int FindStaffIndex(string firstName, string secondName, string commonName, string clubName)
        {
            //var clubList = club.FindAll(x => x.Name.ReadString() == clubName || x.ShortName.ReadString() == clubName);
            if (!clubNames.ContainsKey(clubName))
                return -1;

            if (staffNamesReverse.ContainsKey(commonName == "" ? secondName + ", " + firstName : commonName))
            {
                var matchingNameIDs = staffNamesReverse[commonName == "" ? secondName + ", " + firstName : commonName];
                foreach (var matchingID in matchingNameIDs)
                {
                    if (clubNames[clubName] == staff[matchingID].ClubJob)
                        return matchingID;
                }
            }
            //var matchingNameIDs = staffNames.Where(x => commonName == "" ? x.Value == secondName + ", " + firstName : x.Value == commonName).Select(x => x.Key).ToList();

            return -1;
        }

        public void SortClubNames()
        {
            // Sort
            club.Sort((x, y) => GetTextFromBytes(x.ShortName).CompareTo(GetTextFromBytes(y.ShortName)));

            // Update IDs
            int[] clubMap = new int[club.Count];
            for (int i = 0; i < club.Count; i++)
            {
                var tempClub = club[i];
                clubMap[tempClub.ID] = i;
                tempClub.ID = i;
                club[i] = tempClub;
            }

            // Update Rival Clubs
            for (int i = 0; i < club.Count; i++)
            {
                var tempClub = club[i];
                if (tempClub.Rival1 >= 0)
                {
                    if (clubMap.Contains(tempClub.Rival1))
                        tempClub.Rival1 = clubMap[tempClub.Rival1];
                    else
                        tempClub.Rival1 = -1;
                }
                if (tempClub.Rival2 >= 0)
                {
                    if (clubMap.Contains(tempClub.Rival2))
                        tempClub.Rival2 = clubMap[tempClub.Rival2];
                    else
                        tempClub.Rival2 = -1;
                }
                if (tempClub.Rival3 >= 0)
                {
                    if (clubMap.Contains(tempClub.Rival3))
                        tempClub.Rival3 = clubMap[tempClub.Rival3];
                    else
                        tempClub.Rival3 = -1;
                }
                club[i] = tempClub;
            }

            // Update Staff
            for (int i = 0; i < staff.Count; i++)
            {
                var temp = staff[i];
                if (temp.ClubJob >= 0)
                {
                    temp.ClubJob = clubMap[temp.ClubJob];
                }
                staff[i] = temp;
            }

            // Update Staff Preferences
            for (int i = 0; i < preferences.Count; i++)
            {
                var temp = preferences[i];
                if (temp.StaffDislikedClubs1 >= 0)
                    temp.StaffDislikedClubs1 = clubMap[temp.StaffDislikedClubs1];
                if (temp.StaffDislikedClubs2 >= 0)
                    temp.StaffDislikedClubs2 = clubMap[temp.StaffDislikedClubs2];
                if (temp.StaffDislikedClubs3 >= 0)
                    temp.StaffDislikedClubs3 = clubMap[temp.StaffDislikedClubs3];
                if (temp.StaffFavouriteClubs1 >= 0)
                    temp.StaffFavouriteClubs1 = clubMap[temp.StaffFavouriteClubs1];
                if (temp.StaffFavouriteClubs2 >= 0)
                    temp.StaffFavouriteClubs2 = clubMap[temp.StaffFavouriteClubs2];
                if (temp.StaffFavouriteClubs3 >= 0)
                    temp.StaffFavouriteClubs3 = clubMap[temp.StaffFavouriteClubs3];
                preferences[i] = temp;
            }

            // Update Club Comp History
            for (int i = 0; i < club_comp_history.Count; i++)
            {
                var temp = club_comp_history[i];
                if (temp.Winners >= 0)
                    temp.Winners = clubMap[temp.Winners];
                if (temp.RunnersUp >= 0)
                    temp.RunnersUp = clubMap[temp.RunnersUp];
                if (temp.ThirdPlace >= 0)
                    temp.ThirdPlace = clubMap[temp.ThirdPlace];
                club_comp_history[i] = temp;
            }

            // Update Staff History
            for (int i = 0; i < staff_history.Count; i++)
            {
                var temp = staff_history[i];
                if (temp.ClubID >= 0)
                    temp.ClubID = clubMap[temp.ClubID];
                staff_history[i] = temp;
            }
        }
    }
}

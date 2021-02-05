using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace CM0102Patcher
{
    public class HistoryLoader
    {
        public List<TIndex> index;
        public TIndex staffDetails;
        public TIndex playerDetails;
        public TIndex preferenceDetails;
        public List<TComp> nation_comp;
        public List<TComp> club_comp;
        public List<TStaffComp> staff_comp;
        public List<TClub> club;
        public List<TClub> nat_club;
        public List<TNation> nation;
        public List<TCompHistory> nation_comp_history;
        public List<TCompHistory> club_comp_history;
        public List<TStaffCompHistory> staff_comp_history;
        public List<TStaff> staff;
        public List<TPreferences> preferences;
        public List<TStaffHistory> staff_history;
        public List<TPlayer> players;
        public List<TNames> first_names;
        public List<TNames> second_names;
        public List<TNames> common_names;
        public Dictionary<int, string> staffNames;
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

        public void Load(string indexFile)
        {
            var dir = Path.GetDirectoryName(indexFile);
            
            index = MiscFunctions.ReadFile<TIndex>(indexFile, 8);

            foreach (var idx in index)
            {
                Console.WriteLine("{3}: {0} {1} {2}", idx.Name.ReadString(), idx.Count, idx.FileType, idx.Offset);
            }

            nation_comp = MiscFunctions.ReadFile<TComp>(Path.Combine(dir, "nation_comp.dat"));
            club_comp = MiscFunctions.ReadFile<TComp>(Path.Combine(dir, "club_comp.dat"));
            club = MiscFunctions.ReadFile<TClub>(Path.Combine(dir, "club.dat"));
            nat_club = MiscFunctions.ReadFile<TClub>(Path.Combine(dir, "nat_club.dat"));
            nation = MiscFunctions.ReadFile<TNation>(Path.Combine(dir, "nation.dat"));
            nation_comp_history = MiscFunctions.ReadFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"));
            club_comp_history = MiscFunctions.ReadFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"));
            
            staff_comp = MiscFunctions.ReadFile<TStaffComp>(Path.Combine(dir, "staff_comp.dat"));
            staff_comp_history = MiscFunctions.ReadFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"));

            staffDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 6);

            if (staffDetails.Version == 1)
            {
                throw new Exception("This is a very old version of the data!\r\n\r\nLoad in the Champ Man Editor and then save it to update it before history editing!\r\n\r\n");
            }

            playerDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 10);

            staff = MiscFunctions.ReadFile<TStaff>(Path.Combine(dir, "staff.dat"), staffDetails.Offset, staffDetails.Count);
            staff_history = MiscFunctions.ReadFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"));

            players = MiscFunctions.ReadFile<TPlayer>(Path.Combine(dir, "staff.dat"), playerDetails.Offset, playerDetails.Count);

            preferenceDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 22);
            preferences = MiscFunctions.ReadFile<TPreferences>(Path.Combine(dir, "staff.dat"), preferenceDetails.Offset, preferenceDetails.Count);

            first_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "first_names.dat"));
            second_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "second_names.dat"));
            common_names = MiscFunctions.ReadFile<TNames>(Path.Combine(dir, "common_names.dat"));

            staffNames = new Dictionary<int, string>();
            foreach (var staffMember in staff)
            {
                if (staffMember.ID >= 0 && staffMember.FirstName >= 0 && staffMember.FirstName < first_names.Count && staffMember.SecondName >= 0 && staffMember.SecondName < second_names.Count)
                {
                    if (staffMember.CommonName >= 0 && staffMember.CommonName < common_names.Count && GetTextFromBytes(common_names[staffMember.CommonName].Name).Trim() != "")
                        staffNames[staffMember.ID] = GetTextFromBytes(common_names[staffMember.CommonName].Name);
                    else
                        staffNames[staffMember.ID] = GetTextFromBytes(second_names[staffMember.SecondName].Name) + ", " + GetTextFromBytes(first_names[staffMember.FirstName].Name);
                }
            }

            cities = MiscFunctions.ReadFile<TCities>(Path.Combine(dir, "city.dat"));
            stadiums = MiscFunctions.ReadFile<TStadiums>(Path.Combine(dir, "stadium.dat"));
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

        public void Save(string indexFile, bool saveClubData = false, bool saveStaffData = false)
        {
            var dir = Path.GetDirectoryName(indexFile);

            /*
            UpdateIndex("nation_comp.dat", nation_comp);
            UpdateIndex("nation.dat", nation);
            */

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

            /*
            MiscFunctions.SaveFile<TComp>(Path.Combine(dir, "nation_comp.dat"), nation_comp);
            MiscFunctions.SaveFile<TClub>(Path.Combine(dir, "nat_club.dat"), nat_club);
            MiscFunctions.SaveFile<TNation>(Path.Combine(dir, "nation.dat"), nation);
            */
            MiscFunctions.SaveFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"), nation_comp_history);
            MiscFunctions.SaveFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"), club_comp_history);
            MiscFunctions.SaveFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"), staff_comp_history);
            MiscFunctions.SaveFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"), staff_history);
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
                    tempClub.Rival1 = clubMap[tempClub.Rival1];
                if (tempClub.Rival2 >= 0)
                    tempClub.Rival2 = clubMap[tempClub.Rival2]; 
                if (tempClub.Rival3 >= 0)
                    tempClub.Rival3 = clubMap[tempClub.Rival3]; 
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

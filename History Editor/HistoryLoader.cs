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
        public List<TStaffHistory> staff_history;
        public List<TNames> first_names;
        public List<TNames> second_names;
        public List<TNames> common_names;
        public Dictionary<int, string> staffNames;

        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");

        List<T> ReadFile<T>(string fileName, int seekTo = 0, int count = 0)
        {
            List<T> ret = new List<T>();

            using (var fin = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var br = new BinaryReader(fin))
            {
                fin.Seek(seekTo, SeekOrigin.Begin);
                int objSize = Marshal.SizeOf(typeof(T));

                int counter = 0;
                while (true)
                {
                    var bytes = br.ReadBytes(objSize);
                    if (count != 0 && counter == count)
                        break;
                    if (bytes == null || bytes.Length != objSize)
                        break;
                    var ptrObj = Marshal.AllocHGlobal(objSize);
                    Marshal.Copy(bytes, 0, ptrObj, objSize);
                    var obj = (T)Marshal.PtrToStructure(ptrObj, typeof(T));
                    ret.Add(obj);
                    Marshal.FreeHGlobal(ptrObj);
                    counter++;
                }
            }
            
            return ret;
        }

        void SaveFile<T>(string fileName, List<T> data, int seekTo = 0)
        {
            using (var fout = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var bw = new BinaryWriter(fout))
            {
                int objSize = Marshal.SizeOf(typeof(T));
                fout.SetLength(seekTo);
                fout.Seek(seekTo, SeekOrigin.Begin);

                foreach (var obj in data)
                {
                    byte[] arr = new byte[objSize];
                    IntPtr ptr = Marshal.AllocHGlobal(objSize);
                    Marshal.StructureToPtr(obj, ptr, true);
                    Marshal.Copy(ptr, arr, 0, objSize);
                    Marshal.FreeHGlobal(ptr);
                    bw.Write(arr);
                }
            }
        }

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

        public int StringCompare(string a, string b, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                a = a.ToLower();
                b = b.ToLower();
            }
            return Encoding.UTF8.GetString(latin1.GetBytes(a)).CompareTo(Encoding.UTF8.GetString(latin1.GetBytes(b)));
        }

        public void Load(string indexFile)
        {
            var dir = Path.GetDirectoryName(indexFile);
            
            index = ReadFile<TIndex>(indexFile, 8);
            nation_comp = ReadFile<TComp>(Path.Combine(dir, "nation_comp.dat"));
            club_comp = ReadFile<TComp>(Path.Combine(dir, "club_comp.dat"));
            club = ReadFile<TClub>(Path.Combine(dir, "club.dat"));
            nat_club = ReadFile<TClub>(Path.Combine(dir, "nat_club.dat"));
            nation = ReadFile<TNation>(Path.Combine(dir, "nation.dat"));
            nation_comp_history = ReadFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"));
            club_comp_history = ReadFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"));
            
            staff_comp = ReadFile<TStaffComp>(Path.Combine(dir, "staff_comp.dat"));
            staff_comp_history = ReadFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"));

            var staffDetails = index.Find(x => GetTextFromBytes(x.Name) == "staff.dat" && x.FileType == 6);

            if (staffDetails.Version == 1)
            {
                throw new Exception("This is a very old version of the data!\r\n\r\nLoad in the Champ Man Editor and then save it to update it before history editing!\r\n\r\n");
            }

            staff = ReadFile<TStaff>(Path.Combine(dir, "staff.dat"), staffDetails.Offset, staffDetails.Count);
            staff_history = ReadFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"));

            first_names = ReadFile<TNames>(Path.Combine(dir, "first_names.dat"));
            second_names = ReadFile<TNames>(Path.Combine(dir, "second_names.dat"));
            common_names = ReadFile<TNames>(Path.Combine(dir, "common_names.dat"));

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
        }
        
        void UpdateIndex<T>(string fileName, List<T> data)
        {
            int idx = index.FindIndex(x => GetTextFromBytes(x.Name) == fileName);
            var indexItem = index[idx];
            indexItem.Count = data.Count;
            index[idx] = indexItem;
        }

        public void Save(string indexFile)
        {
            var dir = Path.GetDirectoryName(indexFile);

            /*
            UpdateIndex("nation_comp.dat", nation_comp);
            UpdateIndex("club_comp.dat", club_comp);
            UpdateIndex("club.dat", club);
            UpdateIndex("nation.dat", nation);
            */
            UpdateIndex("nation_comp_history.dat", nation_comp_history);
            UpdateIndex("club_comp_history.dat", club_comp_history);
            UpdateIndex("staff_comp_history.dat", staff_comp_history);
            UpdateIndex("staff_history.dat", staff_history);

            SaveFile<TIndex>(indexFile, index, 8);
            /*
            SaveFile<TComp>(Path.Combine(dir, "nation_comp.dat"), nation_comp);
            SaveFile<TComp>(Path.Combine(dir, "club_comp.dat"), club_comp);
            SaveFile<TClub>(Path.Combine(dir, "club.dat"), club);
            SaveFile<TClub>(Path.Combine(dir, "nat_club.dat"), nat_club);
            SaveFile<TNation>(Path.Combine(dir, "nation.dat"), nation);
            */
            SaveFile<TCompHistory>(Path.Combine(dir, "nation_comp_history.dat"), nation_comp_history);
            SaveFile<TCompHistory>(Path.Combine(dir, "club_comp_history.dat"), club_comp_history);
            SaveFile<TStaffCompHistory>(Path.Combine(dir, "staff_comp_history.dat"), staff_comp_history);
            SaveFile<TStaffHistory>(Path.Combine(dir, "staff_history.dat"), staff_history);
        }
    }
}

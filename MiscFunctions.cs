using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public static class MiscFunctions
    {
        static Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");

        public static string GetTextFromBytes(byte[] bytes, bool useExactSize = false)
        {
            var ret = "";
            if (bytes != null)
            {
                int length = useExactSize ? bytes.Length : Array.IndexOf(bytes, (byte)0);
                if (length == -1)
                    length = bytes.Length;
                ret = latin1.GetString(bytes, 0, length);
            }
            return ret;
        }

        public static byte[] GetBytesFromText(string text, int byteArraySize, bool removeDiatrics = false)
        {
            if (removeDiatrics)
                text = RemoveDiacritics(text);

            var bytes = new byte[byteArraySize];
            var len = text.Length;
            if (len > byteArraySize)
                len = byteArraySize;
            Array.Copy(latin1.GetBytes(text), bytes, len);
            return bytes;
        }

        static Dictionary<string, string> foreign_characters = new Dictionary<string, string>
        {
            { "äæǽ", "ae" },
            { "öœ", "oe" },
            { "ü", "ue" },
            { "Ä", "Ae" },
            { "Ü", "Ue" },
            { "Ö", "Oe" },
            { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
            { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
            { "Б", "B" },
            { "б", "b" },
            { "ÇĆĈĊČ", "C" },
            { "çćĉċč", "c" },
            { "Д", "D" },
            { "д", "d" },
            { "ÐĎĐΔ", "Dj" },
            { "ðďđδ", "dj" },
            { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
            { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
            { "Ф", "F" },
            { "ф", "f" },
            { "ĜĞĠĢΓГҐ", "G" },
            { "ĝğġģγгґ", "g" },
            { "ĤĦ", "H" },
            { "ĥħ", "h" },
            { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
            { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
            { "Ĵ", "J" },
            { "ĵ", "j" },
            { "ĶΚК", "K" },
            { "ķκк", "k" },
            { "ĹĻĽĿŁΛЛ", "L" },
            { "ĺļľŀłλл", "l" },
            { "М", "M" },
            { "м", "m" },
            { "ÑŃŅŇΝН", "N" },
            { "ñńņňŉνн", "n" },
            { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
            { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
            { "П", "P" },
            { "п", "p" },
            { "ŔŖŘΡР", "R" },
            { "ŕŗřρр", "r" },
            { "ŚŜŞȘŠΣС", "S" },
            { "śŝşșšſσςс", "s" },
            { "ȚŢŤŦτТ", "T" },
            { "țţťŧт", "t" },
            { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
            { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
            { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
            { "ýÿŷỳỹỷỵй", "y" },
            { "В", "V" },
            { "в", "v" },
            { "Ŵ", "W" },
            { "ŵ", "w" },
            { "ŹŻŽΖЗ", "Z" },
            { "źżžζз", "z" },
            { "ÆǼ", "AE" },
            { "ß", "ss" },
            { "Ĳ", "IJ" },
            { "ĳ", "ij" },
            { "Œ", "OE" },
            { "ƒ", "f" },
            { "ξ", "ks" },
            { "π", "p" },
            { "β", "v" },
            { "μ", "m" },
            { "ψ", "ps" },
            { "Ё", "Yo" },
            { "ё", "yo" },
            { "Є", "Ye" },
            { "є", "ye" },
            { "Ї", "Yi" },
            { "Ж", "Zh" },
            { "ж", "zh" },
            { "Х", "Kh" },
            { "х", "kh" },
            { "Ц", "Ts" },
            { "ц", "ts" },
            { "Ч", "Ch" },
            { "ч", "ch" },
            { "Ш", "Sh" },
            { "ш", "sh" },
            { "Щ", "Shch" },
            { "щ", "shch" },
            { "ЪъЬь", "" },
            { "Ю", "Yu" },
            { "ю", "yu" },
            { "Я", "Ya" },
            { "я", "ya" },
            {"\u05de", "th"}
        };

        public static char RemoveDiacritics(this char c)
        {
            foreach (KeyValuePair<string, string> entry in foreign_characters)
            {
                if (entry.Key.IndexOf(c) != -1)
                {
                    return entry.Value[0];
                }
            }
            return c;
        }

        public static string RemoveDiacritics(this string s)
        {
            string text = "";

            if (string.IsNullOrEmpty(s))
                return text;

            if (s[0] == 0xde)
                s = "Th" + s.Substring(1);

            foreach (char c in s)
            {
                int len = text.Length;

                if (c == (char)5)
                    Console.WriteLine();

                foreach (KeyValuePair<string, string> entry in foreign_characters)
                {
                    if (entry.Key.IndexOf(c) != -1)
                    {
                        text += entry.Value;
                        break;
                    }
                }

                if (len == text.Length)
                {
                    text += c;
                }
            }
            return text;
        }

        public static string ReadString(this byte[] bytes)
        {
            return MiscFunctions.GetTextFromBytes(bytes);
        }

        public static bool StartsWithIgnoreBlank(this string s, string s2)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrEmpty(s2))
                return false;
            return s.ToLower().StartsWith(s2.ToLower());
        }

        public static void RemoveAccentsFromNameFile(string fileName)
        {
            var names = ReadFile<TNames>(fileName);
            for (int i = 0; i < names.Count; i++)
            {
                var name = GetTextFromBytes(names[i].Name);
                name = name.RemoveDiacritics();
                Array.Copy(ASCIIEncoding.ASCII.GetBytes(name), names[i].Name, name.Length);
            }
            SaveFile(fileName, names);
        }

        public static List<T> ReadFile<T>(string fileName, int seekTo = 0, int count = 0)
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
                    if (bytes == null || bytes.Length == 0)
                        break;
                    if (bytes.Length != objSize)
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

        public static void SaveFile<T>(string fileName, List<T> data, int seekTo = 0, bool truncateFirst = false)
        {
            using (var fout = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var bw = new BinaryWriter(fout))
            {
                int objSize = Marshal.SizeOf(typeof(T));
                if (truncateFirst)
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

        public static byte[] StructToBytes<T>(T obj)
        {
            int objSize = Marshal.SizeOf(typeof(T));
            byte[] arr = new byte[objSize];
            IntPtr ptr = Marshal.AllocHGlobal(objSize);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, objSize);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public static T BytesToStruct<T>(byte[] bytes)
        {
            int objSize = Marshal.SizeOf(typeof(T));
            var ptrObj = Marshal.AllocHGlobal(objSize);
            Marshal.Copy(bytes, 0, ptrObj, objSize);
            var obj = (T)Marshal.PtrToStructure(ptrObj, typeof(T));
            Marshal.FreeHGlobal(ptrObj);
            return obj;
        }

        public static ZipStorer OpenZip(string zipFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(zipFileName));
            return ZipStorer.Open(assembly.GetManifestResourceStream(resourceName), FileAccess.Read);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            bool bEqual = false;
            if (array1.Length == array2.Length)
            {
                int i = 0;
                while ((i < array1.Length) && (array1[i] == array2[i]))
                {
                    i += 1;
                }
                if (i == array1.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }

        public static int FuzzyStringCompare(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Verify arguments.
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Initialize arrays.
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Begin looping.
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    // Compute cost.
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
                }
            }
            // Return cost.
            return d[n, m];
        }

        public static float GetSimilarity(string string1, string string2)
        {
            float dis = FuzzyStringCompare(string1, string2);
            float maxLen = string1.Length;
            if (maxLen < string2.Length)
                maxLen = string2.Length;
            if (maxLen == 0.0F)
                return 1.0F;
            else
                return 1.0F - dis / maxLen;
        }

        // Compares the strings based on the shortest string
        public static bool StringCompare(string a, string b, bool preciseCompare = false)
        {
            if (a.Length == 0 || b.Length == 0)
                return false;

            if (preciseCompare)
                return a == b;
            else
            {
                if (a.Length < b.Length)
                    return a == b.Substring(0, a.Length);
                else
                    return b == a.Substring(0, b.Length);
            }
        }

        public static bool StringCompare(string a, string b, string c, bool preciseCompare = false)
        {
            bool ret = (b != null) ? StringCompare(a, b, preciseCompare) : true;
            if (ret)
                return true;
            else
                return StringCompare(a, c, preciseCompare);
        }

        public static void WriteCSVLine(StreamWriter sw, params object[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].GetType() == typeof(byte[]))
                    sw.Write(GetTextFromBytes((byte[])fields[i]));
                else
                    sw.Write(fields[i].ToString());
                if (i != fields.Length - 1)
                    sw.Write(",");
            }
            sw.WriteLine();
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static double Cbrt(double x)
        {
            return Math.Pow(x, (1.0 / 3.0));
        }
    }
}

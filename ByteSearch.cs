using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CM0102Patcher
{
    public class ByteSearch
    {
        public static byte[] LoadFile(string file)
        {
            using (var fs = File.OpenRead(file))
            {
                using (var br = new BinaryReader(fs))
                {
                    return br.ReadBytes((int)br.BaseStream.Length);
                }
            }
        }

        public static int SearchBytes(byte[] toSearch, byte[] searchBytes, int startIndex = 0)
        {
            int ptr = 0;
            for (int i = startIndex; i < toSearch.Length; i++)
            {
                if (toSearch[i] == searchBytes[ptr])
                {
                    ptr++;
                    if (ptr == searchBytes.Length)
                        return (i - searchBytes.Length) + 1;
                }
                else
                {
                    if (ptr > 0)
                    {
                        i -= ptr;
                        ptr = 0;
                    }
                }
            }
            return -1;
        }

        public static List<int> SearchBytesForAll(byte[] toSearch, byte[] searchBytes)
        {
            List<int> offsets = new List<int>();
            int idx = 0;
            while (true)
            {
                var found = SearchBytes(toSearch, searchBytes, idx);
                if (found != -1)
                {
                    offsets.Add(found);
                    idx = found + 1;
                }
                else
                    break;
            }
            return offsets;
        }

        public static byte[] StringToBytePad(string s, int padTo = 0)
        {
            Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
            var stringBytes = latin1.GetBytes(s);
            if (padTo > 0)
            {
                var newString = new byte[padTo];
                stringBytes.CopyTo(newString, 0);
                stringBytes = newString;
            }
            return stringBytes;
        }

        public static void WriteToBlock(byte[] block, int idx, string str, int padTo = 0)
        {
            var strBytes = StringToBytePad(str, padTo);
            strBytes.CopyTo(block, idx);
        }

        public static void WriteToBinaryWriter(BinaryWriter bw, int pos, string str, int padTo = 0)
        {
            var strBytes = StringToBytePad(str, padTo);
            bw.BaseStream.Seek(pos, SeekOrigin.Begin);
            bw.Write(strBytes);
        }

        public static void WriteToFile(string file, int pos, string str, int padTo = 0)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            using (var fs = File.Open(file, FileMode.Open, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    WriteToBinaryWriter(bw, pos, str, padTo);
                }
            }
        }

        public static void TextFileReplace(string file, string toReplace, string replaceWith)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var tr = new StreamReader(fs))
                {
                    using (var tw = new StreamWriter(fs))
                    {
                        var text = tr.ReadToEnd();
                        text = text.Replace(toReplace, replaceWith);
                        fs.SetLength(0);
                        tw.Write(text);
                    }
                }
            }
        }
    }
}

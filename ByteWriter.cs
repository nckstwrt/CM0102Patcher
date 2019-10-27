using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CM0102Patcher
{
    public class ByteWriter
    {
        public static byte[] LoadFile(string file)
        {
            using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var br = new BinaryReader(fs))
                {
                    return br.ReadBytes((int)br.BaseStream.Length);
                }
            }
        }

        public static int SearchBytes(byte[] toSearch, string searchText, int startIndex = 0, bool ignoreCase = false)
        {
            Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
            var stringBytes = latin1.GetBytes(searchText);
            return SearchBytes(toSearch, stringBytes, startIndex, ignoreCase);
        }

        public static int SearchBytes(byte[] toSearch, byte[] searchBytes, int startIndex = 0, bool ignoreCase = false)
        {
            int ptr = 0;
            for (int i = startIndex; i < toSearch.Length; i++)
            {
                bool equal = toSearch[i] == searchBytes[ptr];
                if (ignoreCase)
                {
                    if (toSearch[i] >= 32 && toSearch[i] < 127 &&
                        searchBytes[ptr] >= 32 && searchBytes[ptr] < 127)
                    {
                        equal = (char.ToUpper((char)toSearch[i]).Equals(char.ToUpper((char)searchBytes[ptr])));
                    }
                }
                if (equal)
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

        public static List<int> SearchBytesForAll(byte[] toSearch, byte[] searchBytes, bool ignoreCase = false)
        {
            List<int> offsets = new List<int>();
            int idx = 0;
            while (true)
            {
                var found = SearchBytes(toSearch, searchBytes, idx, ignoreCase);
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
            if (padTo > stringBytes.Length)
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

        public static int WriteToBinaryWriter(BinaryWriter bw, int pos, string str, int padTo = 0)
        {
            var strBytes = StringToBytePad(str, padTo);
            bw.BaseStream.Seek(pos, SeekOrigin.Begin);
            bw.Write(strBytes);
            return strBytes.Length;
        }

        public static int WriteToFile(string file, int pos, string str, int padTo = 0)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    return WriteToBinaryWriter(bw, pos, str, padTo);
                }
            }
        }

        public static int WriteToFile(string file, int pos, byte[] bytes)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    fs.Seek(pos, SeekOrigin.Begin);
                    bw.Write(bytes);
                    return bytes.Length;
                }
            }
        }

        public static int BinFileReplace(string file, string toReplace, string replaceWith, int startPosition = 0, int timesToReplace = 0, bool ignoreCase = false)
        {
            int lastPosChanged = -1;
            var bytes = ByteWriter.LoadFile(file);
            Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
            var stringBytes = latin1.GetBytes(toReplace);
            var bytePositions = SearchBytesForAll(bytes, stringBytes, ignoreCase);
            int numberChanged = 0;
            foreach (var pos in bytePositions)
            {
                if (pos >= startPosition)
                {
                    WriteToFile(file, pos, replaceWith, toReplace.Length);
                    lastPosChanged = pos;
                    numberChanged++;
                    if (timesToReplace != 0 && numberChanged >= timesToReplace)
                        break;
                }
            }
            return lastPosChanged;
        }

        public static void TextFileReplace(string file, string toReplace, string replaceWith)
        {
            Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
            File.SetAttributes(file, FileAttributes.Normal);
            using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var tr = new StreamReader(fs, latin1))
                {
                    using (var tw = new StreamWriter(fs, latin1))
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

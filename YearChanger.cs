using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class YearChanger
    {
        List<int> startYear = new List<int> { 0x13386, 0x140e5, 0x224f0, 0x44270, 0x44297, 0x55830, 0x5583d, 0x5f4ee, 0x5f97c, 0x5f981, 0x16fc63, /*0x18b387,*/ 0x1aee53, 0x1bab86, 0x1bac32, 0x1BACE7, 0x1bb6ab, 0x1BC2C1, 0x1BC420, 0x1bc8b2, 0x1BF0AE, 0x1C070E, 0x1c3068, 0x1db242, 0x2673c3, 0x267495, 0x267582, 0x26766d, 0x26775a, 0x267829, 0x2678f8, 0x2679c6, 0x267aa1, 0x267b81, 0x267c6d, 0x267d5a, 0x267e55, 0x267f50, 0x268043, 0x268149, 0x268236, 0x268324, 0x268411, 0x2684ff, 0x2685ed, 0x2686bc, 0x2687ac, 0x268899, 0x268987, 0x268a77, 0x268b65, 0x268c54, 0x268d40, 0x268e2f, 0x268f1d, 0x26900b, 0x2690da, /*0x37d858,*/ 0x3d2410, 0x41b93d, 0x430591, 0x430598, 0x4305dc, 0x430a64, 0x430f8e, 0x430fb4, 0x43129a, 0x4312b4, 0x431608, 0x431622, 0x4318ad, 0x4318c6, 0x431b54, 0x431b6d, 0x431e66, 0x431e80, 0x4320b3, 0x4320cd, 0x432324, 0x432577, 0x43290d, 0x433055, 0x43339d, 0x4336eb, 0x433c84, 0x433f8e, 0x434382, 0x43475d, 0x434aad, 0x434dfd, 0x435297, 0x435c39, 0x435fca, 0x4362EF, 0x43668e, 0x436a55, 0x436d68, 0x4371a5, 0x4371d5, 0x4374e9, 0x43805d, 0x438357, 0x43869f, 0x456ce0, 0x4fddd2, 0x5041f3, 0x5059B9, 0x5291B4 };
        List<int> startYearMinus19 = new List<int> { 0x12638d, 0x1263Ba };
        List<int> startYearMinus3 = new List<int> { 0x3e6819, 0x461E36 };
        List<int> startYearMinus2 = new List<int> { 0x135d83, 0x135df2 };
        List<int> startYearMinus1 = new List<int> { 0x55fd1, 0xdc02c, 0x12d2e2, 0x2B4FF4, 0x3e68fe, 0x3e691f, 0x45e98f };
        List<int> startYearPlus1 = new List<int> { 0xdc135 };
        List<int> startYearPlus2 = new List<int> { 0x12d321, 0x29e84e /*, 0x45b841, 0x45b898,  0x45c40c */ };
        List<int> startYearPlus3 = new List<int> { 0xdc113, 0x19ba24 };
        List<int> startYearPlus9 = new List<int> { 0x135d89, 0x135df8, 0x3A3D64, 0x3A3FD1, 0x3A4224, 0x3A4844, 0x3A4CB4, 0x3A4F68, 0x3A4FA1 };

        byte[] YearToBytes(int year)
        {
            return new byte[] { (byte)(year & 0xff), (byte)(year >> 8) };
        }

        public int GetCurrentExeYear(string fileName)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Seek(startYear[0], SeekOrigin.Begin);
                using (var br = new BinaryReader(file))
                {
                    return br.ReadInt16();
                }
            }
        }

        public void ApplyYearChangeToExe(string fileName, int year)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    foreach (var offset in startYear)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year));
                    }
                    foreach (var offset in startYearMinus19)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 19));
                    }
                    foreach (var offset in startYearMinus3)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 3));
                    }
                    foreach (var offset in startYearMinus2)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 2));
                    }
                    foreach (var offset in startYearMinus1)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 1));
                    }
                    foreach (var offset in startYearPlus1)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year + 1));
                    }
                    foreach (var offset in startYearPlus2)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year + 2));
                    }
                    foreach (var offset in startYearPlus3)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year + 3));
                    }
                    foreach (var offset in startYearPlus9)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year + 9));
                    }

                    // Special
                    bw.Seek(0x18B387, SeekOrigin.Begin);
                    int mod4year = ((year + 1) - ((year - 1) % 4));
                    bw.Write(YearToBytes(mod4year));
                }
            }
        }

        int GetStaffOffset(string indexFileName, out int staffCount, out int staffVersion)
        {
            int ret = -1;
            staffCount = 0;
            staffVersion = 0;
            using (var indexFile = File.Open(indexFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Skip header
                indexFile.Seek(8, SeekOrigin.Begin);
                using (var br = new BinaryReader(indexFile))
                {
                    while (true)
                    {
                        var fileNameBytes = br.ReadBytes(51);
                        if (fileNameBytes == null || fileNameBytes.Length == 0)
                            break;
                        var fileName = Encoding.ASCII.GetString(fileNameBytes.ToList().GetRange(0, fileNameBytes.ToList().IndexOf(0)).ToArray());
                        var fileType = br.ReadInt32();
                        var count = br.ReadInt32();
                        var offset = br.ReadInt32();
                        var version = br.ReadInt32();

                        if (fileName == "staff.dat" && fileType == 6)
                        {
                            ret = offset;
                            staffCount = count;
                            staffVersion = version;
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        bool IsLeapYear(int year)
        {
            return ((year % 4) == 0) && ((year % 100) != 0 || (year % 400) == 0);
        }

        void ChangeDate(byte[] staffBytes, long pos, int yearIncrement, string yearType)
        {
            int year = ((staffBytes[pos + 0]) | (staffBytes[pos + 1] << 8));

            if (year > 1900 && year < 2100)
            {
                int new_year = year + yearIncrement;
                YearToBytes(new_year).CopyTo(staffBytes, pos);
                if (yearType != "Year")
                {
                    int isLeapYear = IsLeapYear(new_year) ? 1 : 0;
                    byte[] intBytes = BitConverter.GetBytes(isLeapYear);
                    intBytes.CopyTo(staffBytes, pos + 2);
                }
            }
        }

        public void UpdateStaff(string indexFileName, string staffFileName, int yearIncrement)
        {
            int staffCount = 0;
            int staffVersion = 0;
            int staffOffset = GetStaffOffset(indexFileName, out staffCount, out staffVersion);
            using (var staffFile = File.Open(staffFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                staffFile.Seek(staffOffset, SeekOrigin.Begin);
                var blockLength = (staffVersion == 1 ? 157 : 110);
                var staffFileLength = staffCount * blockLength;
                var staffBytes = new byte[staffFileLength];
                staffFile.Read(staffBytes, 0, staffFileLength);

                int pos = 0;
                for (int i = 0; i < staffCount; i++)
                {
                    // DOB and Year of Birth
                    ChangeDate(staffBytes, pos + 4 + 4 + 4 + 4 + 2, yearIncrement, "DOB");
                    ChangeDate(staffBytes, pos + 4 + 4 + 4 + 4 + 2 + 6, yearIncrement, "Year");

                    // Club Contract Dates
                    ChangeDate(staffBytes, pos + 62 + 2, yearIncrement, "ClubStart");
                    ChangeDate(staffBytes, pos + 70 + 2, yearIncrement, "ClubEnd");

                    // Nation Contract Dates
                    ChangeDate(staffBytes, pos + 41 + 2, yearIncrement, "NationStart");
                    ChangeDate(staffBytes, pos + 49 + 2, yearIncrement, "NationEnd");

                    pos += blockLength;
                }

                staffFile.Seek(staffOffset, SeekOrigin.Begin);
                staffFile.Write(staffBytes, 0, staffFileLength);
            }
        }

        List<string> ConfigLineSplitter(string line)
        {
            List<string> lineList = new List<string>();
            string newStr = "";
            bool inQuotes = false;
            foreach (char c in line)
            {
                if (c == '"')
                    inQuotes = !inQuotes;
                if (c == ' ' && !inQuotes)
                {
                    lineList.Add(newStr);
                    newStr = "";
                    continue;
                }
                newStr += c;
            }
            if (newStr != "")
                lineList.Add(newStr);
            return lineList;
        }

        public void UpdatePlayerConfig(string playerConfigFile, int yearIncrement)
        {
            using (var staffFile = File.Open(playerConfigFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var lineList = new List<string>();
                using (var sr = new StreamReader(staffFile))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        lineList.Add(line);
                    }

                    // Truncate
                    staffFile.SetLength(0);

                    using (var sw = new StreamWriter(staffFile))
                    {
                        // Write out all lines with the new date
                        foreach (var line in lineList)
                        {
                            // Future transfer
                            if (line.Length > "\"FUTURE_TRANSFER\"".Length && line.Substring(0, "\"FUTURE_TRANSFER\"".Length).ToUpper() == "\"FUTURE_TRANSFER\"")
                            {
                                var parts = ConfigLineSplitter(line);
                                if (parts.Count() == 12)
                                {
                                    int year;
                                    if (int.TryParse(parts[8], out year))
                                    {
                                        var new_year = year + yearIncrement;
                                        parts[8] = new_year.ToString();
                                    }
                                    sw.WriteLine(string.Join(" ", parts.ToArray()));
                                }
                            }
                            else
                            if (line.Length > "\"LOAN\"".Length && line.Substring(0, "\"LOAN\"".Length).ToUpper() == "\"LOAN\"")
                            {
                                var parts = ConfigLineSplitter(line);
                                if (parts.Count() == 12)
                                {
                                    int year1;
                                    if (int.TryParse(parts[8], out year1))
                                    { 
                                        var new_year1 = year1 + yearIncrement;
                                        parts[8] = new_year1.ToString();
                                    }

                                    int year2;
                                    if (int.TryParse(parts[11], out year2))
                                    {
                                        var new_year2 = year2 + yearIncrement;
                                        parts[11] = new_year2.ToString();
                                    }

                                    sw.WriteLine(string.Join(" ", parts.ToArray()));
                                }
                            }
                            else
                                sw.WriteLine(line);
                        }
                    }
                }
            }
        }

        public void UpdateHistoryFile(string historyFile, int blockSize, int yearIncrement, params int[] yearOffsets)
        {
            using (var file = File.Open(historyFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var fileLength = (int)file.Length;
                var bytes = new byte[fileLength];
                file.Read(bytes, 0, fileLength);

                for (int i = 0; i != fileLength; i += blockSize)
                {
                    foreach (var yearOffset in yearOffsets)
                    {
                        short year = BitConverter.ToInt16(bytes, i + yearOffset);

                        if (year > 1900 && year < 2100)
                        {
                            year += (short)yearIncrement;
                            BitConverter.GetBytes(year).CopyTo(bytes, i + yearOffset);
                        }
                    }
                }

                file.Seek(0, SeekOrigin.Begin);
                file.Write(bytes, 0, fileLength);
            }
        }
    }
}

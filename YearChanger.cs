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

        public int GetCurrentExeYear(string fileName, int offset = -1)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Seek(offset == -1 ? startYear[0] : offset, SeekOrigin.Begin);
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

                    // Special 2 (the calc for season selection can cause England 18/09 without this)
                    bw.Seek(0x41e9ca, SeekOrigin.Begin);
                    bw.Write((byte)0x64);

                    // Special 3 - Need to fix Euro for 2019
                    if ((year % 4) == 3)
                    {
                        bw.Seek(0x1f9c0a, SeekOrigin.Begin);
                        bw.Write((byte)0xdc);
                    }
                }
            }
        }

        public void ApplyYearChangeTo0001Exe(string fileName, int year)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var br = new BinaryReader(file))
                {
                    using (var bw = new BinaryWriter(file))
                    {
                        List<int> startYear_0001 = new List<int> { 0x0001009F, 0x00010DB5, 0x0001F580, 0x001A4D82, 0x001B39B3, 0x0031B3E1, 0x00364760, 0x00377ADE, 0x0037D5B7, 0x003ABAED, 0x003BFEA8, 0x003BFEEC, 0x003C036E, 0x003C0888, 0x003C0B84, 0x003C0EF2, 0x003C1197, 0x003C143E, 0x003C154C, 0x003C178D, 0x003C19EE, 0x003C1C31, 0x003C1FC7, 0x003C24CC, 0x003C2809, 0x003C2B45, 0x003C312C, 0x003C3428, 0x003C381C, 0x003C3BF7, 0x003C3F37, 0x003C4277, 0x003C465B, 0x003C4C4C, 0x003C4FF3, 0x003C53DA, 0x003C56F9, 0x003C5A98, 0x003C5E5F, 0x003C6172, 0x003C65AF, 0x003C6A31, 0x003C6DAF, 0x003C70F7, 0x003C73E1, 0x003C7729, 0x003CE535, 0x003E4BF0, 0x003E9350 };
                        List<int> startYearMinus1_0001 = new List<int> { /* All German Regional 0x001A6AA0, 0x001A739F, 0x001A759E, *//* new*/0x101865, 0x377ade, 0x377aff, 0xd0e83 };
                        List<int> startYearMinus1_Scotland_0001 = new List<int> { 0x0037EC26, 0x0037F166, 0x0037FFA1, 0x003800B9, 0x00381550, 0x00381D10, 0x00381FA5, 0x00383881, 0x003839CE, 0x00383A9E, 0x00383ADD, 0x00384224, 0x00384FDB, 0x00385F8F };
                        List<int> startYearMinus2_0001 = new List<int> { 0x109e69, 0x109ed8 };
                        List<int> startYearMinus3_0001 = new List<int> { 0x3779f9, 0x3eeb48 };
                        List<int> startYearMinus19_0001 = new List<int> { 0x0fab71, 0x0fab9e };
                        List<int> startYearPlus1_0001 = new List<int> { 0x0d0f86 };
                        List<int> startYearPlus2_0001 = new List<int> { 0x1018a4 };
                        List<int> startYearPlus3_0001 = new List<int> { 0x169b54 };
                        List<int> startYearPlus9_0001 = new List<int> { 0x109e6f, 0x109ede, 0x30042b /*?*/ };

                        foreach (var offset in startYear_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year));
                        }
                        foreach (var offset in startYearMinus1_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year - 1));    // -0 or -1 ?
                        }
                        foreach (var offset in startYearMinus1_Scotland_0001)  // SPL changes team size, so put this the year before
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year - 1));  
                        }
                        foreach (var offset in startYearMinus2_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year - 2));
                        }
                        foreach (var offset in startYearMinus3_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year - 3));   
                        }
                        foreach (var offset in startYearMinus19_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year - 19));
                        }
                        foreach (var offset in startYearPlus1_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year + 1));
                        }
                        foreach (var offset in startYearPlus2_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year + 2));
                        }
                        foreach (var offset in startYearPlus3_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year + 3));    
                        }
                        foreach (var offset in startYearPlus9_0001)
                        {
                            bw.Seek(offset, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year + 9));
                        }

                        // Special 2 (the calc for season selection can cause England 18/09 without this)
                        bw.Seek(0x3AECE1, SeekOrigin.Begin);
                        bw.Write((byte)0x64);

                        // Default Birth Year for Players with no DoB
                        bw.Seek(0x10A910, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 16));

                        // Turkey Fix (doesn't work :( )
                        /*
                        file.Seek(0x21bea1, SeekOrigin.Begin);
                        var bytes = br.ReadBytes(0x21daf5 - 0x21bea1);
                        var turkeyOffsets = ByteWriter.SearchBytesForAll(bytes, new byte[] { 0x68, 0xD0, 0x07, 0x00, 0x00 });
                        foreach (var offset in turkeyOffsets)
                        {
                            bw.Seek(0x21bea1 + offset + 1, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year));
                        }
                        */

                        // Turn off Turkey Division 2 (T2) - as it will crash if selected
                        bw.Seek(0x3c7024, SeekOrigin.Begin);
                        bw.Write((byte)0xEB);

                        // Turn off T2's Awards
                        bw.Seek(0x486e32, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
                        

                        // Special 3 Parts
                        bw.Seek(0x1113b7, SeekOrigin.Begin);
                        bw.Write(YearToBytes(year - 2));

                        // Special 4 - Euro + World Cup Fixes for 1993
                        if (year == 1993)
                        {
                            // Euro
                            bw.Seek(0x1c3a3a, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year-1));  // Make it 1992 instead of 2000
                            bw.Seek(0x1c3ae2, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0x8B, 0x0D, 0x84, 0x64, 0x94, 0x00 }); // Make England the hosts
                            // Everything is shunted along, so after the Euro 96 in England we'll get Switzerland or Wales/Scotland or Sweden in 2000.
                            // Making that dual hosted means finding the host (e.g. Switzerland), Replacing it with Belgium and then 
                            // Making the next 4 bytes be the dual host, using 0x53 - which is the ID for Holland
                            
                            bw.Seek(0x1c3c03, SeekOrigin.Begin);    // Switzerland
                            bw.Write(new byte[] { 0xA1, 0xE4, 0x63, 0x94, 0x00, 0x89, 0x82, 0xC0, 0x01, 0x00, 0x00, 0xC7, 0x81, 0xC4, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });
                            // ^ Suspicious of this - probably not necessary
                            /*
                            bw.Seek(0x1c3b48, SeekOrigin.Begin); // Sweden
                            bw.Write(new byte[] { 0xA1, 0xE4, 0x63, 0x94, 0x00, 0x89, 0x82, 0x6A, 0x01, 0x00, 0x00, 0xC7, 0x81, 0x6E, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });
                            */

                            // New Approach for Switzerland + Sweden:
                            // Doesn't work either - or maybe it does if you make BL = FD rather than FE
                            bw.Seek(0x1c3b34, SeekOrigin.Begin); 
                            bw.Write(new byte[] { 0xE4, 0x63 });
                            bw.Seek(0x1c3b3e, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xC7, 0x82, 0x66, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });
                            bw.Seek(0x1c3b49, SeekOrigin.Begin); 
                            bw.Write(new byte[] { 0xE4, 0x63 });
                            bw.Seek(0x1c3b53, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xC7, 0x82, 0x6e, 0x01, 0x00, 0x00, 0x53, 0x00, 0x00, 0x00 });
                            // Change BL to FD, (use EDX rather than EAX)
                            bw.Seek(0x1c3b5d, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xC6, 0x82, 0x72, 0x01, 0x00, 0x00, 0xFD, 0x90 });
                            bw.Seek(0x1c3b6c, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0x8a });

                            bw.Seek(0x1c3b19, SeekOrigin.Begin); // Wales/Scotland (easier as already dual nation hosted)
                            bw.Write(new byte[] { 0xE4, 0x63 });
                            bw.Seek(0x1c3b27, SeekOrigin.Begin); 
                            bw.Write(new byte[] { 0xD8, 0x64 });

                            // Make 2004 Portugal
                            bw.Seek(0x1c3b67, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xD4, 0x65 });
                            bw.Seek(0x1c3b7d, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xD4, 0x65 });
                            bw.Seek(0x1c3b93, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xD4, 0x65 });
                            
                            // World Cup
                            bw.Seek(0x1c37ec, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year + 1));  // Make it 1994 instead of 1998
                            bw.Seek(0x1c37d1, SeekOrigin.Begin);
                            bw.Write(YearToBytes(year));  // Make it 1993 instead of 1997
                            bw.Seek(0x1c3819, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0x8B, 0x0D, 0x98, 0x66, 0x94, 0x00 }); // Make USA the hosts (replacing France)
                            bw.Seek(0x4b0d93, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xA1, 0xF4, 0x63, 0x94, 0x00 }); // Make Bolivia replace USA in qualifiers so we don't get 2 USAs
                            // This, like the Euros shunts everything along, so the 1998 World Cup would get hosted by S.Korea and Japan. Replace with just France.
                            bw.Seek(0x1c3851, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0x8B, 0x15, 0xA0, 0x64, 0x94, 0x00, 0x89, 0x51, 0x28, 0x8B, 0x06, 0xB9, 0xFF, 0xFF, 0xFF, 0xFF, 0x90 });
                            // Make Germany = S.Korea + Japan
                            bw.Seek(0x1c388d, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0x24, 0x66 });
                            bw.Seek(0x1c3894, SeekOrigin.Begin);
                            bw.Write(new byte[] { 0xC7, 0x40, 0x4E, 0x61, 0x00, 0x00, 0x00 });

                        }

                        // Special 5 - There seems to be a check to stop any more than 20000 players being loaded maybe? Fk that
                        bw.Seek(0x0fc2e7, SeekOrigin.Begin);
                        bw.Write((uint)80000);

                        // This error removing is poor - but at least makes for a mainly smooth game
                        
                        // Turn off the World Cup 1081 error
                        bw.Seek(0x4b1061, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                        
                        // Turn off the World Cup 1358 error
                        bw.Seek(0x4b169c, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                        
                        // Turn off the German Regional 332 error (this occurs because before 2000, Germany had 18 rather than 19 teams)
                        bw.Seek(0x1a7561, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                        
                        // Turn off Database 19941 error
                        bw.Seek(0x119199, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                        // Turn off transfer_manager..cpp 10616
                        // This is a bad one - you need to do more than turn it off
                        // This occurs when the staff member being transferred does not have a Player pointer at +61
                        // So you need to eject! :)
                        //bw.Seek(0x450cbb, SeekOrigin.Begin);
                        //bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                        bw.Seek(0x450c6a, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0xEB, 0xA4, 0x90, 0x90, });

                        // Turn off contract_manager..cpp 5095
                        bw.Seek(0x0c4f57, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                        // Turn off Cup.cpp 1187
                        bw.Seek(0x0ef1b6, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                        // Turn off hall_of_fame.cpp 678
                        bw.Seek(0x1ba2c4, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });

                        // EXTRAS

                        // Turn off using Registry
                        bw.Seek(0x561e10, SeekOrigin.Begin);
                        bw.Write((byte)0x41);

                        // Always report good memory (stops need for XP compatibility)
                        bw.Seek(0x3392c0, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0xB8, 0xFE, 0xFF, 0xFF, 0x7F, 0xC3, 0x90, 0x90 });
                    }
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

        DateTime FromCMDate(short day, short year, int leapYear)
        {
            //31 + 28 == Feb 28th
            return new DateTime(year, 1, 1).AddDays(day - 1);
        }

        void ToCMDate(DateTime dt, out short day, out short year, out int leapYear)
        {
            day = (short)(dt.DayOfYear);
            year = (short)dt.Year;
            leapYear = DateTime.IsLeapYear(year) ? 1 : 0;
        }

        void ChangeDate(byte[] staffBytes, long pos, int yearIncrement, string yearType)
        {
            short year = (short)((staffBytes[pos + 0]) | (staffBytes[pos + 1] << 8));

            if (year > 1900 && year < 2100)
            {
                int new_year = year + yearIncrement;
                YearToBytes(new_year).CopyTo(staffBytes, pos);

                if (yearType != "Year")
                {
                    // Giggs = 29 November 1973 (1956)
                    short oldDay = BitConverter.ToInt16(staffBytes, (int)pos - 2);
                    int oldIsLeapYear = BitConverter.ToInt32(staffBytes, (int)pos + 2);
                    var oldDate = FromCMDate(oldDay, year, oldIsLeapYear);

                   /* if (year == 1956 && oldDate.Month == 10 && oldDate.Day == 29)
                    {
                        Console.WriteLine();
                    }*/

                    var newDate = oldDate.AddYears(yearIncrement);
                    
                    int isLeapYear = DateTime.IsLeapYear(new_year) ? 1 : 0;
                    byte[] intBytes = BitConverter.GetBytes(isLeapYear);
                    intBytes.CopyTo(staffBytes, pos + 2);

                    short shtDay, shtYear;
                    int shtLeapYear;
                    ToCMDate(newDate, out shtDay, out shtYear, out shtLeapYear);
                    var shtDayBytes = BitConverter.GetBytes(shtDay);
                    shtDayBytes.CopyTo(staffBytes, pos - 2); 
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
            var latin1 = Encoding.GetEncoding("ISO-8859-1");
            using (var staffFile = File.Open(playerConfigFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var lineList = new List<string>();
                using (var sr = new StreamReader(staffFile, latin1))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        lineList.Add(line);
                    }

                    // Truncate
                    staffFile.SetLength(0);

                    using (var sw = new StreamWriter(staffFile, latin1))
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
                            {
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
        }

        public void UpdateHistoryFile(string historyFile, int blockSize, int yearIncrement, params int[] yearOffsets)
        {
            using (var file = File.Open(historyFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var fileLength = (int)file.Length;
                var bytes = new byte[fileLength];
                file.Read(bytes, 0, fileLength);

                for (int i = 0; i < fileLength; i += blockSize)
                {
                    foreach (var yearOffset in yearOffsets)
                    {
                        // March 2019 update seemed truncated, so don't except if it is
                        if ((i + yearOffset + 1) >= bytes.Length)
                            continue;

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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NoCDPatch
    {
        public static byte[] pattern = new byte[] { 0x75, 0x00, 0x6a, 0x65, 0x6a, 0x78, 0x6a, 0x65, 0x6a, 0x2e, 0x6a, 0x00, 0x6a, 0x30 };

        public static int FindPattern(string fileName, byte[] pattern, Action<Stream, BinaryReader, BinaryWriter, int> func, bool zeroIsWildCard = true)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                return FindPattern(file, pattern, func, zeroIsWildCard);
            }
        }

        public static int FindPattern(Stream stream, byte[] pattern, Action<Stream, BinaryReader, BinaryWriter, int> func, bool zeroIsWildCard = true)
        {
            int patched = 0;
            using (var bw = new BinaryWriter(stream))
            {
                using (var br = new BinaryReader(stream))
                {
                    int match = 0;
                    int fileLength = (int)stream.Length;
                    var fileContent = br.ReadBytes(fileLength);
                    for (int i = 0; i < fileLength; i++)
                    {
                        if (zeroIsWildCard && pattern[match] == 0)
                        {
                            match++;
                            continue;
                        }

                        if (fileContent[i] == pattern[match])
                        {
                            match += 1;
                            if (match == pattern.Length)
                            {
                                int savedPos = (i - pattern.Length) + 1;
                                func(stream, br, bw, savedPos);
                                patched++;
                                match = 0;
                            }
                        }
                        else
                            match = 0;
                    }
                }
            }
            return patched;
        }

        public static void PatchMemoryStream(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            FindPattern(stream, pattern, (file, br, bw, offset) =>
            {
                file.Seek(offset, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90 });
                file.Seek(17, SeekOrigin.Current);
                bw.Write(new byte[] { 0x00, 0x6a, 0x2a });
            });
        }

        public static int PatchEXEFile(string exeFile)
        {
            int patched = FindPattern(exeFile, pattern, (file, br, bw, offset) =>
            {
                file.Seek(offset, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x90, 0x90 });
                file.Seek(17, SeekOrigin.Current);
                bw.Write(new byte[] { 0x00, 0x6a, 0x2a });
            });

            // If patched more than 10 times, definitely a cm exe
            if (patched > 10)
            {
                // Check if 0001 (could be the origial with more checks
                bool is0001 = false;
                FindPattern(exeFile, Encoding.ASCII.GetBytes("CM00/01 CD"), (file, br, bw, offset) => { is0001 = true; });
                if (is0001)
                {
                    string[] files = new string[] { @"d:\KPWN.AFP", @"d:\SPBB.AFP", @"d:\PWQE.AFP", @"d:\EVWF.AFP" };
                    byte[] bytePattern = new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0xE8 };

                    Action<Stream, BinaryReader, BinaryWriter, int> patchFunc = (file, br, bw, offset) =>
                    {
                        file.Seek(offset + 5, SeekOrigin.Begin);
                        for (int i = 0; i < 0x50; i++)
                        {
                            byte b = br.ReadByte();
                            if (b == 0x74)
                            {
                                file.Seek(-1, SeekOrigin.Current);
                                bw.Write(new byte[] { 0x90, 0x90 });
                            }

                            if (b == 0x75)
                            {
                                file.Seek(-1, SeekOrigin.Current);
                                bw.Write(new byte[] { 0xeb });
                                break;
                            }
                        }
                    };

                    Action<Stream, BinaryReader, BinaryWriter, int> patternSearchFunc = (file, br, bw, offset) =>
                    {
                        BitConverter.GetBytes(offset + 0x400000).ToArray().CopyTo(bytePattern, 1);
                    };

                    foreach (var file in files)
                    {
                        FindPattern(exeFile, Encoding.ASCII.GetBytes(file), patternSearchFunc);
                        FindPattern(exeFile, bytePattern, patchFunc);
                    }

                    // SPBB.AFP - Special Case - With the drive letter overwriting
                    FindPattern(exeFile, Encoding.ASCII.GetBytes(@"d:\SPBB.AFP"), patternSearchFunc);
                    bytePattern[5] = 0x88;
                    FindPattern(exeFile, bytePattern, (file, br, bw, offset) =>
                    {
                        file.Seek(offset - 0x2b, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x31, 0xc0, 0x40, 0xc3 }); // xor eax, eax inc eax retn
                    });

                    // Makes the .AFP files all equal cm0001.exe. This way a file will always be loaded.
                    // Stops corruption on Wine based systems (Like Exagear Strategies on Android).
                    //PatchEXEFile0001Fix(exeFile); 

                    // Improved Fix: Makes SPBB.AFP (the first of the files listed in the exe == data\\index.dat and then makes
                    // all references point to that, rather than the other 4.
                    PatchEXEFile0001FixV2(exeFile);
                }
            }

            return patched;
        }

        static byte[] HexStringToBytes(string hexString)
        {
            byte[] ret = new byte[hexString.Length / 2];
            hexString = hexString.ToLower();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                ret[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return ret;
        }

        public static void GenericCDCrack(string exeFile)
        {
            // Let search for calls to KERNEL32.GetLogicalDriveStringsA. There's always a push PUSH 0C8 beforehand
            // 0040AF74 |.  68 C8000000 PUSH 0C8; | Bufsize = 200.
            // 0040AF79 |.FF15 24817700 CALL DWORD PTR DS:[<&KERNEL32.GetLogicalDriveStringsA>]
            // ^ CM99 
            FindPattern(exeFile, HexStringToBytes("68C8000000FF15"), (f, br, bw, offset) =>
            {
                Console.WriteLine("Offset: {0:x8}", offset);

                // Search for CMP EAX,5 83F805
                var cmpeax5 = FindNext(f, offset, HexStringToBytes("83F805"));

                // Swap with CMP EAX, EAX (if not too far away from what we found)
                if (cmpeax5 != -1 && (cmpeax5 - offset) < 100)
                {
                    Console.WriteLine("   Found CMP EAX, 5: {0:x8}", cmpeax5);
                    bw.Seek(cmpeax5, SeekOrigin.Begin);
                    bw.Write(HexStringToBytes("39c090"));
                }

                /* Search for the pushing of CM0102.exe or similar
                004131DD  |.  6A 65                     ||PUSH 65                                ; /<%c> = 'e'
                004131DF  |.  6A 78                     ||PUSH 78                                ; |<%c> = 'x'
                004131E1  |.  6A 65                     ||PUSH 65                                ; |<%c> = 'e'
                004131E3  |.  6A 2E                     ||PUSH 2E                                ; |<%c> = '.'
                004131E5  |.  6A 32                     ||PUSH 32                                ; |<%c> = '2'
                004131E7  |.  6A 30                     ||PUSH 30                                ; |<%c> = '0'
                004131E9  |.  6A 31                     ||PUSH 31                                ; |<%c> = '1'
                004131EB  |.  6A 30                     ||PUSH 30                                ; |<%c> = '0'
                004131ED  |.  6A 6D                     ||PUSH 6D                                ; |<%c> = 'm'
                004131EF  |.  6A 63                     ||PUSH 63                                ; |<%c> = 'c'
                004131F1  |.  56                        ||PUSH ESI                               ; |<%s> */
                // or PUSH EDI (57)

                // Search for cm 
                var pushcm = FindNext(f, offset, HexStringToBytes("6a6d6a6356"));
                if (pushcm == -1 || (pushcm - offset) >= 200)
                    pushcm = FindNext(f, offset, HexStringToBytes("6a6d6a6357"));

                /* Swap with *\0 
                004131ED |.  6A 00 || PUSH 0; |<% c > = 00
                004131EF |.  6A 2A || PUSH 2A; |<% c > = '*'
                004131F1 |.  56 || PUSH ESI; |<% s > */
                if (pushcm != -1 && (pushcm - offset) < 200)
                {
                    Console.WriteLine("   Found Push CM: {0:x8}", pushcm);
                    bw.Seek(pushcm, SeekOrigin.Begin);
                    bw.Write(HexStringToBytes("6a006a2a56"));
                }
                else
                    Console.WriteLine("   NOT Found Push CM: {0:x8}", pushcm);
            });
        }

        public static void GenericCDCrack2(string exeFile)
        {
            // We are going to hook every call to GetLogicalDriveStringsA and GetDriveTypeA to call our own function that will:
            // GetLogicalDriveStringsA = return ".\" in the buffer and 1 in EAX
            // GetDriveTypeA return EAX with 5 (meaning every drive is a CD Drive)
            var newGetLogialDriveStringsA = HexStringToBytes("8B442408C7002E5C000031C040C20800"); // 16 bytes   <--- This was originally 8B44E4 <- Which is incorrect, needs to be 24 not e4 (Olly issue)
            var newGetDriveTypeA = HexStringToBytes("B805000000C20400"); // 8 bytes

            // Going to place at the very end of the exe code space - 0x966FFF (the last byte) - 24 bytes = 966FE7 (+1) = 966FE8
            // The save space for two pointer too that point to the functions 966FE8 - 4 - 4 = 966FE0
            UInt32 pointerToinjectnewGetLogialDriveStringsAAt = 0x966FE0;
            UInt32 pointerToinjectnewGetDriveTypeAAt = pointerToinjectnewGetLogialDriveStringsAAt + 4;
            UInt32 injectnewGetLogialDriveStringsAAt = pointerToinjectnewGetDriveTypeAAt + 4;
            UInt32 injectnewGetDriveTypeAAt = injectnewGetLogialDriveStringsAAt + 16;
            using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                // Write the pointers to the new functions
                file.Seek(pointerToinjectnewGetLogialDriveStringsAAt - 0x400000, SeekOrigin.Begin);
                file.Write(BitConverter.GetBytes(injectnewGetLogialDriveStringsAAt), 0, 4);
                file.Write(BitConverter.GetBytes(injectnewGetDriveTypeAAt), 0, 4);

                // Write the two new functions
                file.Seek(injectnewGetLogialDriveStringsAAt - 0x400000, SeekOrigin.Begin);
                file.Write(newGetLogialDriveStringsA, 0, newGetLogialDriveStringsA.Length);
                file.Write(newGetDriveTypeA, 0, newGetDriveTypeA.Length);
            }

            // Now need to patch all occurances of calling GetLogicalDriveStringsA or GetDriveTypeA to point to our new functions.
            // Currently they are pointing to pointers of pointers, this is why we store the pointers just before the functions
            // Sometimes they are not called directly like: 
            // 00828186 |.FF15 F8709600 CALL DWORD PTR DS:[< &KERNEL32.GetLogicalDriveStringsA >]; \KERNEL32.GetLogicalDriveStringsA
            // 004131D2  |.  FF15 00719600             ||CALL DWORD PTR DS:[<&KERNEL32.GetDrive ; \KERNEL32.GetDriveTypeA
            // But like this:
            // 0082AA7B  |.  8B2D F8709600             MOV EBP,DWORD PTR DS:[<&KERNEL32.GetLogi
            // 00442ACF |.  8B3D 00719600             MOV EDI, DWORD PTR DS:[<&KERNEL32.GetDriv

            var bytePrefixes = new string[] { "FF15", "8B3D", "8B2D" };
            var byteOffsets = new string[] { "F8709600", "00719600" };
            var byteReplacements = new UInt32[] { pointerToinjectnewGetLogialDriveStringsAAt, pointerToinjectnewGetDriveTypeAAt };
            for (int i = 0; i < byteOffsets.Length; i++)
            {
                foreach (var prefix in bytePrefixes)
                {
                    FindPattern(exeFile, HexStringToBytes(prefix + byteOffsets[i]), (f, br, bw, offset) =>
                    {
                        f.Seek(offset+2, SeekOrigin.Begin);
                        bw.Write(byteReplacements[i]);
                    }, false);
                }
            }

            // Now patch string "%s%c%c%c%c%c%c%c%c%c%c" to point to something in data
            // This way you can rename the exe to whatever
            FindPattern(exeFile, Encoding.ASCII.GetBytes("%s%c%c%c%c%c%c%c%c%c%c\0"), (f, br, bw, offset) =>
            {
                f.Seek(offset, SeekOrigin.Begin);
                bw.Write(Encoding.ASCII.GetBytes("%sdata\\index.dat\0"));
            }, false);
        }

        public static int FindNext(Stream fin, int offset, byte[] pattern)
        {
            int retOffset = -1;
            fin.Seek(offset, SeekOrigin.Begin);
            int match = 0;
            while (true)
            {
                var readByte = fin.ReadByte();
                offset++;
                if (readByte == -1)
                    break;
                if (readByte == pattern[match])
                    match++;
                else
                    match = 0;
                if (match == pattern.Length)
                {
                    retOffset = offset - pattern.Length;
                    break;
                }
            }
            return retOffset;
        }

        public static void PatchEXEFile0001Fix(string exeFile)
        {
            string[] files = new string[] { @"d:\KPWN.AFP", @"d:\SPBB.AFP", @"d:\PWQE.AFP", @"d:\EVWF.AFP" };
            foreach (var file in files)
            {
                FindPattern(exeFile, Encoding.ASCII.GetBytes(file), (f, br, bw, offset) =>
                {
                    bw.Seek(offset, SeekOrigin.Begin);
                    bw.Write(Encoding.ASCII.GetBytes("cm0001.exe"));
                    bw.Write((byte)0);
                });
            }
        }

        public static void PatchEXEFile0001FixV2(string exeFile)
        {
            string[] files = new string[] { @"d:\KPWN.AFP", @"d:\PWQE.AFP", @"d:\EVWF.AFP" };
            byte[] SPBBBytePattern = new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00 };
            int SPBBOffset = 0;

            FindPattern(exeFile, Encoding.ASCII.GetBytes(@"d:\SPBB.AFP"), (f, br, bw, offset) =>
            {
                SPBBOffset = offset;
                BitConverter.GetBytes(offset + 0x400000).ToArray().CopyTo(SPBBBytePattern, 1);
            });

            foreach (var file in files)
            {
                byte[] otherBytePattern = new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0xE8 };
                FindPattern(exeFile, Encoding.ASCII.GetBytes(file), (f, br, bw, offset) =>
                {
                    BitConverter.GetBytes(offset + 0x400000).ToArray().CopyTo(otherBytePattern, 1);
                });

                // Found the byte pattern, now fix it to the SPBB byte pattern
                FindPattern(exeFile, otherBytePattern, (f, br, bw, offset) =>
                {
                    bw.Seek(offset, SeekOrigin.Begin);
                    bw.Write(SPBBBytePattern);
                });
            }

            using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    bw.Seek(SPBBOffset, SeekOrigin.Begin);
                    bw.Write(Encoding.ASCII.GetBytes("data\\index.dat"));
                    bw.Write((byte)0);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NoCDPatch
    {
        public static byte[] pattern = new byte[] { 0x75, 0x00, 0x6a, 0x65, 0x6a, 0x78, 0x6a, 0x65, 0x6a, 0x2e, 0x6a, 0x00, 0x6a, 0x30 };

        public static int FindPattern(string fileName, byte[] pattern, Action<Stream, BinaryReader, BinaryWriter, int> func)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                return FindPattern(file, pattern, func);
            }
        }

        public static int FindPattern(Stream stream, byte[] pattern, Action<Stream, BinaryReader, BinaryWriter, int> func)
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
                        if (pattern[match] == 0)
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

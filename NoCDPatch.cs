using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NoCDPatch
    {
        public byte[] pattern = new byte[] { 0x75, 0x00, 0x6a, 0x65, 0x6a, 0x78, 0x6a, 0x65, 0x6a, 0x2e, 0x6a, 0x00, 0x6a, 0x30 };

        int FindPattern(string fileName, byte[] pattern, Action<FileStream, BinaryReader, BinaryWriter, int> func)
        {
            int patched = 0;
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    using (var br = new BinaryReader(file))
                    {
                        int match = 0;
                        int fileLength = (int)file.Length;
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
                                    func(file, br, bw, savedPos);
                                    patched++;
                                    match = 0;
                                }
                            }
                            else
                                match = 0;
                        }
                    }
                }
            }
            return patched;
        }

        public int PatchEXEFile(string exeFile)
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
                FindPattern(exeFile, Encoding.ASCII.GetBytes("KPWN.AFP"), (file, br, bw, offset) => { is0001 = true; });
                if (is0001)
                {
                    string[] files = new string[] { @"d:\KPWN.AFP", @"d:\SPBB.AFP", @"d:\PWQE.AFP", @"d:\EVWF.AFP" };
                    byte[] bytePatten = new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0xE8 };

                    Action<FileStream, BinaryReader, BinaryWriter, int> patchFunc = (file, br, bw, offset) =>
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

                    Action<FileStream, BinaryReader, BinaryWriter, int> patternSearchFunc = (file, br, bw, offset) =>
                    {
                        BitConverter.GetBytes(offset + 0x400000).ToArray().CopyTo(bytePatten, 1);
                    };

                    foreach (var file in files)
                    {
                        FindPattern(exeFile, Encoding.ASCII.GetBytes(file), patternSearchFunc);
                        FindPattern(exeFile, bytePatten, patchFunc);
                    }

                    // SPBB.AFP - Special Case - With the drive letter overwriting
                    FindPattern(exeFile, Encoding.ASCII.GetBytes(@"d:\SPBB.AFP"), patternSearchFunc);
                    bytePatten[5] = 0x88;
                    FindPattern(exeFile, bytePatten, (file, br, bw, offset) =>
                    {
                        file.Seek(offset - 0x2b, SeekOrigin.Begin);
                        bw.Write(new byte[] { 0x31, 0xc0, 0x40, 0xc3 }); // xor eax, eax inc eax retn
                    });
                }
            }

            return patched;
        }
    }
}

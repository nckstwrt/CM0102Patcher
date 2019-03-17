using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NoCDPatch
    {
        byte[] pattern = new byte[] { 0x75, 0x00, 0x6a, 0x65, 0x6a, 0x78, 0x6a, 0x65, 0x6a, 0x2e, 0x6a, 0x32, 0x6a, 0x30 };
        
        public int PatchEXEFile(string exeFile)
        {
            int patched = 0;
            using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
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
                                    file.Seek(savedPos, SeekOrigin.Begin);
                                    bw.Write(new byte[] { 0x90, 0x90 });
                                    file.Seek(17, SeekOrigin.Current);
                                    bw.Write(new byte[] { 0x00, 0x6a, 0x2a });
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
        }
    }
}

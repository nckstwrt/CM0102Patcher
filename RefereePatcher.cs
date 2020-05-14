using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class RefereePatcher
    {
        public void PatchOfficialsFile(string officialsFileName, int percentChange, bool disciplineOnlyMode)
        {
            using (var officialsFile = File.Open(officialsFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                int fileLength = (int)officialsFile.Length;
                byte[] fileBytes = new byte[fileLength];
                officialsFile.Read(fileBytes, 0, fileLength);
                for (int i = 0; i < fileLength; i += 43)
                {
                    var ca = BitConverter.ToInt16(fileBytes, i + 30);
                    var pa = BitConverter.ToInt16(fileBytes, i + 32);
                    var discipline = fileBytes[i + 37];

                    if (!disciplineOnlyMode)
                    {
                        if (ca > 0)
                        {
                            ca = (short)((((double)ca) * (((double)percentChange) / 100.0)) + 0.5);
                            BitConverter.GetBytes(ca).CopyTo(fileBytes, i + 30);
                        }
                        if (pa > 0)
                        {
                            pa = (short)((((double)pa) * (((double)percentChange) / 100.0)) + 0.5);
                            BitConverter.GetBytes(ca).CopyTo(fileBytes, i + 32);
                        }
                    }
                    if (discipline > 0 || disciplineOnlyMode)
                    {
                        if (disciplineOnlyMode)
                        {
                            fileBytes[i + 37] = (byte)percentChange;
                        }
                        else
                        {
                            discipline = (byte)((((double)discipline) * (((double)percentChange) / 100.0)) + 0.5);
                            fileBytes[i + 37] = discipline;
                        }
                    }
                }

                officialsFile.Seek(0, SeekOrigin.Begin);
                officialsFile.Write(fileBytes, 0, fileLength);
            }
        }
    }   
}

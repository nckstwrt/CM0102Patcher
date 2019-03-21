using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NamePatcher
    {
        string exeFile;
        string dataDir;
        Patcher patcher;
        int freePos = 0x5861d0;

        public NamePatcher(string exeFile, string dataDir)
        {
            this.exeFile = exeFile;
            this.dataDir = dataDir;
            this.patcher = new Patcher();
        }

        public void RunPatch()
        {
            YearChanger yearChanger = new YearChanger();
            var currentYear = yearChanger.GetCurrentExeYear(exeFile);
            var newGameName = currentYear.ToString() + "/" + (currentYear+1).ToString().Substring(2);
            ByteSearch.WriteToFile(exeFile, 0x68029d, newGameName);

            PatchHolland();

            int compChangePos;
            compChangePos = PatchComp("European Champions Cup", "UEFA Champions League");
            if (compChangePos != -1)
                PatchComp("Champions Cup", "Champions League", compChangePos, -1);
            PatchComp("UEFA Cup", "UEFA Europa League");

            compChangePos = PatchComp("English Premier Division", "English Premier League");
            if (compChangePos != -1)
            {
                PatchComp("Premier Division", "Premier League", compChangePos, -1);
                PatchCompAcronym(compChangePos, "EPL");
            }
            
            compChangePos = PatchComp("English First Division", "English Football League Championship");
            if (compChangePos != -1)
            {
                PatchComp("First Division", "Championship", compChangePos, -1);
                PatchCompAcronym(compChangePos, "FLC");
            }

            compChangePos = PatchComp("English Second Division", "English Football League One");
            if (compChangePos != -1)
            {
                PatchComp("Second Division", "League One", compChangePos, -1);
                PatchCompAcronym(compChangePos, "FL1");
            }

            compChangePos = PatchComp("English Third Division", "English Football League Two");
            if (compChangePos != -1)
            {
                PatchComp("Third Division", "League Two", compChangePos, -1);
                PatchCompAcronym(compChangePos, "FL2");
            }
        }

        void PatchCompAcronym(int startPos, string acronym)
        {
            ByteSearch.WriteToFile(Path.Combine(dataDir, "club_comp.dat"), startPos + 79, acronym, 3);
        }

        // https://champman0102.co.uk/showthread.php?t=8267&highlight=Netherlands
        // Going to stick things at 009861d0 (005861d0 = in binary)
        // 0060E100  |> 68 34159B00 PUSH OFFSET 009B1534                     ; /Arg2 = ASCII "Holland"
        // 0060E100     68 D0619800 PUSH OFFSET 009861D0
        void PatchHolland()
        {
            freePos += ByteSearch.WriteToFile(exeFile, freePos, "Netherlands\0");
            patcher.ApplyPatch(exeFile, 0x20e100, "68D0619800");
            // nation.dat
            var nationDatFilename = Path.Combine(dataDir, "nation.dat");
            var nationDat = ByteSearch.LoadFile(nationDatFilename);
            var pos = ByteSearch.SearchBytes(nationDat, "Holland");
            if (pos != -1)
            {
                ByteSearch.WriteToFile(nationDatFilename, pos, "Netherlands", 52);
                ByteSearch.WriteToFile(nationDatFilename, pos+52, "Netherlands", 27);
                ByteSearch.WriteToFile(nationDatFilename, pos+52+27, "NED");
            }
            // nat_club.dat
            ByteSearch.BinFileReplace(Path.Combine(dataDir, "nat_club.dat"), "Holland", "Netherlands");
            // euro.cfg
            ByteSearch.TextFileReplace(Path.Combine(dataDir, "euro.cfg"), "Holland", "Netherlands");
        }

        int PatchComp(string fromComp, string toComp, int clubCompStartPos = 0, int exeStartPos = 0x5d9590)
        {
            var club_comp = Path.Combine(dataDir, "club_comp.dat");
            var exeBytes = ByteSearch.LoadFile(exeFile);
            int compChangePos = ByteSearch.BinFileReplace(club_comp, fromComp, toComp, clubCompStartPos, clubCompStartPos != 0 ? 1 : 0);

            if (exeStartPos != -1)
            {
                // Find where the string is held
                var pos = ByteSearch.SearchBytes(exeBytes, fromComp, exeStartPos);
                // Convert the position of the current string, to a PUSH statement in the exe
                var searchBytes = new byte[5] { 0x68, 0x00, 0x00, 0x00, 0x00 };
                BitConverter.GetBytes(pos + 0x400000).ToArray().CopyTo(searchBytes, 1);
                // Find the PUSH Statement in the EXE to this string
                var posExePush = ByteSearch.SearchBytes(exeBytes, searchBytes);
                if (posExePush != -1)
                {
                    // Get the next free position of text and convert to a PUSH
                    BitConverter.GetBytes(freePos + 0x400000).ToArray().CopyTo(searchBytes, 1);
                    // Write the new PUSH statement to the free pos
                    ByteSearch.WriteToFile(exeFile, posExePush, searchBytes);
                    // Write the new string to the free pos and increment the free pos
                    ByteSearch.WriteToFile(exeFile, freePos, toComp + "\0");
                }
                // Just because it wasn't found this time doesn't mean it wasn't already written to, so push the ptr forward anyway
                freePos += toComp.Length + 1;
            }

            return compChangePos;
        }
    }
}

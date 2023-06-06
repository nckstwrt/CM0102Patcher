using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CM0102Scout;
using System.Windows.Forms;

namespace CM0102Patcher.Scouter
{
    public class ScoutList
    {
        static byte [] byPlsStart = new byte[] { 0x9E, 0x4A, 0x02, 0x00, 0x01, 0x04 };

        static byte[] byPlsMid = new byte[] { 0x7E, 0x68, 0x00, 0x00, 0xFA, 0x00, 0x00, 0x00 };

        static byte byPlsDelimiter = 0xFF;

        static byte[] byPlsEnd = new byte[] {0x81, 0x00, 0x14, 0x14, 0x84, 0xA8, 0xAA, 0x44, 0x10, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00,
                                   0x05, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x81, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00,
                                   0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xF2, 0xFF, 0xFF, 0xF2 };

        static byte[] byPlsEIn = new byte[] {0x81, 0x00, 0x14, 0x14, 0x84, 0x28, 0xAB, 0x44, 0x10, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00,
                                   0x05, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
                                   0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14, 0x14,
                                   0x14, 0x14, 0x14, 0x14, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00, 0x00, 0x81, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00,
                                   0x00, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xF2, 0xFF, 0xFF, 0xF2 };

        static char[] byPlsName = new char[] { 'C', 'M', ' ', 'S', 'c', 'o', 'u', 't', ' ', 'S', 'e', 'a', 'r', 'c', 'h' };
        static char[] byPlsManName = new char[] { 'M', 'i', 'c', 'h', 'a', 'e', 'l', ' ', 'N', 'y', 'g', 'r', 'e', 'e', 'n' };

        static byte byPlsZero = 0x00;

        public static void WriteScoutFile(string fileName, Dictionary<int, Staff> staffList, List<int> playerIDs, bool append)
        {
            using (var fs = File.Open(fileName, append ? FileMode.Open : FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var filScoutList = new BinaryWriter(fs))
            using (var br = new BinaryReader(fs))
            {
                int j;

                if (append)
                {
                    int offsetToPlayers = 367;
                    fs.Seek(offsetToPlayers, SeekOrigin.Begin);
                    var currentNumber = br.ReadInt32();
                    fs.Seek(offsetToPlayers, SeekOrigin.Begin);

                    j = currentNumber + playerIDs.Count;

                    filScoutList.Write(j);

                    fs.Seek(currentNumber * (4+4+4+4+8+20+1), SeekOrigin.Current);
                }
                else
                {
                    filScoutList.Write(byPlsStart);
                    filScoutList.Write(byPlsName);
                    for (j = 0; j <= 85; j++)
                        filScoutList.Write(byPlsZero);

                    filScoutList.Write(byPlsManName);
                    for (j = 0; j <= 236; j++)
                        filScoutList.Write(byPlsZero);

                    filScoutList.Write(byPlsMid);

                    j = playerIDs.Count;

                    filScoutList.Write(j);
                }

                for (j = 0; j < playerIDs.Count; j++)
                {
                    var staff = staffList[playerIDs[j]];
                    filScoutList.Write(staff.firstName);
                    filScoutList.Write(staff.secondName);
                    filScoutList.Write(staff.commonName);
                    filScoutList.Write(staff.staffId);
                    filScoutList.Write(TCMDate.FromDateTime(staff.dob).ToBytes());

                    for (int s = 0; s <= 19; s++)
                        filScoutList.Write(byPlsZero);
                    filScoutList.Write(byPlsDelimiter);
                }

                filScoutList.Write(byPlsEnd);

                // John Locke hack to make it work in CM0102
                fs.Seek(5, SeekOrigin.Begin);
                filScoutList.Write((byte)1);
                for (j = 0; j < 355; j++)
                    filScoutList.Write((byte)0);

                MessageBox.Show(string.Format("Successfully {0} Scout File!", append ? "Appended" : "Saved"), "Scout File", MessageBoxButtons.OK, MessageBoxIcon.Information);

                /*
                filScoutList.Write(byPlsStart, 6);

  filScoutList.Write(byPlsName, 15);
  for j:=0 to 85 do
    filScoutList.Write(byPlsZero, 1);

  filScoutList.Write(byPlsManName, 15);
  for j:=0 to 236 do
    filScoutList.Write(byPlsZero, 1);

  filScoutList.Write(byPlsMid, 8);

  j:=FlcPlayers.Count;
  filScoutList.Write(j, SizeOf(Integer));

  for j:=0 to FlcPlayers.Count - 1 do
  begin
    filScoutList.Write(FlcPlayers[j].FirstName.ID, 4);
    filScoutList.Write(FlcPlayers[j].SecondName.ID, 4);
    filScoutList.Write(FlcPlayers[j].CommonName.ID, 4);
    filScoutList.Write(FlcPlayers[j].ID, 4);
    filScoutList.Write(FlcPlayers[j].DateOfBirth, SizeOf(TCMDate));

    for s:=0 to 19 do
      filScoutList.Write(byPlsZero, 1);
    filScoutList.Write(byPlsDelimiter, 1);
  end;

  if (Interested) then
    filScoutList.Write(byPlsEIn, 186)
  else
    filScoutList.Write(byPlsEnd, 186);

  filScoutList.Free;
                */


                /*
                raf.seek(5);
                raf.writeByte(1);
                for(int i=0; i<355; i++) {
                raf.writeByte(0);
                }
                */
            }
        }
    }
}

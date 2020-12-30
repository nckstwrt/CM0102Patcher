using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using CM0102Patcher;

namespace CM0102Scout
{
    public class SaveReader : IDisposable
    {
        public const int NationSize = 290;
        const int BlockSize = 268;
        const int PlayerSize = 70;
        const int ContractSize = 80;
        const int StaffSize = 110;
        const int NameSize = 60;
        const int ClubSize = 581;

        CMCompressedFileStream cfs;
        List<byte[]> FlstBlock = new List<byte[]>();
        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
        List<string> firstNames = new List<string>();
        List<string> secondNames = new List<string>();
        List<string> commonNames = new List<string>();
        Dictionary<int, Staff> staffList = new Dictionary<int, Staff>();
        List<Player> players = new List<Player>();
        Dictionary<int, Nation> nations = new Dictionary<int, Nation>();
        Dictionary<int, Club> clubs = new Dictionary<int, Club>();
        Dictionary<int, Contract> contracts = new Dictionary<int, Contract>();
        byte[] transfers = null;
        DateTime gameDate;

        public bool IsCompressed;

        public SaveReader(string saveFilename)
        {
            int blockCount;
            using (var sr = new StreamReader(saveFilename))
            {
                using (var br = new BinaryReader(sr.BaseStream))
                {
                    IsCompressed = (br.ReadInt32() == 4);

                    // Skip 4 byes
                    sr.BaseStream.Seek(4, SeekOrigin.Current);

                    // Read blocks
                    blockCount = br.ReadInt32();
                    for (int j = 0; j < blockCount; j++)
                    {
                        byte[] newBlock = new byte[BlockSize];
                        br.Read(newBlock, 0, BlockSize);
                        FlstBlock.Add(newBlock);
                        Console.WriteLine("Added: {0} (Offset: 0x{1} Size: {2})", TBlock.GetName(newBlock), TBlock.GetPosition(newBlock).ToString("X"), TBlock.GetSize(newBlock));
                    }
                }
            }

            cfs = new CMCompressedFileStream(saveFilename, IsCompressed);

            //DumpBlock("index.dat", @"c:\downloads\index.dat");
            //DumpBlock("transfer.dat", @"c:\downloads\transfer_dutch.dat");
            //DumpBlock("Preferences.dat", @"c:\downloads\Preferences.dat");
            //DumpBlock("general.dat", @"c:\downloads\general.dat");
            //DumpBlock("off.dat", @"c:\downloads\off.dat");
        }

        
        byte [] ReadBlock(string blockName)
        {
            var block = FlstBlock.FirstOrDefault(x => TBlock.GetName(x) == blockName);
            cfs.Seek(TBlock.GetPosition(block), SeekOrigin.Begin);

            int dataSize = TBlock.GetSize(block);
            var data = new byte[dataSize];

            int dataPtr = 0;
            var miniBuffer = new byte[32000];
            while (true)
            {
                var toRead = Math.Min(miniBuffer.Length, (dataSize - dataPtr));

                if (toRead == 0)
                    break;
                cfs.Read(miniBuffer, toRead);
                Array.Copy(miniBuffer, 0, data, dataPtr, toRead);
                dataPtr += toRead;
            }
            return data;
        }

        void DumpBlock(string blockName, string outFile)
        {
            var data = ReadBlock(blockName);

            using (var fs = File.Create(outFile))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(data);
            }
        }

        public void LoadNames()
        {
            firstNames.Clear();
            secondNames.Clear();
            commonNames.Clear();

            // Load First Names
            var firstNameBlocks = ReadBlocks("first_names.dat", NameSize);
            foreach (var firstNameBlock in firstNameBlocks)
            {
                firstNames.Add(latin1.GetString(firstNameBlock, 0, 50).TrimEnd('\0'));
            }

            // Load Second Names
            var secondNameBlocks = ReadBlocks("second_names.dat", NameSize);
            foreach (var secondNameBlock in secondNameBlocks)
            {
                secondNames.Add(latin1.GetString(secondNameBlock, 0, 50).TrimEnd('\0'));
            }

            // Load Common Names
            var commonNameBlocks = ReadBlocks("common_names.dat", NameSize);
            foreach (var commonNameBlock in commonNameBlocks)
            {
                commonNames.Add(latin1.GetString(commonNameBlock, 0, 50).TrimEnd('\0'));
            }

            // Load Game Date
            var generalBlocks = ReadBlocks("general.dat", 3944 + 8);
            gameDate = CMDate(generalBlocks[0], 3944);
        }

        DateTime CMDate(byte[] data, int position)
        {
            var day = BitConverter.ToInt16(data, position);
            var year = BitConverter.ToInt16(data, position + 2);
            var leapYear = BitConverter.ToInt16(data, position + 4);
            return CMDate(day, year, leapYear);
        }

        DateTime CMDate(short day, short year, int leapYear)
        {
            if (leapYear == 1)
                day += 1;
            else
                day += 2;
            return new DateTime(year, 1, 1).AddDays(day - 1);
        }

        public void LoadPlayers()
        {
            if (firstNames.Count == 0)
                LoadNames();

            // Load staff
            var staffBlocks = ReadBlocks("staff.dat", StaffSize);
            foreach (var staffBlock in staffBlocks)
            {
                var staff = new Staff();
                staff.staffId = BitConverter.ToInt32(staffBlock, 0);
                staff.firstName = BitConverter.ToInt32(staffBlock, 4);
                staff.secondName = BitConverter.ToInt32(staffBlock, 8);
                staff.commonName = BitConverter.ToInt32(staffBlock, 12);
                staff.playerId = BitConverter.ToInt32(staffBlock, StaffSize - (1 + 4 + 4 + 4));
                staff.value = BitConverter.ToInt32(staffBlock, StaffSize - (1 + 4 + 4 + 4 + 11 + 4));
                staff.determination = staffBlock[StaffSize - (1 + 4 + 4 + 4 + 9)];
                staff.dob = CMDate(staffBlock, 16);
                staff.yearOfBirth = (short)(2001 - BitConverter.ToInt16(staffBlock, 24));
                staff.nationID = BitConverter.ToInt32(staffBlock, 24 + 2);
                staff.clubID = BitConverter.ToInt32(staffBlock, StaffSize - (1 + 4 + 4 + 4 + 11 + 4 + +4 + 8 + 8 + 1 + 4));
                staffList[staff.playerId] = staff;
            }

            // Load Player History
            //var playerHistorBlocks = ReadBlocks("player stats history.tmp", 0x2f);
            //var staffHistoryBlocks = ReadBlocks("staff history.tmp", 0x39);

            // Load Nations
            var nationBlocks = ReadBlocks("nation.dat", NationSize);
            foreach (var nationBlock in nationBlocks)
            {
                var nation = new Nation();
                nation.nationID = BitConverter.ToInt32(nationBlock, 0);
                nation.name = latin1.GetString(nationBlock, 4, 50).TrimEnd('\0');
                nation.genderName = nationBlock[4 + 51];
                nation.shortName = latin1.GetString(nationBlock, 4 + 51 + 1, 26).TrimEnd('\0');
                nation.threeLetterName = latin1.GetString(nationBlock, 4 + 51 + 1 + 26 + 1, 3).TrimEnd('\0');
                nation.nationality = latin1.GetString(nationBlock, 4 + 51 + 1 + 26 + 1 + 4, 26).TrimEnd('\0');
                nation.continent = BitConverter.ToInt32(nationBlock, 4 + 51 + 1 + 26 + 1 + 4 + 26);
                nation.region = nationBlock[4 + 51 + 1 + 26 + 1 + 4 + 26 + 4];
                nation.actualRegion = nationBlock[4 + 51 + 1 + 26 + 1 + 4 + 26 + 4 + 1];
                nations[nation.nationID] = nation;
            }

            // Load Clubs
            var clubBlocks = ReadBlocks("club.dat", ClubSize);
            foreach (var clubBlock in clubBlocks)
            {
                var club = new Club();
                club.clubID = BitConverter.ToInt32(clubBlock, 0);
                club.name = latin1.GetString(clubBlock, 4, 50).TrimEnd('\0');
                club.genderName = clubBlock[4 + 51];
                club.shortName = latin1.GetString(clubBlock, 4 + 51 + 1, 26).TrimEnd('\0');
                clubs[club.clubID] = club;
            }

            // Load National Clubs
            var nationClubBlocks = ReadBlocks("nat_club.dat", ClubSize);

            // Load Players
            var playerBlocks = ReadBlocks("player.dat", PlayerSize);
            var fields = typeof(Player).GetFields();
            foreach (var playerBlock in playerBlocks)
            {
                int pos = 0;
                var player = new Player();
                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(int))
                    {
                        field.SetValue(player, BitConverter.ToInt32(playerBlock, pos));
                        pos += 4;
                    }
                    else
                    if (field.FieldType == typeof(sbyte))
                    {
                        field.SetValue(player, (sbyte)playerBlock[pos]);
                        pos += 1;
                    }
                    else
                    if (field.FieldType == typeof(byte))
                    {
                        field.SetValue(player, (byte)playerBlock[pos]);
                        pos += 1;
                    }
                    else
                    if (field.FieldType == typeof(short))
                    {
                        field.SetValue(player, BitConverter.ToInt16(playerBlock, pos));
                        pos += 2;
                    }
                }

                players.Add(player);
            }

            // Load Contracts
            var contractBlocks = ReadBlocks("contract.dat", ContractSize, true);
            foreach (var contractBlock in contractBlocks)
            {
                var ptrObj = Marshal.AllocHGlobal(ContractSize);
                Marshal.Copy(contractBlock, 0, ptrObj, ContractSize);
                Contract contract = (Contract)Marshal.PtrToStructure(ptrObj, typeof(Contract));
                // Ignore the fact there's two contracts in there for some people - just take the first
                if (!contracts.ContainsKey(contract.ID))
                    contracts[contract.ID] = contract;
                Marshal.FreeHGlobal(ptrObj);
            }

            // Load Transfers
            var transferDat = ReadBlock("transfer.dat");
            var msTransfers = new MemoryStream(transferDat);
            var totalNumberOfClubs = clubBlocks.Count + nationClubBlocks.Count;
            var totalStaff = staffBlocks.Count;
            msTransfers.Seek(0, SeekOrigin.Begin);
            transfers = ReadTransfers(msTransfers, totalNumberOfClubs, totalStaff);
        }

        byte [] ReadTransfers(MemoryStream ms, int totalNumberOfClubs, int totalStaff)
        {
            byte[] ret = null;
            try
            {
                using (var br = new BinaryReader(ms))
                {
                    var v2plus4 = br.ReadInt32();
                    var v2plus8 = br.ReadInt32();
                    var v9 = v2plus8;

                    if (v9 > 0)
                    {
                        for (int i = 0; i < v9; i++)
                        {
                            // Read 0x41
                            // Read 0x2
                            // Read that many 4s
                            // Read 0x2
                            br.ReadBytes(0x41);
                            var fours = br.ReadInt16();
                            br.ReadBytes(4 * fours);
                            br.ReadInt16();
                        }
                    }
                    var location = ms.Position; // 37d7 0x3 0x2
                    var v2_plus16 = br.ReadInt32();
                    var v2_plus20 = br.ReadInt32();
                    var v21 = v2_plus20;
                    if (v21 > 0)
                    {
                        for (int i = 0; i < v21; i++)
                        {
                            // Read 0x41
                            // Read 0x2
                            // Read that many 4s
                            // Read 0x2
                            br.ReadBytes(0x41);
                            var fours = br.ReadInt16();
                            br.ReadBytes(4 * fours);
                            br.ReadInt16();
                        }
                    }
                    location = ms.Position;
                    for (int k = 0; k < 50; k++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            // Keep reading 2 bytes until you get something that isn't 0
                            var twobytes = br.ReadInt16();
                            if (twobytes != 0)
                            {
                                for (int i = 0; i < twobytes; i++)
                                    br.ReadInt32();
                            }
                        }
                    }
                    location = ms.Position;
                    var v2_plus2124 = br.ReadByte();
                    var v2_plus2125_1 = br.ReadInt32(); // current day
                    var v2_plus2125_2 = br.ReadInt32(); // current day
                    var v2_plus2133 = br.ReadByte();
                    // Shortlist Manager Load
                    // 4, then 8, then 16 * 4, then 4, 
                    var s1 = br.ReadInt32();   // 0083AA01 |.E8 2A820F00 CALL 00932C30; \cm0102.00932C30
                    for (int i = 0; i < s1; i++)
                    {
                        var s2_1 = br.ReadInt32(); // Read 8 Bytes at 0083AABD |.E8 6E810F00                   CALL 00932C30; \cm0102.00932C30
                        var s2_2 = br.ReadInt32();
                    }
                    location = ms.Position;
                    List<int> sizes = new List<int>();
                    for (int i = 0; i < 16; i++)
                    {
                        sizes.Add(br.ReadInt32());
                    }
                    var s4 = br.ReadInt32();   // 0083ACBB |.E8 707F0F00                   CALL 00932C30; \cm0102.00932C30
                    location = ms.Position;

                    int edi = 6;
                    while (true)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            var bytes = br.ReadBytes(0x0c * sizes[i]);
                        }
                        edi += 0x10;
                        if (edi >= 0x26)
                            break;
                    }
                    location = ms.Position;

                    // 0083ADCB |.E8 607E0F00                   CALL 00932C30; \cm0102.00932C30   - EAX == AD1 (2798) * 9 ---- 0083ADBB |.A1 5C23AE00 MOV EAX,DWORD PTR DS:[0AE235C]   <---- Number of Clubs + Nation Clubs
                    int clubs = totalNumberOfClubs;
                    var club_data = br.ReadBytes(clubs * 9);
                    location = ms.Position;

                    for (int i = 0; i < clubs; i++)
                    {
                        var d = br.ReadBytes(0x25 * 0x28);
                    }

                    var e = br.ReadInt32(); // 008BC829 |.E8 02640700                   CALL 00932C30; \cm0102.00932C30
                    var ESIplus86A = br.ReadInt32(); // 008BC8BA |.E8 71630700                   CALL 00932C30; \cm0102.00932C30  -> this is then put in 008BC999 |.  8B96 6A080000 MOV EDX,DWORD PTR DS:[ESI+86A]

                    for (int i = 0; i < ESIplus86A; i++)
                    {
                        br.ReadBytes(0x25);
                    }

                    var ESIplus928 = br.ReadInt32(); // 008BC9FF |.E8 2C620700 CALL 00932C30; \cm0102.00932C30

                    for (int i = 0; i < ESIplus928; i++)
                    {
                        br.ReadBytes(0xc);
                    }

                    location = ms.Position;
                    // Then number of staff * 8 is the data we want
                    var temp = new byte[totalStaff * 8];
                    for (int i = 0; i < totalStaff; i++)
                    {
                        var transferBytes = br.ReadBytes(8);
                        Array.Copy(transferBytes, 0, temp, i * 8, 8);
                    }
                    ret = temp;
                }
            }
            catch { }
            return ret;
        }

        public Staff FindPlayer(string firstName, string secondName)
        {
            Staff ret = null;
            var firstNameId = firstNames.IndexOf(firstName);
            var secondNameId = secondNames.IndexOf(secondName);
            foreach (var staff in staffList.Values)
            {
                if (staff.firstName == firstNameId && staff.secondName == secondNameId)
                {
                    ret = staff;
                }
            }
            return ret;
        }

        public DataTable CreateDataTable(bool instrinsicsOn = true)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            dataTable.Columns.Add("Club", typeof(string));
            dataTable.Columns.Add("Nationality", typeof(string));
            dataTable.Columns.Add("Position", typeof(string));
            dataTable.Columns.Add("CA", typeof(int));
            dataTable.Columns.Add("PA", typeof(int));
            dataTable.Columns.Add("Value", typeof(int));
            dataTable.Columns.Add("Acceleration", typeof(sbyte));
            dataTable.Columns.Add("Aggression", typeof(sbyte));
            dataTable.Columns.Add("Agility", typeof(sbyte));
            dataTable.Columns.Add("Anticipation", typeof(sbyte));
            dataTable.Columns.Add("Balance", typeof(sbyte));
            dataTable.Columns.Add("Bravery", typeof(sbyte));
            dataTable.Columns.Add("Consistency", typeof(sbyte));
            dataTable.Columns.Add("Corners", typeof(sbyte));
            dataTable.Columns.Add("Creativity", typeof(sbyte));
            dataTable.Columns.Add("Crossing", typeof(sbyte));
            dataTable.Columns.Add("Decisions", typeof(sbyte));
            dataTable.Columns.Add("Determination", typeof(sbyte));
            dataTable.Columns.Add("Dirtiness", typeof(sbyte));
            dataTable.Columns.Add("Dribbling", typeof(sbyte));
            dataTable.Columns.Add("Finishing", typeof(sbyte));
            dataTable.Columns.Add("Flair", typeof(sbyte));
            dataTable.Columns.Add("Free Kicks", typeof(sbyte));
            dataTable.Columns.Add("Handling", typeof(sbyte));
            dataTable.Columns.Add("Heading", typeof(sbyte));
            dataTable.Columns.Add("Important Matches", typeof(sbyte));
            dataTable.Columns.Add("Influence", typeof(sbyte));
            dataTable.Columns.Add("Injury Proneness", typeof(sbyte));
            dataTable.Columns.Add("Jumping", typeof(sbyte));
            dataTable.Columns.Add("Left Foot", typeof(sbyte));
            dataTable.Columns.Add("Long Shots", typeof(sbyte));
            dataTable.Columns.Add("Marking", typeof(sbyte));
            dataTable.Columns.Add("Natural Fitness", typeof(sbyte));
            dataTable.Columns.Add("Off The Ball", typeof(sbyte));
            dataTable.Columns.Add("One On Ones", typeof(sbyte));
            dataTable.Columns.Add("Pace", typeof(sbyte));
            dataTable.Columns.Add("Passing", typeof(sbyte));
            dataTable.Columns.Add("Penalties", typeof(sbyte));
            dataTable.Columns.Add("Positioning", typeof(sbyte));
            dataTable.Columns.Add("Reflexes", typeof(sbyte));
            dataTable.Columns.Add("Right Foot", typeof(sbyte));
            dataTable.Columns.Add("Stamina", typeof(sbyte));
            dataTable.Columns.Add("Strength", typeof(sbyte));
            dataTable.Columns.Add("Tackling", typeof(sbyte));
            dataTable.Columns.Add("Teamwork", typeof(sbyte));
            dataTable.Columns.Add("Technique", typeof(sbyte));
            dataTable.Columns.Add("Throw Ins", typeof(sbyte));
            dataTable.Columns.Add("Versatility", typeof(sbyte));
            dataTable.Columns.Add("Work Rate", typeof(sbyte));
            dataTable.Columns.Add("Player Morale", typeof(byte));
            dataTable.Columns.Add("Squad Status", typeof(string));
            dataTable.Columns.Add("Contract Age", typeof(int));
            dataTable.Columns.Add("Leaving Club", typeof(string));
            dataTable.Columns.Add("Scouter Rating", typeof(int));

            // Creativity = Vision
            // Movement = Off The Ball

            Weighter weighter = new Weighter();
            foreach (var player in players)
            {
                try
                {
                    var staff = staffList[player.ID];

                    /// SOME NOTES FOR MYSELF WHILE WRITING NEW REGEN CODE
                    // The increase happens at 5d5078. The increase is dependent, in a semi-random way, on a magic number stored at [esp+5c]
                    // https://champman0102.co.uk/showthread.php?t=5310&page=2
                    // CPU Disasm
                    // 005D5078 |.  8846 2F | MOV BYTE PTR DS:[ESI + 2F],AL
                    // Func Starts:
                    // 005CE2D0 /$  55            PUSH EBP
                    // I've found the main retirement/regen code (7AACE0 onwards) 
                    // 7abac0
                    // 538ec0
                    // 3acfa0 <-- offset in file to unhook regens (7acfa0)
                    // Func Starts at:
                    // 007ABAC0 /$  83EC 08       SUB ESP,8; cm0102.007ABAC0(guessed Arg1)
                    // Jumps to 
                    // 00602700   $  60            PUSHAD; cm0102.00602700(guessed void)

                    // 22nd Nov
                    // https://champman0102.co.uk/showthread.php?t=5310&p=220885#post220885 <-- Saturns Table
                    // https://hugo9cf.wordpress.com/resources/the-9cf-page/

                    //if (player.ID == 0x0523)
                        //Console.WriteLine();
                    var name = firstNames[staff.firstName] + " " + secondNames[staff.secondName];

                    if (staff.commonName > 0 && commonNames[staff.commonName] != "" && commonNames[staff.commonName][0] != 0)
                        name = commonNames[staff.commonName];

                    name = MiscFunctions.RemoveDiacritics(name);

                    var age = AgeCalc(staff.dob);
                    var club = "None";
                    if (clubs.ContainsKey(staff.clubID))
                        club = clubs[staff.clubID].shortName;
                    var nationality = "Unknown";
                    if (nations.ContainsKey(staff.nationID))
                        nationality = nations[staff.nationID].nationality;

                    /*
                    if (name == "Roy Keane")
                    {
                        Console.WriteLine();
                        var xx =commonNames.FirstOrDefault(x => x.Contains("Fucker"));
                    }*/

                    weighter.Reset(instrinsicsOn);
                    if (player.Goalkeeper >= 15)
                    {
                        weighter.Add(instrinsicsOn ? player.Handling : player.Convert(player.Handling, true, true), 90, 20);
                        weighter.Add(instrinsicsOn ? player.Reflexes : player.Convert(player.Reflexes, true, true), 90, 10);
                        weighter.Add(instrinsicsOn ? player.OneOnOnes : player.Convert(player.OneOnOnes, true, true), 90, 10);
                        weighter.Add(instrinsicsOn ? player.Positioning : player.Convert(player.Positioning), 90, 7);
                        weighter.Add(instrinsicsOn ? player.Anticipation : player.Convert(player.Anticipation), 90, 7);
                    }
                    else
                    if (player.Defender >= 15)
                    {
                        weighter.Add(instrinsicsOn ? player.Positioning : player.Convert(player.Positioning, true), 120, 12);
                        weighter.Add(instrinsicsOn ? player.Tackling : player.Convert(player.Tackling, true), 120, 8);
                        weighter.Add(instrinsicsOn ? player.Marking : player.Convert(player.Marking, true), 120, 8);
                        weighter.Add(staff.determination, 20, 4);
                        weighter.Add(player.Strength, 20, 4);
                    }
                    else
                    // Winger
                    if (player.AttackingMidfielder>=15 && (player.RightSide>=15 || player.LeftSide >= 15))
                    {
                        weighter.Add(player.Acceleration, 20, 10);
                        weighter.Add(player.PlayerPace, 20, 10);
                        weighter.Add(staff.determination, 20, 10);
                        weighter.Add(player.Aggression, 20, 8);
                        weighter.Add(player.Balance, 20, 8);
                        weighter.Add(player.Bravery, 20, 8);
                        weighter.Add(player.Flair, 20, 7);
                        weighter.Add(player.Technique, 20, 7);
                        weighter.Add(instrinsicsOn ? player.Dribbling : player.Convert(player.Dribbling, true), 120, 7);
                        weighter.Add(instrinsicsOn ? player.Movement : player.Convert(player.Movement, true), 120, 6);
                        weighter.Add(player.Agility, 20, 7);
                        weighter.Add(player.Teamwork, 20, 7);
                        weighter.Add(player.WorkRate, 20, 7);
                        weighter.Add(player.Strength, 20, 5);
                    }
                    else
                    // Midfielder (TODO: Do a separate one for for DMs)
                    if ((player.Midfielder >= 15 || player.DefensiveMidfielder >= 15))
                    {
                        weighter.Add(player.Acceleration, 20, 10);
                        weighter.Add(player.PlayerPace, 20, 10);
                        weighter.Add(staff.determination, 20, 10);
                        weighter.Add(player.Aggression, 20, 8);
                        weighter.Add(player.Balance, 20, 8);
                        weighter.Add(player.Bravery, 20, 8);
                        weighter.Add(player.Passing, 20, 8);
                        weighter.Add(player.Flair, 20, 7);
                        weighter.Add(player.Technique, 20, 7);
                        weighter.Add(instrinsicsOn ? player.Passing : player.Convert(player.Passing, true), 20, 8);
                        weighter.Add(instrinsicsOn ? player.Vision : player.Convert(player.Vision, true), 20, 8);
                        weighter.Add(instrinsicsOn ? player.Dribbling : player.Convert(player.Dribbling, true), 120, 7);
                        weighter.Add(instrinsicsOn ? player.Movement : player.Convert(player.Movement, true), 120, 6);
                        weighter.Add(player.Agility, 20, 7);
                        weighter.Add(player.Teamwork, 20, 7);
                        weighter.Add(player.WorkRate, 20, 7);
                        weighter.Add(player.Strength, 20, 5);
                    }
                    
                    if (player.Attacker>=15)
                    {
                        // If Attacker - reset - overrides everything else
                        weighter.Reset(instrinsicsOn);
                        weighter.Add(instrinsicsOn ? player.Finishing : player.Convert(player.Finishing, true), 120, 12);
                        weighter.Add(instrinsicsOn ? player.Movement : player.Convert(player.Movement, true), 120, 12);
                        weighter.Add(player.PlayerPace, 20, 7);
                        weighter.Add(player.Jumping, 20, 7);
                        weighter.Add(player.Acceleration, 20, 7);
                        weighter.Add(player.Flair, 20, 7);
                        weighter.Add(player.Technique, 20, 5);
                        weighter.Add(staff.determination, 20, 5);
                        weighter.Add(player.Balance, 20, 4);
                        weighter.Add(player.Bravery, 20, 4);
                    }


                    string leavingClub = "Unknown";
                    string squadStatus = "Unknown";
                    int contractAgesMonths = 0;
                    if (contracts.ContainsKey(staff.staffId))
                    {
                        /*
                        if (name == "Williams Velasquez")
                            Console.WriteLine();
                        if (name == "Alejandro Balde")
                            Console.WriteLine();
                        */
                        var contract = contracts[staff.staffId];
                        if ((contract.SquadStatus & 240) == 0)
                            squadStatus = "Uncertain";
                        else if ((contract.SquadStatus & 224) == 0)
                            squadStatus = "Indispensable";
                        else if ((contract.SquadStatus & 208) == 0)
                            squadStatus = "Important";
                        else if ((contract.SquadStatus & 192) == 0)
                            squadStatus = "Rotation";
                        else if ((contract.SquadStatus & 176) == 0)
                            squadStatus = "Backup";
                        else if ((contract.SquadStatus & 160) == 0)
                            squadStatus = "Hot Prospect";
                        else if ((contract.SquadStatus & 144) == 0)
                            squadStatus = "Decent Yng";
                        else if ((contract.SquadStatus & 128) == 0)
                            squadStatus = "Not Needed";
                        else if ((contract.SquadStatus & 112) == 0)
                            squadStatus = "On Trial";

                        DateTime startDate = YearChanger.FromCMDate(contract.DateStarted.Day, contract.DateStarted.Year, contract.DateStarted.LeapYear);
                        contractAgesMonths = (int)Math.Round((gameDate - startDate).TotalDays / 30);

                        if (transfers == null)
                        {
                            if (contract.TransferArrangedFor == -1 && contract.LeavingOnBosman == 0)
                                leavingClub = "No";
                            else
                                leavingClub = "Yes";
                        }
                        else
                        {
                            leavingClub = "No";
                            var transferByte = transfers[8 * staff.staffId];
                            if (transferByte != 0x10 && transferByte != 0x0 && transferByte != 0x40 && transferByte != 0x1)
                            {
                                leavingClub = "Yes";
                            }
                        }
                    }

                    dataTable.Rows.Add(name, age, club, nationality, player.ShortPosition(), player.CurrentAbility, player.PotentialAbility, staff.value,
                        player.Acceleration,
                        player.Aggression,
                        player.Agility,
                        instrinsicsOn ? player.Anticipation : player.Convert(player.Anticipation),
                        player.Balance,
                        player.Bravery,
                        player.Consistency,
                        player.Corners,
                        instrinsicsOn ? player.Vision : player.Convert(player.Vision, true),
                        instrinsicsOn ? player.Crossing : player.Convert(player.Crossing, true),
                        instrinsicsOn ? player.Decisions : player.Convert(player.Decisions),
                        staff.determination,
                        player.Dirtiness,
                        instrinsicsOn ? player.Dribbling : player.Convert(player.Dribbling, true),
                        instrinsicsOn ? player.Finishing : player.Convert(player.Finishing, true),
                        player.Flair,
                        player.FreeKicks,
                        instrinsicsOn ? player.Handling : player.Convert(player.Handling, true, true),
                        instrinsicsOn ? player.Heading : player.Convert(player.Heading),
                        player.ImportantMatches,
                        player.Leadership,
                        player.InjuryProneness,
                        player.Jumping,
                        player.LeftFoot,
                        instrinsicsOn ? player.LongShots : player.Convert(player.LongShots),
                        instrinsicsOn ? player.Marking : player.Convert(player.Marking, true),
                        player.NaturalFitness,
                        instrinsicsOn ? player.Movement : player.Convert(player.Movement, true),
                        instrinsicsOn ? player.OneOnOnes : player.Convert(player.OneOnOnes, true, true),
                        player.PlayerPace,
                        instrinsicsOn ? player.Passing : player.Convert(player.Passing),
                        instrinsicsOn ? player.Penalties : player.Convert(player.Penalties),
                        instrinsicsOn ? player.Positioning : player.Convert(player.Positioning),
                        instrinsicsOn ? player.Reflexes : player.Convert(player.Reflexes, true, true),
                        player.RightFoot,
                        player.Stamina,
                        player.Strength,
                        instrinsicsOn ? player.Tackling : player.Convert(player.Tackling),
                        player.Teamwork,
                        player.Technique,
                        instrinsicsOn ? player.ThrowIns : player.Convert(player.ThrowIns, true),
                        player.Versatility,
                        player.WorkRate,
                        player.PlayerMorale,
                        squadStatus,
                        contractAgesMonths,
                        leavingClub,
                        weighter.RatingPercentage
                        );
                }
                catch
                {

                }
            }
            return dataTable;
        }

        int AgeCalc(DateTime dob)
        {
            int age = gameDate.Year - dob.Year;
            if (dob > gameDate.AddYears(-age)) age--;
            return age;
        }

        public void Dispose()
        {
            cfs.Dispose();
        }

        List<byte[]> ReadBlocks(string blockName, int blockSize, bool contractSpecific = false)
        {
            var blocks = new List<byte[]>();
            var block = FlstBlock.FirstOrDefault(x => TBlock.GetName(x) == blockName);
            cfs.Seek(TBlock.GetPosition(block), SeekOrigin.Begin);

            var blockCount = TBlock.GetSize(block) / blockSize;

            if (contractSpecific)
            {
                var intPreCount = cfs.ReadInt32();
                blockCount = cfs.ReadInt32();

                var byPre = new byte[21];
                for (int j = 0; j < intPreCount; j++)
                    cfs.Read(byPre, 21);
                if (intPreCount > 0)
                    blockCount = BitConverter.ToInt32(byPre, 17);
            }

            for (int j = 0; j < blockCount; j++)
            {
                var blockBytes = new byte[blockSize];
                cfs.Read(blockBytes, blockSize);
                blocks.Add(blockBytes);
            }

            return blocks;
        }

        public int GetBlockPos(string blockName, int blockSize, out int blockCount)
        {
            var block = FlstBlock.FirstOrDefault(x => TBlock.GetName(x) == blockName);
            blockCount = TBlock.GetSize(block) / blockSize;
            return TBlock.GetPosition(block);
        }
    }

    public class Weighter
    {
        bool intrinsicsOn = true;
        double total = 0;
        double total_weight = 0;

        public void Reset(bool intrinsicsOn)
        {
            this.intrinsicsOn = intrinsicsOn;
            total = 0;
            total_weight = 0;
        }

        public void Add(double amount, double outof, double weight)
        {
            if (!intrinsicsOn)
                outof = 20;
            total += (amount / outof) * weight;
            total_weight += weight;
        }

        public int RatingPercentage
        {
            get
            {
                if (total_weight == 0)
                    return 0;
                return (int)(total / total_weight * 100.0);
            }
        }
    }
}
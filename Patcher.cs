using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class Patcher
    {
        public class HexPatch
        {
            public HexPatch(int offset, string hex)
            {
                this.offset = offset;
                this.hex = hex;
            }

            public int offset;
            public string hex;
        }

        public Dictionary<string, List<HexPatch>> patches = new Dictionary<string, List<HexPatch>>
        {
            { "colouredattributes", new List<HexPatch> { new HexPatch(4697073, "e8f2b40e009090"), new HexPatch(4698019, "e840b10e0091"), new HexPatch(5660904, "528b542410668b0c55f66096005ac39014c610c60fc60ec68cc50be788f7c4ffc0ffe0ffe0ffe0fe80fee0fd80fd80f480f3c0f2c2f162f160") } },
            { "idlesensitivity", new List<HexPatch> { new HexPatch(926778, "85d27507668b15de6bdd0083c2fc83fa2c0f87c4080000e81a3d480090"), new HexPatch(5534421, "79ee01"), new HexPatch(5660960, "60689c1597"), new HexPatch(5660966, "ff15387196"), new HexPatch(5660972, "85c07417684c6196"), new HexPatch(5660981, "50ff15b87096"), new HexPatch(5660988, "85c07407ff74242490ffd061c204"), new HexPatch(5661003, "90536c656570"), new HexPatch(5661010, "fe0de67098"), new HexPatch(5661016, "750ec605e67098"), new HexPatch(5661024, "216a14e818"), new HexPatch(5661032, "e95379feff9090906a40909090e8a6ffffff33dbc390909060a19c189f"), new HexPatch(5661062, "85c07524689c1597"), new HexPatch(5661071, "ff15387196"), new HexPatch(5661077, "85c0741b684c6196"), new HexPatch(5661086, "50ff15b87096"), new HexPatch(5661093, "85c0740ba39c189f"), new HexPatch(5661102, "ff742424ffd061c204"), new HexPatch(5661112, "600fb7461283c01c662b05922cae"), new HexPatch(5661127, "807f0f0f937e1a8a472ae822"), new HexPatch(5661142, "28472a8a473af6d8e815"), new HexPatch(5661155, "0410"), new HexPatch(5661158, "473ae83b"), new HexPatch(5661165, "e872"), new HexPatch(5661170, "61c3909090903c9c537e07e8be6dfaff5bc333db6a0de8b36dfaff2ad86a0de8aa6dfaff2ad86a0de8a16dfaff2ad89383c40c5bc3908a4739e8c8ffffff040d2847398a4724e8bbffffff04102847248a471ee8aeffffff041028471e8a4743e8a1ffffff0408284743c3909090909090908b461a85c074478b407185c074408b"), new HexPatch(5661300, "3b0508fa9c"), new HexPatch(5661306, "75366a02e83d6dfaff85c058752a6a04e8316dfaff"), new HexPatch(5661328, "471b"), new HexPatch(5661331, "472e6a06e8246dfaff"), new HexPatch(5661341, "4736"), new HexPatch(5661344, "473d6a08e8176dfaff"), new HexPatch(5661354, "4734"), new HexPatch(5661357, "473c83c40cc3") } },
            { "disablecdremove", new List<HexPatch> { new HexPatch(4368779, "9090909090"), new HexPatch(4383744, "9090909090") } },
            { "disablesplashscreen", new List<HexPatch> { new HexPatch(1887548, "e97203000090") } },
            { "disableunprotectedcontracts", new List<HexPatch> { new HexPatch(1199314, "68d1770000") } },
            { "sevensubs", new List<HexPatch> { new HexPatch(1503780, "c6404907c20800"), new HexPatch(1519120, "07"), new HexPatch(1526787, "c64649075ec3"), new HexPatch(1547764, "eb"), new HexPatch(1533953, "07"), new HexPatch(1540444, "07"), new HexPatch(1391244, "07"), new HexPatch(1819788, "07"), new HexPatch(4139590, "07"), new HexPatch(4155094, "eb"), /* French 7 Subs */ new HexPatch(0x1bc48c, "07") } },
            { "showstarplayers",  new List<HexPatch> { new HexPatch(374828, "9090") } },
            { "hideprivatebids", new List<HexPatch> { new HexPatch(5051539, "e90a01000090") } },
            { "allowclosewindow", new List<HexPatch> { new HexPatch(2676552, "E9E7812B000000") } },
            { "forceloadallplayers", new List<HexPatch> { new HexPatch(0x1255ff, "6683B8800000000190"), new HexPatch(0x125637, "6683B8800000000190"), new HexPatch(0x1269f1, "34080000") } },
            { "regenfixes", new List<HexPatch> { new HexPatch(0x3a6f48, "7c"), new HexPatch(0x3abeab, "e92d0500"), new HexPatch(0x3abeb0, "90") } },
            { "to1280x800", new List<HexPatch> { new HexPatch(11134, "ff04"), new HexPatch(11141, "ff04"), new HexPatch(11150, "1f03"), new HexPatch(14413, "2003"), new HexPatch(14628, "2003"), new HexPatch(14775, "2003"), new HexPatch(15106, "ff04"), new HexPatch(15205, "ff04"), new HexPatch(15326, "1f03"), new HexPatch(15425, "1f03"), new HexPatch(367138, "baeb1e60"), new HexPatch(367143, "9090"), new HexPatch(367163, "0c"), new HexPatch(376856, "08"), new HexPatch(394268, "1f03"), new HexPatch(394273, "ff04"), new HexPatch(462766, "1f03"), new HexPatch(462771, "ff04"), new HexPatch(469719, "1f03"), new HexPatch(469724, "ff04"), new HexPatch(718220, "e42b15"), new HexPatch(718448, "002b15"), new HexPatch(1438411, "1f03"), new HexPatch(1438416, "ff04"), new HexPatch(1438440, "1f03"), new HexPatch(1438445, "ff04"), new HexPatch(1438606, "1f03"), new HexPatch(1438611, "ff04"), new HexPatch(1439084, "1f03"), new HexPatch(1439089, "ff04"), new HexPatch(1439118, "1f03"), new HexPatch(1439123, "ff04"), new HexPatch(1439212, "1f03"), new HexPatch(1439217, "ff04"), new HexPatch(1440144, "e92f250a"), new HexPatch(1440149, "909081ec00020000568bf1"), new HexPatch(1440672, "e813230a"), new HexPatch(1446562, "ff04"), new HexPatch(1446569, "ff04"), new HexPatch(1446603, "1f03"), new HexPatch(1446610, "1f03"), new HexPatch(1642275, "1f03"), new HexPatch(1642283, "ff04"), new HexPatch(1716932, "e86fec05009090"), new HexPatch(1716943, "50e853ec050090"), new HexPatch(1717237, "15"), new HexPatch(1718132, "e8bfe705005090"), new HexPatch(1718142, "28"), new HexPatch(1718147, "90e8afe7050090"), new HexPatch(1718443, "15"), new HexPatch(1719502, "48e864e2050050"), new HexPatch(1719515, "90e847e2050090"), new HexPatch(1719814, "15"), new HexPatch(1720896, "e8f3dc05005090"), new HexPatch(1720907, "90e8d7dc050090"), new HexPatch(1721206, "15"), new HexPatch(1722020, "d2"), new HexPatch(1722030, "33ffebf690"), new HexPatch(1722068, "15"), new HexPatch(1722104, "15"), new HexPatch(1722154, "d2"), new HexPatch(1722163, "83ef15ebf79090"), new HexPatch(1722367, "d2"), new HexPatch(1722377, "33ffebf6"), new HexPatch(1722412, "15"), new HexPatch(1722415, "33ffebfa"), new HexPatch(1722448, "15"), new HexPatch(1722454, "eb2f"), new HexPatch(1722494, "d2"), new HexPatch(1722503, "83ef15ebf79090"), new HexPatch(1722982, "d2"), new HexPatch(1722988, "33ffebfa"), new HexPatch(1723090, "15"), new HexPatch(1723093, "33ffebfa"), new HexPatch(1723199, "15"), new HexPatch(1723208, "eb76"), new HexPatch(1723316, "d2"), new HexPatch(1723328, "83ef15ebf79090"), new HexPatch(1723670, "d2"), new HexPatch(1723676, "33ffebfa"), new HexPatch(1723778, "15"), new HexPatch(1723887, "15"), new HexPatch(1723896, "eb76"), new HexPatch(1724004, "d2"), new HexPatch(1724016, "83ef15ebf700"), new HexPatch(1931758, "2003"), new HexPatch(1931763, "0005"), new HexPatch(1934918, "2003"), new HexPatch(1934923, "0005"), new HexPatch(1934982, "2003"), new HexPatch(1934987, "0005"), new HexPatch(1983880, "10"), new HexPatch(1984274, "60"), new HexPatch(1984276, "60"), new HexPatch(1989866, "6a"), new HexPatch(1989868, "eb4290"), new HexPatch(1989872, "6a"), new HexPatch(1989874, "eb3c90"), new HexPatch(1989878, "6a"), new HexPatch(1989880, "eb3690"), new HexPatch(1989884, "6a"), new HexPatch(1989886, "eb3090"), new HexPatch(1989890, "6a"), new HexPatch(1989892, "eb2a90"), new HexPatch(1989896, "6a"), new HexPatch(1989898, "eb2490"), new HexPatch(1989902, "6a"), new HexPatch(1989904, "eb1e90"), new HexPatch(1989922, "9869c801050000ff348dac1dae00e853c10100c39090"), new HexPatch(1996623, "ada70100"), new HexPatch(1996683, "7ba70100"), new HexPatch(1996719, "4da70100"), new HexPatch(1996771, "2da70100"), new HexPatch(1996813, "f9a60100"), new HexPatch(1996838, "f4a60100"), new HexPatch(1996988, "0005"), new HexPatch(1997030, "0005"), new HexPatch(1997038, "2003"), new HexPatch(1997682, "69c200050000"), new HexPatch(1997693, "909090"), new HexPatch(1997786, "69c000050000"), new HexPatch(1997797, "909090"), new HexPatch(1997879, "69c000050000"), new HexPatch(1997890, "909090"), new HexPatch(1997993, "69c3000500008b5c2414"), new HexPatch(1998008, "909090"), new HexPatch(1998185, "69c000050000"), new HexPatch(1998195, "909090"), new HexPatch(1998296, "69c300050000"), new HexPatch(1998306, "909090"), new HexPatch(1998394, "69c300050000"), new HexPatch(1998404, "909090"), new HexPatch(1998513, "69c000050000"), new HexPatch(1998523, "909090"), new HexPatch(1999605, "2003"), new HexPatch(1999621, "2003"), new HexPatch(1999626, "0005"), new HexPatch(2021291, "60"), new HexPatch(2021293, "60"), new HexPatch(2023926, "ff04"), new HexPatch(2023947, "1f03"), new HexPatch(2036936, "1f03"), new HexPatch(2036944, "ff04"), new HexPatch(2104905, "9090908b44240483f85a7f0869c05e0100"), new HexPatch(2104923, "eb0e83e85a69c09f01000005007f0000c1f808c2040090e821000000c38b44240469c055010000c1f808c2040052b8ffffffbff764240892405ac20400608d74242c8bfe6a0259ad50e8a3ffffffabad50e8c7ffffffabe2ee61c39090e8dbffffffa10c29ae00c390e8cfffffff668b91a0e91200f644240402740b837c2430027d04ff442430e9b0daf5ff"), new HexPatch(2105067, "06051e1006051e1006051e10050101050101050101e86dffffffe9e63efeffe863ffffffe96c4efeffe859ffffffe9321cfeffe84fffffffe9880ffeff60936a155b99f7fb408944241c61c3905393e8e9ffffff5bc390608d7424288bfe6a0259ad83c035abad50e820ffffffabe2f161668b91a0e912"), new HexPatch(2105187, "e92fdaf5ff81c40c020000e825ffffff668b6c240481ec0c020000c390"), new HexPatch(2105715, "9060ff742428ff742428e84e39feff5a5a69c02003000099f7"), new HexPatch(2105741, "f3795d"), new HexPatch(2105746, "44241c61c39060ff742428ff742428e82a39feff5a5a0faf05f3795d"), new HexPatch(2105775, "99b9200300"), new HexPatch(2105781, "f7f98944241c61c3909090"), new HexPatch(2676949, "0005"), new HexPatch(2676960, "2003"), new HexPatch(3248966, "1f03"), new HexPatch(3529745, "1f03"), new HexPatch(3531116, "02"), new HexPatch(3705395, "02"), new HexPatch(3705478, "02"), new HexPatch(3706895, "ab04"), new HexPatch(4196504, "e8cb16e0ff909090"), new HexPatch(4230775, "1f03"), new HexPatch(4230780, "ff04"), new HexPatch(4308242, "1f03"), new HexPatch(4308247, "ff04"), new HexPatch(4314845, "15"), new HexPatch(4332829, "5068251d8200eb06060106010601"), new HexPatch(4332851, "06"), new HexPatch(4333410, "05"), new HexPatch(4335005, "50b8f71e60009090"), new HexPatch(4335017, "90"), new HexPatch(4335027, "09"), new HexPatch(4335678, "06"), new HexPatch(4656334, "8b"), new HexPatch(4656983, "8b"), new HexPatch(4673762, "81"), new HexPatch(4834854, "02"), new HexPatch(4834974, "02"), new HexPatch(4835132, "02"), new HexPatch(4910804, "2003"), new HexPatch(4910809, "0005"), new HexPatch(4910844, "2003"), new HexPatch(4910849, "0005"), new HexPatch(4954740, "1f03"), new HexPatch(4954748, "ff04"), new HexPatch(4958808, "1f03"), new HexPatch(4958816, "ff04"), new HexPatch(6043424, "626b6731323830"), new HexPatch(6043432, "383030"), new HexPatch(6043436, "72676e"), new HexPatch(6382665, "3830302e6d627200"), new HexPatch(6662093, "383030") } },
            { "to1280x800part2", new List<HexPatch> { new HexPatch(1235195, "355f"), new HexPatch(2111540, "81ec200200"), new HexPatch(2111546, "5355565751b9ec04000083c8ffbf78f19c"), new HexPatch(2111564, "f3ab6a1a59bf9c3cb600f3ab59a19423ae"), new HexPatch(2111582, "33db33f63bc3") } },
            { "findallplayers", new List<HexPatch> { new HexPatch(0x3afc4b, "e99e00"), new HexPatch(0x3afc50, "90") } },
            { "jobsabroadboost", new List<HexPatch> { new HexPatch(0x29d315, "eb"), new HexPatch(0x29d665, "eb"), new HexPatch(0x29d6e4, "eb"), new HexPatch(0x29ea7e, "eb") } }
        };

        byte[] HexStringToBytes(string hexString)
        {
            byte[] ret = new byte[hexString.Length / 2];
            hexString = hexString.ToLower();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                ret[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return ret;
        }

        public IEnumerable<HexPatch> LoadPatchFile(string patchFile)
        {
            var patchList = new List<HexPatch>();
            using (var sr = new StreamReader(patchFile))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (string.IsNullOrEmpty(line) || line.StartsWith("/"))
                        continue;
                    var parts = line.Split(' ');
                    if (parts.Length != 3)
                        continue;
                    parts[0] = parts[0].Replace(":", "");
                    var offset = Convert.ToInt32(parts[0], 16);
                    var from = Convert.ToByte(parts[1], 16);
                    var to = Convert.ToByte(parts[2], 16);
                    patchList.Add(new HexPatch(offset, string.Format("{0:x02}", to)));
                }
            }
            return patchList;
        }

        public void ApplyPatch(string fileName, IEnumerable<HexPatch> patch)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    foreach (var hexpatch in patch)
                    {
                        bw.Seek(hexpatch.offset, SeekOrigin.Begin);
                        bw.Write(HexStringToBytes(hexpatch.hex));
                    }
                }
            }
        }

        public void CurrencyInflationChanger(string fileName, double multiplier)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var br = new BinaryReader(file))
                {
                    // Check if it's been applied before, if not and multiplier == 1 - then do nothing
                    if (multiplier == 1.0)
                    {
                        file.Seek(0x5196C1, SeekOrigin.Begin);
                        if (br.ReadByte() == 0x90)
                            return;
                    }

                    // Write Multiplier
                    using (var bw = new BinaryWriter(file))
                    {
                        file.Seek(0x5196C1, SeekOrigin.Begin);
                        bw.Write(multiplier);
                    }
                }
            }

            var patch = new HexPatch(0x566A00, "FF74E40468146A96005589E583E4F8E9E28DADFFDD05C1969100DC0DB89CAD00DD1DB89CAD0083C404C3");
            var jmpPatch = new HexPatch(0x3F7F0, "E90B72520090");
            ApplyPatch(fileName, new HexPatch[] { patch, jmpPatch });
        }

        public void SpeedHack(string fileName, short speed)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    file.Seek(0x5472ce, SeekOrigin.Begin);
                    bw.Write(speed);
                }
            }
        }

        public bool CheckForV3968(string fileName)
        {
            bool ret = false;
            try
            {
                using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var br = new BinaryReader(file))
                    {
                        file.Seek(0x6d4394, SeekOrigin.Begin);
                        byte[] ver = new byte[6];
                        br.Read(ver, 0, 6);
                        if (Encoding.ASCII.GetString(ver) == "3.9.68")
                            ret = true;
                    }
                }
            }
            catch
            {

            }
            return ret;
        }
    }
}

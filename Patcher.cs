using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public class Patcher
    {
        public class HexPatch
        {
            public HexPatch(string command, string part1, string part2, string part3 = null, string part4 = null, string part5 = null, string part6 = null)
            {
                this.offset = -1;
                this.command = command;
                this.part1 = part1;
                this.part2 = part2;
                this.part3 = part3;
                this.part4 = part4;
                this.part5 = part5;
                this.part6 = part6;
            }

            public HexPatch(int offset, string hex, string oldhex = null)
            {
                this.offset = offset;
                this.hex = hex;
                this.oldhex = oldhex;
            }

            public int offset;
            public string hex;
            public string oldhex;
            public string command;
            public string part1;
            public string part2;
            public string part3;
            public string part4;
            public string part5;
            public string part6;
        }

        public Dictionary<string, List<HexPatch>> patches = new Dictionary<string, List<HexPatch>>
        {
            { "colouredattributes", new List<HexPatch> { new HexPatch(4697073, "e8f2b40e009090"), new HexPatch(4698019, "e840b10e0091"), new HexPatch(5660904, "528b542410668b0c55f66096005ac39014c610c60fc60ec68cc50be788f7c4ffc0ffe0ffe0ffe0fe80fee0fd80fd80f480f3c0f2c2f162f1") } },
            { "idlesensitivity", new List<HexPatch> { new HexPatch(926778, "85d27507668b15de6bdd0083c2fc83fa2c0f87c4080000e81a3d480090"), new HexPatch(5534421, "79ee01"), new HexPatch(5660960, "60689c1597"), new HexPatch(5660966, "ff15387196"), new HexPatch(5660972, "85c07417684c6196"), new HexPatch(5660981, "50ff15b87096"), new HexPatch(5660988, "85c07407ff74242490ffd061c204"), new HexPatch(5661003, "90536c656570"), new HexPatch(5661010, "fe0de67098"), new HexPatch(5661016, "750ec605e67098"), new HexPatch(5661024, "216a14e818"), new HexPatch(5661032, "e95379feff9090906a60909090e8a6ffffff33dbc390909060a19c189f"), new HexPatch(5661062, "85c07524689c1597"), new HexPatch(5661071, "ff15387196"), new HexPatch(5661077, "85c0741b684c6196"), new HexPatch(5661086, "50ff15b87096"), new HexPatch(5661093, "85c0740ba39c189f"), new HexPatch(5661102, "ff742424ffd061c204"), new HexPatch(5661112, "600fb7461283c01c662b05922cae"), new HexPatch(5661127, "807f0f0f937e1a8a472ae822"), new HexPatch(5661142, "28472a8a473af6d8e815"), new HexPatch(5661155, "0410"), new HexPatch(5661158, "473ae83b"), new HexPatch(5661165, "e872"), new HexPatch(5661170, "61c3909090903c9c537e07e8be6dfaff5bc333db6a0de8b36dfaff2ad86a0de8aa6dfaff2ad86a0de8a16dfaff2ad89383c40c5bc3908a4739e8c8ffffff040d2847398a4724e8bbffffff04102847248a471ee8aeffffff041028471e8a4743e8a1ffffff0408284743c3909090909090908b461a85c074478b407185c074408b"), new HexPatch(5661300, "3b0508fa9c"), new HexPatch(5661306, "75366a02e83d6dfaff85c058752a6a04e8316dfaff"), new HexPatch(5661328, "471b"), new HexPatch(5661331, "472e6a06e8246dfaff"), new HexPatch(5661341, "4736"), new HexPatch(5661344, "473d6a08e8176dfaff"), new HexPatch(5661354, "4734"), new HexPatch(5661357, "473c83c40cc3") } },
            { "idlesensitivitytransferscreen", new List<HexPatch> { new HexPatch(5162819, "e9339d070090"), new HexPatch(5661819, "e8f0fcffff0fbfc80fbfd7e9be62f8ff") } },
            { "disablecdremove", new List<HexPatch> { new HexPatch(4368779, "9090909090"), new HexPatch(4383744, "9090909090") } },
            { "disablesplashscreen", new List<HexPatch> { new HexPatch(1887548, "e97203000090") } },
            { "disableunprotectedcontracts", new List<HexPatch> { new HexPatch(1199314, "68d1770000") } },
            { "sevensubs", new List<HexPatch> { new HexPatch(1503780, "c6404907c20800"), new HexPatch(1519120, "07"), new HexPatch(1526787, "c64649075ec3"), new HexPatch(1547764, "eb"), new HexPatch(1533953, "07"), new HexPatch(1540444, "07"), new HexPatch(1391244, "07"), new HexPatch(1819788, "07"), new HexPatch(4139590, "07"), new HexPatch(4155094, "eb"), /* French 7 Subs */ new HexPatch(0x1bc48c, "07"), /* FA Tropy */ new HexPatch(0x170C6B, "66C746490503"), /* Charity Shield */ new HexPatch(0x16d3f0, "06"), new HexPatch(0x16C474, "c6404907c20800") } },
            { "showstarplayers",  new List<HexPatch> { new HexPatch(374828, "9090") } },
            { "hideprivatebids", new List<HexPatch> { new HexPatch(5051539, "e90a01000090") } },
            { "allowclosewindow", new List<HexPatch> { new HexPatch(2676552, "E9E7812B000000") } },
            { "forceloadallplayers", new List<HexPatch> { new HexPatch(0x1255ff, "6683B8800000000190"), new HexPatch(0x125637, "6683B8800000000190"), new HexPatch(0x1269f1, "34080000") } },
            { "regenfixes", new List<HexPatch> { new HexPatch(0x3ad1bf, "2d"), new HexPatch(0x3abeab, "e92d0500"), new HexPatch(0x3abeb0, "90"), new HexPatch(0x3AD089, "00"), new HexPatch(0x3AD090, "00") } },
            { "to1280x800", new List<HexPatch> { new HexPatch(11134, "ff04"), new HexPatch(11141, "ff04"), new HexPatch(11150, "1f03"), new HexPatch(14413, "2003"), new HexPatch(14628, "2003"), new HexPatch(14775, "2003"), new HexPatch(15106, "ff04"), new HexPatch(15205, "ff04"), new HexPatch(15326, "1f03"), new HexPatch(15425, "1f03"), new HexPatch(367138, "baeb1e60"), new HexPatch(367143, "9090"), new HexPatch(367163, "0c"), new HexPatch(376856, "08"), new HexPatch(394268, "1f03"), new HexPatch(394273, "ff04"), new HexPatch(462766, "1f03"), new HexPatch(462771, "ff04"), new HexPatch(469719, "1f03"), new HexPatch(469724, "ff04"), new HexPatch(718220, "e42b15"), new HexPatch(718448, "002b15"), new HexPatch(1438411, "1f03"), new HexPatch(1438416, "ff04"), new HexPatch(1438440, "1f03"), new HexPatch(1438445, "ff04"), new HexPatch(1438606, "1f03"), new HexPatch(1438611, "ff04"), new HexPatch(1439084, "1f03"), new HexPatch(1439089, "ff04"), new HexPatch(1439118, "1f03"), new HexPatch(1439123, "ff04"), new HexPatch(1439212, "1f03"), new HexPatch(1439217, "ff04"), new HexPatch(1440144, "e92f250a"), new HexPatch(1440149, "909081ec00020000568bf1"), new HexPatch(1440672, "e813230a"), new HexPatch(1446562, "ff04"), new HexPatch(1446569, "ff04"), new HexPatch(1446603, "1f03"), new HexPatch(1446610, "1f03"), new HexPatch(1642275, "1f03"), new HexPatch(1642283, "ff04"), new HexPatch(1716932, "e86fec05009090"), new HexPatch(1716943, "50e853ec050090"), new HexPatch(1717237, "15"), new HexPatch(1718132, "e8bfe705005090"), new HexPatch(1718142, "28"), new HexPatch(1718147, "90e8afe7050090"), new HexPatch(1718443, "15"), new HexPatch(1719502, "48e864e2050050"), new HexPatch(1719515, "90e847e2050090"), new HexPatch(1719814, "15"), new HexPatch(1720896, "e8f3dc05005090"), new HexPatch(1720907, "90e8d7dc050090"), new HexPatch(1721206, "15"), new HexPatch(1722020, "d2"), new HexPatch(1722030, "33ffebf690"), new HexPatch(1722068, "15"), new HexPatch(1722104, "15"), new HexPatch(1722154, "d2"), new HexPatch(1722163, "83ef15ebf79090"), new HexPatch(1722367, "d2"), new HexPatch(1722377, "33ffebf6"), new HexPatch(1722412, "15"), new HexPatch(1722415, "33ffebfa"), new HexPatch(1722448, "15"), new HexPatch(1722454, "eb2f"), new HexPatch(1722494, "d2"), new HexPatch(1722503, "83ef15ebf79090"), new HexPatch(1722982, "d2"), new HexPatch(1722988, "33ffebfa"), new HexPatch(1723090, "15"), new HexPatch(1723093, "33ffebfa"), new HexPatch(1723199, "15"), new HexPatch(1723208, "eb76"), new HexPatch(1723316, "d2"), new HexPatch(1723328, "83ef15ebf79090"), new HexPatch(1723670, "d2"), new HexPatch(1723676, "33ffebfa"), new HexPatch(1723778, "15"), new HexPatch(1723887, "15"), new HexPatch(1723896, "eb76"), new HexPatch(1724004, "d2"), new HexPatch(1724016, "83ef15ebf700"), new HexPatch(1931758, "2003"), new HexPatch(1931763, "0005"), new HexPatch(1934918, "2003"), new HexPatch(1934923, "0005"), new HexPatch(1934982, "2003"), new HexPatch(1934987, "0005"), new HexPatch(1983880, "10"), new HexPatch(1984274, "60"), new HexPatch(1984276, "60"), new HexPatch(1989866, "6a"), new HexPatch(1989868, "eb4290"), new HexPatch(1989872, "6a"), new HexPatch(1989874, "eb3c90"), new HexPatch(1989878, "6a"), new HexPatch(1989880, "eb3690"), new HexPatch(1989884, "6a"), new HexPatch(1989886, "eb3090"), new HexPatch(1989890, "6a"), new HexPatch(1989892, "eb2a90"), new HexPatch(1989896, "6a"), new HexPatch(1989898, "eb2490"), new HexPatch(1989902, "6a"), new HexPatch(1989904, "eb1e90"), new HexPatch(1989922, "9869c801050000ff348dac1dae00e853c10100c39090"), new HexPatch(1996623, "ada70100"), new HexPatch(1996683, "7ba70100"), new HexPatch(1996719, "4da70100"), new HexPatch(1996771, "2da70100"), new HexPatch(1996813, "f9a60100"), new HexPatch(1996838, "f4a60100"), new HexPatch(1996988, "0005"), new HexPatch(1997030, "0005"), new HexPatch(1997038, "2003"), new HexPatch(1997682, "69c200050000"), new HexPatch(1997693, "909090"), new HexPatch(1997786, "69c000050000"), new HexPatch(1997797, "909090"), new HexPatch(1997879, "69c000050000"), new HexPatch(1997890, "909090"), new HexPatch(1997993, "69c3000500008b5c2414"), new HexPatch(1998008, "909090"), new HexPatch(1998185, "69c000050000"), new HexPatch(1998195, "909090"), new HexPatch(1998296, "69c300050000"), new HexPatch(1998306, "909090"), new HexPatch(1998394, "69c300050000"), new HexPatch(1998404, "909090"), new HexPatch(1998513, "69c000050000"), new HexPatch(1998523, "909090"), new HexPatch(1999605, "2003"), new HexPatch(1999621, "2003"), new HexPatch(1999626, "0005"), new HexPatch(2021291, "60"), new HexPatch(2021293, "60"), new HexPatch(2023926, "ff04"), new HexPatch(2023947, "1f03"), new HexPatch(2036936, "1f03"), new HexPatch(2036944, "ff04"), new HexPatch(2104905, "9090908b44240483f85a7f0869c05e0100"), new HexPatch(2104923, "eb0e83e85a69c09f01000005007f0000c1f808c2040090e821000000c38b44240469c055010000c1f808c2040052b8ffffffbff764240892405ac20400608d74242c8bfe6a0259ad50e8a3ffffffabad50e8c7ffffffabe2ee61c39090e8dbffffffa10c29ae00c390e8cfffffff668b91a0e91200f644240402740b837c2430027d04ff442430e9b0daf5ff"), new HexPatch(2105067, "06051e1006051e1006051e10050101050101050101e86dffffffe9e63efeffe863ffffffe96c4efeffe859ffffffe9321cfeffe84fffffffe9880ffeff60936a155b99f7fb408944241c61c3905393e8e9ffffff5bc390608d7424288bfe6a0259ad83c035abad50e820ffffffabe2f161668b91a0e912"), new HexPatch(2105187, "e92fdaf5ff81c40c020000e825ffffff668b6c240481ec0c020000c390"), new HexPatch(2105715, "9060ff742428ff742428e84e39feff5a5a69c02003000099f7"), new HexPatch(2105741, "f3795d"), new HexPatch(2105746, "44241c61c39060ff742428ff742428e82a39feff5a5a0faf05f3795d"), new HexPatch(2105775, "99b9200300"), new HexPatch(2105781, "f7f98944241c61c3909090"), new HexPatch(2676949, "0005"), new HexPatch(2676960, "2003"), new HexPatch(3248966, "1f03"), new HexPatch(3529745, "1f03"), new HexPatch(3531116, "02"), new HexPatch(3705395, "02"), new HexPatch(3705478, "02"), new HexPatch(3706895, "ab04"), new HexPatch(4196504, "e8cb16e0ff909090"), new HexPatch(4230775, "1f03"), new HexPatch(4230780, "ff04"), new HexPatch(4308242, "1f03"), new HexPatch(4308247, "ff04"), new HexPatch(4314845, "15"), new HexPatch(4332829, "5068251d8200eb06060106010601"), new HexPatch(4332851, "06"), new HexPatch(4333410, "05"), new HexPatch(4335005, "50b8f71e60009090"), new HexPatch(4335017, "90"), new HexPatch(4335027, "09"), new HexPatch(4335678, "06"), new HexPatch(4656334, "8b"), new HexPatch(4656983, "8b"), new HexPatch(4673762, "81"), new HexPatch(4834854, "02"), new HexPatch(4834974, "02"), new HexPatch(4835132, "02"), new HexPatch(4910804, "2003"), new HexPatch(4910809, "0005"), new HexPatch(4910844, "2003"), new HexPatch(4910849, "0005"), new HexPatch(4954740, "1f03"), new HexPatch(4954748, "ff04"), new HexPatch(4958808, "1f03"), new HexPatch(4958816, "ff04"), new HexPatch(6043424, "626b6731323830"), new HexPatch(6043432, "383030"), new HexPatch(6043436, "72676e"), new HexPatch(6382665, "3830302e6d627200"), new HexPatch(6662093, "383030") } },
            { "tapanispacemaker", new List<HexPatch> { new HexPatch(1235195, "355f"), new HexPatch(2111540, "81ec200200"), new HexPatch(2111546, "5355565751b9ec04000083c8ffbf78f19c"), new HexPatch(2111564, "f3ab6a1a59bf9c3cb600f3ab59a19423ae"), new HexPatch(2111582, "33db33f63bc3") } },
            { "findallplayers", new List<HexPatch> { new HexPatch(0x3afc4b, "e99e00"), new HexPatch(0x3afc50, "90") } },
            { "jobsabroadboost", new List<HexPatch> { new HexPatch(0x29ea36, "eb"), new HexPatch(0x29d315, "eb"), new HexPatch(0x29d665, "eb"), new HexPatch(0x29d6e4, "eb"), new HexPatch(0x29ea7e, "eb") } },
            { "tapaninewregencode", new List<HexPatch> { new HexPatch(2105632, "608b0d6c23ae008b35c423ae0033c00fb6560703c283c63ee2f599f7356c23ae00a2e673980061c3"), new HexPatch(2106524, "e87ffcffffa0e673980084c074f2c3"), new HexPatch(2106552, "608b6c243055ff742430ff742430ff7424308a1c2fe8b40000000fb6142f3ad374208b44242c483ac27517526a64e8150000"), new HexPatch(2106603, "5a3b4424247d08e82f0000"), new HexPatch(2106615, "88042f61c210009090"), new HexPatch(3854240, "e85b57e5ff56e815bcd8ff83c408eb04"), new HexPatch(3854258, "88015e"), new HexPatch(2106624, "6905306cad006d4ec64105393000"), new HexPatch(2106639, "a3306cad0033d2c1e81066f77424040fb7c2c20400909060526a02e8d1ffffffd1e05a488bd86a00594180f9147d25515268e80300"), new HexPatch(2106693, "e8b6ffffff5a3d760300007d0ee2eb03d380fa017e0580fa147cd7598954241c61c39090"), new HexPatch(2106753, "9090909090608b6c24308a142f3a54242c7c173a5424287f116a64e81faa30005a3b4424247703fe0c2f61c21000900fbe142f03d083fa7d7c02b27c88142fc3"), new HexPatch(2106818, "0fbe142f2bd083fa837f02b28388142fc3905d6a006a346a2e6a1b6a406a366a2d6a276a266a336a2b6a386a256a376a436a316a426a446a396a326a3e6a1e6a246a1d6a3a6a356a2a6a21ffe590609384db75156a0ae8a3a930"), new HexPatch(2106909, "936a0be89ba930004383c40802d86a095933d2526a16e888a9300083c4045a3ac37f0142e2edd1e242526a09e872a9300083c4045a3ad37d073c057d0142eb043c057df98954241c61c3909090"), new HexPatch(2106992, "ff356423ae00e845a930006bc06e5a0305bc23ae003878187f118b706185f6740a56e809"), new HexPatch(2107029, "000085d87504e2d333f6c38b442404608d700f33d26a0759acd1e23c127c0142e2f68954241c61c204"), new HexPatch(2107071, "906a0158600fb746070fb75f072bc33bc2720cf7d83bc2720633c08944241c61c3526a64e8d8a830005a5a3bc27d0b8a042fe81affffff88042fc390"), new HexPatch(2107135, "90608b7e6185ff750261c3807f07787c466a7f5966bbff32e854ffffff85f674e86a325ae898ffffff4875e58a56238857238a56288857288a56308857308b56388957388b57418957416a0c59578d760f8d7f0ff3a45f57e844ffffff93e872feffff5d85ed7442b900400000b719e8fdfeffff85f674eb6a195ae841ffffff85c074eb56e817ffffff3ac3b0037502040250e829a830"), new HexPatch(2107287, "5a408a142e88142f5d85ed74054875f2ebbe6a06e850fdffffe85bfeffff88472f6a2f6a076a146a2be8c1fdffff6a065ae859fdffff8847218ac3b20542d0e072fb75fae846fdffff8847426a2e5d6a465ae8f2feffff6a2e6a0c6a146a42e8bdfcffff6a2d5d6a245ae8dafeffff6a2d6a096a146a3de8a5fcffff6a3d5d8a042f3c087f086a505ae8bbfeffff6a3d6a0d6a146a56e886fcffff6a345d8a042f3c097f086a505ae89cfeffff6a346a0f6a146a1be867fcffff6a2c5db20ae8cbfcffff88042f6a165ae87afeffff90906a1e5d6a03e84ea730005a0401e848fdffff6a245d6a025a803c2fd07c0380c20552e871fcffff9090e82cfdffff6a405d6a205ae83ffeffff6a406a0e6a146a4ee80afcffff6a235d803c2fe87f0c6a18e842fcffffe8edfcffff6a05e836fcffffe8f3fcffff6a426a076a14906a1b6a0f6a146a1fe8d5fbffff6a1d6a0d6a146a17e8c8fbffff9090906a366a0f6a146a22e8b8fbffff6a435d6a03e8b6a630005ae8a0fcffff6a3f5d8a042f3c077d08e8f1fcffff88042f6a0c5ae8fcfbffff8847446a58e884fbffff90807f0f027c430fb65707c1ea0442e8defbffff8847446a08e8aefbffff85c0750c8a572a8a473a88472a88573a6a04e897fbffff02d0e8b6fbffff884740e898fcffff88471c90eb266a2a5d6a02e878fbffffe835fcffff6a355de82dfcffff6a3a5d6a03e861fbffffe81efcffffe8f8faffff041e38470772336a32e849fbffff85c07435e8e1faffff043c384707721c6a19e832fbffff85c0741ee8cafaffff044a3847077707b0be38470772636a05e814fbffff85c075586a1b5db9"), new HexPatch(2107893, "080000b71ee871fcffff85f6740d8a570780ea0f3856077707ebea6a0158eb126a05e8e4faffff408a142e38142f7f0388142f4583fd2d74fa83fd2574f583fd457d8f487fe26a02e8befaffff85c075e2ebabe854faffff384707777a6a19e8a7faffff85c07417e83ffaffff2c1e38470877636a0ee890faffff85c075586a1b5db9"), new HexPatch(2108025, "080000b71ee8edfbffff85f6740d8a570780c2383856077207ebea6a0158eb136a05e860faffff408a142e38142f7c0388142f4583fd2d74fa83fd2574f583fd457d96487fe26a02e83afaffff85c075e2ebab61c3") } },
            { "transferwindowpatch", new List<HexPatch> { new HexPatch(42666, "66c74002ff1390"), new HexPatch(42676, "05"), new HexPatch(42693, "66c74002ff0a90"), new HexPatch(42703, "08"), new HexPatch(42719, "66c74002ff19908858"), new HexPatch(42729, "90"), new HexPatch(42748, "66c74002ff1890"), new HexPatch(42758, "01"), new HexPatch(43455, "eb"), new HexPatch(43677, "48a1"), new HexPatch(77013, "18"), new HexPatch(77030, "02"), new HexPatch(77141, "6a02598908c740"), new HexPatch(77149, "ff1c0601894806c74008ff130900b50189480cc7400eff04"), new HexPatch(77174, "01894812c7"), new HexPatch(77180, "14"), new HexPatch(77182, "010100c646130690909090909090"), new HexPatch(150539, "18"), new HexPatch(150543, "02"), new HexPatch(150659, "c60003885801c64002ffc6400301c6400406c640"), new HexPatch(150680, "018b460483c006c600"), new HexPatch(150690, "885801c64002ffc6400301c64004088858058b4604c6400c0383c00cc64001"), new HexPatch(150722, "c640"), new HexPatch(150725, "ffc6400301885804c64005018b460483c0125ec60003c6400101"), new HexPatch(150753, "02ff"), new HexPatch(150757, "0301c64004018858055b81c4"), new HexPatch(150770, "020000c39090909090909090909090909090909090909090909090909090909090909090909090909090909090909090909090909090"), new HexPatch(150968, "eb"), new HexPatch(151917, "e989000000"), new HexPatch(152003, "20"), new HexPatch(152131, "20"), new HexPatch(152189, "20"), new HexPatch(152317, "70"), new HexPatch(258357, "18"), new HexPatch(258374, "02"), new HexPatch(258485, "6a04598908c74002ff1c0001894806c74008ff140300b50189480cc7400eff140501894812c74014ff130600"), new HexPatch(258530, "461305c64613329090"), new HexPatch(1148245, "18"), new HexPatch(1148262, "02"), new HexPatch(1148369, "6a05598908c74002ff010601894806c74008ff010800b5"), new HexPatch(1148393, "89480cc7400eff120001894812c74014ff0f0100909090909090"), new HexPatch(1547141, "18"), new HexPatch(1547158, "02"), new HexPatch(1547265, "6a07598908c74002ff0106"), new HexPatch(1547277, "89"), new HexPatch(1547279, "06c7"), new HexPatch(1547282, "08ff010800b50189480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(1743269, "18"), new HexPatch(1743286, "02"), new HexPatch(1743393, "6a08598908c74002ff0c0101894806c74008ff050400b5"), new HexPatch(1743417, "89480cc7400eff050701894812c74014ff020800909090909090"), new HexPatch(1840752, "ff"), new HexPatch(1840756, "0166c740040601"), new HexPatch(1840779, "ff"), new HexPatch(1840783, "01"), new HexPatch(1840787, "08"), new HexPatch(1840803, "66c74002ff01"), new HexPatch(1840829, "66c74002ff1f90"), new HexPatch(1840859, "ff"), new HexPatch(1840863, "01"), new HexPatch(1840867, "00"), new HexPatch(1840887, "c7"), new HexPatch(1840891, "ff010190909090"), new HexPatch(1966222, "01"), new HexPatch(1966226, "08"), new HexPatch(1966248, "8858"), new HexPatch(1966251, "90"), new HexPatch(1966277, "66c7400301015b81c4"), new HexPatch(1966287, "020000c3"), new HexPatch(1966450, "eb"), new HexPatch(1966498, "eb"), new HexPatch(1967325, "eb529090"), new HexPatch(1967415, "90ce9c"), new HexPatch(1967477, "e8aa9e"), new HexPatch(1967539, "e8aa9e"), new HexPatch(1967602, "70"), new HexPatch(2019243, "66c74002ff0190"), new HexPatch(2019253, "06"), new HexPatch(2019275, "ff"), new HexPatch(2019279, "01"), new HexPatch(2019283, "08"), new HexPatch(2019320, "48"), new HexPatch(2019337, "01"), new HexPatch(2070245, "18"), new HexPatch(2070262, "02"), new HexPatch(2070369, "6a0c598908c74002ff010601894806c74008ff010800b50189480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(2343525, "18"), new HexPatch(2343542, "02"), new HexPatch(2343649, "6a0d598908c74002ff010b01894806c74008ff160100b5"), new HexPatch(2343673, "89480cc7400eff010601894812c74014ff1f06009090909090"), new HexPatch(2495643, "ff"), new HexPatch(2495647, "01"), new HexPatch(2495651, "08"), new HexPatch(2495673, "01"), new HexPatch(2495699, "1F"), new HexPatch(2495701, "58"), new HexPatch(2495756, "1f"), new HexPatch(2495760, "02"), new HexPatch(2518222, "ff"), new HexPatch(2518226, "098858"), new HexPatch(2518230, "90"), new HexPatch(2518252, "66c740030103"), new HexPatch(2518277, "ff66c740031b0590"), new HexPatch(2518311, "1b"), new HexPatch(2518315, "06"), new HexPatch(2549381, "18"), new HexPatch(2549398, "02"), new HexPatch(2549505, "6a1b598908c74002ff050001894806c74008ff1c0200b50189480cc7400eff1e05"), new HexPatch(2549539, "894812c74014ff1d060090909090"), new HexPatch(3749669, "18"), new HexPatch(3749686, "02"), new HexPatch(3749793, "6a10598908c74002ff010601894806c74008ff010800b50189480cc7400eff010001894812c74014ff01010090909090909090"), new HexPatch(3753285, "18"), new HexPatch(3753302, "02"), new HexPatch(3753409, "6a11598908c74002ff080001894806c74008ff1f0200b5"), new HexPatch(3753433, "89480cc7400eff160601894812c74014ff120700909090909090"), new HexPatch(3753543, "e9cf00"), new HexPatch(3753548, "90"), new HexPatch(3754322, "20a098"), new HexPatch(3754454, "209f98"), new HexPatch(3987589, "18"), new HexPatch(3987606, "02"), new HexPatch(3987713, "6a12598908c74002ff010601894806c74008ff010800b5"), new HexPatch(3987737, "89480cc7400eff010101894812c74014ff010200909090909090"), new HexPatch(4013703, "06"), new HexPatch(4013723, "66c74003010890"), new HexPatch(4013751, "018858"), new HexPatch(4013755, "90"), new HexPatch(4013781, "8848"), new HexPatch(4013784, "8848045b81c4"), new HexPatch(4013791, "020000c3"), new HexPatch(4114546, "09"), new HexPatch(4114550, "05"), new HexPatch(4114568, "66c74002ff0190"), new HexPatch(4114578, "08"), new HexPatch(4114598, "ff"), new HexPatch(4114602, "1b"), new HexPatch(4114606, "00"), new HexPatch(4114630, "66c740031a0190"), new HexPatch(4154341, "18"), new HexPatch(4154358, "02"), new HexPatch(4154465, "6a15598908c74002ff"), new HexPatch(4154475, "0601894806c74008ff010800b5"), new HexPatch(4154489, "89480cc7"), new HexPatch(4154494, "0eff010001894812c74014ff010100909090909090"), new HexPatch(4560126, "ff"), new HexPatch(4560130, "01"), new HexPatch(4560134, "06"), new HexPatch(4560156, "ff"), new HexPatch(4560160, "01"), new HexPatch(4560164, "08"), new HexPatch(4560187, "01"), new HexPatch(4560191, "00"), new HexPatch(4560203, "48"), new HexPatch(4560220, "01"), new HexPatch(5056659, "390eec"), new HexPatch(5057798, "e617ec"), new HexPatch(5251317, "18"), new HexPatch(5251334, "02"), new HexPatch(5251441, "6a18598908c74002ff010601894806c74008ff010800b5"), new HexPatch(5251465, "89480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(5265686, "ff"), new HexPatch(5265691, "04"), new HexPatch(5265717, "ff"), new HexPatch(5265722, "04"), new HexPatch(5290389, "18"), new HexPatch(5290406, "02"), new HexPatch(5290513, "6a19598908c74002ff12010189"), new HexPatch(5290527, "06c74008ff0b0400b50189480cc7400eff0406"), new HexPatch(5290547, "894812c74014ff03070090909090"), new HexPatch(5304533, "18"), new HexPatch(5304550, "02"), new HexPatch(5304657, "6a1a598908c74002ff010601894806c74008ff010800b50189480cc7400eff0100"), new HexPatch(5304691, "894812c74014ff0101009090909090"), new HexPatch(6082390, "4a616e756172792e00") } },
/*SPECIAL*/ { "transferwindowpatchdetect", new List<HexPatch> { new HexPatch(42666, "66c74002ff1390") } },
            { "manageanyteam", new List<HexPatch> { new HexPatch(0x82a74, "909090909090"), new HexPatch(435031, "9090909090"), new HexPatch(535710, "8b74241c368b86cf00000085c07557eb099090"), new HexPatch(535734, "744c"), new HexPatch(0x1448aa, "0075") } },
            { "remove3playerlimit", new List<HexPatch> { new HexPatch(0x179c65, "01") } },
            { "englishleaguenorthawards", new List<HexPatch> { new HexPatch(1586886, "e1"), new HexPatch(2178304, "9090c705"), new HexPatch(2178312, "5c0100"), new HexPatch(2178316, "90"), new HexPatch(2178333, "9090c705"), new HexPatch(2178341, "5d0100"), new HexPatch(2178345, "90"), new HexPatch(2178362, "9090c705"), new HexPatch(2178370, "5e0100"), new HexPatch(2178374, "90"), new HexPatch(2178391, "9090c705"), new HexPatch(2178399, "5f0100"), new HexPatch(2178403, "90"), new HexPatch(2178420, "9090c705"), new HexPatch(2178428, "600100"), new HexPatch(2178432, "90"), new HexPatch(2178449, "9090c705"), new HexPatch(2178457, "610100"), new HexPatch(2178461, "90"), new HexPatch(2305586, "e1"), new HexPatch(5303039, "e4f2"), new HexPatch(5303513, "ba6801000090"), new HexPatch(5303606, "ba6801000090"), new HexPatch(5303701, "ba6801000090"), new HexPatch(5303796, "ba6801000090"), new HexPatch(5303891, "ba6801000090"), new HexPatch(5304002, "ba6801000090") } },
            { "englishleaguesouthawards", new List<HexPatch> { new HexPatch(1586886, "e1"), new HexPatch(2178304, "9090c705"), new HexPatch(2178312, "5c0100"), new HexPatch(2178316, "90"), new HexPatch(2178333, "9090c705"), new HexPatch(2178341, "5d0100"), new HexPatch(2178345, "90"), new HexPatch(2178362, "9090c705"), new HexPatch(2178370, "5e0100"), new HexPatch(2178374, "90"), new HexPatch(2178391, "9090c705"), new HexPatch(2178399, "5f0100"), new HexPatch(2178403, "90"), new HexPatch(2178420, "9090c705"), new HexPatch(2178428, "600100"), new HexPatch(2178432, "90"), new HexPatch(2178449, "9090c705"), new HexPatch(2178457, "610100"), new HexPatch(2178461, "90"), new HexPatch(2305586, "e1"), new HexPatch(5303039, "e4f2"), new HexPatch(5303513, "ba6701000090"), new HexPatch(5303606, "ba6701000090"), new HexPatch(5303701, "ba6701000090"), new HexPatch(5303796, "ba6701000090"), new HexPatch(5303891, "ba6701000090"), new HexPatch(5304002, "ba6701000090") } },
            { "englishleaguenorthpatch", new List<HexPatch> { new HexPatch(7165624, "656e676c697368206e6f72746865726e207072656d696572206c6561677565207072656d696572206469766973696f6e0000"), new HexPatch(2167004, "b856ad"), new HexPatch(4316033, "22"), new HexPatch(6205579, "733c2573202d20434f"), new HexPatch(6205589, "4d454e54202d204c6f776572206469766973696f6e733e0000"), new HexPatch(1512347, "4cf9"), new HexPatch(1284873, "78"), new HexPatch(1284883, "e85a994100b906"), new HexPatch(1284891, "0000"), new HexPatch(1284994, "8d3c328bf0eb8f"), new HexPatch(5583986, "8d3c328b35e4f29c003b31eb24c3"), new HexPatch(5584035, "8bf075d880b91c01000004eb12"), new HexPatch(5584066, "7cbb9052a110f59c002b0190eb12"), new HexPatch(5584098, "ba22010000f7e203c85a8bc6eb18"), new HexPatch(5584136, "c6811c01000001c3"), new HexPatch(1528312, "a1fcadad008bb8a00500008bb0740100000fb68ec10000000fb6563e2bd14951528bcee8b0d410006a01ff7704ff308b4c2420e870df10005951518bcfe896d410006a01ff7604ff308b4c2420e866de10005941e2c2eb9d508b4424083b054cf99c0074063b059cf69c0058c20400803dfa7c98"), new HexPatch(1528429, "00740ac605fa7c9800003bc0c383"), new HexPatch(1528445, "18f9c38b0285c074548b400485c0744d8b405d85c074468b"), new HexPatch(1528470, "3b05e4f29c00753c807f0c0a7c3609ed7503fe470c83fd0a7c03fe4f0c83fd057c2274068b44aafceb1da14cf99c00c1e0020305fcadad008b00eb0b90909090909090908b04aa85c0c3"), new HexPatch(5397308, "4d05"), new HexPatch(5397318, "17"), new HexPatch(4425164, "eb"), new HexPatch(4425247, "eb"), new HexPatch(4425330, "eb"), new HexPatch(5397716, "07"), new HexPatch(5397632, "96"), new HexPatch(5397614, "8e"), new HexPatch(3536996, "e8175ae1ff"), new HexPatch(0x35FA67, "22"), new HexPatch(4096828, "eb"), new HexPatch(0x524e84,"09") } },
            { "restricttactics", new List<HexPatch> { new HexPatch(4826758, "00"), new HexPatch(4826760, "00"), new HexPatch(4826790, "00"), new HexPatch(4835009, "00"), new HexPatch(4835011, "00"), new HexPatch(4835019, "00"), new HexPatch(4835024, "00"), new HexPatch(4835062, "00"), new HexPatch(4835067, "00"), new HexPatch(4835071, "00"), new HexPatch(4835167, "00"), new HexPatch(4835169, "00"), new HexPatch(4835177, "00"), new HexPatch(4835182, "00"), new HexPatch(4826803, "eb61"), new HexPatch(4827199, "eb61"), new HexPatch(4828121, "e9bb00"), new HexPatch(4828126, "90") } },
            { "changegeneraldat", new List<HexPatch> { new HexPatch(6060932, "6e6f636865"), new HexPatch(6060938, "74") } },
            { "changenamecolour", new List<HexPatch> { new HexPatch(0x35E695, "66B9D0E0") } },
            { "changeregistrylocation", new List<HexPatch> { new HexPatch(0x5f17a0, "41") } },
            { "memorycheckfix", new List<HexPatch> { new HexPatch(0x3a1737, "c1ea14c1e9148d041183c420c390") } },
            { "removemutexcheck", new List<HexPatch> { new HexPatch(0x28d3b6, "eb") } },
            { "chinapatch", new List<HexPatch> { new HexPatch(48194, "98f2"), new HexPatch(48223, "b8c20000"), new HexPatch(48273, "b8c20000"), new HexPatch(48321, "6cf3"), new HexPatch(48351, "5cf6"), new HexPatch(48400, "5cf6"), new HexPatch(81781, "98f2"), new HexPatch(1433872, "98f2"), new HexPatch(1831252, "c74630ffffffffc6464907c3"), new HexPatch(2028851, "98f2"), new HexPatch(2526134, "98f2"), new HexPatch(2526184, "01"), new HexPatch(2526186, "11"), new HexPatch(2526336, "b8d40000"), new HexPatch(2532539, "e8944cf5"), new HexPatch(2532544, "9090"), new HexPatch(2533346, "03"), new HexPatch(2533348, "16"), new HexPatch(2533368, "04"), new HexPatch(2533370, "01"), new HexPatch(2533382, "28"), new HexPatch(2533468, "04"), new HexPatch(2533470, "02"), new HexPatch(2533486, "02"), new HexPatch(2533488, "02"), new HexPatch(2533491, "04"), new HexPatch(2533493, "1d"), new HexPatch(2533506, "32"), new HexPatch(2533613, "03"), new HexPatch(2533616, "04"), new HexPatch(2533618, "1e"), new HexPatch(2533639, "06"), new HexPatch(2533641, "18"), new HexPatch(2533783, "06"), new HexPatch(2533785, "19"), new HexPatch(2533801, "02"), new HexPatch(2533803, "02"), new HexPatch(2533806, "07"), new HexPatch(2533808, "15"), new HexPatch(2533948, "03"), new HexPatch(2533951, "07"), new HexPatch(2533953, "16"), new HexPatch(2533963, "00"), new HexPatch(2533970, "02"), new HexPatch(2533972, "04"), new HexPatch(2533977, "01"), new HexPatch(2533992, "af"), new HexPatch(2534057, "ad"), new HexPatch(2534080, "02"), new HexPatch(2534087, "1c"), new HexPatch(2534322, "bbc2"), new HexPatch(2534325, "000090"), new HexPatch(2534383, "11"), new HexPatch(2534399, "11"), new HexPatch(2534477, "66"), new HexPatch(2534490, "81fac20000"), new HexPatch(2535782, "9e07"), new HexPatch(2535789, "1e"), new HexPatch(2535925, "e9b70400"), new HexPatch(2535930, "90"), new HexPatch(2536035, "09"), new HexPatch(2536037, "1a"), new HexPatch(2536039, "1b"), new HexPatch(2536057, "1b"), new HexPatch(2536075, "01"), new HexPatch(2536077, "05"), new HexPatch(2536081, "0a"), new HexPatch(2536083, "09"), new HexPatch(2536085, "1c"), new HexPatch(2536103, "1c"), new HexPatch(2536127, "0a"), new HexPatch(2536129, "17"), new HexPatch(2536131, "1d"), new HexPatch(2536149, "1d"), new HexPatch(2536156, "e9a60800009090"), new HexPatch(2537145, "02"), new HexPatch(2537147, "02"), new HexPatch(2537185, "01"), new HexPatch(2537187, "05"), new HexPatch(2537191, "02"), new HexPatch(2537193, "09"), new HexPatch(2537237, "02"), new HexPatch(2537239, "1e"), new HexPatch(2537277, "01"), new HexPatch(2537279, "05"), new HexPatch(2537283, "03"), new HexPatch(2537285, "06"), new HexPatch(2537329, "03"), new HexPatch(2537331, "0d"), new HexPatch(2537375, "03"), new HexPatch(2537377, "14"), new HexPatch(2537415, "01"), new HexPatch(2537417, "05"), new HexPatch(2537421, "03"), new HexPatch(2537423, "1b"), new HexPatch(2537461, "01"), new HexPatch(2537467, "04"), new HexPatch(2537469, "04"), new HexPatch(2537513, "04"), new HexPatch(2537515, "0b"), new HexPatch(2537553, "01"), new HexPatch(2537555, "05"), new HexPatch(2537559, "04"), new HexPatch(2537561, "12"), new HexPatch(2537605, "04"), new HexPatch(2537607, "19"), new HexPatch(2537645, "01"), new HexPatch(2537647, "05"), new HexPatch(2537651, "05"), new HexPatch(2537697, "05"), new HexPatch(2537699, "0f"), new HexPatch(2537737, "01"), new HexPatch(2537739, "05"), new HexPatch(2537743, "05"), new HexPatch(2537789, "05"), new HexPatch(2537791, "1d"), new HexPatch(2537829, "01"), new HexPatch(2537831, "05"), new HexPatch(2537835, "06"), new HexPatch(2537837, "06"), new HexPatch(2537881, "06"), new HexPatch(2537883, "0d"), new HexPatch(2537927, "06"), new HexPatch(2537929, "11"), new HexPatch(2537967, "01"), new HexPatch(2537973, "06"), new HexPatch(2537975, "14"), new HexPatch(2538015, "05"), new HexPatch(2538019, "06"), new HexPatch(2538021, "1b"), new HexPatch(2538059, "01"), new HexPatch(2538065, "07"), new HexPatch(2538067, "03"), new HexPatch(2538107, "05"), new HexPatch(2538111, "07"), new HexPatch(2538113, "0a"), new HexPatch(2538151, "01"), new HexPatch(2538157, "07"), new HexPatch(2538159, "11"), new HexPatch(2538199, "05"), new HexPatch(2538203, "08"), new HexPatch(2538205, "0e"), new HexPatch(2538243, "01"), new HexPatch(2538249, "08"), new HexPatch(2538251, "15"), new HexPatch(2538291, "05"), new HexPatch(2538295, "08"), new HexPatch(2538297, "1c"), new HexPatch(2538335, "01"), new HexPatch(2538343, "13"), new HexPatch(2538370, "e9cbf6ffff83c4408bc65f5e81c400020000c21000"), new HexPatch(2538412, "7503"), new HexPatch(2538422, "0f"), new HexPatch(2538738, "c746"), new HexPatch(2538741, "0200888ec2"), new HexPatch(2538747, "0000b202888ec7"), new HexPatch(2538755, "0000884e4a8d463a8d8ea9"), new HexPatch(2538767, "00008896c4"), new HexPatch(2538773, "00008896c6"), new HexPatch(2538779, "00008856528b1650516aff8bcec686c3"), new HexPatch(2538796, "000001c6464201c686c5"), new HexPatch(2538807, "000001c7461c"), new HexPatch(2538815, "ffffc74620"), new HexPatch(2538822, "ffffc6464907ff523c8986ba"), new HexPatch(2538835, "0000b801"), new HexPatch(2538840, "00005ec3"), new HexPatch(2545800, "07"), new HexPatch(2546577, "08"), new HexPatch(2546589, "6a2a516a016a05536a016a175356e80043ebff83c44066c74618020066c7461c020066895e0766895e0966895e0b66c7460d0300c646170666c7461a010066895e0f66895e1166895e1e885e20c6462101885e22895e5c895e60895e648bc65f5e5b81c40002"), new HexPatch(2546692, "00c21000"), new HexPatch(2546851, "b8c20000"), new HexPatch(2546966, "b8d40000"), new HexPatch(2547116, "b8d40000"), new HexPatch(2547807, "98f2"), new HexPatch(2549015, "b8c20000"), new HexPatch(2549037, "98f2"), new HexPatch(2549070, "98f2"), new HexPatch(2549103, "98f2"), new HexPatch(2549136, "98f2"), new HexPatch(2549168, "bac2"), new HexPatch(2549171, "000090"), new HexPatch(2549381, "18"), new HexPatch(2549398, "02"), new HexPatch(2549505, "6a1b598908c74002ff010001894806c74008ff1c0100b50189480cc7400eff0106"), new HexPatch(2549539, "894812c74014ff1f060090909090"), new HexPatch(2549563, "04"), new HexPatch(2661418, "4a"), new HexPatch(2661435, "39"), new HexPatch(4065107, "98f2"), new HexPatch(4065900, "98f2"), new HexPatch(4358133, "98f2"), new HexPatch(4358166, "98f2"), new HexPatch(4420547, "bac2"), new HexPatch(4420550, "000090"), new HexPatch(4420632, "bad400000090"), new HexPatch(4420713, "eb"), new HexPatch(4420798, "bad500000090"), new HexPatch(4420890, "b8d50000"), new HexPatch(5058051, "98f2") } },
            { "datecalcpatch", new List<HexPatch> { new HexPatch(5661364, "e87783beff60807c243b"), new HexPatch(5661375, "75528b0d6423ae"), new HexPatch(5661383, "8b74242833c0e849"), new HexPatch(5661394, "66817e126d077c046601461266817e186d077c046601461866817e336d077c046601463366817e406d077c046601464066817e486d077c046601464883c66ee2bf61e99c5fbcff904a03053db981"), new HexPatch(5661473, "662dd1076683f80ac38b442418ebeb8b44244ce8e2ffffff8944244c8b4424544febd7"), new HexPatch(5661510, "608b0d7423ae"), new HexPatch(5661517, "8b35cc23ae"), new HexPatch(5661523, "a13db981"), new HexPatch(5661528, "662dd1076601460883c611e2f7613b9c24f007"), new HexPatch(5661549, "c3") } },
            { "datecalcpatchjumps", new List<HexPatch> { new HexPatch(0x12c2b0, "E9FF9F4300"), new HexPatch(0x3a41ba, "E85B211C00"), new HexPatch(0x3a3f77, "E89E231C00"), new HexPatch(0x3a47d0, "E8451B1C00"), new HexPatch(0x3a4c8c, "E899161C00909090"), new HexPatch(0x3a4ee5, "4BE845141C0089442450"), new HexPatch(0x12c61f, "E8229D43009090"), new HexPatch(0x3A3CFA, "E81B261C00"), new HexPatch(0x3A4803, "05") } },
            { "comphistory_datecalcpatch", new List<HexPatch> { new HexPatch(0x139AE9, "E912D4420090"), new HexPatch(0x566f00, "8b35d423ae00"), new HexPatch(0x566f06, "60668b15863341"), new HexPatch(0x566f0e, "6681ead10731c089cb2b1d8423ae00"), new HexPatch(0x566f1d, "6601560883c61a4039d875f466f7c201"), new HexPatch(0x566f2e, "74"), new HexPatch(0x566f30, "66426601560883c61a4039c875f461e9ab2bbdff") } },
            { "tapanieurofix", new List<HexPatch> { new HexPatch(1558169, "cd6508"), new HexPatch(1558308, "38"), new HexPatch(1558953, "38"), new HexPatch(1558999, "38"), new HexPatch(2105837, "83f80777078a8000226000c3b0007cfbb00fc3000103050a0c0e0f"), new HexPatch(2108112, "528b4424082b442410c1e00a8b5424142954240c29542418f76c2418f77c240cc1f80a034424105ac2140090528d044050c1f80c8bd0c1e20c52e8def6ffffc1e0045080c6105292c1f80ce8cdf6ffffc1e00450e8a7ffffff405ac30fb7868e0000"), new HexPatch(2108211, "e8c4ffffff03d081c62201"), new HexPatch(2108223, "00c390608b35a823ae008b0d5023ae005633d2e8d5ffffffe2f952e861a430005a5e33d29356e8c2ffffff583bd37ef5"), new HexPatch(2108272, "44241c61c39033c0538b1dbc23ae00395c240872126b056423ae006e03d833c0395c24087701405bc2040090608b0d28"), new HexPatch(2108321, "b60051ff742428e8539802003c02596a0058740b6a01ff742428e8d09902008944241c61c2040090909090608b54242c8b742428ad46468bfe8bca497e0ee80c00000075048366fa004a7fe861c3903907740583c706e2f7c39090e8cbffffff608b7c2424578b44242c6a10592bc87e0c6bc00603f833c0ab66abe2fb5e33d28b0ee31183c6064283fa107cf38954242861c39090e807ffffff8b4871e3f68b093b0d10fa9c0075ec6050e87f87f3ff5b8944241c618b7c24246a1059e88dffffff74d1"), new HexPatch(2108518, "06ebbc90666a0066ff7636ff7614e883ffffff66c74636100083c408e9"), new HexPatch(2108548, "9af7ff") } },
            { "positionintacticsview", new List<HexPatch> { new HexPatch(4825080, "0c"), new HexPatch(4825090, "04"), new HexPatch(4825095, "00"), new HexPatch(4825100, "39"), new HexPatch(4825105, "12"), new HexPatch(4825110, "15"), new HexPatch(4825115, "0b"), new HexPatch(4825120, "03"), new HexPatch(4836405, "d2"), new HexPatch(4837381, "027f"), new HexPatch(4837447, "8b0d7e31ae00740666b9ffff9090516a016a01"), new HexPatch(4837467, "006a01556a0653b95044b700e89470f6ff8a44241b3c01900f84df"), new HexPatch(6864332, "3e0020202020202020202063617074"), new HexPatch(0x49E03C, "05") } },
            { "fixworldcuppre2000", new List<HexPatch> { new HexPatch(1235195, "355f"), new HexPatch(1301218, "c8"), new HexPatch(1558169, "cd6508"), new HexPatch(1558308, "38"), new HexPatch(1558953, "38"), new HexPatch(1558999, "38"), new HexPatch(1583179, "0a"), new HexPatch(1583182, "00"), new HexPatch(1583250, "00"), new HexPatch(1583253, "0a"), new HexPatch(1585499, "9090eb"), new HexPatch(2070945, "c9"), new HexPatch(2070972, "ca"), new HexPatch(2071732, "e4f2"), new HexPatch(2105837, "83f80777078a8000226000c3b0007cfbb00fc3000103050a0c0e0f"), new HexPatch(2108112, "528b4424082b442410c1e00a8b5424142954240c29542418f76c2418f77c240cc1f80a034424105ac2140090528d044050c1f80c8bd0c1e20c52e8def6ffffc1e0045080c6105292c1f80ce8cdf6ffffc1e00450e8a7ffffff405ac30fb7868e0000"), new HexPatch(2108211, "e8c4ffffff03d081c62201"), new HexPatch(2108223, "00c390608b35a823ae008b0d5023ae005633d2e8d5ffffffe2f952e861a430005a5e33d29356e8c2ffffff583bd37ef5"), new HexPatch(2108272, "44241c61c39033c0538b1dbc23ae00395c240872126b056423ae006e03d833c0395c24087701405bc2040090608b0d28"), new HexPatch(2108321, "b60051ff742428e8539802003c02596a0058740b6a01ff742428e8d09902008944241c61c2040090909090608b54242c8b742428ad46468bfe8bca497e0ee80c00000075048366fa004a7fe861c3903907740583c706e2f7c39090e8cbffffff608b7c2424578b44242c6a10592bc87e0c6bc00603f833c0ab66abe2fb5e33d28b0ee31183c6064283fa107cf38954242861c39090e807ffffff8b4871e3f68b093b0d10fa9c0075ec6050e87f87f3ff5b8944241c618b7c24246a1059e88dffffff74d1"), new HexPatch(2108518, "06ebbc90666a0066ff7636ff7614e883ffffff66c74636100083c408e9"), new HexPatch(2108548, "9af7ff"), new HexPatch(2111540, "81ec200200"), new HexPatch(2111546, "5355565751b9ec04000083c8ffbf78f19c"), new HexPatch(2111564, "f3ab6a1a59bf9c3cb600f3ab59a19423ae"), new HexPatch(2111582, "33db33f63bc3"), new HexPatch(5315661, "cc"), new HexPatch(5315720, "cc"), new HexPatch(5315748, "cc"), new HexPatch(5315768, "cc"), new HexPatch(5328550, "08"), new HexPatch(5328724, "08"), new HexPatch(5328917, "08"), new HexPatch(5334142, "08"), new HexPatch(5341916, "cc"), new HexPatch(5364670, "e97f0200"), new HexPatch(5364675, "90"), new HexPatch(5371020, "e9780100"), new HexPatch(5371025, "90"), new HexPatch(5374830, "cc"), new HexPatch(5383077, "e95d0200"), new HexPatch(5383082, "90"), new HexPatch(5419021, "9090"), new HexPatch(5431242, "e95e0700"), new HexPatch(5431247, "90"), new HexPatch(5433249, "9090909090"), new HexPatch(5434188, "e99e0400"), new HexPatch(5434193, "90"), new HexPatch(5435974, "909090909090"), new HexPatch(5436076, "9090909090"), new HexPatch(5446400, "eb"), new HexPatch(5499314, "9090"), new HexPatch(5499321, "9090") } },
            { "englishleaguenorthpatchrelegation", new List<HexPatch> { new HexPatch(5705608, "e8"), new HexPatch(5393381, "b4fc"), new HexPatch(5394052, "28"), new HexPatch(5397545, "c409"), new HexPatch(5397726, "adadc640534e5f5ec3"), new HexPatch(5397736, "81ec00020000568bf1e8fac2cdff90"), new HexPatch(5398142, "6a006850c300"), new HexPatch(5398149, "8bcee884d1d5ffebca"), new HexPatch(2105328, "600fb68ec1000000e35c0fb75e3e4b7c55918b4e04e34f8b895d"), new HexPatch(2105355, "0000e347ff3191e8399e1e00"), new HexPatch(2105367, "85c0743b506a03e89daf30"), new HexPatch(2105379, "85c05858507507e8710000"), new HexPatch(2105391, "eb05e82a00"), new HexPatch(2105397, "006bdb3b039eb1000000ff30ff33e80001"), new HexPatch(2105415, "0083c00683eb3be2efe8753234005861c390909090909090906033ed0fb64e3e968bfead85c0740566ad9803e8e2f48bf755e842af30009258ad9366ad982bd07ff7871f66874704"), new HexPatch(2105488, "5efa668746fe9883c7062be87fd861906033d20fb64e3e9642ad85c0740788505f4646e2f361c390"), new HexPatch(2105672, "608b7424248b7c242885"), new HexPatch(2105683, "741a85ff74166a"), new HexPatch(2105691, "ff7657576a"), new HexPatch(2105697, "ff775756e836100800e8410f080061c208"), new HexPatch(2103864, "ff742408ff7424086a10ff35"), new HexPatch(2103877, "44de00ff156071960085c075048b442404c39085c07425608b70619533ff85f674156a1f59515655e8ce1af4ff5d5e59f62c1903f8e2ee897c241c61c390"), new HexPatch(2104131, "90807a0f0fc3807a100f7d04807a110fc3807a120f7d0a807a130f7d04807a140fc3807a150fc3807a190fc3807a170f7d04807a180fc360adab85c074798b506185d2747280fb"), new HexPatch(2104203, "7507e8b2ffffff7d6880fb01750ee8ccffffff7c5ae8a4ffffff7d5580fb02750ee8beffffff7c47e891ffffff7d4280fb03750ee8a6ffffff7c34e8"), new HexPatch(2104264, "ffffff7d2f80fb047515e898ffffff7c21e876ffffff7d1ce880ffffff7d1580fb05750ee879ffffff7c07e86dffffff7d0233c053c1e30581c3831a6000e84dfeffff5b66d1f866ab490f8563ffffff61c38b4424088b542404668b4004662b420498c36083ec188bec6a055b8db2d7"), new HexPatch(2104377, "0000683001"), new HexPatch(2104383, "00e8ac3034"), new HexPatch(2104389, "83c40489449d00976a3259e825ffffff681a1c60006a066a32ff749d00e8cc36340083c4104b7dce8bf5ad0fbf5004430fbf580ad1fb6603d3ad660350046603500a66035010ad660350040fbf580ad1fb6603d3ad660350046603500a0fbf5810d1fb6603d3ad660350040fbf580ad1fb6603d3ad660350046603500a926a0c5b9966f7fb506a06598bf5ad5150e8f23534005859e2f45883c41840"), new HexPatch(2104546, "44241c61c390"), new HexPatch(2104724, "608bec8b3d5c23ae006bcf0651e8a030340097915a578b15b423ae0033db518d75288b452485c07c098b4a53e31d39017519ad85c074148b4a57e30f390175f28bc2abe850feffff66ab4381c24502000059e2ca58"), new HexPatch(2104810, "451c681a1c60006a065350e83935340083c41061c3"), new HexPatch(2105276, "90909090608b4424246a006865010000686601000050e8bdfdffff5941e2fc"), new HexPatch(2105308, "44241c61c2040090909090909090909090909090"), new HexPatch(2105363, "a9ffffff"), new HexPatch(1235195, "355f"), new HexPatch(2111538, "90"), new HexPatch(5397632, "8e"), new HexPatch(2661486, "e87d83f7ff5e5d5f5bc20400") } },
            { "addextraspaceheader", new List<HexPatch> { new HexPatch(254, "05"), new HexPatch(330, "be"), new HexPatch(504, "0060"), new HexPatch(544, "000002"), new HexPatch(584, "00e0"), new HexPatch(624, "0020"), new HexPatch(656, "2e6e69636b"), new HexPatch(666, "20"), new HexPatch(669, "709e"), new HexPatch(674, "20"), new HexPatch(677, "c06d"), new HexPatch(692, "200000e0") } },
            { "addadditionalcolumns", new List<HexPatch> { new HexPatch(0x47a536, "c7"), new HexPatch(0x47a538, "24"), new HexPatch(0x47a53b, "020302c74424500302"), new HexPatch(0x47a545, "0290909090"), new HexPatch(0x47a6ef, "08"), new HexPatch(0x47ad39, "08"), new HexPatch(0x47ac9a, "07"), new HexPatch(0x47a8a5, "be01"), new HexPatch(0x47a86a, "e991c756"), new HexPatch(0x47a86f, "90"), new HexPatch(0x47acb7, "2f"), new HexPatch(0x47acc7, "f82e"), new HexPatch(0x6dc000, "0f846a38a9ff837c243c240f8cbd39a9ff837c243c2f7c21c74424146580de"), new HexPatch(0x6dc020, "c6056580de"), new HexPatch(0x6dc026, "f8c7442478"), new HexPatch(0x6dc02f, "b9f07fde"), new HexPatch(0x6dc034, "e99c39a9ff8b44240c8b4861837c243c26750a68"), new HexPatch(0x6dc049, "80de"), new HexPatch(0x6dc04c, "8a4121eb31837c243c28750a680c80de"), new HexPatch(0x6dc05d, "8a4125eb31837c243c25750a681680de"), new HexPatch(0x6dc06e, "8a412ceb20837c243c2a750a682280de"), new HexPatch(0x6dc07f, "8a412deb0f837c243c29750a682f80de"), new HexPatch(0x6dc090, "8a4134eb71837c243c2c751e683780de"), new HexPatch(0x6dc0a1, "8a4135505180790f0e7d07e83fd275ffeb50e8c8d275ffeb49837c243c27750a684d80de"), new HexPatch(0x6dc0c6, "8a4122eb3b837c243c24750a687180de"), new HexPatch(0x6dc0d7, "8a4056eb2a837c243c2b750a686980de"), new HexPatch(0x6dc0e8, "8a4059eb19837c243c2d7514684380de"), new HexPatch(0x6dc0f9, "8a41385051e87dd275ff83c408eb0f837c243c2e9090687e80de"), new HexPatch(0x6dc114, "8a4142a26580de"), new HexPatch(0x6dc11c, "59c74424146580de"), new HexPatch(0x6dc125, "e9ab38a9ff90"), new HexPatch(0x6dd000, "436f6e73697374656e6379"), new HexPatch(0x6dd00c, "44697274696e657373"), new HexPatch(0x6dd016, "426967204d617463686573"), new HexPatch(0x6dd022, "496e6a7572792050726f6e65"), new HexPatch(0x6dd02f, "4669746e657373"), new HexPatch(0x6dd037, "4f6e65206f6e204f6e6573"), new HexPatch(0x6dd043, "50656e616c74696573"), new HexPatch(0x6dd04d, "436f726e657273"), new HexPatch(0x6dd055, "416d626974696f6e"), new HexPatch(0x6dd065, "07"), new HexPatch(0x6dd069, "4c6f79616c7479"), new HexPatch(0x6dd071, "41646170746162696c697479"), new HexPatch(0x6dd07e, "566572736174696c697479"), new HexPatch(0x0047A805, "895424"), new HexPatch(0x0047A809, "90"), new HexPatch(0x0047A81F, "30"), new HexPatch(0x0047A836, "03") } },
            { "addadditionalcolumns_italy", new List<HexPatch> { new HexPatch(0x47a536, "c7"), new HexPatch(0x47a538, "24"), new HexPatch(0x47a53b, "010301c74424500302"), new HexPatch(0x47a545, "0290909090"), new HexPatch(0x47a6ef, "08"), new HexPatch(0x47ad39, "08"), new HexPatch(0x47ac9a, "07"), new HexPatch(0x47a8a5, "be01"), new HexPatch(0x47a86a, "e991c756"), new HexPatch(0x47a86f, "90"), new HexPatch(0x47acb7, "2f"), new HexPatch(0x47acc7, "f82e"), new HexPatch(0x6dc000, "0f846a38a9ff837c243c240f8cbd39a9ff837c243c2f7c21c74424146580de"), new HexPatch(0x6dc020, "c6056580de"), new HexPatch(0x6dc026, "f8c7442478"), new HexPatch(0x6dc02f, "b9f07fde"), new HexPatch(0x6dc034, "e99c39a9ff8b44240c8b4861837c243c26750a68"), new HexPatch(0x6dc049, "80de"), new HexPatch(0x6dc04c, "8a4121eb31837c243c28750a680c80de"), new HexPatch(0x6dc05d, "8a4125eb31837c243c25750a681680de"), new HexPatch(0x6dc06e, "8a412ceb20837c243c2a750a682280de"), new HexPatch(0x6dc07f, "8a412deb0f837c243c29750a682f80de"), new HexPatch(0x6dc090, "8a4134eb71837c243c2c751e683780de"), new HexPatch(0x6dc0a1, "8a4135505180790f0e7d07e83fd275ffeb50e8c8d275ffeb49837c243c27750a684d80de"), new HexPatch(0x6dc0c6, "8a4122eb3b837c243c24750a687180de"), new HexPatch(0x6dc0d7, "8a4056eb2a837c243c2b750a686980de"), new HexPatch(0x6dc0e8, "8a4059eb19837c243c2d7514684380de"), new HexPatch(0x6dc0f9, "8a41385051e87dd275ff83c408eb0f837c243c2e9090687e80de"), new HexPatch(0x6dc114, "8a4142a26580de"), new HexPatch(0x6dc11c, "59c74424146580de"), new HexPatch(0x6dc125, "e9ab38a9ff90"), new HexPatch(0x6dd000, "436f6e73697374656e6379"), new HexPatch(0x6dd00c, "44697274696e657373"), new HexPatch(0x6dd016, "426967204d617463686573"), new HexPatch(0x6dd022, "496e6a7572792050726f6e65"), new HexPatch(0x6dd02f, "4669746e657373"), new HexPatch(0x6dd037, "4f6e65206f6e204f6e6573"), new HexPatch(0x6dd043, "50656e616c74696573"), new HexPatch(0x6dd04d, "436f726e657273"), new HexPatch(0x6dd055, "416d626974696f6e"), new HexPatch(0x6dd065, "07"), new HexPatch(0x6dd069, "4c6f79616c7479"), new HexPatch(0x6dd071, "41646170746162696c697479"), new HexPatch(0x6dd07e, "566572736174696c697479") } },
            { "makeyourpotential200", new List<HexPatch> { new HexPatch(0x1fbafe, "608d7e368d7424666a1859f3a4618b4c240881f90000f0007c3c8b4969e33d66c74104010066c74106c80090"), new HexPatch(0x2b5cd7, "e86cd0f4ffc3"), new HexPatch(0x202d48, "9090909090e8e8ffffffc3"), new HexPatch(0x202d3a, "803d882cae00000f8441ffffffc3"), new HexPatch(0x202c88, "60a16423ae00488b35bc23ae"), new HexPatch(0x202c95, "6bc06e03f06a10598b7e6985ff742a0fb747086603470a6603470c0fb7570483c060c1f8073bc2720342eb03740b4a663b5706730466"), new HexPatch(0x202ccc, "570483ee6ee2ca61c3") } },
            { "fixsuperkeepers", new List<HexPatch> { new HexPatch(0x56D80A, "8A") } },
            { "bugfixes", new List<HexPatch> { /* Unlock your creativity */ new HexPatch(0x2eda3a, "1c"), /* MarkingAndPositionClampTo100.patch */ new HexPatch(0x2ef644, "e9347e6f"), new HexPatch(0x2ef649, "90"), new HexPatch(0x2ef692, "e9f77d6f"), new HexPatch(0x2ef697, "90"), new HexPatch(0x6dc470, "83f8647d01c3b864"), new HexPatch(0x6dc47b, "c3"), new HexPatch(0x6dc47d, "e8eeffffff88861001"), new HexPatch(0x6dc488, "e9bd8190ff"), new HexPatch(0x6dc48e, "e8ddffffff88861101"), new HexPatch(0x6dc499, "e9fa8190ff"), /* EnsureCashDoesNotResetToZero */ new HexPatch(0x1986e3, "e91da80600"), new HexPatch(0x202f05, "813e00d197a67f06c706009435778b1e0fbfcfe9cb57f9ff") } }
        };

        public Dictionary<string, List<HexPatch>> ReversePatches = new Dictionary<string, List<HexPatch>>
        {
            { "colouredattributes", new List<HexPatch> { new HexPatch(0x0047ABF1, "668B0DECBDAE00"), new HexPatch(0x0047AFA3, "66A1ECBDAE00"), new HexPatch(0x005660E8, "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000") } },
            { "idlesensitivity", new List<HexPatch> { new HexPatch(0x000E243A, "6685D27507668B15DE6BDD000FBFD283C2FC83FA2C0F87C008000033DB"), new HexPatch(0x005472D5, "E76700"), new HexPatch(0x00566120, "0000000000"), new HexPatch(0x00566126, "0000000000"), new HexPatch(0x0056612C, "0000000000000000"), new HexPatch(0x00566135, "000000000000"), new HexPatch(0x0056613C, "0000000000000000000000000000"), new HexPatch(0x0056614B, "000000000000"), new HexPatch(0x00566152, "0000000000"), new HexPatch(0x00566158, "00000000000000"), new HexPatch(0x00566160, "0000000000"), new HexPatch(0x00566168, "0000000000000000000000000000000000000000000000000000000000"), new HexPatch(0x00566186, "0000000000000000"), new HexPatch(0x0056618F, "0000000000"), new HexPatch(0x00566195, "0000000000000000"), new HexPatch(0x0056619E, "000000000000"), new HexPatch(0x005661A5, "0000000000000000"), new HexPatch(0x005661AE, "000000000000000000"), new HexPatch(0x005661B8, "0000000000000000000000000000"), new HexPatch(0x005661C7, "000000000000000000000000"), new HexPatch(0x005661D6, "00000000000000000000"), new HexPatch(0x005661E3, "0000"), new HexPatch(0x005661E6, "00000000"), new HexPatch(0x005661ED, "0000"), new HexPatch(0x005661F2, "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"), new HexPatch(0x00566274, "0000000000"), new HexPatch(0x0056627A, "000000000000000000000000000000000000000000"), new HexPatch(0x00566290, "0000"), new HexPatch(0x00566293, "000000000000000000"), new HexPatch(0x0056629D, "0000"), new HexPatch(0x005662A0, "000000000000000000"), new HexPatch(0x005662AA, "0000"), new HexPatch(0x005662AD, "000000000000") } },
            { "idlesensitivitytransferscreen", new List<HexPatch> { new HexPatch(0x004EC743, "0FBFC80FBFD7"), new HexPatch(0x0056647B, "00000000000000000000000000000000") } },
            { "disablecdremove", new List<HexPatch> { new HexPatch(0x0042A98B, "E800D9DBFF"), new HexPatch(0x0042E400, "E88B9EDBFF") } },
            { "disablesplashscreen", new List<HexPatch> { new HexPatch(0x001CCD3C, "0F8471030000") } },
            { "disableunprotectedcontracts", new List<HexPatch> { new HexPatch(0x00124CD2, "68D1070000") } },
            { "sevensubs", new List<HexPatch> { new HexPatch(0x0016F224, "C2080090909090"), new HexPatch(0x00172E10, "05"), new HexPatch(0x00174C03, "5EC390909090"), new HexPatch(0x00179DF4, "75"), new HexPatch(0x00176801, "05"), new HexPatch(0x0017815C, "05"), new HexPatch(0x00153A8C, "04"), new HexPatch(0x001BC48C, "05"), new HexPatch(0x003F2A46, "05"), new HexPatch(0x003F66D6, "75"), new HexPatch(0x001BC48C, "05"), new HexPatch(0x00170C6B, "88464988464A"), new HexPatch(0x0016D3F0, "05"), new HexPatch(0x16C474, "c2080090909090") } },
            { "showstarplayers", new List<HexPatch> { new HexPatch(0x0005B82C, "7571") } },
            { "hideprivatebids", new List<HexPatch> { new HexPatch(0x004D1493, "0F8409010000") } },
            { "allowclosewindow", new List<HexPatch> { new HexPatch(0x0028D748, "8D8C2418010000") } },
            { "forceloadallplayers", new List<HexPatch> { new HexPatch(0x001255FF, "6681B8800000004C1D"), new HexPatch(0x00125637, "6681B8800000004C1D"), new HexPatch(0x001269F1, "D6010000") } },
            { "regenfixes", new List<HexPatch> { new HexPatch(0x3ad1bf, "32"), new HexPatch(0x003ABEAB, "0F8D2C05"), new HexPatch(0x003ABEB0, "00"), new HexPatch(0x3AD089, "2B"), new HexPatch(0x3AD090, "24") } },
            { "to1280x800", new List<HexPatch> { new HexPatch(0x00002B7E, "1F03"), new HexPatch(0x00002B85, "1F03"), new HexPatch(0x00002B8E, "5702"), new HexPatch(0x0000384D, "5802"), new HexPatch(0x00003924, "5802"), new HexPatch(0x000039B7, "5802"), new HexPatch(0x00003B02, "1F03"), new HexPatch(0x00003B65, "1F03"), new HexPatch(0x00003BDE, "5702"), new HexPatch(0x00003C41, "5702"), new HexPatch(0x00059A22, "8D9424B4"), new HexPatch(0x00059A27, "0000"), new HexPatch(0x00059A3B, "08"), new HexPatch(0x0005C018, "04"), new HexPatch(0x0006041C, "5702"), new HexPatch(0x00060421, "1F03"), new HexPatch(0x00070FAE, "5702"), new HexPatch(0x00070FB3, "1F03"), new HexPatch(0x00072AD7, "5702"), new HexPatch(0x00072ADC, "1F03"), new HexPatch(0x000AF58C, "406513"), new HexPatch(0x000AF670, "5C6413"), new HexPatch(0x0015F2CB, "5702"), new HexPatch(0x0015F2D0, "1F03"), new HexPatch(0x0015F2E8, "5702"), new HexPatch(0x0015F2ED, "1F03"), new HexPatch(0x0015F38E, "5702"), new HexPatch(0x0015F393, "1F03"), new HexPatch(0x0015F56C, "5702"), new HexPatch(0x0015F571, "1F03"), new HexPatch(0x0015F58E, "5702"), new HexPatch(0x0015F593, "1F03"), new HexPatch(0x0015F5EC, "5702"), new HexPatch(0x0015F5F1, "1F03"), new HexPatch(0x0015F990, "81EC0002"), new HexPatch(0x0015F995, "00568BF1668B96A0E91200"), new HexPatch(0x0015FBA0, "A10C29AE"), new HexPatch(0x001612A2, "1F03"), new HexPatch(0x001612A9, "1F03"), new HexPatch(0x001612CB, "5702"), new HexPatch(0x001612D2, "5702"), new HexPatch(0x00190F23, "5702"), new HexPatch(0x00190F2B, "1F03"), new HexPatch(0x001A32C4, "C1F80403C88BC3"), new HexPatch(0x001A32CF, "5103C2C1F80440"), new HexPatch(0x001A33F5, "10"), new HexPatch(0x001A3774, "F7D9C1F80403C8"), new HexPatch(0x001A377E, "24"), new HexPatch(0x001A3783, "5103C2C1F80440"), new HexPatch(0x001A38AB, "10"), new HexPatch(0x001A3CCE, "F7D9C1F80403C8"), new HexPatch(0x001A3CDB, "5103C2C1F80440"), new HexPatch(0x001A3E06, "10"), new HexPatch(0x001A4240, "C1F80403C88BC3"), new HexPatch(0x001A424B, "5103C2C1F80440"), new HexPatch(0x001A4376, "10"), new HexPatch(0x001A46A4, "A0"), new HexPatch(0x001A46AE, "B90F000000"), new HexPatch(0x001A46D4, "10"), new HexPatch(0x001A46F8, "10"), new HexPatch(0x001A472A, "A0"), new HexPatch(0x001A4733, "8BD72BD083C20F"), new HexPatch(0x001A47FF, "A0"), new HexPatch(0x001A4809, "B80F0000"), new HexPatch(0x001A482C, "10"), new HexPatch(0x001A482F, "B90F0000"), new HexPatch(0x001A4850, "10"), new HexPatch(0x001A4856, "8BD7"), new HexPatch(0x001A487E, "A0"), new HexPatch(0x001A4887, "8BCF2BC883C10F"), new HexPatch(0x001A4A66, "A0"), new HexPatch(0x001A4A6C, "BA0F0000"), new HexPatch(0x001A4AD2, "10"), new HexPatch(0x001A4AD5, "BA0F0000"), new HexPatch(0x001A4B3F, "10"), new HexPatch(0x001A4B48, "8BCF"), new HexPatch(0x001A4BB4, "A0"), new HexPatch(0x001A4BC0, "8BCF2BC883C10F"), new HexPatch(0x001A4D16, "A0"), new HexPatch(0x001A4D1C, "BA0F0000"), new HexPatch(0x001A4D82, "10"), new HexPatch(0x001A4DEF, "10"), new HexPatch(0x001A4DF8, "8BCF"), new HexPatch(0x001A4E64, "A0"), new HexPatch(0x001A4E70, "8BCF2BC883C1"), new HexPatch(0x001D79EE, "5802"), new HexPatch(0x001D79F3, "2003"), new HexPatch(0x001D8646, "5802"), new HexPatch(0x001D864B, "2003"), new HexPatch(0x001D8686, "5802"), new HexPatch(0x001D868B, "2003"), new HexPatch(0x001E4588, "00"), new HexPatch(0x001E4712, "00"), new HexPatch(0x001E4714, "00"), new HexPatch(0x001E5CEA, "B8"), new HexPatch(0x001E5CEC, "000000"), new HexPatch(0x001E5CF0, "B8"), new HexPatch(0x001E5CF2, "000000"), new HexPatch(0x001E5CF6, "B8"), new HexPatch(0x001E5CF8, "000000"), new HexPatch(0x001E5CFC, "B8"), new HexPatch(0x001E5CFE, "000000"), new HexPatch(0x001E5D02, "B8"), new HexPatch(0x001E5D04, "000000"), new HexPatch(0x001E5D08, "B8"), new HexPatch(0x001E5D0A, "000000"), new HexPatch(0x001E5D0E, "B8"), new HexPatch(0x001E5D10, "000000"), new HexPatch(0x001E5D22, "0FBFC08D0C80C1E10803C88B048DAC1DAE00C38D4900"), new HexPatch(0x001E774F, "9DE6FFFF"), new HexPatch(0x001E778B, "F1F5FFFF"), new HexPatch(0x001E77AF, "3DE6FFFF"), new HexPatch(0x001E77E3, "69C3FFFF"), new HexPatch(0x001E780D, "6FF5FFFF"), new HexPatch(0x001E7826, "86B6FFFF"), new HexPatch(0x001E78BC, "2003"), new HexPatch(0x001E78E6, "2003"), new HexPatch(0x001E78EE, "5802"), new HexPatch(0x001E7B72, "8D14928D0492"), new HexPatch(0x001E7B7D, "C1E005"), new HexPatch(0x001E7BDA, "8D14808D0492"), new HexPatch(0x001E7BE5, "C1E005"), new HexPatch(0x001E7C37, "8D14808D0492"), new HexPatch(0x001E7C42, "C1E005"), new HexPatch(0x001E7CA9, "8D149B8B5C24148D0492"), new HexPatch(0x001E7CB8, "C1E005"), new HexPatch(0x001E7D69, "8D14808D0492"), new HexPatch(0x001E7D73, "C1E005"), new HexPatch(0x001E7DD8, "8D149B8D0492"), new HexPatch(0x001E7DE2, "C1E005"), new HexPatch(0x001E7E3A, "8D149B8D0492"), new HexPatch(0x001E7E44, "C1E005"), new HexPatch(0x001E7EB1, "8D14808D0492"), new HexPatch(0x001E7EBB, "C1E005"), new HexPatch(0x001E82F5, "5802"), new HexPatch(0x001E8305, "5802"), new HexPatch(0x001E830A, "2003"), new HexPatch(0x001ED7AB, "00"), new HexPatch(0x001ED7AD, "20"), new HexPatch(0x001EE1F6, "1F03"), new HexPatch(0x001EE20B, "5702"), new HexPatch(0x001F14C8, "5702"), new HexPatch(0x001F14D0, "1F03"), new HexPatch(0x00201E49, "893D20F49C00893D24F49C00893D28F49C"), new HexPatch(0x00201E5B, "893D2CF49C00893D30F49C00893D34F49C00893D38F49C00893D3CF49C00893D40F49C00893D44F49C00893D48F49C00893D4CF49C00893D50F49C00893D54F49C00893D58F49C00893D5CF49C00893D60F49C00893D64F49C00893D68F49C00893D6CF49C00893D70F49C00893D74F49C00893D78F49C00893D7CF49C00893D80F49C00893D84F49C00893D"), new HexPatch(0x00201EEB, "893D8CF49C00893D90F49C00893D94F49C00893D98F49C00893D9CF49C00893DA0F49C00893DA4F49C00893DA8F49C00893DACF49C00893DB0F49C00893DB4F49C00893DB8F49C00893DBCF49C00893DC0F49C00893DC4F49C00893DC8F49C00893DCCF49C00893DD0F49C00893DD4F49C00893DD8F49C"), new HexPatch(0x00201F63, "893DDCF49C00893DE0F49C00893DE4F49C00893DE8F49C00893DECF49C"), new HexPatch(0x00202173, "893D3CF69C00893D40F69C00893D44F69C00893D48F69C0089"), new HexPatch(0x0020218D, "4CF69C"), new HexPatch(0x00202192, "3D50F69C00893D54F69C00893D58F69C00893D5CF69C00893D60F69C"), new HexPatch(0x002021AF, "893D64F69C"), new HexPatch(0x002021B5, "893D68F69C00893D6CF69C"), new HexPatch(0x0028D8D5, "2003"), new HexPatch(0x0028D8E0, "5802"), new HexPatch(0x00319346, "5702"), new HexPatch(0x0035DC11, "5702"), new HexPatch(0x0035E16C, "01"), new HexPatch(0x00388A33, "01"), new HexPatch(0x00388A86, "01"), new HexPatch(0x0038900F, "0203"), new HexPatch(0x00400898, "668BAC240C020000"), new HexPatch(0x00408E77, "5702"), new HexPatch(0x00408E7C, "1F03"), new HexPatch(0x0041BD12, "5702"), new HexPatch(0x0041BD17, "1F03"), new HexPatch(0x0041D6DD, "10"), new HexPatch(0x00421D1D, "884424308D4424288B5424305250"), new HexPatch(0x00421D33, "04"), new HexPatch(0x00421F62, "03"), new HexPatch(0x0042259D, "8844242C8D442434"), new HexPatch(0x004225A9, "52"), new HexPatch(0x004225B3, "06"), new HexPatch(0x0042283E, "03"), new HexPatch(0x00470CCE, "16"), new HexPatch(0x00470F57, "16"), new HexPatch(0x004750E2, "0C"), new HexPatch(0x0049C626, "01"), new HexPatch(0x0049C69E, "01"), new HexPatch(0x0049C73C, "01"), new HexPatch(0x004AEED4, "5802"), new HexPatch(0x004AEED9, "2003"), new HexPatch(0x004AEEFC, "5802"), new HexPatch(0x004AEF01, "2003"), new HexPatch(0x004B9A74, "5702"), new HexPatch(0x004B9A7C, "1F03"), new HexPatch(0x004BAA58, "5702"), new HexPatch(0x004BAA60, "1F03"), new HexPatch(0x005C3720, "44454641554C54"), new HexPatch(0x005C3728, "504943"), new HexPatch(0x005C372C, "52474E"), new HexPatch(0x00616449, "617463682E6D6272"), new HexPatch(0x0065A7CD, "616D65") } },
            { "tapanispacemaker", new List<HexPatch> { new HexPatch(0x0012D8FB, "4141"), new HexPatch(0x00203834, "893D08059D"), new HexPatch(0x0020383A, "893D0C059D00893D10059D00893D14059D"), new HexPatch(0x0020384C, "893D18059D00893D1C059D00893D20059D"), new HexPatch(0x0020385E, "893D24059D00") } },
            { "findallplayers", new List<HexPatch> { new HexPatch(0x003AFC4B, "0F8D9D"), new HexPatch(0x003AFC50, "00") } },
            { "jobsabroadboost", new List<HexPatch> { new HexPatch(0x0029EA36, "74"), new HexPatch(0x0029D315, "7E"), new HexPatch(0x0029D665, "7E"), new HexPatch(0x0029D6E4, "7D"), new HexPatch(0x0029EA7E, "74") } },
            { "tapaninewregencode", new List<HexPatch> { new HexPatch(0x00202120, "3D04F69C00893D08F69C00893D0CF69C00893D10F69C00893D14F69C00893D18F69C00893D1CF69C"), new HexPatch(0x0020249C, "00893D58F89C00893D5CF89C00893D"), new HexPatch(0x002024B8, "F89C00893D6CF89C00893D70F89C00893D74F89C00893D78F89C00893D7CF89C00893D80F89C00893D84F89C00893D88F89C"), new HexPatch(0x002024EB, "893D8CF89C00893D90F89C"), new HexPatch(0x002024F7, "893D94F89C00893D98"), new HexPatch(0x003ACFA0, "56E81ABCD8FF83C4085E83C408C20400"), new HexPatch(0x003ACFB2, "5E8801"), new HexPatch(0x00202500, "F89C00893D9CF89C00893DA0F89C"), new HexPatch(0x0020250F, "893DA4F89C00893DA8F89C00893DACF89C00893DB0F89C00893DB4F89C00893DB8F89C00893DBCF89C00893DC0F89C00893DC4F89C"), new HexPatch(0x00202545, "893DC8F89C00893DCCF89C00893DD0F89C00893DD4F89C00893DD8F89C00893DDCF89C00"), new HexPatch(0x00202581, "893DF0F89C00893DF4F89C00893DF8F89C00893DFCF89C00893D00F99C00893D04F99C00893D08F99C00893D0CF99C00893D10F99C00893D14F99C00893D18F9"), new HexPatch(0x002025C2, "00893D1CF99C00893D20F99C00893D24F99C00893D28F99C00893D2CF99C00893D30F99C00893D34F99C00893D38F99C00893D3CF99C00893D40F99C00893D44F99C00893D48F99C00893D4CF99C00893D50F99C00893D54F99C"), new HexPatch(0x0020261D, "893D58F99C00893D5CF99C00893D60F99C00893D64F99C00893D68F99C00893D6CF99C00893D70F99C00893D74F99C00893D78F99C00893D7CF99C00893D80F99C00893D84F99C00893D88F99C"), new HexPatch(0x00202670, "00893D90F99C00893D94F99C00893D98F99C00893D9CF99C00893DA0F99C00893DA4F99C"), new HexPatch(0x00202695, "893DA8F99C00893DACF99C00893DB0F99C00893DB4F99C00893DB8F99C00893DBCF99C00893DC0F99C"), new HexPatch(0x002026BF, "893DC4F99C00893DC8F99C00893DCCF99C00893DD0F99C00893DD4F99C00893DD8F99C00893DDCF99C00893DE0F99C00893DE4F99C00893DE8F99C00"), new HexPatch(0x002026FF, "9C00893DF0F99C00893DF4F99C00893DF8F99C00893DFCF99C00893D00FA9C00893D04FA9C00893D08FA9C00893D0CFA9C00893D10FA9C00893D14FA9C00893D18FA9C00893D1CFA9C00893D20FA9C00893D24FA9C00893D28FA9C00893D2CFA9C00893D30FA9C00893D34FA9C00893D38FA9C00893D3CFA9C00893D40FA9C00893D44FA9C00893D48FA9C00893D4CFA9C00893D50FA9C"), new HexPatch(0x00202797, "893D54FA9C00893D58FA9C00893D5CFA9C00893D60FA9C00893D64FA9C00893D68FA9C00893D6CFA9C00893D70FA9C00893D74FA9C00893D78FA9C00893D7CFA9C00893D80FA9C00893D84FA9C00893D88FA9C00893D8CFA9C00893D90FA9C00893D94FA9C00893D98FA9C00893D9CFA9C00893DA0FA9C00893DA4FA9C00893DA8FA9C00893DACFA9C00893DB0FA9C00893DB4FA9C00893DB8FA9C00893DBCFA9C00893DC0FA9C00893DC4FA9C00893DC8FA9C00893DCCFA9C00893DD0FA9C00893DD4FA9C00893DD8FA9C00893DDCFA9C00893DE0FA9C00893DE4FA9C00893DE8FA9C00893DECFA9C00893DF0FA9C00893DF4FA9C00893DF8FA9C00893DFCFA9C00893D00FB9C00893D04FB9C00893D08FB9C00893D0CFB9C00893D10FB9C00893D14FB9C00893D18FB9C00893D1CFB9C00893D20FB9C00893D24FB9C00893D28FB9C00893D2CFB9C00893D30FB9C00893D34FB9C00893D38FB9C00893D3CFB9C00893D40FB9C00893D44FB9C00893D48FB9C00893D4CFB9C00893D50FB9C00893D54FB9C00893D58FB9C00893D5CFB9C00893D60FB9C00893D64FB9C00893D68FB9C00893D6CFB9C00893D70FB9C00893D74FB9C00893D78FB9C00893D7CFB9C00893D80FB9C00893D84FB9C00893D88FB9C00893D8CFB9C00893D90FB9C00893D94FB9C00893D98FB9C00893D9CFB9C00893DA0FB9C00893DA4FB9C00893DA8FB9C00893DACFB9C00893DB0FB9C00893DB4FB9C00893DB8FB9C00893DBCFB9C00893DC0FB9C00893DC4FB9C00893DC8FB9C00893DCCFB9C00893DD0FB9C00893DD4FB9C00893DD8FB9C00893DDCFB9C00893DE0FB9C00893DE4FB9C"), new HexPatch(0x002029F5, "893DE8FB9C00893DECFB9C00893DF0FB9C00893DF4FB9C00893DF8FB9C00893DFCFB9C00893D00FC9C00893D04FC9C00893D08FC9C00893D0CFC9C00893D10FC9C00893D14FC9C00893D18FC9C00893D1CFC9C00893D20FC9C00893D24FC9C00893D28FC9C00893D2CFC9C00893D30FC9C00893D34FC9C00893D38FC9C00893D3CFC9C"), new HexPatch(0x00202A79, "893D40FC9C00893D44FC9C00893D48FC9C00893D4CFC9C00893D50FC9C00893D54FC9C00893D58FC9C00893D5CFC9C00893D60FC9C00893D64FC9C00893D68FC9C00893D6CFC9C00893D70FC9C00893D74FC9C0089") } },
            { "transferwindowpatch", new List<HexPatch> { new HexPatch(0x0000A6AA, "885802C6400318"), new HexPatch(0x0000A6B4, "06"), new HexPatch(0x0000A6C5, "885002C6400306"), new HexPatch(0x0000A6CF, "07"), new HexPatch(0x0000A6DF, "885802C640030EC640"), new HexPatch(0x0000A6E9, "0B"), new HexPatch(0x0000A6FC, "885002C6400304"), new HexPatch(0x0000A706, "02"), new HexPatch(0x0000A9BF, "7E"), new HexPatch(0x0000AA9D, "9C79"), new HexPatch(0x00012CD5, "0C"), new HexPatch(0x00012CE6, "01"), new HexPatch(0x00012D55, "B906000000C600"), new HexPatch(0x00012D5D, "885801884802C640030BC6400405C64005018B460403C1C6"), new HexPatch(0x00012D76, "02885801C6"), new HexPatch(0x00012D7C, "02"), new HexPatch(0x00012D7E, "C6400311C6400403885805884E13"), new HexPatch(0x00024C0B, "24"), new HexPatch(0x00024C0F, "03"), new HexPatch(0x00024C83, "80CAFFB101C60003885801885002884803C64004"), new HexPatch(0x00024C98, "8848058B4604C64006"), new HexPatch(0x00024CA2, "83C006885801885002C640031EC64004058858058B460483C00CC600038848"), new HexPatch(0x00024CC2, "8850"), new HexPatch(0x00024CC5, "884803C64004058848058B4604C640120383C012884801885002"), new HexPatch(0x00024CE1, "031F"), new HexPatch(0x00024CE5, "040B8858058B460483C018C6"), new HexPatch(0x00024CF2, "03C6400102885002884803C64004058848058B460488582383C01E5E5BC60003C6400102885002C640031FC640040281C400020000C3"), new HexPatch(0x00024DB8, "74"), new HexPatch(0x0002516D, "8D44240C52"), new HexPatch(0x000251C3, "70"), new HexPatch(0x00025243, "C8"), new HexPatch(0x0002527D, "70"), new HexPatch(0x000252FD, "C4"), new HexPatch(0x0003F135, "0C"), new HexPatch(0x0003F146, "01"), new HexPatch(0x0003F1B5, "B203B10BC60004885801885002884803884804C64005018B4604C640060483C006885801C6400205C6400308"), new HexPatch(0x0003F1E2, "40040A885805885613"), new HexPatch(0x00118555, "0C"), new HexPatch(0x00118566, "01"), new HexPatch(0x001185D1, "80C9FFC60005885801884802C6400301C6400405C64005"), new HexPatch(0x001185E9, "8B460483C006C60005885801884802C640031EC6400402885805"), new HexPatch(0x00179B85, "0C"), new HexPatch(0x00179B96, "01"), new HexPatch(0x00179C01, "B102C60007885801C64002"), new HexPatch(0x00179C0D, "88"), new HexPatch(0x00179C0F, "03C6"), new HexPatch(0x00179C12, "0405C64005018B460483C006C60007885801C6400204C640031A884804885805"), new HexPatch(0x001A99A5, "0C"), new HexPatch(0x001A99B6, "01"), new HexPatch(0x001A9A21, "80C9FFC60008885801884802C6400310C640040AC64005"), new HexPatch(0x001A9A39, "8B460483C006C60008885801884802C640030FC6400407885805"), new HexPatch(0x001C1670, "06"), new HexPatch(0x001C1674, "1E885004884805"), new HexPatch(0x001C168B, "03"), new HexPatch(0x001C168F, "1F"), new HexPatch(0x001C1693, "07"), new HexPatch(0x001C16A3, "885002884803"), new HexPatch(0x001C16BD, "885002C6400314"), new HexPatch(0x001C16DB, "05"), new HexPatch(0x001C16DF, "15"), new HexPatch(0x001C16E3, "0B"), new HexPatch(0x001C16F7, "C6"), new HexPatch(0x001C16FB, "885002C640030A"), new HexPatch(0x001E008E, "0F"), new HexPatch(0x001E0092, "07"), new HexPatch(0x001E00A8, "C640"), new HexPatch(0x001E00AB, "06"), new HexPatch(0x001E00C5, "C640030F5B81C40002"), new HexPatch(0x001E00CF, "00C39090"), new HexPatch(0x001E0172, "74"), new HexPatch(0x001E01A2, "75"), new HexPatch(0x001E04DD, "8D44240C"), new HexPatch(0x001E0537, "70A098"), new HexPatch(0x001E0575, "C89F98"), new HexPatch(0x001E05B3, "709F98"), new HexPatch(0x001E05F2, "C4"), new HexPatch(0x001ECFAB, "885802C6400316"), new HexPatch(0x001ECFB5, "04"), new HexPatch(0x001ECFCB, "06"), new HexPatch(0x001ECFCF, "1D"), new HexPatch(0x001ECFD3, "07"), new HexPatch(0x001ECFF8, "58"), new HexPatch(0x001ED009, "14"), new HexPatch(0x001F96E5, "0C"), new HexPatch(0x001F96F6, "01"), new HexPatch(0x001F9761, "B105C6000C885801C6400206C6400308884804C64005018B460483C006C6000C885801884802C6400304C6400403885805"), new HexPatch(0x0023C265, "0C"), new HexPatch(0x0023C276, "01"), new HexPatch(0x0023C2E1, "80C9FFC6000D885801884802C640030AC6400404C64005"), new HexPatch(0x0023C2F9, "8B460483C006C6000D885801884802C640031F885804885805"), new HexPatch(0x0026149B, "04"), new HexPatch(0x0026149F, "1B"), new HexPatch(0x002614A3, "09"), new HexPatch(0x002614B9, "02"), new HexPatch(0x002614D3, "1F"), new HexPatch(0x002614D5, "58"), new HexPatch(0x0026150C, "1E"), new HexPatch(0x00261510, "03"), new HexPatch(0x00266CCE, "06"), new HexPatch(0x00266CD2, "0CC640"), new HexPatch(0x00266CD6, "0B"), new HexPatch(0x00266CEC, "885003885004"), new HexPatch(0x00266D05, "06C640031E885004"), new HexPatch(0x00266D27, "1D"), new HexPatch(0x00266D2B, "09"), new HexPatch(0x0026E685, "0C"), new HexPatch(0x0026E696, "01"), new HexPatch(0x0026E701, "B119C6001B885801885802884803C640040AC64005018B460483C006C6001B8858"), new HexPatch(0x0026E723, "C64002FF884803C6400406885805"), new HexPatch(0x00393725, "0C"), new HexPatch(0x00393736, "01"), new HexPatch(0x003937A1, "B906000000C60010885801884802884803C6400404C64005018B460403C1C60010885801C64002FFC6400314C6400402885805"), new HexPatch(0x00394545, "0C"), new HexPatch(0x00394556, "01"), new HexPatch(0x003945C1, "80C9FFC60011885801884802C6400301C6400408C64005"), new HexPatch(0x003945D9, "8B460483C006C60011885801884802C640031FC6400407885805"), new HexPatch(0x00394647, "0F84CE"), new HexPatch(0x0039464C, "00"), new HexPatch(0x00394952, "7499A6"), new HexPatch(0x003949D6, "F898A6"), new HexPatch(0x003CD885, "0C"), new HexPatch(0x003CD896, "01"), new HexPatch(0x003CD901, "80C9FFC60012885801884802C6400301C6400406C64005"), new HexPatch(0x003CD919, "8B460483C006C60012885801884802C6400314C6400401885805"), new HexPatch(0x003D3E87, "05"), new HexPatch(0x003D3E9B, "884803C6400408"), new HexPatch(0x003D3EB7, "0FC640"), new HexPatch(0x003D3EBB, "0B"), new HexPatch(0x003D3ED5, "C640"), new HexPatch(0x003D3ED8, "0F5B81C40002"), new HexPatch(0x003D3EDF, "00C39090"), new HexPatch(0x003EC872, "0F"), new HexPatch(0x003EC876, "0B"), new HexPatch(0x003EC888, "885002C6400318"), new HexPatch(0x003EC892, "02"), new HexPatch(0x003EC8A6, "02"), new HexPatch(0x003EC8AA, "1C"), new HexPatch(0x003EC8AE, "05"), new HexPatch(0x003EC8C6, "885003C6400407"), new HexPatch(0x003F63E5, "0C"), new HexPatch(0x003F63F6, "01"), new HexPatch(0x003F6461, "B21EB104C600158858"), new HexPatch(0x003F646B, "C6400206885003884804C64005"), new HexPatch(0x003F6479, "8B4604C6"), new HexPatch(0x003F647E, "061583C006885801884802885003C6400402885805"), new HexPatch(0x004594FE, "03"), new HexPatch(0x00459502, "11"), new HexPatch(0x00459506, "05"), new HexPatch(0x0045951C, "04"), new HexPatch(0x00459520, "14"), new HexPatch(0x00459524, "07"), new HexPatch(0x0045953B, "0F"), new HexPatch(0x0045953F, "0B"), new HexPatch(0x0045954B, "58"), new HexPatch(0x0045955C, "1F"), new HexPatch(0x004D2893, "C92BC8"), new HexPatch(0x004D2D06, "7610FC"), new HexPatch(0x005020F5, "0C"), new HexPatch(0x00502106, "01"), new HexPatch(0x00502171, "80C9FFC60018885801884802C6400301C6400405C64005"), new HexPatch(0x00502189, "8B460483C006C60018885801884802C640031F885804885805"), new HexPatch(0x00505916, "04"), new HexPatch(0x0050591B, "01"), new HexPatch(0x00505935, "01"), new HexPatch(0x0050593A, "02"), new HexPatch(0x0050B995, "0C"), new HexPatch(0x0050B9A6, "01"), new HexPatch(0x0050BA11, "B107C60019885801C640020388"), new HexPatch(0x0050BA1F, "03885804C64005018B460483C006C600198858"), new HexPatch(0x0050BA33, "C64002FFC640030F884804885805"), new HexPatch(0x0050F0D5, "0C"), new HexPatch(0x0050F0E6, "01"), new HexPatch(0x0050F151, "C6001A885801885802C6400307C6400404C64005018B4604C640061A83C0068858"), new HexPatch(0x0050F173, "C64002FFC6400314C6400402885805"), new HexPatch(0x005CCF56, "446563656D6265722E") } },
            { "manageanyteam", new List<HexPatch> { new HexPatch(0x00082A74, "0F848A020000"), new HexPatch(0x0006A357, "E8E4860100"), new HexPatch(0x00082C9E, "57E8EC9D0B0083C40485C0755957E86F1B0C00"), new HexPatch(0x00082CB6, "744C"), new HexPatch(0x001448AA, "0274") } },
            { "remove3playerlimit", new List<HexPatch> { new HexPatch(0x00179C65, "05") } },
            { "englishleaguenorthawards", new List<HexPatch> { new HexPatch(0x001836C6, "D1"), new HexPatch(0x00213D00, "750B893D"), new HexPatch(0x00213D08, "E93F02"), new HexPatch(0x00213D0C, "00"), new HexPatch(0x00213D1D, "750B893D"), new HexPatch(0x00213D25, "E92202"), new HexPatch(0x00213D29, "00"), new HexPatch(0x00213D3A, "750B893D"), new HexPatch(0x00213D42, "E90502"), new HexPatch(0x00213D46, "00"), new HexPatch(0x00213D57, "750B893D"), new HexPatch(0x00213D5F, "E9E801"), new HexPatch(0x00213D63, "00"), new HexPatch(0x00213D74, "750B893D"), new HexPatch(0x00213D7C, "E9CB01"), new HexPatch(0x00213D80, "00"), new HexPatch(0x00213D91, "750B893D"), new HexPatch(0x00213D99, "E9AE01"), new HexPatch(0x00213D9D, "00"), new HexPatch(0x00232E32, "D1"), new HexPatch(0x0050EAFF, "10F5"), new HexPatch(0x0050ECD9, "8B154CF99C00"), new HexPatch(0x0050ED36, "8B154CF99C00"), new HexPatch(0x0050ED95, "8B154CF99C00"), new HexPatch(0x0050EDF4, "8B154CF99C00"), new HexPatch(0x0050EE53, "8B154CF99C00"), new HexPatch(0x0050EEC2, "8B154CF99C00") } },
            { "englishleaguesouthawards", new List<HexPatch> { new HexPatch(0x001836C6, "D1"), new HexPatch(0x00213D00, "750B893D"), new HexPatch(0x00213D08, "E93F02"), new HexPatch(0x00213D0C, "00"), new HexPatch(0x00213D1D, "750B893D"), new HexPatch(0x00213D25, "E92202"), new HexPatch(0x00213D29, "00"), new HexPatch(0x00213D3A, "750B893D"), new HexPatch(0x00213D42, "E90502"), new HexPatch(0x00213D46, "00"), new HexPatch(0x00213D57, "750B893D"), new HexPatch(0x00213D5F, "E9E801"), new HexPatch(0x00213D63, "00"), new HexPatch(0x00213D74, "750B893D"), new HexPatch(0x00213D7C, "E9CB01"), new HexPatch(0x00213D80, "00"), new HexPatch(0x00213D91, "750B893D"), new HexPatch(0x00213D99, "E9AE01"), new HexPatch(0x00213D9D, "00"), new HexPatch(0x00232E32, "D1"), new HexPatch(0x0050EAFF, "10F5"), new HexPatch(0x0050ECD9, "8B154CF99C00"), new HexPatch(0x0050ED36, "8B154CF99C00"), new HexPatch(0x0050ED95, "8B154CF99C00"), new HexPatch(0x0050EDF4, "8B154CF99C00"), new HexPatch(0x0050EE53, "8B154CF99C00"), new HexPatch(0x0050EEC2, "8B154CF99C00") } },
            { "englishleaguenorthpatch", new List<HexPatch> { new HexPatch(0x006D56B8, "453A5C6465765C434D335C636D332030302D30315C636D335C636F64655C636F6D705C6C6561677565735C77656C5F666972"), new HexPatch(0x002110DC, "E0A39D"), new HexPatch(0x0041DB81, "23"), new HexPatch(0x005EB08B, "3C2573202D20434F4D"), new HexPatch(0x005EB095, "454E54202D20456E676C69736820436F6E666572656E63653E"), new HexPatch(0x0017139B, "64F7"), new HexPatch(0x00139B09, "09"), new HexPatch(0x00139B13, "8D3C32B9060000"), new HexPatch(0x00139B1B, "8BF0"), new HexPatch(0x00139B82, "90909090909090"), new HexPatch(0x00553472, "CCCCCCCCCCCCCCCCCCCCCCCCCCCC"), new HexPatch(0x005534A3, "CCCCCCCCCCCCCCCCCCCCCCCCCC"), new HexPatch(0x005534C2, "CCCCCCCCCCCCCCCCCCCCCCCCCCCC"), new HexPatch(0x005534E2, "CCCCCCCCCCCCCCCCCCCCCCCCCCCC"), new HexPatch(0x00553508, "CCCCCCCCCCCCCCCC"), new HexPatch(0x001751F8, "6A035355E8CFA9F3FF0FBFEB83C40C85ED7E6F8B7C24148B1732C08B4A5785C974090FBED83B4C9C1C7406FEC03C037CED3C03754532DB0FBEC3837C841C007409FEC380FB037CEFEB070FBEC3894C841CA19CF69C006A018D0C408D0CC9C1E1022BC8A1D023AE0003C8518B4C241852E843DE10"), new HexPatch(0x0017526D, "80FB0274084683C7043BF57C958B"), new HexPatch(0x0017527D, "2833F632DB66837F3E007E3C80FB037D37568BCFE83AD410"), new HexPatch(0x00175296, "8078370375200FBED36A018BCF8B449420FEC35056E820D410008B008B4C241850E8E4DE10000FBF4F3E463BF17CC48B54241452E8FBFF3C0083C4045F5E5D5B81C41C020000C3909090"), new HexPatch(0x00525B3C, "2604"), new HexPatch(0x00525B46, "12"), new HexPatch(0x004385CC, "74"), new HexPatch(0x0043861F, "74"), new HexPatch(0x00438672, "74"), new HexPatch(0x00525CD4, "05"), new HexPatch(0x00525C80, "86"), new HexPatch(0x00525C6E, "86"), new HexPatch(0x0035F864, "8B04AA85C0"), new HexPatch(0x0035FA67, "23"), new HexPatch(0x003E833C, "75"), new HexPatch(0x00524E84, "28") } },
            { "restricttactics", new List<HexPatch> { new HexPatch(0x0049A686, "01"), new HexPatch(0x0049A688, "01"), new HexPatch(0x0049A6A6, "02"), new HexPatch(0x0049C6C1, "02"), new HexPatch(0x0049C6C3, "15"), new HexPatch(0x0049C6CB, "01"), new HexPatch(0x0049C6D0, "30"), new HexPatch(0x0049C6F6, "01"), new HexPatch(0x0049C6FB, "30"), new HexPatch(0x0049C6FF, "01"), new HexPatch(0x0049C75F, "03"), new HexPatch(0x0049C761, "15"), new HexPatch(0x0049C769, "01"), new HexPatch(0x0049C76E, "30"), new HexPatch(0x0049A6B3, "74"), new HexPatch(0x0049A83F, "74"), new HexPatch(0x0049ABD9, "0F84BA"), new HexPatch(0x0049ABDE, "00") } },
            { "changegeneraldat", new List<HexPatch> { new HexPatch(0x005C7B84, "67656E6572"), new HexPatch(0x005C7B8A, "6C") } },
            { "changenamecolour", new List<HexPatch> { new HexPatch(0x35E695, "8B4C2414") } },
            { "changeregistrylocation", new List<HexPatch> { new HexPatch(0x005F17A0, "4C") } },
            { "memorycheckfix", new List<HexPatch> { new HexPatch(0x003A1737, "8D040AC1E81483C420C390909090") } },
            { "removemutexcheck", new List<HexPatch> { new HexPatch(0x0028D3B6, "75") } },
            // China Patch -> Can clash with "Round Name Changes.patch" at 0x26a806 - but not to a point where we shouldn't apply (seeing as we can't unapply China anyway). Same goes for transfer windows at 26e696
            { "chinapatch", new List<HexPatch> { new HexPatch(0x0000BC42, "6CF3"), new HexPatch(0x0000BC5F, "A15CF69C"), new HexPatch(0x0000BC91, "A15CF69C"), new HexPatch(0x0000BCC1, "84F4"), new HexPatch(0x0000BCDF, "00FA"), new HexPatch(0x0000BD10, "00FA"), new HexPatch(0x00013F75, "84F4"), new HexPatch(0x0015E110, "84F4"), new HexPatch(0x001BF154, "909090909090909090909090"), new HexPatch(0x001EF533, "84F4"), new HexPatch(0x00268BB6, "84F4"), new HexPatch(0x00268BE8, "02"), new HexPatch(0x00268BEA, "19"), new HexPatch(0x00268C80, "A104FA9C"), new HexPatch(0x0026A4BB, "C74630FF"), new HexPatch(0x0026A4C0, "FFFF"), new HexPatch(0x0026A7E2, "09"), new HexPatch(0x0026A7E4, "15"), new HexPatch(0x0026A7F8, "09"), new HexPatch(0x0026A7FA, "1E"), /*new HexPatch(0x0026A806, "1E"),*/ new HexPatch(0x0026A85C, "09"), new HexPatch(0x0026A85E, "1F"), new HexPatch(0x0026A86E, "01"), new HexPatch(0x0026A870, "05"), new HexPatch(0x0026A873, "0A"), new HexPatch(0x0026A875, "03"), /*new HexPatch(0x0026A882, "28"),*/ new HexPatch(0x0026A8ED, "06"), new HexPatch(0x0026A8F0, "0A"), new HexPatch(0x0026A8F2, "04"), new HexPatch(0x0026A907, "0A"), new HexPatch(0x0026A909, "07"), new HexPatch(0x0026A997, "0A"), new HexPatch(0x0026A999, "08"), new HexPatch(0x0026A9A9, "01"), new HexPatch(0x0026A9AB, "05"), new HexPatch(0x0026A9AE, "0A"), new HexPatch(0x0026A9B0, "11"), new HexPatch(0x0026AA3C, "06"), new HexPatch(0x0026AA3F, "0A"), new HexPatch(0x0026AA41, "12"), new HexPatch(0x0026AA4B, "04"), new HexPatch(0x0026AA52, "01"), new HexPatch(0x0026AA54, "06"), new HexPatch(0x0026AA59, "19"), new HexPatch(0x0026AA68, "AD"), new HexPatch(0x0026AAA9, "AF"), new HexPatch(0x0026AAC0, "01"), new HexPatch(0x0026AAC7, "07"), new HexPatch(0x0026ABB2, "8B1D"), new HexPatch(0x0026ABB5, "FA9C00"), new HexPatch(0x0026ABEF, "16"), new HexPatch(0x0026ABFF, "16"), new HexPatch(0x0026AC4D, "84"), new HexPatch(0x0026AC5A, "3B1500FA9C"), new HexPatch(0x0026B166, "DB06"), new HexPatch(0x0026B16D, "1B"), new HexPatch(0x0026B1F5, "0F85B604"), new HexPatch(0x0026B1FA, "00"), new HexPatch(0x0026B263, "07"), new HexPatch(0x0026B265, "04"), new HexPatch(0x0026B267, "02"), new HexPatch(0x0026B279, "02"), new HexPatch(0x0026B28B, "02"), new HexPatch(0x0026B28D, "02"), new HexPatch(0x0026B291, "07"), new HexPatch(0x0026B293, "08"), new HexPatch(0x0026B295, "03"), new HexPatch(0x0026B2A7, "03"), new HexPatch(0x0026B2BF, "07"), new HexPatch(0x0026B2C1, "0B"), new HexPatch(0x0026B2C3, "04"), new HexPatch(0x0026B2D5, "04"), new HexPatch(0x0026B2DC, "668B4F4083C440"), new HexPatch(0x0026B6B9, "05"), new HexPatch(0x0026B6BB, "10"), new HexPatch(0x0026B6E1, "02"), new HexPatch(0x0026B6E3, "02"), new HexPatch(0x0026B6E7, "05"), new HexPatch(0x0026B6E9, "14"), new HexPatch(0x0026B715, "05"), new HexPatch(0x0026B717, "17"), new HexPatch(0x0026B73D, "02"), new HexPatch(0x0026B73F, "02"), new HexPatch(0x0026B743, "05"), new HexPatch(0x0026B745, "1B"), new HexPatch(0x0026B771, "05"), new HexPatch(0x0026B773, "1E"), new HexPatch(0x0026B79F, "06"), new HexPatch(0x0026B7A1, "07"), new HexPatch(0x0026B7C7, "02"), new HexPatch(0x0026B7C9, "02"), new HexPatch(0x0026B7CD, "06"), new HexPatch(0x0026B7CF, "0B"), new HexPatch(0x0026B7F5, "02"), new HexPatch(0x0026B7FB, "06"), new HexPatch(0x0026B7FD, "0E"), new HexPatch(0x0026B829, "06"), new HexPatch(0x0026B82B, "15"), new HexPatch(0x0026B851, "02"), new HexPatch(0x0026B853, "02"), new HexPatch(0x0026B857, "06"), new HexPatch(0x0026B859, "19"), new HexPatch(0x0026B885, "06"), new HexPatch(0x0026B887, "1C"), new HexPatch(0x0026B8AD, "02"), new HexPatch(0x0026B8AF, "02"), new HexPatch(0x0026B8B3, "07"), new HexPatch(0x0026B8E1, "07"), new HexPatch(0x0026B8E3, "12"), new HexPatch(0x0026B909, "02"), new HexPatch(0x0026B90B, "02"), new HexPatch(0x0026B90F, "07"), new HexPatch(0x0026B93D, "07"), new HexPatch(0x0026B93F, "19"), new HexPatch(0x0026B965, "02"), new HexPatch(0x0026B967, "02"), new HexPatch(0x0026B96B, "07"), new HexPatch(0x0026B96D, "1D"), new HexPatch(0x0026B999, "08"), new HexPatch(0x0026B99B, "01"), new HexPatch(0x0026B9C7, "08"), new HexPatch(0x0026B9C9, "05"), new HexPatch(0x0026B9EF, "02"), new HexPatch(0x0026B9F5, "08"), new HexPatch(0x0026B9F7, "08"), new HexPatch(0x0026BA1F, "02"), new HexPatch(0x0026BA23, "08"), new HexPatch(0x0026BA25, "13"), new HexPatch(0x0026BA4B, "02"), new HexPatch(0x0026BA51, "08"), new HexPatch(0x0026BA53, "16"), new HexPatch(0x0026BA7B, "02"), new HexPatch(0x0026BA7F, "08"), new HexPatch(0x0026BA81, "1A"), new HexPatch(0x0026BAA7, "02"), new HexPatch(0x0026BAAD, "09"), new HexPatch(0x0026BAAF, "0D"), new HexPatch(0x0026BAD7, "02"), new HexPatch(0x0026BADB, "09"), new HexPatch(0x0026BADD, "11"), new HexPatch(0x0026BB03, "02"), new HexPatch(0x0026BB09, "09"), new HexPatch(0x0026BB0B, "14"), new HexPatch(0x0026BB33, "02"), new HexPatch(0x0026BB37, "09"), new HexPatch(0x0026BB39, "18"), new HexPatch(0x0026BB5F, "02"), new HexPatch(0x0026BB67, "1B"), new HexPatch(0x0026BB82, "83C4408BC65F5E81C400020000C210009090909090"), new HexPatch(0x0026BBAC, "4E02"), new HexPatch(0x0026BBB6, "0A"), new HexPatch(0x0026BCF2, "894E"), new HexPatch(0x0026BCF5, "888EC20000"), new HexPatch(0x0026BCFB, "B202888EC70000"), new HexPatch(0x0026BD03, "884E4A8D463A8D8EA90000"), new HexPatch(0x0026BD0F, "8896C40000"), new HexPatch(0x0026BD15, "8896C60000"), new HexPatch(0x0026BD1B, "8856528B1650516AFF8BCEC686C30000"), new HexPatch(0x0026BD2C, "01C6464201C686C50000"), new HexPatch(0x0026BD37, "01C7461CFFFF"), new HexPatch(0x0026BD3F, "C74620FFFF"), new HexPatch(0x0026BD46, "C6464907FF523C8986BA0000"), new HexPatch(0x0026BD53, "B8010000"), new HexPatch(0x0026BD58, "5EC39090"), new HexPatch(0x0026D888, "05"), new HexPatch(0x0026DB91, "01"), new HexPatch(0x0026DB9D, "53516A016A06BF0200000053576A0A5356E8FD42EBFF83C44066897E1866897E1C66895E0766895E0966895E0B66C7460D8300C646170666C7461A010066895E0F66895E1166895E1E885E20C6462101885E22895E5C895E60895E648BC65F5E5B81C4000200"), new HexPatch(0x0026DC04, "C2100090"), new HexPatch(0x0026DCA3, "A100FA9C"), new HexPatch(0x0026DD16, "A104FA9C"), new HexPatch(0x0026DDAC, "A104FA9C"), new HexPatch(0x0026E05F, "84F4"), new HexPatch(0x0026E517, "A100FA9C"), new HexPatch(0x0026E52D, "84F4"), new HexPatch(0x0026E54E, "84F4"), new HexPatch(0x0026E56F, "84F4"), new HexPatch(0x0026E590, "84F4"), new HexPatch(0x0026E5B0, "8B15"), new HexPatch(0x0026E5B3, "FA9C00"), /*new HexPatch(0x0026E701, "B119C6001B885801885802884803C640040AC64005018B460483C006C6001B8858"), new HexPatch(0x0026E723, "C64002FF884803C6400406885805"),*/ new HexPatch(0x0026E73B, "07"), new HexPatch(0x00289C2A, "45"), new HexPatch(0x00289C3B, "34"), new HexPatch(0x003E0753, "84F4"), new HexPatch(0x003E0A6C, "84F4"), new HexPatch(0x00427FF5, "84F4"), new HexPatch(0x00428016, "84F4"), new HexPatch(0x004373C3, "8B15"), new HexPatch(0x004373C6, "FA9C00"), new HexPatch(0x00437418, "8B1504FA9C00"), new HexPatch(0x00437469, "74"), new HexPatch(0x004374BE, "8B15F8F99C00"), new HexPatch(0x0043751A, "A1F8F99C"), new HexPatch(0x004D2E03, "84F4") } },
            { "datecalcpatch", new List<HexPatch> { new HexPatch(0x005662B4, "00000000000000000000"), new HexPatch(0x005662BF, "00000000000000"), new HexPatch(0x005662C7, "0000000000000000"), new HexPatch(0x005662D2, "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"), new HexPatch(0x00566321, "0000000000000000000000000000000000000000000000000000000000000000000000"), new HexPatch(0x00566346, "000000000000"), new HexPatch(0x0056634D, "0000000000"), new HexPatch(0x00566353, "00000000"), new HexPatch(0x00566358, "00000000000000000000000000000000000000"), new HexPatch(0x0056636D, "00") } },
            { "datecalcpatchjumps", new List<HexPatch> { new HexPatch(0x0012C2B0, "E87B230200"), new HexPatch(0x003A41BA, "4A663D0A00"), new HexPatch(0x003A3F77, "4A663D0A00"), new HexPatch(0x003A47D0, "4A663D0A00"), new HexPatch(0x003A4C8C, "8B442414663D0A00"), new HexPatch(0x003A4EE5, "8B4424504F4B663D0A00"), new HexPatch(0x0012C61F, "399C24EC070000"), new HexPatch(0x3A3CFA, "4A663D0A00"), new HexPatch(0x3A4803, "09") } },
            { "comphistory_datecalcpatch", new List<HexPatch> { new HexPatch(0x00139AE9, "8B35D423AE00"), new HexPatch(0x00566F00, "0000000000"), new HexPatch(0x00566F06, "00000000000000"), new HexPatch(0x00566F0E, "0000000000000000000000000000"), new HexPatch(0x00566F1D, "00000000000000000000000000000000"), new HexPatch(0x00566F2E, "00"), new HexPatch(0x00566F30, "0000000000000000000000000000000000000000") } },
            { "tapanieurofix", new List<HexPatch> { new HexPatch(0x0017C699, "730000"), new HexPatch(0x0017C724, "36"), new HexPatch(0x0017C9A9, "36"), new HexPatch(0x0017C9D7, "36"), new HexPatch(0x002021ED, "8CF69C00893D90F69C00893D94F69C00893D98F69C00893D9CF69C"), new HexPatch(0x00202AD0, "FC9C00893D7CFC9C00893D80FC9C00893D84FC9C00893D88FC9C00893D8CFC9C00893D90FC9C00893D94FC9C00893D98FC9C00893D9CFC9C00893DA0FC9C00893DA4FC9C00893DA8FC9C00893DACFC9C00893DB0FC9C00893DB4FC9C00893DB8FC9C"), new HexPatch(0x00202B33, "893DBCFC9C00893DC0FC9C"), new HexPatch(0x00202B3F, "893DC4FC9C00893DC8FC9C00893DCCFC9C00893DD0FC9C00893DD4FC9C00893DD8FC9C00893DDCFC9C00893DE0FC9C00"), new HexPatch(0x00202B70, "3DE4FC9C00893DE8FC9C00893DECFC9C00893DF0FC9C00893DF4FC9C00893DF8FC9C00893DFCFC9C00893D00FD9C0089"), new HexPatch(0x00202BA1, "04FD9C00893D08FD9C00893D0CFD9C00893D10FD9C00893D14FD9C00893D18FD9C00893D1CFD9C00893D20FD9C00893D24FD9C00893D28FD9C00893D2CFD9C00893D30FD9C00893D34FD9C00893D38FD9C00893D3CFD9C00893D40FD9C00893D44FD9C00893D48FD9C00893D4CFD9C00893D50FD9C00893D54FD9C00893D58FD9C00893D5CFD9C00893D60FD9C00893D64FD9C00893D68FD9C00893D6CFD9C00893D70FD9C00893D74FD9C00893D78FD9C00893D7CFD9C00893D80FD9C00893D84FD9C00"), new HexPatch(0x00202C66, "3D88FD9C00893D8CFD9C00893D90FD9C00893D94FD9C00893D98FD9C00"), new HexPatch(0x00202C84, "3D9CFD") } },
            { "positionintacticsview", new List<HexPatch> { new HexPatch(0x00499FF8, "0E"), new HexPatch(0x0049A002, "05"), new HexPatch(0x0049A007, "01"), new HexPatch(0x0049A00C, "3C"), new HexPatch(0x0049A011, "0F"), new HexPatch(0x0049A016, "10"), new HexPatch(0x0049A01B, "07"), new HexPatch(0x0049A020, "07"), new HexPatch(0x0049CC35, "D4"), new HexPatch(0x0049D005, "0175"), new HexPatch(0x0049D047, "7418668B0DECBDAE00516A016A016A006A0155"), new HexPatch(0x0049D05B, "06E9EC100000668B157E31AE00526A016A016A006A01556A06E9D4"), new HexPatch(0x0068BDCC, "696F6E3E0000000063617074000000"), new HexPatch(0x49E03C, "06") } },
            { "addextraspaceheader", new List<HexPatch> { new HexPatch(0x000000FE, "04"), new HexPatch(0x0000014A, "9E"), new HexPatch(0x000001F8, "E550"), new HexPatch(0x00000220, "CEF101"), new HexPatch(0x00000248, "3CD6"), new HexPatch(0x00000270, "3812"), new HexPatch(0x00000290, "0000000000"), new HexPatch(0x0000029A, "00"), new HexPatch(0x0000029D, "0000"), new HexPatch(0x000002A2, "00"), new HexPatch(0x000002A5, "0000"), new HexPatch(0x000002B7, "00") } },
            { "addadditionalcolumns", new List<HexPatch> { new HexPatch(0x0047A536, "C6"), new HexPatch(0x0047A538, "24"), new HexPatch(0x0047A53B, "C644244D02C644244E"), new HexPatch(0x0047A545, "C644244F02"), new HexPatch(0x0047A6EF, "06"), new HexPatch(0x0047AD39, "06"), new HexPatch(0x0047AC9A, "05"), new HexPatch(0x0047A8A5, "2E02"), new HexPatch(0x0047A86A, "0F855E01"), new HexPatch(0x0047A86F, "00"), new HexPatch(0x0047ACB7, "24"), new HexPatch(0x0047ACC7, "FD06"), new HexPatch(0x0047A805, "668954"), new HexPatch(0x0047A809, "24"), new HexPatch(0x0047A81F,"24"), new HexPatch(0x0047A836, "02") } },
            { "makeyourpotential200", new List<HexPatch> { new HexPatch(0x001FBAFE, "8B5424668956368A44246A88463A668B4C246B66894E3B8A54246D88563D8A44246E88463E8A4C246F884E3F"), new HexPatch(0x002B5CD7, "C39090909090"), new HexPatch(0x00202D48, "00893D20FE9C00893D24FE"), new HexPatch(0x00202D3A, "FE9C00893D18FE9C00893D1CFE9C"), new HexPatch(0x00202C88, "00893DA0FD9C00893DA4FD9C"), new HexPatch(0x00202C95, "893DA8FD9C00893DACFD9C00893DB0FD9C00893DB4FD9C00893DB8FD9C00893DBCFD9C00893DC0FD9C00893DC4FD9C00893DC8FD9C00"), new HexPatch(0x00202CCC, "3DCCFD9C00893DD0FD") } },
            { "fixsuperkeepers", new List<HexPatch> { new HexPatch(0x56D80A, "8E") } },
            { "bugfixes", new List<HexPatch> { new HexPatch(0x002EDA3A, "2B"), new HexPatch(0x002EF644, "88861001"), new HexPatch(0x002EF649, "00"), new HexPatch(0x002EF692, "88861101"), new HexPatch(0x002EF697, "00"), new HexPatch(0x001986E3, "8B1E0FBFCF"), new HexPatch(0x00202F05, "893D48FF9C00893D4CFF9C00893D50FF9C00893D54FF9C00") } },
        };

        public void CreateReversePatches(string originalExe)
        {
            using (var fin = File.OpenRead(originalExe))
            using (var br = new BinaryReader(fin))
            {
                Patcher patcher = new Patcher();
                foreach (var patch in patcher.patches)
                {
                    var hexes = string.Format("{{ \"{0}\", new List<HexPatch> {{ ", patch.Key);
                    foreach (var hexPatch in patch.Value)
                    {
                        if (hexPatch.offset < fin.Length)
                        {
                            fin.Seek(hexPatch.offset, SeekOrigin.Begin);
                            var origBytes = br.ReadBytes(hexPatch.hex.Length / 2);
                            var hexString = patcher.BytesToHexString(origBytes);
                            hexes += string.Format("new HexPatch({0}, \"{1}\"), ", "0x" + hexPatch.offset.ToString("X8"), hexString);
                        }
                    }
                    hexes = hexes.Substring(0, hexes.Length - 2);
                    hexes += " } },";
                    Console.WriteLine(hexes);
                }
            }
        }

        // TODO: Refactor these two functions
        public bool DetectPatch(string exeFile, IEnumerable<HexPatch> HexPatches)
        {
            bool ret = false;
            using (var fin = File.Open(exeFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(fin))
            {
                bool matches = true;
                foreach (var hexPatch in HexPatches)
                {
                    if (hexPatch.offset == -1)
                        continue;
                    if (hexPatch.offset < fin.Length)
                    {
                        fin.Seek(hexPatch.offset, SeekOrigin.Begin);
                        byte[] patchBytes = HexStringToBytes(hexPatch.hex);
                        byte[] buffer = br.ReadBytes(patchBytes.Length);
                        if (buffer.Length != patchBytes.Length)
                        {
                            // Clearly a completly wrong exe
                            return false;
                        }
                        for (int i = 0; i < patchBytes.Length; i++)
                        {
                            if (patchBytes[i] != buffer[i])
                            {
                                matches = false;
                                break;
                            }
                        }
                    }
                    else
                        matches = false;
                    if (matches == false)
                        break;
                }
                if (matches)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public List<string> DetectPatches(string exeFile, out short speedHack, out double currencyMultiplier)
        {
            List<string> appliedPatches = new List<string>();
            using (var fin = File.Open(exeFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(fin))
            {
                foreach (var patch in patches)
                {
                    bool matches = true;
                    foreach (var hexPatch in patch.Value)
                    {
                        if (hexPatch.offset == -1)
                            continue;
                        if (hexPatch.offset < fin.Length)
                        {
                            fin.Seek(hexPatch.offset, SeekOrigin.Begin);
                            byte[] patchBytes = HexStringToBytes(hexPatch.hex);
                            byte[] buffer = br.ReadBytes(patchBytes.Length);
                            if (buffer.Length != patchBytes.Length)
                            {
                                // Clearly a completly wrong exe
                                speedHack = 10000;
                                currencyMultiplier = 1.0;
                                return appliedPatches;
                            }
                            for (int i = 0; i < patchBytes.Length; i++)
                            {
                                if (patchBytes[i] != buffer[i])
                                {
                                    matches = false;
                                    break;
                                }
                            }
                        }
                        else
                            matches = false;
                        if (matches == false)
                            break;
                    }
                    if (matches)
                    {
                        appliedPatches.Add(patch.Key);
                    }
                }

                if (fin.Length > (0x5472ce + 2))
                {
                    fin.Seek(0x5472ce, SeekOrigin.Begin);
                    speedHack = br.ReadInt16();

                    fin.Seek(0x5196C1, SeekOrigin.Begin);
                    if (br.ReadByte() == 0x90)
                        currencyMultiplier = 1.0;
                    else
                    {
                        fin.Seek(0x5196C1, SeekOrigin.Begin);
                        currencyMultiplier = br.ReadDouble();
                    }
                }
                else
                {
                    // Clearly a completly wrong exe
                    speedHack = 10000;
                    currencyMultiplier = 1.0;
                    return appliedPatches;
                }
            }

            return appliedPatches;
        }

        public byte[] HexStringToBytes(string hexString)
        {
            byte[] ret = new byte[hexString.Length / 2];
            hexString = hexString.ToLower();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                ret[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return ret;
        }

        public string BytesToHexString(byte [] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public IEnumerable<HexPatch> LoadPatchFile(string patchFile)
        {
            using (var fs = File.Open(patchFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                return LoadPatchFile(fs);
            }
        }

        public List<string> patcherCommands = new List<string> { "TAPANISPACEPATCH", "APPLYMISCPATCH", "APPLYEXTERNALPATCH", "EXPANDEXE", "PATCHCLUBCOMP", "RENAMECLUB",
                                                                 "CHANGECLUBDIVISION", "CHANGECLUBLASTDIVISION", "CHANGECLUBLASTPOSITION", "CHANGECLUBNATION", 
                                                                 "CHANGENATIONCOMPNAME", "CHANGENATIONCOMPCOLOR", "CLEARNATIONCOMPHISTORY", 
                                                                 "ADDNATIONCOMPHISTORY", "DELETECLUBCOMPHISTORY", "DELETENATIONCOMPHISTORY", "SHIFTNATIONCOMPHISTORY",
                                                                 "CHANGENATIONCONTINENT"
        };

        Dictionary<string, List<HexPatch>> GetCommands(IEnumerable<HexPatch> patch)
        {
            Dictionary<string, List<HexPatch>> commandDictionary = new Dictionary<string, List<HexPatch>>();
            foreach (var command in patcherCommands)
            {
                commandDictionary[command] = patch.Where(x => x.offset == -1 && (x.command.ToUpper().StartsWith(command))).ToList();
            }
            return commandDictionary;
        }

        public IEnumerable<HexPatch> LoadPatchFile(Stream patchStream)
        {
            var patchList = new List<HexPatch>();
            bool inMultiLineComment = false;
            using (var sr = new StreamReader(patchStream))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                        break;
                    if (line.Contains("/*") && !line.Contains("*/"))
                        inMultiLineComment = true;
                    if (inMultiLineComment)
                    {
                        if (line.Contains("*/"))
                            inMultiLineComment = false;
                        continue;
                    }
                    if (string.IsNullOrEmpty(line) || line.StartsWith("/") || line.StartsWith("#"))
                        continue;
                    var parts = ParseTokens(line);
                    parts[0] = parts[0].Replace(":", "");
                    try
                    {
                        if (patcherCommands.Contains(parts[0].ToUpper()))
                        {
                            patchList.Add(new HexPatch(parts[0].ToUpper(), (parts.Count > 1) ? parts[1] : null, (parts.Count > 2) ? parts[2] : null, (parts.Count > 3) ? parts[3] : null, (parts.Count > 4) ? parts[4] : null, (parts.Count > 5) ? parts[5] : null, (parts.Count > 6) ? parts[6] : null));
                        }
                        else
                        {
                            var offset = Convert.ToInt32(parts[0], 16);
                            var from = Convert.ToByte(parts[1], 16);
                            var to = Convert.ToByte(parts[2], 16);
                            patchList.Add(new HexPatch(offset, string.Format("{0:x02}", to), string.Format("{0:x02}", from)));
                        }
                    }
                    catch { }
                }
            }
#if DEBUG
            // Dupe Finder
            var dupes = patchList.Where(x => x.offset != -1).GroupBy(x => x.offset).Where(x => x.Count() > 1).Select(y => y.Key).ToList();
            if (dupes.Count > 0)
            {
                foreach (var dupe in dupes)
                {
                    Console.WriteLine("{0:X}", dupe);
                }
                Console.WriteLine("DUPES! {0}", dupes.Count);
            }
#endif
            return patchList;
        }

        // Frickin' hate RegEx so doing this old skool
        List<string> ParseTokens(string line)
        {
            List<string> ret = new List<string>();
            string s = "";
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }
                if (!inQuotes && (line[i] == ' ' || line[i] == '\t'))
                {
                    if (s != "")
                        ret.Add(s);
                    s = "";
                }
                else
                    s += line[i];
            }
            if (s != "")
                ret.Add(s);
            return ret;
        }

        // Random Function: 0090CFC0
        // 006DC000 in the file will equal 00DE7000 in memory = 70B000 Difference
        // DE7000 = Extra Attributes Patch (with text at DE8000)
        // DE7300 = Subs Control (9+5 but not England)
        // DE7350 = World Cup NULL Pointer crash (from Fairedinkum notes)
        // DE7375 - DE737D = DoubleHeapMemory.patch 
        // DE7383 - DE7470 = Derby County 12 point deduction (gives a bit of space after)
        // DE7470 - DE74A0 = Floating point clamping patch (with a little space)
        // DE74B0 - DE74C3 = Null News Item Protection Patch
        // DE74C4 - DE74E2 = Better teams in Asia Cup patch
        // DE74F0 - DE75FF = World Cup 2022 - Qatar in Nov/Dec
        // DE7600 - DE7629 = "Irish Football Association Challenge Cup" <-- weird fix where name patching doesnt work
        // DE7630 - DE7648 = Protection patch for danny_bhoy67 crash - squad_manager.cpp
        // DE7650 - DE7677 = Protection patch for vascobenny and staff_records when a player is to transfer but has already left the club they are transferring from
        // DE7680 - DE7698 = Luke trasfer_offer Protection Patch
        // DE76A0 - DE76B8 = woerd86_netherlands_protection_patch.patch
        // DE76C0 - DE76DD = Font Size Changing Code


        public void ExpandExe(string fileName)
        {
            ApplyPatch(fileName, patches["addextraspaceheader"]);
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                if (file.Length < 0x8DC000)
                    file.SetLength(0x8DC000);
                Logger.Log(fileName, "Applying ExpandExe to {0}", fileName);
            }
        }

        public void UnApplyPatch(string fileName, IEnumerable<HexPatch> patch)
        {
            // UnApply patches
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    foreach (var hexpatch in patch)
                    {
                        if (hexpatch.offset == -1)
                        {
                            Logger.Log(fileName, "UnApplying {0} {1} to {2}", hexpatch.command, hexpatch.part1, fileName);

                            if (hexpatch.command.ToUpper().StartsWith("APPLYMISCPATCH"))
                            {
                                MiscPatches.ApplyMiscPatch(fileName, hexpatch.part1, true);
                            }
                        }
                        else
                        {
                            bw.Seek(hexpatch.offset, SeekOrigin.Begin);
                            bw.Write(HexStringToBytes(hexpatch.oldhex));
                        }
                    }
                }
            }
        }

        // Main apply
        public void ApplyPatch(string fileName, IEnumerable<HexPatch> patch)
        {
            // Check if we need to expand the exe
            if (patch.Where(x => x.offset == -1 && x.command.ToUpper().StartsWith("EXPANDEXE")).Count() >= 1)
                ExpandExe(fileName);

            if (patch.Where(x => x.offset == -1 && x.command.ToUpper().StartsWith("TAPANISPACEPATCH")).Count() >= 1)
                ApplyPatch(fileName, patches["tapanispacemaker"]);

            try
            {
                // Apply patches
                using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var bw = new BinaryWriter(file))
                    {
                        foreach (var hexpatch in patch)
                        {
                            if (hexpatch.offset == -1)
                            {
                                Logger.Log(fileName, "Applying {0} {1} to {2}", hexpatch.command, hexpatch.part1, fileName);

                                if (hexpatch.command.ToUpper().StartsWith("APPLYMISCPATCH"))
                                {
                                    PatcherForm.updatingForm.SetUpdateText(hexpatch.part1);
                                    MiscPatches.ApplyMiscPatch(fileName, hexpatch.part1);
                                }
                                if (hexpatch.command.ToUpper().StartsWith("APPLYEXTERNALPATCH"))
                                {
                                    Patcher patcher = new Patcher();
                                    PatcherForm.updatingForm.SetUpdateText(hexpatch.part1);
                                    patcher.ApplyPatch(fileName, hexpatch.part1);
                                }
                            }
                            else
                            {
                                bw.Seek(hexpatch.offset, SeekOrigin.Begin);
                                bw.Write(HexStringToBytes(hexpatch.hex));
                            }
                        }
                    }
                }

                HistoryLoader hl = null;
                bool historyLoaderRequired = false;
                bool saveNationData = false;
                string indexFile = null;

                var commandDictionary = GetCommands(patch);
                var clubDivisionChanges = patch.Where(x => x.offset == -1 && (x.command.ToUpper().StartsWith("CHANGECLUBDIVISION") || x.command.ToUpper().StartsWith("CHANGECLUBLASTDIVISION"))).ToList();

                // The first 6 commands don't need a history loader, but if we have any after that, load one up
                for (int i = 6; i < commandDictionary.Keys.Count; i++)
                { 
                    if (commandDictionary[commandDictionary.Keys.ElementAt(i)].Count > 0)
                    {
                        historyLoaderRequired = true;
                        break;
                    }
                }
                
                if (historyLoaderRequired)
                {
                    PatcherForm.updatingForm.SetUpdateText("Loading Data Files...");
                    hl = new HistoryLoader();
                    var dir = Path.GetDirectoryName(fileName);
                    var dataDir = Path.Combine(dir, "Data");
                    indexFile = Path.Combine(dataDir, "index.dat");
                    hl.Load(indexFile);
                    PatcherForm.updatingForm.SetUpdateText("Data Files Loaded.");
                }

                // CHANGECLUBDIVISION + CHANGECLUBLASTDIVISION
                if (clubDivisionChanges.Count > 0)
                {
                    foreach (var clubDivisionChange in clubDivisionChanges)
                    {
                        var clubName = clubDivisionChange.part1;
                        var divisionName = clubDivisionChange.part2;
                        var tClub = hl.club.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == clubName);
                        var tDivision = hl.club_comp.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == divisionName);
                        if (tDivision != null)
                        {
                            if (tClub != null)
                            {
                                if (tClub.ID != 0 && tDivision.ID != 0)
                                {
                                    PatcherForm.updatingForm.SetUpdateText(clubDivisionChange.command + " " + clubName + " " + divisionName);

                                    if (clubDivisionChange.command.ToUpper().StartsWith("CHANGECLUBDIVISION"))
                                    {
                                        hl.UpdateClubsDivision(tClub.ID, tDivision.ID);
                                    }
                                    else
                                    {
                                        hl.UpdateClubsLastDivision(tClub.ID, tDivision.ID);
                                    }
                                }
                            }
                            else
                                Console.WriteLine("Club ({0}) in clubDivisionChanges not found!!! (Division: {1})", clubName, divisionName);
                        }
                        else
                            Console.WriteLine("Division ({0}) in clubDivisionChanges not found!!! (Club: {1})", divisionName, clubName);
                    }
                }

                // CHANGECLUBLASTPOSITION
                if (commandDictionary["CHANGECLUBLASTPOSITION"].Count > 0)
                {
                    foreach (var clubLastPositionChange in commandDictionary["CHANGECLUBLASTPOSITION"])
                    {
                        var clubName = clubLastPositionChange.part1;
                        var newPosition = int.Parse(clubLastPositionChange.part2);
                        var tClub = hl.club.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == clubName);
                        if (tClub != null)
                        {
                            Logger.Log(fileName, "CHANGECLUBLASTPOSITION " + clubLastPositionChange.command + " " + clubName + " " + newPosition);
                            PatcherForm.updatingForm.SetUpdateText(clubLastPositionChange.command + " " + clubName + " " + newPosition);
                            hl.UpdateClubsLastPosition(tClub.ID, newPosition);
                        }
                        else
                            Console.WriteLine("Cannot find club: ({0}) in CHANGECLUBLASTPOSITION", clubName);
                    }
                }

                // CHANGECLUBNATION
                foreach (var clubNationChange in commandDictionary["CHANGECLUBNATION"])
                {
                    var clubName = clubNationChange.part1;
                    var nationName = clubNationChange.part2;
                    var tClub = hl.club.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == clubName);
                    var tNation = hl.nation.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == nationName);
                    if (tClub.ID != 0 && tNation.ID != 0)
                    {
                        PatcherForm.updatingForm.SetUpdateText(clubNationChange.command + " " + clubName + " " + nationName);
                        Logger.Log(fileName, "CHANGECLUBNATION " + clubNationChange.command + " " + clubName + " " + nationName);
                        hl.UpdateClubsNation(tClub.ID, tNation.ID);
                    }

                }

                // CHANGENATIONCOMPNAME
                foreach (var nationCompNameChange in commandDictionary["CHANGENATIONCOMPNAME"])
                {
                    saveNationData = true;
                    var nationCompName = nationCompNameChange.part1;
                    var newNationCompName = nationCompNameChange.part2;
                    var newNationCompNameShort = nationCompNameChange.part3;
                    var newContinentId = nationCompNameChange.part4;
                    var tNationComp = hl.nation_comp.FirstOrDefault(x => x.Name.ReadString() == nationCompName);
                    if (tNationComp.ID != 0)
                    {
                        int? newContinentIdTemp = null;
                        if (!string.IsNullOrEmpty(newContinentId))
                            newContinentIdTemp = int.Parse(newContinentId);
                        PatcherForm.updatingForm.SetUpdateText(nationCompNameChange.command + " " + nationCompName + " " + newNationCompName + " " + newNationCompNameShort);
                        Logger.Log(fileName, "CHANGENATIONCOMPNAME " + nationCompNameChange.command + " " + nationCompName + " " + newNationCompName + " " + newNationCompNameShort);
                        hl.UpdateNationCompName(tNationComp.ID, newNationCompName, newNationCompNameShort, newContinentIdTemp);
                    }
                }

                // CHANGENATIONCOMPCOLOR
                foreach (var nationCompColorChange in commandDictionary["CHANGENATIONCOMPCOLOR"])
                {
                    saveNationData = true;
                    var nationCompName = nationCompColorChange.part1;
                    var newNationCompForegroundColor = nationCompColorChange.part2;
                    var newNationCompBackgroundColor = nationCompColorChange.part3;
                    var tNationComp = hl.nation_comp.FirstOrDefault(x => x.Name.ReadString() == nationCompName);
                    if (tNationComp.ID != 0)
                    {
                        int newNationCompForegroundColorTemp, newNationCompBackgroundColorTemp;
                        if (int.TryParse(newNationCompForegroundColor, out newNationCompForegroundColorTemp) && int.TryParse(newNationCompBackgroundColor, out newNationCompBackgroundColorTemp))
                        {
                            PatcherForm.updatingForm.SetUpdateText(nationCompColorChange.command + " " + nationCompName + " " + newNationCompForegroundColor + " " + newNationCompBackgroundColor);
                            Logger.Log(fileName, "CHANGENATIONCOMPCOLOR " + nationCompColorChange.command + " " + nationCompName + " " + newNationCompForegroundColor + " " + newNationCompBackgroundColor);
                            hl.UpdateNationCompColor(tNationComp.ID, newNationCompForegroundColorTemp, newNationCompBackgroundColorTemp);
                        }
                    }
                }

                // CLEARNATIONCOMPHISTORY
                foreach (var nationCompClearHistoryItem in commandDictionary["CLEARNATIONCOMPHISTORY"])
                {
                    var nationCompName = nationCompClearHistoryItem.part1;
                    var tNationComp = hl.nation_comp.FirstOrDefault(x => x.Name.ReadString() == nationCompName);
                    if (tNationComp.ID != 0)
                    {
                        PatcherForm.updatingForm.SetUpdateText(nationCompClearHistoryItem.command + " " + nationCompName);
                        Logger.Log(fileName, "CLEARNATIONCOMPHISTORY " + nationCompClearHistoryItem.command + " " + nationCompName);
                        hl.ClearNationCompHistory(tNationComp.ID);
                    }
                }

                // DELETECLUBCOMPHISTORY
                foreach (var deleteClubCompHistoryItem in commandDictionary["DELETECLUBCOMPHISTORY"])
                {
                    var clubCompName = deleteClubCompHistoryItem.part1;
                    int year;
                        
                    if (int.TryParse(deleteClubCompHistoryItem.part2, out year))
                    {
                        PatcherForm.updatingForm.SetUpdateText(deleteClubCompHistoryItem.command + " " + clubCompName + " " + year);
                        Logger.Log(fileName, "DELETECLUBCOMPHISTORY " + deleteClubCompHistoryItem.command + " " + clubCompName + " " + year);
                        var tClubComp = hl.club_comp.FirstOrDefault(x => x.Name.ReadString() == clubCompName);
                        if (tClubComp.ID != 0)
                        {
                            hl.club_comp_history.RemoveAll(x => x.Comp == tClubComp.ID && x.Year == year);
                        }
                    }
                }

                // DELETENATIONCOMPHISTORY
                foreach (var deleteNationCompHistoryItem in commandDictionary["DELETENATIONCOMPHISTORY"])
                {
                    var nationCompName = deleteNationCompHistoryItem.part1;
                    int year;

                    if (int.TryParse(deleteNationCompHistoryItem.part2, out year))
                    {
                        PatcherForm.updatingForm.SetUpdateText(deleteNationCompHistoryItem.command + " " + nationCompName + " " + year);
                        Logger.Log(fileName, "DELETENATIONCOMPHISTORY " + deleteNationCompHistoryItem.command + " " + nationCompName + " " + year);
                        var tNationComp = hl.nation_comp.FirstOrDefault(x => x.Name.ReadString() == nationCompName);
                        if (tNationComp.ID != 0)
                        {
                            hl.nation_comp_history.RemoveAll(x => x.Comp == tNationComp.ID && x.Year == year);
                        }
                    }
                }

                // ADDNATIONCOMPHISTORY (code put after the delete because else it will add then delete them all :) )
                foreach (var nationCompAddHistoryItem in commandDictionary["ADDNATIONCOMPHISTORY"])
                {
                    int year;
                    if (int.TryParse(nationCompAddHistoryItem.part2, out year))
                    {
                        var nationCompName = nationCompAddHistoryItem.part1;
                        var winner = nationCompAddHistoryItem.part3;
                        var runner_up = nationCompAddHistoryItem.part4;
                        var host = nationCompAddHistoryItem.part5;
                        var third_place = nationCompAddHistoryItem.part6;

                        PatcherForm.updatingForm.SetUpdateText(nationCompAddHistoryItem.command + " " + nationCompName + " " + winner + " " + runner_up + " " + host + " " + third_place);
                        Logger.Log(fileName, "ADDNATIONCOMPHISTORY " + nationCompAddHistoryItem.command + " " + nationCompName + " " + winner + " " + runner_up + " " + host + " " + third_place);

                        hl.AddNationCompHistory(nationCompName, year, winner, runner_up, host, third_place);
                    }
                }

                // SHIFTNATIONCOMPHISTORY (Shift the years of the national comps)
                // e.g. SHIFTNATIONCOMPHISTORY -4
                foreach (var nationCompShiftdHistoryItem in commandDictionary["SHIFTNATIONCOMPHISTORY"])
                {
                    int yearShift;
                    if (int.TryParse(nationCompShiftdHistoryItem.part2, out yearShift))
                    {
                        var dir = Path.GetDirectoryName(fileName);
                        var dataDir = Path.Combine(dir, "Data");
                        var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");

                        YearChanger yearChanger = new YearChanger();

                        PatcherForm.updatingForm.SetUpdateText(nationCompShiftdHistoryItem.command + " " + yearShift);
                        Logger.Log(fileName, "SHIFTNATIONCOMPHISTORY " + nationCompShiftdHistoryItem.command + " " + yearShift);

                        yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, yearShift, 0x8);

                    }
                }

                // CHANGENATIONCONTINENT (Mainly for changing Australia to Asia)
                foreach (var changeNationContinent in commandDictionary["CHANGENATIONCONTINENT"])
                {
                    saveNationData = true;
                    var Australia = hl.nation.FirstOrDefault(x => x.Name.ReadString() == "Australia");
                    var AsiaContinent = hl.continent.FirstOrDefault(x => x.ContinentName.ReadString() == "Asia");
                    Australia.Continent = AsiaContinent.ContinentID;
                    Logger.Log(fileName, "CHANGENATIONCONTINENT");
                }

                if (hl != null)
                hl.Save(indexFile, true, false, saveNationData);

                // Patch Club Competition Names
                var clubCompNameChanges = patch.Where(x => x.offset == -1 && (x.command.ToUpper().StartsWith("PATCHCLUBCOMP"))).ToList();
                if (clubCompNameChanges.Count > 0)
                {
                    var dir = Path.GetDirectoryName(fileName);
                    var dataDir = Path.Combine(dir, "Data");
                    NamePatcher np = new NamePatcher(fileName, dataDir);
                    np.FindFreePos();
                    foreach (var clubNameChange in clubCompNameChanges)
                    {
                        PatcherForm.updatingForm.SetUpdateText(clubNameChange.command + " " + clubNameChange.part1);
                        Logger.Log(fileName, "PATCHCLUBCOMP " + clubNameChange.command + " " + clubNameChange.part1);
                        np.PatchClubComp(clubNameChange.part1, clubNameChange.part2, clubNameChange.part3, clubNameChange.part4, clubNameChange.part5);
                    }
                }

                // Rename Clubs
                // e.g.
                // RENAMECLUB "Original Club Full Name" "New Club Full Name" ["New Club Short Name"]
                var clubReNameChanges = patch.Where(x => x.offset == -1 && (x.command.ToUpper().StartsWith("RENAMECLUB"))).ToList();
                if (clubReNameChanges.Count > 0)
                {
                    var dir = Path.GetDirectoryName(fileName);
                    var dataDir = Path.Combine(dir, "Data");
                    var clubs = MiscFunctions.ReadFile<TClub>(Path.Combine(dataDir, "club.dat"));
                    foreach (var clubReNameChange in clubReNameChanges)
                    {
                        PatcherForm.updatingForm.SetUpdateText(clubReNameChange.command + " " + clubReNameChange.part1);
                        Logger.Log(fileName, "RENAMECLUB " + clubReNameChange.command + " " + clubReNameChange.part1);
                        var idx = clubs.FindIndex(x => MiscFunctions.GetTextFromBytes(x.Name) == clubReNameChange.part1);
                        if (idx != -1)
                        {
                            var tempClub = clubs[idx];
                            tempClub.Name = MiscFunctions.GetBytesFromText(clubReNameChange.part2, 51);
                            if (!string.IsNullOrEmpty(clubReNameChange.part3))
                                tempClub.ShortName = MiscFunctions.GetBytesFromText(clubReNameChange.part3, 26);
                            clubs[idx] = tempClub;
                        }
                    }
                    MiscFunctions.SaveFile<TClub>(Path.Combine(dataDir, "club.dat"), clubs);
                }
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
                return;
            }
        }

        // Used by APPLYEXTERNALPATCH
        public void ApplyPatch(string fileName, string patchFile)
        {
            var patch = LoadPatchFile(patchFile);
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    foreach (var hexpatch in patch)
                    {
                        if (hexpatch.offset == -1)
                            continue;
                        bw.Seek(hexpatch.offset, SeekOrigin.Begin);
                        bw.Write(HexStringToBytes(hexpatch.hex));
                    }
                }
            }
        }

        // Only used for internal patching
        public void ApplyPatch(Stream stream, IEnumerable<HexPatch> patch)
        {
            foreach (var hexpatch in patch)
            {
                if (hexpatch.offset == -1)
                    continue;
                var bytes = HexStringToBytes(hexpatch.hex);
                stream.Seek(hexpatch.offset, SeekOrigin.Begin);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void ApplyPatch(string fileName, int pos, string hexBytes)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    bw.Seek(pos, SeekOrigin.Begin);
                    bw.Write(HexStringToBytes(hexBytes));
                }
            }
        }

        public void CurrencyInflationChanger(string fileName, double multiplier)
        {
            Logger.Log(fileName, "Applying CurrencyInflationChanger - Multiplier: {0}", multiplier.ToString());
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

            var patch = new HexPatch(0x566A00, "FF74240468146A96005589E583E4F8E9E28DADFFDD05C1969100DC0DB89CAD00DD1DB89CAD0083C404C3");
            var jmpPatch = new HexPatch(0x3F7F0, "E90B72520090");
            ApplyPatch(fileName, new HexPatch[] { patch, jmpPatch });
        }

        public void CurrencyInflationChanger0001(string fileName, double multiplier)
        {
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var br = new BinaryReader(file))
                {
                    // Write Multiplier
                    using (var bw = new BinaryWriter(file))
                    {
                        file.Seek(0x4e6a2a, SeekOrigin.Begin);
                        bw.Write(multiplier);
                    }
                }
            }

            var patch = new HexPatch(0x4e6a00, "FF74240468146A8E005589E583E4F8E932B2B4FFDD052A6A8E00DC0D7055A300DD1D7055A30083C404C3");
            var jmpPatch = new HexPatch(0x031c40, "E9BB4D4B0090");
            ApplyPatch(fileName, new HexPatch[] { patch, jmpPatch });
        }

        public void SpeedHack(string fileName, short speed)
        {
            Logger.Log(fileName, "Applying SpeedHack: {0}", speed.ToString());
            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    file.Seek(0x5472cd, SeekOrigin.Begin);
                    bw.Write((byte)0x68);       // Write a DWORD push as Tapani sometimes changes this in his patch
                    bw.Write((int)speed);       // Force to DWORD (again to stop Tapani nonsense)
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

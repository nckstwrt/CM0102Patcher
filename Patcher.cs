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
            { "idlesensitivity", new List<HexPatch> { new HexPatch(926778, "85d27507668b15de6bdd0083c2fc83fa2c0f87c4080000e81a3d480090"), new HexPatch(5534421, "79ee01"), new HexPatch(5660960, "60689c1597"), new HexPatch(5660966, "ff15387196"), new HexPatch(5660972, "85c07417684c6196"), new HexPatch(5660981, "50ff15b87096"), new HexPatch(5660988, "85c07407ff74242490ffd061c204"), new HexPatch(5661003, "90536c656570"), new HexPatch(5661010, "fe0de67098"), new HexPatch(5661016, "750ec605e67098"), new HexPatch(5661024, "216a14e818"), new HexPatch(5661032, "e95379feff9090906a60909090e8a6ffffff33dbc390909060a19c189f"), new HexPatch(5661062, "85c07524689c1597"), new HexPatch(5661071, "ff15387196"), new HexPatch(5661077, "85c0741b684c6196"), new HexPatch(5661086, "50ff15b87096"), new HexPatch(5661093, "85c0740ba39c189f"), new HexPatch(5661102, "ff742424ffd061c204"), new HexPatch(5661112, "600fb7461283c01c662b05922cae"), new HexPatch(5661127, "807f0f0f937e1a8a472ae822"), new HexPatch(5661142, "28472a8a473af6d8e815"), new HexPatch(5661155, "0410"), new HexPatch(5661158, "473ae83b"), new HexPatch(5661165, "e872"), new HexPatch(5661170, "61c3909090903c9c537e07e8be6dfaff5bc333db6a0de8b36dfaff2ad86a0de8aa6dfaff2ad86a0de8a16dfaff2ad89383c40c5bc3908a4739e8c8ffffff040d2847398a4724e8bbffffff04102847248a471ee8aeffffff041028471e8a4743e8a1ffffff0408284743c3909090909090908b461a85c074478b407185c074408b"), new HexPatch(5661300, "3b0508fa9c"), new HexPatch(5661306, "75366a02e83d6dfaff85c058752a6a04e8316dfaff"), new HexPatch(5661328, "471b"), new HexPatch(5661331, "472e6a06e8246dfaff"), new HexPatch(5661341, "4736"), new HexPatch(5661344, "473d6a08e8176dfaff"), new HexPatch(5661354, "4734"), new HexPatch(5661357, "473c83c40cc3") } },
            { "idlesensitivitytransferscreen", new List<HexPatch> { new HexPatch(5162819, "e9339d070090"), new HexPatch(5661819, "e8f0fcffff0fbfc80fbfd7e9be62f8ff") } },
            { "disablecdremove", new List<HexPatch> { new HexPatch(4368779, "9090909090"), new HexPatch(4383744, "9090909090") } },
            { "disablesplashscreen", new List<HexPatch> { new HexPatch(1887548, "e97203000090") } },
            { "disableunprotectedcontracts", new List<HexPatch> { new HexPatch(1199314, "68d1770000") } },
            { "sevensubs", new List<HexPatch> { new HexPatch(1503780, "c6404907c20800"), new HexPatch(1519120, "07"), new HexPatch(1526787, "c64649075ec3"), new HexPatch(1547764, "eb"), new HexPatch(1533953, "07"), new HexPatch(1540444, "07"), new HexPatch(1391244, "07"), new HexPatch(1819788, "07"), new HexPatch(4139590, "07"), new HexPatch(4155094, "eb"), /* French 7 Subs */ new HexPatch(0x1bc48c, "07"), /* FA Tropy */ new HexPatch(0x170C6B, "66C746490503"), /* Charity Shield */ new HexPatch(0x16d3f0, "06") } },
            { "showstarplayers",  new List<HexPatch> { new HexPatch(374828, "9090") } },
            { "hideprivatebids", new List<HexPatch> { new HexPatch(5051539, "e90a01000090") } },
            { "allowclosewindow", new List<HexPatch> { new HexPatch(2676552, "E9E7812B000000") } },
            { "forceloadallplayers", new List<HexPatch> { new HexPatch(0x1255ff, "6683B8800000000190"), new HexPatch(0x125637, "6683B8800000000190"), new HexPatch(0x1269f1, "34080000") } },
            { "regenfixes", new List<HexPatch> { new HexPatch(0x3a6f48, "7c"), new HexPatch(0x3abeab, "e92d0500"), new HexPatch(0x3abeb0, "90") } },
            { "to1280x800", new List<HexPatch> { new HexPatch(11134, "ff04"), new HexPatch(11141, "ff04"), new HexPatch(11150, "1f03"), new HexPatch(14413, "2003"), new HexPatch(14628, "2003"), new HexPatch(14775, "2003"), new HexPatch(15106, "ff04"), new HexPatch(15205, "ff04"), new HexPatch(15326, "1f03"), new HexPatch(15425, "1f03"), new HexPatch(367138, "baeb1e60"), new HexPatch(367143, "9090"), new HexPatch(367163, "0c"), new HexPatch(376856, "08"), new HexPatch(394268, "1f03"), new HexPatch(394273, "ff04"), new HexPatch(462766, "1f03"), new HexPatch(462771, "ff04"), new HexPatch(469719, "1f03"), new HexPatch(469724, "ff04"), new HexPatch(718220, "e42b15"), new HexPatch(718448, "002b15"), new HexPatch(1438411, "1f03"), new HexPatch(1438416, "ff04"), new HexPatch(1438440, "1f03"), new HexPatch(1438445, "ff04"), new HexPatch(1438606, "1f03"), new HexPatch(1438611, "ff04"), new HexPatch(1439084, "1f03"), new HexPatch(1439089, "ff04"), new HexPatch(1439118, "1f03"), new HexPatch(1439123, "ff04"), new HexPatch(1439212, "1f03"), new HexPatch(1439217, "ff04"), new HexPatch(1440144, "e92f250a"), new HexPatch(1440149, "909081ec00020000568bf1"), new HexPatch(1440672, "e813230a"), new HexPatch(1446562, "ff04"), new HexPatch(1446569, "ff04"), new HexPatch(1446603, "1f03"), new HexPatch(1446610, "1f03"), new HexPatch(1642275, "1f03"), new HexPatch(1642283, "ff04"), new HexPatch(1716932, "e86fec05009090"), new HexPatch(1716943, "50e853ec050090"), new HexPatch(1717237, "15"), new HexPatch(1718132, "e8bfe705005090"), new HexPatch(1718142, "28"), new HexPatch(1718147, "90e8afe7050090"), new HexPatch(1718443, "15"), new HexPatch(1719502, "48e864e2050050"), new HexPatch(1719515, "90e847e2050090"), new HexPatch(1719814, "15"), new HexPatch(1720896, "e8f3dc05005090"), new HexPatch(1720907, "90e8d7dc050090"), new HexPatch(1721206, "15"), new HexPatch(1722020, "d2"), new HexPatch(1722030, "33ffebf690"), new HexPatch(1722068, "15"), new HexPatch(1722104, "15"), new HexPatch(1722154, "d2"), new HexPatch(1722163, "83ef15ebf79090"), new HexPatch(1722367, "d2"), new HexPatch(1722377, "33ffebf6"), new HexPatch(1722412, "15"), new HexPatch(1722415, "33ffebfa"), new HexPatch(1722448, "15"), new HexPatch(1722454, "eb2f"), new HexPatch(1722494, "d2"), new HexPatch(1722503, "83ef15ebf79090"), new HexPatch(1722982, "d2"), new HexPatch(1722988, "33ffebfa"), new HexPatch(1723090, "15"), new HexPatch(1723093, "33ffebfa"), new HexPatch(1723199, "15"), new HexPatch(1723208, "eb76"), new HexPatch(1723316, "d2"), new HexPatch(1723328, "83ef15ebf79090"), new HexPatch(1723670, "d2"), new HexPatch(1723676, "33ffebfa"), new HexPatch(1723778, "15"), new HexPatch(1723887, "15"), new HexPatch(1723896, "eb76"), new HexPatch(1724004, "d2"), new HexPatch(1724016, "83ef15ebf700"), new HexPatch(1931758, "2003"), new HexPatch(1931763, "0005"), new HexPatch(1934918, "2003"), new HexPatch(1934923, "0005"), new HexPatch(1934982, "2003"), new HexPatch(1934987, "0005"), new HexPatch(1983880, "10"), new HexPatch(1984274, "60"), new HexPatch(1984276, "60"), new HexPatch(1989866, "6a"), new HexPatch(1989868, "eb4290"), new HexPatch(1989872, "6a"), new HexPatch(1989874, "eb3c90"), new HexPatch(1989878, "6a"), new HexPatch(1989880, "eb3690"), new HexPatch(1989884, "6a"), new HexPatch(1989886, "eb3090"), new HexPatch(1989890, "6a"), new HexPatch(1989892, "eb2a90"), new HexPatch(1989896, "6a"), new HexPatch(1989898, "eb2490"), new HexPatch(1989902, "6a"), new HexPatch(1989904, "eb1e90"), new HexPatch(1989922, "9869c801050000ff348dac1dae00e853c10100c39090"), new HexPatch(1996623, "ada70100"), new HexPatch(1996683, "7ba70100"), new HexPatch(1996719, "4da70100"), new HexPatch(1996771, "2da70100"), new HexPatch(1996813, "f9a60100"), new HexPatch(1996838, "f4a60100"), new HexPatch(1996988, "0005"), new HexPatch(1997030, "0005"), new HexPatch(1997038, "2003"), new HexPatch(1997682, "69c200050000"), new HexPatch(1997693, "909090"), new HexPatch(1997786, "69c000050000"), new HexPatch(1997797, "909090"), new HexPatch(1997879, "69c000050000"), new HexPatch(1997890, "909090"), new HexPatch(1997993, "69c3000500008b5c2414"), new HexPatch(1998008, "909090"), new HexPatch(1998185, "69c000050000"), new HexPatch(1998195, "909090"), new HexPatch(1998296, "69c300050000"), new HexPatch(1998306, "909090"), new HexPatch(1998394, "69c300050000"), new HexPatch(1998404, "909090"), new HexPatch(1998513, "69c000050000"), new HexPatch(1998523, "909090"), new HexPatch(1999605, "2003"), new HexPatch(1999621, "2003"), new HexPatch(1999626, "0005"), new HexPatch(2021291, "60"), new HexPatch(2021293, "60"), new HexPatch(2023926, "ff04"), new HexPatch(2023947, "1f03"), new HexPatch(2036936, "1f03"), new HexPatch(2036944, "ff04"), new HexPatch(2104905, "9090908b44240483f85a7f0869c05e0100"), new HexPatch(2104923, "eb0e83e85a69c09f01000005007f0000c1f808c2040090e821000000c38b44240469c055010000c1f808c2040052b8ffffffbff764240892405ac20400608d74242c8bfe6a0259ad50e8a3ffffffabad50e8c7ffffffabe2ee61c39090e8dbffffffa10c29ae00c390e8cfffffff668b91a0e91200f644240402740b837c2430027d04ff442430e9b0daf5ff"), new HexPatch(2105067, "06051e1006051e1006051e10050101050101050101e86dffffffe9e63efeffe863ffffffe96c4efeffe859ffffffe9321cfeffe84fffffffe9880ffeff60936a155b99f7fb408944241c61c3905393e8e9ffffff5bc390608d7424288bfe6a0259ad83c035abad50e820ffffffabe2f161668b91a0e912"), new HexPatch(2105187, "e92fdaf5ff81c40c020000e825ffffff668b6c240481ec0c020000c390"), new HexPatch(2105715, "9060ff742428ff742428e84e39feff5a5a69c02003000099f7"), new HexPatch(2105741, "f3795d"), new HexPatch(2105746, "44241c61c39060ff742428ff742428e82a39feff5a5a0faf05f3795d"), new HexPatch(2105775, "99b9200300"), new HexPatch(2105781, "f7f98944241c61c3909090"), new HexPatch(2676949, "0005"), new HexPatch(2676960, "2003"), new HexPatch(3248966, "1f03"), new HexPatch(3529745, "1f03"), new HexPatch(3531116, "02"), new HexPatch(3705395, "02"), new HexPatch(3705478, "02"), new HexPatch(3706895, "ab04"), new HexPatch(4196504, "e8cb16e0ff909090"), new HexPatch(4230775, "1f03"), new HexPatch(4230780, "ff04"), new HexPatch(4308242, "1f03"), new HexPatch(4308247, "ff04"), new HexPatch(4314845, "15"), new HexPatch(4332829, "5068251d8200eb06060106010601"), new HexPatch(4332851, "06"), new HexPatch(4333410, "05"), new HexPatch(4335005, "50b8f71e60009090"), new HexPatch(4335017, "90"), new HexPatch(4335027, "09"), new HexPatch(4335678, "06"), new HexPatch(4656334, "8b"), new HexPatch(4656983, "8b"), new HexPatch(4673762, "81"), new HexPatch(4834854, "02"), new HexPatch(4834974, "02"), new HexPatch(4835132, "02"), new HexPatch(4910804, "2003"), new HexPatch(4910809, "0005"), new HexPatch(4910844, "2003"), new HexPatch(4910849, "0005"), new HexPatch(4954740, "1f03"), new HexPatch(4954748, "ff04"), new HexPatch(4958808, "1f03"), new HexPatch(4958816, "ff04"), new HexPatch(6043424, "626b6731323830"), new HexPatch(6043432, "383030"), new HexPatch(6043436, "72676e"), new HexPatch(6382665, "3830302e6d627200"), new HexPatch(6662093, "383030") } },
            { "tapanispacemaker", new List<HexPatch> { new HexPatch(1235195, "355f"), new HexPatch(2111540, "81ec200200"), new HexPatch(2111546, "5355565751b9ec04000083c8ffbf78f19c"), new HexPatch(2111564, "f3ab6a1a59bf9c3cb600f3ab59a19423ae"), new HexPatch(2111582, "33db33f63bc3") } },
            { "findallplayers", new List<HexPatch> { new HexPatch(0x3afc4b, "e99e00"), new HexPatch(0x3afc50, "90") } },
            { "jobsabroadboost", new List<HexPatch> { new HexPatch(0x29ea36, "eb"), new HexPatch(0x29d315, "eb"), new HexPatch(0x29d665, "eb"), new HexPatch(0x29d6e4, "eb"), new HexPatch(0x29ea7e, "eb") } },
            { "tapaninewregencode", new List<HexPatch> { new HexPatch(2105632, "608b0d6c23ae008b35c423ae0033c00fb6560703c283c63ee2f599f7356c23ae00a2e673980061c3"), new HexPatch(2106524, "e87ffcffffa0e673980084c074f2c3"), new HexPatch(2106552, "608b6c243055ff742430ff742430ff7424308a1c2fe8b40000000fb6142f3ad374208b44242c483ac27517526a64e8150000"), new HexPatch(2106603, "5a3b4424247d08e82f0000"), new HexPatch(2106615, "88042f61c210009090"), new HexPatch(3854240, "e85b57e5ff56e815bcd8ff83c408eb04"), new HexPatch(3854258, "88015e"), new HexPatch(2106624, "6905306cad006d4ec64105393000"), new HexPatch(2106639, "a3306cad0033d2c1e81066f77424040fb7c2c20400909060526a02e8d1ffffffd1e05a488bd86a00594180f9147d25515268e80300"), new HexPatch(2106693, "e8b6ffffff5a3d760300007d0ee2eb03d380fa017e0580fa147cd7598954241c61c39090"), new HexPatch(2106753, "9090909090608b6c24308a142f3a54242c7c173a5424287f116a64e81faa30005a3b4424247703fe0c2f61c21000900fbe142f03d083fa7d7c02b27c88142fc3"), new HexPatch(2106818, "0fbe142f2bd083fa837f02b28388142fc3905d6a006a346a2e6a1b6a406a366a2d6a276a266a336a2b6a386a256a376a436a316a426a446a396a326a3e6a1e6a246a1d6a3a6a356a2a6a21ffe590609384db75156a0ae8a3a930"), new HexPatch(2106909, "936a0be89ba930004383c40802d86a095933d2526a16e888a9300083c4045a3ac37f0142e2edd1e242526a09e872a9300083c4045a3ad37d073c057d0142eb043c057df98954241c61c3909090"), new HexPatch(2106992, "ff356423ae00e845a930006bc06e5a0305bc23ae003878187f118b706185f6740a56e809"), new HexPatch(2107029, "000085d87504e2d333f6c38b442404608d700f33d26a0759acd1e23c127c0142e2f68954241c61c204"), new HexPatch(2107071, "906a0158600fb746070fb75f072bc33bc2720cf7d83bc2720633c08944241c61c3526a64e8d8a830005a5a3bc27d0b8a042fe81affffff88042fc390"), new HexPatch(2107135, "90608b7e6185ff750261c3807f07787c466a7f5966bbff32e854ffffff85f674e86a325ae898ffffff4875e58a56238857238a56288857288a56308857308b56388957388b57418957416a0c59578d760f8d7f0ff3a45f57e844ffffff93e872feffff5d85ed7442b900400000b719e8fdfeffff85f674eb6a195ae841ffffff85c074eb56e817ffffff3ac3b0037502040250e829a830"), new HexPatch(2107287, "5a408a142e88142f5d85ed74054875f2ebbe6a06e850fdffffe85bfeffff88472f6a2f6a076a146a2be8c1fdffff6a065ae859fdffff8847218ac3b20542d0e072fb75fae846fdffff8847426a2e5d6a465ae8f2feffff6a2e6a0c6a146a42e8bdfcffff6a2d5d6a245ae8dafeffff6a2d6a096a146a3de8a5fcffff6a3d5d8a042f3c087f086a505ae8bbfeffff6a3d6a0d6a146a56e886fcffff6a345d8a042f3c097f086a505ae89cfeffff6a346a0f6a146a1be867fcffff6a2c5db20ae8cbfcffff88042f6a165ae87afeffff90906a1e5d6a03e84ea730005a0401e848fdffff6a245d6a025a803c2fd07c0380c20552e871fcffff9090e82cfdffff6a405d6a205ae83ffeffff6a406a0e6a146a4ee80afcffff6a235d803c2fe87f0c6a18e842fcffffe8edfcffff6a05e836fcffffe8f3fcffff6a426a076a14906a1b6a0f6a146a1fe8d5fbffff6a1d6a0d6a146a17e8c8fbffff9090906a366a0f6a146a22e8b8fbffff6a435d6a03e8b6a630005ae8a0fcffff6a3f5d8a042f3c077d08e8f1fcffff88042f6a0c5ae8fcfbffff8847446a58e884fbffff90807f0f027c430fb65707c1ea0442e8defbffff8847446a08e8aefbffff85c0750c8a572a8a473a88472a88573a6a04e897fbffff02d0e8b6fbffff884740e898fcffff88471c90eb266a2a5d6a02e878fbffffe835fcffff6a355de82dfcffff6a3a5d6a03e861fbffffe81efcffffe8f8faffff041e38470772336a32e849fbffff85c07435e8e1faffff043c384707721c6a19e832fbffff85c0741ee8cafaffff044a3847077707b0be38470772636a05e814fbffff85c075586a1b5db9"), new HexPatch(2107893, "080000b71ee871fcffff85f6740d8a570780ea0f3856077707ebea6a0158eb126a05e8e4faffff408a142e38142f7f0388142f4583fd2d74fa83fd2574f583fd457d8f487fe26a02e8befaffff85c075e2ebabe854faffff384707777a6a19e8a7faffff85c07417e83ffaffff2c1e38470877636a0ee890faffff85c075586a1b5db9"), new HexPatch(2108025, "080000b71ee8edfbffff85f6740d8a570780c2383856077207ebea6a0158eb136a05e860faffff408a142e38142f7c0388142f4583fd2d74fa83fd2574f583fd457d96487fe26a02e83afaffff85c075e2ebab61c3") } },
            { "transferwindowpatch", new List<HexPatch> { new HexPatch(42666, "66c74002ff1390"), new HexPatch(42676, "05"), new HexPatch(42693, "66c74002ff0a90"), new HexPatch(42703, "08"), new HexPatch(42719, "66c74002ff19908858"), new HexPatch(42729, "90"), new HexPatch(42748, "66c74002ff1890"), new HexPatch(42758, "01"), new HexPatch(43455, "eb"), new HexPatch(43677, "48a1"), new HexPatch(77013, "18"), new HexPatch(77030, "02"), new HexPatch(77141, "6a02598908c740"), new HexPatch(77149, "ff1c0601894806c74008ff130900b50189480cc7400eff04"), new HexPatch(77174, "01894812c7"), new HexPatch(77180, "14"), new HexPatch(77182, "010100c646130690909090909090"), new HexPatch(150539, "18"), new HexPatch(150543, "02"), new HexPatch(150659, "c60003885801c64002ffc6400301c6400406c640"), new HexPatch(150680, "018b460483c006c600"), new HexPatch(150690, "885801c64002ffc6400301c64004088858058b4604c6400c0383c00cc64001"), new HexPatch(150722, "c640"), new HexPatch(150725, "ffc6400301885804c64005018b460483c0125ec60003c6400101"), new HexPatch(150753, "02ff"), new HexPatch(150757, "0301c64004018858055b81c4"), new HexPatch(150770, "020000c39090909090909090909090909090909090909090909090909090909090909090909090909090909090909090909090909090"), new HexPatch(150968, "eb"), new HexPatch(151917, "e989000000"), new HexPatch(152003, "20"), new HexPatch(152131, "20"), new HexPatch(152189, "20"), new HexPatch(152317, "70"), new HexPatch(258357, "18"), new HexPatch(258374, "02"), new HexPatch(258485, "6a04598908c74002ff1c0001894806c74008ff140300b50189480cc7400eff140501894812c74014ff130600"), new HexPatch(258530, "461305c64613329090"), new HexPatch(1148245, "18"), new HexPatch(1148262, "02"), new HexPatch(1148369, "6a05598908c74002ff010601894806c74008ff010800b5"), new HexPatch(1148393, "89480cc7400eff120001894812c74014ff0f0100909090909090"), new HexPatch(1547141, "18"), new HexPatch(1547158, "02"), new HexPatch(1547265, "6a07598908c74002ff0106"), new HexPatch(1547277, "89"), new HexPatch(1547279, "06c7"), new HexPatch(1547282, "08ff010800b50189480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(1743269, "18"), new HexPatch(1743286, "02"), new HexPatch(1743393, "6a08598908c74002ff0c0101894806c74008ff050400b5"), new HexPatch(1743417, "89480cc7400eff050701894812c74014ff020800909090909090"), new HexPatch(1840752, "ff"), new HexPatch(1840756, "0166c740040601"), new HexPatch(1840779, "ff"), new HexPatch(1840783, "01"), new HexPatch(1840787, "08"), new HexPatch(1840803, "66c74002ff01"), new HexPatch(1840829, "66c74002ff1f90"), new HexPatch(1840859, "ff"), new HexPatch(1840863, "01"), new HexPatch(1840867, "00"), new HexPatch(1840887, "c7"), new HexPatch(1840891, "ff010190909090"), new HexPatch(1966222, "01"), new HexPatch(1966226, "08"), new HexPatch(1966248, "8858"), new HexPatch(1966251, "90"), new HexPatch(1966277, "66c7400301015b81c4"), new HexPatch(1966287, "020000c3"), new HexPatch(1966450, "eb"), new HexPatch(1966498, "eb"), new HexPatch(1967325, "eb529090"), new HexPatch(1967415, "90ce9c"), new HexPatch(1967477, "e8aa9e"), new HexPatch(1967539, "e8aa9e"), new HexPatch(1967602, "70"), new HexPatch(2019243, "66c74002ff0190"), new HexPatch(2019253, "06"), new HexPatch(2019275, "ff"), new HexPatch(2019279, "01"), new HexPatch(2019283, "08"), new HexPatch(2019320, "48"), new HexPatch(2019337, "01"), new HexPatch(2070245, "18"), new HexPatch(2070262, "02"), new HexPatch(2070369, "6a0c598908c74002ff010601894806c74008ff010800b50189480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(2343525, "18"), new HexPatch(2343542, "02"), new HexPatch(2343649, "6a0d598908c74002ff010b01894806c74008ff160100b5"), new HexPatch(2343673, "89480cc7400eff010601894812c74014ff1f06009090909090"), new HexPatch(2495643, "ff"), new HexPatch(2495647, "01"), new HexPatch(2495651, "08"), new HexPatch(2495673, "01"), new HexPatch(2495699, "01"), new HexPatch(2495701, "48"), new HexPatch(2495756, "1f"), new HexPatch(2495760, "02"), new HexPatch(2518222, "ff"), new HexPatch(2518226, "098858"), new HexPatch(2518230, "90"), new HexPatch(2518252, "66c740030103"), new HexPatch(2518277, "ff66c740031b0590"), new HexPatch(2518311, "1b"), new HexPatch(2518315, "06"), new HexPatch(2549381, "18"), new HexPatch(2549398, "02"), new HexPatch(2549505, "6a1b598908c74002ff050001894806c74008ff1c0200b50189480cc7400eff1e05"), new HexPatch(2549539, "894812c74014ff1d060090909090"), new HexPatch(3749669, "18"), new HexPatch(3749686, "02"), new HexPatch(3749793, "6a10598908c74002ff010601894806c74008ff010800b50189480cc7400eff010001894812c74014ff01010090909090909090"), new HexPatch(3753285, "18"), new HexPatch(3753302, "02"), new HexPatch(3753409, "6a11598908c74002ff080001894806c74008ff1f0200b5"), new HexPatch(3753433, "89480cc7400eff160601894812c74014ff120700909090909090"), new HexPatch(3753543, "e9cf00"), new HexPatch(3753548, "90"), new HexPatch(3754322, "20a098"), new HexPatch(3754454, "209f98"), new HexPatch(3987589, "18"), new HexPatch(3987606, "02"), new HexPatch(3987713, "6a12598908c74002ff010601894806c74008ff010800b5"), new HexPatch(3987737, "89480cc7400eff010101894812c74014ff010200909090909090"), new HexPatch(4013703, "06"), new HexPatch(4013723, "66c74003160890"), new HexPatch(4013751, "018858"), new HexPatch(4013755, "90"), new HexPatch(4013781, "8848"), new HexPatch(4013784, "8848045b81c4"), new HexPatch(4013791, "020000c3"), new HexPatch(4114546, "09"), new HexPatch(4114550, "05"), new HexPatch(4114568, "66c74002ff0190"), new HexPatch(4114578, "08"), new HexPatch(4114598, "ff"), new HexPatch(4114602, "1b"), new HexPatch(4114606, "00"), new HexPatch(4114630, "66c740031a0190"), new HexPatch(4154341, "18"), new HexPatch(4154358, "02"), new HexPatch(4154465, "6a15598908c74002ff"), new HexPatch(4154475, "0601894806c74008ff010800b5"), new HexPatch(4154489, "89480cc7"), new HexPatch(4154494, "0eff010001894812c74014ff010100909090909090"), new HexPatch(4560126, "ff"), new HexPatch(4560130, "01"), new HexPatch(4560134, "06"), new HexPatch(4560156, "ff"), new HexPatch(4560160, "01"), new HexPatch(4560164, "08"), new HexPatch(4560187, "01"), new HexPatch(4560191, "00"), new HexPatch(4560203, "48"), new HexPatch(4560220, "01"), new HexPatch(5056659, "390eec"), new HexPatch(5057798, "e617ec"), new HexPatch(5251317, "18"), new HexPatch(5251334, "02"), new HexPatch(5251441, "6a18598908c74002ff010601894806c74008ff010800b5"), new HexPatch(5251465, "89480cc7400eff010001894812c74014ff0101009090909090"), new HexPatch(5265686, "ff"), new HexPatch(5265691, "04"), new HexPatch(5265717, "ff"), new HexPatch(5265722, "04"), new HexPatch(5290389, "18"), new HexPatch(5290406, "02"), new HexPatch(5290513, "6a19598908c74002ff12010189"), new HexPatch(5290527, "06c74008ff0b0400b50189480cc7400eff0406"), new HexPatch(5290547, "894812c74014ff03070090909090"), new HexPatch(5304533, "18"), new HexPatch(5304550, "02"), new HexPatch(5304657, "6a1a598908c74002ff010601894806c74008ff010800b50189480cc7400eff0100"), new HexPatch(5304691, "894812c74014ff0101009090909090"), new HexPatch(6082390, "4a616e756172792e00") } },
            { "manageanyteam", new List<HexPatch> { new HexPatch(0x82a74, "909090909090"), new HexPatch(435031, "9090909090"), new HexPatch(535710, "8b74241c368b86cf00000085c07557eb099090"), new HexPatch(535734, "744c"), new HexPatch(0x1448aa, "0075") } },
            { "remove3playerlimit", new List<HexPatch> { new HexPatch(0x179c65, "01") } },
            { "englishleaguenorthawards", new List<HexPatch> { new HexPatch(1586886, "e1"), new HexPatch(2178304, "9090c705"), new HexPatch(2178312, "5c0100"), new HexPatch(2178316, "90"), new HexPatch(2178333, "9090c705"), new HexPatch(2178341, "5d0100"), new HexPatch(2178345, "90"), new HexPatch(2178362, "9090c705"), new HexPatch(2178370, "5e0100"), new HexPatch(2178374, "90"), new HexPatch(2178391, "9090c705"), new HexPatch(2178399, "5f0100"), new HexPatch(2178403, "90"), new HexPatch(2178420, "9090c705"), new HexPatch(2178428, "600100"), new HexPatch(2178432, "90"), new HexPatch(2178449, "9090c705"), new HexPatch(2178457, "610100"), new HexPatch(2178461, "90"), new HexPatch(2305586, "e1"), new HexPatch(5303039, "e4f2"), new HexPatch(5303513, "ba6801000090"), new HexPatch(5303606, "ba6801000090"), new HexPatch(5303701, "ba6801000090"), new HexPatch(5303796, "ba6801000090"), new HexPatch(5303891, "ba6801000090"), new HexPatch(5304002, "ba6801000090") } },
            { "englishleaguesouthawards", new List<HexPatch> { new HexPatch(1586886, "e1"), new HexPatch(2178304, "9090c705"), new HexPatch(2178312, "5c0100"), new HexPatch(2178316, "90"), new HexPatch(2178333, "9090c705"), new HexPatch(2178341, "5d0100"), new HexPatch(2178345, "90"), new HexPatch(2178362, "9090c705"), new HexPatch(2178370, "5e0100"), new HexPatch(2178374, "90"), new HexPatch(2178391, "9090c705"), new HexPatch(2178399, "5f0100"), new HexPatch(2178403, "90"), new HexPatch(2178420, "9090c705"), new HexPatch(2178428, "600100"), new HexPatch(2178432, "90"), new HexPatch(2178449, "9090c705"), new HexPatch(2178457, "610100"), new HexPatch(2178461, "90"), new HexPatch(2305586, "e1"), new HexPatch(5303039, "e4f2"), new HexPatch(5303513, "ba6701000090"), new HexPatch(5303606, "ba6701000090"), new HexPatch(5303701, "ba6701000090"), new HexPatch(5303796, "ba6701000090"), new HexPatch(5303891, "ba6701000090"), new HexPatch(5304002, "ba6701000090") } },
            { "englishleaguenorthpatch", new List<HexPatch> { new HexPatch(7165624, "656e676c697368206e6f72746865726e207072656d696572206c6561677565207072656d696572206469766973696f6e0000"), new HexPatch(2167004, "b856ad"), new HexPatch(4316033, "22"), new HexPatch(6205579, "733c2573202d20434f"), new HexPatch(6205589, "4d454e54202d204c6f776572206469766973696f6e733e0000"), new HexPatch(1512347, "4cf9"), new HexPatch(1284873, "78"), new HexPatch(1284883, "e85a994100b906"), new HexPatch(1284891, "0000"), new HexPatch(1284994, "8d3c328bf0eb8f"), new HexPatch(5583986, "8d3c328b35e4f29c003b31eb24c3"), new HexPatch(5584035, "8bf075d880b91c01000004eb12"), new HexPatch(5584066, "7cbb9052a110f59c002b0190eb12"), new HexPatch(5584098, "ba22010000f7e203c85a8bc6eb18"), new HexPatch(5584136, "c6811c01000001c3"), new HexPatch(1528312, "a1fcadad008bb8a00500008bb0740100000fb68ec10000000fb6563e2bd14951528bcee8b0d410006a01ff7704ff308b4c2420e870df10005951518bcfe896d410006a01ff7604ff308b4c2420e866de10005941e2c2eb9d508b4424083b054cf99c0074063b059cf69c0058c20400803dfa7c98"), new HexPatch(1528429, "00740ac605fa7c9800003bc0c383"), new HexPatch(1528445, "18f9c38b0285c074548b400485c0744d8b405d85c074468b"), new HexPatch(1528470, "3b05e4f29c00753c807f0c0a7c3609ed7503fe470c83fd0a7c03fe4f0c83fd057c2274068b44aafceb1da14cf99c00c1e0020305fcadad008b00eb0b90909090909090908b04aa85c0c3"), new HexPatch(5397308, "4d05"), new HexPatch(5397318, "17"), new HexPatch(4425164, "eb"), new HexPatch(4425247, "eb"), new HexPatch(4425330, "eb"), new HexPatch(5397716, "07"), new HexPatch(5397632, "96"), new HexPatch(5397614, "8e"), new HexPatch(3536996, "e8175ae1ff"), new HexPatch(0x35FA67, "22"), new HexPatch(4096828, "eb") } },
            { "restricttactics", new List<HexPatch> { new HexPatch(4826758, "00"), new HexPatch(4826760, "00"), new HexPatch(4826790, "00"), new HexPatch(4835009, "00"), new HexPatch(4835011, "00"), new HexPatch(4835019, "00"), new HexPatch(4835024, "00"), new HexPatch(4835062, "00"), new HexPatch(4835067, "00"), new HexPatch(4835071, "00"), new HexPatch(4835167, "00"), new HexPatch(4835169, "00"), new HexPatch(4835177, "00"), new HexPatch(4835182, "00"), new HexPatch(4826803, "eb"), new HexPatch(4827199, "eb"), new HexPatch(4828121, "e9bb00"), new HexPatch(4828126, "90") } },
            { "changegeneraldat", new List<HexPatch> { new HexPatch(6060932, "6e6f636865"), new HexPatch(6060938, "74") } },
            { "changeregistrylocation", new List<HexPatch> { new HexPatch(0x5f17a0, "41") } },
            { "memorycheckfix", new List<HexPatch> { new HexPatch(0x3a1737, "c1ea14c1e9148d041183c420c390") } },
            { "removemutexcheck", new List<HexPatch> { new HexPatch(0x28d3b6, "eb") } },
            { "chinapatch", new List<HexPatch> { new HexPatch(48194, "98f2"), new HexPatch(48223, "b8c20000"), new HexPatch(48273, "b8c20000"), new HexPatch(48321, "6cf3"), new HexPatch(48351, "5cf6"), new HexPatch(48400, "5cf6"), new HexPatch(81781, "98f2"), new HexPatch(1433872, "98f2"), new HexPatch(1831252, "c74630ffffffffc6464907c3"), new HexPatch(2028851, "98f2"), new HexPatch(2526134, "98f2"), new HexPatch(2526184, "01"), new HexPatch(2526186, "11"), new HexPatch(2526336, "b8d40000"), new HexPatch(2532539, "e8944cf5"), new HexPatch(2532544, "9090"), new HexPatch(2533346, "03"), new HexPatch(2533348, "16"), new HexPatch(2533368, "04"), new HexPatch(2533370, "01"), new HexPatch(2533382, "28"), new HexPatch(2533468, "04"), new HexPatch(2533470, "02"), new HexPatch(2533486, "02"), new HexPatch(2533488, "02"), new HexPatch(2533491, "04"), new HexPatch(2533493, "1d"), new HexPatch(2533506, "32"), new HexPatch(2533613, "03"), new HexPatch(2533616, "04"), new HexPatch(2533618, "1e"), new HexPatch(2533639, "06"), new HexPatch(2533641, "18"), new HexPatch(2533783, "06"), new HexPatch(2533785, "19"), new HexPatch(2533801, "02"), new HexPatch(2533803, "02"), new HexPatch(2533806, "07"), new HexPatch(2533808, "15"), new HexPatch(2533948, "03"), new HexPatch(2533951, "07"), new HexPatch(2533953, "16"), new HexPatch(2533963, "00"), new HexPatch(2533970, "02"), new HexPatch(2533972, "04"), new HexPatch(2533977, "01"), new HexPatch(2533992, "af"), new HexPatch(2534057, "ad"), new HexPatch(2534080, "02"), new HexPatch(2534087, "1c"), new HexPatch(2534322, "bbc2"), new HexPatch(2534325, "000090"), new HexPatch(2534383, "11"), new HexPatch(2534399, "11"), new HexPatch(2534477, "66"), new HexPatch(2534490, "81fac20000"), new HexPatch(2535782, "9e07"), new HexPatch(2535789, "1e"), new HexPatch(2535925, "e9b70400"), new HexPatch(2535930, "90"), new HexPatch(2536035, "09"), new HexPatch(2536037, "1a"), new HexPatch(2536039, "1b"), new HexPatch(2536057, "1b"), new HexPatch(2536075, "01"), new HexPatch(2536077, "05"), new HexPatch(2536081, "0a"), new HexPatch(2536083, "09"), new HexPatch(2536085, "1c"), new HexPatch(2536103, "1c"), new HexPatch(2536127, "0a"), new HexPatch(2536129, "17"), new HexPatch(2536131, "1d"), new HexPatch(2536149, "1d"), new HexPatch(2536156, "e9a60800009090"), new HexPatch(2537145, "02"), new HexPatch(2537147, "02"), new HexPatch(2537185, "01"), new HexPatch(2537187, "05"), new HexPatch(2537191, "02"), new HexPatch(2537193, "09"), new HexPatch(2537237, "02"), new HexPatch(2537239, "1e"), new HexPatch(2537277, "01"), new HexPatch(2537279, "05"), new HexPatch(2537283, "03"), new HexPatch(2537285, "06"), new HexPatch(2537329, "03"), new HexPatch(2537331, "0d"), new HexPatch(2537375, "03"), new HexPatch(2537377, "14"), new HexPatch(2537415, "01"), new HexPatch(2537417, "05"), new HexPatch(2537421, "03"), new HexPatch(2537423, "1b"), new HexPatch(2537461, "01"), new HexPatch(2537467, "04"), new HexPatch(2537469, "04"), new HexPatch(2537513, "04"), new HexPatch(2537515, "0b"), new HexPatch(2537553, "01"), new HexPatch(2537555, "05"), new HexPatch(2537559, "04"), new HexPatch(2537561, "12"), new HexPatch(2537605, "04"), new HexPatch(2537607, "19"), new HexPatch(2537645, "01"), new HexPatch(2537647, "05"), new HexPatch(2537651, "05"), new HexPatch(2537697, "05"), new HexPatch(2537699, "0f"), new HexPatch(2537737, "01"), new HexPatch(2537739, "05"), new HexPatch(2537743, "05"), new HexPatch(2537789, "05"), new HexPatch(2537791, "1d"), new HexPatch(2537829, "01"), new HexPatch(2537831, "05"), new HexPatch(2537835, "06"), new HexPatch(2537837, "06"), new HexPatch(2537881, "06"), new HexPatch(2537883, "0d"), new HexPatch(2537927, "06"), new HexPatch(2537929, "11"), new HexPatch(2537967, "01"), new HexPatch(2537973, "06"), new HexPatch(2537975, "14"), new HexPatch(2538015, "05"), new HexPatch(2538019, "06"), new HexPatch(2538021, "1b"), new HexPatch(2538059, "01"), new HexPatch(2538065, "07"), new HexPatch(2538067, "03"), new HexPatch(2538107, "05"), new HexPatch(2538111, "07"), new HexPatch(2538113, "0a"), new HexPatch(2538151, "01"), new HexPatch(2538157, "07"), new HexPatch(2538159, "11"), new HexPatch(2538199, "05"), new HexPatch(2538203, "08"), new HexPatch(2538205, "0e"), new HexPatch(2538243, "01"), new HexPatch(2538249, "08"), new HexPatch(2538251, "15"), new HexPatch(2538291, "05"), new HexPatch(2538295, "08"), new HexPatch(2538297, "1c"), new HexPatch(2538335, "01"), new HexPatch(2538343, "13"), new HexPatch(2538370, "e9cbf6ffff83c4408bc65f5e81c400020000c21000"), new HexPatch(2538412, "7503"), new HexPatch(2538422, "0f"), new HexPatch(2538738, "c746"), new HexPatch(2538741, "0200888ec2"), new HexPatch(2538747, "0000b202888ec7"), new HexPatch(2538755, "0000884e4a8d463a8d8ea9"), new HexPatch(2538767, "00008896c4"), new HexPatch(2538773, "00008896c6"), new HexPatch(2538779, "00008856528b1650516aff8bcec686c3"), new HexPatch(2538796, "000001c6464201c686c5"), new HexPatch(2538807, "000001c7461c"), new HexPatch(2538815, "ffffc74620"), new HexPatch(2538822, "ffffc6464907ff523c8986ba"), new HexPatch(2538835, "0000b801"), new HexPatch(2538840, "00005ec3"), new HexPatch(2545800, "07"), new HexPatch(2546577, "08"), new HexPatch(2546589, "6a2a516a016a05536a016a175356e80043ebff83c44066c74618020066c7461c020066895e0766895e0966895e0b66c7460d0300c646170666c7461a010066895e0f66895e1166895e1e885e20c6462101885e22895e5c895e60895e648bc65f5e5b81c40002"), new HexPatch(2546692, "00c21000"), new HexPatch(2546851, "b8c20000"), new HexPatch(2546966, "b8d40000"), new HexPatch(2547116, "b8d40000"), new HexPatch(2547807, "98f2"), new HexPatch(2549015, "b8c20000"), new HexPatch(2549037, "98f2"), new HexPatch(2549070, "98f2"), new HexPatch(2549103, "98f2"), new HexPatch(2549136, "98f2"), new HexPatch(2549168, "bac2"), new HexPatch(2549171, "000090"), new HexPatch(2549381, "18"), new HexPatch(2549398, "02"), new HexPatch(2549505, "6a1b598908c74002ff010001894806c74008ff1c0100b50189480cc7400eff0106"), new HexPatch(2549539, "894812c74014ff1f060090909090"), new HexPatch(2549563, "04"), new HexPatch(2661418, "4a"), new HexPatch(2661435, "39"), new HexPatch(4065107, "98f2"), new HexPatch(4065900, "98f2"), new HexPatch(4358133, "98f2"), new HexPatch(4358166, "98f2"), new HexPatch(4420547, "bac2"), new HexPatch(4420550, "000090"), new HexPatch(4420632, "bad400000090"), new HexPatch(4420713, "eb"), new HexPatch(4420798, "bad500000090"), new HexPatch(4420890, "b8d50000"), new HexPatch(5058051, "98f2") } }
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
            using (var fs = File.Open(patchFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return LoadPatchFile(fs);
            }
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
                    var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class ResolutionChanger
    {
        public static void GetResolution(string fileName, out int width, out int height)
        {
            using (var fin = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(fin))
            {
                fin.Seek(0x001D79F3, SeekOrigin.Begin);
                width = br.ReadInt16();
                fin.Seek(0x0000384D, SeekOrigin.Begin);
                height = br.ReadInt16();
            }
        }

        public static void SetResolution(string fileName, int width, int height)
        {
            List<int> widthOffsets = new List<int> {
                0x001D79F3,
                0x001D864B,
                0x001D868B,
                0x001E78BC,
                0x001E78E6,
                0x001E7B74,
                0x001E7BDC,
                0x001E7C39,
                0x001E7CAB,
                0x001E7D6B,
                0x001E7DDA,
                0x001E7E3C,
                0x001E7EB3,
                0x001E830A,
                0x0028D8D5,
                0x004AEED9,
                0x004AEF01
            };

            List<int> heightOffsets = new List<int> {
                0x0000384D,
                0x00003924,
                0x000039B7,
                0x001D79EE,
                0x001D8646,
                0x001D8686,
                0x001E78EE,
                0x001E82F5,
                0x001E8305,
                0x0028D8E0,
                0x004AEED4,
                0x004AEEFC
            };

            List<int> widthMinus1Offsets = new List<int> {
                0x00002B7E,
                0x00002B85,
                0x00003B02,
                0x00003B65,
                0x00060421,
                0x00070FB3,
                0x00072ADC,
                0x0015F2D0,
                0x0015F2ED,
                0x0015F393,
                0x0015F571,
                0x0015F593,
                0x0015F5F1,
                0x001612A2,
                0x001612A9,
                0x00190F2B,
                0x001EE1F6,
                0x001F14D0,
                0x00408E7C,
                0x0041BD17,
                0x004B9A7C,
                0x004BAA60
            };

            List<int> heightMinus1Offsets = new List<int> {
                0x00002B8E,
                0x00003BDE,
                0x00003C41,
                0x0006041C,
                0x00070FAE,
                0x00072AD7,
                0x0015F2CB,
                0x0015F2E8,
                0x0015F38E,
                0x0015F56C,
                0x0015F58E,
                0x0015F5EC,
                0x001612CB,
                0x001612D2,
                0x00190F23,
                0x001EE20B,
                0x001F14C8,
                0x00319346,
                0x0035DC11,
                0x00408E77,
                0x0041BD12,
                0x004B9A74,
                0x004BAA58
            };

            using (var file = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    foreach (var offset in widthOffsets)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((short)width);
                    }

                    foreach (var offset in heightOffsets)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((short)height);
                    }

                    foreach (var offset in widthMinus1Offsets)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((short)(width - 1));
                    }

                    foreach (var offset in heightMinus1Offsets)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((short)(height - 1));
                    }

                    int unknown1 = 0x15;
                    int widthMod = 415;
                    int heightMod = 314;
                    int unknownNegative = 0xbf;
                    int unknown2 = 0x35;
                    int unknown3 = 0x4ab;
                    int centering1 = 0x38b;
                    int centering2 = 0x381;

                    if (width == 720 && height == 480)
                    {
                        unknown1 = 13;
                        widthMod = 226;
                        heightMod = 204;
                        unknownNegative = 255;
                        unknown2 = 248;
                        unknown3 = 693;
                        centering1 = 777;
                        centering2 = 870;

                        //00059A3B: 08 0C
                        //0005C018: 04 08
                        
                        bw.Seek(0x00059A3B, SeekOrigin.Begin);
                        bw.Write((byte)0x08);
                        bw.Seek(0x0005C018, SeekOrigin.Begin);
                        bw.Write((byte)0x04);
                        bw.Seek(0x0001D868C, SeekOrigin.Begin);
                        bw.Write((byte)0x02);
                        bw.Seek(0x1E4588, SeekOrigin.Begin);
                        bw.Write((byte)0x00);
                        bw.Seek(0x1E4712, SeekOrigin.Begin);
                        bw.Write((byte)0x00);
                        bw.Seek(0x1E4714, SeekOrigin.Begin);
                        bw.Write((byte)0x00);
                        bw.Seek(0x1ED7AB, SeekOrigin.Begin);
                        bw.Write((byte)0x00);
                        bw.Seek(0x1ED7AD, SeekOrigin.Begin);
                        bw.Write((byte)0x20);
                        bw.Seek(0x201E68, SeekOrigin.Begin);
                        bw.Write((byte)0x5a);
                        

                        bw.Seek(0x41BD18, SeekOrigin.Begin);
                        bw.Write((byte)0x2);
                        bw.Seek(0x41D6DD, SeekOrigin.Begin);
                        bw.Write((byte)0xc);
                        
                        // Shifts the main part to the left (doesn't shift menu though)
                        bw.Seek(0x421D33, SeekOrigin.Begin);
                        bw.Write((byte)0x4);
                        bw.Seek(0x421F62, SeekOrigin.Begin);
                        bw.Write((byte)0x3);
                        bw.Seek(0x4225B3, SeekOrigin.Begin);
                        bw.Write((byte)0x6);
                        bw.Seek(0x42283E, SeekOrigin.Begin);
                        bw.Write((byte)0x3);

                        // Shrink the menu buttons too
                        bw.Seek(0x201E57, SeekOrigin.Begin);
                        bw.Write((byte)0x00);

                        bw.Seek(0x201ED5, SeekOrigin.Begin);
                        bw.Write((byte)0xeb);

                        /*
                        bw.Seek(0x49C626, SeekOrigin.Begin);
                        bw.Write((byte)0x1);
                        bw.Seek(0x49C69E, SeekOrigin.Begin);
                        bw.Write((byte)0x1);
                        bw.Seek(0x49C73C, SeekOrigin.Begin);
                        bw.Write((byte)0x1);

                        bw.Seek(0x201ED5, SeekOrigin.Begin);
                        bw.Write((byte)0xeb);

                        bw.Seek(0x35E16C, SeekOrigin.Begin);
                        bw.Write((byte)0x1);
                        bw.Seek(0x388A33, SeekOrigin.Begin);
                        bw.Write((byte)0x1);
                        bw.Seek(0x388A86, SeekOrigin.Begin);
                        bw.Write((byte)0x1);
                        bw.Seek(0x38900F, SeekOrigin.Begin);
                        bw.Write((byte)0xb5);
                        bw.Seek(0x389010, SeekOrigin.Begin);
                        bw.Write((byte)0x2);
                        */
                    }
                    else
                    if (width == 800 && height == 600)
                    {
                        unknown1 = 15;
                        widthMod = 240;
                        heightMod = 256;
                        unknownNegative = 255;
                        unknown2 = 24;
                        unknown3 = 908;
                        centering1 = 824;
                        centering2 = 814;
                    }
                    else
                    if (width == 1024 && height == 600)
                    {
                        unknown1 = 16;
                        widthMod = 322;
                        heightMod = 256;
                        unknownNegative = 255;
                        unknown2 = 24;
                        unknown3 = 948;
                        centering1 = 864;
                        centering2 = 854;
                    }
                    else
                    if (width == 1024 && height == 768)
                    {
                        unknown1 = 20;
                        widthMod = 322;
                        heightMod = 327;
                        unknownNegative = 199;
                        unknown2 = 24;
                        unknown3 = 948;
                        centering1 = 864;
                        centering2 = 854;
                    }
                    else
                    if (width == 1280 && (height == 800 || height == 720))
                    {
                        unknown1 = 21;
                        widthMod = 415;
                        heightMod = height == 800 ? 341 : 305;
                        unknownNegative = 191;
                        unknown2 = 53;      // Half of the menu side? 126?
                        unknown3 = 1195;
                        centering1 = 907;
                        centering2 = 897;
                    }
                    else
                    if (width == 1280 && height == 1024)
                    {
                        unknown1 = 26;
                        widthMod = 415;
                        heightMod = 436;
                        unknownNegative = 149;
                        unknown2 = 53;
                        unknown3 = 1195;
                        centering1 = 907;
                        centering2 = 897;
                    }
                    else
                    if (width == 1366 && height == 768)
                    {
                        unknown1 = 20;
                        widthMod = 446;
                        heightMod = 327;
                        unknownNegative = 199;
                        unknown2 = 62;
                        unknown3 = 1277;
                        centering1 = 921;
                        centering2 = 911;
                    }
                    else
                    if (width == 1400 && height == 900)
                    {
                        unknown1 = 23;
                        widthMod = 458;
                        heightMod = 384;
                        unknownNegative = 170;
                        unknown2 = 66;
                        unknown3 = 1054;
                        centering1 = 927;
                        centering2 = 917;
                    }
                    else
                    if (width == 1680 && height == 1050)
                    {
                        unknown1 = 26;
                        widthMod = 560;
                        heightMod = 450;
                        unknownNegative = 170;
                        unknown2 = 77;
                        unknown3 = 1100;
                        centering1 = 1008;
                        centering2 = 987;
                    }
                    else
                    if (width == 1920 && height == 1080)
                    {
                        unknown1 = 27;
                        widthMod = 650;
                        heightMod = 460;
                        unknownNegative = 160;
                        unknown2 = 77;
                        unknown3 = 1100;
                        centering1 = 1088;
                        centering2 = 1057;

                        bw.Seek(0x49C626, SeekOrigin.Begin);
                        bw.Write((byte)0x3);
                        bw.Seek(0x49C69E, SeekOrigin.Begin);
                        bw.Write((byte)0x3);
                        bw.Seek(0x49C73C, SeekOrigin.Begin);
                        bw.Write((byte)0x3);
                    }
                    else
                    {
                        unknown1 = (int)(((double)height) * 0.0255);
                        widthMod = (int)(((double)width) * 0.326);
                        heightMod = (int)(((double)height) * 0.426);
                        unknownNegative = (int)(((double)height) * 0.18);
                        unknown2 = (int)(((double)width) * 0.043);
                        unknown3 = (int)(((double)width) * 0.75);
                        centering1 = (int)(((double)width) * 0.68);
                        centering2 = (int)(((double)width) * 0.65);
                    }

                    // 720x480
                    // 1A33F5 = 1 = 0x0D = 13           (unknown1)
                    // 1A46A4 = 2 = 0x82 = 130          (unknown1*10)
                    // 201E62 = 3 = 0xE2 = 226 (w)      (widthMod)
                    // 201E7E = 4 = 0xCC = 204 (h)      (heightMod)
                    // 201E8D = 5 = 0x3F = 63           (unknownNegative)
                    // 201F2B = 6 = 0x0D = 13           (unknown1)
                    // 201F4F = 7 = 0xF8 = 248          (unknown2)
                    // 38900F = 8 = 0x2B5 = 693         (unknown3)
                    // 470CCE = 9 = 0x309 = 777         (centering1)
                    // 470F57 = 10 = 0x309 = 777        (centering1)
                    // 4750E2 = 11 = 0x3FF = 1023       (centering2)

                    // 1024x600
                    // 1A33F5 = 1 = 0x10 = 16           (unknown1)
                    // 1A46A4 = 2 = 0xA0 = 160          (unknown1*10)
                    // 201E62 = 3 = 0x142 = 322 (w)     (widthMod)
                    // 201E7E = 4 = 0x100 = 256 (h)     (heightMod)
                    // 201E8D = 5 = 0xFF = 255          (unknownNegative)
                    // 201F2B = 6 = 0x10 = 16           (unknown1)
                    // 201F4F = 7 = 0x18 = 24           (unknown2)
                    // 38900F = 8 = 0x3B4 = 948         (unknown3)
                    // 470CCE = 9 = 0x360 = 864         (centering1)
                    // 470F57 = 10 = 0x360 = 864        (centering1)
                    // 4750E2 = 11 = 0x356 = 854        (centering2)

                    // 1024x768
                    // 1A33F5 = 1 = 0x14 = 20           (unknown1)
                    // 1A46A4 = 2 = 0xc8 = 200          (unknown1*10)
                    // 201E62 = 3 = 0x142 = 322 (w)     (widthMod)
                    // 201E7E = 4 = 0x147 = 327 (h)     (heightMod)
                    // 201E8D = 5 = 0xC7 = 199          (unknownNegative)
                    // 201F2B = 6 = 0x14 = 20           (unknown1)
                    // 201F4F = 7 = 0x18 = 24           (unknown2)
                    // 38900F = 8 = 0x3B4 = 948         (unknown3)
                    // 470CCE = 9 = 0x360 = 864         (centering1)
                    // 470F57 = 10 = 0x360 = 864        (centering1)
                    // 4750E2 = 11 = 0x356 = 854        (centering2)

                    // 1280x800
                    // 1A33F5 = 1 = 0x15 = 21           (unknown1)
                    // 1A46A4 = 2 = 0xD2 = 210          (unknown1*10)
                    // 201E62 = 3 = 0x19F = 415 (w)     (widthMod)
                    // 201E7E = 4 = 0x155 = 341 (h)     (heightMod)
                    // 201E8D = 5 = 0xBF = 191          (unknownNegative)
                    // 201F2B = 6 = 0x15 = 21           (unknown1)
                    // 201F4F = 7 = 0x35 = 53           (unknown2)
                    // 38900F = 8 = 0x4AB = 1195        (unknown3)
                    // 470CCE = 9 = 0x38B = 907         (centering1)
                    // 470F57 = 10 = 0x38B = 907        (centering1)
                    // 4750E2 = 11 = 0x381 = 897        (centering2)

                    // 1280x1024
                    // 1A33F5 = 1 = 0x1A = 26           (unknown1)
                    // 1A46A4 = 2 = 0x104 = 260         (unknown1*10)
                    // 201E62 = 3 = 0x19F = 415 (w)     (widthMod)
                    // 201E7E = 4 = 0x1B4 = 436 (h)     (heightMod)
                    // 201E8D = 5 = 0x95 = 149          (unknownNegative)
                    // 201F2B = 6 = 0x1A = 26           (unknown1)
                    // 201F4F = 7 = 0x35 = 53           (unknown2)
                    // 38900F = 8 = 0x4AB = 1195        (unknown3)
                    // 470CCE = 9 = 0x38B = 907         (centering1)
                    // 470F57 = 10 = 0x38B = 907        (centering1)
                    // 4750E2 = 11 = 0x381 = 897        (centering2)

                    // 1366x768
                    // 1A33F5 = 1 = 0x14 = 20           (unknown1)
                    // 1A46A4 = 2 = 0xc8 = 200          (unknown1*10)
                    // 201E62 = 3 = 0x1BE = 446 (w)     (widthMod)
                    // 201E7E = 4 = 0x147 = 327 (h)     (heightMod)
                    // 201E8D = 5 = 0xC7 = 199          (unknownNegative)
                    // 201F2B = 6 = 0x14 = 20           (unknown1)
                    // 201F4F = 7 = 0x3E = 62           (unknown2)
                    // 38900F = 8 = 0x4FD = 1277        (unknown3)
                    // 470CCE = 9 = 0x399 = 921         (centering1)
                    // 470F57 = 10 = 0x399 = 921        (centering1)
                    // 4750E2 = 11 = 0x38F = 911        (centering2)

                    // 1400x900
                    // 1A33F5 = 1 = 0x17 = 23           (unknown1)
                    // 1A46A4 = 2 = 0xE6 = 230          (unknown1*10)
                    // 201E62 = 3 = 0x1CA = 458 (w)     (widthMod)
                    // 201E7E = 4 = 0x180 = 384 (h)     (heightMod)
                    // 201E8D = 5 = 0xAA = 170          (unknownNegative)
                    // 201F2B = 6 = 0x17 = 23           (unknown1)
                    // 201F4F = 7 = 0x42 = 66           (unknown2)
                    // 38900F = 8 = 0x41E = 1054        (unknown3)
                    // 470CCE = 9 = 0x39F = 927         (centering1)
                    // 470F57 = 10 = 0x39F = 927        (centering1)
                    // 4750E2 = 11 = 0x395 = 917        (centering2)

                    // Unknowns

                    // Unknown(15->14) 21 -> 20
                    List<int> unknownsPart1 = new List<int> {
                        0x001A33F5,
                        0x001A38AB,
                        0x001A3E06,
                        0x001A4376,
                        0x001A46D4,
                        0x001A46F8,
                        0x001A4735,
                        0x001A482C,
                        0x001A4850,
                        0x001A4889,
                        0x001A4AD2,
                        0x001A4B3F,
                        0x001A4BC2,
                        0x001A4D82,
                        0x001A4DEF,
                        0x001A4E72
                    };

                    // Unknown(D2->C8) 210 -> 200
                    List<int> unknownsPart2 = new List<int> {
                        0x001A46A4,
                        0x001A472A,
                        0x001A47FF,
                        0x001A487E,
                        0x001A4A66,
                        0x001A4BB4,
                        0x001A4D16,
                        0x001A4E64
                    };

                    foreach (var offset in unknownsPart1)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((byte)unknown1);
                    }

                    foreach (var offset in unknownsPart2)
                    {
                        bw.Seek(offset, SeekOrigin.Begin);
                        bw.Write((short)(unknown1 * 10));
                    }

                    // 00201E62: 9F BE
                    // 00601E60 |.  69C0 9F010000           IMUL EAX, EAX,19F(415 = 1366, 1680)
                    // 19F (415) = 1280
                    // 1CA (458) = 1400
                    // E2 (226) = 720
                    bw.Seek(0x00201E62, SeekOrigin.Begin);
                    bw.Write((short)widthMod);         // Controls the width of the main screen

                    // 00201E7E: 55 47
                    // 00601E7C |.  69C0 55010000           IMUL EAX, EAX,155(341 = 768. = 1050)
                    // CC (204) = 480
                    // 155 (341) = 800
                    bw.Seek(0x00201E7E, SeekOrigin.Begin);
                    bw.Write((short)heightMod);         // Controls the width of the main screen 

                    // Negative - this is the vertical gap between the players names on the team screen
                    // 00201E8D: BF C7
                    // 00601E89 |.B8 FFFFFFBF MOV EAX,BFFFFFFF(BF = 191)
                    bw.Seek(0x00201E8D, SeekOrigin.Begin);
                    bw.Write((byte)unknownNegative);

                    // 00201F2B: 15 14
                    // 00601F2A |.  6A 15                   PUSH 15
                    bw.Seek(0x00201F2B, SeekOrigin.Begin);
                    bw.Write((byte)unknown1);

                    // 00201F4F: 35 3E
                    // 00601F4D |.  83C0 35 | ADD EAX,35(53)
                    bw.Seek(0x00201F4F, SeekOrigin.Begin);
                    bw.Write((byte)unknown2);

                    // Next Unread?
                    // 0038900F: AB FD
                    // 0078900E |.  68 AB040000 PUSH 4AB; | Arg3 = 4AB (1195)
                    bw.Seek(0x0038900F, SeekOrigin.Begin);
                    bw.Write((short)unknown3);

                    // These two are most likely the centering of the text at the top
                    // Like the players name in the center when looking at his stats
                    // 00470CCE: 8B 99
                    // 00870CCD |.  68 8B030000 PUSH 38B; || Arg3 = 38B (907)
                    bw.Seek(0x00470CCE, SeekOrigin.Begin);
                    bw.Write((short)centering1);

                    // 00470F57: 8B 99
                    // 00870F56 |.  68 8B030000 PUSH 38B; | Arg3 = 38B
                    bw.Seek(0x00470F57, SeekOrigin.Begin);
                    bw.Write((short)centering1);

                    // This the born date line in the player details - needs to be centered 
                    // 004750E2: 81 8F
                    // 008750E1 |.  68 81030000             PUSH 381; | Arg3 = 381 (897)
                    bw.Seek(0x004750E2, SeekOrigin.Begin);
                    bw.Write((short)centering2);
                }
            }
        }
    }
}

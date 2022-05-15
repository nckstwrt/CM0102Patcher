using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class LinearExecutableFixUps
    {
        static int FindLEOffset(string exeFile)
        {
            using (var f = File.Open(exeFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var br = new BinaryReader(f))
            {
                var bytes = br.ReadBytes((int)f.Length);

                UInt32 offset = 0;

                while (true)
                {
                    if (offset > bytes.Length - 64)
                        return -1;
                    if (bytes[offset] == 'L' && bytes[offset + 1] == 'E')
                    {
                        return (int)offset;
                    }
                    else
                    if (bytes[offset] == 'M' && bytes[offset + 1] == 'Z')
                    {
                        var embeddedLE = bytes[offset + 0x18];
                        if (embeddedLE >= 0x40)
                        {
                            offset += BitConverter.ToUInt32(bytes, (int)(offset + 0x3C));
                        }
                        else
                        {
                            var exeLen = (UInt32)BitConverter.ToUInt16(bytes, (int)(offset + 0x4));
                            exeLen *= 512;
                            var tmp = BitConverter.ToUInt16(bytes, (int)(offset + 0x2));
                            if (tmp != 0)
                                exeLen -= (UInt16)(512 - tmp);
                            offset += exeLen;
                        }
                    }
                    else
                    if (bytes[offset] == 'B' && bytes[offset + 1] == 'W')
                    {
                        var exeLen = (UInt32)BitConverter.ToUInt16(bytes, (int)(offset + 0x4));
                        exeLen *= 512;
                        var tmp = BitConverter.ToUInt16(bytes, (int)(offset + 0x2));
                        if (tmp != 0)
                            exeLen += tmp;
                        offset += exeLen;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class LEHeader
        {
            public UInt16 LE;                                /* 00h */
            public byte byte_order;                          /* 02h */
            public byte word_order;                          /* 03h */
            public UInt32 format_version;                     /* 04h */
            public UInt16 cpu_type;                           /* 08h */
            public UInt16 os_type;                            /* 0Ah */
            public UInt32 module_version;                     /* 0Ch */
            public UInt32 module_flags;                       /* 10h */
            public UInt32 page_count;                         /* 14h */
            public UInt32 eip_object_index;                   /* 18h */
            public UInt32 eip_offset;                         /* 1Ch */
            public UInt32 esp_object_index;                   /* 20h */
            public UInt32 esp_offset;                         /* 24h */
            public UInt32 page_size;                          /* 28h */
            public UInt32 last_page_size;                     /* 2Ch */
            public UInt32 fixup_section_size;                 /* 30h */
            public UInt32 fixup_section_check_sum;            /* 34h */
            public UInt32 loader_section_size;                /* 38h */
            public UInt32 loader_section_check_sum;           /* 3Ch */
            public UInt32 object_table_offset;                /* 40h */
            public UInt32 object_count;                       /* 44h */
            public UInt32 object_page_table_offset;           /* 48h */
            public UInt32 object_iterated_pages_offset;       /* 4Ch */
            public UInt32 resource_table_offset;              /* 50h */
            public UInt32 resource_entry_count;               /* 54h */
            public UInt32 resident_name_table_offset;         /* 58h */
            public UInt32 entry_table_offset;                 /* 5Ch */
            public UInt32 module_directives_offset;           /* 60h */
            public UInt32 module_directives_count;            /* 64h */
            public UInt32 fixup_page_table_offset;            /* 68h */
            public UInt32 fixup_record_table_offset;          /* 6Ch */
            public UInt32 import_module_name_table_offset;    /* 70h */
            public UInt32 import_module_name_entry_count;     /* 74h */
            public UInt32 import_procedure_name_table_offset; /* 78h */
            public UInt32 per_page_check_sum_table_offset;    /* 7Ch */
            public UInt32 data_pages_offset;                  /* 80h */
            public UInt32 preload_pages_count;                /* 84h */
            public UInt32 non_resident_name_table_offset;     /* 88h */
            public UInt32 non_resident_name_entry_count;      /* 8Ch */
            public UInt32 non_resident_name_table_check_sum;  /* 90h */
            public UInt32 auto_data_segment_object_index;     /* 94h */
            public UInt32 debug_info_offset;                  /* 98h */
            public UInt32 debug_info_size;                    /* 9Ch */
            public UInt32 instance_pages_count;               /* A0h */
            public UInt32 instance_pages_demand_count;        /* A4h */
            public UInt32 heap_size;                          /* A8h */
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class LEObjectHeader
        {
            public UInt32 virtual_size;     /* 00h */
            public UInt32 base_address;     /* 04h */
            public UInt32 flags;            /* 08h */
            public UInt32 first_page_index; /* 0Ch */
            public UInt32 page_count;       /* 10h */
            public UInt32 reserved;         /* 14h */
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class LEObjectPageHeader
        {
            UInt16 first_number; /* 00h */
            byte second_number; /* 02h */
            byte type;   /* 03h */
        }

        public class FixUp
        {
            public FixUp(BinaryReader br, ref UInt32 offset_, List<LEObjectHeader> objects, UInt32 page_offset, byte addr_flags = 0, byte reloc_flags = 0)
            {
                offset = (UInt32)(page_offset + readUpToSourceOffset(br, ref offset_, ref addr_flags, ref reloc_flags));
                address = readDestOffset(br, ref offset_, objects, page_offset, addr_flags, reloc_flags);
            }

            static UInt32 readDestOffset(BinaryReader br, ref UInt32 offset, List<LEObjectHeader> objects, UInt32 page_offset, byte addr_flags, byte reloc_flags)
            {
                if ((reloc_flags & 0x40) != 0)
                { /* 16-bit Object Number/Module Ordinal Flag */
                    throw new Exception("16-bit object or module ordinal numbers are not supported");
                }

                byte obj_index = throwOnInvalidObjectIndex(br, objects, page_offset);
                ++offset;

                UInt32 dst_off_32;
                if ((reloc_flags & 0x10) != 0)
                { /* 32-bit offset */
                    dst_off_32 = br.ReadUInt32();
                    offset += 4;
                }
                else if ((addr_flags & 0xf) != 0x2)
                { /* 16-bit offset */
                    UInt16 dst_off_16;
                    dst_off_16 = br.ReadUInt16();
                    dst_off_32 = dst_off_16;
                    offset += 2;
                }
                else
                {
                    return (UInt32)obj_index + 1;
                }
                return objects[obj_index].base_address + dst_off_32;
            }

            static byte throwOnInvalidObjectIndex(BinaryReader br, List<LEObjectHeader> objects, UInt32 page_offset)
            {
                byte obj_index = br.ReadByte();
                if (obj_index < 1 || obj_index > objects.Count)
                {
                    throw new Exception("Page at offset 0x" + page_offset + ": unexpected object index " + (int)obj_index);
                }
                return (byte)(obj_index - 1);
            }

            static Int16 readUpToSourceOffset(BinaryReader br, ref UInt32 offset, ref byte addr_flags, ref byte reloc_flags)
            {
                addr_flags = throwOnInvalidAddressFlags(br);
                ++offset;

                reloc_flags = throwOnInvalidRelocFlags(br);
                ++offset;

                Int16 src_off = br.ReadInt16();
                offset += 2; //sizeof(int16_t);
                return src_off;
            }

            static byte throwOnInvalidAddressFlags(BinaryReader br)
            {
                byte addr_flags = br.ReadByte();
                if ((addr_flags & 0x20) != 0)
                {
                    throw new Exception();
                    //        } else if ((addr_flags & 0xf) != 0x7) {/* 32-bit offset */
                    //        	throw Error() << "Unsupported fixup type in 0x" << std::hex << (int) addr_flags;
                }
                return addr_flags;
            }

            static byte throwOnInvalidRelocFlags(BinaryReader br)
            {
                byte reloc_flags = br.ReadByte();
                if ((reloc_flags & 0x3) != 0x0)
                { /* internal ref */
                    throw new Exception("Unsupported reloc type in " + (int)reloc_flags);
                }
                return reloc_flags;
            }

            public UInt32 offset;
            public UInt32 address;
        }

        public static void CheckFixups(string fileName)
        {
            using (var f = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var br = new BinaryReader(f))
            {
                var ptrToLE = FindLEOffset(fileName);
                
                // Seems the Code Org Offset is here - probably isn't always. TODO: Parse LE properly
                f.Seek(ptrToLE + 0xC8, SeekOrigin.Begin);
                var codeOrg = br.ReadInt32();

                // Skip to the LE Header (after the two MZ headers)
                f.Seek(ptrToLE, SeekOrigin.Begin);

                var le_bytes = br.ReadBytes(Marshal.SizeOf(typeof(LEHeader)));
                var le_header = MiscFunctions.BytesToStruct<LEHeader>(le_bytes);

                // Read Object Headers
                List<LEObjectHeader> object_headers = new List<LEObjectHeader>();
                f.Seek(ptrToLE + le_header.object_table_offset, SeekOrigin.Begin);
                for (int i = 0; i < le_header.object_count; i++)
                {
                    var bytes = br.ReadBytes(Marshal.SizeOf(typeof(LEObjectHeader)));
                    var obj = MiscFunctions.BytesToStruct<LEObjectHeader>(bytes);
                    obj.first_page_index--;
                    object_headers.Add(obj);
                }

                // Read Object Page Headers
                List<LEObjectPageHeader> object_page_headers = new List<LEObjectPageHeader>();
                f.Seek(ptrToLE + le_header.object_page_table_offset, SeekOrigin.Begin);
                for (int i = 0; i < le_header.page_count; i++)
                {
                    var bytes = br.ReadBytes(Marshal.SizeOf(typeof(LEObjectPageHeader)));
                    object_page_headers.Add(MiscFunctions.BytesToStruct<LEObjectPageHeader>(bytes));
                }

                // Read Fix Up Records
                List<UInt32> fixup_record_offsets = new List<uint>();
                f.Seek(ptrToLE + le_header.fixup_page_table_offset, SeekOrigin.Begin);
                for (int i = 0; i <= le_header.page_count; i++)
                {
                    fixup_record_offsets.Add(br.ReadUInt32());
                }

                // Load the actual Fix Ups
                List<Dictionary<UInt32, UInt32>> fixups = new List<Dictionary<uint, uint>>();
                List<UInt32> fixup_addresses = new List<uint>();
                var table_offset = (UInt32)(ptrToLE + le_header.fixup_record_table_offset);

                int counter = 0;
                using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(fileName), "FixUpOffsets.txt")))
                using (StreamWriter sw2 = new StreamWriter(Path.Combine(Path.GetDirectoryName(fileName), "FixUpOffsetsCode.txt")))
                {
                    for (int oi = 0; oi < object_headers.Count; oi++)
                    {
                        var obj = object_headers[oi];
                        for (UInt32 n = obj.first_page_index; n < obj.first_page_index + obj.page_count; ++n)
                        {
                            UInt32 offset = table_offset + fixup_record_offsets[(int)n];
                            UInt32 end = table_offset + fixup_record_offsets[(int)(n + 1)];
                            UInt32 page_offset = (n - obj.first_page_index) * le_header.page_size;
                            for (f.Seek(offset, SeekOrigin.Begin); offset < end;)
                            {
                                var saved_offset = offset;

                                FixUp fixup = new FixUp(br, ref offset, object_headers, page_offset);

                                if (fixups.Count == oi)
                                    fixups.Add(new Dictionary<uint, uint>());

                                bool dupe = false;
                                if (fixups[oi].ContainsKey(fixup.offset))
                                    dupe = true;
                                fixups[oi][fixup.offset] = fixup.address;
                                fixup_addresses.Add(fixup.address);

                                var outStr = string.Format("{0:0000000}. File Offset: 0x{1:X} has IDA Code: 0x{2:X} (IDA Code raw:{3:X}) and Fixup Address 0x{4:X} {5}", counter++, saved_offset, fixup.offset + codeOrg, fixup.offset, fixup.address, dupe ? "DUPE!" : "");
                                sw.WriteLine(outStr);
                                Console.WriteLine(outStr);

                                sw2.WriteLine("\tFixUpLocation(0x{0:X}, 0x{1:X}),", fixup.offset + codeOrg, fixup.address);
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

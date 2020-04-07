using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class SaveReader2
    {
        public void Load(string inFile)
        {
            using (var fin = File.OpenRead(inFile))
            using (var br = new BinaryReader(fin))
            {
                var compressed = (br.ReadInt32() == 4);

                // Skip 4 bytes
                unknownHdrBytes = br.ReadInt32();

                // Read Blocks
                var blockCount = br.ReadInt32();
                for (int i = 0; i < blockCount; i++)
                {
                    var block = new Block();
                    block.blockBuffer = br.ReadBytes(268);
                    Console.WriteLine("{0}", block.Name);
                    blocks.Add(block);
                }

                // Read Data
                foreach (var block in blocks)
                {
                    fin.Seek(block.Position, SeekOrigin.Begin);
                    block.dataBuffer = new byte[block.Size];

                    int bufPtr = 0;
                    while (bufPtr < block.Size)
                    {
                        var b = fin.ReadByte();
                        if (!compressed || b <= 128)
                        {
                            block.dataBuffer[bufPtr++] = (byte)b;
                        }
                        else
                        {
                            byte byteCount = (byte)(b - 128);
                            byte actualByte = (byte)fin.ReadByte();
                            for (byte i = 0; i < byteCount; i++)
                                block.dataBuffer[bufPtr++] = (byte)actualByte;
                        }
                    }
                }
            }
        }

        public Block FindBlock(string blockName)
        {
            return blocks.FirstOrDefault(x => x.Name == blockName);
        }

        public void DumpBlockToFile(string blockName, string fileName)
        {
            var block = FindBlock(blockName);
            using (var fout = File.Create(fileName))
                fout.Write(block.dataBuffer, 0, block.dataBuffer.Length);
        }

        public List<T> BlockToObjects<T>(string blockName, bool contractSpecific = false)
        {
            List<T> ret = null;
            var block = FindBlock(blockName);
            if (block != null)
            {
                int maxSlices = -1;
                int startAt = 0;
                if (contractSpecific)
                {
                    var intPreCount = BitConverter.ToInt32(block.dataBuffer, 0);
                    var blockCount = BitConverter.ToInt32(block.dataBuffer, 4);
                    startAt = (8 + intPreCount * 21);

                    if (intPreCount > 0)
                    {
                        blockCount = BitConverter.ToInt32(block.dataBuffer, startAt - 4);
                    }
                    maxSlices = blockCount;
                }

                var slices = SliceBlock(block, Marshal.SizeOf(typeof(T)), startAt, maxSlices);
                ret = CastSlicesToObjects<T>(slices);
            }
            return ret;
        }

        public void ObjectsToBlock<T>(string blockName, List<T> objects, bool contractSpecific = false)
        {
            var block = FindBlock(blockName);
            if (block != null)
            {
                var slices = CastObjectsToSlices(objects);
                Rebuild(block, slices, contractSpecific);
            }
        }

        public List<string> NamesFromBlock(string blockName)
        {
            List<string> ret = null;
            var block = FindBlock(blockName);
            if (block != null)
            {
                ret = new List<string>();
                var slices = SliceBlock(block, 60);
                foreach (var slice in slices)
                    ret.Add(latin1.GetString(slice, 0, 50).TrimEnd('\0'));
            }
            return ret;
        }

        public DateTime GetCurrentGameDate()
        {
            var block = FindBlock("general.dat");
            return TCMDate.ToDateTime(new TCMDate(block.dataBuffer, 3944));
        }

        public List<TStaff> FindPlayer(string firstName, string lastName, List<TStaff> staff)
        {
            List<TStaff> ret = new List<TStaff>();
            if (firstNames == null)
            {
                firstNames = NamesFromBlock("first_names.dat");
                secondNames = NamesFromBlock("second_names.dat");
            }

            List<int> fname_idx = new List<int>();
            List<int> sname_idx = new List<int>();
            for (int i = 0; i < firstNames.Count(); i++)
            {
                if (string.Compare(firstName, firstNames[i], CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                {
                    fname_idx.Add(i);
                }
            }
            for (int i = 0; i < secondNames.Count(); i++)
            {
                if (string.Compare(lastName, secondNames[i], CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                {
                    sname_idx.Add(i);
                }
            }

            ret.AddRange(staff.FindAll(x => fname_idx.Contains(x.FirstName) && sname_idx.Contains(x.SecondName)));

            return ret;
        }

        List<byte[]> SliceBlock(Block block, int sliceSize, int startAt = 0, int maxSlices = -1)
        {
            List<byte[]> slices = new List<byte[]>();
            for (int i = startAt; i < block.dataBuffer.Length; i += sliceSize)
            {
                var slice = new byte[sliceSize];
                if (i + sliceSize <= block.dataBuffer.Length)
                    Array.Copy(block.dataBuffer, i, slice, 0, sliceSize);
                slices.Add(slice);
                if (maxSlices != -1 && slices.Count >= maxSlices)
                    break;
            }
            return slices;
        }

        List<T> CastSlicesToObjects<T>(List<byte[]> slices)
        {
            var ret = new List<T>();
            foreach (var slice in slices)
            {
                int objSize = Marshal.SizeOf(typeof(T));
                var ptrObj = Marshal.AllocHGlobal(objSize);
                Marshal.Copy(slice, 0, ptrObj, objSize);
                var obj = (T)Marshal.PtrToStructure(ptrObj, typeof(T));
                ret.Add(obj);
                Marshal.FreeHGlobal(ptrObj);
            }
            return ret;
        }

        List<byte[]> CastObjectsToSlices<T>(List<T> objects)
        {
            var ret = new List<byte[]>();
            foreach (var objbect in objects)
            {
                int objSize = Marshal.SizeOf(typeof(T));
                byte[] arr = new byte[objSize];
                IntPtr ptr = Marshal.AllocHGlobal(objSize);
                Marshal.StructureToPtr(objbect, ptr, true);
                Marshal.Copy(ptr, arr, 0, objSize);
                Marshal.FreeHGlobal(ptr);
                ret.Add(arr);
            }
            return ret;
        }

        void Rebuild(Block block, List<byte[]> slices, bool contractSpecific = false)
        {
            var totalSliceSize = slices.Sum(x => x.Length);
            byte[] newDataBuffer;
            int ptr = 0;

            if (contractSpecific)
            {
                var intPreCount = BitConverter.ToInt32(block.dataBuffer, 0);
                var count = BitConverter.ToInt32(block.dataBuffer, 4);
                ptr = (8 + (intPreCount * 21));
                /*
                newDataBuffer = new byte[totalSliceSize + ptr + 13];
                Array.Copy(block.dataBuffer, 0, newDataBuffer, 0, ptr);

                // Write new Block Count
                if (intPreCount == 0)
                    Array.Copy(BitConverter.GetBytes(slices.Count), 0, newDataBuffer, 4, 4);

                newDataBuffer[newDataBuffer.Length - 1] = 1;
                */
                // Simplified version (as we are not expanding or contracting this right now)
                newDataBuffer = new byte[block.dataBuffer.Length];
                Array.Copy(block.dataBuffer, 0, newDataBuffer, 0, block.dataBuffer.Length);
            }
            else
                newDataBuffer = new byte[totalSliceSize];

            foreach (var slice in slices)
            {
                Array.Copy(slice, 0, newDataBuffer, ptr, slice.Length);
                ptr += slice.Length;
            }

            block.dataBuffer = newDataBuffer;
        }

        public void Write(string outFile, bool compressed)
        {
            using (var fout = File.Create(outFile))
            using (var bw = new BinaryWriter(fout))
            {
                // 3 is uncompressed, 4 is compressed (swap depending on what you're doing)
                bw.Write((int)(compressed ? 4 : 3));

                // Unknown Bytes
                bw.Write(unknownHdrBytes);

                // Write Block Count
                bw.Write(blocks.Count);

                // Skip to go write the data
                fout.Seek((blocks.Count * 268) + 12, SeekOrigin.Begin);

                // Write Block Data
                foreach (var block in blocks)
                {
                    block.Position = (uint)fout.Position;
                    block.Size = (uint)block.dataBuffer.Length;
                    bw.Write(compressed ? Compress(block.dataBuffer) : block.dataBuffer);
                }

                // Write Block Headers (with updated positions)
                fout.Seek(12, SeekOrigin.Begin);
                foreach (var block in blocks)
                {
                    bw.Write(block.blockBuffer);
                }
            }
        }

        byte[] Compress(byte[] buffer)
        {
            var ms = new MemoryStream();
            int lastByte = -1;
            int lastByteCount = 0;
            for (var i = 0; i < buffer.Length; i++)
            {
                var b = buffer[i];

                if (lastByte == -1)
                    lastByte = b;

                if (b != lastByte || lastByteCount >= 126)
                {
                    if (lastByteCount == 1 && lastByte < 128)
                        ms.WriteByte((byte)lastByte);
                    else
                    {
                        ms.WriteByte((byte)(lastByteCount + 128));
                        ms.WriteByte((byte)lastByte);
                    }

                    lastByte = b;
                    lastByteCount = 1;
                }
                else
                {
                    lastByteCount++;
                }
            }

            // Write last part
            if (lastByteCount == 1 && lastByte < 128)
                ms.WriteByte((byte)lastByte);
            else
            {
                ms.WriteByte((byte)(lastByteCount + 128));
                for (int j = 0; j < lastByteCount; j++)
                    ms.WriteByte((byte)lastByte);
            }

            return ms.ToArray();
        }

        int unknownHdrBytes = 0x16;
        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
        List<Block> blocks = new List<Block>();
        List<string> firstNames = null;
        List<string> secondNames = null;
    }
}

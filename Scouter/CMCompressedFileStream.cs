using System;
using System.IO;

namespace CM0102Scout
{
    public class CMCompressedFileStream : IDisposable
    {
        public FileStream fs = null;
        BinaryReader FfilStream = null;
        const int BUFFER_SIZE = 64 * 1024;
        byte[] Fbuffer = new byte[BUFFER_SIZE];
        bool FboCompressed;
        bool FboBufEmpty = true;
        int FintPos = 0;
        int FintReadPos = 0;
        int FintBufPos = 0;

        public CMCompressedFileStream(string FileName, bool Compressed)
        {
            FboCompressed = Compressed;
            fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            FfilStream = new BinaryReader(fs);
        }

        public void Dispose()
        {
            if (FfilStream != null)
            {
                FfilStream.Close();
                FfilStream = null;
            }
            if (fs != null)
            {
                fs.Dispose();
                fs = null;
            }
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            FintPos = 0;
            FintReadPos = 0;
            FintBufPos = 0;
            FboBufEmpty = true;
            return FfilStream.BaseStream.Seek(offset, origin);
        }

        public int Read(byte[] Buffer, int Count)
        {
            int intNewBufPos;
            int byByteCount;
            byte byMultByte;
            byte[] poTemp;

            if (!FboCompressed)
            {
                return FfilStream.Read(Buffer, 0, Count);
            }
            else
            {
                if (Count > BUFFER_SIZE)
                {
                    return 0;
                }
            }

            if ((FintPos != FintReadPos + BUFFER_SIZE) || (FboBufEmpty))
            {
                FboBufEmpty = false;
                FintBufPos = 0;
                FintReadPos = (int)FfilStream.BaseStream.Position;
                FintPos = FintReadPos + FfilStream.Read(Fbuffer, 0, BUFFER_SIZE);
            }

            if (BUFFER_SIZE - FintBufPos < (Count * 2))
            {
                poTemp = new byte[BUFFER_SIZE - FintBufPos];
                Array.Copy(Fbuffer, FintBufPos, poTemp, 0, BUFFER_SIZE - FintBufPos);
                Array.Copy(poTemp, 0, Fbuffer, 0, BUFFER_SIZE - FintBufPos);
                FintReadPos = FintPos - (BUFFER_SIZE - FintBufPos);
                FintPos = FintReadPos + FfilStream.Read(Fbuffer, BUFFER_SIZE - FintBufPos, BUFFER_SIZE - (BUFFER_SIZE - FintBufPos)) + (BUFFER_SIZE - FintBufPos);
                FintBufPos = 0;
            }

            intNewBufPos = 0;

            while (intNewBufPos < Count)
            {
                if (Fbuffer[FintBufPos] <= 128)
                {
                    //Byte(Pointer(Integer(@Buffer) + intNewBufPos)^) = Fbuffer[FintBufPos];
                    Buffer[intNewBufPos] = Fbuffer[FintBufPos];
                    intNewBufPos++;
                    FintBufPos++;
                }
                else
                {
                    byByteCount = Fbuffer[FintBufPos] - 128;
                    FintBufPos++;
                    byMultByte = Fbuffer[FintBufPos];
                    FintBufPos++;
                    if (byByteCount + intNewBufPos > Count)
                    {
                        FintBufPos -= 2;
                        Fbuffer[FintBufPos] = (byte)(byByteCount - (Count - intNewBufPos) + 128);
                        byByteCount = Count - intNewBufPos;
                    }
                    //FillChar(Byte(Pointer(Integer(@Buffer) + intNewBufPos)^), byByteCount, byMultByte);
                    for (int i = intNewBufPos; i < intNewBufPos + byByteCount; i++)
                        Buffer[i] = byMultByte;

                    intNewBufPos += byByteCount;
                }
                if (FintBufPos > BUFFER_SIZE)
                    FboBufEmpty = true;
            }

            return Count;
        }
    }
}

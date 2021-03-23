using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JeremyAnsel.ColorQuant;

namespace CM0102Patcher
{
    public class RGNConverter
    {
        public static Bitmap ResizeImage(Image image, int width, int height, int cropLeft = 0, int cropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            if (width <= 0 || height <= 0)
            {
                width = image.Width;
                height = image.Height;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, cropLeft, cropTop, (image.Width - cropLeft) - cropRight, (image.Height - cropTop) - cropBottom, GraphicsUnit.Pixel, wrapMode);
                }
            }

            if (brightness != 0)
            {
                var tempImage = AdjustBrightness(destImage, brightness);
                destImage.Dispose();
                destImage = tempImage;
            }

            return destImage;
        }

        private static Bitmap AdjustBrightness(Image image, float brightness)
        {
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][] {
                new float[] {b, 0, 0, 0, 0},
                new float[] {0, b, 0, 0, 0},
                new float[] {0, 0, b, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1} 
            });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying
            // the new ColorMatrix.
            Point[] points = {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);

            return bm;
        }

        public static bool GetImageSize(string inFile, out int Width, out int Height)
        {
            bool ret = false;
            try
            {
                if (Path.GetExtension(inFile).ToLower() == ".rgn" || Path.GetExtension(inFile).ToLower() == ".hsr" || Path.GetExtension(inFile).ToLower() == ".mbr")
                {
                    using (var stream = File.OpenRead(inFile))
                    using (var br = new BinaryReader(stream))
                    {
                        Width = br.ReadInt32();
                        Height = br.ReadInt32();
                    }
                }
                else
                {
                    if (Path.GetExtension(inFile).ToLower() == ".pcx")
                    {
                        using (var fin = File.Open(inFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var br = new BinaryReader(fin))
                        {
                            fin.Seek(0, SeekOrigin.Begin);
                            Width = (br.ReadInt16() + 1);
                            Height = (br.ReadInt16() + 1);
                        }
                    }
                    else
                    {
                        using (var bmp = Bitmap.FromFile(inFile))
                        {
                            Width = bmp.Width;
                            Height = bmp.Height;
                        }
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to GetImageSize of: " + inFile, "GetImageSize", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ExceptionMsgBox.Show(ex);
                Width = Height = 0;
            }
            return ret;
        }

        public static void RGN2RGN(string inFile, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int CropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            using (var bmp = RGN2BMP(inFile, newWidth, newHeight, cropLeft, CropTop, cropRight, cropBottom, brightness))
            {
                BMP2RGN(bmp, outFile);
            }
        }

        public static void BMP2BMP(string inFile, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int cropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            Bitmap bmp = null;
            if (Path.GetExtension(inFile).ToLower() == ".pcx")
            {
                using (var fin = File.Open(inFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (var br = new BinaryReader(fin))
                {
                    // Read header
                    int objSize = Marshal.SizeOf(typeof(PCXHeader));
                    var bytes = br.ReadBytes(objSize);
                    var ptrObj = Marshal.AllocHGlobal(objSize);
                    Marshal.Copy(bytes, 0, ptrObj, objSize);
                    var pcxHeader = (PCXHeader)Marshal.PtrToStructure(ptrObj, typeof(PCXHeader));
                    Marshal.FreeHGlobal(ptrObj);

                    // Create Bitmap
                    bmp = new Bitmap(pcxHeader.xMax + 1, pcxHeader.yMax + 1, PixelFormat.Format24bppRgb);

                    // Get pixels
                    RLEReaderWriter.Init();
                    var imageArray = new byte[pcxHeader.bytesPerLine * bmp.Height];
                    for (int i = 0; i < imageArray.Length; i++)
                        imageArray[i] = RLEReaderWriter.ReadRLEByte(fin);

                    // Check for Palette Marker
                    br.ReadByte();

                    // Read 256 Colour Palette
                    var palette = br.ReadBytes(768);

                    // Create a 24 bit Image Array
                    var fullImageArray = new byte[bmp.Width * bmp.Height * 3];
                    for (int i = 0; i < (bmp.Width * bmp.Height); i++)
                    {
                        fullImageArray[(i * 3) + 0] = palette[(imageArray[i] * 3) + 2];
                        fullImageArray[(i * 3) + 1] = palette[(imageArray[i] * 3) + 1];
                        fullImageArray[(i * 3) + 2] = palette[(imageArray[i] * 3) + 0];
                    }

                    // Lock Bits and assign colours
                    var bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                    if (bmp.Width * 3 != bits.Stride)
                        fullImageArray = WuColorQuantizer.ImageArrayToArray(fullImageArray, bmp.Height, bmp.Width * 3, bits.Stride);

                    Marshal.Copy(fullImageArray, 0, bits.Scan0, fullImageArray.Length);

                    bmp.UnlockBits(bits);
                }
            }
            else
                bmp = new Bitmap(inFile);
            BMP2BMP(bmp, outFile, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness);
            bmp.Dispose();
        }

        public static void BMP2BMP(Bitmap bmp, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int cropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            // Resize BMP if need be
            if ((newWidth != -1 && newHeight != -1) || cropLeft != 0 || cropTop != 0 || cropRight != 0 || cropBottom != 0 || brightness != 0)
                bmp = ResizeImage(bmp, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness);

            if (Path.GetExtension(outFile).ToLower() == ".pcx")
            {
                PCXHeader pcxHeader = new PCXHeader();
                using (var fout = File.Open(outFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (var bw = new BinaryWriter(fout))
                {
                    pcxHeader.SetDimensions(bmp.Width, bmp.Height);

                    // Write Header
                    int objSize = Marshal.SizeOf(typeof(PCXHeader));
                    byte[] arr = new byte[objSize];
                    IntPtr ptr = Marshal.AllocHGlobal(objSize);
                    Marshal.StructureToPtr(pcxHeader, ptr, true);
                    Marshal.Copy(ptr, arr, 0, objSize);
                    Marshal.FreeHGlobal(ptr);
                    bw.Write(arr);

                    // Quantize
                    var reduceColorsTo = 64;
                    int bmpStride;
                    var bmpArray = WuColorQuantizer.BitmapToArray(bmp, PixelFormat.Format32bppArgb, false, out bmpStride);
                    var wubytes = new WuColorQuantizer().Quantize(bmpArray, reduceColorsTo);

                    // Copy 32 bit palette to 256 colours 24 bit palettte (768)
                    byte[] newPalette = new byte[768];
                    for (int i = 0; i < reduceColorsTo; i++)
                    {
                        newPalette[(i * 3) + 2] = wubytes.Palette[(i * 4) + 2];
                        newPalette[(i * 3) + 1] = wubytes.Palette[(i * 4) + 1];
                        newPalette[(i * 3) + 0] = wubytes.Palette[(i * 4) + 0];
                    }

                    // Write Image Data
                    RLEReaderWriter.Init();
                    for (int i = 0; i < wubytes.Bytes.Length; i++)
                    {
                        RLEReaderWriter.WriteRLEByte(fout, wubytes.Bytes[i]);
                        RLEReaderWriter.FlushRLEBytes(fout);
                    }
                    RLEReaderWriter.FlushRLEBytes(fout);

                    // Write Palette Marker
                    bw.Write((byte)0x0C);

                    // Write Palette
                    for (int i = 0; i < 256; i++)
                    {
                        bw.Write(newPalette[(i * 3) + 2]);
                        bw.Write(newPalette[(i * 3) + 1]);
                        bw.Write(newPalette[(i * 3) + 0]);
                    }
                }
            }
            else
            {
                // Convert extension to ImageFormat
                ImageFormat imgFormat;
                switch (Path.GetExtension(outFile).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        imgFormat = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        imgFormat = ImageFormat.Png;
                        break;
                    case ".gif":
                        imgFormat = ImageFormat.Gif;
                        break;
                    default:
                    case ".bmp":
                        imgFormat = ImageFormat.Bmp;
                        break;
                }

                bmp.Save(outFile, imgFormat);
            }

            // If we created a new BMP dispose of it
            if ((newWidth != -1 && newHeight != -1) || cropLeft != 0 || cropTop != 0 || cropRight != 0 || cropBottom != 0 || brightness != 0)
                bmp.Dispose();
        }

        public static void RGN2BMP(string inFile, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int CropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            using (var bmp = RGN2BMP(inFile, newWidth, newHeight, cropLeft, CropTop, cropRight, cropBottom, brightness))
            {
                // If file doesn't exist and is a actually a directory, not a directory
                if (!File.Exists(outFile) && Directory.Exists(outFile))
                {
                    outFile = Path.Combine(outFile, Path.GetFileNameWithoutExtension(inFile) + ".bmp");
                }
                BMP2BMP(bmp, outFile, newWidth, newHeight, cropLeft, CropTop, cropRight, cropBottom, brightness);
            }
        }

        public static Bitmap RGN2BMP(string inFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int CropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            using (var stream = File.OpenRead(inFile))
            using (var br = new BinaryReader(stream))
            {
                var width = br.ReadUInt32();
                var height = br.ReadUInt32();
                var bmp = new Bitmap((int)width, (int)height, PixelFormat.Format24bppRgb);
                br.BaseStream.Seek(40, SeekOrigin.Current);
                var bmpBits = bmp.LockBits(new Rectangle(0, 0, (int)width, (int)height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                var bytes = new byte[bmpBits.Stride * bmpBits.Height];
                int ptrStart = 0;
                int ptr = 0;
                for (int y = 0; y < height; y++)
                {
                    ptr = ptrStart;
                    for (int x = 0; x < width; x++)
                    {
                        var rgb16 = br.ReadUInt16();
                        var _5bitRedChannel = (rgb16 & 0xF800) >> 11;
                        var _6bitGreenChannel = (rgb16 & 0x7E0) >> 5;
                        var _5bitBlueChannel = (rgb16 & 0x1f);

                        bytes[ptr++] = (byte)((_5bitBlueChannel * 255 + 15) / 31);
                        bytes[ptr++] = (byte)((_6bitGreenChannel * 255 + 31) / 63);
                        bytes[ptr++] = (byte)((_5bitRedChannel * 255 + 15) / 31);
                    }
                    ptrStart += bmpBits.Stride;
                }
                System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bmpBits.Scan0, bytes.Length);
                bmp.UnlockBits(bmpBits);
                if (newWidth == -1 && newHeight == -1 && cropLeft == 0 && CropTop == 0 && cropRight == 0 && cropBottom == 0 && brightness == 0)
                {
                    return bmp;
                }
                else
                {
                    var newBMP = ResizeImage(bmp, newWidth, newHeight, cropLeft, CropTop, cropRight, cropBottom, brightness);
                    bmp.Dispose();
                    return newBMP;
                }
            }
        }

        public static void BMP2RGN(string inFile, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int cropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            using (var bmp = new Bitmap(inFile))
            {
                BMP2RGN(bmp, outFile, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness);
            }
        }

        public static void BMP2RGN(Bitmap bmp, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int cropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            // Resize BMP if need be
            if ((newWidth != -1 && newHeight != -1) || cropLeft != 0 || cropTop != 0 || cropRight != 0 || cropBottom != 0 || brightness != 0)
                bmp = ResizeImage(bmp, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness);

            using (var stream = File.Create(outFile))
            using (var rgnFile = new BinaryWriter(stream))
            {
                var width = bmp.Width;
                var height = bmp.Height;
                rgnFile.Write(width);
                rgnFile.Write(height);
                rgnFile.Write((UInt32)(width * height * 2));
                // Header RGB565
                var header = "144C68012000000040000000000000001000000000F80000E00700001F00000000000000";
                for (int i = 0; i < header.Length; i += 2)
                {
                    string byteValue = header.Substring(i, 2);
                    rgnFile.Write(byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
                }

                var bmpBits = bmp.LockBits(new Rectangle(0, 0, (int)width, (int)height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                byte[] bytes = new byte[bmpBits.Stride * bmpBits.Height];
                System.Runtime.InteropServices.Marshal.Copy(bmpBits.Scan0, bytes, 0, bytes.Length);
                bmp.UnlockBits(bmpBits);

                int ptrStart = 0;
                int ptr = 0;
                for (int y = 0; y < height; y++)
                {
                    ptr = ptrStart;
                    for (int x = 0; x < width; x++)
                    {
                        UInt32 blue = (UInt32)(bytes[ptr++] * 31 + 127) / 255;
                        UInt32 green = (UInt32)(bytes[ptr++] * 63 + 127) / 255;
                        UInt32 red = (UInt32)(bytes[ptr++] * 31 + 127) / 255;
                        UInt16 RGB565pixel = (UInt16)((red << 11) | (green << 5) | blue);
                        rgnFile.Write(RGB565pixel);
                    }
                    ptrStart += bmpBits.Stride;
                }
            }

            // If we created a new BMP dispose of it
            if ((newWidth != -1 && newHeight != -1) || cropLeft != 0 || cropTop != 0 || cropRight != 0 || cropBottom != 0 || brightness != 0)
                bmp.Dispose();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PCXHeader
    {
        public byte id = 10;
        public byte version = 5;
        public byte encoding = 1;
        public byte bitsPerPixel = 8;
        public ushort xMin = 0;
        public ushort yMin = 0;
        public ushort xMax = 640 - 1;
        public ushort yMax = 480 - 1;
        public ushort hDpi = 150;
        public ushort vDpi = 150;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)] public byte[] colorMap = new byte[48];
        public byte reserved = 0;
        public byte nPlanes = 1;
        public ushort bytesPerLine = 640;
        public ushort paletteInfo = 1;
        public ushort horizScreenResolution = 0;
        public ushort vertScreenResolution = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 54)] public byte[] filler = new byte[58];

        public void SetDimensions(int width, int height)
        {
            xMax = (ushort)(width - 1);
            yMax = (ushort)(height - 1);
            bytesPerLine = (ushort)width;
        }
    }

    public class RLEReaderWriter
    {
        static private byte m_lastValue;
        static private uint m_count = 0;

        public static void Init()
        {
            m_count = 0;
        }

        public static void WriteRLEByte(Stream fout, byte value)
        {
            if (m_count == 0 || m_count == 63 || value != m_lastValue)
            {
                FlushRLEBytes(fout);

                m_lastValue = value;
                m_count = 1;
            }
            else
            {
                m_count++;
            }
        }

        public static void FlushRLEBytes(Stream fout)
        {
            if (m_count == 0)
                return;

            if ((m_count > 1) || ((m_count == 1) && ((m_lastValue & 0xC0) == 0xC0)))
            {
                fout.WriteByte((byte)(0xC0 | m_count));
                fout.WriteByte(m_lastValue);
                m_count = 0;
            }
            else
            {
                fout.WriteByte(m_lastValue);
                m_count = 0;
            }
        }


        public static byte ReadRLEByte(Stream fin)
        {
            if (m_count > 0)
            {
                m_count--;
                return m_lastValue;
            }

            byte code = (byte)fin.ReadByte();

            if ((code & 0xC0) == 0xC0)
            {
                m_count = (uint)(code & (0xC0 ^ 0xff));
                m_lastValue = (byte)fin.ReadByte();

                m_count--;
                return m_lastValue;
            }

            return code;
        }
    }
}

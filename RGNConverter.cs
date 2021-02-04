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
                    using (var bmp = Bitmap.FromFile(inFile))
                    {
                        Width = bmp.Width;
                        Height = bmp.Height;
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

        public static void RGN2BMP(string inFile, string outFile, int newWidth = -1, int newHeight = -1, int cropLeft = 0, int CropTop = 0, int cropRight = 0, int cropBottom = 0, float brightness = 0)
        {
            using (var bmp = RGN2BMP(inFile, newWidth, newHeight, cropLeft, CropTop, cropRight, cropBottom, brightness))
            {

                if (!File.Exists(outFile) && Directory.Exists(outFile))
                {
                    outFile = Path.Combine(outFile, Path.GetFileNameWithoutExtension(inFile) + ".bmp");
                }

                bmp.Save(outFile, ImageFormat.Bmp);
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
                bmp = ResizeImage(bmp, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom);

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
}

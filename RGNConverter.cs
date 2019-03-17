using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;


namespace CM0102Patcher
{
    public class RGNConverter
    {
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
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
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void RGN2RGN(string inFile, string outFile, int newWidth = -1, int newHeight = -1)
        {
            using (var bmp = RGN2BMP(inFile, newWidth, newHeight))
            {
                BMP2RGN(bmp, outFile);
            }
        }

        public static void RGN2BMP(string inFile, string outFile, int newWidth = -1, int newHeight = -1)
        {
            using (var bmp = RGN2BMP(inFile, newWidth, newHeight))
            {
                bmp.Save(outFile, ImageFormat.Bmp);
            }
        }

        public static Bitmap RGN2BMP(string inFile, int newWidth = -1, int newHeight = -1)
        {
            using (var br = new BinaryReader(File.OpenRead(inFile)))
            {
                var width = br.ReadUInt32();
                var height = br.ReadUInt32();
                var bmp = new Bitmap((int)width, (int)height);
                br.BaseStream.Seek(40, SeekOrigin.Current);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var rgb16 = br.ReadUInt16();
                        var _5bitRedChannel = (rgb16 & 0xF800) >> 11;
                        var _6bitGreenChannel = (rgb16 & 0x7E0) >> 5;
                        var _5bitBlueChannel = (rgb16 & 0x1f);
                        int red = (_5bitRedChannel * 255 + 15) / 31;
                        int green = (_6bitGreenChannel * 255 + 31) / 63;
                        int blue = (_5bitBlueChannel * 255 + 15) / 31;

                        bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }
                if (newWidth == -1 && newHeight == -1)
                {
                    return bmp;
                }
                else
                {
                    var newBMP = ResizeImage(bmp, newWidth, newHeight);
                    bmp.Dispose();
                    return newBMP;
                }
            }
        }

        public static void BMP2RGN(string inFile, string outFile)
        {
            using (var bmp = new Bitmap(inFile))
            {
                BMP2RGN(bmp, outFile);
            }
        }

        public static void BMP2RGN(Bitmap bmp, string outFile)
        {
            using (var rgnFile = new BinaryWriter(File.Create(outFile)))
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
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var col = bmp.GetPixel(x, y);
                        UInt32 red = (UInt32)(col.R * 31 + 127) / 255;
                        UInt32 green = (UInt32)(col.G * 63 + 127) / 255;
                        UInt32 blue = (UInt32)(col.B * 31 + 127) / 255;
                        UInt16 RGB565pixel = (UInt16)((red << 11) | (green << 5) | blue);
                        rgnFile.Write(RGB565pixel);
                    }
                }
            }
        }
    }
}

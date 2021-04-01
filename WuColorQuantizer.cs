// <copyright file="WuColorQuantizer.cs" company="Jérémy Ansel">
// Copyright (c) 2014-2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>
// https://github.com/JeremyAnsel/JeremyAnsel.ColorQuant

namespace JeremyAnsel.ColorQuant
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    /// Based on C Implementation of Xiaolin Wu's Color Quantizer (v. 2)
    /// (see Graphics Gems volume II, pages 126-133)
    /// (<see href="http://www.ece.mcmaster.ca/~xwu/cq.c"/>).
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Wu", Justification = "Reviewed")]
    public sealed class WuColorQuantizer
    {
        private const int IndexBits = 7;
        private const int IndexCount = (1 << WuColorQuantizer.IndexBits) + 1;
        private const int TableLength = WuColorQuantizer.IndexCount * WuColorQuantizer.IndexCount * WuColorQuantizer.IndexCount;
        private readonly long[] vwt = new long[WuColorQuantizer.TableLength];
        private readonly long[] vmr = new long[WuColorQuantizer.TableLength];
        private readonly long[] vmg = new long[WuColorQuantizer.TableLength];
        private readonly long[] vmb = new long[WuColorQuantizer.TableLength];
        private readonly double[] m2 = new double[WuColorQuantizer.TableLength];
        private readonly byte[] tag = new byte[WuColorQuantizer.TableLength];
        public ColorQuantizerResult Quantize(byte[] image)
        {
            return this.Quantize(image, 256);
        }

        public ColorQuantizerResult Quantize(byte[] image, int colorCount)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (colorCount < 1 || colorCount > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(colorCount));
            }

            this.Clear();

            this.Build3DHistogram(image);
            this.Get3DMoments();

            Box[] cube;
            this.BuildCube(out cube, ref colorCount);

            return this.GenerateResult(image, colorCount, cube);
        }

        private static int GetIndex(int r, int g, int b)
        {
            return (r << (WuColorQuantizer.IndexBits * 2)) + (r << (WuColorQuantizer.IndexBits + 1)) + (g << WuColorQuantizer.IndexBits) + r + g + b;
        }

        private static double Volume(Box cube, long[] moment)
        {
            return moment[WuColorQuantizer.GetIndex(cube.R1, cube.G1, cube.B1)]
               - moment[WuColorQuantizer.GetIndex(cube.R1, cube.G1, cube.B0)]
               - moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B1)]
               + moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B0)]
               - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B1)]
               + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B0)]
               + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B1)]
               - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B0)];
        }

        private static long Bottom(Box cube, int direction, long[] moment)
        {
            switch (direction)
            {
                // Red
                case 2:
                    return -moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B1)]
                        + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B0)]
                        + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B1)]
                        - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B0)];

                // Green
                case 1:
                    return -moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B1)]
                        + moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B0)]
                        + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B1)]
                        - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B0)];

                // Blue
                case 0:
                    return -moment[WuColorQuantizer.GetIndex(cube.R1, cube.G1, cube.B0)]
                        + moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B0)]
                        + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B0)]
                        - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B0)];

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private static long Top(Box cube, int direction, int position, long[] moment)
        {
            switch (direction)
            {
                // Red
                case 2:
                    return moment[WuColorQuantizer.GetIndex(position, cube.G1, cube.B1)]
                       - moment[WuColorQuantizer.GetIndex(position, cube.G1, cube.B0)]
                       - moment[WuColorQuantizer.GetIndex(position, cube.G0, cube.B1)]
                       + moment[WuColorQuantizer.GetIndex(position, cube.G0, cube.B0)];

                // Green
                case 1:
                    return moment[WuColorQuantizer.GetIndex(cube.R1, position, cube.B1)]
                       - moment[WuColorQuantizer.GetIndex(cube.R1, position, cube.B0)]
                       - moment[WuColorQuantizer.GetIndex(cube.R0, position, cube.B1)]
                       + moment[WuColorQuantizer.GetIndex(cube.R0, position, cube.B0)];

                // Blue
                case 0:
                    return moment[WuColorQuantizer.GetIndex(cube.R1, cube.G1, position)]
                       - moment[WuColorQuantizer.GetIndex(cube.R1, cube.G0, position)]
                       - moment[WuColorQuantizer.GetIndex(cube.R0, cube.G1, position)]
                       + moment[WuColorQuantizer.GetIndex(cube.R0, cube.G0, position)];

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private void Clear()
        {
            Array.Clear(this.vwt, 0, WuColorQuantizer.TableLength);
            Array.Clear(this.vmr, 0, WuColorQuantizer.TableLength);
            Array.Clear(this.vmg, 0, WuColorQuantizer.TableLength);
            Array.Clear(this.vmb, 0, WuColorQuantizer.TableLength);
            Array.Clear(this.m2, 0, WuColorQuantizer.TableLength);

            Array.Clear(this.tag, 0, WuColorQuantizer.TableLength);
        }

        private void Build3DHistogram(byte[] image)
        {
            for (int i = 0; i < image.Length; i += 4)
            {
                int r = image[i + 2];
                int g = image[i + 1];
                int b = image[i];

                int inr = r >> (8 - WuColorQuantizer.IndexBits);
                int ing = g >> (8 - WuColorQuantizer.IndexBits);
                int inb = b >> (8 - WuColorQuantizer.IndexBits);

                int ind = WuColorQuantizer.GetIndex(inr + 1, ing + 1, inb + 1);

                this.vwt[ind]++;
                this.vmr[ind] += r;
                this.vmg[ind] += g;
                this.vmb[ind] += b;
                this.m2[ind] += (r * r) + (g * g) + (b * b);
            }
        }

        private void Get3DMoments()
        {
            long[] area = new long[WuColorQuantizer.IndexCount];
            long[] areaR = new long[WuColorQuantizer.IndexCount];
            long[] areaG = new long[WuColorQuantizer.IndexCount];
            long[] areaB = new long[WuColorQuantizer.IndexCount];
            double[] area2 = new double[WuColorQuantizer.IndexCount];

            for (int r = 1; r < WuColorQuantizer.IndexCount; r++)
            {
                Array.Clear(area, 0, WuColorQuantizer.IndexCount);
                Array.Clear(areaR, 0, WuColorQuantizer.IndexCount);
                Array.Clear(areaG, 0, WuColorQuantizer.IndexCount);
                Array.Clear(areaB, 0, WuColorQuantizer.IndexCount);
                Array.Clear(area2, 0, WuColorQuantizer.IndexCount);

                for (int g = 1; g < WuColorQuantizer.IndexCount; g++)
                {
                    long line = 0;
                    long lineR = 0;
                    long lineG = 0;
                    long lineB = 0;
                    double line2 = 0;

                    for (int b = 1; b < WuColorQuantizer.IndexCount; b++)
                    {
                        int ind1 = WuColorQuantizer.GetIndex(r, g, b);

                        line += this.vwt[ind1];
                        lineR += this.vmr[ind1];
                        lineG += this.vmg[ind1];
                        lineB += this.vmb[ind1];
                        line2 += this.m2[ind1];

                        area[b] += line;
                        areaR[b] += lineR;
                        areaG[b] += lineG;
                        areaB[b] += lineB;
                        area2[b] += line2;

                        int ind2 = ind1 - WuColorQuantizer.GetIndex(1, 0, 0);

                        this.vwt[ind1] = this.vwt[ind2] + area[b];
                        this.vmr[ind1] = this.vmr[ind2] + areaR[b];
                        this.vmg[ind1] = this.vmg[ind2] + areaG[b];
                        this.vmb[ind1] = this.vmb[ind2] + areaB[b];
                        this.m2[ind1] = this.m2[ind2] + area2[b];
                    }
                }
            }
        }

        private double Variance(Box cube)
        {
            double dr = WuColorQuantizer.Volume(cube, this.vmr);
            double dg = WuColorQuantizer.Volume(cube, this.vmg);
            double db = WuColorQuantizer.Volume(cube, this.vmb);

            double xx = this.m2[WuColorQuantizer.GetIndex(cube.R1, cube.G1, cube.B1)]
             - this.m2[WuColorQuantizer.GetIndex(cube.R1, cube.G1, cube.B0)]
             - this.m2[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B1)]
             + this.m2[WuColorQuantizer.GetIndex(cube.R1, cube.G0, cube.B0)]
             - this.m2[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B1)]
             + this.m2[WuColorQuantizer.GetIndex(cube.R0, cube.G1, cube.B0)]
             + this.m2[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B1)]
             - this.m2[WuColorQuantizer.GetIndex(cube.R0, cube.G0, cube.B0)];

            return xx - (((dr * dr) + (dg * dg) + (db * db)) / WuColorQuantizer.Volume(cube, this.vwt));
        }

        private double Maximize(Box cube, int direction, int first, int last, out int cut, double wholeR, double wholeG, double wholeB, double wholeW)
        {
            long baseR = WuColorQuantizer.Bottom(cube, direction, this.vmr);
            long baseG = WuColorQuantizer.Bottom(cube, direction, this.vmg);
            long baseB = WuColorQuantizer.Bottom(cube, direction, this.vmb);
            long baseW = WuColorQuantizer.Bottom(cube, direction, this.vwt);

            double max = 0.0;
            cut = -1;

            for (int i = first; i < last; i++)
            {
                double halfR = baseR + WuColorQuantizer.Top(cube, direction, i, this.vmr);
                double halfG = baseG + WuColorQuantizer.Top(cube, direction, i, this.vmg);
                double halfB = baseB + WuColorQuantizer.Top(cube, direction, i, this.vmb);
                double halfW = baseW + WuColorQuantizer.Top(cube, direction, i, this.vwt);

                if (halfW == 0)
                {
                    continue;
                }

                double temp = ((halfR * halfR) + (halfG * halfG) + (halfB * halfB)) / halfW;

                halfR = wholeR - halfR;
                halfG = wholeG - halfG;
                halfB = wholeB - halfB;
                halfW = wholeW - halfW;

                if (halfW == 0)
                {
                    continue;
                }

                temp += ((halfR * halfR) + (halfG * halfG) + (halfB * halfB)) / halfW;

                if (temp > max)
                {
                    max = temp;
                    cut = i;
                }
            }

            return max;
        }

        private bool Cut(Box set1, Box set2)
        {
            double wholeR = WuColorQuantizer.Volume(set1, this.vmr);
            double wholeG = WuColorQuantizer.Volume(set1, this.vmg);
            double wholeB = WuColorQuantizer.Volume(set1, this.vmb);
            double wholeW = WuColorQuantizer.Volume(set1, this.vwt);

            int cutr;
            int cutg;
            int cutb;

            double maxr = this.Maximize(set1, 2, set1.R0 + 1, set1.R1, out cutr, wholeR, wholeG, wholeB, wholeW);
            double maxg = this.Maximize(set1, 1, set1.G0 + 1, set1.G1, out cutg, wholeR, wholeG, wholeB, wholeW);
            double maxb = this.Maximize(set1, 0, set1.B0 + 1, set1.B1, out cutb, wholeR, wholeG, wholeB, wholeW);

            int dir;

            if ((maxr >= maxg) && (maxr >= maxb))
            {
                dir = 2;

                if (cutr < 0)
                {
                    return false;
                }
            }
            else if ((maxg >= maxr) && (maxg >= maxb))
            {
                dir = 1;
            }
            else
            {
                dir = 0;
            }

            set2.R1 = set1.R1;
            set2.G1 = set1.G1;
            set2.B1 = set1.B1;

            switch (dir)
            {
                // Red
                case 2:
                    set2.R0 = set1.R1 = cutr;
                    set2.G0 = set1.G0;
                    set2.B0 = set1.B0;
                    break;

                // Green
                case 1:
                    set2.G0 = set1.G1 = cutg;
                    set2.R0 = set1.R0;
                    set2.B0 = set1.B0;
                    break;

                // Blue
                case 0:
                    set2.B0 = set1.B1 = cutb;
                    set2.R0 = set1.R0;
                    set2.G0 = set1.G0;
                    break;
            }

            set1.Volume = (set1.R1 - set1.R0) * (set1.G1 - set1.G0) * (set1.B1 - set1.B0);
            set2.Volume = (set2.R1 - set2.R0) * (set2.G1 - set2.G0) * (set2.B1 - set2.B0);

            return true;
        }

        private void Mark(Box cube, byte label)
        {
            for (int r = cube.R0 + 1; r <= cube.R1; r++)
            {
                for (int g = cube.G0 + 1; g <= cube.G1; g++)
                {
                    for (int b = cube.B0 + 1; b <= cube.B1; b++)
                    {
                        this.tag[WuColorQuantizer.GetIndex(r, g, b)] = label;
                    }
                }
            }
        }

        private void BuildCube(out Box[] cube, ref int colorCount)
        {
            cube = new Box[colorCount];
            double[] vv = new double[colorCount];

            for (int i = 0; i < colorCount; i++)
            {
                cube[i] = new Box();
            }

            cube[0].R0 = cube[0].G0 = cube[0].B0 = 0;
            cube[0].R1 = cube[0].G1 = cube[0].B1 = WuColorQuantizer.IndexCount - 1;

            int next = 0;

            for (int i = 1; i < colorCount; i++)
            {
                if (this.Cut(cube[next], cube[i]))
                {
                    vv[next] = cube[next].Volume > 1 ? this.Variance(cube[next]) : 0.0;
                    vv[i] = cube[i].Volume > 1 ? this.Variance(cube[i]) : 0.0;
                }
                else
                {
                    vv[next] = 0.0;
                    i--;
                }

                next = 0;

                double temp = vv[0];
                for (int k = 1; k <= i; k++)
                {
                    if (vv[k] > temp)
                    {
                        temp = vv[k];
                        next = k;
                    }
                }

                if (temp <= 0.0)
                {
                    colorCount = i + 1;
                    break;
                }
            }
        }

        private ColorQuantizerResult GenerateResult(byte[] image, int colorCount, Box[] cube)
        {
            var quantizedImage = new ColorQuantizerResult(image.Length / 4, colorCount);

            for (int k = 0; k < colorCount; k++)
            {
                this.Mark(cube[k], (byte)k);

                double weight = WuColorQuantizer.Volume(cube[k], this.vwt);

                if (weight != 0)
                {
                    quantizedImage.Palette[(k * 4) + 3] = 0xff;
                    quantizedImage.Palette[(k * 4) + 2] = (byte)(WuColorQuantizer.Volume(cube[k], this.vmr) / weight);
                    quantizedImage.Palette[(k * 4) + 1] = (byte)(WuColorQuantizer.Volume(cube[k], this.vmg) / weight);
                    quantizedImage.Palette[k * 4] = (byte)(WuColorQuantizer.Volume(cube[k], this.vmb) / weight);
                }
                else
                {
                    quantizedImage.Palette[(k * 4) + 3] = 0xff;
                    quantizedImage.Palette[(k * 4) + 2] = 0;
                    quantizedImage.Palette[(k * 4) + 1] = 0;
                    quantizedImage.Palette[k * 4] = 0;
                }
            }

            for (int i = 0; i < image.Length / 4; i++)
            {
                int r = image[(i * 4) + 2] >> (8 - WuColorQuantizer.IndexBits);
                int g = image[(i * 4) + 1] >> (8 - WuColorQuantizer.IndexBits);
                int b = image[i * 4] >> (8 - WuColorQuantizer.IndexBits);

                int ind = WuColorQuantizer.GetIndex(r + 1, g + 1, b + 1);

                quantizedImage.Bytes[i] = this.tag[ind];
            }

            return quantizedImage;
        }

        public static byte[] ImageArrayToArray(byte[] src, int Height, int srcStride, int dstStride)
        {
            byte[] ret = new byte[Height * dstStride];
            for (int row = 0; row < Height; row++)
            {
                Array.Copy(src, row * srcStride, ret, row * dstStride, dstStride);
            }
            return ret;
        }

        public static byte[] BitmapToArray(Bitmap bmp, PixelFormat pixfmt, bool withFullStride, out int stride)
        {
            int bytesPerPixel;

            switch (pixfmt)
            {
                default:
                case PixelFormat.Format32bppArgb:
                    bytesPerPixel = 4;
                    break;
                case PixelFormat.Format8bppIndexed:
                    bytesPerPixel = 1;
                    break;
            }

            var bits = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, pixfmt);
            var bytes = new byte[bits.Stride * bmp.Height];
            Marshal.Copy(bits.Scan0, bytes, 0, bytes.Length);
            stride = bits.Stride;
            bmp.UnlockBits(bits);

            if (!withFullStride && bits.Stride != bmp.Width * bytesPerPixel)
            {
                var newBytes = new byte[bmp.Width * bmp.Height * bytesPerPixel];
                int rowSrcPtr = 0;
                int rowDstPtr = 0;
                var rowLength = bmp.Width * bytesPerPixel;
                for (int row = 0; row < bmp.Height; row++)
                {
                    Array.Copy(bytes, rowSrcPtr, newBytes, rowDstPtr, rowLength);
                    rowSrcPtr += bits.Stride;
                    rowDstPtr += rowLength;
                }
                bytes = newBytes;
            }

            return bytes;
        }
    }

    internal sealed class Box
    {
        public int R0 { get; set; }
        public int R1 { get; set; }
        public int G0 { get; set; }
        public int G1 { get; set; }
        public int B0 { get; set; }
        public int B1 { get; set; }
        public int A0 { get; set; }
        public int A1 { get; set; }
        public int Volume { get; set; }
    }

    public sealed class ColorQuantizerResult
    {
        public ColorQuantizerResult(int size, int colorCount)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (colorCount < 1 || colorCount > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(colorCount));
            }

            this.Palette = new byte[colorCount * 4];
            this.Bytes = new byte[size];
        }

        public Bitmap ToBitmap(int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var col = Bytes[(y * width) + x];
                    var r = Palette[(col * 4) + 2];
                    var g = Palette[(col * 4) + 1];
                    var b = Palette[(col * 4) + 0];
                    bmp.SetPixel(x, y, Color.FromArgb(r,g,b));
                }
            }
            return bmp;
            //var ptr = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //WuColorQuantizer.ImageArrayToArray(Bytes, height, width)
        }

        public byte[] DitherBitmap(Bitmap orig, Bitmap bmp)
        {
            int orig_stride, bmp_stride;
            var orig_array = WuColorQuantizer.BitmapToArray(orig, PixelFormat.Format24bppRgb, true, out orig_stride);
            var bmp_array = WuColorQuantizer.BitmapToArray(bmp, PixelFormat.Format24bppRgb, true, out bmp_stride);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    byte old_r = orig_array[((y * orig_stride) + (x*3)) + 2];
                    byte old_g = orig_array[((y * orig_stride) + (x * 3)) + 1];
                    byte old_b = orig_array[((y * orig_stride) + (x * 3)) + 0];
                    byte new_r = bmp_array[((y * bmp_stride) + (x * 3)) + 2];
                    byte new_g = bmp_array[((y * bmp_stride) + (x * 3)) + 1];
                    byte new_b = bmp_array[((y * bmp_stride) + (x * 3)) + 0];

                    double r_error = old_r - new_r;
                    double g_error = old_g - new_g;
                    double b_error = old_b - new_b;

                    // Red first
                    if (x != bmp.Width - 1 && y != bmp.Height - 1)
                    {
                        bmp_array[(((y) * bmp_stride) + ((x + 1) * 3)) + 2] = Clamp((((double)bmp_array[((y * bmp_stride) + ((x + 1) * 3)) + 2]) + (r_error * (7.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 2] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 2]) + (r_error * (3.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 2] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 2]) + (r_error * (5.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 2] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 2]) + (r_error * (1.0 / 16.0))));

                        // Green
                        bmp_array[(((y) * bmp_stride) + ((x + 1) * 3)) + 1] = Clamp((((double)bmp_array[((y * bmp_stride) + ((x + 1) * 3)) + 1]) + (g_error * (7.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 1] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 1]) + (g_error * (3.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 1] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 1]) + (g_error * (5.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 1] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 1]) + (g_error * (1.0 / 16.0))));

                        // Blue
                        bmp_array[(((y) * bmp_stride) + ((x + 1) * 3)) + 0] = Clamp((((double)bmp_array[((y * bmp_stride) + ((x + 1) * 3)) + 0]) + (b_error * (7.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 0] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x - 1) * 3)) + 0]) + (b_error * (3.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 0] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x) * 3)) + 0]) + (b_error * (5.0 / 16.0))));
                        bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 0] = Clamp((((double)bmp_array[(((y + 1) * bmp_stride) + ((x + 1) * 3)) + 0]) + (b_error * (1.0 / 16.0))));
                    }
                    /*
                    pixel[x + 1][y] := pixel[x + 1][y] + quant_error × 7 / 16
                    pixel[x - 1][y + 1] := pixel[x - 1][y + 1] + quant_error × 3 / 16
                    pixel[x][y + 1] := pixel[x][y + 1] + quant_error × 5 / 16
                    pixel[x + 1][y + 1] := pixel[x + 1][y + 1] + quant_error × 1 / 16
                    */
                }
            }

            // Match each colour to a colour in the palette
            var dithered_array = new byte[bmp.Height * bmp.Width];
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    byte r = bmp_array[(((y) * bmp_stride) + ((x) * 3)) + 2];
                    byte g = bmp_array[(((y) * bmp_stride) + ((x) * 3)) + 1];
                    byte b = bmp_array[(((y) * bmp_stride) + ((x) * 3)) + 0];
                    dithered_array[(y * bmp.Width) + x] = MapToPalette(r, g, b);
                }
            }

            var ptr = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmp_array, 0, ptr.Scan0, bmp_array.Length);
            bmp.UnlockBits(ptr);

            Bytes = dithered_array;

            return dithered_array;
        }

        byte MapToPalette(byte r, byte g, byte b)
        {
            int bestErr = -1;
            byte best = 0;
            for (int i = 0; i < Palette.Length; i+=4)
            {
                int err = Math.Abs(((int)r) - ((int)Palette[i + 2])) + Math.Abs(((int)g) - ((int)Palette[i + 1])) + Math.Abs(((int)b) - ((int)Palette[i + 0]));
                if (bestErr == -1 || err < bestErr)
                {
                    best = (byte)(i / 4);
                    bestErr = err;
                }
            }
            return best;
        }

        byte Clamp(double d)
        {
            return (byte)(Math.Max(0, Math.Min(255, d)) + 0.5);
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Reviewed")]
        public byte[] Palette { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Reviewed")]
        public byte[] Bytes;// { get; private set; }
    }
}

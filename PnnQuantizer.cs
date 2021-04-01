using CM0102Patcher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

/* Fast pairwise nearest neighbor based algorithm for multilevel thresholding
Copyright (C) 2004-2016 Mark Tyler and Dmitry Groshev
Copyright (c) 2018-2021 Miller Cy Chan
* error measure; time used is proportional to number of bins squared - WJ */

namespace PnnQuant
{
    public class PnnQuantizer
    {
        protected byte alphaThreshold = 0;
        protected bool hasSemiTransparency = false;
        protected int m_transparentPixelIndex = -1;
        protected Color m_transparentColor = Color.Transparent;
        protected readonly Random rand = new Random();
        protected readonly Dictionary<int, ushort[]> closestMap = new Dictionary<int, ushort[]>();
        protected readonly Dictionary<int, ushort> nearestMap = new Dictionary<int, ushort>();

        protected const int PropertyTagIndexTransparent = 0x5104;
        private sealed class Pnnbin
        {
            internal float ac, rc, gc, bc;
            internal int cnt;
            internal int nn, fw, bk, tm, mtm;
            internal float err;
        }
        protected int GetARGBIndex(int argb, bool hasSemiTransparency)
        {
            var c = Color.FromArgb(argb);
            if (hasSemiTransparency)
                return (c.A & 0xF0) << 8 | (c.R & 0xF0) << 4 | (c.G & 0xF0) | (c.B >> 4);
            return (c.R & 0xF8) << 8 | (c.G & 0xFC) << 3 | (c.B >> 3);
        }

        protected double Sqr(double value)
        {
            return value * value;
        }
        private void Find_nn(Pnnbin[] bins, int idx)
        {
            int nn = 0;
            var err = 1e100;

            var bin1 = bins[idx];
            var n1 = bin1.cnt;
            var wa = bin1.ac;
            var wr = bin1.rc;
            var wg = bin1.gc;
            var wb = bin1.bc;
            for (int i = bin1.fw; i != 0; i = bins[i].fw)
            {
                var nerr = Sqr(bins[i].ac - wa) + Sqr(bins[i].rc - wr) + Sqr(bins[i].gc - wg) + Sqr(bins[i].bc - wb);
                var n2 = bins[i].cnt;
                nerr *= (n1 * n2) / (n1 + n2);
                if (nerr >= err)
                    continue;
                err = nerr;
                nn = i;
            }
            bin1.err = (float) err;
            bin1.nn = nn;
        }
        protected virtual void Pnnquan(int[] pixels, Color[] palettes, int nMaxColors, short quan_rt)
        {
            var bins = new Pnnbin[65536];

            /* Build histogram */
            foreach (var pixel in pixels)
            {
                // !!! Can throw gamma correction in here, but what to do about perceptual
                // !!! nonuniformity then?
                var c = Color.FromArgb(pixel);
                int a = c.A;
                if (a <= alphaThreshold)
                {
                    int index0 = GetARGBIndex(m_transparentColor.ToArgb(), hasSemiTransparency);
                    if (bins[index0] == null)
                        bins[index0] = new Pnnbin();
                    bins[index0].cnt++;
                    continue;
                }
                if (a < Byte.MaxValue)
                {
                    int alpha = a * 2;
                    a = alpha > Byte.MaxValue ? Byte.MaxValue : alpha;
                    c = Color.FromArgb(a, c.R, c.G, c.B);
                }

                int index = GetARGBIndex(c.ToArgb(), hasSemiTransparency);
                if (bins[index] == null)
                    bins[index] = new Pnnbin();
                bins[index].ac += c.A;
                bins[index].rc += c.R;
                bins[index].gc += c.G;
                bins[index].bc += c.B;
                bins[index].cnt++;
            }

            /* Cluster nonempty bins at one end of array */
            int maxbins = 0;
            for (int i = 0; i < bins.Length; ++i)
            {
                if (bins[i] == null)
                    continue;

                var d = 1.0f / (float)bins[i].cnt;
                bins[i].ac *= d;
                bins[i].rc *= d;
                bins[i].gc *= d;
                bins[i].bc *= d;

                bins[maxbins++] = bins[i];
            }

            if (nMaxColors < 16)
                nMaxColors = -1;
            if (Sqr(nMaxColors) / maxbins < .022)
                quan_rt = 0;

            if (quan_rt > 0)
                bins[0].cnt = (int)Math.Sqrt(bins[0].cnt);
            else if (quan_rt < 0)
                bins[0].cnt = (int)MiscFunctions.Cbrt(bins[0].cnt);
            for (int i = 0; i < maxbins - 1; ++i)
            {
                bins[i].fw = i + 1;
                bins[i + 1].bk = i;

                if (quan_rt > 0)
                    bins[i + 1].cnt = (int)Math.Sqrt(bins[i + 1].cnt);
                else if (quan_rt < 0)
                    bins[i + 1].cnt = (int)MiscFunctions.Cbrt(bins[i + 1].cnt);
            }            

            int h, l, l2;
            /* Initialize nearest neighbors and build heap of them */
            var heap = new int[bins.Length + 1];
            for (int i = 0; i < maxbins; ++i)
            {
                Find_nn(bins, i);
                /* Push slot on heap */
                double err = bins[i].err;
                for (l = ++heap[0]; l > 1; l = l2)
                {
                    l2 = l >> 1;
                    if (bins[h = heap[l2]].err <= err)
                        break;
                    heap[l] = h;
                }
                heap[l] = i;
            }

            /* Merge bins which increase error the least */
            int extbins = maxbins - nMaxColors;
            for (int i = 0; i < extbins;)
            {
                Pnnbin tb;
                /* Use heap to find which bins to merge */
                for (; ; )
                {
                    int b1 = heap[1];
                    tb = bins[b1]; /* One with least error */
                    /* Is stored error up to date? */
                    if ((tb.tm >= tb.mtm) && (bins[tb.nn].mtm <= tb.tm))
                        break;
                    if (tb.mtm == 0xFFFF) /* Deleted node */
                        b1 = heap[1] = heap[heap[0]--];
                    else /* Too old error value */
                    {
                        Find_nn(bins, b1);
                        tb.tm = i;
                    }
                    /* Push slot down */
                    var err = bins[b1].err;
                    for (l = 1; (l2 = l + l) <= heap[0]; l = l2)
                    {
                        if ((l2 < heap[0]) && (bins[heap[l2]].err > bins[heap[l2 + 1]].err))
                            ++l2;
                        if (err <= bins[h = heap[l2]].err)
                            break;
                        heap[l] = h;
                    }
                    heap[l] = b1;
                }

                /* Do a merge */
                var nb = bins[tb.nn];
                var n1 = tb.cnt;
                var n2 = nb.cnt;
                var d = 1.0f / (n1 + n2);
                tb.ac = d * (n1 * tb.ac + n2 * nb.ac);
                tb.rc = d * (n1 * tb.rc + n2 * nb.rc);
                tb.gc = d * (n1 * tb.gc + n2 * nb.gc);
                tb.bc = d * (n1 * tb.bc + n2 * nb.bc);
                tb.cnt += nb.cnt;
                tb.mtm = ++i;

                /* Unchain deleted bin */
                bins[nb.bk].fw = nb.fw;
                bins[nb.fw].bk = nb.bk;
                nb.mtm = 0xFFFF;
            }

            /* Fill palette */
            int k = 0;
            for (int i = 0; ; ++k)
            {
                var alpha = MiscFunctions.Clamp((int)bins[i].ac, Byte.MinValue, Byte.MaxValue);
                palettes[k] = Color.FromArgb(alpha, MiscFunctions.Clamp((int)bins[i].rc, Byte.MinValue, Byte.MaxValue), MiscFunctions.Clamp((int)bins[i].gc, Byte.MinValue, Byte.MaxValue), MiscFunctions.Clamp((int)bins[i].bc, Byte.MinValue, Byte.MaxValue));
                if (m_transparentPixelIndex >= 0 && palettes[k] == m_transparentColor)
                    Swap(ref palettes[0], ref palettes[k]);

                if ((i = bins[i].fw) == 0)
                    break;
            }
        }
        protected virtual ushort NearestColorIndex(Color[] palette, int nMaxColors, int pixel)
        {
            if (nearestMap.TryGetValue(pixel, out var k))
                return k;

            var c = Color.FromArgb(pixel);

            double mindist = 1e100;
            for (int i = 0; i < nMaxColors; ++i)
            {
                var c2 = palette[i];
                var curdist = Sqr(c2.A - c.A);
                if (curdist > mindist)
                    continue;

                curdist += Sqr(c2.R - c.R);
                if (curdist > mindist)
                    continue;

                curdist += Sqr(c2.G - c.G);
                if (curdist > mindist)
                    continue;

                curdist += Sqr(c2.B - c.B);
                if (curdist > mindist)
                    continue;

                mindist = curdist;
                k = (ushort)i;
            }
            nearestMap[pixel] = k;
            return k;
        }
        protected virtual ushort ClosestColorIndex(Color[] palette, int nMaxColors, int pixel)
        {
            ushort k = 0;
            var c = Color.FromArgb(pixel);

            if (!closestMap.TryGetValue(pixel, out var closest))
            {
                closest = new ushort[5];
                closest[2] = closest[3] = ushort.MaxValue;

                for (; k < nMaxColors; ++k)
                {
                    Color c2 = palette[k];
                    closest[4] = (ushort)(Math.Abs(c.A - c2.A) + Math.Abs(c.R - c2.R) + Math.Abs(c.G - c2.G) + Math.Abs(c.B - c2.B));
                    if (closest[4] < closest[2])
                    {
                        closest[1] = closest[0];
                        closest[3] = closest[2];
                        closest[0] = (ushort)k;
                        closest[2] = closest[4];
                    }
                    else if (closest[4] < closest[3])
                    {
                        closest[1] = (ushort)k;
                        closest[3] = closest[4];
                    }
                }

                if (closest[3] == ushort.MaxValue)
                    closest[2] = 0;
            }

            if (closest[2] == 0 || (rand.Next(short.MaxValue) % (closest[3] + closest[2])) <= closest[3])
                k = closest[0];
            else
                k = closest[1];

            closestMap[pixel] = closest;
            return k;
        }
        protected int[] CalcDitherPixel(Color c, short[] clamp, int[] rowerr, int cursor, bool noBias)
        {
            var ditherPixel = new int[4];
            if (noBias) {
                ditherPixel[0] = clamp[((rowerr[cursor] + 0x1008) >> 4) + c.R];
                ditherPixel[1] = clamp[((rowerr[cursor + 1] + 0x1008) >> 4) + c.G];
                ditherPixel[2] = clamp[((rowerr[cursor + 2] + 0x1008) >> 4) + c.B];
                ditherPixel[3] = clamp[((rowerr[cursor + 3] + 0x1008) >> 4) + c.A];
	        }
	        else {
                ditherPixel[0] = clamp[((rowerr[cursor] + 0x2010) >> 5) + c.R];
                ditherPixel[1] = clamp[((rowerr[cursor + 1] + 0x1008) >> 4) + c.G];
                ditherPixel[2] = clamp[((rowerr[cursor + 2] + 0x2010) >> 5) + c.B];
                ditherPixel[3] = c.A;
	        }
            return ditherPixel;
        }
        protected virtual int[] Quantize_image(int[] pixels, Color[] palette, int nMaxColors, int width, int height, bool dither)
        {
            var qPixels = new int[width * height];
            int pixelIndex = 0;
            if (dither)
            {
                const short DJ = 4;
                const short BLOCK_SIZE = 256;
                const short DITHER_MAX = 20;
                int err_len = (width + 2) * DJ;
                var clamp = new short[DJ * BLOCK_SIZE];
                var limtb = new short[2 * BLOCK_SIZE];

                for (short i = 0; i < BLOCK_SIZE; ++i)
                {
                    clamp[i] = 0;
                    clamp[i + BLOCK_SIZE] = i;
                    clamp[i + BLOCK_SIZE * 2] = Byte.MaxValue;
                    clamp[i + BLOCK_SIZE * 3] = Byte.MaxValue;

                    limtb[i] = -DITHER_MAX;
                    limtb[i + BLOCK_SIZE] = DITHER_MAX;
                }
                for (short i = -DITHER_MAX; i <= DITHER_MAX; ++i)
                    limtb[i + BLOCK_SIZE] = i;

                bool noBias = hasSemiTransparency || nMaxColors < 64;
                int dir = 1;
                var row0 = new int[err_len];
                var row1 = new int[err_len];
                for (int i = 0; i < height; ++i)
                {
                    if (dir < 0)
                        pixelIndex += width - 1;

                    int cursor0 = DJ, cursor1 = width * DJ;
                    row1[cursor1] = row1[cursor1 + 1] = row1[cursor1 + 2] = row1[cursor1 + 3] = 0;
                    for (int j = 0; j < width; ++j)
                    {
                        var c = Color.FromArgb(pixels[pixelIndex]);
                        var ditherPixel = CalcDitherPixel(c, clamp, row0, cursor0, noBias);
                        int r_pix = ditherPixel[0];
                        int g_pix = ditherPixel[1];
                        int b_pix = ditherPixel[2];
                        int a_pix = ditherPixel[3];

                        var c1 = Color.FromArgb(a_pix, r_pix, g_pix, b_pix);
                        qPixels[pixelIndex] = NearestColorIndex(palette, nMaxColors, c1.ToArgb());

                        var c2 = palette[qPixels[pixelIndex]];
                        if (nMaxColors > 256)
                            qPixels[pixelIndex] = hasSemiTransparency ? c2.ToArgb() : GetARGBIndex(c2.ToArgb(), false);

                        r_pix = limtb[r_pix - c2.R + BLOCK_SIZE];
                        g_pix = limtb[g_pix - c2.G + BLOCK_SIZE];
                        b_pix = limtb[b_pix - c2.B + BLOCK_SIZE];
                        a_pix = limtb[a_pix - c2.A + BLOCK_SIZE];

                        int k = r_pix * 2;
                        row1[cursor1 - DJ] = r_pix;
                        row1[cursor1 + DJ] += (r_pix += k);
                        row1[cursor1] += (r_pix += k);
                        row0[cursor0 + DJ] += (r_pix += k);

                        k = g_pix * 2;
                        row1[cursor1 + 1 - DJ] = g_pix;
                        row1[cursor1 + 1 + DJ] += (g_pix += k);
                        row1[cursor1 + 1] += (g_pix += k);
                        row0[cursor0 + 1 + DJ] += (g_pix += k);

                        k = b_pix * 2;
                        row1[cursor1 + 2 - DJ] = b_pix;
                        row1[cursor1 + 2 + DJ] += (b_pix += k);
                        row1[cursor1 + 2] += (b_pix += k);
                        row0[cursor0 + 2 + DJ] += (b_pix += k);

                        k = a_pix * 2;
                        row1[cursor1 + 3 - DJ] = a_pix;
                        row1[cursor1 + 3 + DJ] += (a_pix += k);
                        row1[cursor1 + 3] += (a_pix += k);
                        row0[cursor0 + 3 + DJ] += (a_pix += k);

                        cursor0 += DJ;
                        cursor1 -= DJ;
                        pixelIndex += dir;
                    }
                    if ((i % 2) == 1)
                        pixelIndex += width + 1;

                    dir *= -1;
                    Swap(ref row0, ref row1);
                }
                return qPixels;
            }

            if (m_transparentPixelIndex >= 0 || nMaxColors < 64)
            {
                for (int i = 0; i < qPixels.Length; ++i)
                    qPixels[i] = NearestColorIndex(palette, nMaxColors, pixels[i]);
            }
            else
            {
                for (int i = 0; i < qPixels.Length; ++i)
                    qPixels[i] = ClosestColorIndex(palette, nMaxColors, pixels[i]);
            }

            return qPixels;
        }
        protected Bitmap ProcessImagePixels(Bitmap dest, Color[] palettes, int[] qPixels)
        {
            var palette = dest.Palette;
            for (int i = 0; i < palettes.Length; ++i)
                palette.Entries[i] = palettes[i];
            dest.Palette = palette;

            int bpp = Image.GetPixelFormatSize(dest.PixelFormat);
            int w = dest.Width;
            int h = dest.Height;

            var targetData = dest.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, dest.PixelFormat);

            int pixelIndex = 0;
            int strideDest;

            //unsafe
            var array = new byte[Math.Abs(targetData.Stride) * h * (bpp / 8)];
            int pRowDest = 0;
            Marshal.Copy(targetData.Scan0, array, 0, array.Length);


            {
                //var pRowDest = (byte*)targetData.Scan0;

                // Compensate for possible negative stride
                if (targetData.Stride > 0)
                    strideDest = targetData.Stride;
                else
                {
                    pRowDest += h * targetData.Stride;
                    strideDest = -targetData.Stride;
                }

                // Second loop: fill indexed bitmap
                for (int y = 0; y < h; ++y)
                {	// For each row...
                    for (int x = 0; x < w; ++x)
                    {	// ...for each pixel...
                        byte nibbles = 0;
                        var index = (byte)qPixels[pixelIndex++];

                        switch (bpp)
                        {
                            case 8:
                                array[pRowDest + x] = index;
                                //pRowDest[x] = index;
                                break;
                            case 4:
                                // First pixel is the high nibble. From and To indices are 0..16
                                //nibbles = pRowDest[x / 2];
                                nibbles = array[pRowDest + (x / 2)];
                                if ((x & 1) == 0)
                                {
                                    nibbles &= 0x0F;
                                    nibbles |= (byte)(index << 4);
                                }
                                else
                                {
                                    nibbles &= 0xF0;
                                    nibbles |= index;
                                }

                                //pRowDest[x / 2] = nibbles;
                                array[pRowDest + (x / 2)] = nibbles;
                                break;
                            case 1:
                                // First pixel is MSB. From and To are 0 or 1.
                                int pos = x / 8;
                                byte mask = (byte)(128 >> (x & 7));
                                if (index == 0)
                                {
                                    //pRowDest[pos] &= (byte)~mask;
                                    array[pRowDest + pos] &= (byte)~mask;
                                }
                                else
                                {
                                    //pRowDest[pos] |= mask;
                                    array[pRowDest + pos] |= mask;
                                }
                                break;
                        }
                    }

                    pRowDest += strideDest;
                }
            }

            Marshal.Copy(array, 0, targetData.Scan0, array.Length);

            dest.UnlockBits(targetData);
            return dest;
        }
        protected Bitmap ProcessImagePixels(Bitmap dest, int[] qPixels, bool hasSemiTransparency, int transparentPixelIndex)
        {
            int bpp = Image.GetPixelFormatSize(dest.PixelFormat);
            if (bpp < 16)
                return dest;            

            int w = dest.Width;
            int h = dest.Height;

            if (hasSemiTransparency && dest.PixelFormat < PixelFormat.Format32bppArgb)
                dest = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            else if (transparentPixelIndex >= 0 && dest.PixelFormat < PixelFormat.Format16bppArgb1555)
                dest = new Bitmap(w, h, PixelFormat.Format16bppArgb1555);
            else if (dest.PixelFormat != PixelFormat.Format16bppRgb565)
                dest = new Bitmap(w, h, PixelFormat.Format16bppRgb565);

            bpp = Image.GetPixelFormatSize(dest.PixelFormat);
            var targetData = dest.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, dest.PixelFormat);            

            int pixelIndex = 0;
            int strideDest;

            //unsafe
            var array = new byte[Math.Abs(targetData.Stride) * h * (bpp / 8)];
            int pRowDest = 0;
            Marshal.Copy(targetData.Scan0, array, 0, array.Length);

            {
                //var pRowDest = (byte*)targetData.Scan0;

                // Compensate for possible negative stride
                if (targetData.Stride > 0)
                    strideDest = targetData.Stride;
                else
                {
                    pRowDest += h * targetData.Stride;
                    strideDest = -targetData.Stride;
                }

                if (bpp == 32)
                {
                    for (int y = 0; y < h; ++y)
                    {   // For each row...
                        for (int x = 0; x < w * 4;)
                        {
                            var c = Color.FromArgb(qPixels[pixelIndex++]);
                            /*
                            pRowDest[x++] = c.B;
                            pRowDest[x++] = c.G;
                            pRowDest[x++] = c.R;
                            pRowDest[x++] = c.A;
                            */
                            array[pRowDest + x++] = c.B;
                            array[pRowDest + x++] = c.G;
                            array[pRowDest + x++] = c.R;
                            array[pRowDest + x++] = c.A;
                        }
                        pRowDest += strideDest;
                    }
                }
                else if (bpp == 16)
                {
                    for (int y = 0; y < h; ++y)
                    {   // For each row...
                        for (int x = 0; x < w * 2;)
                        {
                            var argb = (short)qPixels[pixelIndex++];
                            /*
                            pRowDest[x++] = (byte)(argb & 0xFF);
                            pRowDest[x++] = (byte)(argb >> 8);
                            */
                            array[pRowDest + x++] = (byte)(argb & 0xFF);
                            array[pRowDest + x++] = (byte)(argb >> 8);
                        }
                        pRowDest += strideDest;
                    }
                }
                else
                {
                    for (int y = 0; y < h; ++y)
                    {   // For each row...
                        for (int x = 0; x < w; ++x)
                        {
                            //pRowDest[x] = (byte)qPixels[pixelIndex++];
                            array[pRowDest + x] = (byte)qPixels[pixelIndex++];
                        }
                        pRowDest += strideDest;
                    }
                }
            }

            Marshal.Copy(array, 0, targetData.Scan0, array.Length);

            dest.UnlockBits(targetData);
            return dest;
        }
        protected static void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }
        protected bool IsValidFormat(PixelFormat pixelFormat, int nMaxColors)
        {
            int bitDepth = Image.GetPixelFormatSize(pixelFormat);
            return Math.Pow(2, bitDepth) >= nMaxColors;
        }
        protected bool GrabPixels(Bitmap source, int[] pixels)
        {
            int bitmapWidth = source.Width;
            int bitmapHeight = source.Height;

            hasSemiTransparency = false;
            m_transparentPixelIndex = -1;

            int pixelIndex = 0, strideSource;
            BitmapData data;
            if (source.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                var palette = source.Palette;
                var palettes = palette.Entries;

                data = source.LockBits(new Rectangle(0, 0, bitmapWidth, bitmapHeight), ImageLockMode.ReadOnly, source.PixelFormat);

                //unsafe
                var array = new byte[Math.Abs(data.Stride) * bitmapHeight];
                int pRowSource = 0;
                Marshal.Copy(data.Scan0, array, 0, array.Length);

                {
                    //var pRowSource = (byte*)data.Scan0;

                    // Compensate for possible negative stride
                    if (data.Stride > 0)
                        strideSource = data.Stride;
                    else
                    {
                        pRowSource += bitmapHeight * data.Stride;
                        strideSource = -data.Stride;
                    }

                    // First loop: gather color information
                    //Parallel.For(0, bitmapHeight, y =>
                    for (int y = 0; y < bitmapHeight; y++)
                    {
                        var pPixelSource = pRowSource + (y * strideSource);
                        // For each row...
                        for (int x = 0; x < bitmapWidth; ++x)
                        {   // ...for each pixel...
                            byte pixelAlpha = Byte.MaxValue;

                            //byte index = *pPixelSource++;
                            byte index = array[pPixelSource++];
                            var argb = palettes[index];
                            if (index == m_transparentPixelIndex)
                                pixelAlpha = 0;

                            if (pixelAlpha < Byte.MaxValue)
                            {
                                hasSemiTransparency = true;
                                if (pixelAlpha == 0)
                                {
                                    m_transparentColor = argb;
                                    m_transparentPixelIndex = pixelIndex;
                                }
                            }
                            pixels[pixelIndex++] = argb.ToArgb();
                        }
                    }
                    source.UnlockBits(data);
                }                

                var pPropertyItem = source.GetPropertyItem(PropertyTagIndexTransparent);
                if (pPropertyItem != null)
                {
                    m_transparentPixelIndex = pPropertyItem.Value[0];
                    Color c = palettes[m_transparentPixelIndex];
                    m_transparentColor = Color.FromArgb(0, c.R, c.G, c.B);
                }
                return true;
            }
                        
            data = source.LockBits(new Rectangle(0, 0, bitmapWidth, bitmapHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pRowSrc = (IntPtr)data.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            int bytesLength = Math.Abs(data.Stride) * bitmapHeight;
            var rgbValues = new byte[bytesLength];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(pRowSrc, rgbValues, 0, bytesLength);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {                    
                byte pixelBlue = rgbValues[i];
                byte pixelGreen = rgbValues[i + 1];
                byte pixelRed = rgbValues[i + 2];
                byte pixelAlpha = rgbValues[i + 3];

                var argb = Color.FromArgb(pixelAlpha, pixelRed, pixelGreen, pixelBlue);
                if (pixelAlpha < Byte.MaxValue)
                {
                    hasSemiTransparency = true;
                    if (pixelAlpha == 0)
                    {
                        m_transparentPixelIndex = pixelIndex;
                        m_transparentColor = argb;
                    }
                }
                pixels[pixelIndex++] = argb.ToArgb();
            }

            source.UnlockBits(data);
            return true;
        }
        public virtual Bitmap QuantizeImage(Bitmap source, PixelFormat pixelFormat, int nMaxColors, bool dither)
        {
            int bitmapWidth = source.Width;
            int bitmapHeight = source.Height;

            var dest = new Bitmap(bitmapWidth, bitmapHeight, pixelFormat);
            if (!IsValidFormat(pixelFormat, nMaxColors))
                return dest;
            
            var pixels = new int[bitmapWidth * bitmapHeight];
            if(!GrabPixels(source, pixels))
                return dest;

            var palettes = dest.Palette.Entries;
            if (palettes.Length != nMaxColors)
                palettes = new Color[nMaxColors];
            if (nMaxColors > 256)
                dither = true;

            if (nMaxColors > 2)
                Pnnquan(pixels, palettes, nMaxColors, 1);
            else
            {
                if (m_transparentPixelIndex >= 0)
                {
                    palettes[0] = Color.Transparent;
                    palettes[1] = Color.Black;
                }
                else
                {
                    palettes[0] = Color.Black;
                    palettes[1] = Color.White;
                }
            }

            var qPixels = Quantize_image(pixels, palettes, nMaxColors, bitmapWidth, bitmapHeight, dither);
            if (m_transparentPixelIndex >= 0)
            {
                var k = qPixels[m_transparentPixelIndex];
                if (nMaxColors > 2)
                    palettes[k] = m_transparentColor;
                else if (palettes[k] != m_transparentColor)
                    Swap(ref palettes[0], ref palettes[1]);
            }
            closestMap.Clear();
            nearestMap.Clear();

            if (nMaxColors > 256)
                return ProcessImagePixels(dest, qPixels, hasSemiTransparency, m_transparentPixelIndex);
            
            return ProcessImagePixels(dest, palettes, qPixels);
        }
    }

}
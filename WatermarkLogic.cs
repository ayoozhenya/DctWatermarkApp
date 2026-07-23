using System;
using System.Drawing;
using System.Text;

namespace DctWatermarkApp
{
    public static class WatermarkLogic
    {
        private static string TextToBits(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                sb.Append(Convert.ToString((int)c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private static string BitsToText(string b)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i + 8 <= b.Length; i += 8)
            {
                string byteStr = b.Substring(i, 8);
                int val = Convert.ToInt32(byteStr, 2);
                if (val == 0) break;
                sb.Append((char)val);
            }
            return sb.ToString();
        }

        public static Bitmap EmbedDct(Bitmap src, string watermarkText, int alpha)
        {
            if (watermarkText == null) watermarkText = "";
            // convert to grayscale luminance array
            int width = src.Width;
            int height = src.Height;
            int padW = (8 - (width % 8)) % 8;
            int padH = (8 - (height % 8)) % 8;
            int newW = width + padW;
            int newH = height + padH;
            double[,] lum = new double[newH, newW];
            for (int y = 0; y < newH; y++)
            {
                for (int x = 0; x < newW; x++)
                {
                    int sx = Math.Min(x, width - 1);
                    int sy = Math.Min(y, height - 1);
                    Color c = src.GetPixel(sx, sy);
                    lum[y, x] = 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
                }
            }

            string bits = TextToBits(watermarkText);
            int capacity = (newW / 8) * (newH / 8) * DctHelper.MFPositions.Length;
            if (bits.Length > capacity)
            {
                // trim
                bits = bits.Substring(0, capacity);
            }

            int bitIdx = 0;
            for (int by = 0; by < newH; by += 8)
            {
                for (int bx = 0; bx < newW; bx += 8)
                {
                    double[,] block = new double[8,8];
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            block[i,j] = lum[by + i, bx + j] - 128.0;

                    double[,] d = DctHelper.DCT2(block);

                    foreach (var pos in DctHelper.MFPositions)
                    {
                        if (bitIdx >= bits.Length) break;
                        int i = pos.Item1;
                        int j = pos.Item2;
                        int bit = bits[bitIdx] == '1' ? 1 : 0;
                        double coeff = d[i,j];
                        if (bit == 1)
                        {
                            if (coeff >= 0) coeff = coeff + alpha * Math.Abs(coeff + 1e-6);
                            else coeff = -coeff + alpha;
                        }
                        else
                        {
                            if (coeff >= 0) coeff = coeff - alpha * Math.Abs(coeff + 1e-6);
                            else coeff = -coeff - alpha;
                        }
                        d[i,j] = coeff;
                        bitIdx++;
                    }

                    double[,] id = DctHelper.IDCT2(d);
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            lum[by + i, bx + j] = Clamp(id[i,j] + 128.0, 0.0, 255.0);
                }
            }

            Bitmap outBmp = new Bitmap(newW, newH, src.PixelFormat);
            for (int y = 0; y < newH; y++)
            {
                for (int x = 0; x < newW; x++)
                {
                    int v = (int)Math.Round(lum[y, x]);
                    outBmp.SetPixel(x, y, Color.FromArgb(v, v, v));
                }
            }
            if (padW != 0 || padH != 0)
            {
                Bitmap cropped = outBmp.Clone(new Rectangle(0,0,width,height), outBmp.PixelFormat);
                outBmp.Dispose();
                return cropped;
            }
            return outBmp;
        }

        public static string ExtractDct(Bitmap src)
        {
            int width = src.Width;
            int height = src.Height;
            int padW = (8 - (width % 8)) % 8;
            int padH = (8 - (height % 8)) % 8;
            int newW = width + padW;
            int newH = height + padH;
            double[,] lum = new double[newH, newW];
            for (int y = 0; y < newH; y++)
            {
                for (int x = 0; x < newW; x++)
                {
                    int sx = Math.Min(x, width - 1);
                    int sy = Math.Min(y, height - 1);
                    Color c = src.GetPixel(sx, sy);
                    lum[y, x] = 0.299 * c.R + 0.587 * c.G + 0.114 * c.B;
                }
            }

            StringBuilder bits = new StringBuilder();
            for (int by = 0; by < newH; by += 8)
            {
                for (int bx = 0; bx < newW; bx += 8)
                {
                    double[,] block = new double[8,8];
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            block[i,j] = lum[by + i, bx + j] - 128.0;

                    double[,] d = DctHelper.DCT2(block);

                    foreach (var pos in DctHelper.MFPositions)
                    {
                        int i = pos.Item1;
                        int j = pos.Item2;
                        double coeff = d[i,j];
                        bits.Append(coeff > 0 ? '1' : '0');
                    }
                }
            }

            string bitstr = bits.ToString();
            int len8 = (bitstr.Length / 8) * 8;
            string truncated = bitstr.Substring(0, len8);
            return BitsToText(truncated);
        }

        private static double Clamp(double v, double a, double b)
        {
            if (v < a) return a;
            if (v > b) return b;
            return v;
        }
    }
}

using System;
using System.Drawing;

namespace DctWatermarkApp
{
    public static class ImageMetrics
    {
        public static double MSE(Bitmap a, Bitmap b)
        {
            int w = Math.Min(a.Width, b.Width);
            int h = Math.Min(a.Height, b.Height);
            double sum = 0.0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color ca = a.GetPixel(x, y);
                    Color cb = b.GetPixel(x, y);
                    double da = 0.299 * ca.R + 0.587 * ca.G + 0.114 * ca.B;
                    double db = 0.299 * cb.R + 0.587 * cb.G + 0.114 * cb.B;
                    double diff = da - db;
                    sum += diff * diff;
                }
            }
            return sum / (w * h);
        }

        public static double PSNR(double mse)
        {
            if (mse <= 0) return 99.0;
            double maxI = 255.0;
            return 10.0 * Math.Log10((maxI * maxI) / mse);
        }

        public static double SSIM(Bitmap a, Bitmap b)
        {
            int w = Math.Min(a.Width, b.Width);
            int h = Math.Min(a.Height, b.Height);
            int win = 8;
            double ssimSum = 0.0;
            int count = 0;
            for (int by = 0; by < h; by += win)
            {
                for (int bx = 0; bx < w; bx += win)
                {
                    double[] va = new double[win * win];
                    double[] vb = new double[win * win];
                    int idx = 0;
                    for (int i = 0; i < win; i++)
                        for (int j = 0; j < win; j++)
                        {
                            int y = Math.Min(by + i, h - 1);
                            int x = Math.Min(bx + j, w - 1);
                            Color ca = a.GetPixel(x, y);
                            Color cb = b.GetPixel(x, y);
                            va[idx] = 0.299 * ca.R + 0.587 * ca.G + 0.114 * ca.B;
                            vb[idx] = 0.299 * cb.R + 0.587 * cb.G + 0.114 * cb.B;
                            idx++;
                        }
                    double meanA = Mean(va), meanB = Mean(vb);
                    double varA = Variance(va, meanA), varB = Variance(vb, meanB);
                    double cov = Covariance(va, vb, meanA, meanB);
                    double C1 = Math.Pow(0.01 * 255, 2);
                    double C2 = Math.Pow(0.03 * 255, 2);
                    double ssim = ((2 * meanA * meanB + C1) * (2 * cov + C2)) / ((meanA * meanA + meanB * meanB + C1) * (varA + varB + C2));
                    ssimSum += ssim;
                    count++;
                }
            }
            if (count == 0) return 1.0;
            return ssimSum / count;
        }

        private static double Mean(double[] v)
        {
            double s = 0;
            for (int i = 0; i < v.Length; i++) s += v[i];
            return s / v.Length;
        }
        private static double Variance(double[] v, double mean)
        {
            double s = 0;
            for (int i = 0; i < v.Length; i++) s += (v[i] - mean) * (v[i] - mean);
            return s / v.Length;
        }
        private static double Covariance(double[] a, double[] b, double ma, double mb)
        {
            double s = 0;
            for (int i = 0; i < a.Length; i++) s += (a[i] - ma) * (b[i] - mb);
            return s / a.Length;
        }

        public static double PSNR(Bitmap a, Bitmap b)
        {
            double mse = MSE(a, b);
            return PSNR(mse);
        }
    }
}

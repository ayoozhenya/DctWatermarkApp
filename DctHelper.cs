using System;

namespace DctWatermarkApp
{
    public static class DctHelper
    {
        public static readonly Tuple<int,int>[] MFPositions = new Tuple<int,int>[]
        {
            Tuple.Create(2,1), Tuple.Create(1,2), Tuple.Create(2,2),
            Tuple.Create(3,1), Tuple.Create(1,3), Tuple.Create(3,2)
        };

        public static double[] DCT1D(double[] x)
        {
            int N = x.Length;
            double[] X = new double[N];
            for (int k = 0; k < N; k++)
            {
                double sum = 0.0;
                for (int n = 0; n < N; n++)
                {
                    sum += x[n] * Math.Cos(Math.PI * k * (2 * n + 1) / (2.0 * N));
                }
                double a = (k == 0) ? Math.Sqrt(1.0 / N) : Math.Sqrt(2.0 / N);
                X[k] = a * sum;
            }
            return X;
        }

        public static double[] IDCT1D(double[] X)
        {
            int N = X.Length;
            double[] x = new double[N];
            for (int n = 0; n < N; n++)
            {
                double sum = 0.0;
                for (int k = 0; k < N; k++)
                {
                    double a = (k == 0) ? Math.Sqrt(1.0 / N) : Math.Sqrt(2.0 / N);
                    sum += a * X[k] * Math.Cos(Math.PI * k * (2 * n + 1) / (2.0 * N));
                }
                x[n] = sum;
            }
            return x;
        }

        public static double[,] DCT2(double[,] block)
        {
            int N = 8;
            double[,] tmp = new double[N,N];
            double[,] result = new double[N,N];
            for (int i = 0; i < N; i++)
            {
                double[] row = new double[N];
                for (int j = 0; j < N; j++) row[j] = block[i,j];
                double[] r = DCT1D(row);
                for (int j = 0; j < N; j++) tmp[i,j] = r[j];
            }
            for (int j = 0; j < N; j++)
            {
                double[] col = new double[N];
                for (int i = 0; i < N; i++) col[i] = tmp[i,j];
                double[] c = DCT1D(col);
                for (int i = 0; i < N; i++) result[i,j] = c[i];
            }
            return result;
        }

        public static double[,] IDCT2(double[,] block)
        {
            int N = 8;
            double[,] tmp = new double[N,N];
            double[,] result = new double[N,N];
            for (int i = 0; i < N; i++)
            {
                double[] row = new double[N];
                for (int j = 0; j < N; j++) row[j] = block[i,j];
                double[] r = IDCT1D(row);
                for (int j = 0; j < N; j++) tmp[i,j] = r[j];
            }
            for (int j = 0; j < N; j++)
            {
                double[] col = new double[N];
                for (int i = 0; i < N; i++) col[i] = tmp[i,j];
                double[] c = IDCT1D(col);
                for (int i = 0; i < N; i++) result[i,j] = c[i];
            }
            return result;
        }
    }
}

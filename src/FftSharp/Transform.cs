using System;
using System.Numerics;

namespace FftSharp
{
    public static class Transform
    {
        public static Complex[] FFT(double[] real)
        {
            return FFT(Convert.ToComplex(real));
        }

        public static Complex[] FFT(Complex[] x)
        {
            int N = x.Length;
            Complex[] X = new Complex[N];
            Complex[] d, D, e, E;

            if (N == 1)
            {
                X[0] = x[0];
                return X;
            }

            int k;
            e = new Complex[N / 2];
            d = new Complex[N / 2];

            for (k = 0; k < N / 2; k++)
            {
                e[k] = x[2 * k];
                d[k] = x[2 * k + 1];
            }

            D = FFT(d);
            E = FFT(e);

            for (k = 0; k < N / 2; k++)
            {
                double radians = -2 * Math.PI * k / N;
                D[k] *= new Complex(Math.Cos(radians), Math.Sin(radians));
            }

            for (k = 0; k < N / 2; k++)
            {
                X[k] = E[k] + D[k];
                X[k + N / 2] = E[k] - D[k];
            }

            return X;
        }
    }
}

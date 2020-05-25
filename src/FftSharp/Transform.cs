using System;
using System.Numerics;

namespace FftSharp
{
    public static class Transform
    {
        /// <summary>
        /// fast Fourier transform
        /// </summary>
        public static Complex[] FFT(double[] input)
        {
            return FFT(Convert.ToComplex(input));
        }

        /// <summary>
        /// fast Fourier transform
        /// </summary>
        public static Complex[] FFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];
            Complex[] d, D, e, E;

            if (N == 1)
            {
                output[0] = input[0];
                return output;
            }

            int k;
            e = new Complex[N / 2];
            d = new Complex[N / 2];

            for (k = 0; k < N / 2; k++)
            {
                e[k] = input[2 * k];
                d[k] = input[2 * k + 1];
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
                output[k] = E[k] + D[k];
                output[k + N / 2] = E[k] - D[k];
            }

            return output;
        }

        /// <summary>
        /// discrete Fourier transform
        /// </summary>
        public static Complex[] DFT(double[] input)
        {
            return DFT(Convert.ToComplex(input));
        }

        /// <summary>
        /// discrete Fourier transform
        /// </summary>
        public static Complex[] DFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];

            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double radians = -2 * Math.PI * n * k / N;
                    Complex temp = new Complex(Math.Cos(radians), Math.Sin(radians));
                    temp *= input[n];
                    output[k] += temp;
                }
            }

            return output;
        }
    }
}

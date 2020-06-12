using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp
{
    [Obsolete("This module is for educational purposes only")]
    public static class Experimental
    {
        // An easy-to-read (but non-optimized) implementation of the FFT algorithm
        private static Complex[] FFTsimple(Complex[] input)
        {
            Complex[] output = new Complex[input.Length];

            int H = input.Length / 2;
            Complex[] evens = new Complex[H];
            Complex[] odds = new Complex[H];
            for (int i = 0; i < H; i++)
            {
                evens[i] = input[2 * i];
                odds[i] = input[2 * i + 1];
            }
            odds = FFTsimple(odds);
            evens = FFTsimple(evens);

            double mult1 = -2 * Math.PI / input.Length;
            for (int i = 0; i < H; i++)
            {
                double radians = mult1 * i;
                odds[i] *= new Complex(Math.Cos(radians), Math.Sin(radians));
            }

            for (int i = 0; i < H; i++)
            {
                output[i] = evens[i] + odds[i];
                output[i + H] = evens[i] - odds[i];
            }

            return output;
        }

        // Compute the forward or inverse discrete Fourier Transform (not using the fast FFT algorithm)
        public static Complex[] DFT(Complex[] input, bool inverse = false)
        {
            int N = input.Length;
            double mult1 = (inverse) ? 2 * Math.PI / N : -2 * Math.PI / N;
            double mult2 = (inverse) ? 1.0 / N : 1.0;
            Console.WriteLine($"REAL {mult1} {mult2}");

            Complex[] output = new Complex[N];
            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double radians = n * k * mult1;
                    Complex temp = new Complex(Math.Cos(radians), Math.Sin(radians));
                    temp *= input[n];
                    output[k] += temp * mult2;
                }
            }

            return output;
        }

        public static Complex[] DFT(double[] input, bool inverse = false)
        {
            return DFT(Transform.MakeComplex(input), inverse);
        }
    }
}

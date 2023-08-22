using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp
{
    [Obsolete("This module is for educational purposes only")]
    public static class Experimental
    {
        // An easy-to-read (but non-optimized) implementation of the FFT algorithm
        private static System.Numerics.Complex[] FFTsimple(System.Numerics.Complex[] input)
        {
            System.Numerics.Complex[] output = new System.Numerics.Complex[input.Length];

            int H = input.Length / 2;
            System.Numerics.Complex[] evens = new System.Numerics.Complex[H];
            System.Numerics.Complex[] odds = new System.Numerics.Complex[H];
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
                odds[i] *= new System.Numerics.Complex(Math.Cos(radians), Math.Sin(radians));
            }

            for (int i = 0; i < H; i++)
            {
                output[i] = evens[i] + odds[i];
                output[i + H] = evens[i] - odds[i];
            }

            return output;
        }

        // Compute the forward or inverse discrete Fourier Transform (not using the fast FFT algorithm)
        public static System.Numerics.Complex[] DFT(System.Numerics.Complex[] input, bool inverse = false)
        {
            int N = input.Length;
            double mult1 = (inverse) ? 2 * Math.PI / N : -2 * Math.PI / N;
            double mult2 = (inverse) ? 1.0 / N : 1.0;

            System.Numerics.Complex[] output = new System.Numerics.Complex[N];
            for (int k = 0; k < N; k++)
            {
                output[k] = new System.Numerics.Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double radians = n * k * mult1;
                    System.Numerics.Complex temp = new System.Numerics.Complex(Math.Cos(radians), Math.Sin(radians));
                    temp *= input[n];
                    output[k] += temp * mult2;
                }
            }

            return output;
        }

        public static System.Numerics.Complex[] DFT(double[] input, bool inverse = false)
        {
            System.Numerics.Complex[] inputComplex = new System.Numerics.Complex[input.Length];
            for (int i = 0; i < input.Length; i++)
                inputComplex[i] = new(input[i], 0);

            return DFT(inputComplex, inverse);
        }

        /// <summary>
        /// Computes the discrete Fourier transform (DFT) of the given array.
        /// The array may have any length.
        /// Uses Bluestein's chirp z-transform algorithm.
        /// </summary>
        public static System.Numerics.Complex[] Bluestein(double[] real, bool inverse = false)
        {
            System.Numerics.Complex[] buffer = new System.Numerics.Complex[real.Length];

            for (int i = 0; i < real.Length; i++)
            {
                buffer[i] = new System.Numerics.Complex(real[i], 0);
            }

            Bluestein(buffer, inverse);

            return buffer;
        }

        /// <summary>
        /// Computes the discrete Fourier transform (DFT) of the given complex vector in place.
        /// The vector can have any length.
        /// Uses Bluestein's chirp z-transform algorithm.
        /// </summary>
        public static void Bluestein(System.Numerics.Complex[] buffer, bool inverse = false)
        {
            BluesteinOperations.TransformBluestein(buffer, inverse);
        }
    }
}

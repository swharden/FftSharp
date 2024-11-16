using System;

namespace FftSharp;

/// <summary>
/// Methods for calculating the discrete Fourier Transform (slower than the FFT algorithm)
/// </summary>
public static class DFT
{
    /// <summary>
    /// Compute the forward discrete Fourier Transform
    /// </summary>
    public static System.Numerics.Complex[] Forward(System.Numerics.Complex[] input)
    {
        return GetDFT(input, false);
    }

    /// <summary>
    /// Compute the forward discrete Fourier Transform
    /// </summary>
    public static System.Numerics.Complex[] Forward(double[] real)
    {
        return Forward(real.ToComplexArray());
    }

    /// <summary>
    /// Compute the inverse discrete Fourier Transform
    /// </summary>
    public static System.Numerics.Complex[] Inverse(System.Numerics.Complex[] input)
    {
        return GetDFT(input, true);
    }

    /// <summary>
    /// Compute the inverse discrete Fourier Transform
    /// </summary>
    public static System.Numerics.Complex[] Inverse(double[] real)
    {
        return Inverse(real.ToComplexArray());
    }

    private static System.Numerics.Complex[] GetDFT(System.Numerics.Complex[] input, bool inverse)
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
}

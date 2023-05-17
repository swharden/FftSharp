using System;

namespace FftSharp;

// TODO: support spans

/// <summary>
/// Fast Fourier Transform (FFT) operations using System.Numerics.Complex data types.
/// </summary>
public static class FFT
{
    /// <summary>
    /// Apply the Fast Fourier Transform (FFT) to the Complex array in place.
    /// Length of the array must be a power of 2.
    /// </summary>
    public static void Forward(System.Numerics.Complex[] samples)
    {
        if (!Transform.IsPowerOfTwo(samples.Length))
            throw new ArgumentException($"{nameof(samples)} length must be a power of 2");

        FftOperations.FFT_WithoutChecks(samples);
    }

    /// <summary>
    /// Create a Complex array from the given data, apply the FFT, and return the result.
    /// Length of the array must be a power of 2.
    /// </summary>
    public static System.Numerics.Complex[] Forward(double[] real)
    {
        if (!Transform.IsPowerOfTwo(real.Length))
            throw new ArgumentException($"{nameof(real)} length must be a power of 2");

        System.Numerics.Complex[] samples = real.ToComplexArray();
        FftOperations.FFT_WithoutChecks(samples);
        return samples;
    }

    /// <summary>
    /// Apply the inverse Fast Fourier Transform (iFFT) to the Complex array in place.
    /// Length of the array must be a power of 2.
    /// </summary>
    public static void Inverse(System.Numerics.Complex[] samples)
    {
        if (!Transform.IsPowerOfTwo(samples.Length))
            throw new ArgumentException($"{nameof(samples)} length must be a power of 2");

        FftOperations.IFFT_WithoutChecks(samples);
    }

    /// <summary>
    /// Return frequencies for each index of a FFT.
    /// </summary>
    public static double[] FrequencyScale(int length, double sampleRate, bool positiveOnly = true)
    {
        return Transform.FFTfreq(sampleRate: sampleRate, pointCount: length, oneSided: positiveOnly);
    }
}
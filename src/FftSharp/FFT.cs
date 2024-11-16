using System;

namespace FftSharp;

/// <summary>
/// Fast Fourier Transform (FFT) operations using System.Numerics.Complex data types.
/// </summary>
public static class FFT
{
    /// <summary>
    /// An easy-to-read (but inefficient) implementation of the FFT algorithm
    /// </summary>
    [Obsolete("This method is inefficient and is for educational purposes only.")]
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

    /// <summary>
    /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="samples">Data to transform in-place. Length must be a power of 2.</param>
    public static void Forward(System.Numerics.Complex[] samples)
    {
        if (samples is null)
            throw new ArgumentNullException(nameof(samples));

        Forward(samples.AsSpan());
    }

    /// <summary>
    /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="samples">Data to transform in-place. Length must be a power of 2.</param>
    public static System.Numerics.Complex[] Forward(double[] samples)
    {
        if (samples is null)
            throw new ArgumentNullException(nameof(samples));

        System.Numerics.Complex[] buffer = samples.ToComplexArray();
        Forward(buffer);
        return buffer;
    }

    /// <summary>
    /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="samples">Data to transform in-place. Length must be a power of 2.</param>
    public static void Forward(Span<System.Numerics.Complex> samples)
    {
        if (!FftOperations.IsPowerOfTwo(samples.Length))
            throw new ArgumentException($"{nameof(samples)} length must be a power of 2");

        if (samples.Length < 2)
            throw new ArgumentException($"FFT requires at least 2 samples but sample length was {samples.Length}");

        FftOperations.FFT_WithoutChecks(samples);
    }

    /// <summary>
    /// Calculate FFT and return just the real component of the spectrum
    /// </summary>
    public static System.Numerics.Complex[] ForwardReal(System.Numerics.Complex[] samples)
    {
        if (!FftOperations.IsPowerOfTwo(samples.Length))
            throw new ArgumentException($"{nameof(samples)} length must be a power of 2");

        System.Numerics.Complex[] realBuffer = new System.Numerics.Complex[samples.Length / 2 + 1];
        FftOperations.RFFT_WithoutChecks(realBuffer, samples);
        return realBuffer;
    }

    /// <summary>
    /// Calculate FFT and return just the real component of the spectrum
    /// </summary>
    public static System.Numerics.Complex[] ForwardReal(double[] samples)
    {
        System.Numerics.Complex[] buffer = samples.ToComplexArray();
        Forward(buffer);
        System.Numerics.Complex[] real = new System.Numerics.Complex[samples.Length / 2 + 1];
        Array.Copy(buffer, 0, real, 0, real.Length);
        return real;
    }

    /// <summary>
    /// Calculate IFFT and return just the real component of the spectrum
    /// </summary>
    public static double[] InverseReal(System.Numerics.Complex[] spectrum)
    {
        int spectrumSize = spectrum.Length;
        int windowSize = (spectrumSize - 1) * 2;

        if (!FftOperations.IsPowerOfTwo(windowSize))
            throw new ArgumentException($"{nameof(spectrum)} length must be a power of two plus 1");

        System.Numerics.Complex[] buffer = new System.Numerics.Complex[windowSize];

        for (int i = 0; i < spectrumSize; i++)
            buffer[i] = spectrum[i];

        for (int i = spectrumSize; i < windowSize; i++)
        {
            int iMirrored = spectrumSize - (i - (spectrumSize - 2));
            buffer[i] = System.Numerics.Complex.Conjugate(spectrum[iMirrored]);
        }

        Inverse(buffer);

        double[] result = new double[windowSize];
        for (int i = 0; i < windowSize; i++)
            result[i] = buffer[i].Real;

        return result;
    }

    /// <summary>
    /// Apply the inverse Fast Fourier Transform (iFFT) to the Complex array in place.
    /// Length of the array must be a power of 2.
    /// </summary>
    public static void Inverse(System.Numerics.Complex[] spectrum)
    {
        if (spectrum is null)
            throw new ArgumentNullException(nameof(spectrum));

        Inverse(spectrum.AsSpan());
    }

    /// <summary>
    /// Apply the inverse Fast Fourier Transform (iFFT) to the Complex array in place.
    /// Length of the array must be a power of 2.
    /// </summary>
    public static void Inverse(Span<System.Numerics.Complex> spectrum)
    {
        if (!FftOperations.IsPowerOfTwo(spectrum.Length))
            throw new ArgumentException($"{nameof(spectrum)} length must be a power of 2");

        FftOperations.IFFT_WithoutChecks(spectrum);
    }

    /// <summary>
    /// Return frequencies for each index of a FFT.
    /// </summary>
    public static double[] FrequencyScale(int length, double sampleRate, bool positiveOnly = true)
    {
        double[] freqs = new double[length];

        if (positiveOnly)
        {
            double fftPeriodHz = sampleRate / (length - 1) / 2;

            // freqs start at 0 and approach maxFreq
            for (int i = 0; i < length; i++)
                freqs[i] = i * fftPeriodHz;

            return freqs;
        }
        else
        {
            double fftPeriodHz = sampleRate / length;

            // first half: freqs start a 0 and approach maxFreq
            int halfIndex = length / 2;
            for (int i = 0; i < halfIndex; i++)
                freqs[i] = i * fftPeriodHz;

            // second half: then start at -maxFreq and approach 0
            for (int i = halfIndex; i < length; i++)
                freqs[i] = -(length - i) * fftPeriodHz;

            return freqs;
        }
    }

    /// <summary>
    /// Return the resolution (distance between each frequency) of the FFT in Hz
    /// </summary>
    public static double FrequencyResolution(int length, double sampleRate, bool positiveOnly = true)
    {
        return positiveOnly
            ? sampleRate / (length - 1) / 2
            : sampleRate / length;
    }

    /// <summary>
    /// Return the phase for each point in a Complex array
    /// </summary>
    public static double[] Phase(System.Numerics.Complex[] spectrum)
    {
        double[] phase = new double[spectrum.Length];

        for (int i = 0; i < spectrum.Length; i++)
            phase[i] = spectrum[i].Phase;

        return phase;
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) in RMS units
    /// </summary>
    public static double[] Magnitude(System.Numerics.Complex[] spectrum, bool positiveOnly = true)
    {
        int length = positiveOnly ? spectrum.Length / 2 + 1 : spectrum.Length;

        double[] output = new double[length];

        // first point (DC component) is not doubled
        output[0] = spectrum[0].Magnitude / spectrum.Length;

        // subsequent points are doubled to account for combined positive and negative frequencies
        for (int i = 1; i < output.Length; i++)
            output[i] = 2 * spectrum[i].Magnitude / spectrum.Length;

        return output;
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) in dB units
    /// </summary>
    public static double[] Power(System.Numerics.Complex[] spectrum, bool positiveOnly = true)
    {
        double[] output = Magnitude(spectrum, positiveOnly);

        for (int i = 0; i < output.Length; i++)
            output[i] = 20 * Math.Log10(output[i]);

        return output;
    }

    /// <summary>
    /// Return a copy of the given values with the zero frequency component shifted to the center.
    /// </summary>
    public static double[] FftShift(double[] values)
    {
        int shiftBy = (values.Length + 1) / 2;

        double[] values2 = new double[values.Length];
        for (int i = 0; i < values.Length; i++)
            values2[i] = values[(i + shiftBy) % values.Length];

        return values2;
    }
}
using System;
using System.Buffers;
using System.Linq;

namespace FftSharp;

//[Obsolete("Use methods in the FftSharp.FFT class")]
public static class Transform
{
    /// <summary>
    /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
    [Obsolete("Use FftSharp.FFT.Forward()")]
    public static void FFT(Complex[] buffer)
    {
        if (buffer is null)
            throw new ArgumentNullException(nameof(buffer));

        FFT(buffer.AsSpan());
    }

    /// <summary>
    /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
    [Obsolete("Use FftSharp.FFT.Forward()")]
    public static void FFT(Span<Complex> buffer)
    {
        if (buffer.Length == 0)
            throw new ArgumentException("Buffer must not be empty");

        if (!IsPowerOfTwo(buffer.Length))
            throw new ArgumentException("Buffer length must be a power of 2");

        FftOperations.FFT_WithoutChecks(buffer);
    }

    /// <summary>
    /// Calculate sample frequency for each point in a FFT
    /// </summary>
    /// <param name="sampleRate">Sample rate (Hz) of the original signal</param>
    /// <param name="pointCount">Number of points to generate (typically the length of the FFT)</param>
    /// <param name="oneSided">Whether or not frequencies are for a one-sided FFT (containing only real numbers)</param>
    [Obsolete("Use FftSharp.FFT.FrequencyScale()")]
    public static double[] FFTfreq(double sampleRate, int pointCount, bool oneSided = true)
    {
        return FftSharp.FFT.FrequencyScale(pointCount, sampleRate, oneSided);
    }

    /// <summary>
    /// Calculate sample frequency for each point in a FFT
    /// </summary>
    /// <param name="sampleRate">Sample rate (Hz) of the original signal</param>
    /// <param name="fft">FFT array for which frequencies should be generated</param>
    /// <param name="oneSided">Whether or not frequencies are for a one-sided FFT (containing only real numbers)</param>
    [Obsolete("Use FftSharp.FFT.FrequencyScale()")]
    public static double[] FFTfreq(double sampleRate, double[] fft, bool oneSided = true)
    {
        return FFTfreq(sampleRate, fft.Length, oneSided);
    }

    /// <summary>
    /// Return the phase for each point in a Complex array
    /// </summary>
    [Obsolete("Use FftSharp.FFT.Phase()")]
    public static double[] FFTphase(Complex[] values)
    {
        return FftSharp.FFT.Phase(Complex.ToNumerics(values));
    }

    /// <summary>
    /// Take the FFT of the given array and return its phase
    /// </summary>
    [Obsolete("Use FftSharp.FFT.Phase()")]
    public static double[] FFTphase(double[] values)
    {
        Complex[] fft = FFT(values);
        double[] phase = FFTphase(fft);
        return phase;
    }

    /// <summary>
    /// Return the distance between each FFT point in frequency units (Hz)
    /// </summary>
    [Obsolete("Use FftSharp.FFT.FrequencyResolution()")]
    public static double FFTfreqPeriod(int sampleRate, int pointCount)
    {
        return .5 * sampleRate / pointCount;
    }

    /// <summary>
    /// Returns true if the given number is evenly divisible by a power of 2
    /// </summary>
    [Obsolete("Use FftSharp.FftOperations.IsPowerOfTwo()")]
    public static bool IsPowerOfTwo(int x) => FftOperations.IsPowerOfTwo(x);

    /// <summary>
    /// Create an array of Complex data given the real component
    /// </summary>
    [Obsolete("Use System.Numerics.Complex data structures instead")]
    public static Complex[] MakeComplex(double[] real)
    {
        Complex[] com = new Complex[real.Length];
        MakeComplex(com, real);
        return com;
    }

    /// <summary>
    /// Create an array of Complex data given the real component
    /// </summary>
    [Obsolete("Use System.Numerics.Complex data structures instead")]
    public static void MakeComplex(Span<Complex> com, Span<double> real)
    {
        if (com.Length != real.Length)
            throw new ArgumentOutOfRangeException("Input length must match");

        for (int i = 0; i < real.Length; i++)
            com[i] = new Complex(real[i], 0);
    }

    /// <summary>
    /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
    /// </summary>
    /// <param name="input">real input (must be an array with length that is a power of 2)</param>
    /// <returns>transformed input</returns>
    [Obsolete("Use FftSharp.FFT.Forward()")]
    public static Complex[] FFT(double[] input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        if (input.Length == 0)
            throw new ArgumentException("Input must not be empty");

        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        Complex[] buffer = MakeComplex(input);
        FFT(buffer);
        return buffer;
    }

    /// <summary>
    /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
    /// </summary>
    /// <param name="input">real input (must be an array with length that is a power of 2)</param>
    /// <returns>real component of transformed input</returns>
    [Obsolete("Use FftSharp.FFT.ForwardReal()")]
    public static Complex[] RFFT(double[] input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        if (input.Length == 0)
            throw new ArgumentException("Input must not be empty");

        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        Complex[] realBuffer = new Complex[input.Length / 2 + 1];
        RFFT(realBuffer, input);
        return realBuffer;
    }

    /// <summary>
    /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
    /// </summary>
    /// <param name="destination">Memory location of the results (must be an equal to input length / 2 + 1)</param>
    /// <param name="input">real input (must be an array with length that is a power of 2)</param>
    /// <returns>real component of transformed input</returns>
    [Obsolete("Use FftSharp.FFT.ForwardReal()")]
    public static void RFFT(Span<Complex> destination, Span<double> input)
    {
        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        if (destination.Length != input.Length / 2 + 1)
            throw new ArgumentException("Destination length must be an equal to input length / 2 + 1");

        Complex[] temp = ArrayPool<Complex>.Shared.Rent(input.Length);

        try
        {
            Span<Complex> buffer = temp.AsSpan(0, input.Length);
            MakeComplex(buffer, input);
            FFT(buffer);
            buffer.Slice(0, destination.Length).CopyTo(destination);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not calculate RFFT", ex);
        }
        finally
        {
            ArrayPool<Complex>.Shared.Return(temp);
        }
    }

    /// <summary>
    /// Compute the inverse discrete Fourier Transform (in-place) using the FFT algorithm.
    /// </summary>
    /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
    [Obsolete("Use FftSharp.FFT.Inverse()")]
    public static void IFFT(Complex[] buffer)
    {
        if (buffer is null)
            throw new ArgumentNullException(nameof(buffer));

        if (buffer.Length == 0)
            throw new ArgumentException("Buffer must not be empty");

        if (!IsPowerOfTwo(buffer.Length))
            throw new ArgumentException("Buffer length must be a power of 2");

        FftOperations.IFFT_WithoutChecks(buffer);
    }

    /// <summary>
    /// Return a Complex array as an array of its absolute values
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Obsolete("Use FftSharp.FFT.Magnitude()")]
    public static double[] Absolute(Complex[] input)
    {
        double[] output = new double[input.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = input[i].Magnitude;
        return output;
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) original (RMS) units
    /// </summary>
    /// <param name="input">real input</param>
    [Obsolete("Use FftSharp.FFT.Forward() then FftSharp.FFT.Magnitude()")]
    public static double[] FFTmagnitude(double[] input)
    {
        double[] output = new double[input.Length / 2 + 1];
        FFTmagnitude(output, input);
        return output;
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) original (RMS) units
    /// </summary>
    /// <param name="destination">Memory location of the results.</param>
    /// <param name="input">real input</param>
    [Obsolete("Use FftSharp.FFT.Magnitude()")]
    public static void FFTmagnitude(Span<double> destination, Span<double> input)
    {
        if (input.Length < 2)
            throw new ArgumentException("Input should have 2 points at least");

        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        var temp = ArrayPool<Complex>.Shared.Rent(destination.Length);
        try
        {
            var buffer = temp.AsSpan(0, destination.Length);

            // first calculate the FFT
            RFFT(buffer, input);

            // first point (DC component) is not doubled
            destination[0] = buffer[0].Magnitude / input.Length;

            // subsequent points are doubled to account for combined positive and negative frequencies
            for (int i = 1; i < buffer.Length; i++)
                destination[i] = 2 * buffer[i].Magnitude / input.Length;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not calculate FFT magnitude", ex);
        }
        finally
        {
            ArrayPool<Complex>.Shared.Return(temp);
        }
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) in dB units
    /// </summary>
    /// <param name="input">real input</param>
    [Obsolete("Use FftSharp.FFT.Forward() then FftSharp.FFT.Power()")]
    public static double[] FFTpower(double[] input)
    {
        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        double[] output = FFTmagnitude(input);
        for (int i = 0; i < output.Length; i++)
            output[i] = 2 * 10 * Math.Log10(output[i]);
        return output;
    }

    /// <summary>
    /// Calculate power spectrum density (PSD) in dB units
    /// </summary>
    /// <param name="destination">Memory location of the results.</param>
    /// <param name="input">real input</param>
    [Obsolete("Use FftSharp.FFT.Power()")]
    public static void FFTpower(Span<double> destination, double[] input)
    {
        if (!IsPowerOfTwo(input.Length))
            throw new ArgumentException("Input length must be an even power of 2");

        FFTmagnitude(destination, input);
        for (int i = 0; i < destination.Length; i++)
            destination[i] = 2 * 10 * Math.Log10(destination[i]);
    }

    /// <summary>
    /// Convert a Mel bin to frequency (Hz)
    /// </summary>
    [Obsolete("use FftSharp.Mel.ToFreq()")]
    public static double MelToFreq(double mel)
    {
        return Mel.ToFreq(mel);
    }

    /// <summary>
    /// Convert a frequency (Hz) to a Mel bin
    /// </summary>
    [Obsolete("use FftSharp.Mel.FromFreq()")]
    public static double MelFromFreq(double frequencyHz)
    {
        return Mel.FromFreq(frequencyHz);
    }

    [Obsolete("use FftSharp.Mel.FrequencyScale()")]
    public static double[] MelScale(double[] fft, int sampleRate, int melBinCount)
    {
        return Mel.Scale(fft, sampleRate, melBinCount);
    }
}

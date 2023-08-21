using System;
using System.Buffers;

namespace FftSharp;

/// <summary>
/// Primary methods for calculating FFT and performing related operations.
/// </summary>
internal class FftOperations
{
    /// <summary>
    /// Returns true if the given number is evenly divisible by a power of 2
    /// </summary>
    public static bool IsPowerOfTwo(int x)
    {
        return ((x & (x - 1)) == 0) && (x > 0);
    }

    /// <summary>
    /// High performance FFT function.
    /// Complex input will be transformed in place.
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    [Obsolete("Use methods which consume System.Numerics.Complex")]
    public static void FFT_WithoutChecks(Span<Complex> buffer)
    {
        for (int i = 1; i < buffer.Length; i++)
        {
            int j = BitReverse(i, buffer.Length);
            if (j > i)
                (buffer[j], buffer[i]) = (buffer[i], buffer[j]);
        }

        for (int i = 1; i <= buffer.Length / 2; i *= 2)
        {
            double mult1 = -Math.PI / i;
            for (int j = 0; j < buffer.Length; j += (i * 2))
            {
                for (int k = 0; k < i; k++)
                {
                    int evenI = j + k;
                    int oddI = j + k + i;
                    Complex temp = new(Math.Cos(mult1 * k), Math.Sin(mult1 * k));
                    temp *= buffer[oddI];
                    buffer[oddI] = buffer[evenI] - temp;
                    buffer[evenI] += temp;
                }
            }
        }
    }

    /// <summary>
    /// High performance FFT function.
    /// Complex input will be transformed in place.
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    public static void FFT_WithoutChecks(Span<System.Numerics.Complex> buffer)
    {
        for (int i = 1; i < buffer.Length; i++)
        {
            int j = BitReverse(i, buffer.Length);
            if (j > i)
                (buffer[j], buffer[i]) = (buffer[i], buffer[j]);
        }

        for (int i = 1; i <= buffer.Length / 2; i *= 2)
        {
            double mult1 = -Math.PI / i;
            for (int j = 0; j < buffer.Length; j += (i * 2))
            {
                for (int k = 0; k < i; k++)
                {
                    int evenI = j + k;
                    int oddI = j + k + i;
                    System.Numerics.Complex temp = new(Math.Cos(mult1 * k), Math.Sin(mult1 * k));
                    temp *= buffer[oddI];
                    buffer[oddI] = buffer[evenI] - temp;
                    buffer[evenI] += temp;
                }
            }
        }
    }

    /// <summary>
    /// Calculate FFT of the input and return just the real component
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    public static void RFFT_WithoutChecks(Span<System.Numerics.Complex> destination, Span<System.Numerics.Complex> input)
    {
        System.Numerics.Complex[] temp = ArrayPool<System.Numerics.Complex>.Shared.Rent(input.Length);

        try
        {
            Span<System.Numerics.Complex> buffer = temp.AsSpan(0, input.Length);
            input.CopyTo(buffer);
            FFT.Forward(buffer);
            buffer.Slice(0, destination.Length).CopyTo(destination);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not calculate RFFT", ex);
        }
        finally
        {
            ArrayPool<System.Numerics.Complex>.Shared.Return(temp);
        }
    }

    /// <summary>
    /// High performance inverse FFT function.
    /// Complex input will be transformed in place.
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    [Obsolete("Use methods which consume System.Numerics.Complex")]
    public static void IFFT_WithoutChecks(Span<Complex> buffer)
    {
        // invert the imaginary component
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(buffer[i].Real, -buffer[i].Imaginary);

        // perform a forward Fourier transform
        FFT_WithoutChecks(buffer);

        // invert the imaginary component again and scale the output
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(
                real: buffer[i].Real / buffer.Length,
                imaginary: -buffer[i].Imaginary / buffer.Length);
    }

    /// <summary>
    /// High performance inverse FFT function.
    /// Complex input will be transformed in place.
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    public static void IFFT_WithoutChecks(Span<System.Numerics.Complex> buffer)
    {
        // invert the imaginary component
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(buffer[i].Real, -buffer[i].Imaginary);

        // perform a forward Fourier transform
        FFT_WithoutChecks(buffer);

        // invert the imaginary component again and scale the output
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(
                real: buffer[i].Real / buffer.Length,
                imaginary: -buffer[i].Imaginary / buffer.Length);
    }

    /// <summary>
    /// High performance inverse FFT function.
    /// Complex input will be transformed in place.
    /// Input array length must be a power of two. This length is NOT validated.
    /// Running on an array with an invalid length may throw an invalid index exception.
    /// </summary>
    public static void IFFT_WithoutChecks(System.Numerics.Complex[] buffer)
    {
        // invert the imaginary component
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(buffer[i].Real, -buffer[i].Imaginary);

        // perform a forward Fourier transform
        FFT_WithoutChecks(buffer);

        // invert the imaginary component again and scale the output
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new(
                real: buffer[i].Real / buffer.Length,
                imaginary: -buffer[i].Imaginary / buffer.Length);
    }

    /// <summary>
    /// Reverse the order of bits in an integer
    /// </summary>
    private static int BitReverse(int value, int maxValue)
    {
        int maxBitCount = (int)Math.Log(maxValue, 2);
        int output = value;
        int bitCount = maxBitCount - 1;

        value >>= 1;
        while (value > 0)
        {
            output = (output << 1) | (value & 1);
            bitCount -= 1;
            value >>= 1;
        }

        return (output << bitCount) & ((1 << maxBitCount) - 1);
    }
}

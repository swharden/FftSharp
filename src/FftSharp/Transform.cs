using System;
using System.Buffers;

namespace FftSharp
{
    public static class Transform
    {
        /// <summary>
        /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
        /// </summary>
        /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
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
        public static void FFT(System.Numerics.Complex[] buffer)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            FFT(buffer.AsSpan());
        }

        /// <summary>
        /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
        /// </summary>
        /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
        public static void FFT(Span<Complex> buffer)
        {
            if (buffer.Length == 0)
                throw new ArgumentException("Buffer must not be empty");

            if (!IsPowerOfTwo(buffer.Length))
                throw new ArgumentException("Buffer length must be a power of 2");

            FFT_WithoutChecks(buffer);
        }

        /// <summary>
        /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm.
        /// </summary>
        /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
        public static void FFT(Span<System.Numerics.Complex> buffer)
        {
            if (buffer.Length == 0)
                throw new ArgumentException("Buffer must not be empty");

            if (!IsPowerOfTwo(buffer.Length))
                throw new ArgumentException("Buffer length must be a power of 2");

            FFT_WithoutChecks(buffer);
        }

        /// <summary>
        /// High performance FFT function.
        /// Complex input will be transformed in place.
        /// Input array length must be a power of two. This length is NOT validated.
        /// Running on an array with an invalid length may throw an invalid index exception.
        /// </summary>
        private static void FFT_WithoutChecks(Span<Complex> buffer)
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
                        Complex temp = new Complex(Math.Cos(mult1 * k), Math.Sin(mult1 * k));
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
        private static void FFT_WithoutChecks(Span<System.Numerics.Complex> buffer)
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
                        System.Numerics.Complex temp = new System.Numerics.Complex(Math.Cos(mult1 * k), Math.Sin(mult1 * k));
                        temp *= buffer[oddI];
                        buffer[oddI] = buffer[evenI] - temp;
                        buffer[evenI] += temp;
                    }
                }
            }
        }

        /// <summary>
        /// Compute the inverse discrete Fourier Transform (in-place) using the FFT algorithm.
        /// </summary>
        /// <param name="buffer">Data to transform in-place. Length must be a power of 2.</param>
        public static void IFFT(Complex[] buffer)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            if (buffer.Length == 0)
                throw new ArgumentException("Buffer must not be empty");

            if (!IsPowerOfTwo(buffer.Length))
                throw new ArgumentException("Buffer length must be a power of 2");

            IFFT_WithoutChecks(buffer);
        }

        private static void IFFT_WithoutChecks(Complex[] buffer)
        {
            // invert the imaginary component
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = new Complex(buffer[i].Real, -buffer[i].Imaginary);

            // perform a forward Fourier transform
            FFT(buffer);

            // invert the imaginary component again and scale the output
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = new Complex(
                    real: buffer[i].Real / buffer.Length,
                    imaginary: -buffer[i].Imaginary / buffer.Length);
        }

        /// <summary>
        /// Reverse the sequence of bits in an integer (01101 -> 10110)
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

        /// <summary>
        /// Calculate sample frequency for each point in a FFT
        /// </summary>
        /// <param name="sampleRate">Sample rate (Hz) of the original signal</param>
        /// <param name="pointCount">Number of points to generate (typically the length of the FFT)</param>
        /// <param name="oneSided">Whether or not frequencies are for a one-sided FFT (containing only real numbers)</param>
        public static double[] FFTfreq(double sampleRate, int pointCount, bool oneSided = true)
        {
            double[] freqs = new double[pointCount];

            if (oneSided)
            {
                double fftPeriodHz = sampleRate / (pointCount - 1) / 2;

                // freqs start at 0 and approach maxFreq
                for (int i = 0; i < pointCount; i++)
                    freqs[i] = i * fftPeriodHz;
                return freqs;
            }
            else
            {
                double fftPeriodHz = sampleRate / pointCount;

                // first half: freqs start a 0 and approach maxFreq
                int halfIndex = pointCount / 2;
                for (int i = 0; i < halfIndex; i++)
                    freqs[i] = i * fftPeriodHz;

                // second half: then start at -maxFreq and approach 0
                for (int i = halfIndex; i < pointCount; i++)
                    freqs[i] = -(pointCount - i) * fftPeriodHz;
                return freqs;
            }
        }

        /// <summary>
        /// Return the phase for each point in a Complex array
        /// </summary>
        public static double[] FFTphase(Complex[] values)
        {
            double[] phase = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
                phase[i] = values[i].Phase;

            return phase;
        }

        /// <summary>
        /// Take the FFT of the given array and return its phase
        /// </summary>
        public static double[] FFTphase(double[] values)
        {
            Complex[] fft = FFT(values);
            double[] phase = FFTphase(fft);
            return phase;
        }

        /// <summary>
        /// Calculate sample frequency for each point in a FFT
        /// </summary>
        /// <param name="sampleRate">Sample rate (Hz) of the original signal</param>
        /// <param name="fft">FFT array for which frequencies should be generated</param>
        /// <param name="oneSided">Whether or not frequencies are for a one-sided FFT (containing only real numbers)</param>
        public static double[] FFTfreq(double sampleRate, double[] fft, bool oneSided = true)
        {
            return FFTfreq(sampleRate, fft.Length, oneSided);
        }

        /// <summary>
        /// Return the distance between each FFT point in frequency units (Hz)
        /// </summary>
        public static double FFTfreqPeriod(int sampleRate, int pointCount)
        {
            return .5 * sampleRate / pointCount;
        }

        /// <summary>
        /// Test if a number is an even power of 2
        /// </summary>
        public static bool IsPowerOfTwo(int x)
        {
            return ((x & (x - 1)) == 0) && (x > 0);
        }

        /// <summary>
        /// Create an array of Complex data given the real component
        /// </summary>
        public static Complex[] MakeComplex(double[] real)
        {
            Complex[] com = new Complex[real.Length];
            MakeComplex(com, real);
            return com;
        }

        /// <summary>
        /// Create an array of Complex data given the real component
        /// </summary>
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
        /// Return a Complex array as an array of its absolute values
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        public static void FFTpower(Span<double> destination, double[] input)
        {
            if (!IsPowerOfTwo(input.Length))
                throw new ArgumentException("Input length must be an even power of 2");

            FFTmagnitude(destination, input);
            for (int i = 0; i < destination.Length; i++)
                destination[i] = 2 * 10 * Math.Log10(destination[i]);
        }

        public static double MelToFreq(double mel)
        {
            return 700 * (Math.Pow(10, mel / 2595d) - 1);
        }

        public static double MelFromFreq(double frequencyHz)
        {
            return 2595 * Math.Log10(1 + frequencyHz / 700);
        }

        public static double[] MelScale(double[] fft, int sampleRate, int melBinCount)
        {
            double freqMax = sampleRate / 2;
            double maxMel = MelFromFreq(freqMax);

            double[] fftMel = new double[melBinCount];
            double melPerBin = maxMel / (melBinCount + 1);
            for (int binIndex = 0; binIndex < melBinCount; binIndex++)
            {
                double melLow = melPerBin * binIndex;
                double melHigh = melPerBin * (binIndex + 2);

                double freqLow = MelToFreq(melLow);
                double freqHigh = MelToFreq(melHigh);

                int indexLow = (int)(fft.Length * freqLow / freqMax);
                int indexHigh = (int)(fft.Length * freqHigh / freqMax);
                int indexSpan = indexHigh - indexLow;

                double binScaleSum = 0;
                for (int i = 0; i < indexSpan; i++)
                {
                    double binFrac = (double)i / indexSpan;
                    double indexScale = (binFrac < .5) ? binFrac * 2 : 1 - binFrac;
                    binScaleSum += indexScale;
                    fftMel[binIndex] += fft[indexLow + i] * indexScale;
                }
                fftMel[binIndex] /= binScaleSum;
            }

            return fftMel;
        }
    }
}

using System;
using System.Numerics;

namespace FftSharp
{
    public static class Transform
    {
        /// <summary>
        /// Test if a number is an even power of 2
        /// </summary>
        public static bool IsPowerOfTwo(int x)
        {
            return ((x & (x - 1)) == 0) && (x > 0);
        }

        /// <summary>
        /// Return the input array (or a new zero-padded new one) ensuring length is a power of 2
        /// </summary>
        /// <param name="input">complex array of any length</param>
        /// <returns>the input array or a zero-padded copy</returns>
        public static Complex[] ZeroPad(Complex[] input)
        {
            if (IsPowerOfTwo(input.Length))
                return input;

            int targetLength = 1;
            while (targetLength < input.Length)
                targetLength *= 2;

            int difference = targetLength - input.Length;
            Complex[] padded = new Complex[targetLength];
            Array.Copy(input, 0, padded, difference / 2, input.Length);

            return padded;
        }

        /// <summary>
        /// Create an array of Complex data given the real component
        /// </summary>
        private static Complex[] Complex(double[] real)
        {
            Complex[] com = new Complex[real.Length];
            for (int i = 0; i < real.Length; i++)
                com[i] = new Complex(real[i], 0);
            return com;
        }

        /// <summary>
        /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>transformed input</returns>
        public static Complex[] FFT(double[] input)
        {
            Complex[] buffer = Complex(input);
            FFT(buffer);
            return buffer;
        }

        /// <summary>
        /// An easy-to-read (but non-optimized) implementation of the FFT algorithm
        /// </summary>
        /// <param name="input">complex input</param>
        /// <returns>transformed input</returns>
        [Obsolete("This method is documentation purposes only.", true)]
        private static Complex[] FFTstandard(Complex[] input)
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
            odds = FFTstandard(odds);
            evens = FFTstandard(evens);

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
        /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm
        /// </summary>
        public static void FFT(Complex[] buffer)
        {
            if (!IsPowerOfTwo(buffer.Length))
                throw new ArgumentException("Length must be a power of 2. ZeroPad() may help.");

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
        /// Compute the inverse discrete Fourier Transform (in-place) using the FFT algorithm
        /// </summary>
        public static void IFFT(Complex[] buffer)
        {
            // invert the imaginary component
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = new Complex(buffer[i].Real, -buffer[i].Imaginary);

            // perform a forward Fourier transform
            FFT(buffer);

            // invert the imaginary component again and scale the output
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = new Complex(buffer[i].Real / buffer.Length,
                                       -buffer[i].Imaginary / buffer.Length);
        }

        /// <summary>
        /// Compute the discrete Fourier Transform (not using the FFT algorithm)
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>transformed input</returns>
        [Obsolete("This method is documentation purposes only.")]
        public static Complex[] DFT(double[] input)
        {
            return DFT(Complex(input));
        }

        /// <summary>
        /// Compute the forward or inverse discrete Fourier Transform (not using the FFT algorithm)
        /// </summary>
        /// <param name="real">complex input</param>
        /// <returns>transformed input</returns>
        [Obsolete("This method is documentation purposes only.")]
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

        /// <summary>
        /// Calculte FFT of the input and return the power spectrum density (PSD)
        /// </summary>
        /// <param name="input">real input</param>
        /// <param name="singleSided">combine positive and negative power (useful when symmetrical)</param>
        /// <param name="decibels">return 20*Log10(magnitude) so has dB units</param>
        public static double[] FFTpower(double[] input, bool singleSided = true, bool decibels = true)
        {
            // first calculate the FFT
            Complex[] buffer = Complex(input);
            FFT(buffer);

            // create an array of the complex magnitudes
            double[] output;
            if (singleSided)
            {
                output = new double[buffer.Length / 2];

                // double to account for negative power
                for (int i = 0; i < output.Length; i++)
                    output[i] = buffer[i].Magnitude * 2;

                // first point (DC component) is not doubled
                output[0] = buffer[0].Magnitude;
            }
            else
            {
                output = new double[buffer.Length];
                for (int i = 0; i < output.Length; i++)
                    output[i] = buffer[i].Magnitude;
            }

            // convert to dB (the 2 comes from the conversion from RMS)
            if (decibels)
                for (int i = 0; i < output.Length; i++)
                    output[i] = 2 * 10 * Math.Log10(output[i]);

            return output;
        }

        /// <summary>
        /// Calculate sample frequency for each point in a FFT
        /// </summary>
        public static double[] FFTfreq(int sampleRate, int pointCount, bool oneSided = true)
        {
            double[] freqs = new double[pointCount];

            if (oneSided)
            {
                double fftPeriodHz = (double)sampleRate / pointCount / 2;

                // freqs start at 0 and approach maxFreq
                for (int i = 0; i < pointCount; i++)
                    freqs[i] = i * fftPeriodHz;
                return freqs;
            }
            else
            {
                double fftPeriodHz = (double)sampleRate / pointCount;

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
    }
}

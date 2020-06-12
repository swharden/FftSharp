using System;
using System.Numerics;

namespace FftSharp
{
    public static class Transform
    {
        /// <summary>
        /// Compute the discrete Fourier Transform (in-place) using the FFT algorithm
        /// </summary>
        public static void FFT(Complex[] buffer)
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
            if (!IsPowerOfTwo(input.Length))
                throw new ArgumentException("Input length must be an even power of 2");

            Complex[] buffer = MakeComplex(input);
            FFT(buffer);
            return buffer;
        }

        /// <summary>
        /// Calculte power spectrum density (PSD) original (RMS) units
        /// </summary>
        /// <param name="input">real input</param>
        public static double[] FFTmagnitude(double[] input)
        {
            if (!IsPowerOfTwo(input.Length))
                throw new ArgumentException("Input length must be an even power of 2");

            // first calculate the FFT
            Complex[] buffer = FFT(input);
            FFT(buffer);

            // create an array of the complex magnitudes
            double[] output;
            output = new double[buffer.Length / 2];

            // double to account for negative power
            for (int i = 0; i < output.Length; i++)
                output[i] = buffer[i].Magnitude * 2;

            // first point (DC component) is not doubled
            output[0] = buffer[0].Magnitude; // TODO: divide by FFT length???

            return output;
        }

        /// <summary>
        /// Calculte power spectrum density (PSD) in dB units
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
    }
}

using System;
using System.Numerics;

namespace FftSharp
{
    public static class Transform
    {
        /// <summary>
        /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>transformed input</returns>
        public static Complex[] FFT(double[] input)
        {
            return FFT(Complex(input));
        }

        /// <summary>
        /// Compute the 1D discrete Fourier Transform using the Fast Fourier Transform (FFT) algorithm
        /// </summary>
        /// <param name="input">complex input</param>
        /// <returns>transformed input</returns>
        public static Complex[] FFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];
            Complex[] d, D, e, E;

            if (N == 1)
            {
                output[0] = input[0];
                return output;
            }

            int k;
            e = new Complex[N / 2];
            d = new Complex[N / 2];

            for (k = 0; k < N / 2; k++)
            {
                e[k] = input[2 * k];
                d[k] = input[2 * k + 1];
            }

            D = FFT(d);
            E = FFT(e);

            for (k = 0; k < N / 2; k++)
            {
                double radians = -2 * Math.PI * k / N;
                D[k] *= new Complex(Math.Cos(radians), Math.Sin(radians));
            }

            for (k = 0; k < N / 2; k++)
            {
                output[k] = E[k] + D[k];
                output[k + N / 2] = E[k] - D[k];
            }

            return output;
        }

        /// <summary>
        /// Compute the 1D discrete Fourier Transform (not using the fast FFT algorithm)
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>transformed input</returns>
        public static Complex[] DFT(double[] input)
        {
            return DFT(Complex(input));
        }

        /// <summary>
        /// Compute the 1D discrete Fourier Transform (not using the fast FFT algorithm)
        /// </summary>
        /// <param name="real">complex input</param>
        /// <returns>transformed input</returns>
        public static Complex[] DFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];

            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double radians = -2 * Math.PI * n * k / N;
                    Complex temp = new Complex(Math.Cos(radians), Math.Sin(radians));
                    temp *= input[n];
                    output[k] += temp;
                }
            }

            return output;
        }

        /// <summary>
        /// Return frequencies for each point in an FFT
        /// </summary>
        public static double[] FFTfreq(int sampleRate, int pointCount, bool mirror = false)
        {
            double[] freqs = new double[pointCount];

            if (mirror == false)
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
        /// Create an array of magnitudes for Complex inputs
        /// </summary>
        private static double[] Magnitude(Complex[] input, bool half = false)
        {
            int length = (half) ? input.Length / 2 : input.Length;
            double[] output = new double[length];
            for (int i = 0; i < length; i++)
                output[i] = input[i].Magnitude;
            return output;
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
        /// Calculte FFT amplitude (10 times the Log10 of the magnitude of the FFT)
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>Amplitude (dB)</returns>
        public static double[] FFTamplitude(double[] input)
        {
            return FFTdB(input, 10);
        }

        /// <summary>
        /// Calculte FFT power (20 times the Log10 of the magnitude of the FFT)
        /// </summary>
        /// <param name="input">real input</param>
        /// <returns>Power (dB)</returns>
        public static double[] FFTpower(double[] input)
        {
            return FFTdB(input, 20);
        }

        /// <summary>
        /// Calculte FFT and return the magnitude of the complex values
        /// </summary>
        /// <param name="input">real input</param>
        /// <param name="half">return only the first half of the data (useful if result is mirrored)</param>
        /// <returns>Magnitudes (not log transformed)</returns>
        public static double[] FFTmagnitude(double[] input, bool half = true)
        {
            Complex[] fft = FFT(Complex(input));
            return Magnitude(fft, half);
        }

        /// <summary>
        /// Calculte FFT magnitude and convert to dB
        /// </summary>
        /// <param name="multiplier">10 for amplitude, 20 for power</param>
        /// <returns>Magnitude (dB)</returns>
        private static double[] FFTdB(double[] input, double multiplier = 20)
        {
            Complex[] fft = FFT(Complex(input));
            double[] output = Magnitude(fft, half: true);
            for (int i = 0; i < output.Length; i++)
                output[i] = Math.Log10(output[i]) * multiplier;
            return output;
        }
    }
}

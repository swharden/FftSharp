using System;

namespace FftSharp
{
    public static class Filter
    {
        /// <summary>
        /// Silence frequencies above the given frequency
        /// </summary>
        public static double[] LowPass(double[] values, double sampleRate, double maxFrequency) =>
            BandPass(values, sampleRate, double.NegativeInfinity, maxFrequency);

        /// <summary>
        /// Silence frequencies below the given frequency
        /// </summary>
        public static double[] HighPass(double[] values, double sampleRate, double minFrequency) =>
            BandPass(values, sampleRate, minFrequency, double.PositiveInfinity);

        /// <summary>
        /// Silence frequencies outside the given frequency range
        /// </summary>
        public static double[] BandPass(double[] values, double sampleRate, double minFrequency, double maxFrequency)
        {
            Complex[] fft = Transform.FFT(values);
            double[] fftFreqs = Transform.FFTfreq(sampleRate, fft.Length, oneSided: false);
            for (int i = 0; i < fft.Length; i++)
            {
                double freq = Math.Abs(fftFreqs[i]);
                if ((freq > maxFrequency) || (freq < minFrequency))
                {
                    fft[i].Real = 0;
                    fft[i].Imaginary = 0;
                }
            }
            return InverseReal(fft);
        }

        /// <summary>
        /// Silence frequencies inside the given frequency range
        /// </summary>
        public static double[] BandStop(double[] values, double sampleRate, double minFrequency, double maxFrequency)
        {
            Complex[] fft = Transform.FFT(values);
            double[] fftFreqs = Transform.FFTfreq(sampleRate, fft.Length, oneSided: false);
            for (int i = 0; i < fft.Length; i++)
            {
                double freq = Math.Abs(fftFreqs[i]);
                if ((freq <= maxFrequency) && (freq >= minFrequency))
                {
                    fft[i].Real = 0;
                    fft[i].Imaginary = 0;
                }
            }
            return InverseReal(fft);
        }

        private static double[] InverseReal(Complex[] fft)
        {
            Transform.IFFT(fft);
            double[] Filtered = new double[fft.Length];
            for (int i = 0; i < fft.Length; i++)
                Filtered[i] = fft[i].Real;
            return Filtered;
        }
    }
}

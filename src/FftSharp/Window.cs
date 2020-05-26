using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp
{
    public static class Window
    {
        public static double[] Apply(double[] window, double[] signal, bool normalize = false)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same size");

            if (normalize)
            {
                throw new NotImplementedException();
            }

            double[] output = new double[signal.Length];
            for (int i = 0; i < signal.Length; i++)
                output[i] = signal[i] * window[i];
            return output;
        }

        public static void ApplyInPlace(double[] window, double[] signal, bool normalize = false)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same size");

            if (normalize)
            {
                throw new NotImplementedException();
            }

            for (int i = 0; i < signal.Length; i++)
                signal[i] *= window[i];
        }

        public static double[] Hanning(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
                window[i] = 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / pointCount);
            return window;
        }

        public static double[] Hamming(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / pointCount);
            }
            return window;
        }

        public static double[] Blackman(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = 0.42 - 0.50 * Math.Cos(2 * Math.PI * i / pointCount) +
                                   0.08 * Math.Cos(4 * Math.PI * i / pointCount);
            }
            return window;
        }

        public static double[] BlackmanExact(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = 0.42659071 - 0.49656062 * Math.Cos(2 * Math.PI * i / pointCount) +
                                         0.07684867 * Math.Cos(4 * Math.PI * i / pointCount);
            }
            return window;
        }

        public static double[] BlackmanHarris(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = (0.42323 - 0.49755 * Math.Cos(2 * Math.PI * i / pointCount) +
                                       0.07922 * Math.Cos(4 * Math.PI * i / pointCount));
            }
            return window;
        }

        public static double[] FlatTop(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = (0.2810639 - 0.5208972 * Math.Cos(2 * Math.PI * i / pointCount) +
                                         0.1980399 * Math.Cos(4 * Math.PI * i / pointCount));
            }
            return window;
        }

        public static double[] Bartlett(int pointCount)
        {
            double[] window = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                window[i] = (1 - Math.Abs((double)(i - (pointCount / 2)) / (pointCount / 2)));
            }
            return window;
        }
    }
}

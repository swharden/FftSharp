using System;
using System.Linq;
using System.Reflection;

namespace FftSharp
{
    public static class Window
    {
        /// <summary>
        /// Return the signal scaled by a window
        /// </summary>
        public static double[] Apply(double[] window, double[] signal)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same length");

            double[] output = new double[window.Length];

            for (int i = 0; i < signal.Length; i++)
                output[i] = signal[i] * window[i];

            return output;
        }

        /// <summary>
        /// Scales the signal by a window in-place
        /// </summary>
        public static void ApplyInPlace(double[] window, double[] signal)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same length");

            for (int i = 0; i < signal.Length; i++)
                signal[i] = signal[i] * window[i];
        }

        private static void NormalizeInPlace(double[] window)
        {
            double sum = 0;
            for (int i = 0; i < window.Length; i++)
                sum += window[i];

            for (int i = 0; i < window.Length; i++)
                window[i] /= sum;
        }

        public static double[] Rectangular(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = 1;

            return window;
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
                window[i] = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / pointCount);

            return window;
        }

        public static double[] Blackman(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = 0.42 - 0.50 * Math.Cos(2 * Math.PI * i / pointCount) +
                                   0.08 * Math.Cos(4 * Math.PI * i / pointCount);

            return window;
        }

        public static double[] BlackmanExact(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = 0.42659071 - 0.49656062 * Math.Cos(2 * Math.PI * i / pointCount) +
                                         0.07684867 * Math.Cos(4 * Math.PI * i / pointCount);

            return window;
        }

        public static double[] BlackmanHarris(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = (0.42323 - 0.49755 * Math.Cos(2 * Math.PI * i / pointCount) +
                                       0.07922 * Math.Cos(4 * Math.PI * i / pointCount));

            return window;
        }

        public static double[] FlatTop(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = (0.2810639 - 0.5208972 * Math.Cos(2 * Math.PI * i / pointCount) +
                                         0.1980399 * Math.Cos(4 * Math.PI * i / pointCount));

            return window;
        }

        public static double[] Bartlett(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = 1 - Math.Abs((double)(i - (pointCount / 2)) / (pointCount / 2));

            return window;
        }

        public static double[] Cosine(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = Math.Sin(i * Math.PI / (pointCount - 1));

            return window;
        }

        public static string[] GetWindowNames()
        {
            return typeof(Window)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.ReturnType.Equals(typeof(double[])))
                    .Where(x => x.GetParameters().Length == 1)
                    .Where(x => x.GetParameters()[0].ParameterType == typeof(int))
                    .Select(x => x.Name)
                    .ToArray();
        }

        public static double[] GetWindowByName(string windowName, int pointCount)
        {
            MethodInfo[] windowInfos = typeof(Window)
                                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                                .Where(x => x.ReturnType.Equals(typeof(double[])))
                                .Where(x => x.GetParameters().Length == 1)
                                .Where(x => x.GetParameters()[0].ParameterType == typeof(int))
                                .Where(x => x.Name == windowName)
                                .ToArray();

            if (windowInfos.Length == 0)
                throw new ArgumentException($"invalid window name: {windowName}");

            object[] parameters = new object[] { pointCount };
            double[] result = (double[])windowInfos[0].Invoke(null, parameters);
            return result;
        }
    }
}

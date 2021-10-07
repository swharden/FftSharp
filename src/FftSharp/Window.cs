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

        public static int Factorial(int k)
        {
            int result = 1;

            for (int i = 2; i <= k; i++)
                result *= i;

            return result;
        }

        public static double[] BesselZero(int pointCount)
        {
            double[] window = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
                window[i] = I0(i);

            return window;
        }

        public static double I0(double x)
        {
            // Derived from code workby oygx210/navguide:
            // https://github.com/oygx210/navguide/blob/master/src/common/bessel.c

            double ax = Math.Abs(x);
            if (ax < 3.75)
            {
                double y = Math.Pow(x / 3.75, 2);
                double[] m = { 3.5156229, 3.0899424, 1.2067492, 0.2659732, 0.360768e-1, 0.45813e-2 };
                return 1.0 + y * (m[0] + y * (m[1] + y * (m[2] + y * (m[3] + y * (m[4] + y * m[5])))));
            }
            else
            {
                double y = 3.75 / ax;
                double[] m = { 0.39894228, 0.1328592e-1, 0.225319e-2, -0.157565e-2, 0.916281e-2, -0.2057706e-1, 0.2635537e-1, -0.1647633e-1, 0.392377e-2 };
                return (Math.Exp(ax) / Math.Sqrt(ax)) * (m[0] + y * (m[1] + y * (m[2] + y * (m[3] + y * (m[4] + y * (m[5] + y * (m[6] + y * (m[7] + y * m[8]))))))));
            }
        }

        public static double[] Kaiser(int pointCount, double beta)
        {
            // derived from python/numpy:
            // https://github.com/numpy/numpy/blob/v1.21.0/numpy/lib/function_base.py#L3267-L3392

            int M = pointCount;
            double alpha = (M - 1) / 2.0;

            double[] window = new double[pointCount];

            for (int n = 0; n < pointCount; n++)
            {
                window[n] = I0(beta * Math.Sqrt(1 - (Math.Pow((n - alpha) / alpha, 2)))) / I0(beta);
            }

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

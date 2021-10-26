using System;

namespace FftSharp.Windows
{
    public class Kaiser : WindowBase, IWindow
    {
        public readonly double Beta;

        public Kaiser(double beta)
        {
            Beta = beta;
        }

        public override double[] Create(int size, bool normalize = false)
        {
            // derived from python/numpy:
            // https://github.com/numpy/numpy/blob/v1.21.0/numpy/lib/function_base.py#L3267-L3392

            int M = size;
            double alpha = (M - 1) / 2.0;

            double[] window = new double[size];

            for (int n = 0; n < size; n++)
                window[n] = I0(Beta * Math.Sqrt(1 - Math.Pow((n - alpha) / alpha, 2))) / I0(Beta);

            if (normalize)
                NormalizeInPlace(window);

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
    }
}

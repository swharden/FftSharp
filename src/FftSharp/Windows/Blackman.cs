using System;

namespace FftSharp.Windows
{
    public class Blackman : WindowBase, IWindow
    {
        private readonly double A;
        private readonly double B;
        private readonly double C;

        public override string Name => "Blackman";
        public override string Description => "???";

        public Blackman(double a = 0.42659071, double b = 0.49656062, double c = 0.07684867)
        {
            (A, B, C) = (a, b, c);
        }

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = A - B * Math.Cos(2 * Math.PI * i / size) + C * Math.Cos(4 * Math.PI * i / size);

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

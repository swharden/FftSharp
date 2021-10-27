using System;

namespace FftSharp.Windows
{
    public class Blackman : Window, IWindow
    {
        private readonly double A = 0.42659071;
        private readonly double B = 0.49656062;
        private readonly double C = 0.07684867;

        public override string Name => "Blackman-Harris";
        public override string Description =>
            "The Blackman-Harris window is similar to Hamming and Hanning windows. " +
            "The resulting spectrum has a wide peak, but good side lobe compression.";

        public Blackman()
        {
        }

        public Blackman(double a, double b, double c)
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

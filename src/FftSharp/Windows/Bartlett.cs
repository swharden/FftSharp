using System;

namespace FftSharp.Windows
{
    public class Bartlett : Window, IWindow
    {
        public override string Name => "Bartlett–Hann";
        public override string Description =>
            "The Bartlett–Hann window is triangular in shape (a 2nd order B-spline) which is effectively the " +
            "convolution of two half-sized rectangular windows.";

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = 1 - Math.Abs((double)(i - (size / 2)) / (size / 2));

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

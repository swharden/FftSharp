using System;

namespace FftSharp.Windows
{
    internal class Hamming : WindowBase, IWindow
    {
        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / size);

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

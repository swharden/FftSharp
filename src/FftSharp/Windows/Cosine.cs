using System;

namespace FftSharp.Windows
{
    internal class Cosine : WindowBase, IWindow
    {
        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = Math.Sin(i * Math.PI / (size - 1));

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

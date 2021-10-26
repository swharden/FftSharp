using System;

namespace FftSharp.Windows
{
    public class Bartlett : WindowBase, IWindow
    {
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

using System;

namespace FftSharp.Windows
{
    public class Hanning : WindowBase, IWindow
    {
        public override string Name => "Hanning";
        public override string Description => "???";
        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / size);

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}
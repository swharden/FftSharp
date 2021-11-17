using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Windows
{
    public class Welch : Window, IWindow
    {
        public override string Name => "Welch";
        public override string Description =>
            "The Welch window is typically used for antialiasing and resampling. " +
            "Its frequency response is better than that of the Bartlett windowed cosc function below pi, " +
            "but it shows again a rather distinctive bump above.";

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            double halfN = (size - 1) / 2.0;

            for (int i = 0; i < size; i++)
            {
                double b = (i - halfN) / halfN;
                window[i] = 1 - b * b;
            }

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

using System;
using System.Linq;

namespace FftSharp.Windows
{
    public class Cosine : Window, IWindow
    {
        public override string Name => "Cosine";
        public override string Description =>
            "This window is simply a cosine function. It reaches zero on both sides and is similar to " +
            "Blackman, Hamming, Hanning, and flat top windows, but probably should not be used in practice.";
        
        public override bool IsSymmetric => true;

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = Enumerable.Range(0, size).Select(x => Math.Sin(Math.PI / (size) * (x + .5))).ToArray();

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

using System;

namespace FftSharp.Windows
{
    public class Hamming : Window, IWindow
    {
        public override string Name => "Hamming";
        public override string Description =>
            "The Hamming window has a sinusoidal shape does NOT touch zero at the edges (unlike the similar Hanning window). " +
            "It is similar to the Hanning window but its abrupt edges are designed to cancel the largest side lobe. " +
            "It may be a good choice for low-quality (8-bit) auto where side lobes lie beyond the quantization noise floor.";

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

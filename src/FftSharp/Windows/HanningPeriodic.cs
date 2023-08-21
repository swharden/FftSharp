using System;

namespace FftSharp.Windows
{
    public class HanningPeriodic : Window, IWindow
    {
        public override string Name => "HanningPeriodic";
        public override string Description =>
            "The Hanning window has a sinusoidal shape which touches zero at the edges (unlike the similar Hamming window). " +
            "It has good frequency resolution, low spectral leakage, and is satisfactory for 95 percent of use cases. " +
            "If you do not know the nature of the signal but you want to apply a smoothing window, start with the Hann window." +
            "A periodic window, for use in spectral analysis.";

        public override bool IsSymmetric => false;

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            double phaseStep = (2.0 * Math.PI) / size;

            for (int i = 0; i < size; i++)
                window[i] = 0.5 - 0.5 * Math.Cos(i * phaseStep);

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}
namespace FftSharp.Windows
{
    public class Rectangular : Window, IWindow
    {
        public override string Name => "Rectangular";
        public override string Description =>
            "The rectangular window (sometimes known as the boxcar or Dirichlet window) is the simplest window, " +
            "equivalent to replacing all but N values of a data sequence by zeros, making it appear as though " +
            "the waveform suddenly turns on and off. This window preserves transients at the start and end of the signal.";

        public override bool IsSymmetric => true;

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
                window[i] = 1;

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

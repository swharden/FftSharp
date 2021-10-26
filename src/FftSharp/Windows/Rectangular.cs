namespace FftSharp.Windows
{
    public class Rectangular : WindowBase, IWindow
    {
        public override string Name => "Rectangular";
        public override string Description => "???";
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

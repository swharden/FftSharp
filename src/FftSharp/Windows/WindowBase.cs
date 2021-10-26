namespace FftSharp.Windows
{
    public abstract class WindowBase : IWindow
    {
        public abstract double[] Create(int size, bool normalize = false);

        /// <summary>
        /// Multiply the array by this window and return the result as a new array
        /// </summary>
        public void Apply(double[] input, bool normalize = false)
        {
            double[] window = Create(input.Length, normalize);
            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = input[i] * window[i];
        }

        /// <summary>
        /// Multiply the array by this window, modifying it in place
        /// </summary>
        public void ApplyInPlace(double[] input, bool normalize = false)
        {
            double[] window = Create(input.Length, normalize);
            for (int i = 0; i < input.Length; i++)
                input[i] = input[i] * window[i];
        }

        internal static void NormalizeInPlace(double[] values)
        {
            double sum = 0;
            for (int i = 0; i < values.Length; i++)
                sum += values[i];

            for (int i = 0; i < values.Length; i++)
                values[i] /= sum;
        }
    }
}

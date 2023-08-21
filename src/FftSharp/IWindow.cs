namespace FftSharp
{
    public interface IWindow
    {
        /// <summary>
        /// Generate this window as a new array with the given length.
        /// Normalizing will scale the window so the sum of all points is 1.
        /// </summary>
        double[] Create(int size, bool normalize = false);

        /// <summary>
        /// Return a new array where this window was multiplied by the given signal.
        /// Normalizing will scale the window so the sum of all points is 1 prior to multiplication.
        /// </summary>
        double[] Apply(double[] input, bool normalize = false);

        /// <summary>
        /// Modify the given signal by multiplying it by this window IN PLACE.
        /// Normalizing will scale the window so the sum of all points is 1 prior to multiplication.
        /// </summary>
        void ApplyInPlace(double[] input, bool normalize = false);

        /// <summary>
        /// Single word name for this window
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A brief description of what makes this window unique and what it is typically used for.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Indicates whether the window is symmetric around its midpoint
        /// </summary>
        bool IsSymmetric { get; }
    }
}

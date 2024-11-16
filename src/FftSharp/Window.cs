using System;
using System.Linq;
using System.Reflection;

namespace FftSharp;

/// <summary>
/// Describes a window function that may be used to shape a segment of signal data before spectral transformation
/// </summary>
public abstract class Window : IWindow
{
    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract bool IsSymmetric { get; }

    public override string ToString() => Name;

    /// <summary>
    /// Generate an array of values shaped like this window
    /// </summary>
    /// <param name="size">number of points to generate</param>
    /// <param name="normalize">if true, sum of all values returned will equal 1</param>
    /// <returns></returns>
    public abstract double[] Create(int size, bool normalize = false);

    /// <summary>
    /// Multiply the array by this window and return the result as a new array
    /// </summary>
    public double[] Apply(double[] input, bool normalize = false)
    {
        // TODO: save this window so it can be re-used if the next request is the same size
        double[] window = Create(input.Length, normalize);
        double[] output = new double[input.Length];
        for (int i = 0; i < input.Length; i++)
            output[i] = input[i] * window[i];
        return output;
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

    /// <summary>
    /// Scale all values in the window equally (in-place) so their total is 1
    /// </summary>
    internal static void NormalizeInPlace(double[] values)
    {
        double sum = 0;
        for (int i = 0; i < values.Length; i++)
            sum += values[i];

        for (int i = 0; i < values.Length; i++)
            values[i] /= sum;
    }

    /// <summary>
    /// Return an array containing all available windows.
    /// Note that all windows returned will use the default constructor, but some
    /// windows have customization options in their constructors if you create them individually.
    /// </summary>
    public static IWindow[] GetWindows()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsClass)
            .Where(x => !x.IsAbstract)
            .Where(x => x.GetInterfaces().Contains(typeof(IWindow)))
            .Select(x => (IWindow)Activator.CreateInstance(x))
            .ToArray();
    }
}

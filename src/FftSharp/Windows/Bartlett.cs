using System;

namespace FftSharp.Windows;

public class Bartlett : Window, IWindow
{
    public override string Name => "Bartlett–Hann";
    public override string Description =>
        "The Bartlett–Hann window is triangular in shape (a 2nd order B-spline) which is effectively the " +
        "convolution of two half-sized rectangular windows.";

    public override bool IsSymmetric => true;

    public override double[] Create(int size, bool normalize = false)
    {
        double[] window = new double[size];

        bool isOddSize = size % 2 == 1;

        double halfSize = isOddSize ? size / 2 : (size - 1) / 2.0;

        for (int i = 0; i < size; i++)
            window[i] = 1 - Math.Abs((double)(i - halfSize) / halfSize);

        if (normalize)
            NormalizeInPlace(window);

        return window;
    }
}

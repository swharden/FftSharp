namespace FftSharp;

internal static class Extensions
{
    /// <summary>
    /// Return a Complex array with the given values as the real component
    /// </summary>
    /// <param name="real"></param>
    /// <returns></returns>
    public static System.Numerics.Complex[] ToComplexArray(this double[] real)
    {
        System.Numerics.Complex[] buffer = new System.Numerics.Complex[real.Length];

        for (int i = 0; i < real.Length; i++)
            buffer[i] = new(real[i], 0);

        return buffer;
    }
}

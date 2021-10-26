namespace FftSharp
{
    public interface IWindow
    {
        double[] Create(int size, bool normalize = false);
        double[] Apply(double[] input, bool normalize = false);
        void ApplyInPlace(double[] input, bool normalize = false);
        string Name { get; }
        string Description { get; }
    }
}

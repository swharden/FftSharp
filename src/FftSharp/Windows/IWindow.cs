namespace FftSharp.Windows
{
    public interface IWindow
    {
        double[] Create(int size, bool normalize = false);
    }
}

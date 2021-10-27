using System;
using System.Linq;
using System.Reflection;

namespace FftSharp
{
    public abstract class Window : IWindow
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public override string ToString() => Name;

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

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Rectangular(int pointCount) => new Windows.Rectangular().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Hanning(int pointCount) => new Windows.Hanning().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Hamming(int pointCount) => new Windows.Hanning().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Blackman(int pointCount) => new Windows.Blackman().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] BlackmanCustom(int pointCount, double a = .42, double b = .5, double c = .08) => new Windows.Blackman(a, b, c).Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] BlackmanHarris(int pointCount) => new Windows.Blackman(0.42323, 0.49755, 0.07922).Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] FlatTop(int pointCount) => new Windows.FlatTop().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Bartlett(int pointCount) => new Windows.Bartlett().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Cosine(int pointCount) => new Windows.Cosine().Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Kaiser(int pointCount, double beta) => new Windows.Kaiser(beta).Create(pointCount);

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static double[] Apply(double[] window, double[] signal)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same length");

            double[] output = new double[window.Length];

            for (int i = 0; i < signal.Length; i++)
                output[i] = signal[i] * window[i];

            return output;
        }

        [Obsolete("This method is obsolete. Create a window in the Windows namespace and interact with its methods.")]
        public static void ApplyInPlace(double[] window, double[] signal)
        {
            if (window.Length != signal.Length)
                throw new ArgumentException("window and signal must be same length");

            for (int i = 0; i < signal.Length; i++)
                signal[i] = signal[i] * window[i];
        }

        [Obsolete("Use GetWindows() instead")]
        public static string[] GetWindowNames()
        {
            return typeof(Window)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.ReturnType.Equals(typeof(double[])))
                    .Where(x => x.GetParameters().Length == 1)
                    .Where(x => x.GetParameters()[0].ParameterType == typeof(int))
                    .Select(x => x.Name)
                    .ToArray();
        }

        [Obsolete("Use GetWindows() and work with the output instead")]
        public static double[] GetWindowByName(string windowName, int pointCount)
        {
            MethodInfo[] windowInfos = typeof(Window)
                                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                                .Where(x => x.ReturnType.Equals(typeof(double[])))
                                .Where(x => x.GetParameters().Length == 1)
                                .Where(x => x.GetParameters()[0].ParameterType == typeof(int))
                                .Where(x => x.Name == windowName)
                                .ToArray();

            if (windowInfos.Length == 0)
                throw new ArgumentException($"invalid window name: {windowName}");

            object[] parameters = new object[] { pointCount };
            double[] result = (double[])windowInfos[0].Invoke(null, parameters);
            return result;
        }
    }
}

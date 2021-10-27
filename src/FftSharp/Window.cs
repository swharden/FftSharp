using System;
using System.Linq;
using System.Reflection;

namespace FftSharp
{
    public static class Window
    {
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

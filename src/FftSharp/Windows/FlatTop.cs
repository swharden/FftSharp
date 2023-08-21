using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Windows
{
    public class FlatTop : Window, IWindow
    {
        public override string Name => "Flat Top";
        public override string Description =>
            "A flat top window is a partially negative-valued window that has minimal scalloping loss in the frequency domain. " +
            "These properties are desirable for the measurement of amplitudes of sinusoidal frequency components. " +
            "Drawbacks of the broad bandwidth are poor frequency resolution and high noise bandwidth. " +
            "The flat top window crosses the zero line causing a broader peak in the frequency domain, " +
            "which is closer to the true amplitude of the signal than with other windows";

        public override bool IsSymmetric => true;

        public readonly double A0 = 0.21557895;
        public readonly double A1 = 0.41663158;
        public readonly double A2 = 0.277263158;
        public readonly double A3 = 0.083578947;
        public readonly double A4 = 0.006947368;

        public FlatTop()
        {
        }

        public FlatTop(double a0, double a1, double a2, double a3, double a4)
        {
            A0 = a0;
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }

        public override double[] Create(int size, bool normalize = false)
        {
            double[] window = new double[size];

            for (int i = 0; i < size; i++)
            {
                window[i] = A0
                    - A1 * Math.Cos(2 * Math.PI * i / (size - 1))
                    + A2 * Math.Cos(4 * Math.PI * i / (size - 1))
                    - A3 * Math.Cos(6 * Math.PI * i / (size - 1))
                    + A4 * Math.Cos(8 * Math.PI * i / (size - 1));
            }

            if (normalize)
                NormalizeInPlace(window);

            return window;
        }
    }
}

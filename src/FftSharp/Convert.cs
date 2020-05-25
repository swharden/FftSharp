using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FftSharp
{
    public static class Convert
    {
        public static Complex[] ToComplex(double[] real)
        {
            if (real is null)
                throw new ArgumentException("input cannot be null");

            Complex[] com = new Complex[real.Length];
            for (int i = 0; i < real.Length; i++)
                com[i] = new Complex(real[i], 0);

            return com;
        }

        public static double[] GetReal(Complex[] com)
        {
            if (com is null)
                throw new ArgumentException("input cannot be null");

            double[] values = new double[com.Length];
            for (int i = 0; i < values.Length; i++)
                values[i] = com[i].Real;

            return values;
        }

        public static double[] GetMagnitude(Complex[] com)
        {
            if (com is null)
                throw new ArgumentException("input cannot be null");

            double[] values = new double[com.Length];
            for (int i = 0; i < values.Length; i++)
                values[i] = com[i].Magnitude;

            return values;
        }
    }
}

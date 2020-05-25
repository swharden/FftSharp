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

        public static double[] GetMagnitude(Complex[] com, bool half = false)
        {
            if (com is null)
                throw new ArgumentException("input cannot be null");

            int length = (half) ? com.Length / 2 : com.Length;
            double[] values = new double[length];
            for (int i = 0; i < length; i++)
                values[i] = com[i].Magnitude;
            return values;
        }

        public static double[] ToDecibels(double[] values, double multiplier = 20)
        {
            double[] dB = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
                dB[i] = multiplier * Math.Log10(values[i]);
            return dB;
        }

        public static double[] ToDecibelAmplitude(double[] values)
        {
            return ToDecibels(values, 20);
        }

        public static double[] ToDecibelAmplitude(Complex[] fft)
        {
            return ToDecibelAmplitude(GetMagnitude(fft, half: true));
        }

        public static double[] ToDecibelPower(double[] values)
        {
            return ToDecibels(values, 10);
        }

        public static double[] ToDecibelPower(Complex[] fft)
        {
            return ToDecibelPower(GetMagnitude(fft, half: true));
        }
    }
}

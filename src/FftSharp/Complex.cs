using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp
{
    public struct Complex
    {
        public double Real;
        public double Imaginary;
        public double MagnitudeSquared { get { return Real * Real + Imaginary * Imaginary; } }
        public double Magnitude { get { return Math.Sqrt(MagnitudeSquared); } }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static Complex[] FromReal(double[] real)
        {
            Complex[] complex = new Complex[real.Length];
            for (int i = 0; i < real.Length; i++)
                complex[i].Real = real[i];
            return complex;
        }

        public static double[] GetMagnitudes(Complex[] input)
        {
            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = input[i].Magnitude;
            return output;
        }
    }
}

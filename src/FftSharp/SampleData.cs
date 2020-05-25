using System;
using System.Numerics;

namespace FftSharp
{
    public static class SampleData
    {
        public static double[] OddSines(int pointCount = 128, int sineCount = 2)
        {
            double[] values = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                for (int s = 0; s < sineCount; s++)
                {
                    double mult = 1 + 2 * s;
                    values[i] += 1 / mult * Math.Sin(mult * i / Math.PI);
                }
            }
            return values;
        }
    }
}

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

        public static double[] AddSin(double[] data, int sampleRate, double frequency, double magnitude = 1)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] += Math.Sin(i * frequency / sampleRate * 2 * Math.PI) * magnitude;
            return data;
        }

        public static double[] AddWhiteNoise(double[] data, double magnitude = 1, int? seed = 0)
        {
            Random rand = (seed.HasValue) ? new Random(seed.Value) : new Random();
            for (int i = 0; i < data.Length; i++)
                data[i] += (rand.NextDouble() - .5) * magnitude;
            return data;
        }
    }
}

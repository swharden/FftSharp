using System;
using System.Drawing;
using System.Numerics;

namespace FftSharp
{
    public static class SampleData
    {
        public static double[] Times(int sampleRate, int pointCount)
        {
            double periodSec = 1.0 / sampleRate;
            double[] times = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
                times[i] = i * periodSec;
            return times;
        }

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

        public static void AddSin(double[] data, int sampleRate, double frequency, double magnitude = 1)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] += Math.Sin(i * frequency / sampleRate * 2 * Math.PI) * magnitude;
        }

        public static void AddOffset(double[] data, double offset = 0)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] += offset;
        }

        public static void AddWhiteNoise(double[] data, double magnitude = 1, int? seed = 0, double offset = 0)
        {
            Random rand = (seed.HasValue) ? new Random(seed.Value) : new Random();
            for (int i = 0; i < data.Length; i++)
                data[i] += (rand.NextDouble() - .5) * magnitude + offset;
        }

        public static double[] WhiteNoise(int pointCount, double magnitude = 1, int? seed = 0)
        {
            Random rand = (seed.HasValue) ? new Random(seed.Value) : new Random();
            double[] data = new double[pointCount];
            for (int i = 0; i < data.Length; i++)
                data[i] += (rand.NextDouble() - .5) * magnitude;
            return data;
        }
    }
}

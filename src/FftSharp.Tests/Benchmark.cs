using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FftSharp.Tests
{
    class Benchmark
    {
        [Test]
        public void Test_FFT_BySize()
        {
            Random rand = new Random(0);
            Stopwatch sw = new Stopwatch();

            double[] powers = { 8, 9, 10, 11, 12, 13 };
            double[] fftSizes = powers.Select(x => (double)(1 << (int)x)).ToArray();
            string[] fftLabels = fftSizes.Select(x => x.ToString("N0")).ToArray();
            double[] fftFps = new double[powers.Length];

            for (int i = 0; i < fftSizes.Length; i++)
            {
                int fftSize = (int)fftSizes[i];
                int reps = 3 * 65536 / fftSize;

                // create random input
                Complex[] input = new Complex[fftSize];
                for (int j = 0; j < input.Length; j++)
                    input[j] = new Complex(rand.NextDouble(), rand.NextDouble());

                // benchmark FFT
                sw.Restart();
                for (int j = 0; j < reps; j++)
                {
                    FftSharp.Transform.FFT(input);
                }
                sw.Stop();
                double sec = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                fftFps[i] = reps / sec;
                Console.WriteLine($"FFT size {fftSize}: {fftFps[i]:N2} calculations per second");
            }

            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotScatter(powers, fftFps);
            plt.XTicks(powers, fftLabels);
            plt.YLabel("FFTs / Second");
            plt.XLabel("FFT Size (points)");
            plt.Title("Benchmark Results (FFT)");
            plt.SaveFig("test-benchmark.png");
        }

        [Test]
        public void Test_FFT_VsDFT()
        {
            Random rand = new Random(0);
            Stopwatch sw = new Stopwatch();

            int fftSize = 1024;
            int reps = 10;

            // create random input
            Complex[] input = new Complex[fftSize];
            for (int j = 0; j < input.Length; j++)
                input[j] = new Complex(rand.NextDouble(), rand.NextDouble());

            // benchmark FFT
            sw.Restart();
            for (int j = 0; j < reps; j++)
            {
                FftSharp.Transform.FFT(input);
            }
            sw.Stop();
            double fftMsec = 1000.0 * sw.ElapsedTicks / Stopwatch.Frequency;
            Console.WriteLine($"FFT of {fftSize} points: {fftMsec:N2} msec");

            // benchmark DFT
            sw.Restart();
            for (int j = 0; j < reps; j++)
            {
                FftSharp.Experimental.DFT(input);
            }
            sw.Stop();
            double dftMsec = 1000.0 * sw.ElapsedTicks / Stopwatch.Frequency;
            Console.WriteLine($"DFT of {fftSize} points: {dftMsec:N2} msec");

            // the FFT should be easily 10x faster than the DFT
            Assert.Less(fftMsec * 10, dftMsec);
        }

        [Test]
        public void Test_FFT_FFTfast()
        {
            Random rand = new Random(0);
            Stopwatch sw = new Stopwatch();

            int fftSize = 1024;
            int reps = 100;

            // create random input
            Complex[] input = new Complex[fftSize];
            for (int j = 0; j < input.Length; j++)
                input[j] = new Complex(rand.NextDouble(), rand.NextDouble());

            // benchmark FFT
            sw.Reset();
            for (int j = 0; j < reps; j++)
            {
                sw.Start();
                FftSharp.Transform.FFT(input);
                sw.Stop();
            }
            double fftMsec = 1000.0 * sw.ElapsedTicks / Stopwatch.Frequency;
            Console.WriteLine($"FFT of {fftSize} points: {fftMsec:N2} msec");

            // benchmark FFTfast
            sw.Reset();
            Complex[] input2 = new Complex[input.Length];
            for (int j = 0; j < reps; j++)
            {
                Array.Copy(input, 0, input2, 0, input.Length);
                sw.Start();
                FftSharp.Transform.FFT(input);
                sw.Stop();
            }
            double dftMsec = 1000.0 * sw.ElapsedTicks / Stopwatch.Frequency;
            Console.WriteLine($"FFTfast of {fftSize} points: {dftMsec:N2} msec");

            // the FFT should be easily 10x faster than the DFT
            //Assert.Less(fftMsec * 10, dftMsec);
        }
    }
}

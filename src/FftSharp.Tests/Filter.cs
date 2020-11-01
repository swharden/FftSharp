using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FftSharp.Tests
{
    class Filter
    {
        double[] Values;
        double[] TimesMsec;
        double[] Freqs;

        private const double SampleRate = 48_000;
        private double SamplePeriod => 1.0 / SampleRate;

        [OneTimeSetUp]
        public void LoadAndVerifyData()
        {
            Values = LoadData.Double("sample.txt");
            TimesMsec = ScottPlot.DataGen.Consecutive(Values.Length, SamplePeriod * 1000.0);
            Freqs = LoadData.Double("fftFreq.txt");
        }

        [Test]
        public void Test_Filter_Lowpass()
        {
            Complex[] fft = FftSharp.Transform.FFT(Values);
            double[] fftFreqs = FftSharp.Transform.FFTfreq(SampleRate, fft.Length, oneSided: false);
            double FrequencyLimit = 2000;
            for (int i = 0; i < fft.Length; i++)
            {
                if (Math.Abs(fftFreqs[i]) > FrequencyLimit)
                {
                    fft[i].Real = 0;
                    fft[i].Imaginary = 0;
                }
            }
            FftSharp.Transform.IFFT(fft);
            double[] Filtered = new double[fft.Length];
            for (int i = 0; i < fft.Length; i++)
                Filtered[i] = fft[i].Real;

            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotScatter(TimesMsec, Values, label: "Original");
            plt.PlotScatter(TimesMsec, Filtered, label: "Filtered", lineWidth: 2);
            plt.YLabel("Signal Value");
            plt.XLabel("Time (milliseconds)");
            plt.Legend();
            plt.Axis(x1: 4, x2: 6);

            TestTools.SaveFig(plt);
        }
    }
}

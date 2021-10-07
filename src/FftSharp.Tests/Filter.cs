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
            double[] Filtered = FftSharp.Filter.LowPass(Values, SampleRate, 2000);

            var plt = new ScottPlot.Plot(600, 400);
            plt.AddScatter(TimesMsec, Values, label: "Original");
            plt.AddScatter(TimesMsec, Filtered, label: "Low-Pass", lineWidth: 2);
            plt.YLabel("Signal Value");
            plt.XLabel("Time (milliseconds)");
            plt.Legend();
            plt.SetAxisLimitsX(4, 6);

            TestTools.SaveFig(plt);
        }

        [Test]
        public void Test_Filter_HighPass()
        {
            double[] Filtered = FftSharp.Filter.HighPass(Values, SampleRate, 3000);

            var plt = new ScottPlot.Plot(600, 400);
            plt.AddScatter(TimesMsec, Values, label: "Original");
            plt.AddScatter(TimesMsec, Filtered, label: "High-Pass", lineWidth: 2);
            plt.YLabel("Signal Value");
            plt.XLabel("Time (milliseconds)");
            plt.Legend();
            plt.SetAxisLimitsX(4, 6);

            TestTools.SaveFig(plt);
        }

        [Test]
        public void Test_Filter_BandPass()
        {
            double[] Filtered = FftSharp.Filter.BandPass(Values, SampleRate, 1900, 2100);

            var plt = new ScottPlot.Plot(600, 400);
            plt.AddScatter(TimesMsec, Values, label: "Original");
            plt.AddScatter(TimesMsec, Filtered, label: "Band-Pass", lineWidth: 2);
            plt.YLabel("Signal Value");
            plt.XLabel("Time (milliseconds)");
            plt.Legend();
            plt.SetAxisLimitsX(4, 6);

            TestTools.SaveFig(plt);
        }

        [Test]
        public void Test_Filter_BandStop()
        {
            double[] Filtered = FftSharp.Filter.BandStop(Values, SampleRate, 1900, 2100);

            var plt = new ScottPlot.Plot(600, 400);
            plt.AddScatter(TimesMsec, Values, label: "Original");
            plt.AddScatter(TimesMsec, Filtered, label: "Band-Pass", lineWidth: 2);
            plt.YLabel("Signal Value");
            plt.XLabel("Time (milliseconds)");
            plt.Legend();
            plt.SetAxisLimitsX(4, 6);

            TestTools.SaveFig(plt);
        }
    }
}

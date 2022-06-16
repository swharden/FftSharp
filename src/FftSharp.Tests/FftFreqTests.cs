using NUnit.Framework;
using System;
using System.Linq;

namespace FftSharp.Tests
{
    internal class FftFreqTests
    {
        [Test]
        public void Test_FftFreq_KnownValues()
        {
            // https://github.com/swharden/FftSharp/issues/49

            // generate signal with 2 Hz sine wave
            int sampleCount = 16;
            double sampleRateHz = 16;
            double sinFrequencyHz = 2;
            double[] samples = Enumerable.Range(0, sampleCount)
                .Select(i => Math.Sin(2 * Math.PI * sinFrequencyHz * i / sampleRateHz))
                .ToArray();

            // get FFT
            double[] fft = FftSharp.Transform.FFTmagnitude(samples);
            double[] fftKnown = { 0, 0, 1, 0, 0, 0, 0, 0, 0 };
            Assert.That(fft, Is.EqualTo(fftKnown).Within(1e-10));

            // calculate FFT frequencies both ways
            double[] fftFreq = FftSharp.Transform.FFTfreq(sampleRateHz, fft.Length);
            double[] fftFreq2 = FftSharp.Transform.FFTfreq(sampleRateHz, samples);
            Assert.That(fftFreq2, Is.EqualTo(fftFreq));

            ScottPlot.Plot plt1 = new(400, 200);
            plt1.AddSignal(samples, sampleRateHz);
            TestTools.SaveFig(plt1, "signal");

            ScottPlot.Plot plt2 = new(400, 200);
            plt2.AddScatter(fftFreq, fft);
            plt2.AddVerticalLine(2, System.Drawing.Color.Red, style: ScottPlot.LineStyle.Dash);
            TestTools.SaveFig(plt2, "fft");
        }
    }
}

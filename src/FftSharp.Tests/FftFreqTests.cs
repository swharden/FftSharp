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
            double[] fftFreq2 = FftSharp.Transform.FFTfreq(sampleRateHz, fft);
            Assert.That(fftFreq2, Is.EqualTo(fftFreq));

            ScottPlot.Plot plt1 = new(400, 200);
            plt1.AddSignal(samples, sampleRateHz);
            TestTools.SaveFig(plt1, "signal");

            ScottPlot.Plot plt2 = new(400, 200);
            plt2.AddScatter(fftFreq, fft);
            plt2.AddVerticalLine(2, System.Drawing.Color.Red, style: ScottPlot.LineStyle.Dash);
            TestTools.SaveFig(plt2, "fft");
        }

        [Test]
        public void Test_Freq_Lookup()
        {
            /* Compare against values generated using Python:
             * 
             *   >>> numpy.fft.fftfreq(16, 1/16000)
             *   array([    0.,  1000.,  2000.,  3000.,  4000.,  5000.,  6000.,  7000.,
             *          -8000., -7000., -6000., -5000., -4000., -3000., -2000., -1000.])
             * 
             */

            int sampleRate = 16000;
            int pointCount = 16;
            double[] signal = new double[pointCount];
            double[] fft = FftSharp.Transform.FFTmagnitude(signal);

            double[] freqsFullKnown = {
                0,  1000,  2000,  3000,  4000,  5000,  6000,  7000,
                -8000, -7000, -6000, -5000, -4000, -3000, -2000, -1000
            };
            double[] freqsFull = FftSharp.Transform.FFTfreq(sampleRate, signal, oneSided: false);
            double[] freqsFull2 = FftSharp.Transform.FFTfreq(sampleRate, signal.Length, oneSided: false);
            Assert.That(freqsFull, Is.EqualTo(freqsFullKnown));
            Assert.That(freqsFull2, Is.EqualTo(freqsFullKnown));

            double[] freqsHalfKnown = {
                0, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000
            };
            double[] freqsHalf = FftSharp.Transform.FFTfreq(sampleRate, fft, oneSided: true);
            double[] freqsHalf2 = FftSharp.Transform.FFTfreq(sampleRate, fft.Length, oneSided: true);
            Console.WriteLine(String.Join(",", freqsHalf2.Select(x => x.ToString())));
            Assert.That(freqsHalf, Is.EqualTo(freqsHalfKnown));
            Assert.That(freqsHalf2, Is.EqualTo(freqsHalfKnown));

        }
    }
}

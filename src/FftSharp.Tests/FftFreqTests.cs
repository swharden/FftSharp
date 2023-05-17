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
            System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(samples);
            double[] fft = FftSharp.FFT.Magnitude(spectrum);
            double[] fftKnown = { 0, 0, 1, 0, 0, 0, 0, 0, 0 };
            Assert.That(fft, Is.EqualTo(fftKnown).Within(1e-10));

            // calculate FFT frequencies both ways
            double[] fftFreqKnown = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            double[] fftFreq = FftSharp.FFT.FrequencyScale(fft.Length, sampleRateHz);
            Assert.That(fftFreq, Is.EqualTo(fftFreqKnown));

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

            System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);
            double[] fft = FftSharp.FFT.Magnitude(spectrum);

            double[] freqsFullKnown = {
                0,  1000,  2000,  3000,  4000,  5000,  6000,  7000,
                -8000, -7000, -6000, -5000, -4000, -3000, -2000, -1000
            };
            double[] freqsFull = FftSharp.FFT.FrequencyScale(signal.Length, sampleRate, false);
            Assert.That(freqsFull, Is.EqualTo(freqsFullKnown));

            double[] freqsHalfKnown = {
                0, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000
            };
            double[] freqsHalf = FftSharp.FFT.FrequencyScale(fft.Length, sampleRate, true);
            Assert.That(freqsHalf, Is.EqualTo(freqsHalfKnown));
        }
    }
}

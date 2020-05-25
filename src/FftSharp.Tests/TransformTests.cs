using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace FftSharp.Tests
{
    public class TransformTests
    {
        int sampleRate = 48_000;
        int pointCount = 512;
        double[] audio;

        [SetUp]
        public void MakeSampleAudio()
        {
            // create noisy audio with multiple sine waves NOT centered at 0
            audio = new double[pointCount];
            SampleData.AddWhiteNoise(audio, offset: .1);
            SampleData.AddSin(audio, sampleRate, 2_000, 2);
            SampleData.AddSin(audio, sampleRate, 10_000, 1);
            SampleData.AddSin(audio, sampleRate, 20_000, .5);
        }

        [Test]
        public void Test_DFTvsFFT_ProduceIdenticalOutput()
        {
            // FFT and DST output should be identical (aside from floating-point errors)
            Complex[] fft = Transform.FFT(audio);
            Complex[] dft = Transform.DFT(audio);

            for (int i = 0; i < fft.Length; i++)
            {
                Assert.AreEqual(fft[i].Real, dft[i].Real, 1e-6);
                Assert.AreEqual(fft[i].Imaginary, dft[i].Imaginary, 1e-6);
                Assert.AreEqual(fft[i].Magnitude, dft[i].Magnitude, 1e-6);
            }
        }

        [Test]
        public void Test_Inspect_PosNeg()
        {
            Complex[] fft = Transform.FFT(audio);

            double[] fftAmp = fft.Select(x => x.Magnitude).ToArray();
            double[] fftFreq = Transform.FFTfreq(sampleRate, fftAmp.Length, mirror: true);

            var plt = new ScottPlot.Plot(600, 400);

            for (int i = 0; i < fftAmp.Length; i++)
                plt.PlotLine(fftFreq[i], 0, fftFreq[i], fftAmp[i], Color.Gray);
            plt.PlotScatter(fftFreq, fftAmp, Color.Blue, 0);

            plt.PlotHLine(0, color: Color.Black, lineWidth: 2);
            plt.YLabel("Magnitude (rms^2?)");
            plt.XLabel("Frequency (Hz)");
            plt.SaveFig("test-symmetry.png");
        }

        [Test]
        public void Test_PosNeg_AreSymmetical()
        {
            // FFT produce positive/negative values that are exactly symmetrical

            Complex[] fft = Transform.FFT(audio);
            double[] fftAmp = fft.Select(x => x.Magnitude).ToArray();
            int halfLength = fftAmp.Length / 2;

            // create arrays which isolate positive and negative components
            double[] fftFreq = Transform.FFTfreq(sampleRate, halfLength);
            double[] fftAmpNeg = new double[halfLength];
            double[] fftAmpPos = new double[halfLength];
            for (int i = 0; i < halfLength; i++)
            {
                fftAmpNeg[halfLength - i - 1] = fftAmp[halfLength - i - 1];
                fftAmpPos[i] = fftAmp[i];
            }

            // negative and positive magnitudes will always be equal
            Assert.AreEqual(fftAmpNeg, fftAmpPos);

            // plot these findings
            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotScatter(fftFreq, fftAmpPos, Color.Blue, 0, label: "negative");
            plt.PlotScatter(fftFreq, fftAmpNeg, Color.Red, 0, 10,
                markerShape: ScottPlot.MarkerShape.openCircle, label: "positive");
            plt.Legend(location: ScottPlot.legendLocation.upperRight);
            plt.PlotHLine(0, color: Color.Black, lineWidth: 2);
            plt.YLabel("Magnitude (rms^2?)");
            plt.XLabel("Frequency (Hz)");
            plt.SaveFig("test-negpos.png");
        }
    }
}
using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace FftSharp.Tests
{
    public class TransformTests
    {
        [Test]
        public void Test_Display_SampleData()
        {
            double[] audio = SampleData.SampleAudio1();

            // use this to test FFT algos in other programming langauges
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < audio.Length; i++)
            {
                if (i % 16 == 0)
                    sb.Append("\n");
                double value = audio[i];
                string s = (value < 0) ? $"{value:N2}, " : $"+{value:N2}, ";
                sb.Append(s);
            }
            Console.WriteLine(sb);
        }

        [Test]
        public void Test_DFTvsFFT_ProduceIdenticalOutput()
        {
            double[] audio = SampleData.SampleAudio1();

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
            double[] audio = SampleData.SampleAudio1();
            int sampleRate = 48_000;

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
            double[] audio = SampleData.SampleAudio1();
            int sampleRate = 48_000;

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
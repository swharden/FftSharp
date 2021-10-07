using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FftSharp.Tests
{
    public class Transform
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
            Complex[] fft = FftSharp.Transform.FFT(audio);
            Complex[] dft = FftSharp.Experimental.DFT(audio);

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

            Complex[] fft = FftSharp.Transform.FFT(audio);

            double[] fftAmp = fft.Select(x => x.Magnitude).ToArray();
            double[] fftFreq = FftSharp.Transform.FFTfreq(sampleRate, fftAmp.Length, oneSided: true);

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
            Complex[] fft = FftSharp.Transform.FFT(audio);
            double[] fftAmp = fft.Select(x => x.Magnitude).ToArray();
            int halfLength = fftAmp.Length / 2;

            // create arrays which isolate positive and negative components
            double[] fftFreq = FftSharp.Transform.FFTfreq(sampleRate, halfLength);
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
            plt.Legend(location: ScottPlot.Alignment.UpperRight);
            plt.PlotHLine(0, color: Color.Black, lineWidth: 2);
            plt.YLabel("Magnitude (rms^2?)");
            plt.XLabel("Frequency (Hz)");
            plt.SaveFig("test-negpos.png");
        }

        [Test]
        public void Test_FftHelperMethods_ThrowIfNotPowerOfTwo()
        {
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFT(new double[0]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFT(new double[123]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFT(new double[1234]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTmagnitude(new double[0]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTmagnitude(new double[123]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTmagnitude(new double[1234]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTpower(new double[0]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTpower(new double[123]); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTpower(new double[1234]); });
        }

        [Test]
        public void Test_FftInput_ContainsAllZeros()
        {
            Complex[] complex = new Complex[128];
            for (int i = 0; i < complex.Length; i++)
                complex[i] = new Complex(0, 0);
            FftSharp.Experimental.DFT(complex);
        }

        [Test]
        public void Test_FftInput_Uninitialized()
        {
            Complex[] complex = new Complex[128];
            FftSharp.Experimental.DFT(complex);
        }

        [Test]
        public void Test_FftInput_ThrowsIfNull()
        {
            Complex[] complexValues = null;
            double[] realValues = null;
            Assert.Throws<ArgumentNullException>(() => { FftSharp.Transform.FFT(complexValues); });
            Assert.Throws<ArgumentNullException>(() => { FftSharp.Transform.IFFT(complexValues); });
            Assert.Throws<ArgumentNullException>(() => { FftSharp.Transform.FFT(realValues); });
            Assert.Throws<ArgumentNullException>(() => { FftSharp.Transform.RFFT(realValues); });
        }

        [Test]
        public void Test_FftInput_ThrowsIfEmpty()
        {
            Complex[] complexValues = new Complex[0];
            double[] realValues = new double[0];
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFT(complexValues); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.IFFT(complexValues); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFT(realValues); });
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.RFFT(realValues); });
        }

        [TestCase(123, true)]
        [TestCase(13, true)]
        [TestCase(128, false)]
        [TestCase(16, false)]
        public void Test_FftInput_ThrowsIfLengthIsNotPowerOfTwo(int length, bool shouldThrow)
        {
            Complex[] complexValues = new Complex[length];
            double[] realValues = new double[length];

            var complexFFT = new TestDelegate(() => FftSharp.Transform.FFT(complexValues));
            var complexIFFT = new TestDelegate(() => FftSharp.Transform.IFFT(complexValues));
            var realFFT = new TestDelegate(() => FftSharp.Transform.FFT(realValues));
            var realRFFT = new TestDelegate(() => FftSharp.Transform.RFFT(realValues));

            if (shouldThrow)
            {
                Assert.Throws<ArgumentException>(complexFFT);
                Assert.Throws<ArgumentException>(complexIFFT);
                Assert.Throws<ArgumentException>(realFFT);
                Assert.Throws<ArgumentException>(realRFFT);
            }
            else
            {
                Assert.DoesNotThrow(complexFFT);
                Assert.DoesNotThrow(complexIFFT);
                Assert.DoesNotThrow(realFFT);
                Assert.DoesNotThrow(realRFFT);
            }
        }
    }
}
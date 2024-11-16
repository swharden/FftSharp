using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FftSharp.Tests;

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
        System.Numerics.Complex[] fft = FftSharp.FFT.Forward(audio);
        System.Numerics.Complex[] dft = FftSharp.Experimental.DFT(audio);

        for (int i = 0; i < fft.Length; i++)
        {
            Assert.AreEqual(fft[i].Real, dft[i].Real, 1e-6);
            Assert.AreEqual(fft[i].Imaginary, dft[i].Imaginary, 1e-6);
            Assert.AreEqual(fft[i].Magnitude, dft[i].Magnitude, 1e-6);
        }
    }

    [Test]
    public void Test_PosNeg_AreMirrored()
    {
        double[] audio = SampleData.SampleAudio1();
        int sampleRate = 48_000;

        Complex[] fft = FftSharp.FFT.Forward(audio);
        double[] fftAmp = fft.Select(x => x.Magnitude).ToArray();
        double[] fftFreq = FftSharp.FFT.FrequencyScale(fftAmp.Length, sampleRate, positiveOnly: false);

        TestTools.AssertMirror(fftAmp);

        var plt = new ScottPlot.Plot(600, 400);
        plt.AddLollipop(fftAmp, fftFreq);
        plt.PlotHLine(0, color: Color.Black, lineWidth: 2);
        plt.YLabel("Magnitude (rms^2?)");
        plt.XLabel("Frequency (Hz)");
        TestTools.SaveFig(plt);
    }

    [Test]
    public void Test_FftHelperMethods_ThrowIfNotPowerOfTwo()
    {
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Forward(new double[0]); });
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Forward(new double[123]); });
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Forward(new double[1234]); });
    }

    [Test]
    public void Test_FftInput_ContainsAllZeros()
    {
        System.Numerics.Complex[] complex = new System.Numerics.Complex[128];
        for (int i = 0; i < complex.Length; i++)
            complex[i] = new System.Numerics.Complex(0, 0);
        FftSharp.Experimental.DFT(complex);
    }

    [Test]
    public void Test_FftInput_Uninitialized()
    {
        System.Numerics.Complex[] complex = new System.Numerics.Complex[128];
        FftSharp.Experimental.DFT(complex);
    }

    [Test]
    public void Test_FftInput_ThrowsIfEmpty()
    {
        Complex[] complexValues = new Complex[0];
        double[] realValues = new double[0];
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Forward(complexValues); });
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Inverse(complexValues); });
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.Forward(realValues); });
        Assert.Throws<ArgumentException>(() => { FftSharp.FFT.InverseReal(complexValues); });
    }

    [TestCase(123, true)]
    [TestCase(13, true)]
    [TestCase(128, false)]
    [TestCase(16, false)]
    public void Test_FftInput_ThrowsIfLengthIsNotPowerOfTwo(int length, bool shouldThrow)
    {
        Complex[] complexValues = new Complex[length];
        double[] realValues = new double[length];
        Complex[] destination = new Complex[length / 2 + 1];

        var complexFFT = new TestDelegate(() => FftSharp.FFT.Forward(complexValues));
        var complexSpanFFT = new TestDelegate(() => FftSharp.FFT.Forward(complexValues.AsSpan()));
        var complexIFFT = new TestDelegate(() => FftSharp.FFT.Inverse(complexValues));
        var realFFT = new TestDelegate(() => FftSharp.FFT.Forward(realValues));
        var realRFFT = new TestDelegate(() => FftSharp.FFT.ForwardReal(realValues));

        if (shouldThrow)
        {
            Assert.Throws<ArgumentException>(complexFFT);
            Assert.Throws<ArgumentException>(complexSpanFFT);
            Assert.Throws<ArgumentException>(complexIFFT);
            Assert.Throws<ArgumentException>(realFFT);
            Assert.Throws<ArgumentException>(realRFFT);
        }
        else
        {
            Assert.DoesNotThrow(complexFFT);
            Assert.DoesNotThrow(complexSpanFFT);
            Assert.DoesNotThrow(complexIFFT);
            Assert.DoesNotThrow(realFFT);
            Assert.DoesNotThrow(realRFFT);
        }
    }

    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(2, false)]
    [TestCase(4, false)]
    [TestCase(8, false)]
    [TestCase(16, false)]
    [TestCase(32, false)]
    [TestCase(64, false)]
    public void Test_Fft_Magnitude_DifferentLengths(int pointCount, bool shouldFail)
    {
        double[] signal = new double[pointCount];
        if (shouldFail)
        {
            Assert.Throws<ArgumentException>(() => FftSharp.FFT.ForwardReal(signal));
        }
        else
        {
            Assert.DoesNotThrow(() => FftSharp.FFT.ForwardReal(signal));
        }
    }
}
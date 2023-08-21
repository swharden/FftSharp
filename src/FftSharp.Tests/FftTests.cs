using NUnit.Framework;

namespace FftSharp.Tests;

internal class FftTests
{
    [Test]
    public void Test_FFT_Forward()
    {
        double[] sample = LoadData.Double("sample.txt");
        System.Numerics.Complex[] fft = FftSharp.FFT.Forward(sample);

        System.Numerics.Complex[] numpyFft = LoadData.Complex("fft.txt");
        Assert.AreEqual(numpyFft.Length, fft.Length);

        for (int i = 0; i < fft.Length; i++)
        {
            Assert.AreEqual(numpyFft[i].Real, fft[i].Real, 1e-10);
            Assert.AreEqual(numpyFft[i].Imaginary, fft[i].Imaginary, 1e-10);
        }
    }

    [Test]
    public void Test_FFT_Inverse()
    {
        double[] sample = LoadData.Double("sample.txt");
        System.Numerics.Complex[] fft = FftSharp.FFT.Forward(sample);
        FftSharp.FFT.Inverse(fft);

        for (int i = 0; i < fft.Length; i++)
        {
            Assert.AreEqual(sample[i], fft[i].Real, 1e-10);
        }
    }

    [Test]
    public void Test_FFT_FrequencyScale()
    {
        double[] numpyFftFreq = LoadData.Double("fftFreq.txt");
        double[] fftFreq = FftSharp.FFT.FrequencyScale(length: numpyFftFreq.Length, sampleRate: 48_000);

        Assert.AreEqual(numpyFftFreq.Length, fftFreq.Length);

        for (int i = 0; i < fftFreq.Length; i++)
        {
            Assert.AreEqual(fftFreq[i], fftFreq[i], 1e-10);
        }
    }

    [Test]
    public void Test_FftShift_Odd()
    {
        double[] values = { 0, 1, 2, 3, 4, 5, 6 };
        double[] shifted = FFT.FftShift(values);
        double[] expectedResult = { 4, 5, 6, 0, 1, 2, 3 };
        Assert.AreEqual(expectedResult, shifted);
    }

    [Test]
    public void Test_FftShift_Even()
    {
        double[] values = { 0, 1, 2, 3, 4, 5, 6, 7 };
        double[] shifted = FFT.FftShift(values);
        double[] expectedResult = { 4, 5, 6, 7, 0, 1, 2, 3 };
        Assert.AreEqual(expectedResult, shifted);
    }
}

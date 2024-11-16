using NUnit.Framework;
using System.Linq;

namespace FftSharp.Tests;

class MelTests
{
    [Test]
    public void Test_Mel_VsFFT()
    {
        double[] audio = SampleData.SampleAudio1();
        int sampleRate = 48_000;
        int melBinCount = 20;

        System.Numerics.Complex[] spectrum = FFT.Forward(audio);
        double[] fftMag = FftSharp.FFT.Magnitude(spectrum);
        double[] fftMel = FftSharp.Mel.Scale(fftMag, sampleRate, melBinCount);
        double[] freqMel = FFT.FrequencyScale(fftMag.Length, sampleRate)
            .Select(x => Mel.FromFreq(x))
            .ToArray();

        //ScottPlot.MultiPlot mp = new();
        //plt.SaveFig("audio-mel.png");
    }
}

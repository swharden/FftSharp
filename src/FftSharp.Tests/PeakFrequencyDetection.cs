using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FftSharp.Tests;

internal class PeakFrequencyDetection
{
    [Test]
    public void Test_PeakFrequency_MatchesExpectation()
    {
        double sampleRate = 48_000;

        double[] signal = FftSharp.SampleData.SampleAudio1(); // 2 kHz peak frequency
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);
        double[] fftMag = FftSharp.FFT.Magnitude(spectrum);
        double[] fftFreq = FftSharp.FFT.FrequencyScale(fftMag.Length, sampleRate);

        int peakIndex = 0;
        double peakValue = fftMag[0];
        for (int i = 1; i < fftFreq.Length; i++)
        {
            if (fftMag[i] > peakValue)
            {
                peakValue = fftMag[i];
                peakIndex = i;
            }
        }

        double peakFrequency = fftFreq[peakIndex];
        Console.WriteLine($"Peak frequency: {peakFrequency} Hz");

        Assert.AreEqual(1968.75, peakFrequency);
    }
}

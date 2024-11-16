using NUnit.Framework;
using System;
using System.IO;

namespace FftSharp.Tests;

class Quickstart
{
    public static string OUTPUT_FOLDER = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../../dev/quickstart/"));

    [Test]
    public static void Test_OutputFolder_Exists()
    {
        Console.WriteLine(OUTPUT_FOLDER);
        Assert.That(Directory.Exists(OUTPUT_FOLDER), $"output folder does not exist: {OUTPUT_FOLDER}");
    }

    [TestCase(true)]
    [TestCase(false)]
    public static void Test_SimpleFftWithGraphs(bool useWindow)
    {
        // load sample audio with noise and sine waves at 500, 1200, and 1500 Hz
        double[] audio = FftSharp.SampleData.SampleAudio1();
        int sampleRate = 48_000;

        // optionally apply a window to the data before calculating the FFT
        if (useWindow)
        {
            var window = new FftSharp.Windows.Hanning();
            window.ApplyInPlace(audio);
        }

        // You could get the FFT as a complex result
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(audio);

        // For audio we typically want the FFT amplitude (in dB)
        double[] fftPower = FftSharp.FFT.Power(spectrum);

        // Create an array of frequencies for each point of the FFT
        double[] freqs = FftSharp.FFT.FrequencyScale(fftPower.Length, sampleRate);

        // create an array of audio sample times to aid plotting
        double[] times = ScottPlot.Generate.Consecutive(audio.Length, 1000d / sampleRate);

        // plot the sample audio
        var plt1 = new ScottPlot.Plot();
        var sp1 = plt1.Add.Scatter(times, audio);
        sp1.MarkerSize = 3;
        plt1.YLabel("Amplitude");
        plt1.XLabel("Time (ms)");
        plt1.Axes.TightMargins();

        // plot the FFT amplitude
        var plt2 = new ScottPlot.Plot();
        var sp2 = plt2.Add.Scatter(freqs, fftPower);
        sp2.MarkerSize = 3;
        plt2.YLabel("Power (dB)");
        plt2.XLabel("Frequency (Hz)");
        plt2.Axes.TightMargins();

        // save output
        if (useWindow)
        {
            plt1.SavePng(Path.Combine(OUTPUT_FOLDER, "audio-windowed.png"), 400, 300);
            plt2.SavePng(Path.Combine(OUTPUT_FOLDER, "fft-windowed.png"), 400, 300);
        }
        else
        {
            plt1.SavePng(Path.Combine(OUTPUT_FOLDER, "audio.png"), 400, 300);
            plt2.SavePng(Path.Combine(OUTPUT_FOLDER, "fft.png"), 400, 300);
        }
    }
}

using NUnit.Framework;
using ScottPlot;
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

    [Test]
    public void Test_ExampleCode_Quickstart()
    {
        // Begin with an array containing sample data
        double[] signal = FftSharp.SampleData.SampleAudio1();

        // Shape the signal using a Hanning window
        var window = new FftSharp.Windows.Hanning();
        window.ApplyInPlace(signal);

        // Calculate the FFT as an array of complex numbers
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);

        // or get the magnitude (units²) or power (dB) as real numbers
        double[] magnitude = FftSharp.FFT.Magnitude(spectrum);
        double[] power = FftSharp.FFT.Power(spectrum);

        // plot the result
        ScottPlot.Plot plot = new();
        plot.Add.Signal(power);
        plot.SavePng("quickstart.png", 600, 400).ConsoleWritePath();
    }

    [Test]
    public void Test_ExampleCode_SampleData()
    {
        // sample audio with tones at 2, 10, and 20 kHz plus white noise
        double[] signal = FftSharp.SampleData.SampleAudio1();
        int sampleRate = 48_000;
        double samplePeriod = sampleRate / 1000.0;

        // plot the sample audio
        ScottPlot.Plot plt = new();
        plt.Add.Signal(signal, samplePeriod);
        plt.YLabel("Amplitude");
        plt.SavePng("time-series.png", 500, 200).ConsoleWritePath();
    }

    [Test]
    public void Test_ExampleCode_SpectralDensity()
    {
        // sample audio with tones at 2, 10, and 20 kHz plus white noise
        double[] signal = FftSharp.SampleData.SampleAudio1();
        int sampleRate = 48_000;

        // calculate the power spectral density using FFT
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);
        double[] psd = FftSharp.FFT.Power(spectrum);
        double[] freq = FftSharp.FFT.FrequencyScale(psd.Length, sampleRate);

        // plot the sample audio
        ScottPlot.Plot plt = new();
        plt.Add.ScatterLine(freq, psd);
        plt.YLabel("Power (dB)");
        plt.XLabel("Frequency (Hz)");
        plt.SavePng("periodogram.png", 500, 200).ConsoleWritePath();
    }

    [Test]
    public void Test_ExampleCode_Filtering()
    {
        System.Numerics.Complex[] buffer =
        {
            new(real: 42, imaginary: 12),
            new(real: 96, imaginary: 34),
            new(real: 13, imaginary: 56),
            new(real: 99, imaginary: 78),
        };

        FftSharp.FFT.Forward(buffer);

        double[] audio = FftSharp.SampleData.SampleAudio1();
        double[] filtered = FftSharp.Filter.LowPass(audio, sampleRate: 48000, maxFrequency: 2000);
    }
}

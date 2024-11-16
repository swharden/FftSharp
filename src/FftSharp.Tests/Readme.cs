using NUnit.Framework;
using System.IO;

namespace FftSharp.Tests;

internal class Readme
{
    public static string OUTPUT_FOLDER = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../../dev/quickstart/"));

    [Test]
    public void Test_Readme_Quickstart()
    {
        // Begin with an array containing sample data
        double[] signal = FftSharp.SampleData.SampleAudio1();

        // Shape the signal using a Hanning window
        var window = new FftSharp.Windows.Hanning();
        window.ApplyInPlace(signal);

        // Calculate the FFT as an array of complex numbers
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);

        // Or get the spectral power (dB) or magnitude (RMS²) as real numbers
        double[] fftPower = FftSharp.FFT.Power(spectrum);
        double[] fftMagnitude = FftSharp.FFT.Magnitude(spectrum);
    }

    [Test]
    public void Test_Plot_TimeSeries()
    {
        // sample audio with tones at 2, 10, and 20 kHz plus white noise
        double[] signal = FftSharp.SampleData.SampleAudio1();
        int sampleRate = 48_000;

        // plot the sample audio
        var plt = new ScottPlot.Plot();
        plt.Add.Signal(signal, 1.0 / (sampleRate / 1000.0));
        plt.YLabel("Amplitude");
        plt.Axes.TightMargins();

        plt.SavePng(Path.Combine(OUTPUT_FOLDER, "time-series.png"), 400, 200);
    }

    [Test]
    public void Test_Plot_MagnitudePowerFreq()
    {
        // sample audio with tones at 2, 10, and 20 kHz plus white noise
        double[] signal = FftSharp.SampleData.SampleAudio1();
        int sampleRate = 48_000;

        // calculate the power spectral density using FFT
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);
        double[] psd = FftSharp.FFT.Power(spectrum);
        double[] freq = FftSharp.FFT.FrequencyScale(psd.Length, sampleRate);

        // plot the sample audio
        var plt = new ScottPlot.Plot();
        plt.Add.ScatterLine(freq, psd);
        plt.YLabel("Power (dB)");
        plt.XLabel("Frequency (Hz)");
        plt.Axes.TightMargins();

        plt.SavePng(Path.Combine(OUTPUT_FOLDER, "periodogram.png"), 400, 200);
    }

    [Test]
    public void Test_Window()
    {
        double[] signal = FftSharp.SampleData.SampleAudio1();
        var window = new FftSharp.Windows.Hanning();
        double[] windowed = window.Apply(signal);
    }
}

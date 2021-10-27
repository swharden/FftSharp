using NUnit.Framework;
using System.IO;

namespace FftSharp.Tests
{
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
            Complex[] fft = FftSharp.Transform.FFT(signal);

            // Or get the spectral power (dB) or magnitude (RMS²) as real numbers
            double[] fftPwr = FftSharp.Transform.FFTpower(signal);
            double[] fftMag = FftSharp.Transform.FFTmagnitude(signal);
        }

        [Test]
        public void Test_Plot_TimeSeries()
        {
            // sample audio with tones at 2, 10, and 20 kHz plus white noise
            double[] signal = FftSharp.SampleData.SampleAudio1();
            int sampleRate = 48_000;

            // plot the sample audio
            var plt = new ScottPlot.Plot(400, 200);
            plt.AddSignal(signal, sampleRate / 1000.0);
            plt.YLabel("Amplitude");
            plt.Margins(0);

            plt.SaveFig(Path.Combine(OUTPUT_FOLDER, "time-series.png"));
        }

        [Test]
        public void Test_Plot_MagnitudePowerFreq()
        {
            // sample audio with tones at 2, 10, and 20 kHz plus white noise
            double[] signal = FftSharp.SampleData.SampleAudio1();
            int sampleRate = 48_000;

            // calculate the power spectral density using FFT
            double[] psd = FftSharp.Transform.FFTpower(signal);
            double[] freq = FftSharp.Transform.FFTfreq(sampleRate, psd.Length);

            // plot the sample audio
            var plt = new ScottPlot.Plot(400, 200);
            plt.AddScatterLines(freq, psd);
            plt.YLabel("Power (dB)");
            plt.XLabel("Frequency (Hz)");
            plt.Margins(0);

            plt.SaveFig(Path.Combine(OUTPUT_FOLDER, "periodogram.png"));
        }

        [Test]
        public void Test_Complex()
        {
            Complex[] buffer =
            {
                new Complex(42, 0),
                new Complex(96, 0),
                new Complex(13, 0),
                new Complex(99, 0),
            };

            FftSharp.Transform.FFT(buffer);
        }

        [Test]
        public void Test_Window()
        {
            double[] signal = FftSharp.SampleData.SampleAudio1();
            var window = new FftSharp.Windows.Hanning();
            double[] windowed = window.Apply(signal);
        }
    }
}

using NUnit.Framework;
using System;
using System.IO;

namespace FftSharp.Tests
{
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
            Complex[] fft = FftSharp.Transform.FFT(audio);

            // For audio we typically want the FFT amplitude (in dB)
            double[] fftPower = FftSharp.Transform.FFTpower(audio);

            // Create an array of frequencies for each point of the FFT
            double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftPower.Length);

            // create an array of audio sample times to aid plotting
            double[] times = ScottPlot.DataGen.Consecutive(audio.Length, 1000d / sampleRate);

            // plot the sample audio
            var plt1 = new ScottPlot.Plot(400, 300);
            plt1.AddScatter(times, audio, markerSize: 3);
            plt1.YLabel("Amplitude");
            plt1.XLabel("Time (ms)");
            plt1.AxisAuto(0);

            // plot the FFT amplitude
            var plt2 = new ScottPlot.Plot(400, 300);
            plt2.AddScatter(freqs, fftPower, markerSize: 3);
            plt2.YLabel("Power (dB)");
            plt2.XLabel("Frequency (Hz)");
            plt2.AxisAuto(0);

            // save output
            if (useWindow)
            {
                plt1.SaveFig(Path.Combine(OUTPUT_FOLDER, "audio-windowed.png"));
                plt2.SaveFig(Path.Combine(OUTPUT_FOLDER, "fft-windowed.png"));
            }
            else
            {
                plt1.SaveFig(Path.Combine(OUTPUT_FOLDER, "audio.png"));
                plt2.SaveFig(Path.Combine(OUTPUT_FOLDER, "fft.png"));
            }
        }
    }
}

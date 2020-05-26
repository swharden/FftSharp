using System;
using System.Drawing.Printing;

namespace FftSharp.Quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleFftWithGraphs(useWindow: false);
            SimpleFftWithGraphs(useWindow: true);
        }

        static void SimpleFftWithGraphs(bool useWindow = false)
        {
            // load sample audio with noise and sine waves at 500, 1200, and 1500 Hz
            double[] audio = FftSharp.SampleData.SampleAudio1();
            int sampleRate = 48_000;

            // optionally apply a window to the data before calculating the FFT
            if (useWindow)
            {
                double[] window = FftSharp.Window.Hanning(audio.Length);
                FftSharp.Window.ApplyInPlace(window, audio);
            }

            // You could get the FFT as a complex result
            System.Numerics.Complex[] fft = FftSharp.Transform.FFT(audio);

            // For audio we typically want the FFT amplitude (in dB)
            double[] fftPower = FftSharp.Transform.FFTpower(audio);

            // Create an array of frequencies for each point of the FFT
            double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftPower.Length);

            // create an array of audio sample times to aid plotting
            double[] times = ScottPlot.DataGen.Consecutive(audio.Length, 1000d / sampleRate);

            // plot the sample audio
            var plt1 = new ScottPlot.Plot(400, 300);
            plt1.PlotScatter(times, audio, markerSize: 3);
            //plt1.Title("Audio Signal");
            plt1.YLabel("Amplitude");
            plt1.XLabel("Time (ms)");
            plt1.AxisAuto(0);

            // plot the FFT amplitude
            var plt2 = new ScottPlot.Plot(400, 300);
            plt2.PlotScatter(freqs, fftPower, markerSize: 3);
            //plt2.Title("Fast Fourier Transformation (FFT)");
            plt2.YLabel("Power (dB)");
            plt2.XLabel("Frequency (Hz)");
            plt2.AxisAuto(0);

            // save output
            if (useWindow)
            {
                plt1.SaveFig("../../../output/audio-windowed.png");
                plt2.SaveFig("../../../output/fft-windowed.png");
            }
            else
            {
                plt1.SaveFig("../../../output/audio.png");
                plt2.SaveFig("../../../output/fft.png");
            }
        }

        static void ShowAllWindows()
        {
            var plt = new ScottPlot.Plot();
            double[] xs = ScottPlot.DataGen.Range(-1, 1, .01, true);
            plt.PlotScatter(xs, FftSharp.Window.Hanning(xs.Length), label: "Hanning");
            plt.PlotScatter(xs, FftSharp.Window.Hamming(xs.Length), label: "Hamming");
            plt.PlotScatter(xs, FftSharp.Window.Blackman(xs.Length), label: "Blackman");
            plt.PlotScatter(xs, FftSharp.Window.BlackmanExact(xs.Length), label: "BlackmanExact");
            plt.PlotScatter(xs, FftSharp.Window.BlackmanHarris(xs.Length), label: "BlackmanHarris");
            plt.PlotScatter(xs, FftSharp.Window.FlatTop(xs.Length), label: "FlatTop");
            plt.PlotScatter(xs, FftSharp.Window.Bartlett(xs.Length), label: "Bartlett");

            // customize line styles post-hoc
            foreach (var p in plt.GetPlottables())
            {
                if (p is ScottPlot.PlottableScatter)
                {
                    ((ScottPlot.PlottableScatter)p).markerSize = 0;
                    ((ScottPlot.PlottableScatter)p).lineWidth = 4;
                }
            }

            plt.Legend(location: ScottPlot.legendLocation.upperRight);
            plt.SaveFig("../test-windows.png");
        }
    }
}

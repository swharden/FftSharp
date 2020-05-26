using System;
using System.Drawing.Printing;

namespace FftSharp.Quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            // load sample audio with noise and sine waves at 500, 1200, and 1500 Hz
            double[] audio = FftSharp.SampleData.SampleAudio1();
            int sampleRate = 48_000;

            // You could get the FFT as a complex result
            System.Numerics.Complex[] fft = FftSharp.Transform.FFT(audio);

            // For audio we typically want the FFT amplitude (in dB)
            double[] fftPower = FftSharp.Transform.FFTpower(audio);

            // Create an array of frequencies for each point of the FFT
            double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftPower.Length);

            // create an array of audio sample times to aid plotting
            double[] times = FftSharp.SampleData.Times(sampleRate, audio.Length);

            // plot the sample audio
            var plt1 = new ScottPlot.Plot(600, 300);
            plt1.PlotScatter(times, audio, markerSize: 3);
            plt1.Title("Audio Signal");
            plt1.YLabel("Amplitude");
            plt1.XLabel("Time (milliseconds)");
            plt1.AxisAuto(0);
            plt1.SaveFig("../../../output/audio.png");

            // plot the FFT amplitude
            var plt2 = new ScottPlot.Plot(600, 300);
            plt2.PlotScatter(freqs, fftPower, markerSize: 3);
            plt2.Title("Fast Fourier Transformation (FFT)");
            plt2.YLabel("Power (dB)");
            plt2.XLabel("Frequency (Hz)");
            plt2.AxisAuto(0);
            plt2.SaveFig("../../../output/fft.png");
        }
    }
}

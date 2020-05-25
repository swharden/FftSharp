using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FftSharp.Demo
{
    public partial class FormAudio : Form
    {
        public FormAudio()
        {
            InitializeComponent();
            UpdateFFT();
        }

        private void UpdateFFT()
        {
            // define audio parameters
            int sampleRate = 48000;
            int fftSize = 4096; // 2^12
            double fftPeriod = (double)fftSize / sampleRate;

            // create noisy signal containing sine waves
            double[] data = new double[fftSize];
            data = FftSharp.SampleData.AddWhiteNoise(data, 1);
            data = FftSharp.SampleData.AddSin(data, sampleRate, 2_000, 1);
            data = FftSharp.SampleData.AddSin(data, sampleRate, 10_000, 2);
            data = FftSharp.SampleData.AddSin(data, sampleRate, 20_000, .5);

            // perform the FFT
            double[] fftAmp = FftSharp.Transform.FFTamplitude(data);

            // plot the signal
            formsPlot1.plt.Clear();
            formsPlot1.plt.PlotSignal(data, sampleRate / 1e3);
            formsPlot1.plt.Title("Input Signal");
            formsPlot1.plt.YLabel("Amplitude");
            formsPlot1.plt.XLabel("Time (milliseconds)");
            formsPlot1.plt.AxisAuto(0);
            formsPlot1.Render();

            // plot the FFT
            formsPlot2.plt.Clear();
            formsPlot2.plt.PlotSignal(fftAmp, fftPeriod);
            formsPlot2.plt.Title("FFT Signal");
            formsPlot2.plt.YLabel("Amplitude (dB)");
            formsPlot2.plt.XLabel("Frequency (Hz)");
            formsPlot2.plt.AxisAuto(0);
            formsPlot2.Render();
        }
    }
}

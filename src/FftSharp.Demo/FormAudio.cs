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

#pragma warning disable CA1416 // Validate platform compatibility

namespace FftSharp.Demo
{
    public partial class FormAudio : Form
    {
        readonly int sampleRate = 48000;
        readonly int fftSize = 4096; // 2^12
        readonly double[] OriginalAudio;

        public FormAudio()
        {
            InitializeComponent();
            plotAudio.Plot.Margins(0);
            plotFFT.Plot.Margins(0);

            // generate the sample signal
            OriginalAudio = new double[fftSize];
            SampleData.AddWhiteNoise(OriginalAudio, 1);
            SampleData.AddSin(OriginalAudio, sampleRate, 2_000, 1);
            SampleData.AddSin(OriginalAudio, sampleRate, 10_000, 2);
            SampleData.AddSin(OriginalAudio, sampleRate, 20_000, .5);
            PlotOriginalSignal();

            comboBox1.Items.AddRange(Window.GetWindows());
            comboBox1.SelectedIndex = 0;
        }

        private void PlotOriginalSignal()
        {
            plotAudio.Plot.Clear();
            plotAudio.Plot.AddSignal(OriginalAudio, sampleRate / 1e3);
            plotAudio.Plot.Title("Input Signal");
            plotAudio.Plot.YLabel("Amplitude");
            plotAudio.Plot.XLabel("Time (milliseconds)");
            plotAudio.Plot.AxisAuto(0);
            plotAudio.Refresh();
        }

        private void OnSelectedWindow(object sender, EventArgs e)
        {
            IWindow window = (IWindow)comboBox1.SelectedItem;
            if (window is null)
                return;

            // apply window
            double[] audio = new double[OriginalAudio.Length];
            Array.Copy(OriginalAudio, audio, OriginalAudio.Length);
            window.ApplyInPlace(audio);

            UpdateKernel(window);
            UpdateWindowed(audio);
            UpdateFFT(window, audio);
        }

        private void UpdateKernel(IWindow window)
        {
            double[] ys = window.Create(4096);
            plotKernel.Plot.Clear();
            plotKernel.Plot.AddSignal(ys, sampleRate / 1e3, Color.Red);
            plotKernel.Plot.AxisAuto(0);
            plotKernel.Plot.Title($"{window} Window");
            plotKernel.Plot.YLabel("Amplitude");
            plotKernel.Plot.XLabel("Time (milliseconds)");
            plotKernel.Refresh();
        }

        private void UpdateWindowed(double[] audio)
        {
            plotWindowed.Plot.Clear();
            plotWindowed.Plot.AddSignal(audio, sampleRate / 1e3);
            plotWindowed.Plot.Title("Windowed Signal");
            plotWindowed.Plot.YLabel("Amplitude");
            plotWindowed.Plot.XLabel("Time (milliseconds)");
            plotWindowed.Plot.AxisAuto(0);
            plotWindowed.Refresh();
        }

        private void UpdateFFT(IWindow window, double[] audio)
        {
            double[] fftPower = FftSharp.Transform.FFTpower(audio);
            double fftPeriod = (double)fftSize / sampleRate;

            plotFFT.Plot.Clear();
            plotFFT.Plot.AddSignal(fftPower, fftPeriod);
            plotFFT.Plot.Title("FFT Signal");
            plotFFT.Plot.YLabel("Power (dB)");
            plotFFT.Plot.XLabel("Frequency (Hz)");
            plotFFT.Plot.AxisAuto(0);
            plotFFT.Refresh();
        }
    }
}

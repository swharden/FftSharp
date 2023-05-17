using ScottPlot.Drawing.Colormaps;
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

            OriginalAudio = new double[fftSize];
            GenerateSignal();

            IWindow[] windows = Window.GetWindows();
            comboBox1.Items.AddRange(windows);
            comboBox1.SelectedIndex = windows.ToList().FindIndex(x => x.Name == "Hanning");
        }

        private void OnSelectedWindow(object sender, EventArgs e)
        {
            IWindow window = (IWindow)comboBox1.SelectedItem;
            if (window is null)
            {
                richTextBox1.Clear();
                return;
            }
            else
            {
                richTextBox1.Text = window.Description;
            }

            // apply window
            double[] audio = new double[OriginalAudio.Length];
            Array.Copy(OriginalAudio, audio, OriginalAudio.Length);
            window.ApplyInPlace(audio);

            UpdateKernel(window);
            UpdateWindowed(audio);
            UpdateFFT(audio);
        }

        private void tbNoise_KeyUp(object sender, KeyEventArgs e) => tbNoise_MouseUp(null, null);

        private void tbNoise_MouseUp(object sender, MouseEventArgs e)
        {
            GenerateSignal();
            OnSelectedWindow(null, null);
        }

        private void cbLog_CheckedChanged(object sender, EventArgs e) => OnSelectedWindow(null, null);

        private void GenerateSignal()
        {
            for (int i = 0; i < OriginalAudio.Length; i++)
                OriginalAudio[i] = 0;

            SampleData.AddWhiteNoise(OriginalAudio, tbNoise.Value / 10.0);
            SampleData.AddSin(OriginalAudio, sampleRate, 2_000, 1);
            SampleData.AddSin(OriginalAudio, sampleRate, 10_000, 2);
            SampleData.AddSin(OriginalAudio, sampleRate, 20_000, .5);
            PlotOriginalSignal();
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

        private void UpdateKernel(IWindow window)
        {
            double[] kernel = window.Create(fftSize);
            double[] pad = ScottPlot.DataGen.Zeros(kernel.Length / 4);
            double[] ys = pad.Concat(kernel).Concat(pad).ToArray();

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

        private void UpdateFFT(double[] audio)
        {
            System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(audio);
            double[] ys = cbLog.Checked ? FftSharp.FFT.Power(spectrum) : FftSharp.FFT.Magnitude(spectrum);
            string yLabel = cbLog.Checked ? "Power (dB)" : "Magnitude (RMS²)";

            plotFFT.Plot.Clear();
            plotFFT.Plot.AddSignal(ys, (double)fftSize / sampleRate);
            plotFFT.Plot.Title("Fast Fourier Transform");
            plotFFT.Plot.YLabel(yLabel);
            plotFFT.Plot.XLabel("Frequency (Hz)");
            plotFFT.Plot.AxisAuto(0);
            plotFFT.Refresh();
        }
    }
}

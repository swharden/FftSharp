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
            formsPlot1.Configure(middleClickMarginX: 0);
            formsPlot2.Configure(middleClickMarginX: 0);

            comboBox1.Items.AddRange(Window.GetWindowNames());
            comboBox1.SelectedIndex = 0;
        }

        private void FormAudio_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFFT();
        }

        private void UpdateFFT()
        {
            // define audio parameters
            int sampleRate = 48000;
            int fftSize = 4096; // 2^12
            double fftPeriod = (double)fftSize / sampleRate;

            // create noisy signal containing sine waves
            double[] audio = new double[fftSize];
            SampleData.AddWhiteNoise(audio, 1);
            SampleData.AddSin(audio, sampleRate, 2_000, 1);
            SampleData.AddSin(audio, sampleRate, 10_000, 2);
            SampleData.AddSin(audio, sampleRate, 20_000, .5);

            double[] window = Window.GetWindowByName(comboBox1.Text, audio.Length);
            Window.ApplyInPlace(window, audio);

            // perform the FFT
            double[] fftPower = FftSharp.Transform.FFTpower(audio);

            // plot the signal
            formsPlot1.plt.Clear();
            formsPlot1.plt.PlotSignal(audio, sampleRate / 1e3);
            formsPlot1.plt.Title("Input Signal");
            formsPlot1.plt.YLabel("Amplitude");
            formsPlot1.plt.XLabel("Time (milliseconds)");
            formsPlot1.plt.AxisAuto(0);
            formsPlot1.Render();

            // plot the FFT
            formsPlot2.plt.Clear();
            formsPlot2.plt.PlotSignal(fftPower, fftPeriod);
            formsPlot2.plt.Title("FFT Signal");
            formsPlot2.plt.YLabel("Power (dB)");
            formsPlot2.plt.XLabel("Frequency (Hz)");
            formsPlot2.plt.AxisAuto(0);
            formsPlot2.Render();
        }
    }
}

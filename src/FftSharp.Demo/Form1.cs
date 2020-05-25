using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace FftSharp.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            formsPlot1.plt.Title("Input Signal");
            formsPlot1.plt.YLabel("Value");
            formsPlot1.plt.XLabel("Time");
            formsPlot1.Render();

            formsPlot2.plt.Title("FFT Signal");
            formsPlot2.plt.YLabel("Power");
            formsPlot2.plt.XLabel("Frequency");
            formsPlot2.Render();
        }

        private void UpdateFFT(double[] input)
        {
            formsPlot1.plt.Clear();
            formsPlot1.plt.PlotSignal(input, markerSize: 10);
            formsPlot1.Render();

            Complex[] fft = FftSharp.Transform.FFT(input);
            double[] fftMag = FftSharp.Convert.GetMagnitude(fft);
            formsPlot2.plt.Clear();
            formsPlot2.plt.PlotSignal(fftMag, markerSize: 10);
            formsPlot2.Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateFFT(FftSharp.SampleData.Set1().Select(x => x.Real).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateFFT(FftSharp.SampleData.Set2().Select(x => x.Real).ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateFFT(FftSharp.SampleData.Set3().Select(x => x.Real).ToArray());
        }
    }
}

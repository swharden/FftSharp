using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace FftSharp.Demo
{
    public partial class FormQuickstart : Form
    {
        public FormQuickstart()
        {
            InitializeComponent();

            formsPlot1.plt.Title("Input Signal");
            formsPlot1.plt.YLabel("Value");
            formsPlot1.plt.XLabel("Time");

            formsPlot2.plt.Title("FFT Signal");
            formsPlot2.plt.YLabel("Power");
            formsPlot2.plt.XLabel("Frequency");

            trackBar1_Scroll(null, null);
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"Sine waves: {trackBar1.Value}";
            double[] values = FftSharp.SampleData.OddSines(128, trackBar1.Value);
            UpdateFFT(values);
        }
    }
}

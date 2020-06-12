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
            trackBar1_Scroll(null, null);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"Sine waves: {trackBar1.Value}";
            double[] values = FftSharp.SampleData.OddSines(128, trackBar1.Value);
            UpdateFFT(values);
        }

        private void UpdateFFT(double[] input)
        {
            // calculate FFT
            double[] fft = FftSharp.Transform.FFTpower(input);

            // plot the input signal
            formsPlot1.plt.Clear();
            formsPlot1.plt.PlotScatter(
                xs: ScottPlot.DataGen.Consecutive(input.Length),
                ys: input);
            formsPlot1.plt.Title("Original Signal");
            formsPlot1.Render();

            // plot the FFT
            formsPlot2.plt.Clear();
            formsPlot2.plt.PlotScatter(
                xs: ScottPlot.DataGen.Consecutive(fft.Length),
                ys: fft);
            formsPlot2.plt.Title("Fast Fourier Transform (FFT)");
            formsPlot2.Render();
        }
    }
}

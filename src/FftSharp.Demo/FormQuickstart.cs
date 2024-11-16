using System;
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

        private void FormQuickstart_Load(object sender, EventArgs e)
        {

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
            System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(input);
            double[] fft = FftSharp.FFT.Magnitude(spectrum);

            // plot the input signal
            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Add.Scatter(
                xs: ScottPlot.Generate.Consecutive(input.Length),
                ys: input);
            formsPlot1.Plot.Title("Original Signal");
            formsPlot1.Refresh();

            // plot the FFT
            formsPlot2.Plot.Clear();
            formsPlot2.Plot.Add.Scatter(
                xs: ScottPlot.Generate.Consecutive(fft.Length),
                ys: fft);
            formsPlot2.Plot.Title("Fast Fourier Transform (FFT)");
            formsPlot2.Refresh();
        }
    }
}

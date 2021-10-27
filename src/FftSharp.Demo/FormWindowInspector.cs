using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FftSharp.Demo
{
    public partial class FormWindowInspector : Form
    {
        public FormWindowInspector()
        {
            InitializeComponent();
            lbWindows.Items.AddRange(Window.GetWindows());
            lbWindows.SelectedIndex = Window.GetWindows().ToList().FindIndex(x => x.Name == "Hanning");
        }

        private void lbWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWindow window = (IWindow)lbWindows.SelectedItem;
            if (window is null)
                return;

            rtbDescription.Text = window.Description;
            UpdateTimePlot(window);
            UpdateFrequencyPlot(window);
        }

        private void UpdateTimePlot(IWindow window)
        {
            double[] xs = ScottPlot.DataGen.Consecutive(100);
            double[] ys = window.Create(xs.Length);

            plotWindow.Plot.Clear();
            plotWindow.Plot.AddScatterLines(xs, ys, lineWidth: 2);
            plotWindow.Plot.YLabel("Amplitude");
            plotWindow.Plot.XLabel("Samples");
            plotWindow.Refresh();
        }

        private void UpdateFrequencyPlot(IWindow window)
        {
            int fftSize = (int)Math.Pow(2, 14);
            double[] xs = ScottPlot.DataGen.Consecutive(fftSize);
            double[] ys = xs.Select(x => Math.Sin(x / fftSize * Math.PI * fftSize / 2)).ToArray();
            double[] windowed = window.Apply(ys);
            double[] power = Transform.FFTpower(windowed);

            plotFreq.Plot.Clear();
            var sig = plotFreq.Plot.AddSignal(power, fftSize / 2);
            sig.OffsetX = -.5;
            plotFreq.Plot.YLabel("Power (dB)");
            plotFreq.Plot.XLabel("Frequency (cycles/sample)");
            plotFreq.Refresh();
        }
    }
}

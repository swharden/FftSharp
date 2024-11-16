using ScottPlot;
using System;
using System.Linq;
using System.Windows.Forms;

#pragma warning disable CA1416 // Validate platform compatibility

namespace FftSharp.Demo
{
    public partial class FormMicrophone : Form
    {
        const int SAMPLE_RATE = 48000;
        NAudio.Wave.WaveInEvent wvin;
        double MaxLevel = 0;

        public FormMicrophone()
        {
            InitializeComponent();

            formsPlot1.Plot.Axes.TightMargins();

            cbDevices.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                cbDevices.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            if (cbDevices.Items.Count > 0)
                cbDevices.SelectedIndex = 0;
            else
                MessageBox.Show("ERROR: no recording devices available");
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            wvin?.Dispose();
            wvin = new NAudio.Wave.WaveInEvent();
            wvin.DeviceNumber = cbDevices.SelectedIndex;
            wvin.WaveFormat = new NAudio.Wave.WaveFormat(rate: SAMPLE_RATE, bits: 16, channels: 1);
            wvin.DataAvailable += OnDataAvailable;
            wvin.BufferMilliseconds = 20;
            wvin.StartRecording();
        }

        double[] lastBuffer;
        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            if (lastBuffer is null || lastBuffer.Length != samplesRecorded)
                lastBuffer = new double[samplesRecorded];
            for (int i = 0; i < samplesRecorded; i++)
                lastBuffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
        }

        ScottPlot.Plottables.Signal signalPlot;
        double[] SignalData = null!;
        ScottPlot.Plottables.VerticalLine peakLine;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lastBuffer is null)
                return;

            var window = new Windows.Hanning();
            double[] windowed = window.Apply(lastBuffer);
            double[] zeroPadded = FftSharp.Pad.ZeroPad(windowed);
            System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(zeroPadded);
            double[] fftPower = cbDecibel.Checked ?
                FftSharp.FFT.Power(spectrum) :
                FftSharp.FFT.Magnitude(spectrum);
            double[] fftFreq = FftSharp.FFT.FrequencyScale(fftPower.Length, SAMPLE_RATE);

            // determine peak frequency
            double peakFreq = 0;
            double peakPower = 0;
            for (int i = 0; i < fftPower.Length; i++)
            {
                if (fftPower[i] > peakPower)
                {
                    peakPower = fftPower[i];
                    peakFreq = fftFreq[i];
                }
            }
            lblPeak.Text = $"Peak Frequency: {peakFreq:N0} Hz";

            formsPlot1.Plot.XLabel("Frequency Hz");

            // make the plot for the first time, otherwise update the existing plot
            if (!formsPlot1.Plot.GetPlottables().Any())
            {
                double samplePeriod = 1.0 / (2.0 * fftPower.Length / SAMPLE_RATE);
                SignalData = fftPower;
                signalPlot = formsPlot1.Plot.Add.Signal(fftPower, samplePeriod);
                peakLine = formsPlot1.Plot.Add.VerticalLine(peakFreq, 2, Colors.Red.WithAlpha(.5));
            }
            else
            {
                Array.Copy(fftPower, SignalData, fftPower.Length);
                peakLine.X = peakFreq;
                peakLine.IsVisible = cbPeak.Checked;
            }

            if (cbAutoAxis.Checked)
            {
                try
                {
                    formsPlot1.Plot.Axes.AutoScale();
                    MaxLevel = Math.Max(MaxLevel, formsPlot1.Plot.Axes.GetLimits().Top);
                    double minLevel = cbDecibel.Checked ? -80 : -MaxLevel/100;
                    formsPlot1.Plot.Axes.SetLimitsY(minLevel, MaxLevel);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            try
            {
                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void cbAutoAxis_CheckedChanged(object sender, EventArgs e)
        {
            MaxLevel = 0;
        }

        private void cbDecibel_CheckedChanged(object sender, EventArgs e)
        {
            MaxLevel = 0;
        }
    }
}

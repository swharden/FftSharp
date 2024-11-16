using NUnit.Framework;

namespace FftSharp.Tests;

class Filter
{
    double[] Values;
    double[] TimesMsec;
    double[] Freqs;

    private const double SampleRate = 48_000;
    private double SamplePeriod => 1.0 / SampleRate;

    [OneTimeSetUp]
    public void LoadAndVerifyData()
    {
        Values = LoadData.Double("sample.txt");
        TimesMsec = ScottPlot.Generate.Consecutive(Values.Length, SamplePeriod * 1000.0);
        Freqs = LoadData.Double("fftFreq.txt");
    }

    [Test]
    public void Test_Filter_Lowpass()
    {
        double[] Filtered = FftSharp.Filter.LowPass(Values, SampleRate, 2000);

        var plt = new ScottPlot.Plot();
        
        var sp1 = plt.Add.Scatter(TimesMsec, Values);
        sp1.LegendText = "Original";

        var sp2 = plt.Add.Scatter(TimesMsec, Filtered);
        sp2.LegendText = "Low-Pass";
        sp2.LineWidth = 2;

        plt.YLabel("Signal Value");
        plt.XLabel("Time (milliseconds)");
        plt.Axes.SetLimitsX(4, 6);

        TestTools.SaveFig(plt);
    }

    [Test]
    public void Test_Filter_HighPass()
    {
        double[] Filtered = FftSharp.Filter.HighPass(Values, SampleRate, 3000);

        var plt = new ScottPlot.Plot();

        var sp1 = plt.Add.Scatter(TimesMsec, Values);
        sp1.LegendText = "Original";

        var sp2 = plt.Add.Scatter(TimesMsec, Filtered);
        sp2.LegendText = "High-Pass";
        sp2.LineWidth = 2;

        plt.YLabel("Signal Value");
        plt.XLabel("Time (milliseconds)");
        plt.Axes.SetLimitsX(4, 6);

        TestTools.SaveFig(plt);
    }

    [Test]
    public void Test_Filter_BandPass()
    {
        double[] Filtered = FftSharp.Filter.BandPass(Values, SampleRate, 1900, 2100);

        var plt = new ScottPlot.Plot();

        var sp1 = plt.Add.Scatter(TimesMsec, Values);
        sp1.LegendText = "Original";

        var sp2 = plt.Add.Scatter(TimesMsec, Filtered);
        sp2.LegendText = "Band-Pass";
        sp2.LineWidth = 2;
        plt.YLabel("Signal Value");
        plt.XLabel("Time (milliseconds)");
        plt.Axes.SetLimitsX(4, 6);

        TestTools.SaveFig(plt);
    }

    [Test]
    public void Test_Filter_BandStop()
    {
        double[] Filtered = FftSharp.Filter.BandStop(Values, SampleRate, 1900, 2100);

        var plt = new ScottPlot.Plot();
        
        var sp1 = plt.Add.Scatter(TimesMsec, Values);
        sp1.LegendText = "Original";

        var sp2 = plt.Add.Scatter(TimesMsec, Filtered);
        sp2.LegendText = "Band-Pass";
        sp2.LineWidth = 2;

        plt.YLabel("Signal Value");
        plt.XLabel("Time (milliseconds)");
        plt.Axes.SetLimits(4, 6);

        TestTools.SaveFig(plt);
    }
}

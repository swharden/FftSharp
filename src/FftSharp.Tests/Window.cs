using NUnit.Framework;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace FftSharp.Tests;

class Window
{
    public static string OUTPUT_FOLDER = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../../dev/quickstart/"));

    [Test]
    public void Test_GetWindow_Works()
    {
        IWindow[] windows = FftSharp.Window.GetWindows();

        foreach (IWindow window in windows)
        {
            Console.WriteLine(window);
        }

        Assert.IsNotNull(windows);
        Assert.IsNotEmpty(windows);
    }

    [Test]
    public void Test_WindowNames_AreNotEmpty()
    {
        foreach (var window in FftSharp.Window.GetWindows())
        {
            Assert.That(string.IsNullOrWhiteSpace(window.Name) == false);
        }
    }

    [Test]
    public void Test_WindowDescriptions_AreNotEmpty()
    {
        foreach (var window in FftSharp.Window.GetWindows())
        {
            Assert.That(string.IsNullOrWhiteSpace(window.Description) == false);
        }
    }

    [Test]
    public void Test_WindowNames_AreUnique()
    {
        var names = FftSharp.Window.GetWindows().Select(x => x.Name);

        Assert.IsNotEmpty(names);
        Assert.AreEqual(names.Count(), names.Distinct().Count());
    }

    [Test]
    public void Test_WindowDescriptions_AreUnique()
    {
        var descriptions = FftSharp.Window.GetWindows().Select(x => x.Description);

        Assert.IsNotEmpty(descriptions);
        Assert.AreEqual(descriptions.Count(), descriptions.Distinct().Count());
    }

    [Test]
    public void Test_NormalizedWindows_SumIsOne()
    {
        foreach (IWindow window in FftSharp.Window.GetWindows())
        {
            double[] kernel = window.Create(123, true);
            double sum = kernel.Sum();
            Assert.AreEqual(1, sum, delta: 1e-10, $"{window} sum is {sum}");
        }
    }

    [Test]
    public void Test_OddLength_CenterIndexIsBiggest()
    {
        foreach (IWindow window in FftSharp.Window.GetWindows())
        {
            double[] values = window.Create(13);
            Assert.GreaterOrEqual(values[6], values[5], window.Name);
            Assert.GreaterOrEqual(values[6], values[7], window.Name);
        }
    }

    [Test]
    public void Test_EvenLength_CenterTwoAreSame()
    {
        foreach (IWindow window in FftSharp.Window.GetWindows())
        {
            double[] values = window.Create(12);
            if (window.IsSymmetric)
            {
                Assert.AreEqual(values[5], values[6], 1e-5, window.Name);
                Assert.AreEqual(values.First(), values.Last(), 1e-5, window.Name);
            }
            else
            {
                Assert.AreNotEqual(values[5], values[6], window.Name);
                Assert.AreNotEqual(values.First(), values.Last(), window.Name);
            }
        }
    }

    [Test]
    public void Test_Plot_AllWindowKernels()
    {
        ScottPlot.Plot plt = new();
        plt.Add.Palette = new ScottPlot.Palettes.ColorblindFriendly();

        foreach (IWindow window in FftSharp.Window.GetWindows())
        {
            if (window.Name == "Rectangular")
                continue;
            double[] xs = ScottPlot.Generate.Range(-1, 1, .01);
            double[] ys = window.Create(xs.Length);
            var sp = plt.Add.Scatter(xs, ys);
            sp.LegendText = window.Name;
            sp.MarkerSize = 0;
            sp.LineWidth = 3;
            sp.Color = sp.Color.WithAlpha(.8);
        }

        plt.Legend.Alignment = Alignment.UpperRight;
        plt.SavePng(Path.Combine(OUTPUT_FOLDER, "windows.png"), 500, 400);
    }

    [Test]
    public void Test_Window_Reflection()
    {
        IWindow[] window = FftSharp.Window.GetWindows();
        Assert.IsNotEmpty(window);
    }

    [Test]
    public void Test_PlotAllWindows()
    {
        foreach (IWindow window in FftSharp.Window.GetWindows())
        {
            double[] values = window.Create(32);
            ScottPlot.Plot plt = new();
            var sig = plt.Add.Signal(values);
            sig.Data.XOffset = -values.Length / 2 + .5;
            plt.Title(window.Name);
            plt.Add.VerticalLine(0);

            string filename = Path.GetFullPath($"test_window_{window.Name}.png");
            Console.WriteLine(filename);
            plt.SavePng(filename, 400, 300);
        }
    }
}

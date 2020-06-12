using NUnit.Framework;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FftSharp.Tests
{
    class Window
    {
        [Test]
        public void Test_Window_Functions()
        {
            var plt = new ScottPlot.Plot(500, 400);
            double[] xs = ScottPlot.DataGen.Range(-1, 1, .01, true);
            plt.PlotScatter(xs, FftSharp.Window.Hanning(xs.Length), label: "Hanning");
            plt.PlotScatter(xs, FftSharp.Window.Hamming(xs.Length), label: "Hamming");
            plt.PlotScatter(xs, FftSharp.Window.Bartlett(xs.Length), label: "Bartlett");
            plt.PlotScatter(xs, FftSharp.Window.Blackman(xs.Length), label: "Blackman");
            //plt.PlotScatter(xs, FftSharp.Window.BlackmanExact(xs.Length), label: "BlackmanExact");
            //plt.PlotScatter(xs, FftSharp.Window.BlackmanHarris(xs.Length), label: "BlackmanHarris");
            plt.PlotScatter(xs, FftSharp.Window.FlatTop(xs.Length), label: "FlatTop");
            plt.PlotScatter(xs, FftSharp.Window.Cosine(xs.Length), label: "Cosine");

            // customize line styles post-hoc
            foreach (var p in plt.GetPlottables())
            {
                if (p is ScottPlot.PlottableScatter)
                {
                    ((ScottPlot.PlottableScatter)p).markerSize = 0;
                    ((ScottPlot.PlottableScatter)p).lineWidth = 3;
                    var c = ((ScottPlot.PlottableScatter)p).color;
                    ((ScottPlot.PlottableScatter)p).color = System.Drawing.Color.FromArgb(200, c.R, c.G, c.B);
                }
            }

            plt.Legend(location: ScottPlot.legendLocation.upperRight);
            plt.SaveFig("../../../../../dev/windows.png");
        }

        [Test]
        public void Test_Window_Reflection()
        {
            foreach (var windowName in FftSharp.Window.GetWindowNames())
            {
                Console.WriteLine(windowName);
                double[] windowed = FftSharp.Window.WindowByName(windowName, 5);
                Console.WriteLine(String.Join(", ", windowed.Select(x => $"{x:N3}").ToArray()));
                Console.WriteLine();
                Assert.AreEqual(5, windowed.Length);
            }
        }
    }
}

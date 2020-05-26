using NUnit.Framework;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Tests
{
    class Window
    {
        [Test]
        public void Test_Window_Functions()
        {
            var plt = new Plot();
            double[] xs = DataGen.Range(-1, 1, .01, true);
            plt.PlotScatter(xs, FftSharp.Window.Hanning(xs.Length), label: "Hanning");
            plt.PlotScatter(xs, FftSharp.Window.Hamming(xs.Length), label: "Hamming");
            plt.PlotScatter(xs, FftSharp.Window.Blackman(xs.Length), label: "Blackman");
            plt.PlotScatter(xs, FftSharp.Window.BlackmanExact(xs.Length), label: "BlackmanExact");
            plt.PlotScatter(xs, FftSharp.Window.BlackmanHarris(xs.Length), label: "BlackmanHarris");
            plt.PlotScatter(xs, FftSharp.Window.FlatTop(xs.Length), label: "FlatTop");
            plt.PlotScatter(xs, FftSharp.Window.Bartlett(xs.Length), label: "Bartlett");

            // customize line styles post-hoc
            foreach (var p in plt.GetPlottables())
            {
                if (p is PlottableScatter)
                {
                    ((PlottableScatter)p).markerSize = 0;
                    ((PlottableScatter)p).lineWidth = 2;
                }
            }

            plt.Legend(location: legendLocation.upperRight);
            plt.SaveFig("test-windows.png");
        }
    }
}

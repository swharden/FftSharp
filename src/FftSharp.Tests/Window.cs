using NUnit.Framework;
using ScottPlot;
using System;
using System.IO;
using System.Linq;

namespace FftSharp.Tests
{
    class Window
    {
        public static string OUTPUT_FOLDER = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../../dev/quickstart/"));
        [Test]
        public void Test_Window_Functions()
        {
            var plt = new ScottPlot.Plot(500, 400);

            double[] xs = ScottPlot.DataGen.Range(-1, 1, .01, true);
            plt.AddScatter(xs, FftSharp.Window.Hanning(xs.Length), label: "Hanning");
            plt.AddScatter(xs, FftSharp.Window.Hamming(xs.Length), label: "Hamming");
            plt.AddScatter(xs, FftSharp.Window.Bartlett(xs.Length), label: "Bartlett");
            plt.AddScatter(xs, FftSharp.Window.Blackman(xs.Length), label: "Blackman");
            //plt.AddScatter(xs, FftSharp.Window.BlackmanExact(xs.Length), label: "BlackmanExact");
            //plt.AddScatter(xs, FftSharp.Window.BlackmanHarris(xs.Length), label: "BlackmanHarris");
            plt.AddScatter(xs, FftSharp.Window.FlatTop(xs.Length), label: "FlatTop");
            plt.AddScatter(xs, FftSharp.Window.Cosine(xs.Length), label: "Cosine");
            plt.AddScatter(xs, FftSharp.Window.Kaiser(xs.Length, 15), label: "Kaiser");

            // customize line styles post-hoc
            foreach (var p in plt.GetPlottables())
            {
                if (p is ScottPlot.Plottable.ScatterPlot sp)
                {
                    sp.MarkerSize = 0;
                    sp.LineWidth = 3;
                    sp.Color = System.Drawing.Color.FromArgb(200, sp.Color);
                }
            }

            plt.Legend(enable: true, location: Alignment.UpperRight);
            plt.SaveFig(Path.Combine(OUTPUT_FOLDER, "windows.png"));
        }

        [Test]
        public void Test_Window_Reflection()
        {
            foreach (var windowName in FftSharp.Window.GetWindowNames())
            {
                Console.WriteLine(windowName);
                double[] windowed = FftSharp.Window.GetWindowByName(windowName, 5);
                Console.WriteLine(String.Join(", ", windowed.Select(x => $"{x:N3}").ToArray()));
                Console.WriteLine();
                Assert.AreEqual(5, windowed.Length);
            }
        }

        [Test]
        public void Test_Kaiser_MatchesPython()
        {
            /* expected values calculated with python:
                >>> import numpy as np
                >>> np.kaiser(50, 14)
            */
            double[] expected = {
                7.72686684e-06, 8.15094846e-05, 3.26000767e-04, 9.42588751e-04, 2.26624847e-03,
                4.80567914e-03, 9.27621459e-03, 1.66164301e-02, 2.79789657e-02, 4.46873500e-02,
                6.81537432e-02, 9.97574012e-02, 1.40689805e-01, 1.91778970e-01, 2.53311387e-01,
                3.24874218e-01, 4.05241756e-01, 4.92328143e-01, 5.83222700e-01, 6.74315433e-01,
                7.61509399e-01, 8.40504954e-01, 9.07130390e-01, 9.57685605e-01, 9.89261639e-01,
                1.00000000e+00, 9.89261639e-01, 9.57685605e-01, 9.07130390e-01, 8.40504954e-01,
                7.61509399e-01, 6.74315433e-01, 5.83222700e-01, 4.92328143e-01, 4.05241756e-01,
                3.24874218e-01, 2.53311387e-01, 1.91778970e-01, 1.40689805e-01, 9.97574012e-02,
                6.81537432e-02, 4.46873500e-02, 2.79789657e-02, 1.66164301e-02, 9.27621459e-03,
                4.80567914e-03, 2.26624847e-03, 9.42588751e-04, 3.26000767e-04, 8.15094846e-05,
            };

            double[] actual = FftSharp.Window.Kaiser(51, 14);

            for (int i = 0; i < expected.Length; i++)
            {
                double allowableError = .00001 * expected[i];
                Assert.AreEqual(expected[i], actual[i], allowableError);
            }
        }
    }
}

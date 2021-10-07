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
            plt.PlotScatter(xs, FftSharp.Window.Kaiser(xs.Length, 15), label: "Kaiser");

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
                double[] windowed = FftSharp.Window.GetWindowByName(windowName, 5);
                Console.WriteLine(String.Join(", ", windowed.Select(x => $"{x:N3}").ToArray()));
                Console.WriteLine();
                Assert.AreEqual(5, windowed.Length);
            }
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(9, 362880)]
        public void Test_Factorial_MatchesKnown(int k, int factorial)
        {
            Assert.AreEqual(factorial, FftSharp.Window.Factorial(k));
        }

        [Test]
        public void Test_Bessel_MatchesPython()
        {
            /* expected values calculated with python:
                >>> import numpy as np
                >>> np.i0(np.arange(20))
            */

            double[] expected = {
                1.00000000e+00, 1.26606588e+00, 2.27958530e+00, 4.88079259e+00,
                1.13019220e+01, 2.72398718e+01, 6.72344070e+01, 1.68593909e+02,
                4.27564116e+02, 1.09358835e+03, 2.81571663e+03, 7.28848934e+03,
                1.89489253e+04, 4.94444896e+04, 1.29418563e+05, 3.39649373e+05,
                8.93446228e+05, 2.35497022e+06, 6.21841242e+06, 1.64461904e+07,
             };

            double[] actual = FftSharp.Window.BesselZero(expected.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                double allowableError = .00001 * expected[i];
                Assert.AreEqual(expected[i], actual[i], allowableError);
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

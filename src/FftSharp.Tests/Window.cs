using NUnit.Framework;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FftSharp.Tests
{
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
        public void Test_Plot_AllWindowKernels()
        {
            var plt = new ScottPlot.Plot(500, 400);
            plt.Palette = ScottPlot.Palette.ColorblindFriendly;

            foreach (IWindow window in FftSharp.Window.GetWindows())
            {
                if (window.Name == "Rectangular")
                    continue;
                double[] xs = ScottPlot.DataGen.Range(-1, 1, .01, true);
                double[] ys = window.Create(xs.Length);
                var sp = plt.AddScatter(xs, ys, label: window.Name);
                sp.MarkerSize = 0;
                sp.LineWidth = 3;
                sp.Color = System.Drawing.Color.FromArgb(200, sp.Color);
            }

            plt.Legend(enable: true, location: Alignment.UpperRight);
            plt.SaveFig(Path.Combine(OUTPUT_FOLDER, "windows.png"));
        }

        [Test]
        public void Test_Window_Reflection()
        {
            IWindow[] window = FftSharp.Window.GetWindows();
            Assert.IsNotEmpty(window);
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

            var window = new FftSharp.Windows.Kaiser(14);
            double[] actual = window.Create(51);

            for (int i = 0; i < expected.Length; i++)
            {
                double allowableError = .00001 * expected[i];
                Assert.AreEqual(expected[i], actual[i], allowableError);
            }
        }
    }
}

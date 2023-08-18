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
            foreach (IWindow window in FftSharp.Window.GetSymmetricWindows())
            {
                double[] values = window.Create(12);
                Assert.AreEqual(values[5], values[6], 1e-5, window.Name);
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
    }
}

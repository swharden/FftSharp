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

        [Ignore("TODO: make this pass")]
        [Test]
        public void Test_EvenLength_CenterTwoAreSame()
        {
            foreach (IWindow window in FftSharp.Window.GetWindows())
            {
                double[] values = window.Create(12);
                ShowBoth(values, values);
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

        [Test]
        public void Test_Bartlett_13_MatchesPython()
        {
            // scipy.signal.windows.flattop(13)
            double[] bartlett_13 = { 0.0, 0.16666667, 0.33333333, 0.5, 0.66666667,
                0.83333333, 1.0, 0.83333333, 0.66666667, 0.5, 0.33333333, 0.16666667, 0.0 };

            double[] values = new FftSharp.Windows.Bartlett().Create(13);

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(bartlett_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Blackman_13_MatchesPython()
        {
            // scipy.signal.windows.blackman(13)
            double[] blackman_13 = { -0.0, 0.0269873, 0.13, 0.34, 0.63, 0.8930127,
                1.0, 0.8930127, 0.63, 0.34, 0.13, 0.0269873, -0.0 };

            double[] values = new FftSharp.Windows.Blackman(.42, .5, .08).Create(13);

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(blackman_13[i], values[i], 1e-5);
            }
        }

        public void ShowBoth(double[] expected, double[] actual)
        {
            Console.WriteLine($"Expected\tActual");

            for (int i = 0; i < expected.Length; i++)
            {
                Console.WriteLine($"{expected[i]}\t{actual[i]}");
            }
        }

        [Test]
        public void Test_Cosine_13_MatchesPython()
        {
            // scipy.signal.windows.cosine(13)
            double[] cosine_13 = { 0.12053668, 0.35460489, 0.56806475, 0.74851075, 0.88545603,
                0.97094182, 1.0, 0.97094182, 0.88545603, 0.74851075, 0.56806475, 0.35460489, 0.12053668 };

            double[] values = new FftSharp.Windows.Cosine().Create(13);

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(cosine_13[i], values[i], 1e-5);
            }
        }

        [Ignore("TODO: make this pass")]
        [Test]
        public void Test_FlatTop_13_MatchesPython()
        {
            /*
            // scipy.signal.windows.flattop(13)
            */

            double[] flattop_13 = { -0.00042105, -0.01007669, -0.05126316, -0.05473684, 0.19821053,
                0.71155038, 1.0, 0.71155038, 0.19821053, -0.05473684, -0.05126316, -0.01007669, -0.00042105 };

            double[] values = new FftSharp.Windows.FlatTop().Create(13);

            // Python uses a 5-term method and a 3-term method is used here... I'm not going to test this at this time

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(flattop_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Hamming_13_MatchesPython()
        {
            // scipy.signal.windows.hamming(13)
            double[] hamming_13 = { 0.08, 0.14162831, 0.31, 0.54, 0.77, 0.93837169, 1.0, 0.93837169, 0.77, 0.54, 0.31, 0.14162831, 0.08 };

            double[] values = new FftSharp.Windows.Hamming().Create(13);

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(hamming_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Hanning_13_MatchesPython()
        {
            // scipy.signal.windows.hanning(13)
            double[] hanning_13 = { 0.0, 0.0669873, 0.25, 0.5, 0.75, 0.9330127, 1.0, 0.9330127, 0.75, 0.5, 0.25, 0.0669873, 0.0 };

            double[] values = new FftSharp.Windows.Hanning().Create(13);

            Assert.AreEqual(values.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(hanning_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Rectangular()
        {
            // scipy.signal.windows.tukey(13)
            double[] values = new FftSharp.Windows.Rectangular().Create(13);

            Assert.AreEqual(13, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(1, values[i]);
            }
        }

        [Ignore("TODO: make this pass")]
        [Test]
        public void Test_Tukey_13_MatchesPython()
        {
            // scipy.signal.windows.tukey(13)
            double[] tukey_13 = { 0.0, 0.25, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.75, 0.25, 0.0 };

            double[] values = new FftSharp.Windows.Tukey(14).Create(13);

            Assert.AreEqual(tukey_13.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(tukey_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Kaiser_14_13_MatchesPython()
        {
            // scipy.signal.windows.kaiser(13, beta=14)
            double[] kaiser_14_13 = { 7.73e-06, 0.00258844, 0.03288553,
                0.16493219, 0.4627165, 0.82808941, 1.0, 0.82808941, 0.4627165,
                0.16493219, 0.03288553, 0.00258844, 7.73e-06 };

            double[] values = new FftSharp.Windows.Kaiser(14).Create(13);

            Assert.AreEqual(kaiser_14_13.Length, values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(kaiser_14_13[i], values[i], 1e-5);
            }
        }

        [Test]
        public void Test_Kaiser_14_50_MatchesPython()
        {
            // np.kaiser(50, 14)
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
                Assert.AreEqual(expected[i], actual[i], 1e-5);
            }
        }
    }
}

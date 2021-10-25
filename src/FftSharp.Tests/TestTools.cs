using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Tests
{
    public static class TestTools
    {
        public static string SaveFig(ScottPlot.Plot plt, string subName = "", bool artifact = false)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;

            if (subName != "")
                subName = "_" + subName;

            string fileName = callingMethod + subName + ".png";
            string filePath = System.IO.Path.GetFullPath(fileName);
            plt.SaveFig(filePath);

            Console.WriteLine($"Saved: {filePath}");
            Console.WriteLine();

            return filePath;
        }

        /// <summary>
        /// assert values have mirror symmetry (except the first and last N points)
        /// </summary>
        public static void AssertMirror(double[] values, int ignoreFirst = 1)
        {
            for (int i = ignoreFirst; i < values.Length / 2; i++)
            {
                int i2 = values.Length - i;
                Assert.AreEqual(values[i], values[i2], delta: 1e-10, $"Not mirror at index {i} and {i2}");
            }
        }
    }
}

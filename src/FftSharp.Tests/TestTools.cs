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
    }
}

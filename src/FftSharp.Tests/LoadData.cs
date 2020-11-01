using System;
using System.IO;
using System.Linq;

namespace FftSharp.Tests
{
    public static class LoadData
    {
        public static double[] Double(string fileName) =>
             File.ReadLines($"../../../../../dev/data/{fileName}")
                 .Where(x => !x.StartsWith('#') && x.Length > 1)
                 .Select(x => double.Parse(x))
                 .ToArray();

        public static Complex[] Complex(string fileName) =>
             File.ReadLines($"../../../../../dev/data/{fileName}")
                 .Select(x => x.Trim('(').Trim(')').Trim('j'))
                 .Select(x => x.Replace("-", " -").Replace("+", " +").Trim())
                 .Select(x => new Complex(double.Parse(x.Split(' ')[0]), double.Parse(x.Split(' ')[1])))
                 .ToArray();
    }
}

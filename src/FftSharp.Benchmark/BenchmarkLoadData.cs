namespace FftSharp.Benchmark;

public static class BenchmarkLoadData
{
    public static double[] Double(string fileName) =>
         File.ReadLines(fileName)
             .Where(x => !x.StartsWith('#') && x.Length > 1)
             .Select(x => double.Parse(x))
             .ToArray();

    public static System.Numerics.Complex[] Complex(string fileName) =>
         File.ReadLines(fileName)
             .Select(x => x.Trim('(').Trim(')').Trim('j'))
             .Select(x => x.Replace("-", " -").Replace("+", " +").Trim())
             .Select(x => new System.Numerics.Complex(double.Parse(x.Split(' ')[0]), double.Parse(x.Split(' ')[1])))
             .ToArray();
}

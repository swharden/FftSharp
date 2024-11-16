using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace FftSharp.Benchmark;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("FftSharp Benchmarks!");
        BenchmarkRunner.Run<FftBenchmark>();
        BenchmarkRunner.Run<BluesteinSizeBenchmark>();
    }
}

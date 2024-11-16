using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using MathNet.Numerics;
namespace FftSharp.Benchmark;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
public class BluesteinSizeBenchmark
{
    private double[] Sample;
    [Params(100, 1000, 10_000, 100_000)]
    public int DataLength { get; set; }
    public double Frequency = 60;
    public double SampleRate = 1000;

    [GlobalSetup]
    public void BluesteinSizeBenchmarkSetup()
    {
        this.Sample = Generate.Sinusoidal(this.DataLength, this.SampleRate, this.Frequency, 1);
        if (this.Sample.Length != this.DataLength)
        {
            throw new Exception("Sample length does not match DataLength");
        }
    }

    [Benchmark]
    public void Bluestein()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        var something = Experimental.Bluestein(this.Sample);
    }
}


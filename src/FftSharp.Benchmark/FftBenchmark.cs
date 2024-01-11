using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FftSharp.Benchmark;
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class FftBenchmark
{
    private double[] Sample;

    [GlobalSetup]
    public void FftBenchmarkSetup()
    {
        this.Sample = BenchmarkLoadData.Double("sample.txt");
    }

    [Benchmark]
    public void FFT_Forward()
    {
        var something = FFT.Forward(this.Sample);
    }

    [Benchmark]
    public void FFT_ForwardReal()
    {
        var something = FFT.ForwardReal(this.Sample);
    }

    [Benchmark]
    public void FFT_BluesteinComparason()
    {
        var something = Experimental.Bluestein(this.Sample);
    }
}


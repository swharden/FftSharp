# Benchmarking 

## Benchmarking FFTSharp

To run the benchmarks navigate to the `src/FftSharp.Benchmark` directory and run the following command:

```bash
dotnet run -c Release
```

## Results 12/20/2023

### BluesteinSizeBenchmark
```
BenchmarkDotNet v0.13.11, Windows 11 (10.0.22631.2861/23H2/2023Update/SunValley3)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]   : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```
| Method    | Job      | Runtime  | DataLength | Mean         | Error        | StdDev       | Gen0     | Gen1     | Gen2     | Allocated   |
|---------- |--------- |--------- |----------- |-------------:|-------------:|-------------:|---------:|---------:|---------:|------------:|
| **Bluestein** | **.NET 6.0** | **.NET 6.0** | **100**        |     **26.40 μs** |     **0.401 μs** |     **0.375 μs** |   **7.1716** |   **0.0305** |        **-** |    **29.36 KB** |
| Bluestein | .NET 8.0 | .NET 8.0 | 100        |     24.97 μs |     0.321 μs |     0.284 μs |   7.1716 |        - |        - |    29.36 KB |
| **Bluestein** | **.NET 6.0** | **.NET 6.0** | **1000**       |    **247.14 μs** |     **3.809 μs** |     **3.181 μs** |  **58.1055** |  **16.1133** |        **-** |   **239.49 KB** |
| Bluestein | .NET 8.0 | .NET 8.0 | 1000       |    239.35 μs |     4.046 μs |     3.379 μs |  58.1055 |  16.3574 |        - |   239.49 KB |
| **Bluestein** | **.NET 6.0** | **.NET 6.0** | **10000**      |  **5,426.03 μs** |    **88.896 μs** |   **102.372 μs** | **984.3750** | **984.3750** | **984.3750** |  **3641.08 KB** |
| Bluestein | .NET 8.0 | .NET 8.0 | 10000      |  5,348.21 μs |   103.018 μs |    86.025 μs | 984.3750 | 984.3750 | 984.3750 |  3641.04 KB |
| **Bluestein** | **.NET 6.0** | **.NET 6.0** | **100000**     | **84,122.15 μs** | **1,655.385 μs** | **2,374.104 μs** | **833.3333** | **833.3333** | **833.3333** | **29749.57 KB** |
| Bluestein | .NET 8.0 | .NET 8.0 | 100000     | 83,047.50 μs | 1,654.760 μs | 2,718.818 μs | 666.6667 | 666.6667 | 666.6667 | 29751.45 KB |

### FftBenchmark
```
BenchmarkDotNet v0.13.11, Windows 11 (10.0.22631.2861/23H2/2023Update/SunValley3)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.100
  [Host]   : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```
| Method                  | Job      | Runtime  | Mean      | Error    | StdDev   | Gen0    | Gen1    | Allocated |
|------------------------ |--------- |--------- |----------:|---------:|---------:|--------:|--------:|----------:|
| FFT_Forward             | .NET 6.0 | .NET 6.0 |  41.47 μs | 0.722 μs | 0.676 μs |  1.9531 |       - |   8.02 KB |
| FFT_ForwardReal         | .NET 6.0 | .NET 6.0 |  42.72 μs | 0.715 μs | 0.669 μs |  2.9297 |       - |  12.06 KB |
| FFT_BluesteinComparason | .NET 6.0 | .NET 6.0 | 233.21 μs | 3.444 μs | 3.053 μs | 54.4434 | 15.3809 | 224.24 KB |
| FFT_Forward             | .NET 8.0 | .NET 8.0 |  41.08 μs | 0.749 μs | 0.585 μs |  1.9531 |       - |   8.02 KB |
| FFT_ForwardReal         | .NET 8.0 | .NET 8.0 |  41.79 μs | 0.788 μs | 0.698 μs |  2.9297 |       - |  12.06 KB |
| FFT_BluesteinComparason | .NET 8.0 | .NET 8.0 | 223.61 μs | 4.330 μs | 4.633 μs | 54.4434 | 15.3809 | 224.23 KB |
# FftSharp

**FftSharp is a collection of simple FFT and IFFT methods for .NET.** FftSharp is provided under the permissive [MIT license](LICENSE) so it is suitable for use in commercial products. FftSharp has no dependencies and is written for .NET Standard so it can be used in cross-platform .NET Framework and .NET Core applications.

<div align="center">

![](dev/screenshot.png)

</div>

## Quickstart

> **⚠️ WARNING:** FftSharp is early in development and its public API may change as it matures.

`FFT()` can be run with `Complex[]` input, but an overload also accepts `double[]` and automatically generates the complex input with the input values assigned to the real component.

```cs
// create sample data containing two sine waves
double[] sampleData = new double[128];
for (int i = 0; i < sampleData.Length; i++){
    double wave1 = Math.Sin(i / Math.PI);
    double wave2 = Math.Sin(3 * i / Math.PI) * 0.333;
    sampleData[i] = wave1 + wave2;
}

// Calculate the FFT with FftSharp
Complex[] fft = FftSharp.Transform.FFT(sampleData);

// convert the output to a double array for graphing
double[] fftMag = FftSharp.Convert.GetMagnitude(fft);
```

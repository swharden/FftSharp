# FftSharp

**FftSharp is a collection of simple FFT and IFFT methods for .NET.** FftSharp is provided under the permissive [MIT license](LICENSE) so it is suitable for use in commercial products. FftSharp targets .NET Standard and it has no dependencies so it can be easily used in cross-platform .NET Framework and .NET Core applications.

<div align="center">

![](dev/screenshot.png)

</div>

## Installation

FftSharp is available on NuGet:
* https://www.nuget.org/packages/FftSharp/

## Quickstart

A quickstart application ([Program.cs](src/FftSharp.Quickstart/Program.cs)) demonstrates common FftSharp features. The quickstart program generates the graphs shown here, but plotting-related code has been omitted from these code samples.

> **⚠️ WARNING:** FftSharp is early in development and its public API may change as it matures.

### Generate Sample Data

```cs
int sampleRate = 48_000;
int pointCount = 512;

// create a signal with noise and sine waves at 500, 1200, and 1500 Hz
double[] audio = FftSharp.SampleData.WhiteNoise(pointCount);
audio = FftSharp.SampleData.AddSin(audio, sampleRate, 2_000, 2);
audio = FftSharp.SampleData.AddSin(audio, sampleRate, 10_000, 1);
audio = FftSharp.SampleData.AddSin(audio, sampleRate, 20_000, .5);

// You could get the FFT as a complex result
System.Numerics.Complex[] fft = FftSharp.Transform.FFT(audio);

// For audio we typically want the FFT amplitude (in dB)
double[] fftAmp = FftSharp.Transform.FFTamplitude(audio);

// Create an array of frequencies for each point of the FFT
double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftAmp.Length);
```

![](src/FftSharp.Quickstart/output/audio.png)

### Get the Fast Fourier Transform (FFT)

While you could choose to work with the `Complex` datatype (which has real and imaginary components), with FftSharp you don't have to. Methods have been created to calculate FFT amplitude and FFT power (both with dB units), which are the most common uses for the FFT in audio applications.

```cs
// You could get the FFT as a complex result
System.Numerics.Complex[] fft = FftSharp.Transform.FFT(audio);

// For audio we typically want the FFT amplitude (in dB)
double[] fftAmp = FftSharp.Transform.FFTamplitude(audio);

// Create an array of frequencies for each point of the FFT
double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftAmp.Length);
```

![](src/FftSharp.Quickstart/output/fft.png)
**FftSharp is a collection of Fast Fourier Transform (FFT) tools for .NET**

### Quickstart

```cs
// Begin with an array containing sample data
double[] signal = FftSharp.SampleData.SampleAudio1();

// Shape the signal using a Hanning window
var window = new FftSharp.Windows.Hanning();
window.ApplyInPlace(signal);

// Calculate the FFT as an array of complex numbers
System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);

// or get the magnitude (units²) or power (dB) as real numbers
double[] magnitude = FftSharp.FFT.Magnitude(spectrum);
double[] power = FftSharp.FFT.Power(spectrum);
```
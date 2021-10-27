**FftSharp is a collection of Fast Fourier Transform (FFT) tools for .NET**

### Quickstart

```cs
// Begin with an array containing sample data
double[] signal = FftSharp.SampleData.SampleAudio1();

// Shape the signal using a Hanning window
var window = new FftSharp.Windows.Hanning();
window.ApplyInPlace(signal);

// Calculate the FFT as an array of complex numbers
Complex[] fftRaw = FftSharp.Transform.FFT(signal);

// or get the magnitude (units²) or power (dB) as real numbers
double[] fftMag = FftSharp.Transform.FFTmagnitude(signal);
double[] fftPwr = FftSharp.Transform.FFTpower(signal);
```
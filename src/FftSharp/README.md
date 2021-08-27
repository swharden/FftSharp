**FftSharp is a collection of Fast Fourier Transform (FFT) tools for .NET.** Tools are available to calculate forward and inverse FFT, generate and apply window functions, perform Mel Scaling, and more. See https://github.com/swharden/FftSharp for examples.

### Quickstart: Power Spectrum Analysis

```cs
// Begin with sample data
int sampleRate = 48000;
double[] audio = FftSharp.SampleData.SampleAudio1();

// Window the audio signal
double[] window = FftSharp.Window.Hanning(audio.Length);
FftSharp.Window.ApplyInPlace(window, audio);

// Calculate power spectral density (dB)
double[] fftPower = FftSharp.Transform.FFTpower(audio);

// Calculate the frequencies for each data point
double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftPower.Length);
```

### Quickstart: Calculate the FFT of Complex Data

```cs
// Begin with sample data
double[] audio = FftSharp.SampleData.SampleAudio1();

// convert the data to an array of complex numbers
Complex[] buffer = new Complex[audio.Length];
for (int i=0; i<buffer.Length; i++)
    buffer[i] = new Complex(audio[i], 0);

// compute the FFT in-place
FftSharp.Transform.FFT(buffer);
```
# FFT Notes

### FFT Resolution
```cs
var Resolution = 1.0 / (sampleLength * samplePeriod)
```

### Two-Sided FFT to Single-Sided Power Spectrum

* The two-sided spectrum shows positive and negative power.
* If power of AC signals is centered at 0, positive and negative are identical.
* The two-sided spectrum is redundant for real-world signals centered at zero.
* The single-sided power spectrum of a waveform is the original units * rms^2
* Power Spectral Density (PSD) is amplitude^2 / Hz

**To combine energy** from the positive and negative spectra, discard the second half of the array and multiply every point (except the DC / the first FFT bin) by 2. Points are then `amplitude / sqrt(2)`. Amplitude divided by `sqrt(2)` of a sine wave is called [root mean square](https://en.wikipedia.org/wiki/Root_mean_square).

**To get the amplitude spectrum** (in original units rms) apply this formula to every point to N/2 (where N = length of FFT output)

```cs
int N = FFT.Length;

// convert FFT to amplitude spectrum (single-sided)
for (int i=1; i< N/2; i++)
    Amplitude[i] = sqrt(2) * FFT[i].Magnitude / N

// special case for first (DC) bin
Amplitude[0] = Magnitude(FFT[i]) / N
```

**Convert to log units** by applying a dB conversion:
```
Power (dB) = 2 * 10 * Log10(amplitude / reference)
```

```
magnitude = FFTpos * 2 / N
dB = 2 * 10 * np.log10(magnitude / reference)
```

## Resources

* NI Application Note 41: [The Fundamentals of FFT-Based Signal Analysis
and Measurement ](https://www.sjsu.edu/people/burford.furman/docs/me120/FFT_tutorial_NI.pdf)

* [FFT Spectrum and Spectral Densities](https://www.ap.com/blog/fft-spectrum-and-spectral-densities-same-data-different-scaling/)

* [Frequency and the Fast Fourier Transform](https://www.oreilly.com/library/view/elegant-scipy/9781491922927/ch04.html)

* [FTT and power spectra](http://faculty.jsd.claremont.edu/jmilton/Math_Lab_tool/Labs/Lab9.pdf) with notes about application in biological contexts

* [Cooley–Tukey FFT algorithm](https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm) on Wikipedia
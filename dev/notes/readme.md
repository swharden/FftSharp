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

### Converting to Log Units

Usually amplitude or power spectra are shown in dB units.

```
dB = 10 * Log10(power / reference)
```

If our stating point is amplitude, our output becomes:

```
dB = 20 * Log10(amplitude / reference)
```

(because multiplying the decibel ratio by two is equivalent to having a squared ratio)

## Resources

* NI Application Note 41: [The Fundamentals of FFT-Based Signal Analysis
and Measurement ](https://www.sjsu.edu/people/burford.furman/docs/me120/FFT_tutorial_NI.pdf)

* [FFT Spectrum and Spectral Densities](https://www.ap.com/blog/fft-spectrum-and-spectral-densities-same-data-different-scaling/)

* [Frequency and the Fast Fourier Transform](https://www.oreilly.com/library/view/elegant-scipy/9781491922927/ch04.html)
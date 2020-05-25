# FFT Implementations on the Internet

This page aggregates FFT/IFFT implementation resources which can be found on the internet. 

If you are aware of a useful open-source implementation I'd love to hear about it! Post it in an issue or add it directly to this page and make a pull request.

## KISS FFT

The [KISS FFT project](https://github.com/mborgerding/kissfft) aims to provide a C FFT library. It is distributed as a combination of the [Unlicense](https://spdx.org/licenses/Unlicense.html) and [BSD 3-clause](https://spdx.org/licenses/BSD-3-Clause.html) and licenses.

This library implements some very creative optimizations for maximizing performance while maintaining thread-safety.

## Takuya (C/Fortran)

This is a package to calculate Discrete Fourier/Cosine/Sine Transforms of 1-dimensional sequences of length 2^N. This package contains C and Fortran FFT codes. The original webpage is [here](http://www.kurims.kyoto-u.ac.jp/~ooura/fft.html) and a copy of the source code has been added to this repository. The author has a webpage of [performance assessments](http://www.kurims.kyoto-u.ac.jp/~ooura/fftbmk.html).

* Source code: [ooura](ooura)

## FFTW (C)

[FFTW](http://www.fftw.org/) is an open-source C library for computing the discrete Fourier transform (DFT) in one or more dimensions, of arbitrary input size, and of both real and complex data. They have [extensive documentation](http://www.fftw.org/fftw3.pdf) which is useful to review.

FFTW is GPL-licensed, so it may not be suitable for many commercial applications. Their website indicates an alternative licenses may be purchased from the author.

## Trasformata di Fourier veloce (C++)
A C++ implementation of the [Cooley–Tukey FFT algorithm](https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm) can be found on this page (code comments are in Italian). I'm unsure of the copyright or licensing which may apply.

* Source code: [wiki/Trasformata_di_Fourier_veloce](https://it.wikipedia.org/wiki/Trasformata_di_Fourier_veloce)

## Oakley (C#)

This C# FFT implementation came from a [2010 lecture](http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf) and a 2011 [write-up of a capstone project](https://www.egr.msu.edu/classes/ece480/capstone/fall11/group06/style/Application_Note_ChrisOakley.pdf) by Christopher Oakley. The author was contacted on May 25, 2020 and agreed to its redistribution under a MIT license. These documents are useful because they provide graphs of input waveforms alongside the FFT output.

* [complex.cs](oakley/complex.cs)
* [fft.cs](oakley/fft.cs)
* [dft.cs](oakley/dft.cs)
* [main.cs](oakley/main.cs)

## Sparky (C#)

This C# FFT/IFFT implementation came from a 2007 blogspot article [Fast Fourier Transform (FFT/IFFT) in C#](http://sdasrath.blogspot.com/2007/11/20071101-fast-fourier-transform-fftifft.html) by Surujlal 'Sparky' Dasrath. Source code indicates a copyright but no license. The original author was contacted on May 24, 2020 and agreed to its redistribution under a MIT license.

* [fftifft.cs](sparky/fftifft.cs)
* [complex.cs](sparky/complex.cs)

## FFTPACK (Fortran)

Many modern FFT implementations are translated versions of [FFTPACK](https://www.netlib.org/fftpack/) for Fortran. On example of this is [scipy.fftpack.fft](https://docs.scipy.org/doc/scipy/reference/generated/scipy.fftpack.fft.html) for Python.

I found a C# version of FFTPACK on [dcprojects/SharpFFTPACK](https://github.com/dcprojects/SharpFFTPACK). This looks like it should a useful reference. It is released under the permissive "unlicense" and seems to be a C# translation of [jfftpack](https://github.com/fjfdeztoro/fftpack).
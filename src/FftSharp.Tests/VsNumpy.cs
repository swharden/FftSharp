using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace FftSharp.Tests
{
    /// <summary>
    /// Code here analyzes standard test audio data and compares FftSharp values to those calculated by Numpy.
    /// </summary>
    class VsNumpy
    {
        double[] values;

        [OneTimeSetUp]
        public void LoadAndVerifyData()
        {
            values = LoadDoubleData("sample.txt");
            Assert.AreEqual(512, values.Length);
            Assert.AreEqual(1.44, values[2]);
            Assert.AreEqual(71.52, values.Sum(), 1e-10);
            Assert.AreEqual(10.417026634786811, values.Select(x => Math.Sin(x)).Sum(), 1e-10);
        }

        private double[] LoadDoubleData(string fileName) =>
             File.ReadLines($"../../../../../dev/data/{fileName}")
                 .Where(x => !x.StartsWith('#') && x.Length > 1)
                 .Select(x => double.Parse(x))
                 .ToArray();

        private Complex[] LoadComplexData(string fileName) =>
             File.ReadLines($"../../../../../dev/data/{fileName}")
                 .Select(x => x.Trim('(').Trim(')').Trim('j'))
                 .Select(x => x.Replace("-", " -").Replace("+", " +").Trim())
                 .Select(x => new Complex(double.Parse(x.Split(' ')[0]), double.Parse(x.Split(' ')[1])))
                 .ToArray();

        [Test]
        public void Test_VsNumpy_Fft()
        {
            Complex[] fft = FftSharp.Transform.FFT(values);
            Complex[] numpyFft = LoadComplexData("fft.txt");

            Assert.AreEqual(numpyFft.Length, fft.Length);

            for (int i = 0; i < fft.Length; i++)
            {
                Assert.AreEqual(numpyFft[i].Real, fft[i].Real, 1e-10);
                Assert.AreEqual(numpyFft[i].Imaginary, fft[i].Imaginary, 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Rfft()
        {
            Complex[] rfft = FftSharp.Transform.RFFT(values);
            Complex[] numpyRfft = LoadComplexData("fftReal.txt");

            Assert.AreEqual(numpyRfft.Length, rfft.Length);

            for (int i = 0; i < rfft.Length; i++)
            {
                Assert.AreEqual(numpyRfft[i].Real, rfft[i].Real, 1e-10);
                Assert.AreEqual(numpyRfft[i].Imaginary, rfft[i].Imaginary, 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_FftMag()
        {
            double[] fftMag = FftSharp.Transform.FFTmagnitude(values);
            double[] numpyFftMag = LoadDoubleData("fftMag.txt");

            Assert.AreEqual(numpyFftMag.Length, fftMag.Length);

            for (int i = 0; i < fftMag.Length; i++)
                Assert.AreEqual(numpyFftMag[i], fftMag[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_FftDB()
        {
            double[] fftDB = FftSharp.Transform.FFTpower(values);
            double[] numpyFftDB = LoadDoubleData("fftDB.txt");

            Assert.AreEqual(numpyFftDB.Length, fftDB.Length);

            for (int i = 0; i < fftDB.Length; i++)
                Assert.AreEqual(numpyFftDB[i], fftDB[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_FftFreq()
        {
            double[] fftFreq = FftSharp.Transform.FFTfreq(sampleRate: 48_000, pointCount: values.Length, oneSided: false);
            double[] numpyFftFreq = LoadDoubleData("fftFreq.txt");

            Assert.AreEqual(numpyFftFreq.Length, fftFreq.Length);

            for (int i = 0; i < fftFreq.Length; i++)
                Assert.AreEqual(numpyFftFreq[i], fftFreq[i], 1e-10);
        }
    }
}

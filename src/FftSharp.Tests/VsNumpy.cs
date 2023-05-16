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
            values = LoadData.Double("sample.txt");
            Assert.AreEqual(512, values.Length);
            Assert.AreEqual(1.44, values[2]);
            Assert.AreEqual(71.52, values.Sum(), 1e-10);
            Assert.AreEqual(10.417026634786811, values.Select(x => Math.Sin(x)).Sum(), 1e-10);
        }

        [Test]
        public void Test_VsNumpy_Fft()
        {
            Complex[] fft = FftSharp.Transform.FFT(values);
            Complex[] numpyFft = LoadData.Complex("fft.txt");

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
            Complex[] numpyRfft = LoadData.Complex("fftReal.txt");

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
            double[] numpyFftMag = LoadData.Double("fftMag.txt");

            Assert.AreEqual(numpyFftMag.Length, fftMag.Length);

            for (int i = 0; i < fftMag.Length; i++)
                Assert.AreEqual(numpyFftMag[i], fftMag[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_FftDB()
        {
            double[] fftDB = FftSharp.Transform.FFTpower(values);
            double[] numpyFftDB = LoadData.Double("fftDB.txt");

            Assert.AreEqual(numpyFftDB.Length, fftDB.Length);

            for (int i = 0; i < fftDB.Length; i++)
                Assert.AreEqual(numpyFftDB[i], fftDB[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_FftPhase()
        {
            double[] phases = FftSharp.Transform.FFTphase(values);
            double[] numpyFftPhase = LoadData.Double("fftPhase.txt");

            Assert.AreEqual(numpyFftPhase.Length, phases.Length);

            for (int i = 0; i < phases.Length; i++)
                Assert.AreEqual(numpyFftPhase[i], phases[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_FftFreq()
        {
            double[] fftFreq = FftSharp.Transform.FFTfreq(sampleRate: 48_000, pointCount: values.Length, oneSided: false);
            double[] numpyFftFreq = LoadData.Double("fftFreq.txt");

            Assert.AreEqual(numpyFftFreq.Length, fftFreq.Length);

            for (int i = 0; i < fftFreq.Length; i++)
                Assert.AreEqual(numpyFftFreq[i], fftFreq[i], 1e-10);
        }

        [Test]
        public void Test_VsNumpy_Fft_Length32()
        {
            double[] values = { 0.33, 2.15, 1.44, 1.37, 0.24, 2.6, 3.51, 1.98, 1.88, 0.08,
                1.82, 1.3, 0.23, -1.16, -1.35, -0.58, -0.84, -1.35, -2.72, -2.53, -0.02,
                -0.76, -0.48, -2.1, 0.3, 1.86, 1.6, 1.49, 0.58, 2.12, 2.79, 1.99  };

            Assert.AreEqual(32, values.Length);

            double[] numpyFftMag = { 0.5553125, 1.746009233890808, 0.6806147230825694, 0.2700447117556277,
                0.13510742832008432, 0.19195357237244665, 0.4374273994066255, 0.8583254324650509, 0.2941652649872177,
                0.12648992889321545, 0.17946592505602227, 0.08145444884938172, 0.1681978420275756, 0.2919426435801102,
                0.35936842267425245, 0.2704107530566828, 0.05312499999999987 };

            double[] mag = FftSharp.Transform.FFTmagnitude(values);

            Assert.AreEqual(numpyFftMag.Length, mag.Length);
            for (int i = 0; i < numpyFftMag.Length; i++)
            {
                Assert.AreEqual(numpyFftMag[i], mag[i], 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Fft_Length16()
        {
            double[] values = { 0.33, 2.15, 1.44, 1.37, 0.24, 2.6, 3.51, 1.98, 1.88, 0.08, 1.82, 1.3, 0.23, -1.16, -1.35, -0.58 };

            Assert.AreEqual(16, values.Length);

            double[] numpyFftMag = { 0.99, 1.3848691625350331, 0.3643928332909465, 1.0042721499156904,
                0.34613039450473, 0.1401344145671993, 0.2637216582804615, 0.5006617594915167, 0.04499999999999993 };

            double[] mag = FftSharp.Transform.FFTmagnitude(values);

            Assert.AreEqual(numpyFftMag.Length, mag.Length);
            for (int i = 0; i < numpyFftMag.Length; i++)
            {
                Assert.AreEqual(numpyFftMag[i], mag[i], 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Fft_Length8()
        {
            double[] values = { 0.33, 2.15, 1.44, 1.37, 0.24, 2.6, 3.51, 1.98 };

            Assert.AreEqual(8, values.Length);

            double[] numpyFftMag = { 1.7025, 0.706710339966861, 1.1495760087962865, 0.33016737480242325, 0.645 };

            double[] mag = FftSharp.Transform.FFTmagnitude(values);

            Assert.AreEqual(numpyFftMag.Length, mag.Length);
            for (int i = 0; i < numpyFftMag.Length; i++)
            {
                Assert.AreEqual(numpyFftMag[i], mag[i], 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Fft_Length4()
        {
            double[] values = { 0.33, 2.15, 1.44, 1.37 };

            Assert.AreEqual(4, values.Length);

            double[] numpyFftMag = { 1.3225, 0.6783251432757007, 0.875 };

            double[] mag = FftSharp.Transform.FFTmagnitude(values);

            Assert.AreEqual(numpyFftMag.Length, mag.Length);
            for (int i = 0; i < numpyFftMag.Length; i++)
            {
                Assert.AreEqual(numpyFftMag[i], mag[i], 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Fft_Length2()
        {
            double[] values = { 0.33, 2.15 };

            Assert.AreEqual(2, values.Length);

            double[] numpyFftMag = { 1.24, 1.8199999999999998 };

            double[] mag = FftSharp.Transform.FFTmagnitude(values);

            Assert.AreEqual(numpyFftMag.Length, mag.Length);
            for (int i = 0; i < numpyFftMag.Length; i++)
            {
                Assert.AreEqual(numpyFftMag[i], mag[i], 1e-10);
            }
        }

        [Test]
        public void Test_VsNumpy_Fft_Length1()
        {
            double[] values = { 0.33 };
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTmagnitude(values); });
        }

        [Test]
        public void Test_VsNumpy_Fft_Length0()
        {
            double[] values = { };
            Assert.Throws<ArgumentException>(() => { FftSharp.Transform.FFTmagnitude(values); });
        }
    }
}

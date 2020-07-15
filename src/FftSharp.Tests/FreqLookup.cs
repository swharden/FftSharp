using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FftSharp.Tests
{
    class FreqLookup
    {
        [Test]
        public void Test_Freq_Lookup()
        {
            int sampleRate = 8000;
            int pointCount = 10;

            // look up frequencies corresponding to FFT points
            double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, pointCount);

            // frequencies should span from 0 to nyquist frequency (half the sample rate)
            Assert.AreEqual(new double[] { 0, 400, 800, 1200, 1600, 2000, 2400, 2800, 3200, 3600 }, freqs);
            Assert.AreEqual(sampleRate / 2, freqs.Last() + freqs[1]);

            // frequencies should be spaced by the frequency period
            Assert.AreEqual(400, FftSharp.Transform.FFTfreqPeriod(sampleRate, pointCount));
        }
    }
}

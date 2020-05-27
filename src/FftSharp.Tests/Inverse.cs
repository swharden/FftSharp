using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Numerics;
using System.Text;

namespace FftSharp.Tests
{
    class Inverse
    {
        [Test]
        public void Test_IDFT_MatchesOriginal()
        {
            Random rand = new Random(0);
            Complex[] original = new Complex[1024];
            for (int i = 0; i < original.Length; i++)
                original[i] = new Complex(rand.NextDouble() - .5, rand.NextDouble() - .5);

            Complex[] fft = FftSharp.Transform.DFT(original);
            Complex[] ifft = FftSharp.Transform.DFT(fft, inverse: true);

            for (int i = 0; i < ifft.Length; i++)
            {
                Assert.AreEqual(original[i].Real, ifft[i].Real, 1e-6);
                Assert.AreEqual(original[i].Imaginary, ifft[i].Imaginary, 1e-6);
            }
        }
    }
}

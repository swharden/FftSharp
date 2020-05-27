using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FftSharp.Tests
{
    class Padding
    {
        [Test]
        public void Test_FFT_AllLengthInput()
        {
            for (int i = 1; i < 200; i++)
            {
                Complex[] input = new Complex[i];
                Complex[] padded = FftSharp.Transform.ZeroPad(input);
                Console.WriteLine($"Length {input.Length} -> {padded.Length}");
                FftSharp.Transform.FFT(padded);
            }
        }
    }
}

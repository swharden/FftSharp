using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FftSharp.Tests;

class Padding
{
    [Test]
    public void Test_FFT_AllLengthInput()
    {
        for (int i = 2; i < 200; i++)
        {
            System.Numerics.Complex[] input = new System.Numerics.Complex[i];
            System.Numerics.Complex[] padded = FftSharp.Pad.ZeroPad(input);
            Console.WriteLine($"Length {input.Length} -> {padded.Length}");
            FftSharp.FFT.Forward(padded);
        }
    }
}

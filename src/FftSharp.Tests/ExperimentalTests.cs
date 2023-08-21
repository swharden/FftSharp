using NUnit.Framework;
using System.Linq;

namespace FftSharp.Tests;

internal class ExperimentalTests
{
    [Test]
    [System.Obsolete]
    public void Test_Experimental_DFT()
    {
        double[] prime = { 1, 2, 3, 4, 5, 6, 7 };
        System.Numerics.Complex[] primeComplex = prime.Select(x => new System.Numerics.Complex(x, 0)).ToArray();
        System.Numerics.Complex[] result = FftSharp.Experimental.DFT(primeComplex);

        // tested with python
        double[] expectedReal = { 28.00000, -3.50000, -3.50000, -3.50000, -3.50000, -3.50000, -3.50000 };
        double[] expectedImag = { -0.00000, 7.26782, 2.79116, 0.79885, -0.79885, -2.79116, -7.26782 };

        Assert.AreEqual(expectedReal.Length, result.Length);

        for (int i = 0; i < result.Length; i++)
        {
            Assert.AreEqual(expectedReal[i], result[i].Real, 1e-5);
            Assert.AreEqual(expectedImag[i], result[i].Imaginary, 1e-5);
        }
    }
}

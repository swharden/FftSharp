using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Tests
{
    [Obsolete("Use methods which consume System.Numerics.Complex")]
    class ComplexTests
    {
        [Test]
        public void Test_Complex_ToString()
        {
            Assert.AreEqual("0+0j", new FftSharp.Complex(0, 0).ToString());
            Assert.AreEqual("3+5j", new FftSharp.Complex(3, 5).ToString());
            Assert.AreEqual("-3-5j", new FftSharp.Complex(-3, -5).ToString());
            Assert.AreEqual("3-5j", new FftSharp.Complex(3, -5).ToString());
            Assert.AreEqual("-3+5j", new FftSharp.Complex(-3, 5).ToString());
        }

        [Test]
        public void Test_Complex_Phase()
        {
            Complex v = new(42, 69);
            Assert.AreEqual(1.0240074859056494, v.Phase);
        }
    }
}

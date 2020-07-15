using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FftSharp.Tests
{
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
    }
}

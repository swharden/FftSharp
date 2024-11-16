using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FftSharp.Tests;

internal class WindowValueTests
{
    /* values in these test functions were calculated using SciPy */

    private void AssertEqual(double[] expected, double[] actual, double precision = 1e-5)
    {
        Assert.IsNotNull(expected);
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.Length, actual.Length);

        Console.WriteLine($"Expected\tActual");
        for (int i = 0; i < expected.Length; i++)
            Console.WriteLine($"{expected[i]}\t{actual[i]}");

        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i], precision);
    }

    [Test]
    public void Test_Bartlett()
    {
        double[] Known_bartlett_13 = { 0.0, 0.16666667, 0.33333333, 0.5, 0.66666667, 0.83333333, 1.0, 0.83333333, 0.66666667, 0.5, 0.33333333, 0.16666667, 0.0 };
        double[] Known_bartlett_14 = { 0.0, 0.15384615, 0.30769231, 0.46153846, 0.61538462, 0.76923077, 0.92307692, 0.92307692, 0.76923077, 0.61538462, 0.46153846, 0.30769231, 0.15384615, 0.0 };
        AssertEqual(Known_bartlett_13, new Windows.Bartlett().Create(13));
        AssertEqual(Known_bartlett_14, new Windows.Bartlett().Create(14));
    }

    [Test]
    public void Test_Blackman()
    {
        double[] Known_blackman_13 = { -0.0, 0.0269873, 0.13, 0.34, 0.63, 0.8930127, 1.0, 0.8930127, 0.63, 0.34, 0.13, 0.0269873, -0.0 };
        double[] Known_blackman_14 = { -0.0, 0.02271717, 0.10759924, 0.28205631, 0.53742158, 0.80389831, 0.97630739, 0.97630739, 0.80389831, 0.53742158, 0.28205631, 0.10759924, 0.02271717, -0.0 };
        AssertEqual(Known_blackman_13, new Windows.Blackman(.42, .5, .08).Create(13));
        AssertEqual(Known_blackman_14, new Windows.Blackman(.42, .5, .08).Create(14));
    }

    [Test]
    public void Test_Cosine()
    {
        double[] Known_cosine_13 = { 0.12053668, 0.35460489, 0.56806475, 0.74851075, 0.88545603, 0.97094182, 1.0, 0.97094182, 0.88545603, 0.74851075, 0.56806475, 0.35460489, 0.12053668 };
        double[] Known_cosine_14 = { 0.11196448, 0.33027906, 0.53203208, 0.70710678, 0.8467242, 0.94388333, 0.99371221, 0.99371221, 0.94388333, 0.8467242, 0.70710678, 0.53203208, 0.33027906, 0.11196448 };
        AssertEqual(Known_cosine_13, new Windows.Cosine().Create(13));
        AssertEqual(Known_cosine_14, new Windows.Cosine().Create(14));
    }

    [Test]
    public void Test_FlatTop()
    {
        double[] Known_flattop_13 = { -0.00042105, -0.01007669, -0.05126316, -0.05473684, 0.19821053, 0.71155038, 1.0, 0.71155038, 0.19821053, -0.05473684, -0.05126316, -0.01007669, -0.00042105 };
        double[] Known_flattop_14 = { -0.00042105, -0.00836447, -0.04346352, -0.06805774, 0.08261602, 0.5066288, 0.9321146, 0.9321146, 0.5066288, 0.08261602, -0.06805774, -0.04346352, -0.00836447, -0.00042105 };
        AssertEqual(Known_flattop_13, new Windows.FlatTop().Create(13));
        AssertEqual(Known_flattop_14, new Windows.FlatTop().Create(14));
    }

    [Test]
    public void Test_Hamming()
    {
        double[] Known_hamming_13 = { 0.08, 0.14162831, 0.31, 0.54, 0.77, 0.93837169, 1.0, 0.93837169, 0.77, 0.54, 0.31, 0.14162831, 0.08 };
        double[] Known_hamming_14 = { 0.08, 0.13269023, 0.27869022, 0.48455313, 0.70311825, 0.88431494, 0.98663324, 0.98663324, 0.88431494, 0.70311825, 0.48455313, 0.27869022, 0.13269023, 0.08 };
        AssertEqual(Known_hamming_13, new Windows.Hamming().Create(13));
        AssertEqual(Known_hamming_14, new Windows.Hamming().Create(14));
    }

    [Test]
    public void Test_HammingPeriodic()
    {
        double[] Known_hamming_periodic_13 = { 0.08, 0.13269023, 0.27869022, 0.48455313, 0.70311825, 0.88431494, 0.98663324, 0.98663324, 0.88431494, 0.70311825, 0.48455313, 0.27869022, 0.13269023 };
        double[] Known_hamming_periodic_14 = { 0.08, 0.12555432, 0.25319469, 0.43764037, 0.64235963, 0.82680531, 0.95444568, 1.0, 0.95444568, 0.82680531, 0.64235963, 0.43764037, 0.25319469, 0.12555432 };
        AssertEqual(Known_hamming_periodic_13, new Windows.HammingPeriodic().Create(13));
        AssertEqual(Known_hamming_periodic_14, new Windows.HammingPeriodic().Create(14));
    }

    [Test]
    public void Test_Hanning()
    {
        double[] Known_hanning_13 = { 0.0, 0.0669873, 0.25, 0.5, 0.75, 0.9330127, 1.0, 0.9330127, 0.75, 0.5, 0.25, 0.0669873, 0.0 };
        double[] Known_hanning_14 = { 0.0, 0.05727199, 0.21596763, 0.43973166, 0.67730244, 0.87425537, 0.98547091, 0.98547091, 0.87425537, 0.67730244, 0.43973166, 0.21596763, 0.05727199, 0.0 };
        AssertEqual(Known_hanning_13, new Windows.Hanning().Create(13));
        AssertEqual(Known_hanning_14, new Windows.Hanning().Create(14));
    }

    [Test]
    public void Test_HanningPeriodic()
    {
        double[] Known_hanning_periodic_13 = { 0.0, 0.05727199, 0.21596763, 0.43973166, 0.67730244, 0.87425537, 0.98547091, 0.98547091, 0.87425537, 0.67730244, 0.43973166, 0.21596763, 0.05727199 };
        double[] Known_hanning_periodic_14 = { 0.0, 0.04951557, 0.1882551, 0.38873953, 0.61126047, 0.8117449, 0.95048443, 1.0, 0.95048443, 0.8117449, 0.61126047, 0.38873953, 0.1882551, 0.04951557 };
        AssertEqual(Known_hanning_periodic_13, new Windows.HanningPeriodic().Create(13));
        AssertEqual(Known_hanning_periodic_14, new Windows.HanningPeriodic().Create(14));
    }

    [Test]
    public void Test_Rectangular()
    {
        double[] Known_rectangular_13 = Enumerable.Range(0, 13).Select(x => 1.0).ToArray();
        double[] Known_rectangular_14 = Enumerable.Range(0, 14).Select(x => 1.0).ToArray();
        AssertEqual(Known_rectangular_13, new Windows.Rectangular().Create(13));
        AssertEqual(Known_rectangular_14, new Windows.Rectangular().Create(14));
    }

    [Test]
    public void Test_Tukey()
    {
        double[] Known_tukey_13 = { 0.0, 0.25, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.75, 0.25, 0.0 };
        double[] Known_tukey_14 = { 0.0, 0.21596763, 0.67730244, 0.98547091, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.98547091, 0.67730244, 0.21596763, 0.0 };
        AssertEqual(Known_tukey_13, new Windows.Tukey().Create(13));
        AssertEqual(Known_tukey_14, new Windows.Tukey().Create(14));
    }

    [Test]
    public void Test_Kaiser()
    {
        double[] Known_kaiser14_13 = { 7.73e-06, 0.00258844, 0.03288553, 0.16493219, 0.4627165, 0.82808941, 1.0, 0.82808941, 0.4627165, 0.16493219, 0.03288553, 0.00258844, 7.73e-06 };
        double[] Known_kaiser14_14 = { 7.73e-06, 0.00199846, 0.02397752, 0.12057536, 0.35483775, 0.69493873, 0.96081903, 0.96081903, 0.69493873, 0.35483775, 0.12057536, 0.02397752, 0.00199846, 7.73e-06 };
        AssertEqual(Known_kaiser14_13, new Windows.Kaiser(beta: 14).Create(13));
        AssertEqual(Known_kaiser14_14, new Windows.Kaiser(beta: 14).Create(14));
    }
}

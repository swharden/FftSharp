using System;

namespace FftSharp;

/// <summary>
/// Methods for comuting the discrete Fourier transform (DFT) using Bluestein's chirp z-transform algorithm on arrays of any length
/// </summary>
public static class Bluestein
{
    /// <summary>
    /// Compute the discrete Fourier transform (DFT) of an array of any length.
    /// </summary>
    public static System.Numerics.Complex[] Forward(double[] real)
    {
        System.Numerics.Complex[] buffer = real.ToComplexArray();
        Forward(buffer);
        return buffer;
    }

    /// <summary>
    /// Compute the discrete Fourier transform (DFT) of an array of any length.
    /// </summary>
    public static void Forward(System.Numerics.Complex[] buffer)
    {
        TransformBluestein(buffer, false);
    }

    /// <summary>
    /// Compute the discrete Fourier transform (DFT) of an array of any length.
    /// </summary>
    public static System.Numerics.Complex[] Inverse(double[] real)
    {
        System.Numerics.Complex[] buffer = real.ToComplexArray();
        Inverse(buffer);
        return buffer;
    }

    /// <summary>
    /// Compute the discrete Fourier transform (DFT) of an array of any length.
    /// </summary>
    public static void Inverse(System.Numerics.Complex[] buffer)
    {
        TransformBluestein(buffer, true);
    }

    /* 
	 * Computes the discrete Fourier transform (DFT) or inverse transform of the given complex vector, storing the result back into the vector.
	 * The vector can have any length. This is a wrapper function. The inverse transform does not perform scaling, so it is not a true inverse.
	 */
    private static void Transform(System.Numerics.Complex[] vec, bool inverse)
    {
        int n = vec.Length;
        if (n == 0)
            return;
        else if ((n & (n - 1)) == 0)  // Is power of 2
            TransformRadix2(vec, inverse);
        else  // More complicated algorithm for arbitrary sizes
            TransformBluestein(vec, inverse);
    }


    /* 
	 * Computes the discrete Fourier transform (DFT) of the given complex vector, storing the result back into the vector.
	 * The vector's length must be a power of 2. Uses the Cooley-Tukey decimation-in-time radix-2 algorithm.
	 */
    private static void TransformRadix2(System.Numerics.Complex[] vec, bool inverse)
    {
        // Length variables
        int n = vec.Length;
        int levels = 0;  // compute levels = floor(log2(n))
        for (int temp = n; temp > 1; temp >>= 1)
            levels++;
        if (1 << levels != n)
            throw new ArgumentException("Length is not a power of 2");

        // Trigonometric table
        System.Numerics.Complex[] expTable = new System.Numerics.Complex[n / 2];
        double coef = 2 * Math.PI / n * (inverse ? 1 : -1);
        for (int i = 0; i < n / 2; i++)
            expTable[i] = System.Numerics.Complex.FromPolarCoordinates(1, i * coef);

        // Bit-reversed addressing permutation
        for (int i = 0; i < n; i++)
        {
            int j = ReverseBits(i, levels);
            if (j > i)
            {
                System.Numerics.Complex temp = vec[i];
                vec[i] = vec[j];
                vec[j] = temp;
            }
        }

        // Cooley-Tukey decimation-in-time radix-2 FFT
        for (int size = 2; size <= n; size *= 2)
        {
            int halfsize = size / 2;
            int tablestep = n / size;
            for (int i = 0; i < n; i += size)
            {
                for (int j = i, k = 0; j < i + halfsize; j++, k += tablestep)
                {
                    System.Numerics.Complex temp = vec[j + halfsize] * expTable[k];
                    vec[j + halfsize] = vec[j] - temp;
                    vec[j] += temp;
                }
            }
            if (size == n)  // Prevent overflow in 'size *= 2'
                break;
        }
    }

    /* 
	 * Computes the discrete Fourier transform (DFT) of the given complex vector, storing the result back into the vector.
	 * The vector can have any length. This requires the convolution function, which in turn requires the radix-2 FFT function.
	 * Uses Bluestein's chirp z-transform algorithm.
	 */
    private static void TransformBluestein(System.Numerics.Complex[] vec, bool inverse)
    {
        // Find a power-of-2 convolution length m such that m >= n * 2 + 1
        int n = vec.Length;
        if (n >= 0x20000000)
            throw new ArgumentException("Array too large");
        int m = 1;
        while (m < n * 2 + 1)
            m *= 2;

        // Trigonometric table
        System.Numerics.Complex[] expTable = new System.Numerics.Complex[n];
        double coef = Math.PI / n * (inverse ? 1 : -1);
        for (int i = 0; i < n; i++)
        {
            int j = (int)((long)i * i % (n * 2));  // This is more accurate than j = i * i
            expTable[i] = System.Numerics.Complex.Exp(new System.Numerics.Complex(0, j * coef));
        }

        // Temporary vectors and preprocessing
        System.Numerics.Complex[] avec = new System.Numerics.Complex[m];
        for (int i = 0; i < n; i++)
            avec[i] = vec[i] * expTable[i];
        System.Numerics.Complex[] bvec = new System.Numerics.Complex[m];
        bvec[0] = expTable[0];
        for (int i = 1; i < n; i++)
            bvec[i] = bvec[m - i] = System.Numerics.Complex.Conjugate(expTable[i]);

        // Convolution
        System.Numerics.Complex[] cvec = new System.Numerics.Complex[m];
        Convolve(avec, bvec, cvec);

        // Postprocessing
        for (int i = 0; i < n; i++)
            vec[i] = cvec[i] * expTable[i];
    }

    /* 
	 * Computes the circular convolution of the given complex vectors. Each vector's length must be the same.
	 */
    private static void Convolve(System.Numerics.Complex[] xvec, System.Numerics.Complex[] yvec, System.Numerics.Complex[] outvec)
    {
        int n = xvec.Length;
        if (n != yvec.Length || n != outvec.Length)
            throw new ArgumentException("Mismatched lengths");
        xvec = (System.Numerics.Complex[])xvec.Clone();
        yvec = (System.Numerics.Complex[])yvec.Clone();
        Transform(xvec, false);
        Transform(yvec, false);
        for (int i = 0; i < n; i++)
            xvec[i] *= yvec[i];
        Transform(xvec, true);
        for (int i = 0; i < n; i++)  // Scaling (because this FFT implementation omits it)
            outvec[i] = xvec[i] / n;
    }

    private static int ReverseBits(int val, int width)
    {
        int result = 0;
        for (int i = 0; i < width; i++, val >>= 1)
            result = (result << 1) | (val & 1);
        return result;
    }
}

using System;

namespace FftSharp;

/// <summary>
/// Methods for conversion to/from Mel scaling
/// </summary>
public static class Mel
{
    public static double ToFreq(double mel) => 700 * (Math.Pow(10, mel / 2595d) - 1);

    public static double FromFreq(double frequencyHz) => 2595 * Math.Log10(1 + frequencyHz / 700);

    public static double[] Scale(double[] fft, int sampleRate, int melBinCount)
    {
        double freqMax = sampleRate / 2;
        double maxMel = FromFreq(freqMax);

        double[] fftMel = new double[melBinCount];
        double melPerBin = maxMel / (melBinCount + 1);
        for (int binIndex = 0; binIndex < melBinCount; binIndex++)
        {
            double melLow = melPerBin * binIndex;
            double melHigh = melPerBin * (binIndex + 2);

            double freqLow = ToFreq(melLow);
            double freqHigh = ToFreq(melHigh);

            int indexLow = (int)(fft.Length * freqLow / freqMax);
            int indexHigh = (int)(fft.Length * freqHigh / freqMax);
            int indexSpan = indexHigh - indexLow;

            double binScaleSum = 0;
            for (int i = 0; i < indexSpan; i++)
            {
                double binFrac = (double)i / indexSpan;
                double indexScale = (binFrac < .5) ? binFrac * 2 : 1 - binFrac;
                binScaleSum += indexScale;
                fftMel[binIndex] += fft[indexLow + i] * indexScale;
            }
            fftMel[binIndex] /= binScaleSum;
        }

        return fftMel;
    }
}

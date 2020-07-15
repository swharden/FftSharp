using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FftSharp.Tests
{
    class Mel
    {
        [Test]
        public void Test_Mel_VsFFT()
        {
            double[] audio = SampleData.SampleAudio1();
            int sampleRate = 48_000;
            int melBinCount = 20;

            double[] fftMag = FftSharp.Transform.FFTmagnitude(audio);
            double[] fftMagMel = FftSharp.Transform.MelScale(fftMag, sampleRate, melBinCount);
            double fftFreqPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);

            var plt1 = new ScottPlot.Plot(800, 400);
            plt1.PlotSignal(fftMag, fftFreqPeriod);
            double maxMel = FftSharp.Transform.MelFromFreq(sampleRate / 2);
            for (int i = 0; i < melBinCount; i++)
            {
                double thisMel = (double)i / melBinCount * maxMel;
                double thisFreq = FftSharp.Transform.MelToFreq(thisMel);
                plt1.PlotVLine(thisFreq, lineWidth: 2);
            }
            plt1.YLabel("Magnitude");
            plt1.XLabel("Frequency (Hz)");
            plt1.SaveFig("audio-fft.png");


            var plt2 = new ScottPlot.Plot(800, 400);
            plt2.PlotSignal(fftMagMel);
            for (int i = 0; i < melBinCount; i++)
            {
                plt2.PlotVLine(i, lineWidth: 2);
            }
            plt2.YLabel("Magnitude");
            plt2.XLabel("Frequency (Mel)");
            plt2.SaveFig("audio-fftMel.png");
        }
    }
}

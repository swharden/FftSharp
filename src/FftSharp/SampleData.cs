using System;
using System.Numerics;

namespace FftSharp
{
    public static class SampleData
    {
        public static Complex[] Set1()
        {
            // http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf page 2
            int N = 128;
            Complex[] TD = new Complex[N];
            for (int i = 0; i < N; i++)
                TD[i] = new Complex(Math.Sin(i / Math.PI), 0.0);
            return TD;
        }

        public static Complex[] Set2()
        {
            // http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf pages 4 and 8
            int N = 128;
            Complex[] TD = new Complex[N];
            for (int i = 0; i < N; i++)
                TD[i] = new Complex(Math.Sin(i / Math.PI) + 0.333 * Math.Sin(3 * i / Math.PI), 0.0);
            return TD;
        }

        public static Complex[] Set3()
        {
            // http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf page9
            int N = 128;
            Complex[] TD = new Complex[N];
            for (int i = 0; i < N; i++)
                TD[i] = new Complex(
                    1 / 1f * Math.Sin(1 * i / Math.PI) +
                    1 / 3f * Math.Sin(3 * i / Math.PI) +
                    1 / 5f * Math.Sin(5 * i / Math.PI) +
                    1 / 7f * Math.Sin(7 * i / Math.PI)
                    , 0.0);
            return TD;
        }
    }
}

// The author (Christopher Oakley) agreed to release this code under MIT license (May 25, 2020)
// https://www.egr.msu.edu/classes/ece480/capstone/fall11/group06/style/Application_Note_ChrisOakley.pdf
// http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf

Complex[] DFT(Complex[] x)
{
    int N = x.Length;
    Complex[] X = new Complex[N];
    for (int k = 0; k < N; k++)
    {
        X[k] = new Complex(0, 0);
        for (int n = 0; n < N; n++)
        {
            Complex temp = Complex.from_polar(1, -2 * Math.PI * n * k / N);
            temp *= x[n];
            X[k] += temp;
        }
    }
    return X;
}
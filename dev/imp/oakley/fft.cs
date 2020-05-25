// The author (Christopher Oakley) agreed to release this code under MIT license (May 25, 2020)
// https://www.egr.msu.edu/classes/ece480/capstone/fall11/group06/style/Application_Note_ChrisOakley.pdf
// http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf

Complex[] FFT(Complex[] x)
{
    int N = x.Length;
    Complex[] X = new Complex[N];
    Complex[] d, D, e, E;
    if (N == 1)
    {
        X[0] = x[0];
        return X;
    }
    int k;
    e = new Complex[N / 2];
    d = new Complex[N / 2];
    for (k = 0; k < N / 2; k++)
    {
        e[k] = x[2 * k];
        d[k] = x[2 * k + 1];
    }
    D = FFT(d);
    E = FFT(e);
    for (k = 0; k < N / 2; k++)
    {
        Complex temp = Complex.from_polar(1, -2 * Math.PI * k / N);
        D[k] *= temp;
    }
    for (k = 0; k < N / 2; k++)
    {
        X[k] = E[k] + D[k];
        X[k + N / 2] = E[k] - D[k];
    }
    return X;
}
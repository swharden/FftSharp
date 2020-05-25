// The author (Christopher Oakley) agreed to release this code under MIT license (May 25, 2020)
static void Main(string[] args)
{
    int N = 128;
    complex[] TD = new complex[N];
    complex[] FD = new complex[N];

    for (int i = 0; i < N; i++)
        TD[i] = new complex(Math.Sin(i / Math.PI), 0.0);

    FD = fourier.DFT(TD);

    show_real(TD);
    show_magnitude(FD);
    Console.ReadKey();
}
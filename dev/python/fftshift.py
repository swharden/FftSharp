import scipy.fft
import numpy as np

if __name__ == "__main__":

    values = np.arange(7)
    print(values)
    print(scipy.fft.fftshift(values))
    
    values = np.arange(8)
    print(values)
    print(scipy.fft.fftshift(values))

    values = [ 1, 2, 3, 4, 5, 6, 7]
    print(values)
    print(", ".join([f"{x.real:.05f}" for x in scipy.fft.fft(values)]))
    print(", ".join([f"{x.imag:.05f}" for x in scipy.fft.fft(values)]))
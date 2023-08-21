import scipy.fft
import numpy as np

if __name__ == "__main__":

    values = np.arange(7)
    print(values)
    print(scipy.fft.fftshift(values))
    
    values = np.arange(8)
    print(values)
    print(scipy.fft.fftshift(values))

from sample import sampleAudio1
import matplotlib.pyplot as plt
import numpy as np
np.set_printoptions(suppress=True)

if __name__ == "__main__":
    fft = np.fft.fft(sampleAudio1)
    for val in fft:
        print(f"new Complex({val.real}, {val.imag}), ")
from sample import sampleAudio1
import matplotlib.pyplot as plt
import numpy as np
np.set_printoptions(suppress=True)

if __name__ == "__main__":

    fft = np.fft.fft(sampleAudio1)
    rfft = np.fft.rfft(sampleAudio1)

    with open("numpyValues.txt", "w") as f:

        f.write("\n\n### FFT ###")
        for val in fft:
            f.write(f"\nnew Complex({val.real}, {val.imag}), ")

        f.write("\n\n### FFT Magnitude ###\n")
        for val in np.absolute(fft):
            f.write(f"{val}, ")
            
        f.write("\n\n### RFFT ###")
        for val in rfft:
            f.write(f"\nnew Complex({val.real}, {val.imag}), ")

        f.write("\n\n### RFFT Magnitude ###\n")
        for val in np.absolute(rfft):
            f.write(f"{val}, ")
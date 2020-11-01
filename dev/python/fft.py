"""
This script analyzes standard test audio using Numpy's FFT
methods to compare the output to FftSharp's output.
"""

from os.path import dirname, basename, join, abspath
import matplotlib.pyplot as plt
import numpy as np
np.set_printoptions(suppress=True)


def readData(filename: str) -> np.array:
    filePath = join(dirname(__file__), f"../data/{filename}")
    with open(filePath) as f:
        lines = f.readlines()
    values = [float(x) for x in lines if x[0] in "+-"]
    values = np.array(values)
    assert len(values) == 512
    assert values[2] == 1.44
    return values


def saveData(filename: str, values: np.ndarray):
    filePath = join(dirname(__file__), f"../data/{filename}")
    filePath = abspath(filePath)
    if (basename(filePath).lower() == "sample.txt"):
        raise Exception("dont overwrite sample values")
    lines = [str(x) for x in values]
    assert len(lines) == len(values)
    with open(filePath, 'w') as f:
        f.write("\n".join(lines))
    print("saved", filePath)


if __name__ == "__main__":

    values = readData("sample.txt")
    assert np.sum(values) == 71.52
    assert np.sum(np.sin(values)) == 10.417026634786811

    sampleRate = 48_000
    samplePeriod = 1.0 / sampleRate

    N = len(values)
    fft = np.fft.fft(values)
    fftFreq = np.fft.fftfreq(N, samplePeriod)
    fftReal = np.fft.rfft(values)
    fftMag = np.abs(fftReal) * 2 / N
    fftDB = 20 * np.log10(fftMag)

    saveData("fft.txt", fft)
    saveData("fftFreq.txt", fftFreq)
    saveData("fftReal.txt", fftReal)
    saveData("fftMag.txt", fftMag)
    saveData("fftDB.txt", fftDB)

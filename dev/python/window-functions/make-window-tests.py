import numpy as np
import scipy.signal
import scipy.signal.windows


def printCsArray(name: str, values: np.ndarray, precision: int = 8):
    arrayContents = ", ".join([str(np.round(x, precision)) for x in values])
    assignmentString = f"public double[] Known_{name}_{len(values)} = {{ {arrayContents} }};"
    print(assignmentString)


def printCsArrays(count: int):
    printCsArray("bartlett", scipy.signal.windows.bartlett(count))
    printCsArray("blackman", scipy.signal.windows.blackman(count))
    printCsArray("cosine", scipy.signal.windows.cosine(count))
    printCsArray("flattop", scipy.signal.windows.flattop(count))
    printCsArray("hamming",  scipy.signal.windows.hamming(count))
    printCsArray("hanning",  scipy.signal.windows.hann(count))
    printCsArray("kaiser14", scipy.signal.windows.kaiser(count, beta=14))
    printCsArray("tukey",  scipy.signal.windows.tukey(count))


if __name__ == "__main__":
    printCsArrays(13)
    printCsArrays(14)

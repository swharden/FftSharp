import numpy as np
import scipy.signal
import scipy.signal.windows


def printCsArray(name: str, values: np.ndarray, precision: int = 8):
    arrayContents = ", ".join([str(np.round(x, precision)) for x in values])
    assignmentString = f"double[] {name}_{len(values)} = {{ {arrayContents} }}"
    print(assignmentString)


if __name__ == "__main__":
    printCsArray("bartlett", scipy.signal.windows.bartlett(13))
    printCsArray("blackman", scipy.signal.windows.blackman(13))
    printCsArray("cosine", scipy.signal.windows.cosine(13))
    printCsArray("flattop", scipy.signal.windows.flattop(13))
    printCsArray("hamming",  scipy.signal.windows.hamming(13))
    printCsArray("hanning",  scipy.signal.windows.hanning(13))
    printCsArray("kaiser_14", scipy.signal.windows.kaiser(13, beta=14))
    printCsArray("rectangular", np.ones(13))
    printCsArray("tukey",  scipy.signal.windows.tukey(13))
    #printCsArray("welch", scipy.signal.welch())
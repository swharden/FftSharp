import numpy as np


def printCsArray(name: str, values: np.ndarray, precision: int = 8):
    arrayContents = ", ".join([str(np.round(x, precision)) for x in values])
    assignmentString = f"double[] {name}_{len(values)} = {{ {arrayContents} }}"
    print(assignmentString)


if __name__ == "__main__":

    # https://numpy.org/doc/stable/reference/generated/numpy.hanning.html
    printCsArray("bartlett", np.bartlett(13))
    printCsArray("blackman", np.blackman(13))
    # printCsArray("cosine", np.(13))
    # printCsArray("flattop", np.(13))
    printCsArray("hamming", np.hamming(13))
    printCsArray("hanning", np.hanning(13))
    printCsArray("kaiser_14", np.kaiser(13, 14))
    # printCsArray("rectangular", np.(13))
    # printCsArray("tukey", np.(13))
    # printCsArray("welch", np.(13))

    print("DONE")

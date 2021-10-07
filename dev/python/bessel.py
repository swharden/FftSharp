import numpy as np
xs = np.arange(20)
print(xs)
print(np.i0(xs))

import matplotlib.pyplot as plt
window = np.kaiser(50, 14)
print(window)
plt.plot(window)
plt.show()
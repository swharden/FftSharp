# ---- for GNU gcc ----

CC = gcc

CFLAGS = -Wall

OFLAGS_FFT = -O6 -ffast-math
OFLAGS_PI = -O6 -ffast-math

# ---- for SUN WS cc ----
#
#CC = cc
#
#CFLAGS = 
#
#OFLAGS_FFT = -fast -xO5
#OFLAGS_PI = -fast -xO5




all: pi_fft4g pi_fft8g pi_fftsg


pi_fft4g : pi_fft.o fft4g.o
	$(CC) pi_fft.o fft4g.o -lm -o pi_fft4g

pi_fft8g : pi_fft.o fft8g.o
	$(CC) pi_fft.o fft8g.o -lm -o pi_fft8g

pi_fftsg : pi_fft.o fftsg.o
	$(CC) pi_fft.o fftsg.o -lm -o pi_fftsg


pi_fft.o : pi_fft.c
	$(CC) $(CFLAGS) $(OFLAGS_PI) -c pi_fft.c -o pi_fft.o


fft4g.o : ../fft4g.c
	$(CC) $(CFLAGS) $(OFLAGS_FFT) -c ../fft4g.c -o fft4g.o

fft8g.o : ../fft8g.c
	$(CC) $(CFLAGS) $(OFLAGS_FFT) -c ../fft8g.c -o fft8g.o

fftsg.o : ../fftsg.c
	$(CC) $(CFLAGS) $(OFLAGS_FFT) -c ../fftsg.c -o fftsg.o




clean:
	rm -f *.o


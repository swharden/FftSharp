NMAX_FLAGS = -DNMAX=65536 -DNMAXSQRT=256

# ---- for GNU gcc ----

CC = gcc

CFLAGS = -Wall

OFLAGS = -O2

# ---- for SUN WS cc ----
#
#CC = cc
#
#CFLAGS = 
#
#OFLAGS = -xO2




all: test4g test8g testsg test4g_h test8g_h testsg_h


test4g : testxg.o fft4g.o
	$(CC) testxg.o fft4g.o -lm -o test4g

test8g : testxg.o fft8g.o
	$(CC) testxg.o fft8g.o -lm -o test8g

testsg : testxg.o fftsg.o
	$(CC) testxg.o fftsg.o -lm -o testsg

test4g_h : testxg_h.o fft4g_h.o
	$(CC) testxg_h.o fft4g_h.o -lm -o test4g_h

test8g_h : testxg_h.o fft8g_h.o
	$(CC) testxg_h.o fft8g_h.o -lm -o test8g_h

testsg_h : testxg_h.o fftsg_h.o
	$(CC) testxg_h.o fftsg_h.o -lm -o testsg_h


testxg.o : testxg.c
	$(CC) $(CFLAGS) $(OFLAGS) $(NMAX_FLAGS) -c testxg.c -o testxg.o

testxg_h.o : testxg_h.c
	$(CC) $(CFLAGS) $(OFLAGS) $(NMAX_FLAGS) -c testxg_h.c -o testxg_h.o


fft4g.o : ../fft4g.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fft4g.c -o fft4g.o

fft8g.o : ../fft8g.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fft8g.c -o fft8g.o

fftsg.o : ../fftsg.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fftsg.c -o fftsg.o

fft4g_h.o : ../fft4g_h.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fft4g_h.c -o fft4g_h.o

fft8g_h.o : ../fft8g_h.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fft8g_h.c -o fft8g_h.o

fftsg_h.o : ../fftsg_h.c
	$(CC) $(CFLAGS) $(OFLAGS) -c ../fftsg_h.c -o fftsg_h.o




clean:
	rm -f *.o


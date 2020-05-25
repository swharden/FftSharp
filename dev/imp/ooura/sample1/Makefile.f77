# ---- for GNU g77 ----

F77 = g77

FFLAGS = -Wall

OFLAGS = -O2

# ---- for SUN WS f77 ----
#
#F77 = f77
#
#FFLAGS = 
#
#OFLAGS = -xO2




all: test4g_f test8g_f testsg_f


test4g_f : testxg_f.o fft4g_f.o
	$(F77) testxg_f.o fft4g_f.o -o test4g_f

test8g_f : testxg_f.o fft8g_f.o
	$(F77) testxg_f.o fft8g_f.o -o test8g_f

testsg_f : testxg_f.o fftsg_f.o
	$(F77) testxg_f.o fftsg_f.o -o testsg_f


testxg_f.o : testxg.f
	$(F77) $(FFLAGS) $(OFLAGS) -c testxg.f -o testxg_f.o


fft4g_f.o : ../fft4g.f
	$(F77) $(FFLAGS) $(OFLAGS) -c ../fft4g.f -o fft4g_f.o

fft8g_f.o : ../fft8g.f
	$(F77) $(FFLAGS) $(OFLAGS) -c ../fft8g.f -o fft8g_f.o

fftsg_f.o : ../fftsg.f
	$(F77) $(FFLAGS) $(OFLAGS) -c ../fftsg.f -o fftsg_f.o




clean:
	rm -f *.o


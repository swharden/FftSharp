! test of fft*g.f
!
!
      program main
      integer nmax, nmaxsqrt
      parameter (nmax = 32768)
      parameter (nmaxsqrt = 128)
      integer n, ip(0 : nmaxsqrt + 1)
      real*8 a(0 : nmax), w(0 : nmax * 5 / 4 - 1), t(0 : nmax / 2), 
     &    err, errorcheck
!
      write (*, *) 'data length n=? (must be 2**m)'
      read (*, *) n
      ip(0) = 0
!
!   check of CDFT
      call putdata(0, n - 1, a)
      call cdft(n, 1, a, ip, w)
      call cdft(n, -1, a, ip, w)
      err = errorcheck(0, n - 1, 2.0d0 / n, a)
      write (*, *) 'cdft err= ', err
!
!   check of RDFT
      call putdata(0, n - 1, a)
      call rdft(n, 1, a, ip, w)
      call rdft(n, -1, a, ip, w)
      err = errorcheck(0, n - 1, 2.0d0 / n, a)
      write (*, *) 'rdft err= ', err
!
!   check of DDCT
      call putdata(0, n - 1, a)
      call ddct(n, 1, a, ip, w)
      call ddct(n, -1, a, ip, w)
      a(0) = a(0) * 0.5d0
      err = errorcheck(0, n - 1, 2.0d0 / n, a)
      write (*, *) 'ddct err= ', err
!
!   check of DDST
      call putdata(0, n - 1, a)
      call ddst(n, 1, a, ip, w)
      call ddst(n, -1, a, ip, w)
      a(0) = a(0) * 0.5d0
      err = errorcheck(0, n - 1, 2.0d0 / n, a)
      write (*, *) 'ddst err= ', err
!
!   check of DFCT
      call putdata(0, n, a)
      a(0) = a(0) * 0.5d0
      a(n) = a(n) * 0.5d0
      call dfct(n, a, t, ip, w)
      a(0) = a(0) * 0.5d0
      a(n) = a(n) * 0.5d0
      call dfct(n, a, t, ip, w)
      err = errorcheck(0, n, 2.0d0 / n, a)
      write (*, *) 'dfct err= ', err
!
!   check of DFST
      call putdata(1, n - 1, a)
      call dfst(n, a, t, ip, w)
      call dfst(n, a, t, ip, w)
      err = errorcheck(1, n - 1, 2.0d0 / n, a)
      write (*, *) 'dfst err= ', err
!
      end
!
!
      subroutine putdata(nini, nend, a)
      integer nini, nend, j, seed
      real*8 a(0 : *), drnd
      seed = 0
      do j = nini, nend
          a(j) = drnd(seed)
      end do
      end
!
!
      function errorcheck(nini, nend, scale, a)
      integer nini, nend, j, seed
      real*8 scale, a(0 : *), drnd, err, e, errorcheck
      err = 0
      seed = 0
      do j = nini, nend
          e = drnd(seed) - a(j) * scale
          err = max(err, abs(e))
      end do
      errorcheck = err
      end
!
!
! random number generator, 0 <= drnd < 1
      real*8 function drnd(seed)
      integer seed
      seed = mod(seed * 7141 + 54773, 259200)
      drnd = seed * (1.0d0 / 259200.0d0)
      end
!

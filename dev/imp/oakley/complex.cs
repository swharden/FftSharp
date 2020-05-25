// The author (Christopher Oakley) agreed to release this code under MIT license (May 25, 2020)
// https://www.egr.msu.edu/classes/ece480/capstone/fall11/group06/style/Application_Note_ChrisOakley.pdf
// http://csclab.murraystate.edu/~bob.pilgrim/565/lectures/lecture_08.pdf
class complex
{
    public double real = 0.0;
    public double imag = 0.0;
    //Empty constructor
    public complex()
    {
    }
    public complex(double real, double im)
    {
        this.real = real;
        this.imag = imag;
    }
    public string ToString()
    {
        string data = real.ToString() + " " + imag.ToString() + "i";
        return data;
    }
    public static complex from_polar(double r, double radians)
    {
        complex data = new complex(r * Math.Cos(radians),
        r * Math.Sin(radians));
        return data;
    }
    public static complex operator +(complex a, complex b)
    {
        complex data = new complex(a.real + b.real, a.imag + b.imag);
        return data;
    }
    public static complex operator -(complex a, complex b)
    {
        complex data = new complex(a.real - b.real, a.imag - b.imag);
        return data;
    }
    public static complex operator *(complex a, complex b)
    {
        complex data = new complex((a.real * b.real) - (a.imag * b.imag),
        (a.real * b.imag) + (a.imag * b.real));
        return data;
    }
    public double magnitude
    {
        get
        {
            return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imag, 2));
        }
    }
    public double phase
    {
        get
        {
            return Math.Atan(imag / real);
        }
    }
}

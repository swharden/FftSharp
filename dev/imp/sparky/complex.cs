/********************************************
*  Author Name : Surujlal 'Sparky' Dasrath  *
*  Date        : July 10 2007               *
*  Updated On  : November 16 2007           * 
*  Contact     : sdasrath@gmail.com         *
*  License     : MIT                        *
* *******************************************
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sparky
{
    #region Summary of the Complex structure
    /* Version: 1.0.1
    * 1. Added a reference to System.Drawing namespace
    *      to access PointF structure
    * 2. Added Methods:
    *  (i)  ConvertToPoint()
    *  (ii) ConvertToComplex(PointF floatingPoint)
    *
    * Version: 1.0.0
    * Member List:
    *  [1] TYPES
    *      -> Complex
    *  [2] PRIVATE FIELDS
    *      -> real - represents the Real part of a complex number
    *      -> imag - represents the Imaginary part of a complex number
    *  [3] ACCESSOR METHODS/ PROPERTIES
    *      -> Real - read/write property that accesses the real part of a Complex object
    *      -> Imaginary - read/write property that accesses the imaginary part of a Complex object
    *  [4] CONSTRUCTORS
    *      -> Complex(Double real, Double imag) - initialize an instance of a Complex object
    *  [5] OPERATOR OVERLOAD
    *      -> -        : negation
    *      -> -, +    : add and subtract Complex objects
    *                  (i)   complex ( -,+ ) complex
    *                  (ii)  complex ( -,+ ) real
    *                  (iii) real    ( -,+ ) complex
    *      -> *, /    : multiply and divide Complex objects
    *                  (i)   complex ( *,/ ) complex
    *                  (ii)  complex ( *,/ ) real
    *                  (iii) real    ( *,/ ) complex
    *      -> ==, !=   : tests for equality/inequality of
    *                      Complex objects
    *  [6] METHODS
    *      -> Conjugate() - Calculates the complex conjugate of a Complex number
    *      -> Inverse() - Computes the inverse of the current instance of a complex number
    *      -> Normalize() - Method to normalize a complex number
    *      -> Modulus() - Computes the magnitude/modulus/absolute value of a complex number
    *      -> ModulusSquared() - Computes the square of the modulus
    *      -> AngleRad() - Computes the angle/argument in radians of a complex number
    *      -> AngleDeg() - Computes the angle/argument in degrees of a complex number
    *      -> Rect2Polar() - Converts a complex number from rectangular form to polar form
    *      -> Polar2Rect() - Converts a complex number from polar form to rectangular form
    *      -> ConvertToPointF() - Converts a Complex type to a an ordered pair of floating-point x-and y-coordinates
    *      -> ConvertToComplex(PointF floatingPoint) - Converts a floating-point ordered pair to a Complex structure
    *      -> ToString() - Override the ToString() method to display a complex number in the traditional format
    *      -> Boolean Equals(Object obj) - Override of the System.Object.Equals() method
    *      -> GetHashCode()- Override of the System.Object.GetHashCode() method
    * */
    #endregion  // End region definition of Summary

    #region Start Struct Complex
    /// <summary>
    /// Implementation of the Complex type of the form A + iB
    /// where A, B are real numbers and i^2 = -1
    /// A structure is used since most of the computations involving
    /// complex numbers are very similar to primitive types
    /// </summary>
    /// <example>Examples of complex numbers are: 1 + i; 2 - 3i; -3 + i*PI </example>
    public struct Complex
    {   //Begin definition of the Complex structure

        #region PRIVATE FIELDS
        /// <summary>
        /// Private field for the real part of a complex number of the form: Real + i * Imaginary
        /// </summary>
        private Double real;    //  represents the Real part of a complex number
        /// <summary>
        /// Private field for the imaginary part of a complex number of the form: Real + i * Imaginary
        /// </summary>
        private Double imag;    //  represents the Real part of a complex number
        #endregion  // End region definition for Private Fields

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the Complex structure with the specified
        /// real and imaginary parts.
        /// Note: A struct is a value type and value types cannot have parameterless default constructors
        /// </summary>
        /// <param name="real">Real part of a complex number</param>
        /// <param name="imag">Imaginary part of a complex number</param>
        /// <example>Complex(2,3) will create the complex number: 2 + 3i</example>
        public Complex(Double real, Double imag)
        {   //  Begin default constructor
            this.real = real;
            this.imag = imag;
        }   //  end default constructor
        #endregion  // End region defintion for Constructors

        #region ACCESSOR METHODS/ PROPERTIES
        /// <summary>
        /// Property that provides access to the real part of a complex number
        /// so that it can either be retreived or modified
        /// </summary>
        /// <example>
        ///    <code>
        ///        Complex C = new Complex(2,3);
        ///        Double real = C.Real;
        ///        Console.WriteLine("The real part of {0} is {1}", C.ToString(), real);
        ///    </code>
        /// The output will be: The real part of 2 + 3i is 2
        /// </example>
        public Double Real
        {
            get { return this.real; }
            set { this.real = value; }
        }
        /// <summary>
        /// Property that provides access to the imaginary part of a complex number
        /// so that it can either be retreived or modified
        /// </summary>
        /// <example>
        ///    <code>
        ///        Complex C = new Complex(2,3);
        ///        Double imag = C.Imaginary;
        ///        Console.WriteLine("The imaginary part of {0} is {1}", C.ToString(), imag);
        ///    </code>
        /// The output will be: The imaginary part of 2 + 3i is 3
        /// </example>
        public Double Imaginary
        {
            get { return this.imag; }
            set { this.imag = value; }
        }
        #endregion  // End region definition for Properties

        #region OPERATOR OVERLOADS
        //====================
        //  OPERATOR OVERLOADS
        //====================
        #region + Operator
        //[1]   + operator
        //  4 cases
        //      (i)    0 + complex (Additive identity)
        //      (ii)    complex + complex
        //      (iii)   complex + real
        //      (iiv)   real + complex
        /// <summary>
        /// Overloaded + operator which returns the Additive Identity for a complex number C = 0 + C
        /// </summary>
        /// <param name="C">The current instance of the complex number</param>
        /// <returns>Returns the current complex number</returns>
        public static Complex operator +(Complex C)// (i) Additive Identity; 0 + complex
        {
            return C;
        }
        /// <summary>
        /// Overload the + operator and find the sum of two complex numbers
        /// </summary>
        /// <param name="C1">First of two complex numbers to be added together</param>
        /// <param name="C2">Second of two complex numbers to be added together</param>
        /// <returns>Returns the complex result of the summation of two complex numbers</returns>
        /// <example>
        ///    <code>
        ///        Complex c1 = new Complex(2,3);
        ///        Complex c2 = new Complex(3,4);
        ///        Complex result = c1 + c2;
        ///        Console.WriteLine("({0}) + ({1}) = {2}", c1, c2, result);
        ///    </code>
        ///  The result will be: (2 + 3i) + (3 + 4i) = 5 + 7i
        /// </example>
        public static Complex operator +(Complex C1, Complex C2)// (ii) complex+complex
        {
            return new Complex(C1.real + C2.real, C1.imag + C2.imag);
        }
        /// <summary>
        /// Overload the + operator and find the sum of a complex and a real number
        /// </summary>
        /// <param name="C">First (complex) of two numbers in the sum</param>
        /// <param name="num">Second (real) of two numbers in the sum</param>
        /// <returns>Returns the complex result of the summation of a complex number and a real number</returns>
        /// <example>
        ///    <code>
        ///        Double num = 10;
        ///        Complex c1 = new Complex(2,3);
        ///        Complex result = c1 + num;
        ///        Console.WriteLine("({0}) + ({1}) = {2}", c1, num, result);
        ///    </code>
        ///    The result will be: (2 + 3i) + (10) = 12 + 3i
        /// </example>
        public static Complex operator +(Complex C, Double num)// (iii) complex+real
        {
            return new Complex(C.real + num, C.imag);
        }
        /// <summary>
        /// Overload the + operator and find the sum of a real and complex number
        /// </summary>
        /// <param name="num">First (real) of two numbers of the sum</param>
        /// <param name="C">Second (complex) of two numbers of the sum</param>
        /// <returns>Returns the complex result of the summation of a real number and a complex number</returns>
        public static Complex operator +(Double num, Complex C)// (iv) real+complex
        {
            return new Complex(C.real + num, C.imag);
        }
        #endregion  // End region definition for + operator

        #region - Operator
        //[2]   - operator
        //  4 cases
        //      (i)    Additive Inverse or negation
        //      (ii)    complex - complex
        //      (iii)   complex - real
        //      (iv)    real - complex
        /// <summary>
        /// Returns the additive inverse (negation) of a complex number
        /// </summary>
        /// <param name="C">Current instance of a complex number</param>
        /// <returns>Returns the negative value of the current instance</returns>
        public static Complex operator -(Complex C)// (i) Additive Inverse
        {
            return new Complex(-C.real, -C.imag);
        }
        /// <summary>
        /// Overload the - operator and find the difference of two complex numbers
        /// </summary>
        /// <param name="C1">First of two complex numbers to be subtracted</param>
        /// <param name="C2">Second of two complex numbers to be subtracted</param>
        /// <returns>Returns the complex result of the difference of two complex numbers</returns>
        /// <example>
        ///    <code>
        ///        Complex c1 = new Complex(2,3);
        ///        Complex c2 = new Complex(3,4);
        ///        Complex result = c1 - c2;
        ///        Console.WriteLine("({0}) - ({1}) = {2}", c1, c2, result);
        ///    </code>
        /// The result will be: (2 + 3i) - (3 + 4i) = -1 + -1i
        /// </example>
        public static Complex operator -(Complex C1, Complex C2)// (ii) complex-complex
        {
            return new Complex(C1.real - C2.real, C1.imag - C2.imag);
        }
        /// <summary>
        /// Overload the - operator and find the difference between a complex and a real number
        /// </summary>
        /// <param name="C">First (complex) of the two numbers to be subtracted</param>
        /// <param name="num">Second (real) of the two numbers to be subtracted</param>
        /// <returns>Returns the complex result of the difference between a complex and a real number</returns>
        /// <example>
        ///    <code>
        ///        Double num = 10;
        ///        Complex c1 = new Complex(2,3);
        ///        Complex result = c1 - num;
        ///        Console.WriteLine("({0}) - ({1}) = {2}", c1, num, result);
        ///    </code>
        ///    The result will be: (2 + 3i) - (10) = -8 + 3i
        /// </example>
        public static Complex operator -(Complex C, Double num)// (iii) complex-real
        {
            return new Complex(C.real - num, C.imag);
        }
        /// <summary>
        /// Overload the - operator and find the difference between a real and a complex number
        /// </summary>
        /// <param name="num">First (real) of two numbers to be subtracted</param>
        /// <param name="C">Second (complex) of two numbers to be subtracted</param>
        /// <returns>Returns the complex result of the difference between a real and complex number</returns>
        public static Complex operator -(Double num, Complex C)// (iv) real-complex
        {
            return new Complex(num - C.real, C.imag * -1);
        }
        #endregion  // End region definiton for - Operator

        #region * Operator
        //[3]   * operator
        //  3 cases
        //      (i)   complex * complex
        //      (ii)  complex * real
        //      (iii) real * complex

        /// <summary>
        /// Overload the * operator and perform multiplication on two complex numbers
        /// </summary>
        /// <param name="C1">First of two complex numbers to be multiplied</param>
        /// <param name="C2">Second of two complex numbers to be multiplied</param>
        /// <returns>Returns the complex result of the product of two complex numbers</returns>
        /// <example>
        ///    <code>
        ///    Complex c1 = new Complex(2,3);
        ///    Complex c2 = new Complex(3,4);
        ///    Complex result = c1 * c2;
        ///    Console.WriteLine("({0}) - ({1}) = {2}", c1, c2, result);
        ///    </code>
        ///    The result will be: (2 + 3i) * (3 + 4i) = -6 + 17i
        /// </example>
        public static Complex operator *(Complex C1, Complex C2)// (i) complex*complex
        {
            Double r = (C1.real * C2.real) - (C1.imag * C2.imag);
            Double i = (C1.imag * C2.real) + (C1.real * C2.imag);
            return new Complex(r, i);
        }
        /// <summary>
        /// Overload the * operator and perform multiplication of a complex number and a real number
        /// </summary>
        /// <param name="C">First (complex) of two numbers to be multiplied</param>
        /// <param name="num">Second (real) of two numbers to be multiplied</param>
        /// <returns>Returns the complex result of the product of a complex and a real number</returns>
        /// <example>
        ///    <code>
        ///        Double num = 10;
        ///        Complex c1 = new Complex(2,3);
        ///        Complex result = c1 * num;
        ///        Console.WriteLine("({0}) * ({1}) = {2}", c1, num, result);
        ///    </code>
        ///    The result will be: (2 + 3i) * (10) = 20 + 30i
        /// </example>
        public static Complex operator *(Complex C, Double num)// (ii) complex*real
        {
            return new Complex(C.real * num, C.imag * num);
        }
        /// <summary>
        /// Overload the * operator perform multiplication of a real number and a complex number
        /// </summary>
        /// <param name="num">First (real) of two numbers to be multiplied</param>
        /// <param name="C">Second (complex) of two numbers to be multiplied</param>
        /// <returns>Returns the complex result of the product a real and a complex number</returns>
        public static Complex operator *(Double num, Complex C)// (iii) real*complex
        {
            return new Complex(C.real * num, C.imag * num);
        }
        #endregion  // End region defintion for * Operator

        #region / Operator
        //[4]   / operator
        //  3 cases
        //      (i)   complex / complex
        //      (ii)  complex / real
        //      (iii) real / complex

        /// <summary>
        /// Overload the / operator and perform division on two complex numbers
        /// </summary>
        /// <param name="C1">First (numerator) complex number of the division</param>
        /// <param name="C2">Second (denominator) complex number of the division</param>
        /// <returns>Returns the complex result of the division of two complex numbers: complex/complex
        /// </returns>
        /// <example>
        ///    <code>
        ///        Complex c1 = new Complex(2,3);
        ///        Complex c2 = new Complex(3,4);
        ///        Complex result = c1 / c2;
        ///        Console.WriteLine("({0}) / ({1}) = {2}", c1, c2, result);
        ///    </code>
        ///    The result of will be: (2 + 3i) / (3 + 4i) = 0.0288 + 0.0016i
        /// </example>
        public static Complex operator /(Complex C1, Complex C2)// (i) complex/complex
        {
            Double numR = (C1.real * C2.real) + (C1.imag * C2.imag);
            Double numI = (C1.imag * C2.real) - (C1.real * C2.imag);
            Double den = (C2.real * C2.real) + (C2.imag * C2.imag);
            try
            {
                numR = numR / den;
                numI = numI / den;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            // if no errors, continue to process division
            return new Complex(numR / den, numI / den);
        }

        /// <summary>
        /// Overload the / operator and perform the division of a complex and real number
        /// </summary>
        /// <param name="C">First (numerator) of two numbers to be divided</param>
        /// <param name="num">Second (denominator) of two numbers to be divided</param>
        /// <returns>Returns the complex result of the division of a complex number by a real number: complex/real
        /// </returns>
        /// <example>
        ///    <code>
        ///        Double num = 10;
        ///        Complex c1 = new Complex(2,3);
        ///        Complex result = c1 / num;
        ///        Console.WriteLine("({0}) / ({1}) = {2}", c1, num, result);
        ///    </code>
        ///    The result of this will be: (2 + 3i) / (10) = 0.2 + 0.3i
        /// </example>
        public static Complex operator /(Complex C, Double num)// (ii) complex/real
        {
            Double r, i;
            try
            {
                r = C.real / num;
                i = C.imag / num;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            // if no errors, continue to process division
            return new Complex(C.real / num, C.imag / num);

        }

        /// <summary>
        /// Overload the / operator and perform division of a real and complex number
        /// </summary>
        /// <param name="num">First (numerator) of two numbers to be divided</param>
        /// <param name="C">Second (denominator) of two numbers to be divided</param>
        /// <returns>Returns the complex result of a real number divided by a complex number: real/complex
        /// </returns>
        /// <example>
        ///    <code>
        ///    Double num = 10;
        ///    Complex c1 = new Complex(2,3);
        ///    Complex result = num / c1;
        ///    Console.WriteLine("({0}) / ({1}) = {2}", num, c1, result);
        ///    </code>
        ///    The result will be: (10) / (2 + 3i) = 0.118343195266272 + -0.177514792899408i
        /// </example>
        public static Complex operator /(Double num, Complex C)// (iii) real/complex
        {
            Double numR = C.real * num;
            Double numI = C.imag * num * -1; // multiply imaginary part by -1 to get conjugate
            Double den = (C.real * C.real) + (C.imag * C.imag);
            try
            {
                numR = numR / den;
                numI = numI / den;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            // if no errors, continue to process division
            return new Complex(numR / den, numI / den);
        }
        #endregion  // End region definition for / Operator

        #region == and != Operator
        /// <summary>
        /// Overload the === operator to test equality of two complex numbers
        /// </summary>
        /// <param name="C1">First (left-hand side) parameter to test</param>
        /// <param name="C2">Second (right-hand side) parameter to test</param>
        /// <returns>Returns true if the two complex numbers are equal</returns>
        public static Boolean operator ==(Complex C1, Complex C2)
        {
            return ((C1.real == C2.real) && (C1.imag == C2.imag));
        }

        /// <summary>
        /// Overload the != operator to test whether two complex numbers are not equal to each other
        /// </summary>
        /// <param name="C1">First (left-hand side) parameter to test</param>
        /// <param name="C2">Second (right-hand side) parameter to test</param>
        /// <returns>Returns true if the two complex numbers are not equal to each othr</returns>
        public static Boolean operator !=(Complex C1, Complex C2)
        {
            return ((C1.real != C2.real) || (C1.imag != C2.imag));
        }
        #endregion  // End region defintion for == and != Operators
        #endregion  // End region defintion for Operator Overloads

        #region METHODS
        //=========
        //  METHODS
        //=========

        /// <summary>
        /// Computes the complex conjugate of the current instance of a complex number
        /// </summary>
        /// <returns>Returns the complex conjugate of a complex number</returns>
        public Complex Conjugate()  //  Complex conjugate
        {//start Conjugate()
            Complex C;
            C.real = this.real;
            C.imag = -this.imag;
            return C;
        }//end Conjugate()

        /// <summary>
        /// Computes the inverse of the current instance of a complex number
        /// </summary>
        /// <returns>Returns the inverse of a complex number</returns>
        public Complex Inverse()
        {//start Inverse()
            Complex C;
            C.real = this.real;
            C.imag = this.imag * -1;
            Double den = (C.real * C.real) + (C.imag * C.imag);
            try
            {
                C.real = C.real / den;
                C.imag = C.imag / den;
                return C;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }//end Inverse()

        /// <summary>
        /// Method to normalize a complex number
        /// </summary>
        /// <returns>Returns the normalized version of a complex number</returns>
        public Complex Normalize()
        {//start Normalize()
            try
            {
                Double den = this.Modulus();
                Complex C;
                C.real = this.real / den;
                C.imag = this.imag / den;
                return C;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }//end Normalize()

        /// <summary>
        /// Computes the magnitude/modulus/absolute value of a complex number
        /// </summary>
        /// <returns>Returns the modulus of a complex number</returns>
        public Double Modulus()
        {//start Magnitude()
            Double mag = Math.Sqrt((this.real * this.real) + (this.imag * this.imag));
            return mag;
        }//end Magnitude

        /// <summary>
        /// Computes the square of the modulus
        /// </summary>
        /// <returns>Returns the square of the modulus</returns>
        public Double ModulusSquared()
        {//start ModulusSquared()
            return (this.Modulus() * this.Modulus());
        }//end ModulusSquared()

        /// <summary>
        /// Computes the angle/argument in radians of a complex number
        /// </summary>
        /// <returns>Returns the angle (in radians) of a complex number</returns>
        public Double AngleRad()
        {//start AngleRad()
            Double angle = Math.Atan2(this.imag, this.real); // the Atan2() function is used to find the angle in the correct quadrant of the unit circle
            return angle;
        }//end AngleRad()

        /// <summary>
        /// Computes the angle/argument in degrees of a complex number
        /// </summary>
        /// <returns>Returns the angle (in degrees) of a complex number</returns>
        public Double AngleDeg()
        {//start AngleDeg()
            Double angle = Math.Atan2(this.imag, this.real);
            return ((360 * angle) / (2 * Math.PI));
        }//end AngleDeg()

        /// <summary>
        /// Converts a complex number from rectangular form to polar form
        /// </summary>
        /// <returns>Returns the polar form  of a complex number given in rectangular form: a + i*b to r * exp(i * theta) where r is the magnitude of a + i*b and theta is the angle in radians of a + i*b</returns>
        public Complex Rect2Polar()
        {//start Rect2Polar()
            Double magnitude = this.Modulus();
            Double angle = this.AngleRad();
            Complex C;
            C.real = magnitude;
            C.imag = angle;
            return C;
        }//end Rect2Polar()

        /// <summary>
        /// Converts a complex number from polar form to rectangular form
        /// </summary>
        /// <returns>Returns the rectangular form of a complex number given in polar form:  r * exp(i * theta) to a + i*b where a is r * cos(theta) and b is r * sin(theta)</returns>
        public Complex Polar2Rect()
        {//start Polar2Rect()
            Double magnitude = this.real;
            Double angle = this.imag; //returns the angle in Radians of a Complex number
            Complex C;
            C.real = magnitude * Math.Cos(angle);
            C.imag = magnitude * Math.Sin(angle);
            return C;
        }//end Polar2Rect()

        /// <summary>
        /// Converts a Complex type to a an ordered pair of floating-point x- and y-coordinates
        /// </summary>
        /// <returns>Returns an ordered pair of floating-point x- and y-coordinates
        /// that defines a point in a two-dimensional plane</returns>
        public PointF ConvertToPointF()
        {// start ConvertToPoint()
            PointF p = new Point();
            p.X = (Single)this.Real;
            p.Y = (Single)this.Imaginary;
            return p;
        }// end ConvertToPoint()

        /// <summary>
        /// Converts a floating-point ordered pair to a Complex structure
        /// </summary>
        /// <param name="floatingPoint">An ordered pair represented as a floating point values</param>
        /// <returns>Returns a Complex type from a floating-point ordered pair</returns>
        public Complex ConvertToComplex(PointF floatingPoint)
        {// start ConvertToComplex()
            Complex c = new Complex(floatingPoint.X, floatingPoint.Y);
            return c;
        }// end ConvertToComplex()


        /// <summary>
        /// Override the ToString() method to display a complex number in the traditional format
        /// </summary>
        /// <returns>Returns the representation of a complex number in the traditional format
        /// in the form a + ib or a - ib
        /// </returns>
        public override string ToString()
        {
            if (imag < 0)
            {
                return (System.String.Format("{0} - {1}i", real, imag * -1));
            }
            else
            {
                return (System.String.Format("{0} + {1}i", real, imag));
            }
        }

        /// <summary>
        /// Override of the Equals() method
        /// </summary>
        /// <param name="obj">Object type to be compared to</param>
        /// <returns>True if equal, false otherwise
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            // check if obj is null or not
            if (obj == null)
            { return false; }

            // check if obj can be cast to Complex
            // it will fail if C is null

            if (obj is Complex)
            {
                return this == (Complex)obj;
            }
            else
                return false;

        }
        /// <summary>
        /// Override of the GetHashCode() method
        /// </summary>
        /// <returns>The hash code for a complex number
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (this.real.GetHashCode() ^ this.imag.GetHashCode());
        }

        #endregion  // End region definiton for Methods
    }   //End definition of the Complex structure
    #endregion // End region definition for Struct
}//end namespace

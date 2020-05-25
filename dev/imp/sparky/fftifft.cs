/********************************************
*  Author Name : Surujlal 'Sparky' Dasrath  *
*  Date        : November 01 2007           * 
*  Contact     : sdasrath@gmail.com         *
*  License     : MIT                        *
* *******************************************
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
 
namespace FastFourier
{
    #region Summary of FFT and IFFT
    /*  Verion: 1.0.0
    *  Member List:
    *      [1] TYPES
    *          -> FFT
    *          -> IFFT
    *          -> FourierTransformException
    *      [2] PRIVATE FIELDS
    *          FFT:
    *              -> complexArrayInput - an array of Complex types
    *              -> noOfPoints - number of points to be used in the FFT calculation
    *              -> modifiedArray - an arry of Complex type that is a resized version of complexArrayInput
    *          IFFT:
    *              -> complexArrayInput - an array of Complex types
    *              -> noOfPoints - number of points to be used in the IFFT calculation
    *      [3] ACCESSOR METHODS/ PROPERTIES
    *          FFT:
    *              -> ComplexArray - read/write property that accesses the complexArrayInput field
    *              -> NumberOfPoints - read/write property that accesses the noOfPoints field
    *              -> ModifiedArray - read/write property that accesses the modifiedArray field
    *              -> Length - gets the length of an array
    *      [4] CONSTRUCTORS
    *              -> FFT(Complex[] complexArray) - initialize an instance of the FFT type using its argument
    *                  to set the complexInputArray field
    *              -> FFT(Complex[] complexArray, Int32 noOfPoints) - initialize an instance of the FFT type using
    *                  the first argument to set the complexInputArray field and the second to set numberOfPoints
    *              -> IFFT(Complex[] complexArray) : base(complexArray) - inherits from FFT and used to
    *                  initialize an instance of the FFT type using its argument to set the complexInputArray field
    *              -> IFFT(Complex[] complexArray, Int32 noOfPoints) : base(complexArray, noOfPoints) - inherits
    *                  from FFT and is used to initialize an instance of the FFT type using the first argument
    *                  to set the complexInputArray field and the second to set numberOfPoints
    *      [5] METHODS
    *              -> Fft() - used to call ZeroPad() and DoFFT() methods
    *              -> ZeroPad() - truncate/extend-by-zero-padding/leave-intact a Complex array
    *              -> DoFFT() - perform Fourier decomposition
    *              -> Real(Complex[] x) - returns real part of each element of an array of Complex types
    *              -> Imaginary(Complex[] x) - returns imaginary part of each element of an array of Complex types
    *              -> Abs(Complex[] x) - returns the magnitude of each element of an array of Complex types
    *              -> Equals() - overridden method of System.Object to suit the needs to FFT/IFFT
    *              -> GetHashCode() - overridden method of System.Object to suit the needs of FFT/IFFT
    * */
    #endregion
 
    #region Start of Class "FFT"
 
    //        N
    // X(k) = SUM x(n) * Exp(-j * 2 * PI * k * n / N)       1 <= k <= N
    //        n=0
    // and Euler's Identity:
    //       j*theta
    //      e        = cos(theta) + j * sin(theta)
 
    /// <summary>
    /// Performs the Fast Fourier Transformation technique on an array of real or complex values
    /// and returns the result.
    /// </summary>
    public class FFT
    {   // start definition of Class fft01
 
        #region FFT -> PRIVATE FIELDS
        /// <summary>
        /// Array of time domain Complex values to be transformed to frequency domain using FFT.
        /// </summary>
        private Complex[] complexInputArray;    // x(n)
 
        /// <summary>
        /// 32-bit representation of the number of points used in the FFT calculation.
        /// </summary>
        private Int32 noOfPoints;               // N
 
        /// <summary>
        /// Modified array of time domain Complex values to be transformed to frequency domain using FFT.
        /// Modification is in the form of either no changes was done to the original input array or the input
        /// was truncated/zero-padded.
        /// </summary>
        private Complex[] modifiedArray;          // x(n) - Modified for FFT
        #endregion
 
        #region FFT -> ACCESSOR METHODS/PROPERTIES
        /// <summary>
        /// Gets or sets the array to be transformed.
        /// </summary>
        public Complex[] ComplexArray
        {// start property "ComplexArray"
            get // gets x(n)
            { return this.complexInputArray; }
            set // sets x(n)
            { this.complexInputArray = value; }
        }// end property "ComplexArray"
 
        /// <summary>
        /// Gets or sets the number of points used in the Fast Fourier transformation.
        /// </summary>
        public Int32 NumberOfPoints
        {// start property "NumberOfPoints"
            get // gets N
            { return this.noOfPoints; }
            set // sets N
            { this.noOfPoints = value; }
        }// end property "NumberOfPoints"
 
        /// <summary>
        /// Gets or sets the number of values in the array with respect to the number of FFT points.
        /// </summary>
        public Complex[] ModifiedArray
        {// start property "ModifiedArray"
            get
            { return this.modifiedArray; }
            set
            { this.modifiedArray = value; }
        }// end property "ModifiedArray"
 
        /// <summary>
        /// Gets the length of the input array to be transformed.
        /// </summary>
        public Int32 Length
        {// start property "Length"
            get
            { return this.complexInputArray.Length; }
        }// end property "Length"
        #endregion
 
        #region FFT -> CONSTRUCTORS
        /// <summary>
        /// Initialize an new instance of a FFT object using the "complexArray" argument
        /// to populate the array to be transformed.
        /// </summary>
        /// <param name="complexArray">An array of Complex types to be transformed.</param>
        public FFT(Complex[] complexArray)
        { // start "FFT(Complex[] complexArray)"
            this.complexInputArray = complexArray;
            this.noOfPoints = complexArray.Length;
            this.modifiedArray = new Complex[this.noOfPoints];
        } // end "FFT(Complex[] complexArray)"
 
        /// <summary>
        /// Initialize an new instance of a FFT object using the "complexArray" argument
        /// to populate the array to be transformed and the "noOfPoints" argument to set
        /// the number of points used in the calculation.
        /// </summary>
        /// <param name="complexArray">An array of Complex types to be transformed.</param>
        /// <param name="noOfPoints">32-bit representation of the number of FFT points to be used.</param>
        public FFT(Complex[] complexArray, Int32 noOfPoints)
        { // start "FFT(Complex[] complexArray, Int32 noOfPoints)"
 
            try
            {
                this.complexInputArray = complexArray;
                this.noOfPoints = noOfPoints;
                this.modifiedArray = new Complex[this.noOfPoints];
 
                if (noOfPoints <= 0)
                    throw new FourierTransformException("The number of points must be greater than 0");
            }
            catch (FourierTransformException ex)
            {
                Console.WriteLine(ex.Message.ToString());   
            }
        } // end "FFT(Complex[] complexArray, Int32 noOfPoints)"
        #endregion  // End region definition for Constructors
 
        #region FFT -> METHODS
        /// <summary>
        /// Performs a complete Fast Fourier Transformation on an array of Complex values.
        /// Automatically calls ZeroPad() to leave/truncate/pad-with-zeros the array as
        /// specified by the number of FFT points to be used; then calls the DoFFT() method
        /// which is where the actual FFT computations are done.
        /// </summary>
        /// <returns>Returns an array of Fourier Transformed values.</returns>
        public Complex[] Fft()
        {   // start method "Complex[] Fft()"
 
            // First:
            // call the ZeroPad() method and that will retrieve the original
            //  input array of values to be transformed; ZeroPad() will also
            //  resize the array appropriately to the number of FFT points
            //  desired in the output before any processing is done
            Complex[] modArr = ZeroPad();
 
            // Second:
            // call the DoFFT() method which is where all the processing/math is done
            Complex[] freqDom = DoFFT();
 
            // return the results to the caller
            return freqDom;
        }   // end method "Complex[] Fft()"
 
        /// <summary>
        /// Used to either leave the original input array intact or truncate or
        /// zero pad the array based on the number of FFT points to be used. It is
        /// called from within DoFFT() and handles conditions where the Number of FFT points
        /// is equal, less than and greater than the length of the input array.
        /// </summary>
        /// <param name="inputArray">The array of Complex values that will undergo FFT.</param>
        /// <returns>Returns a new Complex valued array that has the same number of indices
        /// as the number of FFT points.
        /// <list type="bullet">
        /// <item>If the number of FFT points EQUALS the length of the input array,
        /// nothing needs to be done and the original input array is returned.</item>
        /// <item>If the number of FFT points is LESS than the length of the input array,
        /// then the result array is truncated up to the 'FFT points' index.</item>
        /// <item>If the number of FFT points is GREATER than the length of the input array,
        /// then the length of the resultant array will be based on the number of FFT points
        /// and each additional index is filled with zero valued Complex objects.</item>
        /// </list>
        /// If the number of FFT points is less than the length
        /// of the </returns>
        /// <remarks>This method is provided in order to provide users with a finer
        /// grain of control at each step of the calculations.</remarks>
        public Complex[] ZeroPad()
        {// start method "public Complex[] ZeroPad(Complex[] inputArray)"
 
            // This method will test 3 conditions and the resulting output
            //  array is based as each of these are evaluated
            //  [1] If the number of FFT points and the input array
            //      that is to have FFT performed on it has an EQUAL
            //      number of points, then just return the original
            //      input array
            //  [2] If the number of FFT points is GREATER than than the
            //      size of the input array, create a new array and copy
            //      over all the results of the input array and the
            //      remaining indices are to be filled with zero-valued
            //      Complex objects
            //  [3] If the number of FFT points is LESS than the size
            //      of the input array then copy over that many points
            //      to the new array of Complex values and discard the rest
 
            // [1] Perform Step 1: if FFT points EQUAL length of input array
            //      do nothing and just return the input array without modification
 
            // First:
            // retrieve the original user input array of values stored in the
            //  properties of the type
            Complex[] inputArray = this.ComplexArray;
 
            // [1] Perform Step 1
            if (inputArray.Length == this.noOfPoints)
            {
                this.modifiedArray = inputArray;
                return inputArray;
            }
 
            // [2,3] Perform Steps 2 & 3:
            else
            {
                // create an array that will hold Complex objects and make
                //  the length of the array as large as the number of points
                //  that the FFT formula uses; in this case the number of
                //  FFT points is GREATHER than the length of the input array
                Complex[] paddedArray = new Complex[this.NumberOfPoints];
 
 
                // iterate through the newly created array (paddedArray)
                //  and at each index, create a new Complex object
                //  with both the real and imaginary fields set to 0
                for (Int32 i = 0; i < this.NumberOfPoints; i++)
                {// start for with i=0
 
                    // create Complex objects with real & imaginary
                    //  fields set to 0
                    paddedArray[i] = new Complex(0, 0);
 
                    // within the same for loop and at matching indices,
                    //  copy the value of the original input array to the
                    //  newly created padded array
                    //  NOTE: The 'if' does step 3, the 'else' does step 2
                    if (i < inputArray.Length)
                    { // start if
                        paddedArray[i] = inputArray[i];
                    } // end if
                    else
                    { // start else
                        // since the original array is smaller than
                        //  the new padded one, no value need to be
                        //  copied over, BUT the resulting indices still
                        //  need to be zeroed out; jump out of the if-else
                        //  conditions and continue processing the for loop
                        //  which will contine to zero out the remaining indices
                        continue;
                    } //end else     
                }// end for with i=0
 
                // return the new zero padded array after all processing is done
                this.modifiedArray = paddedArray;
                return paddedArray;
            }
        }// end method "public Complex[] ZeroPad(Complex[] inputArray)"
 
        /// <summary>
        /// The actual FFT computations are done within this method.
        /// </summary>
        /// <returns>Returns an array of Complex values that have been transformed
        /// using the Fourier Transformation technique.</returns>
        /// <remarks>This method is provided in order to provide users with a finer
        /// grain of control at each step of the calculations.</remarks>
        public Complex[] DoFFT()
        {// start method "public Complex[] DoFFT(Complex[] paddedArray)"
            //        N
            // X(k) = SUM x(n) * Exp(-j * 2 * PI * k * n / N)
            //        n=0
            // and Euler's Identity:
            //       j*theta
            //      e        = cos(theta) + j * sin(theta)
            // the resulting array will hold the computations after
            //  FFT is performed and it will be the same length
            //  as the number of FFT points (same as the lenth
            //  of the padded array from the ZeroPad() method)
            //  result = X(k);  paddedArray = x(n)
 
            // retrieve the modified input array from the property
            Complex[] paddedArray = this.ModifiedArray;
 
            // create storage for the resulting transformed output
            Complex[] frequencyDomain = new Complex[paddedArray.Length];
 
            // get the number of FFT points based on the length of
            //  the padded array; this is done to simply shorten
            //  some of the expressions below
            Int32 points = paddedArray.Length;
 
            // outer iteration to determine the value of each point
            //  in the result set of the FFT transformation
            //  this loop calculates the X(k)'s in the FFT equation
            for (Int32 k = 0; k < points; k++)
            { // start for with k=0
 
                // temp1 will hold the results of the inner interations
                //  below and it is reset on each outer iteration
                Complex temp1 = new Complex(0, 0);
 
                // inner iteration to compute the sums of values needed
                //  for each FFT point - this is a shifted sum of x(n)'s
                for (Int32 n = 0; n < points; n++)
                { // start for with n=0
 
                    // local variable to compute each iteration of
                    //  x(n) * exp(...)
                    Complex temp2 = new Complex(0, 0);
 
                    // local variables to hold the result of each
                    //  part of the Euler Identity portion of the
                    //  FFT transformaton equation
                    Double real_part = 0;   // cos(theta)
                    Double imag_part = 0;   // sin(theta)
 
                    real_part = Math.Cos((2 * Math.PI * n * k) / points);
                    imag_part = Math.Sin((2 * Math.PI * n * k) / points) * -1;
 
                    temp2.Real = real_part;    // set the Real and Imaginary properties of the local
                    temp2.Imaginary = imag_part;//  Complex object uses to keep track of computations
 
                    // X(k) = [cos(T) + j Sin(T)] * x(n)
                    temp2 = temp2 * paddedArray[n];
                    temp1 = temp1 + temp2;  // sum each successive iteration
                }//end for with n=0
 
                // pass each calculated FFT point back to the result array
                frequencyDomain[k] = temp1;
            } // end for with k=0
 
            return frequencyDomain;
        }// end method "public Complex[] DoFFT(Complex[] paddedArray)"
 
        /// <summary>
        /// Retrieves the real part of each Complex value in an array of Complex objects.
        /// </summary>
        /// <param name="complexArray">An array of Complex types.</param>
        /// <returns>Returns an array of Double values representing the real part of each Complex value.</returns>
        public Double[] Real(Complex[] complexArray)
        {// start method "public Double[] Real()"
            try
            {
                if (complexArray == null)
                { throw new NullReferenceException("Object is a null reference and needs to be initialized"); }
                else
                {
                    Double[] real = new Double[complexArray.Length];
 
                    for (Int32 i = 0; i < complexArray.Length; i++)
                    {// start for with i=0
                        real[i] = complexArray[i].Real;
                    }// end for with i=0
                    return real;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
 
        }// end start method "public Double[] Real()"
 
        /// <summary>
        /// Retrieves the imaginary part of each Complex value in an array of Complex objects.
        /// </summary>
        /// <param name="complexArray">An array of Complex types.</param>
        /// <returns>Returns an array of Double values representing the imaginary part of each Complex value.</returns>
        public Double[] Imaginary(Complex[] complexArray)
        {// start method "public Double[] Imaginary(Complex[] complexArray)"
            try
            {
                if (complexArray == null)
                { throw new NullReferenceException("Object is a null reference and needs to be initialized"); }
                else
                {
                    Double[] imag = new Double[complexArray.Length];
                    for (Int32 i = 0; i < complexArray.Length; i++)
                    {// start for with i=0
                        imag[i] = complexArray[i].Imaginary;
                    }// end for with i=0
                    return imag;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }// end method "public Double[] Imaginary(Complex[] complexArray)"
 
        /// <summary>
        /// Computes the absolute value/magnitude of a Complex object.
        /// </summary>
        /// <param name="complexArray">An array of Complex types.</param>
        /// <returns>Returns an array of Double values representing the absolute value/magnitude of each
        /// index of the complexArray argument.</returns>
        public Double[] Abs(Complex[] complexArray)
        {// start method "public Double Abs(Complex[] complexArray)"
            try
            {
                if (complexArray == null)
                { throw new NullReferenceException("Object is a null reference and needs to be initialized"); }
                else
                {
                    Double[] abs = new Double[complexArray.Length];
 
                    for (Int32 i = 0; i < complexArray.Length; i++)
                    {// start for with i=0
                        //abs[i] = Math.Sqrt((complexArray[i].Real * complexArray[i].Real) + (complexArray[i].Imaginary * complexArray[i].Imaginary));
                        abs[i] = complexArray[i].Modulus();
                    }// end for with i=0
                    return abs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }// end method "public Double Abs(Complex[] complexArray)"
 
        /// <summary>
        /// Overriden version of the System.Object.Equals() method.
        /// </summary>
        /// <param name="obj">Object type to be compared to.</param>
        /// <returns>Returns true if the current object and the argument 'obj' are equal and false otherwise.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (obj is FFT)
            { return this == (FFT)obj; }
            else
            { return false; }
        }
 
        /// <summary>
        /// Overidden version of System.Object.GetHashCode() method.
        /// </summary>
        /// <returns>A hash code for the current FFT object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion // End region defintion for Methods
 
    }// end defintion of Class "FFT"
    #endregion // End region definition for Class "FFT"
 
    #region Start Class "IFFT"
    //        1    N
    // x(n) = - * SUM X(k) * Exp(j * 2 * PI * k * n / N)        1 <= n <= N
    //        N   n=0
    // and Euler's Identity:
    //       j*theta
    //      e        = cos(theta) + j * sin(theta)
    //
    /// <summary>
    /// Performs the Inverse Fast Fourier Transformation on an array of real or complex values
    /// and returns the result.
    /// </summary>
    public class IFFT : FFT
    {// start of class "IFFT"
 
        #region IFFT -> PRIVATE FIELDS
        /// <summary>
        /// Array of frequency domain Complex values to be transformed to time domain using IFFT.
        /// </summary>
        private Complex[] inputArray;
 
        /// <summary>
        /// 32-bit representation of the number of points used in the IFFT calculation.
        /// </summary>
        private Int32 noOfPoints;
 
        /// <summary>
        /// Modified array of frequence domain Complex values to be transformed to time domain using IFFT.
        /// Modification is in the form of either no changes was done to the original input array or the input
        /// was truncated/zero-padded.
        /// </summary>
        private Complex[] modifiedArray;
        #endregion  // End of region definition for Private Fields
 
        #region IFFT -> ACCESSOR METHODS/PROPERTIES
 
        /// <summary>
        /// Gets or sets the array to be transformed.
        /// </summary>
        public new Complex[] ComplexArray
        {// start property "ComplexArray"
            get
            { return this.inputArray; }
            set
            { this.inputArray = value; }
        }// end property "ComplexArray"
 
        /// <summary>
        /// Gets or sets the number of points used in the Fast Fourier transformation.
        /// </summary>
        public new Int32 NumberOfPoints
        {// start property "NumberOfPoints"
            get
            { return this.noOfPoints; }
            set
            { this.noOfPoints = value; }
        }// end property "NumberOfPoints"
 
        #endregion // End of region definiton for Properties
 
        #region IFFT -> CONSTRUCTORS
        /// <summary>
        /// Initialize an new instance of an IFFT object using the "complexArray" argument
        /// to populate the array to be transformed.
        /// </summary>
        /// <param name="complexArray">An array of Complex types to be transformed.</param>
        public IFFT(Complex[] complexArray) : base(complexArray)
        {// start "IFFT(Complex[] complexArray)"
            this.inputArray = complexArray;
            this.noOfPoints = complexArray.Length;
            this.modifiedArray = new Complex[this.noOfPoints];
        }// end "IFFT(Complex[] complexArray)"
 
        /// <summary>
        /// Initialize an new instance of an IFFT object using the "complexArray" argument
        /// to populate the array to be transformed and the "noOfPoints" argument to set
        /// the number of points used in the calculation.
        /// </summary>
        /// <param name="complexArray">An array of Complex types to be transformed.</param>
        /// <param name="noOfPoints">32-bit representation of the number of FFT points to be used.</param>
        public IFFT(Complex[] complexArray, Int32 noOfPoints) : base(complexArray, noOfPoints)
        {// start "IFFT(Complex[] complexArray, Int32 noOfPoints)"
            try
            {
                this.inputArray = complexArray;
                this.noOfPoints = noOfPoints;
                this.modifiedArray = new Complex[this.noOfPoints];
 
                if (noOfPoints <= 0)
                    throw new FourierTransformException("The number of points must be greater than 0");
            }
            catch (FourierTransformException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }// end "IFFT(Complex[] complexArray, Int32 noOfPoints)"
        #endregion // End region definition for Constructors
 
        #region IFFT -> METHODS
        public Complex[] Ifft()
        {// start method "public Complex[] Ifft()"
 
            // First:
            //
            Complex[] padIfft = ZeroPad();
 
            Complex[] timeDomain = DoIFFT();
 
            return timeDomain;
 
        }// end method "public Complex[] Ifft()"
 
 
        //        1    N
        // x(n) = - * SUM X(k) * Exp(j * 2 * PI * k * n / N)        1 <= n <= N
        //        N   n=0
        // and Euler's Identity:
        //       j*theta
        //      e        = cos(theta) + j * sin(theta)
        //
        /// <summary>
        /// The actual IFFT computations are done within this method.
        /// </summary>
        /// <returns>Returns an array of Complex values that have been transformed
        /// using the Inverse Fourier Transformation technique.</returns>
        /// <remarks>This method is provided in order to provide users with a finer
        /// grain of control at each step of the calculations.</remarks>
        public Complex[] DoIFFT()
        {// start method "public Complex[] DoIFFT()"
 
            // get the modified array to be operated on from
            //  the base class property
            Complex[] paddedArray = base.ModifiedArray; /*this.ModifiedArray;)*/
 
            // create storage for the resulting transformed output
            Complex[] timeDomain = new Complex[paddedArray.Length];
 
            // get the number of points used in the IFFT calculation
            Int32 points = paddedArray.Length;
 
            for (Int32 n = 0; n < points; n++)
            {// start for with n=0
 
 
                // temp1 will hold the results of the inner interations
                //  below and it is reset on each outer iteration
                Complex temp1 = new Complex(0, 0);
 
                for (Int32 k = 0; k < points; k++)
                {// start for with k=0
 
                    // local variable to compute each iteration of
                    //  X(k) * exp(...)
                    Complex temp2 = new Complex(0, 0);
 
                    // local variables to hold the result of each
                    //  part of the Euler Identity portion of the
                    //  FFT transformaton equation
                    Double real_part = 0;   // cos(theta)
                    Double imag_part = 0;   // sin(theta)
 
                    real_part = Math.Cos((2 * Math.PI * n * k) / points);
                    imag_part = Math.Sin((2 * Math.PI * n * k) / points);
 
                    temp2.Real = real_part;    // set the Real and Imaginary properties of the local
                    temp2.Imaginary = imag_part;//  Complex object uses to keep track of computations
 
                    // x(n) = [cos(T) + j Sin(T)] * X(k)
                    temp2 = temp2 * paddedArray[k];
                    temp1 = temp1 + temp2;  // sum each successive iteration
 
 
                }//end for with k=0
 
                timeDomain[n] = (temp1) / points;
 
            }// end for with n=0
 
            return timeDomain;
        }// end method "public Complex[] DoIFFT()"
 
        /// <summary>
        /// Overriden version of the System.Object.Equals() method.
        /// </summary>
        /// <param name="obj">Object type to be compared to.</param>
        /// <returns>Returns true if the current object and the argument 'obj' are equal and false otherwise.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
                return false;
            if (obj is IFFT)
            { return this == (IFFT)obj; }
            else
            { return false; }
        }
 
        /// <summary>
        /// Overidden version of System.Object.GetHashCode() method.
        /// </summary>
        /// <returns>A hash code for the current IFFT object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion // End region definiton for DoIFFT()
 
    }// end of class "IFFT"
    #endregion // End region definition for Class IFFT
 
    #region Start of Class "FourierTransformException"
    public class FourierTransformException : Exception
    {// start definiton of Class "FourierTransformException"
        public FourierTransformException() : base() { }
        public FourierTransformException(String message) : base(message) { }
    }// end definiton of Class "FourierTransformException"
    #endregion End of CLASS "FourierTransformException"
}
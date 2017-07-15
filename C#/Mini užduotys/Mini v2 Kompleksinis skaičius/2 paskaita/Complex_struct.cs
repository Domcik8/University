using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    struct Complex
    {
        int real;
        int imaginary;

        public Complex(int x, int y)
        {
            real = x;
            imaginary = y;
        }


        public static Complex operator +(Complex x, Complex y)
        {
            return new Complex(x.real + y.real, x.imaginary + y.imaginary);
        }

        public static Complex operator -(Complex x, Complex y)
        {
            return new Complex(x.real - y.real, x.imaginary - y.imaginary);
        }

        public static Complex operator *(Complex x, Complex y)
        {
            return new Complex((x.real * y.real - x.imaginary * y.imaginary), (x.imaginary * y.real + x.real * y.imaginary));
        }


        public override string ToString()
        {
            return String.Format("{0} + {1}i", real, imaginary);
        }

    }
}

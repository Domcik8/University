using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    public struct Complex : IEquatable<Complex>, IComparable<Complex>
    {
        public int real;
        public int imaginary;

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

        public bool Equals(Complex x)
        {
            if(this.real == x.real)
            {
                if(this.imaginary == x.imaginary)
                {
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(Complex x)
        {
            if (x.real == this.real)
                return this.real.CompareTo(x.imaginary);
            return this.real.CompareTo(x.real);
        }
    }
}



/*namespace CustomArray
{
    class Test : IEquatable<Test>, IComparable
    {
        public int a;
        public int b;

        public bool Equal(Test x)
        {          
            if (x == null)
                    return false;
            return x.a == this.a && x.b == this.b;
        }

        public int CompareTo(Test o)
        {
            Test x = o as Test;
            if (x == null)
                return 0;
            if (x.a == this.a)
                return this.a.CompareTo(x.b);
            return this.a.CompareTo(x.a);
        }

    }

    class TestComparer : IComparer<Test>
    {
        int Compare(Test x, Test y)
        {
            return 0;
        }
    }

}*/
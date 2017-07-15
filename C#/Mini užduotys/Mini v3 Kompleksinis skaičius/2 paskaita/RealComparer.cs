using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    public class RealComparer : IComparer<Complex>
    {
        public int Compare(Complex x, Complex y)
        {
            if (x.imaginary == y.imaginary)
                return x.real.CompareTo(y.real);
            return x.imaginary.CompareTo(y.imaginary);
        }
    }
}

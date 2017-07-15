using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex a = new Complex(5, 8);
            Complex b = new Complex(6, 8);
            Complex c = new Complex(5, 8);

            Console.WriteLine(a.Equals(b));
            Console.WriteLine(a.Equals(c));
                
            List<Complex> list = new List<Complex>();
            list.Add(new Complex(1, 2));
            list.Add(new Complex(4, 5));
            list.Add(new Complex(0, 0));
            list.Add(new Complex(2, 3));
            list.Add(new Complex(0, 1));
            list.Add(new Complex(1, 0));

            list.Sort();
            foreach (Complex d in list)
             Console.WriteLine(d);

            Console.WriteLine(); Console.WriteLine();

            list.Sort(new RealComparer());
            foreach (Complex d in list)
                Console.WriteLine(d);



            Console.ReadLine();
        }
    }
}
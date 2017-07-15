using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paieška
{
    class Stringholder
    {
        IPaieska paieska;
        String[] holder = {"www.google.com",
                          "destytojas",
                          "mokytojas",
                          "Dominik@gmail.com",
                          "Gabriel@gmail.com",
                          "www.bing.com"};

        public Stringholder(IPaieska paieska)
        {
            this.paieska = paieska;
        }

        public void Rasti()
        {
            String[] printas;
            int i = 0;
            Simbolis<string>("Ieskau");

            printas = paieska.Rasti(holder);
            foreach (string print in printas)
                if (print != null)
                {
                    Console.WriteLine(print);
                    i++;
                }
            Simbolis<int>(i);
        }

        public void Simbolis<T>(T eil)
        {
            Console.WriteLine(eil.ToString());
        }
    }
}

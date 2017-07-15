using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paieška
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailPaieska email = new EmailPaieska();
            WebPaieska web = new WebPaieska();

            Console.WriteLine("Bandau su email paieska");
            Stringholder holder1 = new Stringholder(email);
            holder1.Rasti();
            Console.WriteLine("Bandau su web paieska");
            Stringholder holder2 = new Stringholder(web);
            holder2.Rasti();
            Console.ReadLine();
        }
    }
}

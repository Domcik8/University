using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Paieška
{
    class EmailPaieska : IPaieska
    {
        String[] holder = new String[10];
        int i = 0;

        public string[] Rasti(string[] array)
        {
            foreach(string mail in array)
            {
                if(new Regex(@"@").IsMatch(mail))
                {
                    holder[i] = mail;
                    i++;
                }
            }
            return holder;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace Encoding
{
    class Program
    {
        private string path1 = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\Mini v5 Enkodingai\Encoding\passwords.txt";
        private string path2 = @"C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\Mini v5 Enkodingai\Encoding\answers.txt";

        static void Main(string[] args)
        {
            List<string> pass = new List<string>();
            Program program = new Program();
            pass = program.ReadFile(pass);
            IEnumerable<string> passwordQuery = program.Filter(pass);
            Console.WriteLine("\nFiltruojam");
            foreach (string pas in passwordQuery)
                Console.WriteLine(pas);
            program.WriteFile(passwordQuery);
            Console.ReadLine();
        }






        public List<string> ReadFile(List<string> pass)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path1);
            System.Console.WriteLine("Nuskaitau");
            while ((line = file.ReadLine()) != null)
            {
                pass.Add(line);
                System.Console.WriteLine(line);
            }
            file.Close();
            return pass;
        }

        public IEnumerable<string> Filter(List<string> pass)
        {
            var passwordQuery =
                from password in pass
                where (Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]") && Regex.IsMatch(password, @"[\W\D]") && Regex.IsMatch(password, ".{5}"))
                select password;
            return passwordQuery;
        }



        public void WriteFile(IEnumerable<string> passwordQuery)
        {
            Byte[] coded;
            using (StreamWriter file = new StreamWriter(path2))
            {
                foreach (string pass in passwordQuery)
                {
                    coded = System.Text.Encoding.ASCII.GetBytes(pass);
                    coded = new MD5CryptoServiceProvider().ComputeHash(coded);
                    foreach (byte b in coded)
                        file.Write(b.ToString("x2"));
                    file.WriteLine();
                }
            }
        }
    }
}
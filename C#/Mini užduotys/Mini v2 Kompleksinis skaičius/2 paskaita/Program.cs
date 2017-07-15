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
            int option = 0;
            Complex sk = new Complex(0, 0);
            CustomArray<Complex> array = null;
            while (option != 7 )
            {
                System.Console.WriteLine("Svieki!");
                System.Console.WriteLine("1. Sukurti kompleksini skaiciu");
                System.Console.WriteLine("2. Sukurti nauja masiva");
                System.Console.WriteLine("3. Nustatyti masivo elemento reiksme");
                System.Console.WriteLine("4. Perziureti masivo elemento reiksme");
                System.Console.WriteLine("5. Gauti masivo didi");
                System.Console.WriteLine("6. Sukeisti dvi reiksmes vietomis");
                System.Console.WriteLine("7. Paigti darba");
                System.Console.Write("Pasirikite norma veiksma : ");
                System.Console.Write("Pasirikite norma veiksma : ");
                option = Convert.ToInt32(System.Console.ReadLine());
                switch (option)
                {
                    case 1:
                        {
                            int a, b;
                            System.Console.Write("Iveskite realia dali : ");
                            a = Convert.ToInt32(System.Console.ReadLine());
                            System.Console.Write("Iveskite isivivaizduojama dali : ");
                            b = Convert.ToInt32(System.Console.ReadLine());
                            sk = new Complex(a, b);
                            break;
                        }

                    case 2:
                        {
                            int size;
                            System.Console.Write("Iveskite masivo didi : ");
                            size = Convert.ToInt32(System.Console.ReadLine());
                            array = new CustomArray<Complex>(size);
                            break;
                        }
                    case 3:
                        {
                            if(array == null)
                            { 
                                System.Console.WriteLine("Nera sukurto masivo!");
                                break;
                            }
                            int i;
                            System.Console.Write("Iveskite indeksa : ");
                            i = Convert.ToInt32(System.Console.ReadLine());
                            if (i < 0 || i > array.getSize())
                            {
                                System.Console.Write("Blogas indeksas!");
                                break;
                            }
                            array[i] = sk;
                            break;
                        }
                    case 4:
                        {
                            if (array == null)
                            {
                                System.Console.WriteLine("Nera sukurto masivo!");
                                break;
                            }
                            int i;
                            System.Console.Write("Iveskite indeksa : ");
                            i = Convert.ToInt32(System.Console.ReadLine());
                            if(i < 0 || i > array.getSize())
                            {
                                System.Console.Write("Blogas indeksas!");
                                break;
                            }
                            System.Console.WriteLine(array[i]);
                            break;
                        }
                    case 5:
                        {
                            if (array == null)
                            {
                                System.Console.WriteLine("Nera sukurto masivo!");
                                break;
                            }
                            System.Console.Write(array.getSize());
                            break;
                        }
                    case 6:
                        {
                            if (array == null)
                            {
                                System.Console.WriteLine("Nera sukurto masivo!");
                                break;
                            }
                            int i, j;
                            System.Console.Write("Iveskite pirma indeksa : ");
                            i = Convert.ToInt32(System.Console.ReadLine());
                            if (i < 0 || i > array.getSize())
                            {
                                System.Console.Write("Blogas indeksas!");
                                break;
                            }
                            System.Console.Write("Iveskite antra indeksa : ");
                            j = Convert.ToInt32(System.Console.ReadLine());
                            if (j < 0 || j > array.getSize())
                            {
                                System.Console.Write("Blogas indeksas!");
                                break;
                            }
                            array.Swap(i, j);
                            break;
                        }
                    default:
                        {
                        System.Console.WriteLine("Nera tokios opcijos!");
                        break;
                        }
                }
                System.Console.WriteLine("ATLIKTA!\n\n\n\n\n\n\n\n\n");
            }
        }
    }
}
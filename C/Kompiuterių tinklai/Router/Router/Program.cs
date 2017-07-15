#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterManager;
using Simulation;

namespace Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\Dominik\Desktop\VU\4 semestras\Kompiuterių tinklai\Router\Data2.txt";
            Router router = new Router();
            RouterSimulation simulation;
            Welcome();
            router.FillData(path);
#if DEBUG
            Console.WriteLine("*****Start of Debug*****");
            router.ShowAllData();
            Console.WriteLine("*****End of Debug*****");
#endif
            simulation = new RouterSimulation(router);
            simulation.Start();
            Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();


        }

        private static void Welcome()
        {
            Console.WriteLine("Welcome to router simulation by Dominik");
            Clear();
        }

        public static void Clear()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

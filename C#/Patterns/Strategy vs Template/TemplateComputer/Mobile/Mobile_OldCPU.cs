using System;
using System.Threading;

namespace TemplateComputer.Mobile
{
    class Mobile_OldCPU : Mobile
    {
        protected override void Start()
        {
            Waiting("CPU is starting up");
            Console.Write("CPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Mobile is ready for work.");
        }

        protected override void Calculate(float a, float b)
        {
            Thread.Sleep(500);
            Console.WriteLine("{0} + {1} = {2}", a, b, a + b);
            Console.ReadLine();
        }

        protected override void ShutDown()
        {
            Waiting("CPU is shutting down");
            Console.Write("CPU has shut down.");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Mobile has shut down.");
        }

        private static void Waiting(string message)
        {
            for (int i = 0; i < 12; i++)
            {
                Thread.Sleep(500);
                Console.Clear();
                Console.Write(message);
                for (int j = 0; j < i % 4; j++)
                    Console.Write(".");
            }
            Thread.Sleep(500);
            Console.Clear();
        }
    }
}

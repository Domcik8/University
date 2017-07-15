using System;
using System.Threading;
using System.Windows.Forms;

namespace TemplateComputer.Computer
{
    class Computer_NewCPU_NewGPU : Computer
    {
        protected override void Start()
        {
            NewCPUStart();
            NewGPUStart();
            Console.WriteLine("Computer is ready for work.");
        }

        protected override void Calculate(float a, float b)
        {
            NewGPUDisplay(NewCPUCalculate(a, b));
        }

        protected override void ShutDown()
        {
            NewCPUShutDown();
            NewGPUShutDown();
            Console.WriteLine("Computer has shut down.");
        }

        protected void NewCPUStart()
        {
            NewWaiting("CPU is starting up");
            Console.Write("CPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        protected string NewCPUCalculate(float a, float b)
        {
            Thread.Sleep(250);
            return String.Format("{0} + {1} = {2}", a, b, a + b);
        }

        protected void NewCPUShutDown()
        {
            NewWaiting("CPU is shutting down");
            Console.Write("CPU has shut down.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        protected void NewGPUStart()
        {
            NewWaiting("GPU is starting up");
            Console.Write("GPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        protected void NewGPUDisplay(string message)
        {
            Thread.Sleep(500);
            MessageBox.Show(message);
        }

        protected void NewGPUShutDown()
        {
            NewWaiting("GPU is shutting down");
            Console.Write("GPU has shut down.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        private static void NewWaiting(string message)
        {
            for (int i = 0; i < 4; i++)
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

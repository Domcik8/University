using System;
using System.Threading;

using System.Windows.Forms;


namespace TemplateComputer.Computer
{
    class Computer_OldCPU_NewGPU : Computer
    {
        protected override void Start()
        {
            OldCPUStart();
            NewGPUStart();
            Console.WriteLine("Computer is ready for work.");
        }

        protected override void Calculate(float a, float b)
        {
            NewGPUDisplay(OldCPUCalculate(a, b));
        }

        protected override void ShutDown()
        {
            OldCPUShutDown();
            NewGPUShutDown();
            Console.WriteLine("Computer has shut down.");
        }

        protected void OldCPUStart()
        {
            OldCPUWaiting("CPU is starting up");
            Console.Write("CPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        protected string OldCPUCalculate(float a, float b)
        {
            Thread.Sleep(500);
            return String.Format("{0} + {1} = {2}", a, b, a + b);
        }

        protected void OldCPUShutDown()
        {
            OldCPUWaiting("CPU is shutting down");
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

        private static void OldCPUWaiting(string message)
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

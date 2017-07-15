using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateComputer.Computer
{
    class Computer_NewCPU_OldGPU : Computer
    {
        protected override void Start()
        {
            NewCPUStart();
            OldGPUStart();
            Console.WriteLine("Computer is ready for work.");
        }

        protected override void Calculate(float a, float b)
        {
            OldGPUDisplay(NewCPUCalculate(a, b));
        }

        protected override void ShutDown()
        {
            NewCPUShutDown();
            OldGPUShutDown();
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

        protected void OldGPUStart()
        {
            OldCPUWaiting("GPU is starting up");
            Console.Write("GPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        protected void OldGPUDisplay(string message)
        {
            Console.WriteLine("Rendering, please wait...");
            Thread.Sleep(1000);
            MessageBox.Show(message);
        }

        public void OldGPUShutDown()
        {
            OldCPUWaiting("GPU is shutting down");
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

using System;
using System.Threading;
using System.Windows.Forms;

namespace Computer.GPU
{
    class OldGPU : IGPU
    {
        public void Start()
        {
            Waiting("GPU is starting up");
            Console.Write("GPU is ready for work.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void Display(string message)
        {
            Console.WriteLine("Rendering, please wait...");
            Thread.Sleep(1000);
            MessageBox.Show(message);
        }

        public void ShutDown()
        {
            Waiting("GPU is shutting down");
            Console.Write("GPU has shut down.");
            Thread.Sleep(1000);
            Console.Clear();
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

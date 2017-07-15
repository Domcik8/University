using System;
using Computer.CPU;
using Computer.GPU;

namespace Computer.Computer
{
    class Computer
    {
        public ICPU CPU = null;
        public IGPU GPU = null;

        private Computer(ICPU CPU, IGPU GPU)
        {
            this.CPU = CPU;
            this.GPU = GPU;
        }

        public void Start()
        {
            CPU.Start();
            GPU.Start();
            Console.WriteLine("Computer is ready for work.");
        }

        public void Calculate(float a, float b)
        {
            GPU.Display(CPU.Calculate(a, b));
        }

        public void ShutDown()
        {
            CPU.ShutDown();
            GPU.ShutDown();
            Console.WriteLine("Computer has shut down.");
        }

        public static Computer GetComputer_NewCPU_NewGPU()
        {
            return new Computer(new NewCPU(), new NewGPU());
        }

        public static Computer GetComputer_OldCPU_NewGPU()
        {
            return new Computer(new OldCPU(), new NewGPU());
        }

        public static Computer GetComputer_NewCPU_OldGPU()
        {
            return new Computer(new OldCPU(), new OldGPU());
        }

        public static Computer GetComputer_OldCPU_OldGPU()
        {
            return new Computer(new OldCPU(), new OldGPU());
        }

        public void Work()
        {
            float a, b;
            Start();
            Console.WriteLine("Please enter 2 digits");
            float.TryParse(Console.ReadLine(), out a);
            float.TryParse(Console.ReadLine(), out b);
            Calculate(a, b);
            ShutDown();

            Console.ReadLine();
        }
    }
}

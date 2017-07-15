using System;
using Computer.CPU;
using Computer.GPU;

namespace Computer.Mobile
{
    class Mobile
    {
        public ICPU CPU = null;

        private Mobile(ICPU CPU)
        {
            this.CPU = CPU;
        }

        public void Start()
        {
            CPU.Start();
            Console.WriteLine("Mobile is ready for work.");
        }

        public void Calculate(float a, float b)
        {
            Console.WriteLine(CPU.Calculate(a, b));
            Console.ReadLine();
        }

        public void ShutDown()
        {
            CPU.ShutDown();
            Console.WriteLine("Mobile has shut down.");
        }

        public static Mobile GetMobile_NewCPU()
        {
            return new Mobile(new NewCPU());
        }

        public static Mobile GetMobile_OldCPU()
        {
            return new Mobile(new OldCPU());
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

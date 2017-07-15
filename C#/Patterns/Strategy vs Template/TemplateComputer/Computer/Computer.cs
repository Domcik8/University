using System;

namespace TemplateComputer.Computer
{
    abstract class Computer
    {
        protected abstract void Start();

        protected abstract void Calculate(float a, float b);

        protected abstract void ShutDown();

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

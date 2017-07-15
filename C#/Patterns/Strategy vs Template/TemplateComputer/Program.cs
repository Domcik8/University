using System;
using TemplateComputer.Computer;
using TemplateComputer.Mobile;

namespace TemplateComputer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Computer.Computer computer = new Computer_NewCPU_NewGPU();
            Computer.Computer computer2 = new Computer_NewCPU_OldGPU();
            Computer.Computer computer3 = new Computer_OldCPU_OldGPU();
            computer.Work();
            computer2.Work();
            computer3.Work();
            
            Mobile.Mobile mobile = new Mobile_NewCPU();
            //Mobile.Mobile mobile = new Mobile_OldCPU();
            mobile.Work();
        }


    }
}

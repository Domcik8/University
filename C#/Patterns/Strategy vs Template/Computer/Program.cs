using System;

namespace Computer
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {

            Computer.Computer computer = Computer.Computer.GetComputer_NewCPU_NewGPU();
            //Computer.Computer computer = Computer.Computer.GetComputer_OldCPU_OldGPU();
            computer.Work();


            Mobile.Mobile mobile = Mobile.Mobile.GetMobile_NewCPU();
            //Mobile.Mobile mobile = Mobile.Mobile.GetMobile_OldCPU();
            mobile.Work();
        }
    }
}

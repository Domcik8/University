using System;
using System.Windows.Forms;

namespace Messenger
{
    static class Program
    {
        //Užkrauna programos formas
        //Parametrai: nėra
        //Gražina: void
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

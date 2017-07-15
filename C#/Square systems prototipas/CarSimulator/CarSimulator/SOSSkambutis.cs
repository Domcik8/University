using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSimulator
{
    class SOSSkambutis
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public bool suveike = true;
        bool ijungta = false;
        public void AutomatinisSOS()
        {
            ijungta = !ijungta;
        }
        public bool ArIjungta()
        {
            return ijungta;
        }
        public bool Laikmatis()
        {
            if (timer.Enabled)
            {
                MessageBox.Show("SOS Skambutis negalimas! Palaukite!");
                return false;
            }
            return true;
        }
        public void SOS()
        {
            if (!timer.Enabled)
            {
                suveike = true;
                timer.Start();
                MessageBox.Show("Skambinama 911!");
            }
        }
        public bool ArSuveike()
        {
            return suveike;
        }
        public void NustatytiLaikmati(int min, int s)
        {
        }
    }
}

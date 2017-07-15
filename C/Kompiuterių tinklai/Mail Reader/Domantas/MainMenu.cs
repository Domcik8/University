using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace email
{
    public partial class MainMenu : Form
    {
        ReadEmailForm re;
        SendEmailForm se;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void composeEmailButton_Click(object sender, EventArgs e)
        {
            if (se == null)
            {
                se = new SendEmailForm(this);
            }
            
            se.Show();
            this.Hide();
        }

        private void readEmailButton_Click(object sender, EventArgs e)
        {
            if (re == null)
            {
                re = new ReadEmailForm(this);
            }
            re.Show();
            this.Hide();
        }

        private void updateCredentialsButton_Click(object sender, EventArgs e)
        {
            LoginForm l = new LoginForm(this);
            l.Show();
            this.Hide();
        }
    }
}

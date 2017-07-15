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
    public partial class LoginForm : Form
    {
        MainMenu menu;
        public static string password = "";
        public static string userName = "djpokis@gmail.com";
        public LoginForm(MainMenu menu)
        {
            this.menu = menu;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            password = passwordTextBox.Text;
            userName = loginTextBox.Text;
            this.Hide();
            menu.Show();
            MessageBox.Show("pass: "+ password + "user: " + userName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Windows_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (!new Regex(@"^[a-zA-Z0-9]+(.[a-zA-Z0-9]+)?(.[a-zA-Z0-9]+)?@[a-zA-Z0-9]+.[a-zA-Z0-9]+$").IsMatch(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(sender as Control, "klaida");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string[] emailData = new string[2];
            string nameData;
            string surnameData;
            string[] name = new string[3];
            string[] name2 = new string[3];
            emailData = email.Split('@');
            nameData = emailData[0];
            surnameData = emailData[1];
            name2 = nameData.Split('.');
            StringBuilder str = new StringBuilder();


            for (int i = 0; i < name2.Length; i++ )
                name[i] = name2[i];


            if (name2.Length == 1)
            {
                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                }
                str.Append("@");
                str.Append(surnameData);
                label1.Text = str.ToString();
                str.Clear();
            }

            if (name2.Length == 2)
            {
                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                }
                str.Append("@");
                str.Append(surnameData);
                label1.Text = str.ToString();
                str.Clear();



                


                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                }
                str.Append("@");
                str.Append(surnameData);
                label2.Text = str.ToString();
                str.Clear();

            }

            if (name2.Length == 3)
            {


                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                }
                str.Append("@");
                str.Append(surnameData);
                label1.Text = str.ToString();
                str.Clear();



                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                }
                str.Append("@");
                str.Append(surnameData);
                label2.Text = str.ToString();
                str.Clear();


                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                }
                str.Append("@");
                str.Append(surnameData);
                label3.Text = str.ToString();
                str.Clear();

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                }
                str.Append("@");
                str.Append(surnameData);
                label4.Text = str.ToString();
                str.Clear();

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                }
                str.Append("@");
                str.Append(surnameData);
                label5.Text = str.ToString();
                str.Clear();

                if (!string.IsNullOrEmpty(name[2]))
                {
                    str.Append(name[2]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[0]))
                {
                    str.Append(name[0]);
                    str.Append(".");
                }

                if (!string.IsNullOrEmpty(name[1]))
                {
                    str.Append(name[1]);
                }
                str.Append("@");
                str.Append(surnameData);
                label6.Text = str.ToString();
                str.Clear();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace email
{
    public partial class SendEmailForm : Form
    {
        MainMenu menu;
        ReadEmailForm re;
        int mode = 0;
        string hostName = "smtp.gmail.com";
        int port = 587;
        public SendEmailForm(MainMenu menu)
        {
            this.menu = menu;
            InitializeComponent();
            //Console.WriteLine("potato");
        }
        public SendEmailForm(ReadEmailForm re, string from)
        {
            mode = 1;
            InitializeComponent();
            this.re = re;
            toTextBox.Text = from;
            toTextBox.Enabled = false;
        }
        //-------------------------------------------------------------------
        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {      
                 SmtpClient client = new SmtpClient(hostName, port);
                 client.Credentials = new NetworkCredential(LoginForm.userName, LoginForm.password);
                 MailMessage msg = new MailMessage();
                 if (attachTextBox1.Text != "") 
                    msg.Attachments.Add(new Attachment(attachTextBox1.Text));
                 if (attachTextBox2.Text != "") 
                     msg.Attachments.Add(new Attachment(attachTextBox2.Text));
                 msg.To.Add(new MailAddress(toTextBox.Text));
                 msg.From = new MailAddress(LoginForm.userName);
                 msg.Subject = subjectTextBox.Text;
                 msg.Body = messageTextBox.Text;
                 client.EnableSsl = true;
                 //for (int i = 0; i < 10; i++ )
                     client.Send(msg);
                 MessageBox.Show("Mail sent!", "Success", MessageBoxButtons.OK);
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: \n" +ex);
            }
            
        }
        //-------------------------------------------------------------------
        private void backButton_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                toTextBox.Enabled = true;
                this.Hide();
                re.Show();
            }
            else
            {
                menu.Show();
                this.Hide();
            }
            
        }
        //-------------------------------------------------------------------
        private void attachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picPath = dlg.FileName.ToString();
                attachTextBox1.Text = picPath;
            }

        }
        //-------------------------------------------------------------------
        private void testInfoButton_Click(object sender, EventArgs e)
        {
            toTextBox.Text = "djpokis@gmail.com";
            subjectTextBox.Text = "This is a test run";
            messageTextBox.Text = "This is a generated text without a meaning";
        }
        //---------------------------------------------------------------------
        private void attachButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picPath = dlg.FileName.ToString();
                attachTextBox2.Text = picPath;
            }
        }
        //-------------------------------------------------------------------
        

    }
}

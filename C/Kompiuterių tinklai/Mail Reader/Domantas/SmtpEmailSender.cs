using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace email
{
    public class SmtpEmailSender :TcpClient
    {
        const int smtp_port = 25;
        const string crlf = "\r\n";

        string response = "";
        public string hostname = "";
        public string errormessage = "";
        public string sender = "";
        public string recipient = "";
        public string subject = "";
        public string messagebody = "";

        //--------------------------------------------------------------------
        public SmtpEmailSender(string hostname, string sender, string recipient, string subject, string messageBody)
            : base()
        {
            this.hostname = hostname;
            this.sender = sender;
            this.recipient = recipient;
            this.subject = subject;
            this.messagebody = messageBody;
        }
        //--------------------------------------------------------------------
        public bool isconnected()
        {
            return base.Active;
        }
        //--------------------------------------------------------------------
        private bool ValidEmail()
        {
            if ((sender.Length > 0) & (recipient.Length > 0)
                & (subject.Length > 0) & (messagebody.Length > 0))
            { return true; }
            else { return false; }
        }
        //--------------------------------------------------------------------
        public bool SendEmail()
        {
            try
            {
                this.Connect(hostname, smtp_port);
            }
            catch (Exception e)
            {
                errormessage = e.ToString();
                MessageBox.Show("err 1");
                return false;
            }

            if (!isconnected())
            {
                errormessage = "Could not connect to the server.";
                MessageBox.Show("err 2");
                return false;
            }
            if (!ValidEmail())
            {
                errormessage = "Invalid email data.";
                MessageBox.Show("err 3");
                return false;
            }

            snd("HELO", true);
            snd("MAIL FROM:<" + sender + ">", true);
            snd("RCPT TO:<" + recipient + ">", true);
            snd("DATA", true);
            snd("From: " + sender);
            snd("To: " + recipient);
            snd("Subject: " + subject);
            snd("");
            snd(messagebody);
            snd(".", true);
            snd("QUIT", true);

            return true;
        }
        //--------------------------------------------------------------------

        private bool snd(string buffer) { return snd(buffer, false); }
        private bool snd(string buffer, bool getresponse)
        {
            this.response = "";
            NetworkStream stream = this.GetStream();
            Byte[] sendBytes = Encoding.Default.GetBytes(buffer + crlf);
            if (!stream.CanWrite) { return false; }
            try
            {
                stream.Write(sendBytes, 0, sendBytes.Length);
            }
            catch (Exception ex)
            {
                errormessage = ex.ToString();
                return false;
            }
            if (getresponse) { rd(); }
            return true;
        }
        //--------------------------------------------------------------------
        private void rd()
        {
            response = "";
            NetworkStream stream = this.GetStream();
            if (!stream.CanRead) { return; }

            int timeout = System.Environment.TickCount;
            while (!stream.DataAvailable &&
                (System.Environment.TickCount - timeout < 2000))
            {
                System.Threading.Thread.Sleep(100);
            }
            if (!stream.DataAvailable)
            {
                return;
            }

            byte[] bytes = new byte[this.ReceiveBufferSize];
            try
            {
                stream.Read(bytes,
                    0, (int)this.ReceiveBufferSize);
                response = Encoding.Default.GetString(bytes);
            }
            catch (Exception ex)
            {
                errormessage = ex.ToString();
            }
        }
        //--------------------------------------------------------------------
    }
}

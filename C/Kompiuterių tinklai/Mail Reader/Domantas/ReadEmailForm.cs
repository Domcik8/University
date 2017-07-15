using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using ActiveUp.Net.Mail;
//using OpenPop.Pop3;

namespace email
{
    delegate void SetTextCallback(string text);
    public partial class ReadEmailForm : Form
    {

        MainMenu menu;
        string hostname = "pop.gmail.com";
        int port = 995;
        bool useSsl = true;
        string username = LoginForm.userName;
        string password = LoginForm.password;
        int retrieve = 200;
        string name;
        string address;
        public ReadEmailForm(MainMenu menu)
        {
            this.menu = menu;
            InitializeComponent();
           // testing();
        }
        //-----------------------------------------------------------------------------------
        private void backButton_Click(object sender, EventArgs e)
        {
            menu.Show();
            this.Hide();
        }
        //-----------------------------------------------------------------------------------
        private void testing()
        {
            using(Imap imap = new Imap())
        {
            imap.ConnectSSL("imap.gmail.com", 993);
            imap.Login(username, password);
            imap.SelectInbox();
            List<long> uids = imap.Search(Flag.Unseen);

            foreach (long uid in uids)
            {
                var eml = imap.GetMessageByUID(uid);
                IMail email = new MailBuilder()
                    .CreateFromEml(eml);
                Console.WriteLine(email.Subject);
                Console.WriteLine(email.Text);
            }
            imap.Close();
        }
        }
        //---------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //----------------------------------------------------------------------------------------------
        private void loadButton_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (this.dataGridView.SelectedRows.Count == 1)
                {
                    string messageText = this.dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                    messageTextBox.Text = messageText;

                    
                    
                    using (Imap imap = new Imap())
                    {
                        imap.ConnectSSL("imap.gmail.com", 993);
                        imap.Login(username, password);

                        imap.SelectInbox();
                        List<long> uids = imap.Search(Flag.Unseen);
                        //MessageBox.Show(this.dataGridView.SelectedRows[0].Cells[4].Value.ToString());
                        if (uids.Count > 0)
                            imap.MarkMessageSeenByUID(Convert.ToInt64(this.dataGridView.SelectedRows[0].Cells[4].Value.ToString()));
                       
                        imap.Close();
                    }
                    

                }
            }

        }
        //------------------------------------------------------------------------------------
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.amountLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.amountLabel.Text = text;
            }
        }
        //------------------------------------------------------------------------------------
        private void loadNewButton_Click(object sender, EventArgs e)
        {

            this.dataGridView.DataSource = null;
            this.dataGridView.Rows.Clear();
            int i = 0;
            

            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap.gmail.com", 993);
                imap.Login(username, password);
                imap.SelectInbox();
                List<long> uids = imap.Search(Flag.Unseen);

               // List<long> uids = imap.GetAll();
               // int count = uids.Count;

               // uids = uids.GetRange(100, 110);

                foreach (long uid in uids)
                {
                   // var eml = imap.GetMessageByUID(uid);
                    var eml = imap.PeekMessageByUID(uid);
                    IMail email = new MailBuilder()
                        .CreateFromEml(eml);
                    
                    foreach (MailBox m in email.From) 
                    {
                        address = m.Address;
                        name = m.Name;
                    }

                    //dataGridView.column
                    string[] row = new string[] {email.Date.ToString(), address, email.Subject, email.Text, uid.ToString() };
                    
                    i++;
                    SetText(i.ToString());
                    dataGridView.Rows.Add(row);
                    Console.WriteLine(email.Subject);
                    Console.WriteLine(email.Text);
                }
                imap.Close();
            }
        }

        private void loadLastButton_Click(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------
        private void replyButton_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView.Rows)
          //  {
                if (this.dataGridView.SelectedRows.Count == 1)
                {
                    string from = this.dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                    this.Hide();
                    SendEmailForm se = new SendEmailForm(this, from);
                    se.Show();
                }
           // }
        }
        //-----------------------------------------------------------------------------------.
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        //-----------------------------------------------------------------------------------
        private void LoadButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcpclient = new TcpClient();
                tcpclient.Connect(hostname, port);
                System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());
                sslstream.AuthenticateAsClient(hostname);
               
                System.IO.StreamWriter sw = new StreamWriter(sslstream);
                System.IO.StreamReader reader = new StreamReader(sslstream);
                // pop rfc komandos

                sw.WriteLine("USER " + username);
                sw.Flush();
                sw.WriteLine("PASS " + password);
                sw.Flush();
                sw.WriteLine("STAT");
                sw.Flush();
                sw.WriteLine("RETR " + retrieve);
                sw.Flush();
                sw.WriteLine("RSET");
                sw.Flush();
                sw.WriteLine("Quit ");
                sw.Flush(); 


                string str = string.Empty;
                string strTemp = string.Empty;
              //  reader.ReadLine(); reader.ReadLine(); reader.ReadLine(); 
                
                while ((strTemp = reader.ReadLine()) != null)
                {
                    // randa . eiluteje
                    if (strTemp == ".")
                    {
                        break;
                    }
                    if (strTemp.IndexOf("-ERR") != -1)
                    {
                        break;
                    }
                    str += strTemp;
                    str += System.Environment.NewLine;
                }
                messageTextBox.Text = str;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        //------------------------------------------------------------------------------------
    }
}








/*
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using ActiveUp.Net.Mail;
//using OpenPop.Pop3;

namespace email
{
    delegate void SetTextCallback(string text);
    public partial class ReadEmailForm : Form
    {

        MainMenu menu;
        string hostname = "pop.gmail.com";
        int port = 995;
        bool useSsl = true;
        string username = LoginForm.userName;
        string password = LoginForm.password;
        List<OpenPop.Mime.Message> allMessages;
        int retrieve;
        public ReadEmailForm(MainMenu menu)
        {
            this.menu = menu;
            InitializeComponent();

            Thread workerThread = new Thread(FetchAllMessages);
            workerThread.Start();



 



            //idk();
            
            Thread workerThread = new Thread(FetchAllMessages);
            workerThread.Start();
            Thread.Sleep(5000);
            messageTextBox.Text = ASCIIEncoding.ASCII.GetString(allMessages[0].RawMessage);     
            Console.WriteLine("working");
           // FetchAllMessages();.
             






        }
        //-----------------------------------------------------------------------------------
        private void backButton_Click(object sender, EventArgs e)
        {
            menu.Show();
            this.Hide();
        }
        //-----------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------
        public void idk()
        {
            try
            {
                TcpClient tcpclient = new TcpClient();

                // HOST NAME POP SERVER and gmail uses port number 995 for POP 

                tcpclient.Connect("pop.gmail.com", 995);

                // This is Secure Stream // opened the connection between client and POP Server

                System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream());

                // authenticate as client  

                sslstream.AuthenticateAsClient("pop.gmail.com");

                //bool flag = sslstream.IsAuthenticated;   // check flag

                // Asssigned the writer to stream 

                System.IO.StreamWriter sw = new StreamWriter(sslstream);

                // Assigned reader to stream

                System.IO.StreamReader reader = new StreamReader(sslstream);

                // refer POP rfc command, there very few around 6-9 command

                sw.WriteLine("USER "+username);
                sw.Flush(); 
                sw.WriteLine("PASS "+password);
                sw.Flush();

                // RETR 1 will retrive your first email. it will read content of your first email

                sw.WriteLine("RETR "+retrieve);

                sw.Flush();
                // close the connection

                sw.WriteLine("Quit ");
                sw.Flush(); string str = string.Empty;
                string strTemp = string.Empty;
                while ((strTemp = reader.ReadLine()) != null)
                {
                    // find the . character in line
                    if (strTemp == ".")
                    {
                        break;
                    }
                    if (strTemp.IndexOf("-ERR") != -1)
                    {
                        break;
                    }
                    str += strTemp;
                }
                messageTextBox.Text = str;
               // Console.WriteLine(str);
               // Console.WriteLine("<BR>" + "Congratulation.. ....!!! You read your first gmail email ");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        //---------------------------------------------------------------------------------
       public void fetchMessages(List<string> seenUids)
        {
            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(this.hostname, this.port, this.useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(LoginForm.userName, LoginForm.password);

             
            }
        }
        //-----------------------------------------------------------------------------------
        public void FetchAllMessages() //List<OpenPop.Mime.Message>
        {
            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();
                SetText(messageCount.ToString());
                //amountLabel.Text = messageCount.ToString();
                

                // We want to download all messages
                allMessages = new List<OpenPop.Mime.Message>(messageCount); //List<OpenPop.Mime.Message>

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = 10; i > 0; i--) //(int i = messageCount; i > messageCount-10; i--)
                {
                    Console.WriteLine(i);
                    allMessages.Add(client.GetMessage(i));
                }

                // Now return the fetched messages
                // return allMessages;
                //messageTextBox.Text = ASCIIEncoding.ASCII.GetString(allMessages[1].RawMessage);
            }
        }
        //--------------------------------------------------------------------------------------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //----------------------------------------------------------------------------------------------
        private void loadButton_Click(object sender, EventArgs e)
        {
            if (loadTextBox.Text != "")
            {
                retrieve = int.Parse(loadTextBox.Text);
                idk();
            }
            
        }
        //------------------------------------------------------------------------------------
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.amountLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.amountLabel.Text = text;
            }
        }
        //-----------------------------------------------------------------------------------
    }
}







*/
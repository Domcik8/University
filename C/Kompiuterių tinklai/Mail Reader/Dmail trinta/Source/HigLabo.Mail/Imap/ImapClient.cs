using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using G = System.Collections.Generic;
using HigLabo.Net.Mail;
using HigLabo.Net.Internal;
using HigLabo.Net.Pop3;

namespace HigLabo.Net.Imap
{
    public class ImapClient : SocketClient, IDisposable
    {
        public static readonly Int32 DefaultTagNo = 1;
        public class RegexList //+
        {
            public static readonly Regex SelectFolderResultFlagsLine = new Regex(@"^\* FLAGS \((?<Flags>[^)]*)\)\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase); //+
            public static readonly Regex SelectFolderResult = new Regex(@"^\* (?<exst>\d+) EXISTS\r\n\* (?<rcnt>\d+) RECENT\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase); //+
        }
        public static readonly Int32 DefaultPort = 143;
        private ImapConnectionState _State = ImapConnectionState.Disconnected; //+
        public ImapConnectionState State
        {
            get
            {
                if (this.Socket == null ||
                    this.Socket.Connected == false)
                {
                    this._State = ImapConnectionState.Disconnected;
                }
                return this._State;
            }
        }
        public ImapFolder CurrentFolder { get; private set; }
        public Boolean Available
        {
            get { return this._State != ImapConnectionState.Disconnected; }
        }
        public Int32 TagNo { get; set; } //+
        private String Tag //+
        {
            get { return "tag" + this.TagNo; }
        }
        public ImapClient(String serverName)
            : base(serverName, DefaultPort)
        {
            this.TagNo = ImapClient.DefaultTagNo;
        }
        public ImapClient(String serverName, Int32 port, String userName, String password)  //+
            : base(serverName, port)   //+
        {
            this.UserName = userName; //+
            this.Password = password; //+
            this.TagNo = ImapClient.DefaultTagNo;  //+
        }
        public ImapConnectionState Open()  //Nustato ar esame prisijunge ir grazina connecter/disconnected
        {
            if (this.Connect() == true)  //Grazina true jei turim socketa sujungta su hosty ir ne null Stream
            {
                this._State = ImapConnectionState.Connected; //+
            }
            else
            {
                this._State = ImapConnectionState.Disconnected; //+
            }
            return this._State; //+
        }
        public ImapConnectionState EnsureOpen() //Grazina musu connection state (connected,  disconnected, authenticated, idle)
        {
            if ((this.Socket != null) &&  //+
                this.Socket.Connected) //+
            { return this._State; }  //+

            return this.Open();    //Nustato ar esame prisijunge ir grazina connecter/disconnected
        }
        private void ValidateState(ImapConnectionState state)  //Meta error jei esame blogame state
        {
            this.ValidateState(state, false);  //Meta error jei esame blogame state arba isrinkome neegzistuojanti folderi
        }
        private void ValidateState(ImapConnectionState state, Boolean folderSelected) //Meta error jei esame blogame state arba isrinkome neegzistuojanti folderi
        {
            if (this._State != state) //+
            {
                switch (state) //+
                {
                    case ImapConnectionState.Disconnected: throw new MailClientException("You can execute this command only when State is Disconnected"); //+
                    case ImapConnectionState.Connected: throw new MailClientException("You can execute this command only when State is Connected"); //+
                    case ImapConnectionState.Authenticated: throw new MailClientException("You can execute this command only when State is Authenticated"); //+
                    default: throw new MailClientException(); //+
                }
            }
            if (folderSelected == true && this.CurrentFolder == null)
            {
                throw new MailClientException("You must select folder before executing this command." //+
                  + "You can select folder by calling SelectFolder,ExecuteSelect,ExecuteExamine method of this object."); //+
            }
        }
        private String GetResponse() //Grazina response kaip string
        {
            MemoryStream ms = new MemoryStream(); //+
            this.GetResponse(ms); //Gauna atsakyma ir uzraso ji i duota stream
            return this.ResponseEncoding.GetString(ms.ToArray()); //Uzkoduoja Stream kaip string 
        }
        private void GetResponse(Stream stream) //Gauna atsakyma ir uzraso ji i duota stream
        {
            Byte[] bb = this.GetResponseBytes(new ImapDataReceiveContext(this.Tag, this.ResponseEncoding)); //Laukia atsakymo is context streamo //Nustatomas tagas, encoding ir kiti duomenys 
            this.ReadText(stream, bb);  //I duota streama uzraso baitu array
            this.Commnicating = false;
        }
        private void ReadText(Stream stream, Byte[] bytes) //I duota streama uzraso baitu array
        {
            String CurrentLine = ""; //+
            Byte[] bb = null; //+
            StringReader sr = new StringReader(this.ResponseEncoding.GetString(bytes)); //String readeris su uzkoduotu bytes array

            while (true)
            {
                CurrentLine = sr.ReadLine();  //+
                if (CurrentLine == null) { break; } //+
                bb = ResponseEncoding.GetBytes(CurrentLine + MailParser.NewLine); //+
                stream.Write(bb, 0, bb.Length); //Raso i streama duomenys
            }
        }
        public Boolean Authenticate()  //Grazina true jeiauthenticated
        {
            if (this._State == ImapConnectionState.Authenticated) { return true; } //+
            var rs = this.ExecuteLogin(); //Grazina informacija apie atsakyma (ar geras ar ne) ir nustato ImapConnectionState (authenticated / conneceted)
            return this.State == ImapConnectionState.Authenticated;
        }
        private String Execute(String command) //Ivykdo komanda ir grazina atsakyma
        {
            this.Send(command);  //Nusiuncia ASCII uzkoduoda komanda su new line simoliu
            this.Commnicating = true;
            return this.GetResponse(); //Grazina response kaip string
        }
        public CapabilityResult ExecuteCapability()
        {
            String s = this.Execute(this.Tag + " CAPABILITY");
            return new CapabilityResult(this.Tag, s);
        }
        public ImapCommandResult ExecuteLogin()   //Grazina informacija apie atsakyma (ar geras ar ne) ir nustato ImapConnectionState (authenticated / conneceted)
        {
            if (this.EnsureOpen() == ImapConnectionState.Disconnected) { throw new MailClientException(); } //Metamas error jei esame disconnected ir negalima prisijungti

            String commandText = String.Format(this.Tag + " LOGIN {0} {1}", this.UserName, this.Password);
            String s = this.Execute(commandText); //Ivykdo komanda ir grazina atsakyma
            ImapCommandResult rs = new ImapCommandResult(this.Tag, s); //Laiko informacija apie atsakyma (ar geras ar ne)
            if (rs.Status == ImapCommandResultStatus.Ok) //+
            {
                this._State = ImapConnectionState.Authenticated; //+
            }
            else //+
            {
                this._State = ImapConnectionState.Connected; //+
            }
            return rs; //+
        }
        public ImapCommandResult ExecuteLogout()
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " Logout");
            var rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok)
            {
                this._State = ImapConnectionState.Connected;
            }
            return rs;
        }
        /// <summary>
        /// Send select command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public SelectResult ExecuteSelect(String folderName) //Sudeda informacija apie rezultata i ImapFolder ir grazina SelectResult su ta pacia informacija
        {
            this.ValidateState(ImapConnectionState.Authenticated); //Meta error jei esame blogame state
            //String commandText = String.Format(this.Tag + " Select {0}", NamingConversion.EncodeString(folderName)); //Uzkodoja folder name i base64, bet to nenoriu
            //String s = this.Execute(commandText);
            String s = this.Execute(this.Tag + " Select " + folderName);  //Ivykdo komanda ir grazina atsakyma
            var rs = this.GetSelectResult(folderName, s); //Sudeda visa informacija apie rezultata i SelectResult
            this.CurrentFolder = new ImapFolder(rs); //Sudeda informacija apie rezultata i ImapFolder
            return rs; //+
        }
        private SelectResult GetSelectResult(String folderName, String text) //Sudeda visa informacija apie rezultata i SelectResult
        {
            var rs = new ImapCommandResult(this.Tag, text); //Laiko informacija apie atsakyma (ar geras ar ne)
            if (rs.Status == ImapCommandResultStatus.Ok) //+
            {
                Int32 exists = 0; //+
                Int32 recent = 0; //+
                List<String> l = new List<string>(); //+
                Match m = null; //+
                m = RegexList.SelectFolderResult.Match(rs.Text); //+
                if (m.Success) //+
                {
                    Int32.TryParse(m.Groups["exst"].Value, out exists); //+
                    Int32.TryParse(m.Groups["rcnt"].Value, out recent); //+
                }
                m = RegexList.SelectFolderResultFlagsLine.Match(rs.Text);
                if (m.Success == true) //+
                {
                    String flags = m.Groups["Flags"].Value; //+
                    foreach (var el in flags.Split(' ')) //+
                    {
                        if (el.StartsWith("\\") == true) //+
                        {
                            l.Add(el.Substring(1, el.Length - 1)); //+
                        }
                    }
                }
                return new SelectResult(folderName, exists, recent, l.ToArray());  //Sudeda visa informacija apie rezultata i objekta
            }
            throw new MailClientException(); //+
        }
       
        public ImapCommandResult ExecuteDelete(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Delete {0}", NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            return new ImapCommandResult(this.Tag, s);
        }
        public ImapFolder SelectFolder(String folderName)  //Sudeda informacija apie rezultata i ImapFolder ir ji grazina
        {
            var rs = this.ExecuteSelect(folderName);  //Sudeda informacija apie rezultata i ImapFolder ir grazina SelectResult su ta pacia informacija
            return new ImapFolder(rs); //+
        }
    }
}
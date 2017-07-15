using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using HigLabo.Net.Mail;
using HigLabo.Net.Internal;
using HigLabo.Net.Pop3;

namespace HigLabo.Net.Smtp
{
    /// Represent and probide functionality about smtp command.
    /// <summary>
    /// Represent and probide functionality about smtp command.
    /// </summary>
    public class SmtpClient : SocketClient, IDisposable
    {
        /// <summary>
        /// Default smtp port
        /// </summary>
        public static readonly Int32 DefaultPort = 25;
        /// <summary>
        /// Default ssl port
        /// </summary>
        public static readonly Int32 DefaultSslPort = 443;
        private SmtpAuthenticateMode _Mode = SmtpAuthenticateMode.Auto;
        private String _HostName = "";
        private Boolean _Tls = false;
        private Pop3Client _Pop3Client = new Pop3Client("127.0.0.1");
        private SmtpConnectionState _State = SmtpConnectionState.Disconnected;
        /// 認証の方法を取得または設定します。
        /// <summary>
        /// 認証の方法を取得または設定します。
        /// </summary>
        public SmtpAuthenticateMode AuthenticateMode
        {
            get { return this._Mode; }
            set { this._Mode = value; }
        }
        /// 送信元マシンのホスト名を取得または設定します。
        /// <summary>
        /// 送信元マシンのホスト名を取得または設定します。
        /// </summary>
        public String HostName
        {
            get { return this._HostName; }
            set { this._HostName = value; }
        }
        /// 通信にTLSを使用するかどうかを示す値を取得します。
        /// <summary>
        /// 通信にTLSを使用するかどうかを示す値を取得します。
        /// </summary>
        public Boolean Tls
        {
            get { return this._Tls; }
            set { this._Tls = value; }
        }
        /// 接続の状態を示す値を取得します。
        /// <summary>
        /// 接続の状態を示す値を取得します。
        /// </summary>
        public SmtpConnectionState State
        {
            get { return this._State; }
        }
        /// サーバーへ接続済みかどうかを示す値を取得します。
        /// <summary>
        /// サーバーへ接続済みかどうかを示す値を取得します。
        /// </summary>
        public Boolean Available
        {
            get { return this._State != SmtpConnectionState.Disconnected; }
        }
        /// PopBeforeSmtp認証を行う場合に使用されるPop3Clientを取得します。
        /// <summary>
        /// PopBeforeSmtp認証を行う場合に使用されるPop3Clientを取得します。
        /// </summary>
        public Pop3Client Pop3Client
        {
            get { return this._Pop3Client; }
        }
        /// <summary>
        /// 
        /// </summary>
        public SmtpClient()
            : base("127.0.0.1", DefaultPort)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        public SmtpClient(String serverName)
            : base(serverName, DefaultPort)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public SmtpClient(String serverName, Int32 port, String userName, String password)
            : base(serverName, port)
        {
            this.UserName = userName;
            this.Password = password;
        }
        /// サーバーへの接続を開きます。
        /// <summary>
        /// サーバーへの接続を開きます。
        /// </summary>
        public SmtpConnectionState Open()
        {
            if (this.Connect() == true)
            {
                var rs = this.GetResponse();
                if (rs.StatusCode == SmtpCommandResultCode.ServiceReady)
                {
                    this._State = SmtpConnectionState.Connected;
                }
                else
                {
                    this.Socket = null;
                    this.Stream = null;
                    this._State = SmtpConnectionState.Disconnected;
                }
            }
            else
            {
                this._State = SmtpConnectionState.Disconnected;
            }
            return this._State;
        }
        /// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
        /// <summary>
        /// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
        /// </summary>
        public SmtpConnectionState EnsureOpen()
        {
            if (this.Socket != null)
            { return this._State; }
            return this.Open();
        }
        private SmtpCommandResult GetResponse()
        {
            List<SmtpCommandResultLine> l = new List<SmtpCommandResultLine>();
            String lineText = "";
            SmtpCommandResultLine CurrentLine = null;
            Byte[] bb = this.GetResponseBytes(new SmtpDataReceiveContext(this.ResponseEncoding));
            StringReader sr = new StringReader(this.ResponseEncoding.GetString(bb));
            while (true)
            {
                lineText = sr.ReadLine();
                CurrentLine = new SmtpCommandResultLine(lineText);
                if (CurrentLine.InvalidFormat == true)
                {
                    throw new MailClientException("Invalid format response." + Environment.NewLine + lineText);
                }
                l.Add(CurrentLine);
                //次の行があるかチェック
                if (CurrentLine.HasNextLine == false)
                { break; }
            }
            this.SetSmtpCommandState();
            return new SmtpCommandResult(l.ToArray());
        }
        /// SMTPコマンドの種類に基づいて状態を変化させます。
        /// <summary>
        /// SMTPコマンドの種類に基づいて状態を変化させます。
        /// </summary>
        /// <param name="command"></param>
        private void SetSmtpCommandState(SmtpCommand command)
        {
            if (command is MailCommand)
            {
                this._State = SmtpConnectionState.MailFromCommandExecuting;
            }
            else if (command is RcptCommand)
            {
                this._State = SmtpConnectionState.RcptToCommandExecuting;
            }
            else if (command is DataCommand)
            {
                this._State = SmtpConnectionState.DataCommandExecuting;
            }
        }
        /// サーバーからのレスポンスの受信時に現在の状態に基づいて状態を変化させます。
        /// <summary>
        /// サーバーからのレスポンスの受信時に現在の状態に基づいて状態を変化させます。
        /// </summary>
        private void SetSmtpCommandState()
        {
            this.Commnicating = false;
            switch (this._State)
            {
                case SmtpConnectionState.MailFromCommandExecuting: this._State = SmtpConnectionState.MailFromCommandExecuted; break;
                case SmtpConnectionState.RcptToCommandExecuted: this._State = SmtpConnectionState.RcptToCommandExecuted; break;
                case SmtpConnectionState.DataCommandExecuting: this._State = SmtpConnectionState.DataCommandExecuted; break;
            }
        }
        /// SMTPサーバーに認証が必要かどうかを示す値を取得します。
        /// <summary>
        /// SMTPサーバーに認証が必要かどうかを示す値を取得します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static Boolean NeedAuthenticate(String text)
        {
            return text.IndexOf("auth", StringComparison.InvariantCultureIgnoreCase) > -1;
        }
        /// StartTLSコマンドをサーバーに対して送信し、暗号化された通信を開始します。
        /// <summary>
        /// StartTLSコマンドをサーバーに対して送信し、暗号化された通信を開始します。
        /// </summary>
        private Boolean StartTls()
        {
            SmtpCommandResult rs = null;

            if (this.EnsureOpen() == SmtpConnectionState.Connected)
            {
                rs = this.Execute("STARTTLS");
                if (rs.StatusCode != SmtpCommandResultCode.ServiceReady)
                { return false; }

                this.Ssl = true;
                this._Tls = true;
                SslStream ssl = new SslStream(new NetworkStream(this.Socket), true, this.RemoteCertificateValidationCallback, null);
                ssl.AuthenticateAsClient(this.ServerName);
                this.Stream = ssl;
                return true;
            }
            return false;
        }
        /// SMTPメールサーバーへログインします。
        /// <summary>
        /// SMTPメールサーバーへログインします。
        /// </summary>
        /// <returns></returns>
        public Boolean Authenticate()
        {
            SmtpCommandResult rs = null;

            if (this._Mode == SmtpAuthenticateMode.Auto)
            {
                if (this.EnsureOpen() == SmtpConnectionState.Connected)
                {
                    rs = this.ExecuteEhlo();
                    String s = rs.Message.ToUpper();
                    //SMTP認証に対応している場合
                    if (s.Contains("AUTH") == true)
                    {
                        if (s.Contains("PLAIN") == true)
                        { return this.AuthenticateByPlain(); }
                        if (s.Contains("LOGIN") == true)
                        { return this.AuthenticateByLogin(); }
                        if (s.Contains("CRAM-MD5") == true)
                        { return this.AuthenticateByCramMD5(); }
                    }
                    else
                    {
                        rs = this.ExecuteEhlo();
                        return rs.StatusCode == SmtpCommandResultCode.ServiceReady;
                    }
                    //TLS認証
                    if (this.Tls == true)
                    {
                        if (s.Contains("STARTTLS") == false)
                        { throw new MailClientException("TLS is not allowed."); }
                        this.StartTls();
                        rs = this.ExecuteEhlo();
                        return rs.StatusCode == SmtpCommandResultCode.ServiceReady;
                    }
                }
            }
            else
            {
                switch (this._Mode)
                {
                    case SmtpAuthenticateMode.None: return true;
                    case SmtpAuthenticateMode.Plain: return this.AuthenticateByPlain();
                    case SmtpAuthenticateMode.Login: return this.AuthenticateByLogin();
                    case SmtpAuthenticateMode.Cram_MD5: return this.AuthenticateByCramMD5();
                    case SmtpAuthenticateMode.PopBeforeSmtp:
                        {
                            Boolean bl = this._Pop3Client.Authenticate();
                            this._Pop3Client.Close();
                            return bl;
                        }
                }
                throw new InvalidOperationException();
            }
            return false;
        }
        /// SMTPメールサーバーへPlain認証でログインします。
        /// <summary>
        /// SMTPメールサーバーへPlain認証でログインします。
        /// </summary>
        /// <returns></returns>
        public Boolean AuthenticateByPlain()
        {
            SmtpCommandResult rs = null;

            if (this.EnsureOpen() == SmtpConnectionState.Connected)
            {
                rs = this.Execute("Auth Plain");
                if (rs.StatusCode != SmtpCommandResultCode.WaitingForAuthentication)
                { return false; }
                //ユーザー名＆パスワードの文字列を送信
                rs = this.Execute(MailParser.ToBase64String(String.Format("{0}\0{0}\0{1}", this.UserName, this.Password)));
                if (rs.StatusCode == SmtpCommandResultCode.AuthenticationSuccessful)
                {
                    this._State = SmtpConnectionState.Authenticated;
                }
            }
            return this._State == SmtpConnectionState.Authenticated;
        }
        /// SMTPメールサーバーへLogin認証でログインします。
        /// <summary>
        /// SMTPメールサーバーへLogin認証でログインします。
        /// </summary>
        /// <returns></returns>
        public Boolean AuthenticateByLogin()
        {
            SmtpCommandResult rs = null;

            if (this.EnsureOpen() == SmtpConnectionState.Connected)
            {
                rs = this.Execute("Auth Login");
                if (rs.StatusCode != SmtpCommandResultCode.WaitingForAuthentication)
                { return false; }
                //ユーザー名送信
                rs = this.Execute(MailParser.ToBase64String(this.UserName));
                if (rs.StatusCode != SmtpCommandResultCode.WaitingForAuthentication)
                { return false; }
                //パスワード送信
                rs = this.Execute(MailParser.ToBase64String(this.Password));
                if (rs.StatusCode == SmtpCommandResultCode.AuthenticationSuccessful)
                {
                    this._State = SmtpConnectionState.Authenticated;
                }
            }
            return this._State == SmtpConnectionState.Authenticated;
        }
        /// SMTPメールサーバーへCRAM-MD5認証でログインします。
        /// <summary>
        /// SMTPメールサーバーへCRAM-MD5認証でログインします。
        /// </summary>
        /// <returns></returns>
        public Boolean AuthenticateByCramMD5()
        {
            SmtpCommandResult rs = null;

            if (this.EnsureOpen() == SmtpConnectionState.Connected)
            {
                rs = this.Execute("Auth CRAM-MD5");
                if (rs.StatusCode != SmtpCommandResultCode.WaitingForAuthentication)
                { return false; }
                //ユーザー名＋チャレンジ文字列をBase64エンコードした文字列を送信
                String s = MailParser.ToCramMd5String(rs.Message, this.UserName, this.Password);
                rs = this.Execute(s);
                if (rs.StatusCode == SmtpCommandResultCode.AuthenticationSuccessful)
                {
                    this._State = SmtpConnectionState.Authenticated;
                }
            }
            return this._State == SmtpConnectionState.Authenticated;
        }
        /// 同期でSMTPメールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して返します。
        /// <summary>
        /// 同期でSMTPメールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して返します。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public SmtpCommandResult Execute(SmtpCommand command)
        {
            return this.Execute(command.GetCommandString());
        }
        /// 同期でSMTPメールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して返します。
        /// <summary>
        /// 同期でSMTPメールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して返します。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private SmtpCommandResult Execute(String command)
        {
            this.Send(command);
            this.Commnicating = true;
            return this.GetResponse();
        }
        /// SMTPメールサーバーへEHLOコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへEHLOコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteEhlo()
        {
            this.EnsureOpen();
            return this.Execute(new EhloCommand(this._HostName));
        }
        /// SMTPメールサーバーへHELOコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへHELOコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteHelo()
        {
            this.EnsureOpen();
            return this.Execute(new HeloCommand(this._HostName));
        }
        private SmtpCommandResult ExecuteEhloAndHelo()
        {
            SmtpCommandResult rs = null;
            //サーバーへメールトランザクションの開始コマンドを送信
            rs = this.ExecuteEhlo();
            if (rs.StatusCode != SmtpCommandResultCode.RequestedMailActionOkay_Completed)
            {
                rs = this.ExecuteHelo();
            }
            return rs;
        }
        /// SMTPメールサーバーへMAILコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへMAILコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteMail(String reversePath)
        {
            this.EnsureOpen();
            return this.Execute(new MailCommand(reversePath));
        }
        /// SMTPメールサーバーへRCPTコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへRCPTコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteRcpt(String forwardPath)
        {
            this.EnsureOpen();
            return this.Execute(new RcptCommand(forwardPath));
        }
        /// SMTPメールサーバーへDATAコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへDATAコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteData()
        {
            this.EnsureOpen();
            return this.Execute(new DataCommand());
        }
        /// SMTPメールサーバーへRESETコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへRESETコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteRset()
        {
            this.EnsureOpen();
            return this.Execute(new RsetCommand());
        }
        /// SMTPメールサーバーへVRFYコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへVRFYコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteVrfy(String userName)
        {
            this.EnsureOpen();
            return this.Execute(new VrfyCommand(userName));
        }
        /// SMTPメールサーバーへEXPNコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへEXPNコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteExpn(String mailingList)
        {
            this.EnsureOpen();
            return this.Execute(new ExpnCommand(mailingList));
        }
        /// SMTPメールサーバーへHELPコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへHELPコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteHelp()
        {
            this.EnsureOpen();
            return this.Execute(new HelpCommand());
        }
        /// SMTPメールサーバーへNOOPコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへNOOPコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteNoop()
        {
            this.EnsureOpen();
            return this.Execute("Noop");
        }
        /// SMTPメールサーバーへQUITコマンドを送信します。
        /// <summary>
        /// SMTPメールサーバーへQUITコマンドを送信します。
        /// </summary>
        /// <returns></returns>
        public SmtpCommandResult ExecuteQuit()
        {
            this.EnsureOpen();
            var rs = this.Execute("Quit");
            //サーバー側から強制的に接続が切断されるので一度Disposeを呼び出してTcpClient=nullにする。
            if (rs.StatusCode == SmtpCommandResultCode.ServiceClosingTransmissionChannel)
            {
                this.DisposeSocket();
            }
            return rs;
        }
        /// メールを送信し、送信結果となるSendMailResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailResultを取得します。
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public SendMailResult SendMail(String from, String to, String cc, String bcc, String text)
        {
            List<MailAddress> l = new List<MailAddress>();
            String[] ss = null;

            ss = to.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                if (String.IsNullOrEmpty(ss[i]) == true)
                { continue; }
                l.Add(MailAddress.Create(ss[i]));
            }
            ss = cc.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                if (String.IsNullOrEmpty(ss[i]) == true)
                { continue; }
                l.Add(MailAddress.Create(ss[i]));
            }
            ss = bcc.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                if (String.IsNullOrEmpty(ss[i]) == true)
                { continue; }
                l.Add(MailAddress.Create(ss[i]));
            }
            return this.SendMail(new SendMailCommand(from, text, l));
        }
        /// メールを送信し、送信結果となるSendMailResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailResultを取得します。
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SendMailResult SendMail(String from, SmtpMessage message)
        {
            return this.SendMail(new SendMailCommand(from, message));
        }
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public SendMailResult SendMail(SmtpMessage message)
        {
            return this.SendMail(new SendMailCommand(message));
        }
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public SendMailListResult SendMailList(IEnumerable<SmtpMessage> messages)
        {
            List<SendMailCommand> l = new List<SendMailCommand>();
            foreach (var mg in messages)
            {
                l.Add(new SendMailCommand(mg));
            }
            return this.SendMailList(l.ToArray());
        }
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public SendMailResult SendMail(SendMailCommand command)
        {
            var l = this.SendMailList(new[] { command });
            if (l.Results.Count == 1)
            {
                return new SendMailResult(l.Results[0].State, command);
            }
            return new SendMailResult(l.State, command);
        }
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// <summary>
        /// メールを送信し、送信結果となるSendMailListResultを取得します。
        /// </summary>
        /// <param name="commandList"></param>
        /// <returns></returns>
        public SendMailListResult SendMailList(IEnumerable<SendMailCommand> commandList)
        {
            SmtpCommandResult rs = null;
            Boolean HasRcpt = false;

            //接続失敗
            if (this.EnsureOpen() == SmtpConnectionState.Disconnected)
            { return new SendMailListResult(SendMailResultState.Connection); }

            //不正な状態でのメソッドの実行
            if (this.State != SmtpConnectionState.Connected &&
                this.State != SmtpConnectionState.Authenticated)
            {
                return new SendMailListResult(SendMailResultState.InvalidState);
            }
            //認証済みで無い場合
            if (this.State != SmtpConnectionState.Authenticated)
            {
                //サーバーへメールトランザクションの開始コマンドを送信
                rs = this.ExecuteEhloAndHelo();
                if (rs.StatusCode != SmtpCommandResultCode.RequestedMailActionOkay_Completed)
                { return new SendMailListResult(SendMailResultState.Helo); }
                //TLS/SSL通信
                if (this._Tls == true)
                {
                    if (this.StartTls() == false)
                    { return new SendMailListResult(SendMailResultState.Tls); }
                    rs = this.ExecuteEhloAndHelo();
                    if (rs.StatusCode != SmtpCommandResultCode.RequestedMailActionOkay_Completed)
                    { return new SendMailListResult(SendMailResultState.Helo); }
                }
                //ログイン認証が必要とされるかチェック
                if (SmtpClient.NeedAuthenticate(rs.Message) == true)
                {
                    if (this.Authenticate() == false)
                    { return new SendMailListResult(SendMailResultState.Authenticate); }
                }
            }

            List<SendMailResult> results = new List<SendMailResult>();

            foreach (var command in commandList)
            {
                //Mail Fromの送信
                rs = this.ExecuteMail(command.From);
                if (rs.StatusCode != SmtpCommandResultCode.RequestedMailActionOkay_Completed)
                {
                    results.Add(new SendMailResult(SendMailResultState.MailFrom, command));
                    continue;
                }
                List<MailAddress> mailAddressList = new List<MailAddress>();
                //Rcpt Toの送信
                foreach (var m in command.RcptTo)
                {
                    String mailAddress = m.ToString();
                    if (mailAddress.StartsWith("<") == true && mailAddress.EndsWith(">") == true)
                    {
                        rs = this.ExecuteRcpt(mailAddress);
                    }
                    else
                    {
                        rs = this.ExecuteRcpt("<" + mailAddress + ">");
                    }
                    if (rs.StatusCode == SmtpCommandResultCode.RequestedMailActionOkay_Completed)
                    {
                        HasRcpt = true;
                    }
                    else
                    {
                        mailAddressList.Add(m);
                    }
                }
                if (HasRcpt == false)
                {
                    results.Add(new SendMailResult(SendMailResultState.Rcpt, command, mailAddressList));
                    continue;
                }
                //Dataの送信
                rs = this.ExecuteData();
                if (rs.StatusCode == SmtpCommandResultCode.StartMailInput)
                {
                    this.Send(command.Text + MailParser.NewLine + ".");
                    rs = this.GetResponse();
                    if (rs.StatusCode == SmtpCommandResultCode.RequestedMailActionOkay_Completed)
                    {
                        results.Add(new SendMailResult(SendMailResultState.Success, command, mailAddressList));
                        this.ExecuteRset();
                    }
                    else
                    {
                        results.Add(new SendMailResult(SendMailResultState.Data, command, mailAddressList));
                    }
                }
                else
                {
                    results.Add(new SendMailResult(SendMailResultState.Data, command, mailAddressList));
                }
            }
            rs = this.ExecuteQuit();
            //全て成功したかどうかチェック
            if (results.Exists(el => el.State != SendMailResultState.Success) == true)
            {
                return new SendMailListResult(SendMailResultState.SendMailData, results);
            }
            return new SendMailListResult(SendMailResultState.Success, results);
        }
        /// <summary>
        /// 
        /// </summary>
        ~SmtpClient()
        {
            this.Dispose(false);
        }
    }
}

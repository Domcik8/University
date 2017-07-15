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
    /// Represent and probide functionality about IMAP command.
    /// <summary>
    /// Represent and probide functionality about IMAP command.
    /// </summary>
    public class ImapClient : SocketClient, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Int32 DefaultTagNo = 1;
        /// <summary>
        /// 
        /// </summary>
        public class RegexList
        {
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex SelectFolderResultFlagsLine = new Regex(@"^\* FLAGS \((?<Flags>[^)]*)\)\r\n", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex SelectFolderResult = new Regex(@"^\* (?<exst>\d+) EXISTS\r\n\* (?<rcnt>\d+) RECENT\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex GetListFolderResult = new Regex("^\\* LIST \\(((?<opt>\\\\\\w+)\\s?)+\\) \".\" \"(?<name>.*?)\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex GetLsubFolderResult = new Regex("^\\* LSUB \\(((?<opt>\\\\\\w+)\\s?)+\\) \".\" \"(?<name>.*?)\"", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex GetRlsubFolderResult = new Regex("^\\* LSUB \\(\\) \".\" (?<name>.*)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly Int32 DefaultPort = 143;
        private ImapConnectionState _State = ImapConnectionState.Disconnected;
        /// <summary>
        /// Get connection state.
        /// </summary>
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
        /// <summary>
        /// Get selected folder
        /// </summary>
        public ImapFolder CurrentFolder { get; private set; }
        /// <summary>
        /// Get connection is ready.
        /// </summary>
        public Boolean Available
        {
            get { return this._State != ImapConnectionState.Disconnected; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Int32 TagNo { get; set; }
        private String Tag
        {
            get { return "tag" + this.TagNo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ImapClient(String serverName)
            : base(serverName, DefaultPort)
        {
            this.TagNo = ImapClient.DefaultTagNo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public ImapClient(String serverName, Int32 port, String userName, String password)
            : base(serverName, port)
        {
            this.UserName = userName;
            this.Password = password;
            this.TagNo = ImapClient.DefaultTagNo;
        }
        /// <summary>
        /// Open connection to a server.
        /// </summary>
        public ImapConnectionState Open()
        {
            if (this.Connect() == true)
            {
                this._State = ImapConnectionState.Connected;
            }
            else
            {
                this._State = ImapConnectionState.Disconnected;
            }
            return this._State;
        }
        /// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
        /// <summary>
        /// Ensure connection is opened.
        /// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
        /// </summary>
        public ImapConnectionState EnsureOpen()
        {
            if ((this.Socket != null) &&
                this.Socket.Connected)
            { return this._State; }

            return this.Open();
        }
        private void ValidateState(ImapConnectionState state)
        {
            this.ValidateState(state, false);
        }
        private void ValidateState(ImapConnectionState state, Boolean folderSelected)
        {
            if (this._State != state)
            {
                switch (state)
                {
                    case ImapConnectionState.Disconnected: throw new MailClientException("You can execute this command only when State is Disconnected");
                    case ImapConnectionState.Connected: throw new MailClientException("You can execute this command only when State is Connected");
                    case ImapConnectionState.Authenticated: throw new MailClientException("You can execute this command only when State is Authenticated");
                    default: throw new MailClientException();
                }
            }
            if (folderSelected == true && this.CurrentFolder == null)
            {
                throw new MailClientException("You must select folder before executing this command."
                  + "You can select folder by calling SelectFolder,ExecuteSelect,ExecuteExamine method of this object.");
            }
        }
        private String GetResponse()
        {
            MemoryStream ms = new MemoryStream();
            this.GetResponse(ms);
            return this.ResponseEncoding.GetString(ms.ToArray());
        }
        private void GetResponse(Stream stream)
        {
            Byte[] bb = this.GetResponseBytes(new ImapDataReceiveContext(this.Tag, this.ResponseEncoding));
            this.ReadText(stream, bb);
            this.Commnicating = false;
        }
        private void ReadText(Stream stream, Byte[] bytes)
        {
            String CurrentLine = "";
            Byte[] bb = null;
            StringReader sr = new StringReader(this.ResponseEncoding.GetString(bytes));

            while (true)
            {
                CurrentLine = sr.ReadLine();
                if (CurrentLine == null) { break; }
                bb = ResponseEncoding.GetBytes(CurrentLine + MailParser.NewLine);
                stream.Write(bb, 0, bb.Length);
            }
        }
        /// <summary>
        /// Log in to IMAP server.
        /// </summary>
        /// <returns></returns>
        public Boolean Authenticate()
        {
            if (this._State == ImapConnectionState.Authenticated) { return true; }
            var rs = this.ExecuteLogin();
            return this.State == ImapConnectionState.Authenticated;
        }
        private String Execute(String command)
        {
            this.Send(command);
            this.Commnicating = true;
            return this.GetResponse();
        }
        /// <summary>
        /// Send capability command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public CapabilityResult ExecuteCapability()
        {
            String s = this.Execute(this.Tag + " CAPABILITY");
            return new CapabilityResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteLogin()
        {
            if (this.EnsureOpen() == ImapConnectionState.Disconnected) { throw new MailClientException(); }

            String commandText = String.Format(this.Tag + " LOGIN {0} {1}", this.UserName, this.Password);
            String s = this.Execute(commandText);
            ImapCommandResult rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok)
            {
                this._State = ImapConnectionState.Authenticated;
            }
            else
            {
                this._State = ImapConnectionState.Connected;
            }
            return rs;
        }
        /// <summary>
        /// Send Logout command to IMAP server.
        /// </summary>
        /// <returns></returns>
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
        public SelectResult ExecuteSelect(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Select {0}", NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            var rs = this.GetSelectResult(folderName, s);
            this.CurrentFolder = new ImapFolder(rs);
            return rs;
        }
        /// <summary>
        /// Send examine command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public SelectResult ExecuteExamine(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Examine {0}", NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            var rs = this.GetSelectResult(folderName, s);
            this.CurrentFolder = new ImapFolder(rs);
            return rs;
        }
        private SelectResult GetSelectResult(String folderName, String text)
        {
            var rs = new ImapCommandResult(this.Tag, text);
            if (rs.Status == ImapCommandResultStatus.Ok)
            {
                Int32 exists = 0;
                Int32 recent = 0;
                List<String> l = new List<string>();
                Match m = null;
                m = RegexList.SelectFolderResult.Match(rs.Text);
                if (m.Success)
                {
                    Int32.TryParse(m.Groups["exst"].Value, out exists);
                    Int32.TryParse(m.Groups["rcnt"].Value, out recent);
                }
                m = RegexList.SelectFolderResultFlagsLine.Match(rs.Text);
                if (m.Success == true)
                {
                    String flags = m.Groups["Flags"].Value;
                    foreach (var el in flags.Split(' '))
                    {
                        if (el.StartsWith("\\") == true)
                        {
                            l.Add(el.Substring(1, el.Length - 1));
                        }
                    }
                }
                return new SelectResult(folderName, exists, recent, l.ToArray());
            }
            throw new MailClientException();
        }
        /// <summary>
        /// Send create folder command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteCreate(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Create {0}", NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send delete folder command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteDelete(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Delete {0}", NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send close command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteClose()
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " Close");
            this.CurrentFolder = null;
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send list command to IMAP server.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public ListResult ExecuteList(String folderName, Boolean recursive)
        {
            this.ValidateState(ImapConnectionState.Authenticated);

            List<ListLineResult> l = new List<ListLineResult>();
            String name = "";
            Boolean noSelect = false;
            Boolean hasChildren = false;
            String rc = "%";
            if (recursive == true)
            {
                rc = "*";
            }
            String s = this.Execute(String.Format(this.Tag + " LIST \"{0}\" \"{1}\"", folderName, rc));
            foreach (Match m in RegexList.GetListFolderResult.Matches(s))
            {
                name = NamingConversion.DecodeString(m.Groups["name"].Value);
                foreach (Capture c in m.Groups["opt"].Captures)
                {
                    if (c.Value.ToString() == "\\Noselect")
                    {
                        noSelect = true;
                    }
                    else if (c.Value.ToString() == "\\HasNoChildren")
                    {
                        hasChildren = false;
                    }
                    else if (c.Value.ToString() == "\\HasChildren")
                    {
                        hasChildren = true;
                    }
                }
                l.Add(new ListLineResult(name, noSelect, hasChildren));
            }
            return new ListResult(l);
        }
        /// <summary>
        /// Send subscribe command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteSubscribe(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " subscribe " + folderName);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send unsubscribe command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteUnsubscribe(String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " Unsubscribe " + folderName);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send list command to IMAP server.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public ListResult ExecuteLsub(String folderName, Boolean recursive)
        {
            this.ValidateState(ImapConnectionState.Authenticated);

            List<ListLineResult> l = new List<ListLineResult>();
            String name = "";
            Boolean noSelect = false;
            Boolean hasChildren = false;
            String rc = "%";
            if (recursive == true)
            {
                rc = "*";
            }
            String s = this.Execute(String.Format(this.Tag + " Lsub \"{0}\" \"{1}\"", folderName, rc));
            foreach (Match m in RegexList.GetLsubFolderResult.Matches(s))
            {
                name = NamingConversion.DecodeString(m.Groups["name"].Value);
                foreach (Capture c in m.Groups["opt"].Captures)
                {
                    if (c.Value.ToString() == "\\Noselect")
                    {
                        noSelect = true;
                    }
                    else if (c.Value.ToString() == "\\HasNoChildren")
                    {
                        hasChildren = false;
                    }
                    else if (c.Value.ToString() == "\\HasChildren")
                    {
                        hasChildren = true;
                    }
                }
                l.Add(new ListLineResult(name, noSelect, hasChildren));
            }
            return new ListResult(l);
        }
        /// <summary>
        /// Send Fetch command to IMAP server.
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public MailMessage ExecuteFetch(Int64 mailIndex)
        {
            this.ValidateState(ImapConnectionState.Authenticated, true);
            String s = this.Execute(String.Format(this.Tag + " FETCH {0} (BODY[])", mailIndex));
            Regex messageRegex = new Regex(@"^\* \d+ FETCH \([^\r\n]*BODY\[\] \{\d+\}\r\n(?<msg>.*?)\)\r\n" + this.Tag + " OK"
                , RegexOptions.Multiline | RegexOptions.Singleline);
            Match m = messageRegex.Match(s);
            if (m.Success)
            {
                s = m.Groups["msg"].Value;
                return new MailMessage(s, mailIndex);
            }
            throw new MailClientException();
        }
        /// <summary>
        /// Send search command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public SearchResult ExecuteSearch(String searchText)
        {
            this.ValidateState(ImapConnectionState.Authenticated, true);
            String s = this.Execute(this.Tag + " SEARCH " + searchText);
            return new SearchResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <param name="command"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ImapCommandResult ExecuteStore(Int64 mailIndex, StoreItem command, String flags)
        {
            this.ValidateState(ImapConnectionState.Authenticated, true);
            StringBuilder sb = new StringBuilder(256);
            sb.Append(this.Tag);
            sb.Append(" STORE ");
            sb.Append(mailIndex);
            sb.Append(" ");
            if (command == StoreItem.FlagsReplace)
            {
                sb.Append("FLAGS ");
            }
            else if (command == StoreItem.FlagsReplaceSilent)
            {
                sb.Append("FLAGS.SILENT ");
            }
            else if (command == StoreItem.FlagsAdd)
            {
                sb.Append("+FLAGS ");
            }
            else if (command == StoreItem.FlagsAddSilent)
            {
                sb.Append("+FLAGS.SILENT ");
            }
            else if (command == StoreItem.FlagsRemove)
            {
                sb.Append("-FLAGS ");
            }
            else if (command == StoreItem.FlagsRemoveSilent)
            {
                sb.Append("-FLAGS.SILENT ");
            }
            else
            {
                throw new ArgumentException("command");
            }
            if (String.IsNullOrEmpty(flags))
            {
                throw new ArgumentException("flags");
            }
            sb.Append("(");
            sb.Append(flags);
            sb.Append(")");

            var s = this.Execute(sb.ToString());
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public ImapCommandResult ExecuteAppend(String folderName, String mailData)
        {
            return this.ExecuteAppend(folderName, mailData, "", DateTimeOffset.Now);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="mailData"></param>
        /// <param name="flag"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public ImapCommandResult ExecuteAppend(String folderName, String mailData, String flag, DateTimeOffset datetime)
        {
            String commandText = String.Format(this.Tag + " APPEND \"{0}\" ({1}) \"{2}\" "
                , NamingConversion.EncodeString(folderName), flag, MailParser.DateTimeOffsetString(datetime));
            commandText += "{" + mailData.Length + "}";
            String s = this.Execute(commandText);
            var rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok ||
                rs.Status == ImapCommandResultStatus.None)
            {
                var ss = this.Execute(mailData);
                return new ImapCommandResult(this.Tag, ss);
            }
            return rs;
        }
        /// <summary>
        /// Send rename folder command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteRename(String oldFolderName, String folderName)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String commandText = String.Format(this.Tag + " Rename {0} {1}", NamingConversion.EncodeString(oldFolderName), NamingConversion.EncodeString(folderName));
            String s = this.Execute(commandText);
            var rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok ||
                rs.Status == ImapCommandResultStatus.None)
            {
                return rs;
            }
            return rs;
        }
        /// <summary>
        /// Send rlsub command to IMAP server.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public ListResult ExecuteRlsub(String folderName, Boolean recursive)
        {
            this.ValidateState(ImapConnectionState.Authenticated);

            List<ListLineResult> l = new List<ListLineResult>();
            String name = "";
            Boolean noSelect = false;
            Boolean hasChildren = false;
            String rc = "%";
            if (recursive == true)
            {
                rc = "*";
            }
            String s = this.Execute(String.Format(this.Tag + " RLSUB \"{0}\" \"{1}\"", folderName, rc));
            foreach (Match m in RegexList.GetRlsubFolderResult.Matches(s))
            {
                name = NamingConversion.DecodeString(m.Groups["name"].Value);
                foreach (Capture c in m.Groups["opt"].Captures)
                {
                    if (c.Value.ToString() == "\\Noselect")
                    {
                        noSelect = true;
                    }
                    else if (c.Value.ToString() == "\\HasNoChildren")
                    {
                        hasChildren = false;
                    }
                    else if (c.Value.ToString() == "\\HasChildren")
                    {
                        hasChildren = true;
                    }
                }
                l.Add(new ListLineResult(name, noSelect, hasChildren));
            }
            return new ListResult(l);

        }
        /// <summary>
        /// Send status command to IMAP server.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="message"></param>
        /// <param name="recent"></param>
        /// <param name="uidnext"></param>
        /// <param name="uidvalidity"></param>
        /// <param name="unseen"></param>
        /// <returns></returns>
        public ImapCommandResult ExecuteStatus(String folderName, Boolean message, Boolean recent, Boolean uidnext, Boolean uidvalidity, Boolean unseen)
        {
            String s = null;
            this.ValidateState(ImapConnectionState.Authenticated);
            StringBuilder sb = new StringBuilder(256);
            sb.Append(this.Tag);
            sb.Append(" Status");
            sb.Append(" ");
            sb.Append(folderName);
            if (message || recent || uidnext || uidvalidity || unseen)
            {
                sb.Append(" ");
                sb.Append("(");
                if (message)
                {
                    sb.Append("messages");
                }

                if (recent)
                {
                    sb.Append(" recent");
                }

                if (uidnext)
                {
                    sb.Append(" uidnext");
                }

                if (uidvalidity)
                {
                    sb.Append(" uidvalidity");
                }

                if (unseen)
                {
                    sb.Append(" unseen");
                }

                sb.Append(")");
            }

            s = this.Execute(sb.ToString());
            var rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok ||
                rs.Status == ImapCommandResultStatus.None)
            {
                return rs;
            }
            return rs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteCheck()
        {
            this.ValidateState(ImapConnectionState.Authenticated, true);
            String s = this.Execute(this.Tag + " Check");
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send copy command to IMAP server.
        /// </summary>
        /// <param name="mailindexstart"></param>
        /// <param name="mailindexend"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public ImapCommandResult ExecuteCopy(Int32 mailindexstart, Int32 mailindexend, String folderName)
        {
            String s = null;
            this.ValidateState(ImapConnectionState.Authenticated);
            StringBuilder sb = new StringBuilder(256);
            sb.Append(this.Tag);
            sb.Append(" Copy ");
            if (!Int32.Equals(mailindexstart, 0))
            {
                sb.Append(mailindexstart);
            }
            if (!Int32.Equals(mailindexend, 0) && !Int32.Equals(mailindexstart, 0))
            {
                sb.Append(":");
                sb.Append(mailindexend);
            }
            else if (!Int32.Equals(mailindexend, 0))
            {
                sb.Append(mailindexend);
            }
            sb.Append(" ");
            sb.Append(folderName);

            s = this.Execute(sb.ToString());
            var rs = new ImapCommandResult(this.Tag, s);
            if (rs.Status == ImapCommandResultStatus.Ok ||
                rs.Status == ImapCommandResultStatus.None)
            {
                return rs;
            }
            return rs;

        }
        /// <summary>
        /// Send UID command to IMAP server.
        /// <param name="command"></param>
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteUid(String command)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " UID " + command);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send NAMESPACE command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteNamespace()
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " NAMESPACE");
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImapIdleCommand CreateImapIdleCommand()
        {
            return new ImapIdleCommand(this.Tag, this.ResponseEncoding);
        }
        /// <summary>
        /// Send IDLE command to IMAP server.
        /// You can receive message from server by register event handler to MessageReceived event of ImapIdleCommand object
        /// </summary>
        /// <returns></returns>
        public void ExecuteIdle(ImapIdleCommand command)
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            this.Send(this.Tag + " IDLE");
            var bb = command.GetByteArray();
            command.IAsyncResult = this.Stream.BeginRead(bb, 0, bb.Length, this.ExecuteIdleCallback, command);
            this._State = ImapConnectionState.Idle;
        }
        private void ExecuteIdleCallback(IAsyncResult result)
        {
            DataReceiveContext cx = null;

            try
            {
                cx = (DataReceiveContext)result.AsyncState;
                if (this.Socket == null)
                {
                    throw new SocketClientException("Connection is closed");
                }
                Int32 size = Stream.EndRead(result);
                if (cx.ReadBuffer(size) == true)
                {
                    var bb = cx.GetByteArray();
                    this.Stream.BeginRead(bb, 0, bb.Length, this.GetResponseCallback, cx);
                }
                else
                {
                    cx.Dispose();
                }
            }
            catch (Exception ex)
            {
                cx.Exception = ex;
                this.OnError(ex);
            }
            finally
            {
                if (cx.Exception != null)
                {
                    cx.Dispose();
                }
            }
        }
        /// <summary>
        /// Send done command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteDone(ImapIdleCommand command)
        {
            this.ValidateState(ImapConnectionState.Idle);
            if (command.IAsyncResult != null)
            {
                var x = this.Stream.EndRead(command.IAsyncResult);
            }
            String s = this.Execute("DONE");

            this._State = ImapConnectionState.Authenticated;

            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send GetQuota command to IMAP server.
        /// <param name="resourceName"></param>
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteGetQuota(String resourceName)
        {
            String s = this.Execute(this.Tag + " GetQuota " + resourceName);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send SETQUOTA command to IMAP server.
        /// <param name="resourceName"></param>
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteSetQuota(String resourceName)
        {
            String s = this.Execute(this.Tag + " SETQUOTA " + resourceName);
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send GETQUOTAROOT command to IMAP server.
        /// <param name="folderName"></param>
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteGetQuotaRoot(String folderName)
        {
            String s = this.Execute(this.Tag + " GETQUOTAROOT " + folderName);
            String commandText = String.Format(this.Tag + " GETQUOTAROOT {0}", NamingConversion.EncodeString(folderName));
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// Send noop command to IMAP server.
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteNoop()
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " NOOP");
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImapCommandResult ExecuteExpunge()
        {
            this.ValidateState(ImapConnectionState.Authenticated);
            String s = this.Execute(this.Tag + " EXPUNGE");
            return new ImapCommandResult(this.Tag, s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        public ImapFolder SelectFolder(String folderName)
        {
            var rs = this.ExecuteSelect(folderName);
            return new ImapFolder(rs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        public void UnselectFolder(String folderName)
        {
            this.ExecuteClose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ImapFolder> GetAllFolders()
        {
            return this.GetFolders("", true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public List<ImapFolder> GetFolders(String folderName, Boolean recursive)
        {
            ValidateState(ImapConnectionState.Authenticated);
            List<ImapFolder> l = new List<ImapFolder>();
            var rs = this.ExecuteList(folderName, recursive);
            foreach (var el in rs.Lines)
            {
                l.Add(new ImapFolder(el));
            }
            return l;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public MailMessage GetMessage(Int64 mailIndex)
        {
            return this.ExecuteFetch(mailIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public Boolean DeleteEMail(params Int64[] mailIndex)
        {
            this.ValidateState(ImapConnectionState.Authenticated, true);
            return this.DeleteEMail(this.CurrentFolder.Name, mailIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public Boolean DeleteEMail(String folderName, params Int64[] mailIndex)
        {
            if (this.EnsureOpen() == ImapConnectionState.Disconnected) { return false; }
            if (this.Authenticate() == false) { return false; }

            for (int i = 0; i < mailIndex.Length; i++)
            {
                var rs = this.ExecuteStore(mailIndex[i], StoreItem.FlagsAdd, @"\Deleted");
                if (rs.Status != ImapCommandResultStatus.Ok) { return false; }
            }
            this.ExecuteExpunge();
            this.ExecuteLogout();
            return true;
        }
        /// <summary>
        /// disconnect connection to IMAP server.
        /// </summary>
        public void Close()
        {
            this.Socket.Close();
            this.Socket = null;
            this._State = ImapConnectionState.Disconnected;
        }
        /// <summary>
        /// 
        /// </summary>
        ~ImapClient()
        {
            this.Dispose(false);
        }
    }
}
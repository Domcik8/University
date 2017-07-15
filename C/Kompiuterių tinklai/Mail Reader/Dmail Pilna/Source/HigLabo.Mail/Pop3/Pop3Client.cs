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

namespace HigLabo.Net.Pop3
{
	/// Represent and probide functionality about pop3 command.
	/// <summary>
	/// Represent and probide functionality about pop3 command.
	/// </summary>
	public class Pop3Client : SocketClient, IDisposable
	{
        /// <summary>
        /// 
        /// </summary>
        public static readonly Int32 DefaultPort = 110;
        private Pop3AuthenticateMode _Mode = Pop3AuthenticateMode.Pop;
		private Pop3ConnectionState _State = Pop3ConnectionState.Disconnected;
		/// 認証の方法を取得または設定します。
		/// <summary>
		/// Get or set how authenticate to server.
		/// 認証の方法を取得または設定します。
		/// </summary>
		public Pop3AuthenticateMode AuthenticateMode
		{
			get { return this._Mode; }
			set { this._Mode = value; }
		}
        /// 接続の状態を示す値を取得します。
		/// <summary>
		/// Get connection state.
		/// 接続の状態を示す値を取得します。
		/// </summary>
		public Pop3ConnectionState State
		{
            get
            {
                if (this.Socket == null ||
                    this.Socket.Connected == false)
                {
                    this._State = Pop3ConnectionState.Disconnected;
                }
                return this._State;
            }
		}
		/// サーバーへ接続済みかどうかを示す値を取得します。
		/// <summary>
		/// Get connection is ready.
		/// サーバーへ接続済みかどうかを示す値を取得します。
		/// </summary>
		public Boolean Available
		{
			get { return this._State != Pop3ConnectionState.Disconnected; }
		}
		/// <summary>
		/// 
		/// </summary>
        public Pop3Client(String serverName)
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
		public Pop3Client(String serverName, Int32 port, String userName, String password)
            : base(serverName, port)
		{
			this.UserName = userName;
			this.Password = password;
		}
		/// サーバーへの接続を開きます。
		/// <summary>
		/// Open connection to a server.
		/// サーバーへの接続を開きます。
		/// </summary>
		public Pop3ConnectionState Open()
		{
            if (this.Connect() == true)
            {
                var s = this.GetResponse();
                if (s.StartsWith("+OK") == true)
                {
                    this._State = Pop3ConnectionState.Connected;
                }
                else
                {
                    this.Socket = null;
                    this.Stream = null;
                    this._State = Pop3ConnectionState.Disconnected;
                }
            }
            else
            {
                this._State = Pop3ConnectionState.Disconnected;
            }
            return this._State;
		}
		/// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
		/// <summary>
		/// Ensure connection is opened.
		/// サーバーへの接続が開かれていない場合、サーバーへの接続を開きます。
		/// </summary>
		public Pop3ConnectionState EnsureOpen()
		{
			if (this.Socket != null)
			{ return this._State; }

			return this.Open();
		}
		private void CheckAuthenticate()
		{
			if (this._State == Pop3ConnectionState.Authenticated) { return; }
            throw new MailClientException("You must authenticate to pop3 server before executing this command.");
		}
        private void CheckResponseError(String text)
        {
            if (text.StartsWith("+OK", StringComparison.OrdinalIgnoreCase) == true) { return; }
            throw new MailClientException(text);
        }
		private String GetResponse()
		{
			return this.GetResponse(false);
		}
		private String GetResponse(Boolean isMultiLine)
		{
			MemoryStream ms = new MemoryStream();
			this.GetResponse(ms, isMultiLine);
			return this.ResponseEncoding.GetString(ms.ToArray());
		}
		private void GetResponse(Stream stream)
		{
			this.GetResponse(stream, false);
		}
		private void GetResponse(Stream stream, Boolean isMultiLine)
		{
            Byte[] bb = this.GetResponseBytes(new Pop3DataReceiveContext(this.ResponseEncoding, isMultiLine));
            this.ReadText(stream, isMultiLine, bb);
            this.Commnicating = false;
		}
		private void ReadText(Stream stream, Boolean isMultiLine, Byte[] bytes)
		{
			String CurrentLine = "";
			Byte[] bb = null;
			StringReader sr = new StringReader(this.ResponseEncoding.GetString(bytes));
			while (true)
			{
				CurrentLine = sr.ReadLine();
				if (CurrentLine == null) { CurrentLine = ""; }
				bb = this.ResponseEncoding.GetBytes(CurrentLine + MailParser.NewLine);
				stream.Write(bb, 0, bb.Length);

				//複数行ならば処理続行。単一行ならば処理終了。
				if (isMultiLine == false)
				{ break; }
				//終端文字（ピリオド）で受信処理終了
				if (CurrentLine == ".")
				{ break; }
			}
		}
		/// POP3メールサーバーへログインします。
		/// <summary>
		/// Log in to pop3 server.
		/// POP3メールサーバーへログインします。
		/// </summary>
		/// <returns></returns>
		public Boolean Authenticate()
		{
			if (this._Mode == Pop3AuthenticateMode.Auto)
			{
				if (this.AuthenticateByPop() == true)
				{
					this._Mode = Pop3AuthenticateMode.Pop;
					return true;
				}
				else if (this.AuthenticateByAPop() == true)
				{
					this._Mode = Pop3AuthenticateMode.APop;
					return true;
				}
				return false;
			}
			else
			{
				switch (this._Mode)
				{
					case Pop3AuthenticateMode.Pop: return this.AuthenticateByPop();
					case Pop3AuthenticateMode.APop: return this.AuthenticateByAPop();
				}
			}
			return false;
		}
		/// POP3メールサーバーへPOP認証でログインします。
		/// <summary>
		/// Log in to pop3 server by POP authenticate.
		/// POP3メールサーバーへPOP認証でログインします。
		/// </summary>
		/// <returns></returns>
		public Boolean AuthenticateByPop()
		{
			String s = "";

			if (this.EnsureOpen() == Pop3ConnectionState.Connected)
			{
				//ユーザー名送信
				s = this.Execute("user " + this.UserName, false);
				if (s.StartsWith("+OK") == true)
				{
					//パスワード送信
					s = this.Execute("pass " + this.Password, false);
					if (s.StartsWith("+OK") == true)
					{
						this._State = Pop3ConnectionState.Authenticated;
					}
				}
			}
			return this._State == Pop3ConnectionState.Authenticated;
		}
		/// POP3メールサーバーへAPOP認証でログインします。
		/// <summary>
		/// Log in to pop3 server by A-POP authenticate.
		/// POP3メールサーバーへAPOP認証でログインします。
		/// </summary>
		/// <returns></returns>
		public Boolean AuthenticateByAPop()
		{
			String s = "";
			String TimeStamp = "";
			Int32 StartIndex = 0;
			Int32 EndIndex = 0;

			if (this.EnsureOpen() == Pop3ConnectionState.Connected)
			{
				//ユーザー名送信
				s = this.Execute("user " + this.UserName, false);
				if (s.StartsWith("+OK") == true)
				{
					if (s.IndexOf("<") > -1 &&
						s.IndexOf(">") > -1)
					{
						StartIndex = s.IndexOf("<");
						EndIndex = s.IndexOf(">");
						TimeStamp = s.Substring(StartIndex, EndIndex - StartIndex + 1);
						//パスワード送信
						s = this.Execute("pass " + MailParser.ToMd5DigestString(TimeStamp + this.Password), false);
						if (s.StartsWith("+OK") == true)
						{
							this._State = Pop3ConnectionState.Authenticated;
						}
					}
				}
			}
			return this._State == Pop3ConnectionState.Authenticated;
		}
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// <summary>
		/// Send a command with synchronous and get response data as string text if the command is a type to get response.
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public String Execute(Pop3Command command)
		{
			Boolean IsResponseMultiLine = false;

			if (command is TopCommand ||
				command is RetrCommand ||
				command is ListCommand ||
				command is UidlCommand)
			{
				IsResponseMultiLine = true;
			}
			return this.Execute(command.GetCommandString(), IsResponseMultiLine);
		}
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// <summary>
		/// Send a command with synchronous and get response data as string text if the command is a type to get response.
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// </summary>
		/// <param name="command"></param>
		/// <param name="isMultiLine"></param>
		/// <returns></returns>
		private String Execute(String command, Boolean isMultiLine)
		{
			this.Send(command);
			this.Commnicating = true;
			return this.GetResponse(isMultiLine);
		}
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// <summary>
		/// Send a command with synchronous and get response data as string text if the command is a type to get response.
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		public void Execute(Stream stream, Pop3Command command)
		{
			Boolean IsResponseMultiLine = false;

			if (command is TopCommand ||
				command is RetrCommand ||
				command is ListCommand ||
				command is UidlCommand)
			{
				IsResponseMultiLine = true;
			}
			this.Execute(stream, command.GetCommandString(), IsResponseMultiLine);
		}
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// <summary>
		/// Send a command with synchronous and get response data as string text if the command is a type to get response.
		/// 同期でPOP3メールサーバーへコマンドを送信し、コマンドの種類によってはレスポンスデータを受信して文字列として返します。
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="command"></param>
		/// <param name="isMultiLine"></param>
		/// <returns></returns>
		private void Execute(Stream stream, String command, Boolean isMultiLine)
		{
			this.Send(command);
			this.Commnicating = true;
			this.GetResponse(stream, isMultiLine);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="isMultiLine"></param>
        /// <param name="callbackFunction"></param>
        public void BeginExecute(String command, Boolean isMultiLine, Action<String> callbackFunction)
        {
            this.BeginSend(command, new Pop3DataReceiveContext(this.ResponseEncoding, isMultiLine, callbackFunction), callbackFunction);
        }
		/// 非同期でPOP3メールサーバーへコマンドを送信します。受信したレスポンスの文字列はcallbackFunctionの引数として取得できます。
		/// <summary>
		/// Send a command with asynchronous and get response text by first parameter of callbackFunction.
		/// 非同期でPOP3メールサーバーへコマンドを送信します。受信したレスポンスの文字列はcallbackFunctionの引数として取得できます。
		/// </summary>
		/// <param name="command"></param>
		/// <param name="callbackFunction"></param>
        public void BeginExecute(Pop3Command command, Action<String> callbackFunction)
		{
            Boolean isMultiLine = false;

			if (command is TopCommand ||
				command is RetrCommand ||
				command is ListCommand ||
				command is UidlCommand)
			{
                isMultiLine = true;
			}
            this.BeginExecute(command.GetCommandString(), isMultiLine, callbackFunction);
		}
		/// POP3メールサーバーへListコマンドを送信します。
		/// <summary>
		/// Send list command to pop3 server.
		/// POP3メールサーバーへListコマンドを送信します。
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public List<ListCommandResult> ExecuteList(ListCommand command)
		{
			List<ListCommandResult> l = new List<ListCommandResult>();
			if (command.MailIndex.HasValue == true)
			{
				var rs = this.ExecuteList(command.MailIndex.Value);
				l.Add(rs);
			}
			else
			{
				l = this.ExecuteList();
			}
			return l;
		}
		/// POP3メールサーバーへListコマンドを送信します。
		/// <summary>
		/// Send list command to pop3 server.
		/// POP3メールサーバーへListコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public ListCommandResult ExecuteList(Int64 mailIndex)
		{
			ListCommand cm = new ListCommand(mailIndex);
			ListCommandResult rs = null;
			String s = "";

			this.CheckAuthenticate();
			s = this.Execute(cm);
            this.CheckResponseError(s);
			rs = new ListCommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへListコマンドを送信します。
		/// <summary>
		/// Send list command to pop3 server.
		/// POP3メールサーバーへListコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public List<ListCommandResult> ExecuteList()
		{
			ListCommand cm = new ListCommand();
			List<ListCommandResult> l = new List<ListCommandResult>();
			StringReader sr = null;
			String s = "";
			String line = "";

			this.CheckAuthenticate();
			s = this.Execute(cm);
            this.CheckResponseError(s);
            
            sr = new StringReader(s);
			while (sr.Peek() > -1)
			{
				line = sr.ReadLine();
				if (line == ".")
				{ break; }
				if (line.StartsWith("+OK", StringComparison.OrdinalIgnoreCase) == true)
				{ continue; }

				l.Add(new ListCommandResult(line));
			}
			return l;
		}
		/// POP3メールサーバーへUIDLコマンドを送信します。
		/// <summary>
		/// Send uidl command to pop3 server.
		/// POP3メールサーバーへUIDLコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public UidlCommandResult ExecuteUidl(Int64 mailIndex)
		{
			UidlCommand cm = new UidlCommand(mailIndex);
			UidlCommandResult rs = null;
			String s = "";

			this.CheckAuthenticate();
			s = this.Execute(cm);
            this.CheckResponseError(s);

            rs = new UidlCommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへUIDLコマンドを送信します。
		/// <summary>
		/// Send uidl command to pop3 server.
		/// POP3メールサーバーへUIDLコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public List<UidlCommandResult> ExecuteUidl()
		{
			UidlCommand cm = new UidlCommand();
			List<UidlCommandResult> l = new List<UidlCommandResult>();
			StringReader sr = null;
			String s = "";
			String line = "";

			this.CheckAuthenticate();
			s = this.Execute(cm);
            this.CheckResponseError(s);
            
            sr = new StringReader(s);
			while (sr.Peek() > -1)
			{
				line = sr.ReadLine();
				if (line == ".")
				{ break; }
				if (line.StartsWith("+OK", StringComparison.OrdinalIgnoreCase) == true)
				{ continue; }

				l.Add(new UidlCommandResult(line));
			}
			return l;
		}
		/// POP3メールサーバーへRETRコマンドを送信します。
		/// <summary>
		/// Send retr command to pop3 server.
		/// POP3メールサーバーへRETRコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public MailMessage ExecuteRetr(Int64 mailIndex)
		{
			return this.GetMessage(mailIndex, Int32.MaxValue);
		}
		/// POP3メールサーバーへTOPコマンドを送信します。
		/// <summary>
		/// Send top command to pop3 server.
		/// POP3メールサーバーへTOPコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="lineCount"></param>
		/// <returns></returns>
		public MailMessage ExecuteTop(Int64 mailIndex, Int32 lineCount)
		{
			return this.GetMessage(mailIndex, lineCount);
		}
		/// POP3メールサーバーへDELEコマンドを送信します。
		/// <summary>
		/// Send dele command to pop3 server.
		/// POP3メールサーバーへDELEコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public Pop3CommandResult ExecuteDele(Int64 mailIndex)
		{
			DeleCommand cm = new DeleCommand(mailIndex);
			Pop3CommandResult rs = null;
			String s = "";

			this.CheckAuthenticate();
			s = this.Execute(cm);
            this.CheckResponseError(s);
            rs = new Pop3CommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへSTATコマンドを送信します。
		/// <summary>
		/// Send stat command to pop3 server.
		/// POP3メールサーバーへSTATコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public StatCommandResult ExecuteStat()
		{
			StatCommandResult rs = null;
			String s = "";

			this.CheckAuthenticate();
            s = this.Execute("Stat", false);
            this.CheckResponseError(s);
            rs = new StatCommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへNOOPコマンドを送信します。
		/// <summary>
		/// Send noop command to pop3 server.
		/// POP3メールサーバーへNOOPコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public Pop3CommandResult ExecuteNoop()
		{
			Pop3CommandResult rs = null;
			String s = "";

			this.EnsureOpen();
			s = this.Execute("Noop", false);
			rs = new Pop3CommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへRESETコマンドを送信します。
		/// <summary>
		/// Send reset command to pop3 server.
		/// POP3メールサーバーへRESETコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public Pop3CommandResult ExecuteRset()
		{
			Pop3CommandResult rs = null;
			String s = "";

			this.CheckAuthenticate();
            s = this.Execute("Rset", false);
            this.CheckResponseError(s);
            rs = new Pop3CommandResult(s);
			return rs;
		}
		/// POP3メールサーバーへQUITコマンドを送信します。
		/// <summary>
		/// Send quit command to pop3 server.
		/// POP3メールサーバーへQUITコマンドを送信します。
		/// </summary>
		/// <returns></returns>
		public Pop3CommandResult ExecuteQuit()
		{
			Pop3CommandResult rs = null;
			String s = "";

			this.EnsureOpen();
            s = this.Execute("Quit", false);
            this.CheckResponseError(s);
            rs = new Pop3CommandResult(s);
			return rs;
		}
		/// メールボックスの総メール数を取得します。
		/// <summary>
		/// Get total mail count at mailbox.
		/// メールボックスの総メール数を取得します。
		/// </summary>
		/// <returns></returns>
		public Int64 GetTotalMessageCount()
		{
			StatCommandResult rs = null;
			rs = this.ExecuteStat();
			return rs.TotalMessageCount;
		}
		/// 指定したMailIndexのメールデータを取得します。
		/// <summary>
		/// Get mail data of specified mail index.
		/// 指定したMailIndexのメールデータを取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public MailMessage GetMessage(Int64 mailIndex)
		{
			MailMessage pm = null;

			this.CheckAuthenticate();
            String s = this.Execute(new RetrCommand(mailIndex));
            this.CheckResponseError(s);
            try
			{
				pm = new MailMessage(s, mailIndex);
			}
			catch (Exception ex)
			{
				throw new InvalidMailMessageException(s, ex);
			}
			return pm;
		}
		/// 指定したMailIndexのメールデータを本文の行数を指定して取得します。
		/// <summary>
		/// Get mail data of specified mail index with indicate body line count.
		/// 指定したMailIndexのメールデータを本文の行数を指定して取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="lineCount"></param>
		/// <returns></returns>
		public MailMessage GetMessage(Int64 mailIndex, Int32 lineCount)
		{
			MailMessage pm = null;

            this.CheckAuthenticate();
            String s = this.Execute(new TopCommand(mailIndex, lineCount));
            this.CheckResponseError(s);
            try
			{
				pm = new MailMessage(s, mailIndex);
			}
			catch (Exception ex)
			{
                throw new InvalidMailMessageException(s, ex);
			}
			return pm;
		}
		/// 指定したMailIndexのメールデータの文字列を取得します。
		/// <summary>
		/// Get mail text of specified mail index.
		/// 指定したMailIndexのメールデータの文字列を取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public String GetMessageText(Int64 mailIndex)
		{
			RetrCommand cm = null;

			this.CheckAuthenticate();
			try
			{
				cm = new RetrCommand(mailIndex);
				return this.Execute(cm);
			}
			catch (Exception ex)
			{
                throw new MailClientException(ex);
			}
		}
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定して取得します。
		/// <summary>
		/// Get mail text of specified mail index with indicate body line count.
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定して取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="lineCount"></param>
		/// <returns></returns>
		public String GetMessageText(Int64 mailIndex, Int32 lineCount)
		{
			TopCommand cm = null;

			this.CheckAuthenticate();
			try
			{
				cm = new TopCommand(mailIndex, lineCount);
				return this.Execute(cm);
			}
			catch (Exception ex)
			{
                throw new MailClientException(ex);
			}
		}
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定してストリームに出力します。
		/// <summary>
		/// Get mail text of specified mail index with indicate body line count.
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定してストリームに出力します。
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public void GetMessageText(Stream stream, Int64 mailIndex)
		{
			RetrCommand cm = null;

			this.CheckAuthenticate();
			try
			{
				cm = new RetrCommand(mailIndex);
				this.Execute(stream, cm);
			}
			catch (Exception ex)
			{
                throw new MailClientException(ex);
			}
		}
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定してストリームに出力します。
		/// <summary>
		/// Get mail text of specified mail index with indicate body line count.
		/// 指定したMailIndexのメールデータの文字列を本文の行数を指定してストリームに出力します。
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="mailIndex"></param>
		/// <param name="lineCount"></param>
		/// <returns></returns>
		public void GetMessageText(Stream stream, Int64 mailIndex, Int32 lineCount)
		{
			TopCommand cm = null;

			this.CheckAuthenticate();
			try
			{
				cm = new TopCommand(mailIndex, lineCount);
				this.Execute(stream, cm);
			}
			catch (Exception ex)
			{
                throw new MailClientException(ex);
			}
		}
		/// 非同期で指定したMailIndexのメールデータの文字列を取得します。
		/// <summary>
		/// Get mail text of specified mail index by asynchronous request.
		/// 非同期で指定したMailIndexのメールデータの文字列を取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="callbackFunction"></param>
        public void GetMessageText(Int64 mailIndex, Action<String> callbackFunction)
		{
			RetrCommand cm = null;
			var md = callbackFunction;

			this.CheckAuthenticate();
			cm = new RetrCommand(mailIndex);
			this.BeginExecute(cm, md);
		}
		/// 指定したMailIndexのメールをメールサーバーから削除します。
		/// <summary>
		/// Set delete flag to specify mail index.
		/// To complete delete execution,call quit command after calling dele command.
		/// 指定したMailIndexのメールに削除フラグをたてます。
		/// 実際に削除するにはさらにQUITコマンドで削除処理を完了させる必要があります。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <returns></returns>
		public Boolean DeleteEMail(params Int64[] mailIndex)
		{
			DeleCommand cm = null;
			String s = "";

            if (this.EnsureOpen() == Pop3ConnectionState.Disconnected) { return false; }
            if (this.Authenticate() == false) { return false; }
            for (int i = 0; i < mailIndex.Length; i++)
            {
                cm = new DeleCommand(mailIndex[i]);
                s = this.Execute(cm);
                if (MailParser.IsResponseOk(s) == false) { return false; }
            }
            this.ExecuteQuit();
            return true;
		}
		/// 非同期でPOP3メールサーバーへListコマンドを送信します。
		/// <summary>
		/// Send asynchronous list command to pop3 server.
		/// 非同期でPOP3メールサーバーへListコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="callbackFunction"></param>
		/// <returns></returns>
		public void ExecuteList(Int64 mailIndex, Action<List<ListCommandResult>> callbackFunction)
		{
			ListCommand cm = new ListCommand(mailIndex);

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                List<ListCommandResult> l = new List<ListCommandResult>();
				var rs = new ListCommandResult(responseString);
				l.Add(rs);
				callbackFunction(l);
			};
			this.CheckAuthenticate();
			this.BeginExecute(cm, md);
		}
		/// 非同期でPOP3メールサーバーへListコマンドを送信します。
		/// <summary>
		/// Send asynchronous list command to pop3 server.
		/// 非同期でPOP3メールサーバーへListコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		/// <returns></returns>
		public void ExecuteList(Action<List<ListCommandResult>> callbackFunction)
		{
			ListCommand cm = new ListCommand();

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                List<ListCommandResult> l = new List<ListCommandResult>();
				StringReader sr = null;
				String line = "";

				sr = new StringReader(responseString);
				while (sr.Peek() > -1)
				{
					line = sr.ReadLine();
					if (line == ".")
					{ break; }
					if (line.StartsWith("+OK", StringComparison.OrdinalIgnoreCase) == true)
					{ continue; }

					l.Add(new ListCommandResult(line));
				}
				callbackFunction(l);
			};
			this.CheckAuthenticate();
			this.BeginExecute(cm, md);
		}
		/// 非同期でPOP3メールサーバーへUIDLコマンドを送信します。
		/// <summary>
		/// Send asynchronous uidl command to pop3 server.
		/// 非同期でPOP3メールサーバーへUIDLコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="callbackFunction"></param>
		public void ExecuteUidl(Int64 mailIndex, Action<UidlCommandResult[]> callbackFunction)
		{
			UidlCommand cm = new UidlCommand(mailIndex);

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                UidlCommandResult[] rs = new UidlCommandResult[1];
				rs[0] = new UidlCommandResult(responseString);
				callbackFunction(rs);
			};
			this.CheckAuthenticate();
			this.BeginExecute(cm, md);
		}
		/// 非同期でPOP3メールサーバーへUIDLコマンドを送信します。
		/// <summary>
		/// Send asynchronous uidl command to pop3 server.
		/// 非同期でPOP3メールサーバーへUIDLコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		public void ExecuteUidl(Action<List<UidlCommandResult>> callbackFunction)
		{
			UidlCommand cm = new UidlCommand();

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                List<UidlCommandResult> l = new List<UidlCommandResult>();
				StringReader sr = null;
				String line = "";

				sr = new StringReader(responseString);
				while (sr.Peek() > -1)
				{
					line = sr.ReadLine();
					if (line == ".")
					{ break; }
					if (line.StartsWith("+OK", StringComparison.OrdinalIgnoreCase) == true)
					{ continue; }

					l.Add(new UidlCommandResult(line));
				}
				callbackFunction(l);
			};
			this.CheckAuthenticate();
			this.BeginExecute(cm, md);
		}
		/// 非同期で指定したMailIndexのメールデータを取得します。
		/// <summary>
		/// Get mail data by asynchronous request.
		/// 非同期で指定したMailIndexのメールデータを取得します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="callbackFunction"></param>
		public void GetMessage(Int64 mailIndex, Action<MailMessage> callbackFunction)
		{
			RetrCommand cm = null;

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                callbackFunction(new MailMessage(responseString, mailIndex));
			};
			this.CheckAuthenticate();
			cm = new RetrCommand(mailIndex);
			this.BeginExecute(cm, md);
		}
		/// 非同期でPOP3メールサーバーへRETRコマンドを送信します。
		/// <summary>
		/// Send asynchronous retr command to pop3 server.
		/// 非同期でPOP3メールサーバーへRETRコマンドを送信します。
		/// </summary>
		/// <param name="mailIndex"></param>
		/// <param name="callbackFunction"></param>
		public void ExecuteRetr(Int64 mailIndex, Action<MailMessage> callbackFunction)
		{
			RetrCommand cm = null;

			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                callbackFunction(new MailMessage(responseString, mailIndex));
			};
			this.CheckAuthenticate();
			cm = new RetrCommand(mailIndex);
			this.BeginExecute(cm, md);
		}
		/// 非同期でPOP3メールサーバーへSTATコマンドを送信します。
		/// <summary>
		/// Send asynchronous stat command to pop3 server.
		/// 非同期でPOP3メールサーバーへSTATコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		public void ExecuteStat(Action<StatCommandResult> callbackFunction)
		{
			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                callbackFunction(new StatCommandResult(responseString));
			};
			this.CheckAuthenticate();
			this.BeginExecute("Stat", false, md);
		}
		/// 非同期でPOP3メールサーバーへNOOPコマンドを送信します。
		/// <summary>
		/// Send asynchronous noop command to pop3 server.
		/// 非同期でPOP3メールサーバーへNOOPコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		public void ExecuteNoop(Action<Pop3CommandResult> callbackFunction)
		{
			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                callbackFunction(new Pop3CommandResult(responseString));
			};
			this.EnsureOpen();
            this.BeginExecute("Noop", false, md);
        }
		/// 非同期でPOP3メールサーバーへRESETコマンドを送信します。
		/// <summary>
		/// Send asynchronous reset command to pop3 server.
		/// 非同期でPOP3メールサーバーへRESETコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		public void ExecuteRset(Action<Pop3CommandResult> callbackFunction)
		{
			Action<String> md = responseString =>
			{
				callbackFunction(new Pop3CommandResult(responseString));
			};
			this.CheckAuthenticate();
            this.BeginExecute("Rset", false, md);
		}
		/// 非同期でPOP3メールサーバーへQUITコマンドを送信します。
		/// <summary>
		/// Send asynchronous quit command to pop3 server.
		/// 非同期でPOP3メールサーバーへQUITコマンドを送信します。
		/// </summary>
		/// <param name="callbackFunction"></param>
		public void ExecuteQuit(Action<Pop3CommandResult> callbackFunction)
		{
			Action<String> md = responseString =>
			{
                this.CheckResponseError(responseString);
                Pop3CommandResult rs = new Pop3CommandResult(responseString);
				callbackFunction(rs);
			};
			this.EnsureOpen();
            this.BeginExecute("Quit", false, md);
		}
		/// メールサーバーとの接続を切断します。
		/// <summary>
		/// disconnect connection to pop3 server.
		/// メールサーバーとの接続を切断します。
		/// </summary>
		public void Close()
		{
			this.Socket.Close();
			this._State = Pop3ConnectionState.Disconnected;
		}
		/// <summary>
		/// 
		/// </summary>
		~Pop3Client()
		{
			this.Dispose(false);
		}
	}
}
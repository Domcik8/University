﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using HigLabo.Net.Internal;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class SocketClient
    {
        /// 改行文字列の値です。
        /// <summary>
        /// 改行文字列の値です。
        /// </summary>
        public static readonly String NewLine = "\r\n";
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncSocketCallErrorEventArgs> Error;
        private String _UserName = "";
        private String _Password = "";
        private String _ServerName = "";
        private Int32 _Port = -1;
        private Boolean _Ssl = false;
        private Int32 _ReceiveTimeout = 60 * 1000;
        private Int32 _SendBufferSize = 8192;
        private Int32 _ReceiveBufferSize = 8192;
        private Encoding _ResponseEncoding = Encoding.ASCII;
        private Socket _Socket = null;
        private Stream _Stream = null;
        private AutoResetEvent _SendDone = new AutoResetEvent(false);
        private AutoResetEvent _GetResponseDone = new AutoResetEvent(false);
        private Boolean _Commnicating = false;
        private RemoteCertificateValidationCallback _RemoteCertificateValidationCallback = SocketClient.DefaultRemoteCertificateValidationCallback;
        /// 認証に使用するユーザー名を取得または設定します。
        /// <summary>
        /// Get or set UserName.
        /// 認証に使用するユーザー名を取得または設定します。
        /// </summary>
        public String UserName
        {
            get { return this._UserName; }
            set { this._UserName = value; }
        }
        /// 認証に使用するパスワードを取得または設定します。
        /// <summary>
        /// Get or set password.
        /// 認証に使用するパスワードを取得または設定します。
        /// </summary>
        public String Password
        {
            get { return this._Password; }
            set { this._Password = value; }
        }
        /// POP3メールサーバーのサーバー名を取得または設定します。
        /// <summary>
        /// Get or set server.
        /// POP3メールサーバーのサーバー名を取得または設定します。
        /// </summary>
        public String ServerName
        {
            get { return this._ServerName; }
            set { this._ServerName = value; }
        }
        /// 通信に使用するPort番号を取得または設定します。
        /// <summary>
        /// Get or set port.
        /// 通信に使用するPort番号を取得または設定します。
        /// </summary>
        public Int32 Port
        {
            get { return this._Port; }
            set { this._Port = value; }
        }
        /// 通信をSSLで暗号化するかどうかを示す値を取得または設定します。
        /// <summary>
        /// Get or set use ssl protocol.
        /// 通信をSSLで暗号化するかどうかを示す値を取得または設定します。
        /// </summary>
        public Boolean Ssl
        {
            get { return this._Ssl; }
            set { this._Ssl = value; }
        }
        /// 受信処理のタイムアウトの秒数をミリ秒単位で取得または設定します。
        /// <summary>
        /// Get or set timeout milliseconds.
        /// 受信処理のタイムアウトの秒数をミリ秒単位で取得または設定します。
        /// </summary>
        public Int32 ReceiveTimeout
        {
            get { return this._ReceiveTimeout; }
            set
            {
                this._ReceiveTimeout = value;
                if (this._Socket != null)
                {
                    this._Socket.ReceiveTimeout = this._ReceiveTimeout;
                }
            }
        }
        /// 送信データのバッファサイズを取得または設定します。
        /// <summary>
        /// Get or set buffer size to send.
        /// 送信データのバッファサイズを取得または設定します。
        /// </summary>
        public Int32 SendBufferSize
        {
            get { return this._SendBufferSize; }
            set
            {
                this._SendBufferSize = value;
                if (this._Socket != null)
                {
                    this._Socket.SendBufferSize = this._SendBufferSize;
                }
            }
        }
        /// 受信データのバッファサイズを取得または設定します。
        /// <summary>
        /// Get or set buffer size to receive.
        /// 受信データのバッファサイズを取得または設定します。
        /// </summary>
        public Int32 ReceiveBufferSize
        {
            get { return this._ReceiveBufferSize; }
            set
            {
                this._ReceiveBufferSize = value;
                if (this._Socket != null)
                {
                    this._Socket.ReceiveBufferSize = this._ReceiveBufferSize;
                }
            }
        }
        /// 受信データのエンコーディングを取得または設定します。
        /// <summary>
        /// 受信データのエンコーディングを取得または設定します。
        /// </summary>
        public Encoding ResponseEncoding
        {
            get { return _ResponseEncoding; }
            set { _ResponseEncoding = value; }
        }
        /// <summary>
        /// Get specify value whether communicating to server or not.
        /// Between send command and finish get all response data,this property get true.
        /// </summary>
        public Boolean Commnicating
        {
            get { return this._Commnicating; }
            protected set { _Commnicating = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Socket Socket
        {
            get { return _Socket; }
            set { _Socket = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Stream Stream
        {
            get { return _Stream; }
            set { _Stream = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected AutoResetEvent SendDone
        {
            get { return _SendDone; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected AutoResetEvent GetResponseDone
        {
            get { return _GetResponseDone; }
        }
        /// SSL証明書の検証を行うためのメソッドを取得または設定します。
        /// <summary>
        /// SSL証明書の検証を行うためのメソッドを取得または設定します。
        /// </summary>
        public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
        {
            get { return this._RemoteCertificateValidationCallback; }
            set { this._RemoteCertificateValidationCallback = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="port"></param>
        public SocketClient(String serverName, Int32 port)
        {
            this.ServerName = serverName;
            this.Port = port;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public SocketClient(String serverName, Int32 port, String userName, String password)
        {
            this.ServerName = serverName;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        public SocketClient(Socket socket)
        {
            this.Socket = socket;
        }
        /// 接続先のサーバーと通信を行うためのSocketオブジェクトを取得します。
        /// <summary>
        /// Get Socket object to communicate to server.
        /// 接続先のサーバーと通信を行うためのSocketオブジェクトを取得します。
        /// </summary>
        /// <returns></returns>
        protected Socket GetSocket()
        {
            Socket tc = null;
            IPHostEntry hostEntry = null;

            //サーバー名からIPアドレスのリストを取得します。
            hostEntry = this.GetHostEntry();
            //有効なIPアドレスかどうか判別し、有効なIPアドレスにセットされたソケットを取得します。
            if (hostEntry != null)
            {
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    tc = this.TryGetSocket(address);
                    if (tc != null) { break; }
                }
            }
            return tc;
        }
        private Socket TryGetSocket(IPAddress address)
        {
            IPEndPoint ipe = new IPEndPoint(address, this._Port);
            Socket tc = null;

            try
            {
                tc = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tc.Connect(ipe);
                if (tc.Connected == true)
                {
                    tc.ReceiveTimeout = this.ReceiveTimeout;
                    tc.SendBufferSize = this.SendBufferSize;
                    tc.ReceiveBufferSize = this.ReceiveBufferSize;
                }
            }
            catch
            {
                tc = null;
            }
            return tc;
        }
        private IPHostEntry GetHostEntry()
        {
            try
            {
                return Dns.GetHostEntry(this.ServerName);
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void SetProperty(SocketClient client)
        {
            var cl = client;
            this.ServerName = cl.ServerName;
            this.Port = cl.Port;
            this.UserName = cl.UserName;
            this.Password = cl.Password;
            this.ReceiveBufferSize = cl.ReceiveBufferSize;
            this.ReceiveTimeout = cl.ReceiveTimeout;
            this.RemoteCertificateValidationCallback = cl.RemoteCertificateValidationCallback;
            this.ResponseEncoding = cl.ResponseEncoding;
            this.SendBufferSize = cl.SendBufferSize;
            this.Ssl = cl.Ssl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public Boolean Connect(Socket socket)
        {
            this.Socket = socket;
            return this.Connect();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean Connect()
        {
            if (this.Socket == null)
            {
                this.Socket = this.GetSocket();
            }
            if (this.Socket == null)
            {
                this.Stream = null;
            }
            else
            {
                if (this.Ssl == true)
                {
                    SslStream ssl = new SslStream(new NetworkStream(this.Socket), true, this.RemoteCertificateValidationCallback);
                    ssl.AuthenticateAsClient(this.ServerName);
                    if (ssl.IsAuthenticated == false)
                    {
                        this.Socket = null;
                        this.Stream = null;
                        return false;
                    }
                    this.Stream = ssl;
                }
                else
                {
                    this.Stream = new NetworkStream(this.Socket);
                }
            }
            if (this.Stream == null) { return false; }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void Send(String command)
        {
            this.Send(Encoding.ASCII.GetBytes(command + SocketClient.NewLine));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(Byte[] bytes)
        {
            this.Send(new MemoryStream(bytes));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Send(Stream stream)
        {
            DataSendContext cx = null;

            if (this.Socket == null)
            {
                throw new SocketClientException("Connection is closed");
            }
            try
            {
                cx = new DataSendContext(stream, Encoding.ASCII);
                cx.FillBuffer();
                this.Stream.BeginWrite(cx.GetByteArray(), 0, cx.SendBufferSize, this.SendCallback, cx);
                this.SendDone.WaitOne();
            }
            catch (Exception ex)
            {
                throw new SocketClientException(ex);
            }
            finally
            {
                if (cx != null)
                {
                    cx.Dispose();
                }
            }
            //Throw exception that occor other thread.
            if (cx.Exception != null)
            {
                throw cx.Exception;
            }
        }
        private void SendCallback(IAsyncResult result)
        {
            DataSendContext cx = null;
            try
            {
                cx = (DataSendContext)result.AsyncState;
                Stream.EndWrite(result);
                if (cx.DataRemained == true)
                {
                    cx.FillBuffer();
                    this.Stream.BeginWrite(cx.GetByteArray(), 0, cx.SendBufferSize, this.SendCallback, cx);
                }
                else
                {
                    this.SendDone.Set();
                }
            }
            catch (Exception ex)
            {
                cx.Exception = ex;
            }
            if (cx.Exception != null)
            {
                try
                {
                    //タイミングの問題でDisposeされている場合がある
                    this.SendDone.Set();
                }
                catch (ObjectDisposedException) { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String GetResponseText()
        {
            var bb = this.GetResponseBytes();
            return this.ResponseEncoding.GetString(bb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Byte[] GetResponseBytes()
        {
            MemoryStream ms = new MemoryStream();
            this.GetResponseStream(ms);
            return ms.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void GetResponseStream(Stream stream)
        {
            this.GetResponseStream(new DataReceiveContext(stream, this.ResponseEncoding));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void GetResponseStream(DataReceiveContext context)
        {
            if (this.Socket == null)
            {
                throw new SocketClientException("Connection is closed");
            }
            using (var cx = context)
            {
                var bb = cx.GetByteArray();
                this.Stream.BeginRead(bb, 0, bb.Length, this.GetResponseCallback, cx);
                var bl = this.GetResponseDone.WaitOne(this.ReceiveTimeout);
                if (cx.Exception != null)
                {
                    throw cx.Exception;
                }
                if (cx.Timeout == true || bl == false)
                {
                    throw new SocketClientException("Response timeout");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected Byte[] GetResponseBytes(DataReceiveContext context)
        {
            this.GetResponseStream(context);
            return context.GetData();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        protected void GetResponseCallback(IAsyncResult result)
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
                TimeSpan ts = DateTime.Now - cx.StartTime;

                if (ts.TotalMilliseconds > this.ReceiveTimeout)
                {
                    cx.Timeout = true;
                    this.GetResponseDone.Set();
                }
                if (cx.ReadBuffer(size) == true)
                {
                    var bb = cx.GetByteArray();
                    this.Stream.BeginRead(bb, 0, bb.Length, this.GetResponseCallback, cx);
                }
                else
                {
                    this.GetResponseDone.Set();
                }
            }
            catch (Exception ex)
            {
                cx.Exception = ex;
            }
            if (cx.Exception != null)
            {
                try
                {
                    this.GetResponseDone.Set();
                }
                catch (ObjectDisposedException) { }
            }
        }
        /// 非同期でPOP3メールサーバーへコマンドを送信します。受信したレスポンスの文字列はcallbackFunctionの引数として取得できます。
        /// <summary>
        /// Send a command with asynchronous and get response text by first parameter of callbackFunction.
        /// 非同期でPOP3メールサーバーへコマンドを送信します。受信したレスポンスの文字列はcallbackFunctionの引数として取得できます。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <param name="callbackFunction"></param>
        public void BeginSend(String command, DataReceiveContext context, Action<String> callbackFunction)
        {
            Boolean IsException = false;
            var cx = context;

            try
            {
                this.Send(command);
                var bb = cx.GetByteArray();
                this.Stream.BeginRead(bb, 0, bb.Length, this.BeginSendCallBack, cx);
            }
            catch
            {
                IsException = true;
                throw;
            }
            finally
            {
                if (IsException == true && cx != null)
                {
                    cx.Dispose();
                }
            }
        }
        /// 非同期でPOP3メールサーバーからのデータを受信します。
        /// 受信データがまだある場合、再度BeginExecuteメソッドを呼び出し残りのデータを取得します。
        /// <summary>
        /// Send a command with asynchronous and get response text by first parameter of callbackFunction.
        /// If there is more data to receive,continously call BeginExecuteCallback method and get response data.
        /// 非同期でPOP3メールサーバーからのデータを受信します。
        /// 受信データがまだある場合、再度BeginExecuteメソッドを呼び出し残りのデータを取得します。
        /// </summary>
        /// <param name="result"></param>
        private void BeginSendCallBack(IAsyncResult result)
        {
            DataReceiveContext cx = (DataReceiveContext)result.AsyncState;
            Boolean IsException = false;

            try
            {
                Int32 size = this.Stream.EndRead(result);
                if (cx.ReadBuffer(size) == true)
                {
                    //まだデータが受信中の場合、再度レスポンスデータを受信します。
                    var bb = cx.GetByteArray();
                    this.Stream.BeginRead(bb, 0, bb.Length, this.BeginSendCallBack, cx);
                }
                else
                {
                    cx.OnEndGetResponse();
                    cx.Dispose();
                }
            }
            catch (Exception ex)
            {
                IsException = true;
                this.OnError(ex);
            }
            finally
            {
                if (IsException == true && cx != null)
                {
                    cx.Dispose();
                }
            }
        }
        private static Boolean DefaultRemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        /// <summary>
        /// Get string about mail account information.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(256);

            sb.AppendFormat("ServerName:{0}", this.ServerName);
            sb.AppendLine();
            sb.AppendFormat("Port:{0}", this.Port);
            sb.AppendLine();
            sb.AppendFormat("UserName:{0}", this.UserName);
            sb.AppendLine();
            sb.AppendFormat("SSL:{0}", this.Ssl);

            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        protected void OnError(Exception exception)
        {
            var eh = this.Error;
            if (eh != null)
            {
                eh(this, new AsyncSocketCallErrorEventArgs(exception));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected void DisposeSocket()
        {
            if (this.Socket != null)
            {
                ((IDisposable)this.Socket).Dispose();
                this.Socket = null;
                this.Stream = null;
            }
        }
        /// 終了処理を実行し、システムリソースを解放します。
        /// <summary>
        /// dipose and release system resoures.
        /// 終了処理を実行し、システムリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.DisposeSocket();
                ((IDisposable)this.SendDone).Dispose();
                ((IDisposable)this.GetResponseDone).Dispose();
            }
        }
    }
}

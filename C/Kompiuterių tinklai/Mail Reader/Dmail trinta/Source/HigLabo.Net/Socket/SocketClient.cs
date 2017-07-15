using System;
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
    public class SocketClient
    {
        public static readonly String NewLine = "\r\n"; //+
        public event EventHandler<AsyncSocketCallErrorEventArgs> Error;
        private String _UserName = "";  //+
        private String _Password = "";  //+
        private String _ServerName = "";  //+
        private Int32 _Port = -1;  //+
        private Boolean _Ssl = false; //+
        private Int32 _ReceiveTimeout = 60 * 1000; //+
        private Int32 _SendBufferSize = 8192; //+
        private Int32 _ReceiveBufferSize = 8192; //+
        private Encoding _ResponseEncoding = Encoding.ASCII; //+
        private Socket _Socket = null; //+
        private Stream _Stream = null; //+
        private AutoResetEvent _SendDone = new AutoResetEvent(false); //+
        private AutoResetEvent _GetResponseDone = new AutoResetEvent(false);
        private Boolean _Commnicating = false; //+
        private RemoteCertificateValidationCallback _RemoteCertificateValidationCallback = SocketClient.DefaultRemoteCertificateValidationCallback;
        public String UserName //+
        {
            get { return this._UserName; }
            set { this._UserName = value; }
        }
        public String Password //+
        {
            get { return this._Password; }
            set { this._Password = value; }
        }
        public String ServerName  //+
        {
            get { return this._ServerName; }
            set { this._ServerName = value; }
        }
        public Int32 Port  //+
        {
            get { return this._Port; }
            set { this._Port = value; }
        }
        public Boolean Ssl  //+
        {
            get { return this._Ssl; }
            set { this._Ssl = value; }
        }
        public Int32 ReceiveTimeout //+
        {
            get { return this._ReceiveTimeout; } //+
            set
            {
                this._ReceiveTimeout = value; //+
                if (this._Socket != null) //+
                {
                    this._Socket.ReceiveTimeout = this._ReceiveTimeout; //+
                }
            }
        }
        public Int32 SendBufferSize  //+
        {
            get { return this._SendBufferSize; } //+
            set
            {
                this._SendBufferSize = value; //+
                if (this._Socket != null) //+
                {
                    this._Socket.SendBufferSize = this._SendBufferSize; //+
                }
            }
        }
        public Int32 ReceiveBufferSize //+
        {
            get { return this._ReceiveBufferSize; } //+
            set
            {
                this._ReceiveBufferSize = value; //+
                if (this._Socket != null) //+
                {
                    this._Socket.ReceiveBufferSize = this._ReceiveBufferSize; //+
                }
            }
        }
        public Encoding ResponseEncoding //+
        {
            get { return _ResponseEncoding; }
            set { _ResponseEncoding = value; }
        }
        public Boolean Commnicating //+
        {
            get { return this._Commnicating; }
            protected set { _Commnicating = value; }
        }
        protected Socket Socket //+
        {
            get { return _Socket; }
            set { _Socket = value; }
        }
        protected Stream Stream //+
        {
            get { return _Stream; }
            set { _Stream = value; }
        }
        protected AutoResetEvent SendDone //+
        {
            get { return _SendDone; }
        }
        protected AutoResetEvent GetResponseDone
        {
            get { return _GetResponseDone; }
        }
        public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
        {
            get { return this._RemoteCertificateValidationCallback; }
            set { this._RemoteCertificateValidationCallback = value; }
        }
        public SocketClient(String serverName, Int32 port)
        {
            this.ServerName = serverName;  //+
            this.Port = port;  //+
        }
        public SocketClient(String serverName, Int32 port, String userName, String password)
        {
            this.ServerName = serverName;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
        }
        public SocketClient(Socket socket)
        {
            this.Socket = socket;
        }
        protected Socket GetSocket() //Grazina socketa sujungtu su musu host adresu arba null
        {
            Socket tc = null; //+
            IPHostEntry hostEntry = null; //+

            hostEntry = this.GetHostEntry(); //+
            if (hostEntry != null) //+
            {
                foreach (IPAddress address in hostEntry.AddressList)  //+
                {
                    tc = this.TryGetSocket(address); //Grazina socket'a prijungta prie adreso arba null reiksme
                    if (tc != null) { break; } //Mums tinka bet koks is musu host adresu listo
                }
            }
            return tc;
        }
        private Socket TryGetSocket(IPAddress address)  //Grazina socket'a prijungta prie adreso arba null reiksme
        {
            IPEndPoint ipe = new IPEndPoint(address, this._Port);  //+
            Socket tc = null;  //+

            try
            {
                tc = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp); //Grazina atidaryta socketa
                tc.Connect(ipe); //Prisijungia socket prie hosto (mano address + port)
                if (tc.Connected == true) //Jei pavyko
                {
                    tc.ReceiveTimeout = this.ReceiveTimeout; //+
                    tc.SendBufferSize = this.SendBufferSize; //+
                    tc.ReceiveBufferSize = this.ReceiveBufferSize; //+
                }
            }
            catch
            {
                tc = null;
            }
            return tc;
        }
        private IPHostEntry GetHostEntry()  //+
        {
            try
            {
                return Dns.GetHostEntry(this.ServerName); //Gauna Hosto entry
            }
            catch { }  //+
            return null;  //+
        }
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
        public Boolean Connect(Socket socket)
        {
            this.Socket = socket;
            return this.Connect();
        }
        public Boolean Connect()  //Grazina true jei turim socketa sujungta su hosty ir ne null Stream
        {
            if (this.Socket == null)  //+
            {
                this.Socket = this.GetSocket(); //Grazina socketa sujungtu su musu host adresu arba null
            }
            if (this.Socket == null)
            {
                this.Stream = null;
            }
            else
            {
                if (this.Ssl == true)
                {
                    /*SslStream ssl = new SslStream(new NetworkStream(this.Socket), true, this.RemoteCertificateValidationCallback);
                    ssl.AuthenticateAsClient(this.ServerName);
                    if (ssl.IsAuthenticated == false)
                    {
                        this.Socket = null;
                        this.Stream = null;
                        return false;
                    }
                    this.Stream = ssl;*/
                }
                else
                {
                    this.Stream = new NetworkStream(this.Socket);
                }
            }
            if (this.Stream == null) { return false; }
            return true;
        }
        public void Send(String command) //Nusiuncia ASCII uzkoduoda komanda su new line simoliu
        {
            this.Send(Encoding.ASCII.GetBytes(command + SocketClient.NewLine));  //+
        }
        public void Send(Byte[] bytes) //Nusiunciamas duomenu buferis i MemoryStream
        {
            this.Send(new MemoryStream(bytes)); //Nusiunciamas duomenu buferis duotu streamu, o sito stream backign store tai memory
        }
        public void Send(Stream stream) //Nusiunciamas duomenu buferis duotu streamu
        {
            DataSendContext cx = null; //Duomenys apie siuntimui naudojama streama, bufferio didi ir informacija apie duomenys

            if (this.Socket == null)
            {
                throw new SocketClientException("Connection is closed"); //Jei socketas uzdaritas
            }
            try
            {
                cx = new DataSendContext(stream, Encoding.ASCII); //Uzpildomas DataTransferContext bufferis, nustatomas encoding, streamas ir bufferio didis
                cx.FillBuffer();  //Nuskaitom bb.lenght baitu is streamo arba tiek kiek liko
                this.Stream.BeginWrite(cx.GetByteArray(), 0, cx.SendBufferSize, this.SendCallback, cx);  //Siuncia bufferi streamu
                this.SendDone.WaitOne(); //Laukia kol bus nusiustas visas bufferis
            }
            catch (Exception ex)
            {
                throw new SocketClientException(ex); //Error siunciant bufferi
            }
            finally
            {
                if (cx != null)
                {
                    cx.Dispose(); //Istrinam Datasendcontext, nes daug vietos uzeme su buffereis
                }
            }
            //Throw exception that occor other thread.
            if (cx.Exception != null)
            {
                throw cx.Exception; //Jei ivyko kitas exception cx viduje
            }
        }
        private void SendCallback(IAsyncResult result) //Nustato SendDone event kai nusiunciamas visas bufferis
        {
            DataSendContext cx = null; //+
            try
            {
                cx = (DataSendContext)result.AsyncState; //+
                Stream.EndWrite(result); //+
                if (cx.DataRemained == true) //+
                {
                    cx.FillBuffer(); //Nuskaitom bb.lenght baitu is streamo arba tiek kiek liko
                    this.Stream.BeginWrite(cx.GetByteArray(), 0, cx.SendBufferSize, this.SendCallback, cx); //+
                }
                else
                {
                    this.SendDone.Set(); //+
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
                    this.SendDone.Set();
                }
                catch (ObjectDisposedException) { }
            }
        }
        public String GetResponseText()
        {
            var bb = this.GetResponseBytes();
            return this.ResponseEncoding.GetString(bb);
        }
        public virtual Byte[] GetResponseBytes()
        {
            MemoryStream ms = new MemoryStream();
            this.GetResponseStream(ms);
            return ms.ToArray();
        }
        public void GetResponseStream(Stream stream)
        {
            this.GetResponseStream(new DataReceiveContext(stream, this.ResponseEncoding));
        }
        protected void GetResponseStream(DataReceiveContext context) //Is stream laukia atsakymo
        {
            if (this.Socket == null)
            {
                throw new SocketClientException("Connection is closed");
            }
            using (var cx = context)
            {
                var bb = cx.GetByteArray(); //Grazina buferi
                this.Stream.BeginRead(bb, 0, bb.Length, this.GetResponseCallback, cx); //Is stream i buferi nuskaito
                var bl = this.GetResponseDone.WaitOne(this.ReceiveTimeout); //Laukia nuskaitymo galo
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
        protected Byte[] GetResponseBytes(DataReceiveContext context) //Laukia atsakymo is context streamo
        {
            this.GetResponseStream(context); //Is stream laukia atsakymo
            return context.GetData(); //Is stream nuskaito duomenys i MemoryStream ir i Byte[] ideda
        }
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
        /// Send a command with asynchronous and get response text by first parameter of callbackFunction.
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
        /// Send a command with asynchronous and get response text by first parameter of callbackFunction.
        /// If there is more data to receive,continously call BeginExecuteCallback method and get response data.
        private void BeginSendCallBack(IAsyncResult result)
        {
            DataReceiveContext cx = (DataReceiveContext)result.AsyncState;
            Boolean IsException = false;

            try
            {
                Int32 size = this.Stream.EndRead(result);
                if (cx.ReadBuffer(size) == true)
                {
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
        /// Get string about mail account information.
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
        protected void OnError(Exception exception)
        {
            var eh = this.Error;
            if (eh != null)
            {
                eh(this, new AsyncSocketCallErrorEventArgs(exception));
            }
        }
        protected void DisposeSocket()
        {
            if (this.Socket != null)
            {
                ((IDisposable)this.Socket).Dispose();
                this.Socket = null;
                this.Stream = null;
            }
        }
        /// dipose and release system resoures.
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }
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

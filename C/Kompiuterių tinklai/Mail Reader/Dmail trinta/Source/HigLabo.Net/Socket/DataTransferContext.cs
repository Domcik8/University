using System;
using System.Collections.Generic;
using System.Text;

namespace HigLabo.Net.Internal
{
    public class DataTransferContext : IDisposable //Informacija apie duomenys (kada sukurem si objekta, koks encription)
    {
        private static BufferManager _BufferManager = null; //+
        private DateTime _StartTime = DateTime.Now; //+
        private Byte[] _Buffer; //+
        private List<Byte> _Data = new List<Byte>(); //+
        private Exception _Exception = null; //+
        private Boolean _Timeout = false; //+
        private Encoding _Encoding = Encoding.ASCII; //+
        private Boolean _IsDisposed = false; //+
		public static BufferManager BufferManager //Laiko bufferi duomenyms
		{
			get
			{
                if (_BufferManager == null) //+
				{
                    _BufferManager = new BufferManager(256, 8192); //Sukuriam bufferi duomenyms
				}
                return _BufferManager; //+
			}
            set { _BufferManager = value; } //+
		}
        internal protected DateTime StartTime
		{
			get { return _StartTime; }
			set { _StartTime = value; }
		}
		protected Encoding Encoding
		{
			get { return _Encoding; }
			set { _Encoding = value; }
		}
        protected Byte[] Buffer
        {
            get { return _Buffer; }
        }
        public Exception Exception //+
        {
            get { return _Exception; }
            set { _Exception = value; }
        }
		public Boolean Timeout
		{
			get { return _Timeout; }
			set { _Timeout = value; }
		}
        internal DataTransferContext(Encoding encoding) //Is buferio isimam _DequeueRetryCount baitu ir nustatomas encoding
		{
			_Encoding = encoding; //+
            _Buffer = DataTransferContext.BufferManager.CheckOut(); //Is buferio isimam _DequeueRetryCount baitu
		}
        public Byte[] GetByteArray() //Grazina buferi
        {
            return this._Buffer;
        }
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}
		protected void Dispose(Boolean disposing)
		{
			if (disposing)
			{
				if (this._IsDisposed == false &&
					this._Buffer != null)
				{
					DataTransferContext.BufferManager.CheckIn(this._Buffer);
					this._IsDisposed = true;
				}
			}
		}
		~DataTransferContext()
		{
			this.Dispose(false);
		}
    }
}

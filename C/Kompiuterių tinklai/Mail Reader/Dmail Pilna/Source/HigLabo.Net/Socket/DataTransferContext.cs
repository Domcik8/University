using System;
using System.Collections.Generic;
using System.Text;

namespace HigLabo.Net.Internal
{
	/// <summary>
    /// Represent context of request and response process and provide data about context.
    /// </summary>
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
		/// <summary>
		/// 
		/// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        internal protected DateTime StartTime
		{
			get { return _StartTime; }
			set { _StartTime = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		protected Encoding Encoding
		{
			get { return _Encoding; }
			set { _Encoding = value; }
		}
        /// <summary>
        /// 
        /// </summary>
        protected Byte[] Buffer
        {
            get { return _Buffer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception //+
        {
            get { return _Exception; }
            set { _Exception = value; }
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Byte[] GetByteArray() //Grazina buferi
        {
            return this._Buffer;
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
		/// <summary>
		/// 
		/// </summary>
		~DataTransferContext()
		{
			this.Dispose(false);
		}
    }
}

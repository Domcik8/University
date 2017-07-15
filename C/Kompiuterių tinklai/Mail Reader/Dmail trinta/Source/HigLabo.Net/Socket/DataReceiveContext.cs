using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HigLabo.Net.Internal
{
    public class DataReceiveContext : DataTransferContext
    {
        private Action<String> _EndGetResponse = null; //+
        private Stream _Stream = null; //+
        protected Stream Stream //+
        {
            get { return _Stream; }
        }
        protected Action<String> EndGetResponse //+
        {
            get { return _EndGetResponse; }
            set { _EndGetResponse = value; }
        }
        public DataReceiveContext(Encoding encoding) //Nustatomas Stream ir (datatransfercontext) Is buferio isimam _DequeueRetryCount baitu ir nustatomas encoding
            : base(encoding)  //Is buferio isimam _DequeueRetryCount baitu ir nustatomas encoding
        {
            _Stream = new MemoryStream(); //+
        }
        public DataReceiveContext(Stream stream, Encoding encoding)
            : base(encoding)
        {
            _Stream = stream;
        }
        public virtual Boolean ReadBuffer(Int32 size)
        {
            Byte[] bb = this.GetByteArray();

            for (int i = 0; i < size; i++)
            {
                this.Stream.WriteByte(bb[i]);
                bb[i] = 0;
            }
            if (size < bb.Length)
            {
                return false;   
            }
            return true;
        }
        internal protected Byte[] GetData() //Is stream nuskaito duomenys i MemoryStream ir i Byte[] ideda
        {
            _Stream.Position = 0; //+
            return _Stream.ToByteArray(); //Is stream nuskaito duomenys i MemoryStream ir i Byte[] ideda
        }
        protected Byte[] GetLastByte(Int32 size)
        {
            _Stream.Position = _Stream.Length - size;
            return _Stream.ToByteArray();
        }
        internal protected void OnEndGetResponse()
        {
            var eh = _EndGetResponse;
            if (eh != null)
            {
                eh(this.Encoding.GetString(this.GetData()));
            }
        }
    }
}

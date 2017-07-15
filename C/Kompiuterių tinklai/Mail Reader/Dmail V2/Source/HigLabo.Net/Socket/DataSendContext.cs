using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HigLabo.Net.Internal
{
	/// <summary>
    /// Represent context of request and response process and provide data about context.
    /// </summary>
    internal class DataSendContext : DataTransferContext
    {
        private Stream _Stream = null;
        private Int32 _SendBufferSize = 0;
        internal Int32 SendBufferSize
        {
            get { return _SendBufferSize; }
        }
        internal Boolean DataRemained
        {
            get { return _Stream.Position < _Stream.Length; }
        }
        internal DataSendContext(Stream stream, Encoding encoding) :
            base(encoding)
        {
            _Stream = stream;
            this._SendBufferSize = (Int32)_Stream.Length;
        }
        internal void FillBuffer()
        {
            Byte[] bb = this.GetByteArray();

            if (_Stream.Position + bb.Length < _Stream.Length)
            {
                _Stream.Read(bb, 0, bb.Length);
                _SendBufferSize = bb.Length;
            }
            else
            {
                Int32 count = (Int32)(_Stream.Length - _Stream.Position);
                _Stream.Read(bb, 0, count);
                _SendBufferSize = count;
            }
        }
    }
}

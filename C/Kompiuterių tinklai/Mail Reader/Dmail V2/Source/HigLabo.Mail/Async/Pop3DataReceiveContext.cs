using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Internal
{
    /// <summary>
    /// Represent context of request and response process and provide data about context.
    /// </summary>
    internal class Pop3DataReceiveContext : DataReceiveContext
    {
		private Boolean _IsMultiLine = false;
        internal Pop3DataReceiveContext(Encoding encoding, Boolean isMultiLine) :
			base(encoding)
		{
			_IsMultiLine = isMultiLine;
		}
        internal Pop3DataReceiveContext(Encoding encoding, Boolean isMultiLine, Action<String> callbackFunction) :
			base(encoding)
		{
			_IsMultiLine = isMultiLine;
			this.EndGetResponse = callbackFunction;
		}
		/// <summary>
		/// Read buffer data to Data property and initialize buffer.
		/// If response has next data,return true.
		/// </summary>
		/// <param name="size"></param>
		/// <returns>If response has next data,return true</returns>
		public override Boolean ReadBuffer(Int32 size)
		{
			Byte[] bb = this.Buffer;
            Byte[] lastBytes = null;

			for (int i = 0; i < size; i++)
			{
				this.Stream.WriteByte(bb[i]);
				bb[i] = 0;
			}
			if (this._IsMultiLine == false)
			{
				if (size < bb.Length)
				{
					return false;
				}
                if (this.Stream.Length > 1)
                {
                    lastBytes = this.GetLastByte(2);
                    //\r\n
                    if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.LineFeed.GetNumber())
                    { return false; }
                }
			}
			else 
			{
                if (this.Stream.Length > 4)
                {
                    lastBytes = this.GetLastByte(5);
                    //\r\n.\r\n
                    if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.LineFeed.GetNumber() &&
                        lastBytes[2] == AsciiCharCode.Period.GetNumber() &&
                        lastBytes[3] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[4] == AsciiCharCode.LineFeed.GetNumber())
                    { return false; }
                }
			}
			return true;
		}
	}
}

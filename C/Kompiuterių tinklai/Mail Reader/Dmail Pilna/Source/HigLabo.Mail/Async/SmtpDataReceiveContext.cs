using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Internal
{
	/// <summary>
    /// Represent context of request and response process and provide data about context.
    /// </summary>
    internal class SmtpDataReceiveContext : DataReceiveContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
		internal SmtpDataReceiveContext(Encoding encoding):
			base(encoding)
		{
		}
		/// <summary>
		/// Read buffer data to Data property and initialize buffer.
		/// If response has next data,return true.
		/// </summary>
		/// <param name="size"></param>
		/// <returns>If response has next data,return true</returns>
		public override Boolean ReadBuffer(Int32 size)
		{
			Int64? NewLineStartIndex = null;
			Boolean isLastLine = false;
            Byte[] bb = this.Buffer;
            Byte[] lastBytes = null;

			for (int i = 0; i < size; i++)
			{
				this.Stream.WriteByte(bb[i]);
				bb[i] = 0;
				//Check the response stream has next line or not.
				if (this.Stream.Length == 4)
				{
                    lastBytes = this.GetLastByte(1);
					if (lastBytes[0] == AsciiCharCode.Space.GetNumber())
					{
						isLastLine = true;
					}
				}
				else if (this.Stream.Length > 4)
				{
                    lastBytes = this.GetLastByte(2);
                    if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.LineFeed.GetNumber())
                    {
                        isLastLine = true;
                    }
					NewLineStartIndex = this.Stream.Length + 1;
				}
				if (NewLineStartIndex.HasValue == true &&
					NewLineStartIndex.Value + 3 == this.Stream.Length)
				{
                    lastBytes = this.GetLastByte(1);
                    //Space is indicate last line.
					if (lastBytes[0] == AsciiCharCode.Space.GetNumber())
					{
						isLastLine = true;
					}
				}
			}

			if (isLastLine == true)
			{
				if (size < bb.Length)
				{
					return false;
				}
				//\r\n
                if (this.Stream.Length > 1)
                {
                    lastBytes = this.GetLastByte(2);
                    if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.LineFeed.GetNumber())
                    {
                        return false;
                    }
                }
			}
			return true;
		}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;
using HigLabo.Net.Imap;

namespace HigLabo.Net.Internal
{
    /// <summary>
    /// Represent context of request and response process and provide data about context.
    /// </summary>
    public class ImapDataReceiveContext : DataReceiveContext
    {
        private Byte[] _TagBytes = null;
        private Boolean _IsLastline = false;
        private Action<String> _EndGetResponseCallback = null;
        internal ImapDataReceiveContext(String tag, Encoding encoding) : //Nustatomas tagas, encoding ir giti duomenys
            base(encoding)  //(DataReceiveContext) Nustatomas Stream ir (datatransfercontext) Is buferio isimam _DequeueRetryCount baitu ir nustatomas encoding
        {
            _TagBytes = this.Encoding.GetBytes(tag); //++
        }
        internal ImapDataReceiveContext(String tag, Encoding encoding, Action<String> callbackFunction) :
            base(encoding)
        {
            _TagBytes = this.Encoding.GetBytes(tag);
            _EndGetResponseCallback = callbackFunction;
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

                if (i < _TagBytes.Length - 1) { continue; }
                if (i == _TagBytes.Length - 1)
                {
                    //tag1....で始まってないかチェック
                    this.CheckIsLastline(true);
                }
                else if (i == _TagBytes.Length)
                {
                    //?tag1という場合はないのでスキップ
                    continue;
                }
                else
                {
                    //\r\ntag1かどうかチェック
                    this.CheckIsLastline(i == _TagBytes.Length - 1);
                }
            }

            if (_IsLastline == true)
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
                if (this.Stream.Length > 2)
                {
                    lastBytes = this.GetLastByte(3);
                    //\r\n*
                    if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.LineFeed.GetNumber() &&
                        lastBytes[1] == AsciiCharCode.Asterisk.GetNumber())
                    {
                        _IsLastline = true;
                    }
                }
            }
            return true;
        }
        private void CheckIsLastline(Boolean isFirstCheck)
        {
            Byte[] lastBytes = null;

            if (isFirstCheck == true)
            {
                //tag1
                lastBytes = this.GetLastByte(_TagBytes.Length);
            }
            else
            {
                //\r\ntag1
                lastBytes = this.GetLastByte(_TagBytes.Length + 2);
                //改行コードの次の場合のみチェック
                if (lastBytes[0] == AsciiCharCode.CarriageReturn.GetNumber() &&
                    lastBytes[1] == AsciiCharCode.LineFeed.GetNumber())
                {
                    lastBytes = this.GetLastByte(_TagBytes.Length);
                }
                else { return; }
            }
            for (int tagIndex = 0; tagIndex < _TagBytes.Length; tagIndex++)
            {
                if (_TagBytes[tagIndex] != lastBytes[tagIndex])
                {
                    return;
                }
            }
            this._IsLastline = true;
        }
    }
}

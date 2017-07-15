using System;
using System.Collections.Generic;
using System.Text;
using HigLabo.Net.Mail;
using HigLabo.Net.Imap;

namespace HigLabo.Net.Internal
{
    public class ImapDataReceiveContext : DataReceiveContext
    {
        private Byte[] _TagBytes = null;
        private Boolean _IsLastline = false;
        private Action<String> _EndGetResponseCallback = null;
        internal ImapDataReceiveContext(String tag, Encoding encoding) : //Nustatomas tagas, encoding ir kiti duomenys
            base(encoding)  //(DataReceiveContext) Nustatomas Stream ir (datatransfercontext) Is buferio isimam _DequeueRetryCount baitu ir nustatomas encoding
        {
            _TagBytes = this.Encoding.GetBytes(tag); //++
        }
    }
}

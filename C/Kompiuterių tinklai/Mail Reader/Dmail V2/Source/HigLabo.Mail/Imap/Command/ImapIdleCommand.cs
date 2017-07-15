using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HigLabo.Net.Internal;

namespace HigLabo.Net.Imap
{
    /// <summary>
    /// 
    /// </summary>
    public class ImapIdleCommand : ImapDataReceiveContext
    {
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ImapIdleCommandMessageReceivedEventArgs> MessageReceived;
        /// <summary>
        /// 
        /// </summary>
        public IAsyncResult IAsyncResult { get; set; }
        internal ImapIdleCommand(String tag, Encoding encoding)
            : base(tag, encoding)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnMessageReceived(ImapIdleCommandMessageReceivedEventArgs e)
        {
            var eh = this.MessageReceived;
            if (eh != null)
            {
                eh(this, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public override bool ReadBuffer(int size)
        {
            var position = this.Stream.Position;
            var bl = base.ReadBuffer(size);
            this.Stream.Position = position;

            var text = this.Encoding.GetString(this.Stream.ToByteArray());
            var e = new ImapIdleCommandMessageReceivedEventArgs(text);
            this.OnMessageReceived(e);
            return e.Done == false;
        }
    }
}

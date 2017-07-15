using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HigLabo.Net.Mail;

namespace HigLabo.Net.Smtp
{
    /// <summary>
    /// 
    /// </summary>
    public class SendMailCommand
    {
        private List<MailAddress> _RcptTo = new List<MailAddress>();
        /// <summary>
        /// 
        /// </summary>
        public String From { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MailAddress> RcptTo
        {
            get { return _RcptTo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="text"></param>
        /// <param name="rcptTo"></param>
        public SendMailCommand(String from, String text, IEnumerable<MailAddress> rcptTo)
        {
            this.From = from;
            this.RcptTo.AddRange(rcptTo);
            this.Text = text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SendMailCommand(SmtpMessage message)
        {
            this.From = this.EnsureFrom(message.From);
            List<MailAddress> l = new List<MailAddress>();
            l.AddRange(message.To);
            l.AddRange(message.Cc);
            l.AddRange(message.Bcc);
            this.RcptTo.AddRange(l);
            this.Text = message.GetDataText();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        public SendMailCommand(String from, SmtpMessage message)
        {
            this.From = from;
            List<MailAddress> l = new List<MailAddress>();
            l.AddRange(message.To);
            l.AddRange(message.Cc);
            l.AddRange(message.Bcc);
            this.RcptTo.AddRange(l);
            this.Text = message.GetDataText();
        }
        private String EnsureFrom(String from)
        {
            var m = MailAddress.TryCreate(from);
            if (m == null)
            {
                if (from.StartsWith("<") == true &&
                    from.EndsWith(">") == true)
                {
                    return from;
                }
                return "<" + from + ">";
            }
            return "<" + m.Value + ">";
        }
    }
}

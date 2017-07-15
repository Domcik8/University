using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net.Mail
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MailClientException : SocketClientException
    {
        /// <summary>
        /// 
        /// </summary>
        public MailClientException()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MailClientException(String message)
            : base(message)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public MailClientException(Exception exception)
            : base(exception)
        {
        }
    }
}

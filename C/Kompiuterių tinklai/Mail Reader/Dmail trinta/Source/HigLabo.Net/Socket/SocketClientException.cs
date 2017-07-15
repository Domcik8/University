using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net
{
    [Serializable]
    public class SocketClientException : Exception
    {
        public SocketClientException()
        {
        }
        public SocketClientException(String message)
            : base(message)
        {
        }
        public SocketClientException(Exception exception)
            : base(exception.Message, exception)
        {
        }
    }
}

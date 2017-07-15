using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class ResponseObjectParseException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public String Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public ResponseObjectParseException(String key)
        {
            this.Key = key;
        }
    }
}

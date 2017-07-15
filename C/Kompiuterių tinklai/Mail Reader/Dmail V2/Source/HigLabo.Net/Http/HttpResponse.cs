using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpResponse
    {
        private Dictionary<String, String> _Headers = new Dictionary<String, String>();
        private Byte[] _BodyData = null;
        private String _BodyText = "";
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public String StatusDescription { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public String ContentType { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 ContentLength { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public CookieCollection Cookies { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public String Method { get; private set; }
#if !SILVERLIGHT
        /// <summary>
        /// 
        /// </summary>
        public String CharacterSet { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Boolean IsMutuallyAuthenticated { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModified { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Version ProtocolVersion { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Uri ResponseUri { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public String Server { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsFromCache { get; private set; }
#endif
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<String, String> Headers
        {
            get { return _Headers; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Byte[] BodyData
        {
            get { return _BodyData; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String BodyText
        {
            get { return _BodyText; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="encoding"></param>
        public HttpResponse(HttpWebResponse response, Encoding encoding)
        {
            var res = response;

            this.StatusCode = res.StatusCode;
            this.StatusDescription = res.StatusDescription;
            this.Method = res.Method;
            this.ContentType = res.ContentType;
            this.ContentLength = res.ContentLength;
            this.Cookies = res.Cookies;
#if !SILVERLIGHT
            this.CharacterSet = res.CharacterSet;
            this.IsMutuallyAuthenticated = res.IsMutuallyAuthenticated;
            this.LastModified = res.LastModified;
            this.ProtocolVersion = res.ProtocolVersion;
            this.Server = res.Server;
            this.IsFromCache = res.IsFromCache;
#endif
            foreach (String key in res.Headers.AllKeys)
            {
                this.Headers[key] = res.Headers[key];
            }
            var bb = res.GetResponseStream().ToByteArray();
            var stm = new MemoryStream(bb);
            StreamReader reader = new StreamReader(stm, encoding);
            _BodyText = reader.ReadToEnd();
            _BodyData = bb;
        }
    }
}

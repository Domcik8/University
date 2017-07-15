using System;

namespace HigLabo.Net
{
    ///<summary>
    /// 
    ///</summary>
    public class AuthorizeInfo
    {
        ///<summary>
        /// 
        ///</summary>
        public string AuthorizeUrl { get; set; }
        ///<summary>
        /// 
        ///</summary>
        public string RequestToken { get; set; }
        ///<summary>
        /// 
        ///</summary>
        public string RequestTokenSecret { get; set; }
        ///<summary>
        /// 
        ///</summary>
        public AuthorizeInfo()
        {
            this.AuthorizeUrl = String.Empty;
            this.RequestToken = String.Empty;
            this.RequestTokenSecret = String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizeUrl"></param>
        /// <param name="requestToken"></param>
        /// <param name="requestTokenSecret"></param>
        public AuthorizeInfo(String authorizeUrl, String requestToken, String requestTokenSecret)
        {
            this.AuthorizeUrl = authorizeUrl;
            this.RequestToken = requestToken;
            this.RequestTokenSecret = requestTokenSecret;
        }
    }
}
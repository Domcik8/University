using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HigLabo.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class AccessTokenInfo
    {
        private Dictionary<String, String> _Values = null;
        private String _Token = "";
        private String _TokenSecret = "";
        /// <summary>
        /// 
        /// </summary>
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String TokenSecret
        {
            get { return _TokenSecret; }
            set { _TokenSecret = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<String, String> Values
        {
            get { return _Values; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        public AccessTokenInfo(String token, String tokenSecret)
        {
            this.Token = token;
            this.TokenSecret = tokenSecret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tokenKey"></param>
        /// <param name="tokenSecretKey"></param>
        /// <returns></returns>
        public static AccessTokenInfo Create(String value, String tokenKey, String tokenSecretKey)
        {
            AccessTokenInfo t = new AccessTokenInfo("", "");
            String[] ss = value.Split('&');
            String[] sss = null;
            Dictionary<String, String> d = new Dictionary<string, string>();
            for (int i = 0; i < ss.Length; i++)
            {
                if (ss[i].Contains("=") == false) { continue; }
                sss = ss[i].Split('=');
                if (sss.Length == 2)
                {
                    d[sss[0].ToLower()] = sss[1];
                }
            }
            t._Values = d;
            if (d.ContainsKey(tokenKey) == true)
            {
                t.Token = d[tokenKey];
            }
            if (d.ContainsKey(tokenSecretKey) == true)
            {
                t.TokenSecret = d[tokenSecretKey];
            }
            return t;
        }
   }
}

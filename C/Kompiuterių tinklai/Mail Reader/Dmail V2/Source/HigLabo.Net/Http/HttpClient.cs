using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace HigLabo.Net
{
    /// <summary>
    /// HTTPでのリクエスト及びレスポンスデータの取得を行う機能を提供するクラスです。
    /// </summary>
    public partial class HttpClient
    {
        private static String UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
#if SILVERLIGHT
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;
#else
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding("us-ascii");
        private X509CertificateCollection _ClientCertificates = new X509CertificateCollection();
#endif
        /// <summary>
        /// WEBサーバーへポストする際に必要なヘッダーの属性のキーを表す文字を取得します。
        /// </summary>
        public static readonly String ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HttpRequestUploadingEventArgs> Uploading;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncHttpCallErrorEventArgs> Error;
        /// <summary>
        /// 
        /// </summary>
        public Int32? RequestBufferSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Encoding RequestEncoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Encoding ResponseEncoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICredentials Credentials { get; set; }
#if !SILVERLIGHT
        /// <summary>
        /// 証明書情報
        /// </summary>
        public X509CertificateCollection ClientCertificates
        {
            get { return _ClientCertificates; }
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        public Action<Action> BeginInvoke { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpClient()
        {
            this.RequestEncoding = DefaultEncoding;
            this.ResponseEncoding = DefaultEncoding;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public HttpWebRequest CreateRequest(HttpRequestCommand command)
        {
            HttpWebRequest req = HttpWebRequest.Create(command.Url) as HttpWebRequest;

            req.Method = command.MethodName.ToString().ToUpper();
            req.ContentType = command.ContentType;
            if (this.CookieContainer != null)
            {
                req.CookieContainer = this.CookieContainer;
            }
            if (this.Credentials != null)
            {
                req.Credentials = this.Credentials;
            }
#if !SILVERLIGHT
            req.ClientCertificates.AddRange(this.ClientCertificates);
#endif
            foreach (String key in command.Headers.AllKeys)
            {
                req.Headers[key] = command.Headers[key];
            }            
            return req;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static String CreateQueryString(String baseUrl, IDictionary<String, String> parameters)
        {
            return CreateQueryString(baseUrl, parameters, s => s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="urlEncodingFunction"></param>
        /// <returns></returns>
        public static String CreateQueryString(String baseUrl, IDictionary<String, String> parameters, Func<String, String> urlEncodingFunction)
        {
            String result = CreateKeyEqualValueAndFormatString(parameters, urlEncodingFunction);
            if (String.IsNullOrEmpty(result))
            {
                return baseUrl;
            }
            return baseUrl + "?" + result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="urlEncodingFunction"></param>
        /// <returns></returns>
        public static String CreateKeyEqualValueAndFormatString(IDictionary<String, String> parameters, Func<String, String> urlEncodingFunction)
        {
            StringBuilder sb = new StringBuilder(256);
            Boolean first = true;
            foreach (var parameter in parameters)
            {
                if (first == true)
                {
                    first = false;
                }
                else
                {
                    sb.Append('&');
                }
                sb.Append(parameter.Key);
                sb.Append('=');
                sb.Append(urlEncodingFunction(parameter.Value));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(String value)
        {
            return UrlEncode(value, Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string UrlEncode(String value, Encoding encode)
        {
            StringBuilder result = new StringBuilder();
            byte[] data = encode.GetBytes(value);
            int len = data.Length;

            for (int i = 0; i < len; i++)
            {
                int c = data[i];
                if (c < 0x80 && UnreservedChars.IndexOf((char)c) != -1)
                {
                    result.Append((char)c);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)data[i]));
                }
            }

            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnUploading(HttpRequestUploadingEventArgs e)
        {
            var eh = this.Uploading;
            if (eh != null)
            {
                if (this.BeginInvoke == null)
                {
                    eh(this, e);
                }
                else
                {
                    this.BeginInvoke(() => eh(this, e));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace HigLabo.Net
{
    public partial class OAuthClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AuthorizeInfo GetAuthorizeInfo()
        {
            String url = this.RequestTokenUrl;
            var cm = new GetRequestTokenCommand(this.ConsumerKey, this.ConsumerSecret, "", "", HttpMethodName.Post);
            SignatureInfo si = OAuthClient.GenerateSignature(new Uri(url), cm, OAuthSignatureTypes.HMACSHA1);
            HttpClient cl = new HttpClient();
            HttpRequestCommand command =
                new HttpRequestCommand(String.Format("{0}?{1}&oauth_signature={2}", url, si.NormalizedRequestParameters, si.Signature), HttpMethodName.Post);
            String result = cl.GetBodyText(command);
            //正規表現でoauth_token,oauth_token_secret取得
            AuthorizeInfo ai = new AuthorizeInfo();
            ai.AuthorizeUrl = String.Format("{0}?{1}", this.AuthorizeUrl, result);
            ai.RequestToken = this.GetMatchValue(RegexList.OAuthToken, result);
            ai.RequestTokenSecret = this.GetMatchValue(RegexList.OAuthTokenSecret, result);
            return ai;
        }
        private String GetMatchValue(Regex regex, String input)
        {
            var m = regex.Match(input);
            if (m.Groups.Count > 1)
            {
                return m.Groups[1].Value;
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestToken"></param>
        /// <param name="requestTokenSecret"></param>
        /// <returns></returns>
        public AccessTokenInfo GetAccessToken(String requestToken, String requestTokenSecret)
        {
            String url = this.AccessTokenUrl;
            GetRequestTokenCommand cm = new GetRequestTokenCommand(this.ConsumerKey, this.ConsumerSecret
                , requestToken, requestTokenSecret, HttpMethodName.Post);
            SignatureInfo si = OAuthClient.GenerateSignature(new Uri(url), cm, OAuthSignatureTypes.HMACSHA1);
            HttpRequestCommand command =
                new HttpRequestCommand(String.Format("{0}?{1}&oauth_signature={2}", url, si.NormalizedRequestParameters, si.Signature), HttpMethodName.Post);
            HttpClient cl = new HttpClient();
            String result = cl.GetBodyText(command);
            return AccessTokenInfo.Create(result, "oauth_token", "oauth_token_secret");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizeInfo"></param>
        /// <param name="oauthVerifier"></param>
        /// <returns></returns>
        public AccessTokenInfo GetAccessToken(AuthorizeInfo authorizeInfo, String oauthVerifier)
        {
            var ai = authorizeInfo;
            var cm = new GetRequestTokenCommand(this.ConsumerKey, this.ConsumerSecret
                , ai.RequestToken, ai.RequestTokenSecret, HttpMethodName.Post);
            var si = OAuthClient.GenerateSignature(new Uri(this.AccessTokenUrl), cm, OAuthSignatureTypes.HMACSHA1);
            HttpRequestCommand command = new HttpRequestCommand(String.Format("{0}?{1}&oauth_verifier={2}&oauth_signature={3}"
                    , this.AccessTokenUrl, si.NormalizedRequestParameters, oauthVerifier, si.Signature), HttpMethodName.Post);
            HttpClient cl = new HttpClient();
            String s = cl.GetBodyText(command);
            return AccessTokenInfo.Create(s, "oauth_token", "oauth_token_secret");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <returns></returns>
        public HttpResponse GetResponse(HttpMethodName methodName, String url, String token, String tokenSecret)
        {
            return this.GetResponse(methodName, url, token, tokenSecret, new Dictionary<String, String>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public HttpResponse GetResponse(HttpMethodName methodName, String url, String token, String tokenSecret
            , IDictionary<String, String> queryString)
        {
            var cm = this.CreateHttpRequestCommand(methodName, url, token, tokenSecret, queryString);
            return this.GetResponse(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <param name="bodyData"></param>
        /// <returns></returns>
        public HttpResponse GetResponse(HttpMethodName methodName, String url, String token, String tokenSecret
            , Byte[] bodyData)
        {
            var cm = this.CreateHttpRequestCommand(methodName, url, token, tokenSecret, new Dictionary<String, String>());
            cm.SetBodyStream(bodyData);
            return this.GetResponse(cm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <returns></returns>
        public HttpWebResponse GetHttpWebResponse(HttpMethodName methodName, String url, String token, String tokenSecret)
        {
            return this.GetHttpWebResponse(methodName, url, token, tokenSecret, new Dictionary<String, String>(), new Dictionary<String, String>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public HttpWebResponse GetHttpWebResponse(HttpMethodName methodName, String url, String token, String tokenSecret
            , IDictionary<String, String> queryString)
        {
            return this.GetHttpWebResponse(methodName, url, token, tokenSecret, queryString, new Dictionary<String, String>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public HttpWebResponse GetHttpWebResponse(HttpMethodName methodName, String url, String token, String tokenSecret
            , IDictionary<String, String> queryString, IDictionary<String, String> parameters)
        {
            Dictionary<String, String> d = new Dictionary<string, string>();
            var cm = this.CreateHttpRequestCommand(methodName, url, token, tokenSecret, queryString);
            foreach (var p in parameters)
            {
                d[p.Key] = cm.UrlEncodeFunction(p.Value);
            }
            cm.SetBodyStream(this.RequestEncoding, d);
            return this.GetHttpWebResponse(cm);
        }
    }
}

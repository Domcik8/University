using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HigLabo.Net
{
    public partial class OAuthClient
    {
#if SILVERLIGHT
        private static readonly Encoding GenerateSignatureEncoding = Encoding.UTF8;
#else
        private static readonly Encoding GenerateSignatureEncoding = Encoding.GetEncoding("us-ascii");
#endif
        ///<summary>
        /// 
        ///</summary>
        public class RegexList
        {
            ///<summary>
            /// 
            ///</summary>
            public static readonly Regex OAuthToken = new Regex(@"oauth_token=([^&]*)");
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex OAuthTokenSecret = new Regex(@"oauth_token_secret=([^&]*)");
            /// <summary>
            /// 
            /// </summary>
            public static readonly Regex OAuthCallback = new Regex(@"oauth_callback=([^&]*)");
        }
        /// <summary>
        /// 
        /// </summary>
        public class Key
        {
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthVersionNo = "1.0";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthParameterPrefix = "oauth_";
            /// <summary>
            /// List of know and used oauth parameters' names 
            /// </summary>
            public static readonly String OAuthConsumerKey = "oauth_consumer_key";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthCallback = "oauth_callback";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthVersion = "oauth_version";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthSignatureMethod = "oauth_signature_method";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthSignature = "oauth_signature";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthTimestamp = "oauth_timestamp";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthNonce = "oauth_nonce";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthToken = "oauth_token";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String OAuthTokenSecret = "oauth_token_secret";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String HMACSHA1SignatureType = "HMAC-SHA1";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String PlainTextSignatureType = "PLAINTEXT";
            /// <summary>
            /// 
            /// </summary>
            public static readonly String RSASHA1SignatureType = "RSA-SHA1";
        }
        private static readonly Random Random = new Random();
        /// <summary>
        /// Internal function to cut out all non oauth query String parameters (all parameters not begining with "oauth_")
        /// </summary>
        /// <param name="parameters">The query String part of the Url</param>
        /// <returns>A list of QueryParameter each containing the parameter name and value</returns>
        protected static List<KeyValuePair<String, String>> GetQueryParameters(String parameters)
        {
            if (parameters.StartsWith("?"))
            {
                parameters = parameters.Remove(0, 1);
            }

            List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>();

            if (!String.IsNullOrEmpty(parameters))
            {
                String[] p = parameters.Split('&');
                foreach (String s in p)
                {
                    if (!String.IsNullOrEmpty(s) && !s.StartsWith(Key.OAuthParameterPrefix))
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            String[] temp = s.Split('=');
                            result.Add(new KeyValuePair<String, String>(temp[0], temp[1]));
                        }
                        else
                        {
                            result.Add(new KeyValuePair<String, String>(s, String.Empty));
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Normalizes the request parameters according to the spec
        /// </summary>
        /// <param name="parameters">The list of parameters already sorted</param>
        /// <returns>a String representing the normalized parameters</returns>
        protected static String NormalizeRequestParameters(IList<KeyValuePair<String, String>> parameters)
        {
            StringBuilder sb = new StringBuilder(256);
            KeyValuePair<String, String> p;
            for (int i = 0; i < parameters.Count; i++)
            {
                p = parameters[i];
                sb.AppendFormat("{0}={1}", p.Key, p.Value);

                if (i < parameters.Count - 1)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="command"></param>
        /// <param name="signatureType"></param>
        /// <returns></returns>
        public static SignatureInfo GenerateSignature(Uri url, GetRequestTokenCommand command, OAuthSignatureTypes signatureType)
        {
            var cm = command;
            SignatureInfo si = new SignatureInfo();
            switch (signatureType)
            {
                case OAuthSignatureTypes.PLAINTEXT:
                    si.Signature = OAuthClient.UrlEncode(String.Format("{0}&{1}", cm.ConsumerKeySecret, cm.TokenSecret));
                    return si;
                case OAuthSignatureTypes.HMACSHA1:
                    si = GenerateSignatureBase(url, cm, Key.HMACSHA1SignatureType);
                    HMACSHA1 hs = new HMACSHA1();
                    hs.Key = OAuthClient.GenerateSignatureEncoding.GetBytes(String.Format("{0}&{1}"
                        , OAuthClient.UrlEncode(cm.ConsumerKeySecret), String.IsNullOrEmpty(cm.TokenSecret) ? "" : OAuthClient.UrlEncode(cm.TokenSecret)));
                    si.Signature = GenerateSignatureUsingHash(si.Signature, hs);
                    return si;
                case OAuthSignatureTypes.RSASHA1: throw new NotImplementedException();
            }
            throw new ArgumentException("Unknown signature type", "signatureType");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="command"></param>
        /// <param name="signatureType"></param>
        /// <returns></returns>
        public static SignatureInfo GenerateSignatureBase(Uri url, GetRequestTokenCommand command, String signatureType)
        {
            SignatureInfo si = new SignatureInfo();
            var cm = command;

            if (String.IsNullOrEmpty(signatureType))
            {
                throw new ArgumentNullException("signatureType");
            }

            List<KeyValuePair<String, String>> parameters = OAuthClient.GetQueryParameters(url.Query);
            parameters.Add(new KeyValuePair<String, String>(Key.OAuthVersion, Key.OAuthVersionNo));
            parameters.Add(new KeyValuePair<String, String>(Key.OAuthNonce, cm.Nonce));
            parameters.Add(new KeyValuePair<String, String>(Key.OAuthTimestamp, cm.TimeStamp));
            parameters.Add(new KeyValuePair<String, String>(Key.OAuthSignatureMethod, signatureType));
            parameters.Add(new KeyValuePair<String, String>(Key.OAuthConsumerKey, cm.ConsumerKey));

            if (!String.IsNullOrEmpty(cm.Token))
            {
                parameters.Add(new KeyValuePair<String, String>(Key.OAuthToken, cm.Token));
            }

            parameters.Sort(CompareQueryString);

            si.NormalizedUrl = String.Format("{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                si.NormalizedUrl += ":" + url.Port;
            }
            si.NormalizedUrl += url.AbsolutePath;
            si.NormalizedRequestParameters = NormalizeRequestParameters(parameters);

            StringBuilder sb = new StringBuilder(1000);
            sb.AppendFormat("{0}&", cm.MethodName.ToString().ToUpper());
            sb.AppendFormat("{0}&", OAuthClient.UrlEncode(si.NormalizedUrl));
            sb.AppendFormat("{0}", OAuthClient.UrlEncode(si.NormalizedRequestParameters));
            si.Signature = sb.ToString();
            return si;
        }
        private static int CompareQueryString(KeyValuePair<String, String> x, KeyValuePair<String, String> y)
        {
            if (x.Key == y.Key)
            {
                return String.Compare(x.Value, y.Value);
            }
            return String.Compare(x.Key, y.Key);
        }
        /// <summary>
        /// Generate the signature value based on the given signature base and hash algorithm
        /// </summary>
        /// <param name="signatureBase">The signature based as produced by the GenerateSignatureBase method or by any other means</param>
        /// <param name="hash">The hash algorithm used to perform the hashing. If the hashing algorithm requires initialization or a key it should be set prior to calling this method</param>
        /// <returns>A base64 String of the hash value</returns>
        public static String GenerateSignatureUsingHash(String signatureBase, HashAlgorithm hash)
        {
            return ComputeHash(hash, signatureBase);
        }
        /// <summary>
        /// Helper function to compute a hash value
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
        /// <param name="data">The data to hash</param>
        /// <returns>a Base64 String of the hash value</returns>
        public static String ComputeHash(HashAlgorithm hashAlgorithm, String data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = OAuthClient.GenerateSignatureEncoding.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static SignatureInfo GenerateSignature(Uri url, GetRequestTokenCommand command)
        {
            return GenerateSignature(url, command, OAuthSignatureTypes.HMACSHA1);
        }
        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        internal static String GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static String GenerateNonce()
        {
            return GenerateNonce1();
        }
        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        private static String GenerateNonce0()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return Random.Next(123400, 9999999).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static String GenerateNonce1()
        {
            String letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(8);
            Random random = new Random();
            for (int i = 0; i < 8; ++i)
            {
                result.Append(letters[random.Next(letters.Length)]);
            }
            return result.ToString();
        }
    }
}


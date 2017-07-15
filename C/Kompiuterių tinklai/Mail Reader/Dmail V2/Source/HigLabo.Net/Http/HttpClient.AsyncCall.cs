using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace HigLabo.Net
{
    /// <summary>
    /// HTTPでのリクエスト及びレスポンスデータの取得を行う機能を提供するクラスです。
    /// </summary>
    public partial class HttpClient
    {
#if SILVERLIGHT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcher"></param>
        public void SetDispatcher(System.Windows.Threading.Dispatcher dispatcher)
        {
            this.BeginInvoke = action => dispatcher.BeginInvoke(action);
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="context"></param>
        public void GetHttpWebResponse(String url, AsyncHttpContext context)
        {
            var req = this.CreateRequest(new HttpRequestCommand(url));
            context.BeginRequest(req);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public void GetHttpWebResponse(String url, Action<HttpWebResponse> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <param name="callback"></param>
        public void GetHttpWebResponse(String url, Dictionary<String, String> values, Action<HttpWebResponse> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, this.RequestEncoding, values), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        public void GetHttpWebResponse(String url, Byte[] data, Action<HttpWebResponse> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, data), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <param name="callback"></param>
        public void GetHttpWebResponse(String url, Stream stream, Action<HttpWebResponse> callback)
        {
            var cm = new HttpRequestCommand(url, stream);
            var req = this.CreateRequest(cm);
            AsyncHttpContext cx = null;
            if (this.RequestBufferSize.HasValue == true)
            {
                cx = new AsyncHttpContext(cm, this.RequestBufferSize.Value, this.AsyncHttpContextCallback(callback));
            }
            else
            {
                cx = new AsyncHttpContext(cm, this.AsyncHttpContextCallback(callback));
            }
            cx.Uploading += (o, e) => this.OnUploading(e);
            cx.Error += (o, e) => this.OnError(e);
            cx.BeginRequest(req);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="callback"></param>
        public void GetHttpWebResponse(HttpRequestCommand command, Action<HttpWebResponse> callback)
        {
            var req = this.CreateRequest(command);
            AsyncHttpContext cx = this.CreateAsyncHttpContext(command, callback);
            cx.Uploading += (o, e) => this.OnUploading(e);
            cx.Error += (o, e) => this.OnError(e);
            cx.BeginRequest(req);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        protected AsyncHttpContext CreateAsyncHttpContext(HttpRequestCommand command, Action<HttpWebResponse> callback)
        {
            AsyncHttpContext cx = null;
            if (this.RequestBufferSize.HasValue == true)
            {
                cx = new AsyncHttpContext(command, this.RequestBufferSize.Value, this.AsyncHttpContextCallback(callback));
            }
            else
            {
                cx = new AsyncHttpContext(command, this.AsyncHttpContextCallback(callback));
            }
            return cx;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        private Action<HttpWebResponse> AsyncHttpContextCallback(Action<HttpWebResponse> callback)
        {
            Action<HttpWebResponse> f = res =>
            {
                var eh = callback;
                if (eh != null)
                {
                    if (this.BeginInvoke == null)
                    {
                        eh(res);
                    }
                    else
                    {
                        this.BeginInvoke(() => eh(res));
                    }
                }
            };
            return f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public void GetResponse(String url, Action<HttpResponse> callback)
        {
            this.GetResponse(new HttpRequestCommand(url), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <param name="callback"></param>
        public void GetResponse(String url, Dictionary<String, String> values, Action<HttpResponse> callback)
        {
            this.GetResponse(new HttpRequestCommand(url, this.RequestEncoding, values), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        public void GetResponse(String url, Byte[] data, Action<HttpResponse> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, data), res => this.GetResponseCallback(res, callback));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <param name="callback"></param>
        public void GetResponse(String url, Stream stream, Action<HttpResponse> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, stream), res => this.GetResponseCallback(res, callback));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="callback"></param>
        public void GetResponse(HttpRequestCommand command, Action<HttpResponse> callback)
        {
            var req = this.CreateRequest(command);
            AsyncHttpContext cx = this.CreateAsyncHttpContext(command, res => this.GetResponseCallback(res, callback));
            cx.Uploading += (o, e) => this.OnUploading(e);
            cx.Error += (o, e) => this.GetResponseErrorCallback(e, callback);
            cx.BeginRequest(req);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="callback"></param>
        protected void GetResponseCallback(HttpWebResponse response, Action<HttpResponse> callback)
        {
            var res = response;
            var hr = new HttpResponse(res, this.ResponseEncoding);
            callback(hr);
        }
        private void GetResponseErrorCallback(AsyncHttpCallErrorEventArgs e, Action<HttpResponse> callback)
        {
            var ex = e.Exception as WebException;
            if (ex == null)
            {
                this.OnError(e);
                return;
            }
            var res = ex.Response as HttpWebResponse;
            if (res == null)
            {
                this.OnError(e);
                return;
            }
            this.GetResponseCallback(res, callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public void GetBodyText(String url, Action<String> callback)
        {
            this.GetBodyText(new HttpRequestCommand(url), callback);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <param name="callback"></param>
        public void GetBodyText(String url, Dictionary<String, String> values, Action<String> callback)
        {
            this.GetBodyText(new HttpRequestCommand(url, this.RequestEncoding, values), text => callback(text));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        public void GetBodyText(String url, Byte[] data, Action<String> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, data), res => this.GetBodyTextCallback(res, callback));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <param name="callback"></param>
        public void GetBodyText(String url, Stream stream, Action<String> callback)
        {
            this.GetHttpWebResponse(new HttpRequestCommand(url, stream), res => this.GetBodyTextCallback(res, callback));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="callback"></param>
        public void GetBodyText(HttpRequestCommand command, Action<String> callback)
        {
            this.GetHttpWebResponse(command, res => this.GetBodyTextCallback(res, callback));
        }
        private void GetBodyTextCallback(HttpWebResponse response, Action<String> callback)
        {
            this.GetBodyTextCallback(response, HttpClient.DefaultEncoding, callback);
        }
        private void GetBodyTextCallback(HttpWebResponse response, Encoding responseEncoding, Action<String> callback)
        {
            var res = response;
            StreamReader sr = new StreamReader(res.GetResponseStream(), responseEncoding);
            var text = sr.ReadToEnd();
            callback(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnError(AsyncHttpCallErrorEventArgs e)
        {
            var eh = this.Error;
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

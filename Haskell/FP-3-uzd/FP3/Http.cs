using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace FP3
{
    public class Http
    {
        public static async Task<string> HttpGet(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "application/m-expr+map";

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException)
            {
                return "Internal Server Error";
            }
        }

        public static async Task<string> HttpPost(string url, string message)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(message);

            request.Method = "POST";
            request.ContentType = "application/m-expr+map";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        static string baseUrl = "http://tictactoe.homedir.eu/game/";

        public static string BuildUrl(string gameID, string player)
        {
            return (baseUrl + gameID + "/player/" + player); //Monoid
        }
    }
}

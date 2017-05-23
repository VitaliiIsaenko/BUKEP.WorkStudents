using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Bukep.ShedulerApi
{
    /// <summary>
    /// Use for get JSON from API BUKEP.
    /// Execute post and get requests and return JSON.
    /// </summary>
    static class HttpRequstHelper
    {
        private const string urlApi = "https://my.bukep.ru:447/api/Schedule";

        public static string ExecuteGet(string nameMethod, IDictionary<string, string> parameters)
        {
            string url = CreateURL(nameMethod, parameters);
            Console.WriteLine("URL = " + url);
            WebRequest request = WebRequest.Create(url);

            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string json = reader.ReadToEnd();

            reader.Close();
            response.Close();
            return json;
        }

        private static string CreateURL(string nameMethod, IDictionary<string, string> parameter)
        {
            string urlParameter = CreateUrlParameter(parameter);
            string url = String.Format("{0}/{1}?{2}", urlApi, nameMethod, urlParameter);
            return url;
        }

        public static string ExecutePost(string nameMethod, IDictionary<string, string> parameters,
            String bodyForPost)
        {
            string url = CreateURL(nameMethod, parameters);
            Console.WriteLine("URL = " + url);
            WebRequest request = WebRequest.Create(url);


            var dataForPost = Encoding.ASCII.GetBytes(bodyForPost);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = dataForPost.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(dataForPost, 0, dataForPost.Length);
            stream.Close();

            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string json = reader.ReadToEnd();
            reader.Close();

            response.Close();
            return json;
        }

        private static string CreateUrlParameter(IDictionary<string, string> parameters)
        {
            StringBuilder urlParameter = new StringBuilder();
            foreach (string name in parameters.Keys)
            {
                string value = parameters[name];
                urlParameter.AppendFormat("{0}={1}&", name, value);
            }
            return urlParameter.ToString();
        }

    }
}


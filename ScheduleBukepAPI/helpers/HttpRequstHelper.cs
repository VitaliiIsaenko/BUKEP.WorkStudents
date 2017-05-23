using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ScheduleBukepAPI.helpers
{
    /// <summary>
    /// Use for get Json from API BUKEP.
    /// Execute post and get requests and return Json.
    /// </summary>
    internal static class HttpRequstHelper
    {
        private const string UrlApi = "https://my.bukep.ru:447/api/Schedule";

        public static string ExecuteGet(string nameMethod, IDictionary<string, string> parameters)
        {
            var url = CreateUrl(nameMethod, parameters);
            Console.WriteLine("URL = " + url);
            var request = WebRequest.Create(url);

            request.Credentials = CredentialCache.DefaultCredentials;
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var json = reader.ReadToEnd();

            reader.Close();
            response.Close();
            return json;
        }

        private static string CreateUrl(string nameMethod, IDictionary<string, string> parameter)
        {
            var urlParameter = CreateUrlParameter(parameter);
            var url = $"{UrlApi}/{nameMethod}?{urlParameter}";
            return url;
        }

        public static string ExecutePost(string nameMethod, IDictionary<string, string> parameters,
            string bodyForPost)
        {
            var url = CreateUrl(nameMethod, parameters);
            Console.WriteLine("URL = " + url);
            var request = WebRequest.Create(url);


            var dataForPost = Encoding.ASCII.GetBytes(bodyForPost);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = dataForPost.Length;

            var stream = request.GetRequestStream();
            stream.Write(dataForPost, 0, dataForPost.Length);
            stream.Close();

            request.Credentials = CredentialCache.DefaultCredentials;
            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var json = reader.ReadToEnd();
            reader.Close();

            response.Close();
            return json;
        }

        private static string CreateUrlParameter(IDictionary<string, string> parameters)
        {
            var urlParameter = new StringBuilder();
            foreach (var name in parameters.Keys)
            {
                var value = parameters[name];
                urlParameter.AppendFormat("{0}={1}&", name, value);
            }
            return urlParameter.ToString();
        }

    }
}


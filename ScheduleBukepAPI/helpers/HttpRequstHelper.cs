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
    public class HttpRequstHelper
    {
        private const string UrlApi = "https://my.bukep.ru:447/api/Schedule";

        
        public string ExecuteGet(string nameMethod, IDictionary<string, string> parameters)
        {
            var url = CreateUrl(nameMethod, parameters);
            Console.WriteLine("URL = " + url);
            var request = WebRequest.Create(url);

            request.Credentials = CredentialCache.DefaultCredentials;
            var response = request.GetResponse();

            //TODO: повторения этого кода в ExecutePost()
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var json = reader.ReadToEnd();

            //TODO: добавить close() в try/catch
            reader.Close();
            response.Close();
            return json;
        }


        public string ExecutePost(string nameMethod, IDictionary<string, string> parameters,
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

            //TODO: добавить close() в try/catch
            reader.Close();
            response.Close();
            Console.Write("Json = " + json + "\n");
            return json;
        }

        private string CreateUrl(string nameMethod, IDictionary<string, string> parameter)
        {
            var urlParameter = CreateUrlParameter(parameter);
            var url = $"{UrlApi}/{nameMethod}?{urlParameter}";
            return url;
        }

        private string CreateUrlParameter(IDictionary<string, string> parameters)
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
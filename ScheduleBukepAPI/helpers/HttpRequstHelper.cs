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

        public string ExecuteGet(string url)
        {
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


        public string ExecutePost(string url, string bodyForPost)
        {
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


    }
}
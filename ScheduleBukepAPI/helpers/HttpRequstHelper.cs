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

            return ReadingJsonFromResponse(request.GetResponse());
        }

        public string ExecutePost(string url, string bodyForPost)
        {
            Console.WriteLine("URL = " + url);
            WebRequest request = WebRequest.Create(url);


            byte[] dataForPost = Encoding.ASCII.GetBytes(bodyForPost);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = dataForPost.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(dataForPost, 0, dataForPost.Length);
            stream.Close();

            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse webResponse = request.GetResponse();
            return ReadingJsonFromResponse(webResponse);
        }

        private static string ReadingJsonFromResponse(WebResponse response)
        {
            StreamReader reader = null;
            try
            {
                var dataStream = response.GetResponseStream();
                if (dataStream == null)
                {
                    throw new WebException("Failed get response stream.");
                }
                reader = new StreamReader(dataStream);
                var json = reader.ReadToEnd();
                return json;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                reader?.Close();
                response?.Close();
            }
        }
    }
}
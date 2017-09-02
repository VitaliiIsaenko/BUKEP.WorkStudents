using System.Collections.Generic;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service.paremeters;
using System;
using System.Collections.Specialized;
using System.Web;

namespace ScheduleBukepAPI.service
{
    public class BaseService
    {
        private readonly HttpRequstHelper _httpRequestHelper;

        public BaseService(HttpRequstHelper httpRequestHelper)
        {
            _httpRequestHelper = httpRequestHelper;
        }

        public BaseService() : this(new HttpRequstHelper())
        {
        }

        protected List<T> ConvertToList<T>(string json)
        {
            return JsonConvert.ConvertToList<T>(json);
        }

        protected T ConvertTo<T>(string json)
        {
            return JsonConvert.ConvertTo<T>(json);
        }

        protected string ConvertToJson<T>(T dto)
        {
            return JsonConvert.ConvertToJson(dto);
        }

        protected string ExecuteGet(MethodApi nameMethod, IDictionary<string, string> parameters)
        {
            var url = UriBuilder(parameters);
            return _httpRequestHelper.ExecuteGet(url);
        }

        private static string UriBuilder(IDictionary<string, string> parameters)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Host = "https";
            uriBuilder.Scheme = "my.bukep.ru";

            NameValueCollection parametersQuery = HttpUtility.ParseQueryString(string.Empty);
            foreach(var ketValuePair in parameters)
            {
                parametersQuery[ketValuePair.Key] = ketValuePair.Value;
            }
            uriBuilder.Query = parametersQuery.ToString();
            return uriBuilder.Uri.ToString();
        }

        protected string ExecutePost(MethodApi nameMethod, IDictionary<string, string> parameters,
            IList<int> bodyForPost)
        {
            var url = UriBuilder(parameters);
            return _httpRequestHelper.ExecutePost(url, bodyForPost);
        }
    }
}
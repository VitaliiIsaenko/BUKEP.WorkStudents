using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    public class BaseService
    {
        private readonly HttpRequstHelper _httpRequestHelper;
        private readonly JsonConvert _jsonConvert;

        public BaseService(HttpRequstHelper httpRequestHelper, JsonConvert jsonConvert)
        {
            _httpRequestHelper = httpRequestHelper;
            _jsonConvert = jsonConvert;
        }

        public BaseService() : this(new HttpRequstHelper(), new JsonConvert())
        {
        }

        protected List<T> ConvertToList<T>(string json)
        {
            return _jsonConvert.ConvertToList<T>(json);
        }

        protected T ConvertTo<T>(string json)
        {
            return _jsonConvert.ConvertTo<T>(json);
        }

        protected string ConvertToJson<T>(T dto)
        {
            return _jsonConvert.ConvertToJson(dto);
        }

        protected string ExecuteGet(string nameMethod, IDictionary<string, string> parameters)
        {
            var url = CreatorUrl.CreateUrl(nameMethod, parameters);
            return _httpRequestHelper.ExecuteGet(url);
        }

        protected string ExecutePost(string nameMethod, IDictionary<string, string> parameters,
            string bodyForPost)
        {
            var url = CreatorUrl.CreateUrl(nameMethod, parameters);
            return _httpRequestHelper.ExecutePost(url, bodyForPost);
        }
    }
}
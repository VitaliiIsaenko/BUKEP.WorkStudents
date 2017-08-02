using System.Collections.Generic;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service.paremeters;

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
            var url = CreatorUrl.CreateUrl(nameMethod.ToString(), parameters);
            return _httpRequestHelper.ExecuteGet(url);
        }

        protected string ExecutePost(MethodApi nameMethod, IDictionary<string, string> parameters,
            IList<int> bodyForPost)
        {
            var url = CreatorUrl.CreateUrl(nameMethod.ToString(), parameters);
            return _httpRequestHelper.ExecutePost(url, bodyForPost);
        }
    }
}
using System.Collections.Generic;
using System.Text;

namespace ScheduleBukepAPI.helpers
{
    /// <summary>
    /// Создаёт Url запрос для Api.
    /// </summary>
    public static class CreatorUrl
    {
        private const string UrlApi = "https://my.bukep.ru:447/api/Schedule";

        /// <summary>
        /// Создать Url для Api с параметрами.
        /// </summary>
        /// <param name="nameMethod">Имя метода для Api</param>
        /// <param name="parameter">Парвметры</param>
        /// <returns></returns>
        public static string CreateUrl(string nameMethod, IDictionary<string, string> parameter)
        {
            var urlParameter = CreateUrlParameter(parameter);
            var url = $"{UrlApi}/{nameMethod}?{urlParameter}";
            return url;
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
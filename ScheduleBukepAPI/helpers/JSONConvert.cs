using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.helpers
{
    public static class JsonConvert
    {
        public static List<T> ConvertToList<T>(string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json, jsonSerializerSettings);
        }

        public static T ConvertTo<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string ConvertToJson<T>(T dto)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dto);
        }
    }
}
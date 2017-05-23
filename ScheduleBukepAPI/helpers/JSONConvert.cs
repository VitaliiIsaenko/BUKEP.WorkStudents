using System.Collections.Generic;

namespace ScheduleBukepAPI.helpers
{
    public static class JsonConvert
    {
        public static List<T> ConvertToList<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
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

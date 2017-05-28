using System.Collections.Generic;

namespace ScheduleBukepAPI.helpers
{
    public class JsonConvert
    {
        public List<T> ConvertToList<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
        }

        public T ConvertTo<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public string ConvertToJson<T>(T dto)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dto);
        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bukep.ShedulerApi
{
    public static class JSONConvert
    {
        public static List<T> ConvertJSONToListDTO<T>(string json)
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static T ConvertJSONToDTO<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ConvertDTOToJSON<T>(T dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
    }
}

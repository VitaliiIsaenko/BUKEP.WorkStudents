using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ScheduleBukepAPI.helpers
{
    public static class JsonConvert
    {
        public static List<T> ConvertToList<T>(string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new RequireObjectPropertiesContractResolver()
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


    /// <summary>
    /// Нужен для проверки заполнения всех полей объекта при получении его из Json.
    /// </summary>
    internal class RequireObjectPropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.ItemRequired = Required.Always;
            return contract;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Pulpit
    {
        /// <summary>
        /// Key - id
        /// Value - name
        /// </summary>
        [JsonProperty("pulpit")]
        public KeyValuePair<int, string> Info { get; set; }
        public bool IsActiveSchedule { get; set; }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Pulpit
    {
        public int IdPulpit { get; set; }
        [JsonProperty("pulpit")]
        public KeyValuePair<int, string> Info { get; set; }
        public bool IsActiveSchedule { get; set; }
    }
}
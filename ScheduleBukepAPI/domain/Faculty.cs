using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    /// <summary>
    ///  DTO from API method GetFaculties.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Key - id
        /// Value - name 
        /// </summary>
        [JsonProperty("faculty")]
        public KeyValuePair<int, string> Info { get; set; }

        public bool IsActiveSchedule { get; set; }
    }
}
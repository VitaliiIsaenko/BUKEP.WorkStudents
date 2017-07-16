using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Specialty
    {
        /// <summary>
        /// Key - List key
        /// Value - name 
        /// </summary>
        [JsonProperty("speciality")]
        public KeyValuePair<IList<int>, string> SpecialityInfo { get; set; }

        public KeyValuePair<int, string> LevelQualification { get; set; }
    }
}
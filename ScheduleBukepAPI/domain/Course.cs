using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Course
    {
        [JsonProperty("course")]
        public KeyValuePair<int, string> CourseInfo { get; set; }
    }
}
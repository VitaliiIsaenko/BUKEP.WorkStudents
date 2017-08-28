using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Group
    {
        [JsonProperty("idsSchedulGroup")]
        public IList<int> Ids { get; set; }
        [JsonProperty("group")]
        public string Info { get; set; }
        public string AffixusNameGroup { get; set; }
        public string NameGroupOld { get; set; }
        public int IdSemestr { get; set; }
        public string NameTypeSchedule { get; set; }
        public DateTime? ScheduleDateFrom { get; set; }
        public DateTime? ScheduleDateTo { get; set; }
    }
}
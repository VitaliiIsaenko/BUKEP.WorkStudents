using System;
using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class Group
    {
        public IList<int> IdsSchedulGroup { get; set; }
        public string NameGroup { get; set; }
        public string AffixusNameGroup { get; set; }
        public string NameGroupOld { get; set; }
        public int IdSemestr { get; set; }
        public string NameTypeSchedule { get; set; }
        public DateTime? ScheduleDateFrom { get; set; }
        public DateTime? ScheduleDateTo { get; set; }
    }
}
using System.Collections.Generic;

namespace Bukep.ShedulerApi.apiDTO
{
    public class Group
    {
        public IList<int> IdsSchedulGroup { get; set; }
        public string NameGroup { get; set; }
        public string AffixusNameGroup { get; set; }
        public string NameGroupOld { get; set; }
        public string IdSemestr { get; set; }
        public string NameTypeShedule { get; set; }
        public string ScheduleDateFrom { get; set; }
        public string ScheduleDateTo { get; set; }
    }
}
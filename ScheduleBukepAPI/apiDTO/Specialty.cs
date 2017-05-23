using System.Collections.Generic;

namespace Bukep.ShedulerApi.apiDTO
{
    public class Specialty
    {
        public IList<int> IdsSpecialty { get; set; }
        public string NameSpeciality { get; set; }
        public string IdLevelQualification { get; set; }
        public string NameLevelQualification { get; set; }
    }
}

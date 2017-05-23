using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class Specialty
    {
        public IList<int> IdsSpecialty { get; set; }
        public string NameSpeciality { get; set; }
        public string IdLevelQualification { get; set; }
        public string NameLevelQualification { get; set; }
    }
}

using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class Specialty
    {
        //TODO: Конфликт имени класса и метода
        public IList<KeyValue> speciality { get; set; }
        public string NameSpeciality { get; set; }
        public string IdLevelQualification { get; set; }
        public string NameLevelQualification { get; set; }
    }
}

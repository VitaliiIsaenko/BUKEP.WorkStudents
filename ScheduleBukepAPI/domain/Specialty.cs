using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class Specialty
    {
        /// <summary>
        /// Key - List key
        /// Value - name 
        /// </summary>
        public KeyValuePair<IList<int>, string> SpecialityInfo { get; set; }

        public KeyValuePair<int, string> LevelQualification { get; set; }
    }
}
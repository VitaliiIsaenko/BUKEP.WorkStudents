using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class Teacher
    {
        public IList<int> IdsTeacher { get; set; }
        public string Fio { get; set; }
        public bool IsActiveSchedule { get; set; }
        public string Post { get; set; }
        public string Degree { get; set; }
    }
}
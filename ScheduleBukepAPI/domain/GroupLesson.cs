using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    public class GroupLesson
    {
        public DataValue TypeShedule { get; set; }
        public DataValue TypeWeek { get; set; }
        public DataValue Day { get; set; }
        public DataValue Lesson { get; set; }
        public DataValue TypeLesson { get; set; }
        public string DateLesson { get; set; }
        public TimeLesson TimeLesson { get; set; }

        public List<DataValue> Auditory { get; set; }
        public List<DataValue> Teachers { get; set; }
        public DataValue Discipline { get; set; }
    }

    public class TimeLesson
    {
        public string NameLessonTime { get; set; }
        public string StartLesson { get; set; }
        public string EndLesson { get; set; }
        public string Durability { get; set; }
    }

    //TODO: переименовать класс
    public class DataValue
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

}
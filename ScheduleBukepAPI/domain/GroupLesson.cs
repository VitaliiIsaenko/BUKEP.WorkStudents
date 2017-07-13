using System.Collections.Generic;

namespace ScheduleBukepAPI.domain
{
    //TODO: Конфликт имени класса и метода
    public class GroupLesson
    {
        public KeyValue TypeShedule { get; set; }
        public KeyValue TypeWeek { get; set; }
        public KeyValue Day { get; set; }
        public KeyValue Lesson { get; set; }
        public KeyValue TypeLesson { get; set; }
        public string DateLesson { get; set; }
        public TimeLesson TimeLesson { get; set; }

        public List<KeyValue> Auditory { get; set; }
        public List<KeyValue> Teachers { get; set; }
        public KeyValue Discipline { get; set; }
    }

    public class TimeLesson
    {
        public string NameLessonTime { get; set; }
        public string StartLesson { get; set; }
        public string EndLesson { get; set; }
        public string Durability { get; set; }
    }
}
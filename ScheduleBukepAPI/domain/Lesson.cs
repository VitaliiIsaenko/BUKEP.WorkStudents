using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleBukepAPI.domain
{
    public class Lesson
    {
        [JsonProperty("typeShedule")]
        public KeyValuePair<int, string> TypeShedule { get; set; }
        public KeyValuePair<int, string> TypeWeek { get; set; }
        public KeyValuePair<int, string> Day { get; set; }
        [JsonProperty("lesson")]
        public KeyValuePair<int, string> LessonInfo { get; set; }
        public KeyValuePair<int, string> TypeLesson { get; set; }
        public DateTime DateLesson { get; set; }
        public TimeLesson TimeLesson { get; set; }
        public List<KeyValuePair<int, string>> Auditory { get; set; }
        public List<KeyValuePair<int, string>> Teachers { get; set; }
        public KeyValuePair<int, string> Discipline { get; set; }
    }


    public class TimeLesson
    {
        public string NameLessonTime { get; set; }
        public DateTime StartLesson { get; set; }
        public DateTime EndLesson { get; set; }
        public DateTime Durability { get; set; }
    }
}
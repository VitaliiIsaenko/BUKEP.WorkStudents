using System;
using System.Collections.Generic;
using Java.Util;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler
{
    /// <summary>
    /// Используется для отображения дней на ScheduleActivity.
    /// </summary>
    public class LessonOnDay
    {
        private IList<GroupLesson> groupLessons;

        public LessonOnDay()
        {
        }

        public DateTime DateLesson { get; set; }

        public void AddLesson(GroupLesson groupLesson)
        {
            groupLessons.Add(groupLesson);
        }

        public static void Parse(IEnumerable<GroupLesson> groupLessons)
        {
            var lessonOnDays = new List<LessonOnDay>();
            foreach (var groupLesson in groupLessons)
            {
                if (DateTime.TryParse(groupLesson.DateLesson, out DateTime dateTime))
                {
                    foreach (var lessonOnDay in lessonOnDays)
                    {
                        if (lessonOnDay.DateLesson.Equals(dateTime))
                        {
                            lessonOnDay.AddLesson(groupLesson);
                        }
                        else
                        {
                            var newLessonOnDay = new LessonOnDay();
                            newLessonOnDay.AddLesson(groupLesson);
                            lessonOnDays.Add(newLessonOnDay);
                        }
                    }
                }
                throw new Exception("Failed parse dateLesson in DateTime.");
            }
        }
    }
}
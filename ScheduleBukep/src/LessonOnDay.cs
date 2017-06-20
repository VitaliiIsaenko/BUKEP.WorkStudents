using System;
using System.Collections.Generic;
using System.Globalization;
using Java.Util;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler
{
    /// <summary>
    /// Используется для группировки Lesson по дате.
    /// Группировка происходит по параметру DateLesson в Lesson.
    /// </summary>
    public class LessonOnDay
    {
        public LessonOnDay()
        {
            GroupLessons = new List<GroupLesson>();
        }

        public DateTime DateLesson { get; set; }
        public IList<GroupLesson> GroupLessons { get; }

        public static List<LessonOnDay> Parse(IList<GroupLesson> groupLessons)
        {
            var lessonOnDays = new List<LessonOnDay>();
            foreach (var groupLesson in groupLessons)
            {
                Console.WriteLine("Start iteration. DateLesson = " + groupLesson.DateLesson);
                var dateTimeLesson = ParseDateTime(groupLesson.DateLesson);

                var foundLessonOnDay = lessonOnDays.Find(
                    x => x.DateLesson.Equals(dateTimeLesson)
                );

                Console.WriteLine("FoundLessonOnDay = " + foundLessonOnDay);
                if (foundLessonOnDay == null)
                {
                    Console.WriteLine("Create new LessonOnDay " + dateTimeLesson);
                    foundLessonOnDay = new LessonOnDay() {DateLesson = dateTimeLesson};
                    lessonOnDays.Add(foundLessonOnDay);
                }
                Console.WriteLine("Add groupLesson in lessonOnDay");
                foundLessonOnDay.GroupLessons.Add(groupLesson);

                Console.WriteLine("End iteration.\n");
            }
            return lessonOnDays;
        }

        private static DateTime ParseDateTime(string dateLesson)
        {
            if (!DateTime.TryParse(dateLesson, out DateTime dateTimeLesson))
            {
                throw new Exception($"Failed parse DateTime. dateLesson = {dateLesson}");
            }
            return dateTimeLesson;
        }
    }
}
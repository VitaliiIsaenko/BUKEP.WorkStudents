using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            Lessons = new List<GroupLesson>();
        }

        public DateTime DateLesson { get; set; }
        public IList<GroupLesson> Lessons { get; }

        public static List<LessonOnDay> Parse(IList<GroupLesson> lessons)
        {
            var lessonOnDays = new List<LessonOnDay>();
            foreach (var lesson in lessons)
            {
                Console.WriteLine("Start iteration. DateLesson = " + lesson.DateLesson);
                var dateTimeLesson = ParseDateTime(lesson.DateLesson);

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
                foundLessonOnDay.Lessons.Add(lesson);

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
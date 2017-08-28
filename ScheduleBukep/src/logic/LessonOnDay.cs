using System;
using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Используется для группировки Lesson по дате.
    /// Группировка происходит по параметру DateLesson в Lesson.
    /// </summary>
    public class LessonOnDay
    {
        public LessonOnDay()
        {
            Lessons = new List<Lesson>();
        }

        public DateTime DateLesson { get; set; }
        public IList<Lesson> Lessons { get; }

        /// <summary>
        /// Сортирует Lessons по дате и помещает в GroupLesson.
        /// </summary>
        /// <param name="lessons">Уроки которые нужно спарсить в GroupLesson</param>
        /// <returns>Список LessonOnDay с отсортированными в них Lessons</returns>
        public static List<LessonOnDay> Parse(IList<Lesson> lessons)
        {
            var lessonOnDays = new List<LessonOnDay>();
            foreach (var lesson in lessons)
            {
                Console.WriteLine("Start iteration. DateLesson = " + lesson.DateLesson);
                var dateTimeLesson = lesson.DateLesson;

                var foundLessonOnDay = lessonOnDays.Find(
                    x => x.DateLesson.Equals(dateTimeLesson)
                );

                Console.WriteLine("FoundLessonOnDay = " + foundLessonOnDay);
                if (foundLessonOnDay == null)
                {
                    Console.WriteLine("Create new LessonOnDay " + dateTimeLesson);
                    foundLessonOnDay = new LessonOnDay {DateLesson = dateTimeLesson};
                    lessonOnDays.Add(foundLessonOnDay);
                }
                Console.WriteLine("Add groupLesson in lessonOnDay");
                foundLessonOnDay.Lessons.Add(lesson);

                Console.WriteLine("End iteration.\n");
            }
            return lessonOnDays;
        }
    }
}
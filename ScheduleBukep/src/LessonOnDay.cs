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
            Lessons = new List<Lesson>();
        }

        public DateTime DateLesson { get; set; }
        public IList<Lesson> Lessons { get; private set; }

        /// <summary>
        /// Объединение уроков с одинаковым именем дисцеплины.
        /// </summary>
        public void CombineLesson()
        {
            if (!Lessons.Any()) {return;}

            var result = new List<Lesson>();
            foreach (var lesson in new List<Lesson>(Lessons))
            {
                Lessons.Remove(lesson);

                Console.WriteLine("Lesson = " + lesson.NameDiscipline);
                if (result.Any(x => x.NameDiscipline == lesson.NameDiscipline))
                {
                    Console.WriteLine("continue\n");
                    continue;
                }

                CombineInLesson(lesson, FindDoubleAtDiscipline(lesson));
                Console.WriteLine("Add lesson in result\n");
                result.Add(lesson);
            }
            Lessons = result;
            Console.WriteLine("Result count = " + Lessons.Count);
        }

        /// <summary>
        /// Используется для соединения списка уроков в один. 
        /// </summary>
        /// <param name="lessonMain">Урок в который будут соединены остальные уроки</param>
        /// <param name="lessons"></param>
        private static void CombineInLesson(Lesson lessonMain, IEnumerable<Lesson> lessons)
        {
            
            foreach (var lesson in lessons)
            {
                //TODO: use StringBuilder
                lessonMain.FioTeacher += ", " + lesson.FioTeacher;
                if (lessonMain.NameAuditory != lesson.NameAuditory)
                {
                    lessonMain.NameAuditory += ", " + lesson.NameAuditory;
                }
            }
        }

        private IEnumerable<Lesson> FindDoubleAtDiscipline(Lesson lesson)
        {
            return Lessons.Where(
                x => x.NameDiscipline.Equals(lesson.NameDiscipline)
            );
        }

        public static List<LessonOnDay> Parse(IList<Lesson> lessons)
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
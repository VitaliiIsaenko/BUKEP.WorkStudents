using System;
using System.Collections.Generic;
using Android.Util;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.controllers
{
    public class ScheduleForTeacher : Schedule
    {
        private const string Tag = "ScheduleForTeacher";
        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyTeacherJson = "TeacherJson";

        public ScheduleForTeacher(ScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            Teacher teacher = GetTeacherFromeIntent();

            var lessons = RequestSchedules(
                teacher,
                GetDateTimeFromeIntent(IntentKeyDateLessonStart),
                GetDateTimeFromeIntent(IntentKeyDateLessonEnd)
            );

            var lessonOnDays = LessonOnDay.Parse(lessons);

            View.ShowLessonOnDay(lessonOnDays);
            //View.SetGroopName(group.NameGroup);
            View.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        private Teacher GetTeacherFromeIntent()
        {
            var jsonTeacher = GetJsonFromeIntent(IntentKeyTeacherJson);
            Log.Info(Tag, "jsonTeacher = " + jsonTeacher);
            var teacher = JsonConvert.ConvertTo<Teacher>(jsonTeacher);
            return teacher;
        }

        //TODO: такой есть в ScheduleForStudent
        private IList<Lesson> RequestSchedules(Teacher teacher, DateTime dateLessonStart, DateTime dateLessonEnd)
        {
            var lessons = DataProvider.GetTeacherLessons(teacher.IdsTeacher, dateLessonStart, dateLessonEnd);
            return lessons;
        }
    }
}
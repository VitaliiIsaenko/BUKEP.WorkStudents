using System;
using System.Collections.Generic;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.controllers
{
    public class ScheduleForTeacher : Schedule
    {
        private const string Tag = "ScheduleForTeacher";
        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyTeacherJson = "TeacherJson";
        public const string UserDataTeacherJson = "UserDataTeacherJson";

        private Teacher _teacher;

        private Teacher Teacher => _teacher ?? 
            (_teacher = GetObjectFromeIntent<Teacher>(IntentKeyTeacherJson));

        public ScheduleForTeacher(ScheduleActivity view) : base(view)
        {
        }

        protected override IList<Lesson> GetLessons()
        {
            var lessons = DataProvider.GetTeacherLessons(
                Teacher.IdsTeacher,
                GetDateTimeFromeIntent(IntentKey.DateLessonStart),
                GetDateTimeFromeIntent(IntentKey.DateLessonEnd)
            );
            return lessons;
        }

        protected override void SaveScheduleInFavorites()
        {
            CacheHelper.PutUserData(UserDataTeacherJson, Teacher);
        }

        protected override void DeleteScheduleInFavorites()
        {
            CacheHelper.DeleteUserData(UserDataTeacherJson);
        }
    }
}
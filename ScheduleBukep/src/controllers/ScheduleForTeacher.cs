using System.Collections.Generic;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
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
            (_teacher = intent.GetObject<Teacher>(IntentKeyTeacherJson));

        public ScheduleForTeacher(ScheduleActivity view) : base(view)
        {
        }

        protected override IList<Lesson> GetLessons()
        {
            var lessons = DataProvider.GetTeacherLessons(
                Teacher.IdsTeacher,
                intent.GetDateTime(IntentKey.DateLessonStart.ToString()),
                intent.GetDateTime(IntentKey.DateLessonEnd.ToString())
            );
            return lessons;
        }

        protected override void SaveScheduleInFavorites()
        {
            string key = SelectFavoritesGroup.UserDataKey.FavoritesTeacher.ToString();
            CacheHelper.PutUserData(key, Teacher);
        }

        protected override void DeleteScheduleInFavorites()
        {
            string key = SelectFavoritesGroup.UserDataKey.FavoritesTeacher.ToString();
            CacheHelper.DeleteUserData(key);
        }
    }
}
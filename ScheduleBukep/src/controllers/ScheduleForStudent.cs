using System;
using System.Collections.Generic;
using Android.Util;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    public class ScheduleForStudent : Schedule
    {
        private const string Tag = "ScheduleForStudent";

        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyGroupsJson = "GroupJson";

        public ScheduleForStudent(ScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            Group group = GetGropeFromeIntent();

            var lessons = RequestSchedules(
                group,
                GetDateTimeFromeIntent(IntentKeyDateLessonStart),
                GetDateTimeFromeIntent(IntentKeyDateLessonEnd)
            );
            var lessonOnDays = LessonOnDay.Parse(lessons);

            View.ShowLessonOnDay(lessonOnDays);
            View.SetGroopName(group.NameGroup);
            View.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        private Group GetGropeFromeIntent()
        {
            var jsonGroup = GetJsonFromeIntent(IntentKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = JsonConvert.ConvertTo<Group>(jsonGroup);
            return group;
        }

        /// <summary>
        /// Выполнить запрос на получения списка уроков в указанный интервал времени.
        /// </summary>
        /// <param name="group">Группа для которой нужно получить список уроков</param>
        /// <param name="dateLessonStart">Начало интервала</param>
        /// <param name="dateLessonEnd">Конец интервала</param>
        /// <returns>Список уроков в указанный интервал времени</returns>
        private IList<Lesson> RequestSchedules(Group group, DateTime dateLessonStart, DateTime dateLessonEnd)
        {
            var lessons = DataProvider.GetGroupLessons(group.IdsSchedulGroup, dateLessonStart, dateLessonEnd);
            return lessons;
        }
    }
}
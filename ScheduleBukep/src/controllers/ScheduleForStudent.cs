using System;
using System.Collections.Generic;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    public class ScheduleForStudent : Schedule
    {
        private const string Tag = "ScheduleForStudent";

        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyGroupsJson = "GroupJson";

        public const string UserDataGroupsJson = "UserDataGroupJson";

        private Group _group;

        private Group Group => _group ?? (_group = GetObjectFromeIntent<Group>(IntentKeyGroupsJson));

        public ScheduleForStudent(ScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            base.Update();
            view.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        protected override IList<Lesson> GetLessons()
        {
            view.SetGroopName(Group.NameGroup);
            var lessons = DataProvider.GetGroupLessons(
                Group.IdsSchedulGroup,
                GetDateTimeFromeIntent(IntentKey.DateLessonStart),
                GetDateTimeFromeIntent(IntentKey.DateLessonEnd)
            );
            return lessons;
        }

        protected override void SaveScheduleInFavorites()
        {
            CacheHelper.PutUserData(UserDataGroupsJson, Group);
        }

        protected override void DeleteScheduleInFavorites()
        {
            CacheHelper.DeleteUserData(UserDataGroupsJson);
        }
    }
}
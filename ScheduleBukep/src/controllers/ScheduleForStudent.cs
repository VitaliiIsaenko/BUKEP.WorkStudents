using System;
using System.Collections.Generic;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
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

        private Group Group => _group 
            ?? (_group = intent.GetObject<Group>(IntentKeyGroupsJson));

        public ScheduleForStudent(ScheduleActivity view) : base(view)
        {
            ChangeImageForFavorites(Favorites.ExistGroup(Group));
        }

        public override void Update()
        {
            base.Update();
            view.SetTodayForToolbar(DateTime.Today.ToString(ToolbarDateFormat));
        }

        protected override IList<Lesson> GetLessons()
        {
            view.SetGroopName(Group.Info);
            var lessons = DataProvider.GetGroupLessons(
                Group.Ids,
                intent.GetDateTime(IntentKey.DateLessonStart.ToString()),
                intent.GetDateTime(IntentKey.DateLessonEnd.ToString())
            );
            return lessons;
        }

        protected override void SaveScheduleInFavorites()
        {
            Favorites.AddGroup(Group);
        }

        protected override void DeleteScheduleInFavorites()
        {
            Favorites.DeleteGroup(Group);
        }
    }
}
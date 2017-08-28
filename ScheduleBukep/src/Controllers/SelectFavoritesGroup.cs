using System;
using System.Collections.Generic;
using System.Linq;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.controllers
{
    internal class SelectFavoritesGroup : SelectItem
    {
        public SelectFavoritesGroup(SelectItemActivity activity) : base(activity)
        {
        }

        /// <summary>
        /// Разделы в избранном.
        /// </summary>
        private enum FavoritesSections
        {
            Group,
            Teather
        }

        private static string FavoritesSectionsConvertInString(FavoritesSections sections)
        {
            switch (sections)
            {
                case FavoritesSections.Group:
                    return "Группы";
                case FavoritesSections.Teather:
                    return "Преподаватели";
            }
            throw new ArgumentOutOfRangeException(
                "Failed select FavoritesSections. FavoritesSections = " + sections);
        }

        public override void Update()
        {
            var items = Enum.GetValues(typeof(FavoritesSections)).Cast<FavoritesSections>();
            InitSelect(items, ShowSelectSections, FavoritesSectionsConvertInString);
        }

        private void ShowSelectSections(FavoritesSections sections)
        {
            switch (sections)
            {
                case FavoritesSections.Group:
                    List<Group> groups = Favorites.GetGroups();
                    InitSelect(groups, ShowScheduleFavoritesGroup, group => group.Info);
                    return;
                case FavoritesSections.Teather:
                    List<Teacher> teachers = Favorites.GetTeachers();
                    InitSelect(teachers, ShowScheduleFavoritesTeacher, teacher => teacher.Fio);
                    return;
            }
            throw new ArgumentOutOfRangeException(
                "Failed select FavoritesSections. FavoritesSections = " + sections);
        }

        private void ShowScheduleFavoritesTeacher(Teacher teacher)
        {
            ScheduleActivity.StartScheduleActivity(_view, teacher);
        }

        private void ShowScheduleFavoritesGroup(Group group)
        {
            ScheduleActivity.StartScheduleActivity(_view, group);
        }
    }
}
﻿using System;
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
        public SelectFavoritesGroup(SelectItemActivity view) : base(view)
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
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип избранного. FavoritesSections = " + sections);
            }
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
                    InitSelect(groups, ShowScheduleFavoritesGroup, group => group.Info[0].Group.Value + group.TypeShedule.Value);
                    break;
                case FavoritesSections.Teather:
                    List<Teacher> teachers = Favorites.GetTeachers();
                    InitSelect(teachers, ShowScheduleFavoritesTeacher, teacher => teacher.Fio);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип избранного. FavoritesSections = " + sections);
            }
        }

        private void ShowScheduleFavoritesTeacher(Teacher teacher)
        {
            ScheduleActivity.StartScheduleActivity(View, teacher);
        }

        private void ShowScheduleFavoritesGroup(Group group)
        {
            ScheduleActivity.StartScheduleActivity(View, group);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.controllers
{
    //TODO: rename in SelectFavorites
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
            var selectOption = new SelectOption<FavoritesSections>();
            selectOption.SetItems(items)
                .SetConvertInString(FavoritesSectionsConvertInString)
                .SetOnClickItem(ShowSelectSections);
            View.ShowSelectItem(selectOption);
        }

        private void ShowSelectSections(FavoritesSections sections)
        {
            switch (sections)
            {
                case FavoritesSections.Group:
                    ShowFavoritesGroup();
                    break;
                case FavoritesSections.Teather:
                    ShowFavoritesTeather();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Не удалось выбрать тип избранного. FavoritesSections = " + sections);
            }
        }

        private void ShowFavoritesTeather()
        {
            List<Teacher> teachers = Favorites.GetTeachers();
            var selectOption = new SelectOption<Teacher>();
            selectOption
                .SetItems(teachers)
                .SetOnClickItem(ShowScheduleFavoritesTeacher)
                .SetConvertInString(teacher => teacher.Fio);
            
            View.ShowSelectItem(selectOption);
        }

        private void ShowFavoritesGroup()
        {
            List<Group> groups = Favorites.GetGroups();

            var selectOption = new SelectOption<Group>();
            selectOption.SetItems(groups)
                .SetOnClickItem(ShowScheduleFavoritesGroup)
                .SetConvertInString(group => group.GetName());
            View.ShowSelectItem(selectOption);
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
using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.controllers
{
    internal class SelectFavoritesGroup : SelectItem
    {
        public enum UserDataKey
        {
            FavoritesGroup,
            FavoritesTeacher
        }

        public SelectFavoritesGroup(SelectItemActivity activity) : base(activity)
        {
        }

        public override void Update()
        {
            Teacher userData = CacheHelper.GetUserData<Teacher>(
                UserDataKey.FavoritesTeacher.ToString());
            var groups = new List<Teacher> { userData };

            InitChoice(groups, ShowScheduleFavoritesGroup, teacher => teacher.Fio);
        }

        private void ShowScheduleFavoritesGroup(Teacher teacher)
        {
            Toast.MakeText(_view,$"Show Teacher Fio = {teacher.Fio}", ToastLength.Long).Show();
        }
    }
}
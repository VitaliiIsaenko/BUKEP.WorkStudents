using System;
using Android.Content;
using Android.Widget;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    public class Menu : Controller
    {
        private const string Tag = "Menu";
        private readonly MenuActivity _view;

        public Menu(MenuActivity view) : base(view)
        {
            _view = view;
        }

        public override void Update()
        {
        }

        public void ClickScheduleForStudent(object sender, EventArgs ea)
        {
            StartActivitySchedule(SelectItemType.SelectScheduleStudent);
        }

        public void ClickScheduleForTeacher(object sender, EventArgs e)
        {
            StartActivitySchedule(SelectItemType.SelectScheduleTeacher);
        }

        public void ClickScheduleBell(object sender, EventArgs e)
        {
            CacheHelper.ClearAll();
            Toast.MakeText(_view,"Clear all cache",ToastLength.Short).Show();
        }

        public void ClickScheduleFavorite(object sender, EventArgs e)
        {
            StartActivitySchedule(SelectItemType.SelectFavoritesGroup);
        }

        private void StartActivitySchedule(SelectItemType selectItemType)
        {
            SelectItemActivity.StartSelectItemActivity(_view,selectItemType);
        }
    }
}
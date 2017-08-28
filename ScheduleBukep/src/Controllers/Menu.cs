using System;
using Android.Widget;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    public class Menu : Controller<MenuActivity>
    {
        private const string Tag = "Menu";

        public Menu(MenuActivity view) : base(view)
        {
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
            
        }

        public void ClickScheduleFavorite(object sender, EventArgs e)
        {
            StartActivitySchedule(SelectItemType.SelectFavoritesGroup);
        }

        public void Click—lear—ache(object sender, EventArgs e)
        {
            CacheHelper.ClearAll();
            Toast.MakeText(View, "Clear all cache", ToastLength.Short).Show();
        }

        private void StartActivitySchedule(SelectItemType selectItemType)
        {
            SelectItemActivity.StartSelectItemActivity(View, selectItemType);
        }
    }
}
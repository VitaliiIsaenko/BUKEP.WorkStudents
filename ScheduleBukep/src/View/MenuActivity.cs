using System;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler.View
{
    [Activity(MainLauncher = true)]
    public class MenuActivity : NavigationActivity
    {
        private const string Tag = "MenuActivity";
        private readonly Menu _menu;

        public MenuActivity()
        {
            _menu = new Menu(this);
        }

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuLayout);

            var scheduleGroup = FindViewById<Button>(Resource.Id.schedule_group);
            scheduleGroup.Click += _menu.ClickScheduleGroup;

            var scheduleTeacher = FindViewById<Button>(Resource.Id.schedule_teacher);
            scheduleTeacher.Click += _menu.ClickScheduleTeacher;

            var scheduleBell = FindViewById<Button>(Resource.Id.schedule_bell);
            scheduleBell.Click += _menu.ClickScheduleBell;

            InitNavigationView();
        }
    }
}
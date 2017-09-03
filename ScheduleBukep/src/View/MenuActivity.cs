using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Compat;
using Android.Widget;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler.View
{
    [Activity(MainLauncher = true)]
    public class MenuActivity : BaseActivity
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
            scheduleGroup.Click += _menu.ClickScheduleForStudent;

            var scheduleTeacher = FindViewById<Button>(Resource.Id.schedule_teacher);
            scheduleTeacher.Click += _menu.ClickScheduleForTeacher;

            var scheduleBell = FindViewById<Button>(Resource.Id.schedule_bell);
            scheduleBell.Click += _menu.ClickScheduleBell;

            var scheduleFavorite = FindViewById<Button>(Resource.Id.schedule_favorite);
            scheduleFavorite.Click += _menu.ClickScheduleFavorite;

            var clearCache = FindViewById<Button>(Resource.Id.clear_сache);
            clearCache.Click += _menu.ClickСlearСache;

            Context context = ApplicationContext;

            TextView versionName = FindViewById<TextView>(Resource.Id.version_name);
            versionName.Text = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;

        }
    }
}
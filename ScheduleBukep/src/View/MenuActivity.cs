using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Bukep.Sheduler.View
{
    [Activity(MainLauncher = true)]
    public class MenuActivity : NavigationActivity
    {
        private const string Tag = "MenuActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuActivity);

            var scheduleGroup = FindViewById<Button>(Resource.Id.schedule_group);
            scheduleGroup.Click += ClickScheduleGroup;

            var scheduleTeacher = FindViewById<Button>(Resource.Id.schedule_teacher);
            scheduleTeacher.Click += ClickScheduleTeacher;

            var scheduleBell = FindViewById<Button>(Resource.Id.schedule_bell);
            scheduleBell.Click += ClickScheduleBell;

            InitNavigationView();
        }

        public void ClickScheduleGroup(object sender, EventArgs ea)
        {
            var intent = new Intent(this, typeof(IdentifyScheduleActivity));
            StartActivity(intent);
        }

        public void ClickScheduleTeacher(object sender, EventArgs ea)
        {
            Toast.MakeText(this, "ClickScheduleTeacher", ToastLength.Long).Show();
        }

        public void ClickScheduleBell(object sender, EventArgs ea)
        {
            Toast.MakeText(this, "ClickScheduleBell", ToastLength.Long).Show();
        }
    }
}
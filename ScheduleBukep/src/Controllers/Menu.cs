using System;
using System.Net;
using Android.Content;
using Android.Util;
using Bukep.Sheduler.View;

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
            StartActivitySchedule(SchedulesType.ForStudent);
        }

        public void ClickScheduleForTeacher(object sender, EventArgs e)
        {
            StartActivitySchedule(SchedulesType.ForTeacher);
        }

        private void StartActivitySchedule(SchedulesType schedulesType)
        {
            var intent = new Intent(_view, typeof(IdentifyScheduleActivity));
            intent.PutExtra(IdentifySchedule.IntentKeyDateSchedulesType, (int) schedulesType);
            _view.StartActivity(intent);
        }

        public void ClickScheduleBell(object sender, EventArgs e)
        {
           
        }
    }
}
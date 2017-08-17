using System;
using System.Net;
using Android.Content;
using Android.Util;
using Bukep.Sheduler.logic;
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
            StartActivitySchedule(SelectItemType.SelectScheduleStudent);
        }

        public void ClickScheduleForTeacher(object sender, EventArgs e)
        {
            StartActivitySchedule(SelectItemType.SelectScheduleTeacher);
        }

        private void StartActivitySchedule(SelectItemType selectItemType)
        {
            var intent = new Intent(_view, typeof(SelectItemActivity));
            intent.PutExtra(SelectItem.IntentKeyDateSelectItemType, (int) selectItemType);
            _view.StartActivity(intent);
        }

        public void ClickScheduleBell(object sender, EventArgs e)
        {
           
        }
    }
}
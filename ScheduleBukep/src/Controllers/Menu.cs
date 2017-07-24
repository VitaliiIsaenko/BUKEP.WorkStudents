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

        public void ClickScheduleGroup(object sender, EventArgs ea)
        {
            var intent = new Intent(_view, typeof(IdentifyScheduleActivity));
            _view.StartActivity(intent);
        }

        public void ClickScheduleTeacher(object sender, EventArgs e)
        {
            var intent = new Intent(_view, typeof(IdentifyScheduleActivity));
            _view.StartActivity(intent);
        }

        public void ClickScheduleBell(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
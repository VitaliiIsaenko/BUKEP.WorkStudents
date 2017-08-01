using System;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    internal abstract class IdentifySchedule : Controller
    {
        protected readonly IdentifyScheduleActivity _view;
        public const string IntentKeyDateSchedulesType = "SchedulesType";

        protected IdentifySchedule(IdentifyScheduleActivity activity) : base(activity)
        {
            _view = activity;
        }

        public override void Update()
        {
            _view.SetButtoneShowClickable(false);
            //TODO: make ShowSchedulesButtone private, add setter for ClickeButtoneShow
            _view.ShowSchedulesButtone.Click += ClickeButtoneShow;
        }

        protected abstract void ClickeButtoneShow(object sender, EventArgs e);
    }
}
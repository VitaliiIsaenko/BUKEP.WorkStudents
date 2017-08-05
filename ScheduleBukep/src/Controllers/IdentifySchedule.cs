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
    }
}
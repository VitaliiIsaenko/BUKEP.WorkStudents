using System;
using Android.Util;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Schedule : Controller
    {
        private const string Tag = "Schedule";

        protected const string ToolbarDateFormat = "ddd, dd MMM";

        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyDateLessonStart = "DateLessonStart";
        public const string IntentKeyDateLessonEnd = "DateLessonEnd";

        public readonly ScheduleActivity View;

        public SchedulesManager SchedulesManager { get; set; }

        protected Schedule(ScheduleActivity view) : base(view)
        {
            View = view;
            var periodNames = View.Resources.GetStringArray(Resource.Array.schedules_periods);
            SchedulesManager = new SchedulesManager(periodNames, this);
        }


        protected DateTime GetDateTimeFromeIntent(string key)
        {
            return DateTime.Parse(GetJsonFromeIntent(key));
        }

        protected string GetJsonFromeIntent(string key)
        {
            var json = View.Intent.GetStringExtra(key);
            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed get json " + key + " from Intent");
            Log.Debug(Tag, "GetJsonFromeIntent() json = " + json);
            return json;
        }
    }
}
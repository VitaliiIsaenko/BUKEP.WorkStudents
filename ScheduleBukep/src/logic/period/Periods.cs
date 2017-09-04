using System;
using Android.Util;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;

namespace Bukep.Sheduler.logic.period
{
    /// <summary>
    /// Используется для работы с периодами расписания (На день, Три дня, Неделю)
    /// </summary>
    public class Periods
    {
        private readonly Schedule _schedule;
        private readonly string[] _periodNames;
        private readonly ScheduleActivity _view;
        private const string Tag = "SchedulesManager";

        public Periods(ScheduleActivity view, Schedule schedule)
        {
            _view = view;
            _periodNames = view.Resources.GetStringArray(Resource.Array.schedules_periods);
            _schedule = schedule;
        }

        /// <summary>
        /// Устанавливает период в один день.
        /// </summary>
        public void SelectPeriodOneDay()
        {
            Log.Debug(Tag, "ChoosePeriodOneDay()");
            var startAndEndPeriod = DateTime.Today;

            SavePeriod(startAndEndPeriod, startAndEndPeriod);

            ChangePeriod(_periodNames[0]);
        }

        /// <summary>
        /// Устанавливает период в три деня.
        /// </summary>
        public void SelectPeriodThreeDay()
        {
            Log.Debug(Tag, "ChoosePeriodThreeDay()");

            var startPeriod = DateTime.Today;
            var endPeriod = startPeriod.AddDays(2);

            SavePeriod(startPeriod, endPeriod);

            Log.Debug(Tag, $"today = {startPeriod} threeDayFuture = {endPeriod}");

            ChangePeriod(_periodNames[1]);
        }

        /// <summary>
        /// Устанавливает период в неделю.
        /// </summary>
        public void SelectPeriodWeek()
        {
            Log.Debug(Tag, "ChoosePeriodWeek()");
            DateTime startPeriod = GetStartWeek(DateTime.Today);
            DateTime endPeriod = startPeriod.AddDays(5);

            SavePeriod(startPeriod, endPeriod);

            Log.Debug(Tag, $"ChoosePeriodWeek() monday = {startPeriod} saturday = {endPeriod}");

            var periodsName = _periodNames[2];

            ChangePeriod(periodsName);
        }

        public void SavePeriod(DateTime startPeriod, DateTime endPeriod)
        {
            if (_view.IsShowNextWeek())
            {
                startPeriod = startPeriod.AddDays(7);
                endPeriod = endPeriod.AddDays(7); 
            }
            PutExtraData(Schedule.IntentKey.DateLessonStart, startPeriod);
            PutExtraData(Schedule.IntentKey.DateLessonEnd, endPeriod);
        }

        private void ChangePeriod(string periodsName)
        {
            _view.SetPeriodName(periodsName);
        }

        /// <summary>
        /// Возвращает день с которого начинается текущая неделя.  
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static DateTime GetStartWeek(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        private void PutExtraData(Schedule.IntentKey intentKey, DateTime dateTime)
        {
            _view.Intent.PutExtra(
                intentKey.ToString(),
                dateTime.ToString(Api.DateTimeFormat)
            );
        }
    }
}
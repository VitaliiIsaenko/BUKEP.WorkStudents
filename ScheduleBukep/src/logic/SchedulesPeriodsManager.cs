using System;
using Android.Util;
using Bukep.Sheduler.Controllers;
using ScheduleBukepAPI;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Содержит логику работы с периодами расписание (На день, Три дня, Неделю)
    /// </summary>
    public class SchedulesManager
    {
        private readonly Schedule _schedule;
        private readonly string[] _periodNames;
        private const string Tag = "SchedulesManager";

        public SchedulesManager(string[] periodNames, Schedule schedule)
        {
            _periodNames = periodNames;
            _schedule = schedule;
        }

        public void ChoosePeriodOneDay()
        {
            Log.Debug(Tag, "ChoosePeriodOneDay()");
            var today = DateTime.Today;
            PutExtraData(Schedule.IntentKeyDateLessonStart, today);
            PutExtraData(Schedule.IntentKeyDateLessonEnd, today);
            var periodsName = _periodNames[0];
            _schedule.View.SetPeriodForToolbar(periodsName);
            _schedule.Update();
        }

        public void ChoosePeriodThreeDay()
        {
            Log.Debug(Tag, "ChoosePeriodThreeDay()");
            var today = DateTime.Today;
            PutExtraData(Schedule.IntentKeyDateLessonStart, today);

            var threeDayFuture = today.AddDays(2);
            PutExtraData(Schedule.IntentKeyDateLessonEnd, threeDayFuture);

            Log.Debug(Tag, $"today = {today} threeDayFuture = {threeDayFuture}");
            var periodsName = _periodNames[1];
            _schedule.View.SetPeriodForToolbar(periodsName);
            _schedule.Update();
        }

        public void ChoosePeriodWeek()
        {
            Log.Debug(Tag, "ChoosePeriodWeek()");
            var monday = GetStartWeek(DateTime.Today);
            PutExtraData(Schedule.IntentKeyDateLessonStart, monday);

            var saturday = monday.AddDays(5);
            PutExtraData(Schedule.IntentKeyDateLessonEnd, saturday);

            Log.Debug(Tag, $"monday = {monday} saturday = {saturday}");

            var periodsName = _periodNames[2];
            _schedule.View.SetPeriodForToolbar(periodsName);
            _schedule.Update();
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

        private void PutExtraData(string key, DateTime dateTime)
        {
            _schedule.View.Intent.PutExtra(
                key,
                dateTime.ToString(Api.DateTimeFormat)
            );
        }
    }
}
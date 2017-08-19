using System;
using Android.Util;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;

namespace Bukep.Sheduler.logic
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
        /// Устанавливает период в один день и обновляет активити
        /// </summary>
        public void SelectPeriodOneDay()
        {
            Log.Debug(Tag, "ChoosePeriodOneDay()");
            var startAndEndPeriod = DateTime.Today;

            PutExtraData(Schedule.IntentKey.DateLessonStart, startAndEndPeriod);
            PutExtraData(Schedule.IntentKey.DateLessonEnd, startAndEndPeriod);

            ChangePeriod(_periodNames[0]);
        }

        /// <summary>
        /// Устанавливает период в три деня и обновляет активити
        /// </summary>
        public void SelectPeriodThreeDay()
        {
            Log.Debug(Tag, "ChoosePeriodThreeDay()");

            var startPeriod = DateTime.Today;
            var endPeriod = startPeriod.AddDays(2);

            PutExtraData(Schedule.IntentKey.DateLessonStart, startPeriod);
            PutExtraData(Schedule.IntentKey.DateLessonEnd, endPeriod);

            Log.Debug(Tag, $"today = {startPeriod} threeDayFuture = {endPeriod}");

            ChangePeriod(_periodNames[1]);
        }

        /// <summary>
        /// Устанавливает период в неделю и обновляет активити
        /// </summary>
        public void SelectPeriodWeek()
        {
            Log.Debug(Tag, "ChoosePeriodWeek()");
            var startPeriod = GetStartWeek(DateTime.Today);
            var endPeriod = startPeriod.AddDays(5);

            PutExtraData(Schedule.IntentKey.DateLessonStart, startPeriod);
            PutExtraData(Schedule.IntentKey.DateLessonEnd, endPeriod);

            Log.Debug(Tag, $"ChoosePeriodWeek() monday = {startPeriod} saturday = {endPeriod}");

            var periodsName = _periodNames[2];

            ChangePeriod(periodsName);
        }

        private void ChangePeriod(string periodsName)
        {
            _view.SetPeriodForToolbar(periodsName);
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

        private void PutExtraData(Schedule.IntentKey intentKey, DateTime dateTime)
        {
            _view.Intent.PutExtra(
                intentKey.ToString(),
                dateTime.ToString(Api.DateTimeFormat)
            );
        }
    }
}
﻿using System;
using System.Collections.Generic;
using Android.Util;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    public class Schedule : Controller
    {
        private const string Tag = "Schedule";
        private readonly ScheduleActivity _view;

        /// <summary>
        /// Используется для получения данных из Intent.
        /// </summary>
        public const string IntentKeyGroupsJson = "GroupJson";

        public const string IntentKeyDateLessonStart = "DateLessonStart";
        public const string IntentKeyDateLessonEnd = "DateLessonEnd";

        public Schedule(ScheduleActivity view)
        {
            _view = view;
        }

        public override void Update()
        {
            try
            {
                var lessons = RequestSchedules(
                    GetGropeFromeIntent(),
                    GetJsonFromeIntent(IntentKeyDateLessonStart),
                    GetJsonFromeIntent(IntentKeyDateLessonEnd)
                );
                _view.ShowLessonOnDay(LessonOnDay.Parse(lessons));
            }
            catch (Exception e)
            {
                Log.Error(Tag, e.Message, e);
                //TODO: delete this
                _view.CloseIfHappenedExeption = false;
                _view.ShowError(e.Message);
            }
        }

        private IList<Lesson> RequestSchedules(Group group, string dateLessonStart, string dateLessonEnd)
        {
            var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
            var lessons = FacadeApi
                .GetGroupLessons(ids, dateLessonStart, dateLessonEnd);
            return lessons;
        }

        private Group GetGropeFromeIntent()
        {
            var jsonGroup = GetJsonFromeIntent(IntentKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = JsonConvert.ConvertTo<Group>(jsonGroup);
            return group;
        }

        private string GetJsonFromeIntent(string key)
        {
            var json = _view.Intent.GetStringExtra(key);
            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed get json " + key + " from Intent");
            Log.Debug(Tag, "GetJsonFromeIntent() json = " + json);
            return json;
        }

        public void ChoosePeriodOneDay()
        {
            Log.Debug(Tag, "ChoosePeriodOneDay()");
            var today = DateTime.Today;
            PutExtraData(IntentKeyDateLessonStart, today);
            PutExtraData(IntentKeyDateLessonEnd, today);
            Update();
        }



        public void ChoosePeriodThreeDay()
        {
            Log.Debug(Tag, "ChoosePeriodThreeDay()");
            var today = DateTime.Today;
            PutExtraData(IntentKeyDateLessonStart, today);

            var threeDayFuture = today.AddDays(3);
            PutExtraData(IntentKeyDateLessonEnd, threeDayFuture);

            Log.Debug(Tag, $"today = {today} threeDayFuture = {threeDayFuture}");

            Update();
        }

        public void ChoosePeriodWeek()
        {
            Log.Debug(Tag, "ChoosePeriodWeek()");
            var monday = GetStartWeek(DateTime.Today);
            PutExtraData(IntentKeyDateLessonStart, monday);

            var saturday = monday.AddDays(5);
            PutExtraData(IntentKeyDateLessonEnd, saturday);

            Log.Debug(Tag, $"monday = {monday} saturday = {saturday}");

            Update();
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
            _view.Intent.PutExtra(
                key,
                dateTime.ToString(FacadeApi.DateTimeFormat)
            );
        }
    }
}
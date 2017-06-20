using System;
using System.Collections.Generic;
using Android.Content;
using Android.Nfc;
using Android.Util;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

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
                //TODO: Вынести в отдельный метод.
                var group = GetGrope();
                var dateLessonStart = GetJsonFromeIntent(IntentKeyDateLessonStart);
                var dateLessonEnd = GetJsonFromeIntent(IntentKeyDateLessonEnd);
                //=========================================================

                //TODO: Вынести в отдельный метод.
                var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
                var groupLessons = FacadeApi
                    .GetGroupLessons(ids, dateLessonStart, dateLessonEnd);
                //=========================================================

                _view.ShowGroupLesson(groupLessons);
            }
            catch (Exception e)
            {
                Log.Error(Tag, e.Message, e);
                //TODO: delete this
                _view.CloseIfHappenedExeption = false;
                _view.ShowError(e.Message);
            }
        }

        private DateTime GetDateFromeIntent(string keyDateTime)
        {
            var jsonDate = GetJsonFromeIntent(keyDateTime);
            var dateTime = ConvertToDateTime(jsonDate);
            Log.Debug(Tag, "GetDateFromeIntent() dateTime = " + dateTime);
            return dateTime;
        }

        private Group GetGrope()
        {
            var jsonGroup = GetJsonFromeIntent(IntentKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = JsonConvert.ConvertTo<Group>(jsonGroup);
            return group;
        }

        private static DateTime ConvertToDateTime(string json)
        {
            if (DateTime.TryParse(json, out DateTime result))
            {
                return result;
            }
            throw new Exception("Failed convert DateTime frome json. json = " + json);
        }

        private string GetJsonFromeIntent(string key)
        {
            var json = _view.Intent.GetStringExtra(key);
            if (string.IsNullOrEmpty(json))
                throw new Exception("Failed get json " + key + " from Intent");
            Log.Debug(Tag, "GetJsonFromeIntent() json = " + json);
            return json;
        }

        //TODO: создать и вынести в класс DataHelper
        /// <summary>
        /// Конвентирует DateTime в string с форматом для api.
        /// </summary>
        /// <returns></returns>
        private static string ConvertToString(DateTime dateTime)
        {
            var todayString = dateTime.ToString("yyyy-MM-dd");
            return todayString;
        }
    }
}
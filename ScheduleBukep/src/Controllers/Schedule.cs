using System;
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
                var group = GetGropeFromeIntent();
                var dateLessonStart = GetJsonFromeIntent(IntentKeyDateLessonStart);
                var dateLessonEnd = GetJsonFromeIntent(IntentKeyDateLessonEnd);

                var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
                var lessons = FacadeApi
                    .GetGroupLessons(ids, dateLessonStart, dateLessonEnd);

                var lessonOnDays = LessonOnDay.Parse(lessons);

                _view.ShowLessonOnDay(lessonOnDays);
            }
            catch (Exception e)
            {
                Log.Error(Tag, e.Message, e);
                //TODO: delete this
                _view.CloseIfHappenedExeption = false;
                _view.ShowError(e.Message);
            }
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
    }
}
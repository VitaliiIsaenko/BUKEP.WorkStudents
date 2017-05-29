using System;
using Android.Content;
using Android.Nfc;
using Android.Util;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class Schedule : IController
    {
        private readonly ScheduleActivity _view;
        private readonly FacadeApi _facadeApi = new FacadeApi();
        private readonly JsonConvert _jsonConvert = new JsonConvert();

        public const string DataKeyGroupsJson = "GroupJson";
        private const string Tag = "Schedule";

        public Schedule(ScheduleActivity view)
        {
            _view = view;
        }

        public void Update()
        {
            var group = GetGropeFromeIntent();
            var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
            var todayString = DataToday();
            var groupLessons =
                _facadeApi.GetGroupLessons(ids, todayString, todayString);

            _view.ShowGroupLesson(groupLessons);
        }

        private Group GetGropeFromeIntent()
        {
            var jsonGroup = _view.Intent.GetStringExtra(DataKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = _jsonConvert.ConvertTo<Group>(jsonGroup);
            return group;
        }

        private static string DataToday()
        {
            var today = DateTime.Today;
            var todayString = today.ToString("yyyy-MM-dd");
            return todayString;
        }
    }
}
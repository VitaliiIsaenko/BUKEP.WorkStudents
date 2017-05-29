using System;
using Android.Content;
using Android.Nfc;
using Android.Util;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class Schedule : Controller
    {
        private readonly ScheduleActivity _view;


        public const string DataKeyGroupsJson = "GroupJson";
        private const string Tag = "Schedule";

        public Schedule(ScheduleActivity view)
        {
            _view = view;
        }

        public override void Update()
        {
            var group = GetGropeFromeIntent();
            var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
            var todayString = DataToday();
            var groupLessons =
                FacadeApi.GetGroupLessons(ids, todayString, todayString);

            _view.ShowGroupLesson(groupLessons);
        }

        private Group GetGropeFromeIntent()
        {
            var jsonGroup = _view.Intent.GetStringExtra(DataKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = JsonConvert.ConvertTo<Group>(jsonGroup);
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
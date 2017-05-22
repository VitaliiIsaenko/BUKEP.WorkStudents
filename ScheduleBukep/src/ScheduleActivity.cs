using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using Android.Util;
using Android.Views;
using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;

namespace Bukep.Sheduler.src
{
    [Activity(Label = "ScheduleActivity")]
    public class ScheduleActivity : Activity
    {
        private const string TAG = "ScheduleActivity";
        public const string DataKeyGroupsJson = "GroupJson";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            Group group = GetGropeFromeIntent();
            Toast.MakeText(this, "Group = " + group.NameGroup, ToastLength.Short).Show();

            string ids = FacadeAPI.ConvertIdsToString(group.IdsSchedulGroup);
            List<GroupLesson> groupLessons = 
                FacadeAPI.GetGroupLessons(ids, "2017-05-23", "2017-05-23");

            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.liner_layout);
            linearLayout.RemoveAllViews();

            foreach (var item in groupLessons)
            {
                linearLayout.AddView(CreateCardLesson(item));
            }
        }

        private View CreateCardLesson(GroupLesson lesson)
        {
            CardView card = (CardView)LayoutInflater.Inflate(Resource.Layout.Card_lesson_viwe, null, false);
            TextView nameLesson = card.FindViewById<TextView>(Resource.Id.nameLesson);
            nameLesson.Text = lesson.NameDiscipline;
            return card;
        }

        private Group GetGropeFromeIntent()
        {
            string jsonGroup = Intent.GetStringExtra(DataKeyGroupsJson);
            Log.Info(TAG, "jsonGroup = " + jsonGroup);
            Group group = JSONConvert.ConvertJSONToDTO<Group>(jsonGroup);
            return group;
        }
    }
}
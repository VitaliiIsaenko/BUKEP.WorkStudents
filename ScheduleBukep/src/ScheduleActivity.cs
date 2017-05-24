using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler
{
    [Activity(Label = "ScheduleActivity")]
    public class ScheduleActivity : Activity
    {
        private const string Tag = "ScheduleActivity";
        public const string DataKeyGroupsJson = "GroupJson";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleLayout);

            var group = GetGropeFromeIntent();
            Toast.MakeText(this, "Group = " + group.NameGroup, ToastLength.Short).Show();

            var ids = FacadeApi.ConvertIdsToString(group.IdsSchedulGroup);
            var groupLessons =
                FacadeApi.GetGroupLessons(ids, "2017-05-23", "2017-05-23");

            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.liner_layout);
            linearLayout.RemoveAllViews();

            foreach (var item in groupLessons)
            {
                linearLayout.AddView(CreateCardLesson(item));
            }
        }

        private View CreateCardLesson(GroupLesson lesson)
        {
            CardView card = (CardView) LayoutInflater.Inflate(Resource.Layout.Card_lesson_viwe, null, false);
            TextView nameLesson = card.FindViewById<TextView>(Resource.Id.nameLesson);
            nameLesson.Text = lesson.NameDiscipline;
            return card;
        }

        private Group GetGropeFromeIntent()
        {
            var jsonGroup = Intent.GetStringExtra(DataKeyGroupsJson);
            Log.Info(Tag, "jsonGroup = " + jsonGroup);
            var group = JsonConvert.ConvertTo<Group>(jsonGroup);
            return group;
        }
    }
}
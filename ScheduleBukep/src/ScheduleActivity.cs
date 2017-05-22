using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using Android.Util;

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

            string jsonGroup = Intent.GetStringExtra(DataKeyGroupsJson);
            Log.Info(TAG, "jsonGroup = " + jsonGroup);
            Group group = JSONConvert.ConvertJSONToDTO<Group>(jsonGroup);
            Toast.MakeText(this, "Group = "+group.NameGroup, ToastLength.Short).Show();
        }
    }
}
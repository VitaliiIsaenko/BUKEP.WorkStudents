using Android.App;
using Android.Widget;
using Android.OS;

namespace ScheduleBukep
{
    [Activity(Label = "ScheduleBukep", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
        }
    }
}


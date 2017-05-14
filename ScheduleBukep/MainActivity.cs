using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace ScheduleBukep
{
    [Activity(Label = "ScheduleBukep", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            string[] data = { "Item_1", "Item_2", "Item_3", "Item_4" };

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, data);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinner.Adapter = adapter;
        }
    }
}


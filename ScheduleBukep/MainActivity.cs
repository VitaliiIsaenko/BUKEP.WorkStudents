using Android.App;
using Android.Widget;
using Android.OS;
using System;
using ScheduleBukepAPI;
using ScheduleBukepAPI.apiDTO;
using System.Collections.Generic;

namespace ScheduleBukep
{
    [Activity(Label = "ScheduleBukep", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            List<string> dataForSpinnerFaculty = GetDataForSpinnerFaculty();

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, dataForSpinnerFaculty);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapter;
        }

        private static List<string> GetDataForSpinnerFaculty()
        {
            FacadeAPI api = new FacadeAPI();
            List<Faculty> faculties = api.getFaculties("2016", "1000");

            List<string> dataForSpinnerFaculty = new List<string>();

            foreach (var faculty in faculties)
            {
                dataForSpinnerFaculty.Add(faculty.name);
            }

            return dataForSpinnerFaculty;
        }
    }
}


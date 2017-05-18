using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;
using Android;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler
{
    /// <summary>
    /// Данное Activity используется как форма идентификации для студентов.
    /// Предоставляет пошаговый доступ к расписанию, состоящий из шагов:
    /// -   выбор факультета
    /// -	выбор специальности
    /// -	выбор курса
    /// -	выбор группы
    /// После выполнение всех шагов появляется кнопка «показать».
    /// Нажатие на эту кнопку открывает расписание по заданным параметрам.
    /// </summary>
    [Activity(Label = "ScheduleBukep", MainLauncher = true, Icon = "@drawable/icon")]
    public class IdentifyScheduleActivity : Activity
    {
        private IdentifySchedule identifySchedule;
        private ArrayAdapter<Faculty> adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            identifySchedule = new IdentifySchedule(this);
            identifySchedule.Update();
        }

        internal void ShowFaculty(List<Faculty> faculties)
        {
            adapter = new ArrayAdapter<Faculty>(this, Android.Resource.Layout.SimpleSpinnerItem, faculties);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapter;
            spinnerFaculty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SelectSpinnerFaculty);
        }

        private void SelectSpinnerFaculty(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Faculty faculty = adapter.GetItem(e.Position);
            Toast.MakeText(this, "Slelect = "+faculty, ToastLength.Long).Show();
        }
    }
}


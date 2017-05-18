using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;
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
        private const string TAG = "IdentifyScheduleActivity";
        private IdentifySchedule controller;
        private ArrayAdapter<Faculty> adapterFaculty;
        private ArrayAdapter<Specialty> adapterSpecialty;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            controller = new IdentifySchedule(this);
            controller.Update();
        }

        internal void ShowSpecialtys(List<Specialty> specialtys)
        {
            adapterSpecialty = new ArrayAdapter<Specialty>(
                this, Android.Resource.Layout.SimpleSpinnerItem, specialtys);
            adapterSpecialty.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerSpecialtys = FindViewById<Spinner>(Resource.Id.spinnerSpecialty);
            spinnerSpecialtys.Adapter = adapterSpecialty;
            spinnerSpecialtys.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(
                SelectSpinnerSpecialtys);
        }

        private void SelectSpinnerSpecialtys(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Specialty specialty = adapterSpecialty.GetItem(e.Position);
            Toast.MakeText(this, "Specialtys = " + specialty, ToastLength.Short).Show();
            controller.SelectSpecialt(specialty);
        }

        internal void ShowFaculty(List<Faculty> faculties)
        {
            adapterFaculty = new ArrayAdapter<Faculty>(this, Android.Resource.Layout.SimpleSpinnerItem, faculties);
            adapterFaculty.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapterFaculty;
            spinnerFaculty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SelectSpinnerFaculty);
        }

        private void SelectSpinnerFaculty(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Faculty faculty = adapterFaculty.GetItem(e.Position);
            Toast.MakeText(this, "Faculty = " + faculty, ToastLength.Short).Show();
            controller.SelectFaculty(faculty);
        }
    }
}


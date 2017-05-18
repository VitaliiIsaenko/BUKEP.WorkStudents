using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;
using Bukep.Sheduler.Controllers;
using Android.Util;

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
        private ArrayAdapter<Courses> adapterCourses;
        private ArrayAdapter<Group>  adapterGroup;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            controller = new IdentifySchedule(this);
            controller.Update();
        }

        internal void ShowGroup(List<Group> groups)
        {
            adapterGroup = new ArrayAdapter<Group>(
                this,
                Android.Resource.Layout.SimpleSpinnerItem,
                groups
                );
            adapterSpecialty.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerGroup = FindViewById<Spinner>(Resource.Id.spinnerGroup);
            spinnerGroup.Adapter = adapterGroup;
            spinnerGroup.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(
                SelectSpinnerGroup);
        }

        private void SelectSpinnerGroup(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Group group = adapterGroup.GetItem(e.Position);
            Log.Info(TAG,"Group = " + group);
            controller.SelectGroup(group);
        }

        internal void ShowCourses(List<Courses> courses)
        {
            adapterCourses = new ArrayAdapter<Courses>(
                this,
                Android.Resource.Layout.SimpleSpinnerItem,
                courses
                );
            adapterSpecialty.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spinnerCourse = FindViewById<Spinner>(Resource.Id.spinnerCourse);
            spinnerCourse.Adapter = adapterCourses;
            spinnerCourse.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(
                SelectSpinnerCourses);
        }

        private void SelectSpinnerCourses(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Courses cours = adapterCourses.GetItem(e.Position);
           Log.Info(TAG,"Courses = " + cours);
            controller.SelectCourses(cours);
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
           Log.Info(TAG,"Specialtys = " + specialty);
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
           Log.Info(TAG,"Faculty = " + faculty);
            controller.SelectFaculty(faculty);
        }
    }
}


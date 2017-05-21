﻿using Android.App;
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
        private DTOAdapter<Faculty> adapterFaculty;
        private DTOAdapter<Specialty> adapterSpecialty;
        private DTOAdapter<Courses> adapterCourses;
        private DTOAdapter<Group>  adapterGroup;

        private Button buttoneShow;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            buttoneShow = FindViewById<Button>(Resource.Id.buttoneShow);
            buttoneShow.Click += new EventHandler(OnClickeButtoneShow);

            controller = new IdentifySchedule(this);
            controller.Update();
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            buttoneShow.Clickable = clickable;
        }

        private void OnClickeButtoneShow(object sender, EventArgs e)
        {
            controller.ClickeButtoneShow();
        }

        public void ShowGroup(List<Group> groups)
        {
            adapterGroup = new GroupAdapter<Group>(groups, this);

            Spinner spinnerGroup = FindViewById<Spinner>(Resource.Id.spinnerGroup);
            spinnerGroup.Adapter = adapterGroup;
            spinnerGroup.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(
                SelectSpinnerGroup);
        }

        private void SelectSpinnerGroup(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int posotion = e.Position;
            if (posotion == 0) return;
            Spinner spinner = (Spinner)sender;
            Group group = adapterGroup.GetObject(posotion);
            Log.Info(TAG,"Group = " + group.NameGroup);
            controller.SelectGroup(group);
        }

        public void ShowCourses(List<Courses> courses)
        {
            adapterCourses = new CoursesAdapter<Courses>(courses, this);

            Spinner spinnerCourse = FindViewById<Spinner>(Resource.Id.spinnerCourse);
            spinnerCourse.Adapter = adapterCourses;
            spinnerCourse.ItemSelected +=
                new EventHandler<AdapterView.ItemSelectedEventArgs>(SelectSpinnerCourses);
        }

        private void SelectSpinnerCourses(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int posotion = e.Position;
            if (posotion == 0) return;
            Spinner spinner = (Spinner)sender;
            Courses cours = adapterCourses.GetObject(posotion);
            Log.Info(TAG,"Courses = " + cours.NameCourse);
            controller.SelectCourses(cours);
        }

        public void ShowSpecialtys(List<Specialty> specialtys)
        {
            adapterSpecialty = new SpecialtyAdapter<Specialty>(specialtys, this);

            Spinner spinnerSpecialtys = FindViewById<Spinner>(Resource.Id.spinnerSpecialty);
            spinnerSpecialtys.Adapter = adapterSpecialty;
            spinnerSpecialtys.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(
                SelectSpinnerSpecialtys);
        }

        private void SelectSpinnerSpecialtys(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int posotion = e.Position;
            if (posotion == 0) return;
            Spinner spinner = (Spinner)sender;
            Specialty specialty = adapterSpecialty.GetObject(posotion);
           Log.Info(TAG,"Specialtys = " + specialty.NameSpeciality);
            controller.SelectSpecialt(specialty);
        }

        public void ShowFaculty(List<Faculty> faculties)
        {

            adapterFaculty = new FacultyAdapter<Faculty>(faculties, this);

            Spinner spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = adapterFaculty;
            spinnerFaculty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SelectSpinnerFaculty);
        }

        private void SelectSpinnerFaculty(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int posotion = e.Position;
            if (posotion == 0) return;
            Spinner spinner = (Spinner)sender;
            Faculty faculty = adapterFaculty.GetObject(posotion);
            Log.Info(TAG,"Faculty = " + faculty.Name);
            controller.SelectFaculty(faculty);
        }
    }
}

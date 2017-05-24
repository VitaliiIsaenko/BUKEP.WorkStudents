using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using Bukep.Sheduler.Controllers;
using Android.Util;
using ScheduleBukepAPI.domain;

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
        private const string Tag = "IdentifyScheduleActivity";
        private IdentifySchedule _controller;
        private DtoAdapter<Faculty> _adapterFaculty;
        private DtoAdapter<Specialty> _adapterSpecialty;
        private DtoAdapter<Courses> _adapterCourses;
        private DtoAdapter<Group> _adapterGroup;

        private Button _buttoneShow;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            _buttoneShow = FindViewById<Button>(Resource.Id.buttoneShow);
            _buttoneShow.Click += OnClickeButtoneShow;

            _controller = new IdentifySchedule(this);
            _controller.Update();
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            _buttoneShow.Clickable = clickable;
        }

        private void OnClickeButtoneShow(object sender, EventArgs e)
        {
            _controller.ClickeButtoneShow();
        }

        public void ShowGroup(IList<Group> groups)
        {
            _adapterGroup = new GroupAdapter(groups, this);

            var spinnerGroup = FindViewById<Spinner>(Resource.Id.spinnerGroup);
            spinnerGroup.Adapter = _adapterGroup;
            spinnerGroup.ItemSelected += SelectSpinnerGroup;
        }

        private void SelectSpinnerGroup(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var group = _adapterGroup.GetObject(posotion);
            Log.Info(Tag, "Group = " + group.NameGroup);
            _controller.SelectGroup(group);
        }

        public void ShowCourses(IList<Courses> courses)
        {
            _adapterCourses = new CoursesAdapter(courses, this);

            var spinnerCourse = FindViewById<Spinner>(Resource.Id.spinnerCourse);
            spinnerCourse.Adapter = _adapterCourses;
            spinnerCourse.ItemSelected += SelectSpinnerCourses;
        }

        private void SelectSpinnerCourses(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var cours = _adapterCourses.GetObject(posotion);
            Log.Info(Tag, "Courses = " + cours.NameCourse);
            _controller.SelectCourses(cours);
        }

        public void ShowSpecialtys(IList<Specialty> specialtys)
        {
            _adapterSpecialty = new SpecialtyAdapter(specialtys, this);

            var spinnerSpecialtys = FindViewById<Spinner>(Resource.Id.spinnerSpecialty);
            spinnerSpecialtys.Adapter = _adapterSpecialty;
            spinnerSpecialtys.ItemSelected += SelectSpinnerSpecialtys;
        }

        private void SelectSpinnerSpecialtys(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var specialty = _adapterSpecialty.GetObject(posotion);
            Log.Info(Tag, "Specialtys = " + specialty.NameSpeciality);
            _controller.SelectSpecialt(specialty);
        }

        public void ShowFaculty(IList<Faculty> faculties)
        {
            _adapterFaculty = new FacultyAdapter(faculties, this);

            var spinnerFaculty = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            spinnerFaculty.Adapter = _adapterFaculty;
            spinnerFaculty.ItemSelected += SelectSpinnerFaculty;
        }

        private void SelectSpinnerFaculty(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var faculty = _adapterFaculty.GetObject(posotion);
            Log.Info(Tag, "Faculty = " + faculty.Name);
            _controller.SelectFaculty(faculty);
        }
    }
}
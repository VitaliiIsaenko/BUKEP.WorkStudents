using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.View
{
    /// <summary>
    ///     Данное Activity используется как форма идентификации для студентов.
    ///     Предоставляет пошаговый доступ к расписанию, состоящий из шагов:
    ///     -   выбор факультета
    ///     -	выбор специальности
    ///     -	выбор курса
    ///     -	выбор группы
    ///     После выполнение всех шагов появляется кнопка «показать».
    ///     Нажатие на эту кнопку открывает расписание по заданным параметрам.
    /// </summary>
    [Activity(Icon = "@drawable/icon")]
    public class IdentifyScheduleActivity : NavigationActivity
    {
        private const string Tag = "IdentifyScheduleActivity";
        private DtoAdapter<Course> _coursesAdapter;
        private DtoAdapter<Faculty> _facultyAdapter;
        private DtoAdapter<Group> _groupAdapter;
        private DtoAdapter<Specialty> _specialtyAdapter;

        private Button _showSchedulesButtone;

        private IdentifySchedule _controller;

        private Spinner _specialtysSpinner;
        private Spinner _courseSpinner;
        private Spinner _groupSpinner;
        private Spinner _facultySpinner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IdentifyScheduleLayout);

            InitSpinner();
            InitShowButtone();
            InitController();
            InitNavigationView();
        }

        private void InitController()
        {
            _controller = new IdentifySchedule(this);
            _controller.Update();
        }

        private void InitShowButtone()
        {
            _showSchedulesButtone = FindViewById<Button>(Resource.Id.buttoneShow);
            _showSchedulesButtone.Click += OnClickeShowSchedulesButtone;
        }

        private void InitSpinner()
        {
            _facultySpinner = FindViewById<Spinner>(Resource.Id.spinnerFaculty);
            _specialtysSpinner = FindViewById<Spinner>(Resource.Id.spinnerSpecialty);
            _courseSpinner = FindViewById<Spinner>(Resource.Id.spinnerCourse);
            _groupSpinner = FindViewById<Spinner>(Resource.Id.spinnerGroup);
        }

        public void SetButtoneShowClickable(bool clickable)
        {
            _showSchedulesButtone.Clickable = clickable;
        }

        private void OnClickeShowSchedulesButtone(object sender, EventArgs e)
        {
            _controller.ClickeButtoneShow();
        }

        public void ShowGroup(IList<Group> groups)
        {
            _groupAdapter = new DtoAdapter<Group>(groups, this,x => x.NameGroup );

            _groupSpinner.Adapter = _groupAdapter;
            _groupSpinner.ItemSelected += SelectGroupSpinner;
        }

        private void SelectGroupSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var group = _groupAdapter.GetObject(posotion);
            Log.Info(Tag, "Group = " + group.NameGroup);
            _controller.SelectGroup(group);
        }

        public void ShowCourses(IList<Course> courses)
        {
            _coursesAdapter = new DtoAdapter<Course>(
                courses,
                this,
                x => x.Info.Value);

            
            _courseSpinner.Adapter = _coursesAdapter;
            _courseSpinner.ItemSelected += SelectCourseSpinner;
        }

        private void SelectCourseSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var cours = _coursesAdapter.GetObject(posotion);
            Log.Info(Tag, "Courses = " + cours.Info.Value);
            _controller.SelectCourses(cours);
        }

        public void ShowSpecialtys(IList<Specialty> specialtys)
        {
            _specialtyAdapter = new DtoAdapter<Specialty>(specialtys, this,
                x => x.Info.Value);

            _specialtysSpinner.Adapter = _specialtyAdapter;
            _specialtysSpinner.ItemSelected += SelectSpecialtysSpinner;
        }

        private void SelectSpecialtysSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var specialty = _specialtyAdapter.GetObject(posotion);
            Log.Info(Tag, "Specialtys = " + specialty.Info.Value);
            _controller.SelectSpecialt(specialty);
        }

        public void ShowFaculty(IList<Faculty> faculties)
        {
            _facultyAdapter = new DtoAdapter<Faculty>(
                faculties,
                this,
                x => x.Info.Value);
            _facultySpinner.Adapter = _facultyAdapter;
            _facultySpinner.ItemSelected += SelectFacultySpinner;
        }

        private void SelectFacultySpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            var faculty = _facultyAdapter.GetObject(posotion);
            Log.Info(Tag, "Faculty = " + faculty.Info.Value);
            _controller.SelectFaculty(faculty);
        }

        public void FacultySpinnerEnabled(bool enabled)
        {
            _facultySpinner.Enabled = enabled;
        }

        public void SpecialtysSpinnerEnabled(bool enabled)
        {
            _specialtysSpinner.Enabled = enabled;
        }

        public void CourseSpinnerEnabled(bool enabled)
        {
            _courseSpinner.Enabled = enabled;
        }

        public void GroupSpinnerEnabled(bool enabled)
        {
            _groupSpinner.Enabled = enabled;
        }
    }
}
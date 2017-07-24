using System;
using System.Collections.Generic;
using Android.Content;
using Bukep.Sheduler.View;
using Java.Interop;
using Java.Lang;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifySchedule : Controller
    {
        private readonly IdentifyScheduleActivity _view;

        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Course _selectedCourse;
        private Group _selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view) : base(view)
        {
            _view = view;
        }

        public override void Update()
        {
            _view.SetButtoneShowClickable(false);
            IList<Faculty> faculties = GetFaculties();

            _view.Show(faculties, faculty => faculty.Info.Value, SelectFaculty);
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            IList<Specialty> specialties = GetSpecialtys(faculty.Info.Key);

            _view.Show(specialties, specialty => specialty.Info.Value, SelectSpecialt);
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );

            _view.Show(courses, course => course.Info.Value, SelectCourses);
        }

        public void SelectCourses(Course cours)
        {
            _selectedCourse = cours;
            IList<Group> groups = GetGroups(
                _selectedFaculty.Info.Key,
                _selectedCourse.Info.Key,
                _selectedSpecialty.Info.Key
            );

            _view.Show(groups, group => $"{group.NameGroup} {group.NameTypeShedule}" , SelectGroup);
        }

        public void SelectGroup(Group group)
        {
            _selectedGroup = group;
            _view.SetButtoneShowClickable(true);
        }

        internal void ClickeButtoneShow(object sender, EventArgs e)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonGroup = ConvertToJson(_selectedGroup);
            intent.PutExtra(Schedule.IntentKeyGroupsJson, jsonGroup);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);

            _view.StartActivity(intent);
        }
    }
}
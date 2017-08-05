using System;
using System.Collections.Generic;
using Android.Content;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class IdentifyScheduleStudent : IdentifySchedule
    {
        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Course _selectedCourse;

        public IdentifyScheduleStudent(IdentifyScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            ItemChoiceFaculty();
        }

        private void ItemChoiceFaculty()
        {
            ItemAdapter<Faculty> adapterFaculty = new ItemAdapter<Faculty>(
                DataProvider.GetFaculties(),
                faculty => faculty.Info.Value
            );
            _view.ChoiceItem(adapterFaculty, InitChoiceSpecialty);
        }

        private void InitChoiceSpecialty(Faculty faculty)
        {
            _selectedFaculty = faculty;

            ItemAdapter<Specialty> adapterSpecialty = new ItemAdapter<Specialty>(
                DataProvider.GetSpecialtys(faculty.Info.Key),
                specialty => specialty.Info.Value
            );
            _view.ChoiceItem(adapterSpecialty, InitChoiceCourse);
        }

        private void InitChoiceCourse(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = DataProvider.GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );

            ItemAdapter<Course> adapterCourse = new ItemAdapter<Course>(
                courses,
                course => course.Info.Value
            );

            _view.ChoiceItem(adapterCourse, InitChoiceGroup);
        }

        private void InitChoiceGroup(Course course)
        {
            _selectedCourse = course;
            IList<Group> groups = DataProvider.GetGroups(
                _selectedFaculty.Info.Key,
                _selectedCourse.Info.Key,
                _selectedSpecialty.Info.Key
            );

            ItemAdapter<Group> adapterGroup = new ItemAdapter<Group>(
                groups,
                group => $"{group.NameGroup} {group.NameTypeSchedule}"
            );

            _view.ChoiceItem(adapterGroup, ClickeButtoneShow);
        }

        protected void ClickeButtoneShow(Group group)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonGroup = JsonConvert.ConvertToJson(group);
            intent.PutExtra(ScheduleForStudent.IntentKeyGroupsJson, jsonGroup);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);
            intent.PutExtra(IntentKeyDateSchedulesType, (int)SchedulesType.ForStudent);

            _view.StartActivity(intent);
        }
    }
}
using System;
using System.Collections.Generic;
using Android.Content;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.logic.extension;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal class SelectScheduleStudent : SelectItem
    {
        private Faculty _selectedFaculty;
        private Specialty _selectedSpecialty;
        private Course _selectedCourse;

        public SelectScheduleStudent(SelectItemActivity view) : base(view)
        {
        }

        public override void Update()
        {
            ItemChoiceFaculty();
        }

        private void ItemChoiceFaculty()
        {
            InitChoice(
                DataProvider.GetFaculties(),
                InitChoiceSpecialty,
                faculty => faculty.Info.Value);
        }

        private void InitChoiceSpecialty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            InitChoice(
                DataProvider.GetSpecialtys(faculty.Info.Key),
                InitChoiceCourse,
                specialty => specialty.Info.Value);
        }

        private void InitChoiceCourse(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = DataProvider.GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );

            InitChoice(
                courses,
                InitChoiceGroup,
                course => course.Info.Value);
        }

        private void InitChoiceGroup(Course course)
        {
            _selectedCourse = course;
            IList<Group> groups = DataProvider.GetGroups(
                _selectedFaculty.Info.Key,
                _selectedCourse.Info.Key,
                _selectedSpecialty.Info.Key
            );

            InitChoice(
                groups,
                StartScheduleActivity,
                group => $"{group.NameGroup} {group.NameTypeSchedule}");
        }

        //TODO: такой же метод в SelectScheduleTeacher
        protected void StartScheduleActivity(Group group)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));

            intent.PutObject(ScheduleForStudent.IntentKeyGroupsJson, group);
            intent.PutDateTime(Schedule.IntentKey.DateLessonStart.ToString(), DateTime.Today);
            intent.PutDateTime(Schedule.IntentKey.DateLessonEnd.ToString(), DateTime.Today);
            intent.PutObject(IntentKeyDateSelectItemType, SelectItemType.SelectScheduleStudent);

            _view.StartActivity(intent);
        }
    }
}
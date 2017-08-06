using System;
using System.Collections.Generic;
using Android.Content;
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
                ClickeButtoneShow,
                group => $"{group.NameGroup} {group.NameTypeSchedule}");
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
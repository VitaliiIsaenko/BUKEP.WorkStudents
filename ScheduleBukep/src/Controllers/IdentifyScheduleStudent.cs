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
        private Group _selectedGroup;
        private ItemAdapter<Group> _itemAdapterGroup;
        private ItemAdapter<Course> _itemAdapterCourse;
        private ItemAdapter<Specialty> _itemAdapterSpecialty;
        private ItemAdapter<Faculty> _itemAdapterFaculty;

        public IdentifyScheduleStudent(IdentifyScheduleActivity view) : base(view)
        {
        }

        public override void Update()
        {
            base.Update();
            IList<Faculty> faculties = DataProvider.GetFaculties();

            ItemChoiceFaculty();
            _itemAdapterFaculty.Items = faculties;

            InitChoiceSpecialty();
            InitChoiceCourse();
            InitChoiceGroup();
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            IList<Specialty> specialties = DataProvider.GetSpecialtys(faculty.Info.Key);

            _itemAdapterSpecialty.Items = specialties;
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = DataProvider.GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );

            _itemAdapterCourse.Items = courses;
        }

        public void SelectCourses(Course cours)
        {
            _selectedCourse = cours;
            IList<Group> groups = DataProvider.GetGroups(
                _selectedFaculty.Info.Key,
                _selectedCourse.Info.Key,
                _selectedSpecialty.Info.Key
            );

            _itemAdapterGroup.Items = groups;
        }

        public void SelectGroup(Group group)
        {
            _selectedGroup = group;
            _view.SetButtoneShowClickable(true);
        }

        protected override void ClickeButtoneShow(object sender, EventArgs e)
        {
            var intent = new Intent(_view, typeof(ScheduleActivity));
            var jsonGroup = JsonConvert.ConvertToJson(_selectedGroup);
            intent.PutExtra(ScheduleForStudent.IntentKeyGroupsJson, jsonGroup);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);
            intent.PutExtra(IntentKeyDateSchedulesType, (int) SchedulesType.ForStudent);

            _view.StartActivity(intent);
        }

        private void ItemChoiceFaculty()
        {
            _itemAdapterFaculty = new ItemAdapter<Faculty>(_view,
                faculty => faculty.Info.Value
            );

            ItemChoice<Faculty> itemChoice = new ItemChoice<Faculty>(
                _itemAdapterFaculty, SelectFaculty, _view);
            _view.ShowItems(itemChoice);
        }

        private void InitChoiceSpecialty()
        {
            _itemAdapterSpecialty = new ItemAdapter<Specialty>(_view,
                specialty => specialty.Info.Value
            );

            ItemChoice<Specialty> itemChoice = new ItemChoice<Specialty>(
                _itemAdapterSpecialty, SelectSpecialt, _view);
            _view.ShowItems(itemChoice);
        }

        private void InitChoiceCourse()
        {
            _itemAdapterCourse = new ItemAdapter<Course>(_view,
                course => course.Info.Value
            );

            ItemChoice<Course> itemChoice = new ItemChoice<Course>(
                _itemAdapterCourse, SelectCourses, _view);
            _view.ShowItems(itemChoice);
        }

        private void InitChoiceGroup()
        {
            _itemAdapterGroup = new ItemAdapter<Group>(_view,
                group => $"{@group.NameGroup} {@group.NameTypeSchedule}"
            );

            ItemChoice<Group> itemChoice = new ItemChoice<Group>(
                _itemAdapterGroup, SelectGroup, _view);
            _view.ShowItems(itemChoice);
        }
    }
}
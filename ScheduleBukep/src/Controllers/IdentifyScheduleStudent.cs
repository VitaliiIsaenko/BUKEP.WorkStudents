using System;
using System.Collections.Generic;
using Android.Content;
using Bukep.Sheduler.View;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;

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
            IList<Faculty> faculties = _dateShedules.GetFaculties();

            ItemChoiceFaculty();
            _itemAdapterFaculty.Items = faculties;

            InitChoiceSpecialty();
            InitChoiceCourse();
            InitChoiceGroup();
        }

        public void SelectFaculty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            IList<Specialty> specialties = _dateShedules.GetSpecialtys(faculty.Info.Key);

            _itemAdapterSpecialty.Items = specialties;
        }

        public void SelectSpecialt(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = _dateShedules.GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );

            _itemAdapterCourse.Items = courses;
        }

        public void SelectCourses(Course cours)
        {
            _selectedCourse = cours;
            IList<Group> groups = _dateShedules.GetGroups(
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
            var jsonGroup = _dateShedules.ConvertToJson(_selectedGroup);
            intent.PutExtra(Schedule.IntentKeyGroupsJson, jsonGroup);

            var today = DateTime.Today.ToString(Api.DateTimeFormat);
            intent.PutExtra(Schedule.IntentKeyDateLessonStart, today);
            intent.PutExtra(Schedule.IntentKeyDateLessonEnd, today);

            _view.StartActivity(intent);
        }

        private void ItemChoiceFaculty()
        {
            _itemAdapterFaculty = new ItemAdapter<Faculty>(_view,
                faculty => faculty.Info.Value
            );

            ChoiceItem<Faculty> choiceItem = new ChoiceItem<Faculty>(
                _itemAdapterFaculty, SelectFaculty, _view);
            _view.ShowItems(choiceItem);
        }

        private void InitChoiceSpecialty()
        {
            _itemAdapterSpecialty = new ItemAdapter<Specialty>(_view,
                specialty => specialty.Info.Value
            );

            ChoiceItem<Specialty> choiceItem = new ChoiceItem<Specialty>(
                _itemAdapterSpecialty, SelectSpecialt, _view);
            _view.ShowItems(choiceItem);
        }

        private void InitChoiceCourse()
        {
            _itemAdapterCourse = new ItemAdapter<Course>(_view,
                course => course.Info.Value
            );

            ChoiceItem<Course> choiceItem = new ChoiceItem<Course>(
                _itemAdapterCourse, SelectCourses, _view);
            _view.ShowItems(choiceItem);
        }

        private void InitChoiceGroup()
        {
            _itemAdapterGroup = new ItemAdapter<Group>(_view,
                group => $"{@group.NameGroup} {@group.NameTypeShedule}"
            );

            ChoiceItem<Group> choiceItem = new ChoiceItem<Group>(
                _itemAdapterGroup, SelectGroup, _view);
            _view.ShowItems(choiceItem);
        }
    }
}
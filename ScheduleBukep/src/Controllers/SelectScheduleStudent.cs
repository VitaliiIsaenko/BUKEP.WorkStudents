﻿using System.Collections.Generic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

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
            IList<Faculty> faculties = DataProvider.GetFaculties();
            var selectOption = new SelectOption<Faculty>();
            
            selectOption
                .SetItems(faculties)
                .SetOnClickItem(InitChoiceSpecialty)
                .SetConvertInString(faculty => faculty.Info.Value);
            
            View.ShowSelectItem(selectOption);
        }

        private void InitChoiceSpecialty(Faculty faculty)
        {
            _selectedFaculty = faculty;
            IList<Specialty> specialties = DataProvider.GetSpecialtys(faculty.Info.Key);
            
            var selectOption = new SelectOption<Specialty>();
            
            selectOption
                .SetItems(specialties)
                .SetOnClickItem(InitChoiceCourse)
                .SetConvertInString(specialty => specialty.Info.Value);
            
            View.ShowSelectItem(selectOption);
        }

        private void InitChoiceCourse(Specialty specialty)
        {
            _selectedSpecialty = specialty;
            IList<Course> courses = DataProvider.GetCourses(
                _selectedFaculty.Info.Key,
                specialty.Info.Key
            );
            
            var selectOption = new SelectOption<Course>();
            
            selectOption
                .SetItems(courses)
                .SetOnClickItem(InitChoiceGroup)
                .SetConvertInString(course => course.Info.Value);
            
            View.ShowSelectItem(selectOption);
        }

        private void InitChoiceGroup(Course course)
        {
            _selectedCourse = course;
            IList<Group> groups = DataProvider.GetGroups(
                _selectedFaculty.Info.Key,
                _selectedCourse.Info.Key,
                _selectedSpecialty.Info.Key
            );

            var selectOption = new SelectOption<Group>();
            
            selectOption
                .SetItems(groups)
                .SetOnClickItem(StartScheduleActivity)
                .SetConvertInString(group => group.GetName());
            
            View.ShowSelectItem(selectOption);
        }

        protected void StartScheduleActivity(Group group)
        {
            ScheduleActivity.StartScheduleActivity(View, group);
        }
    }
}
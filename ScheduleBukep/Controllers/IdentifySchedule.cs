using System;
using System.Collections.Generic;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.Sheduler.Controllers
{
    class IdentifySchedule : IController
    {
        private IdentifyScheduleActivity view;

        private Faculty selectedFaculty;
        private Specialty selectedSpecialty;
        private Courses selectedCourse;
        private Group selectedGroup;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            this.view = view;
        }

        public void Update()
        {
            List<Faculty> faculties = FacadeAPI.GetFaculties();           
            view.ShowFaculty(faculties);
        }

        internal void SelectFaculty(Faculty faculty)
        {
            selectedFaculty = faculty;
            List<Specialty> specialtys = FacadeAPI.GetSpecialtys(faculty.IdFaculty);
            view.ShowSpecialtys(specialtys);
        }

        internal void SelectSpecialt(Specialty specialty)
        {
            selectedSpecialty = specialty;
            List<Courses> courses = FacadeAPI.GetCourses(
                selectedFaculty.IdFaculty,
                FacadeAPI.ConvertIdsToString(specialty.IdsSpecialty)
                );
            view.ShowCourses(courses);
        }

        internal void SelectCourses(Courses cours)
        {
            selectedCourse = cours;
            List<Group> groups = FacadeAPI.GetGroups(
                selectedFaculty.IdFaculty,
                selectedCourse.IdCourse,
                FacadeAPI.ConvertIdsToString(selectedSpecialty.IdsSpecialty)
                );
            view.ShowGroup(groups);
        }

        internal void SelectGroup(Group group)
        {
            //throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPITest.fake
{
    internal class FacultiesServiceFake : IFacultiesService
    {
        public List<Faculty> GetFaculties(string year, string idFilial)
        {
            var faculty = new List<Faculty>
            {
                DtoFactoryFake.CreateFaculty("1", "1"),
                DtoFactoryFake.CreateFaculty("2", "2"),
                DtoFactoryFake.CreateFaculty("3", "3"),
                DtoFactoryFake.CreateFaculty("4", "4")
            };
            return faculty;
        }

        public List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty)
        {
            List<Specialty> specialtys = new List<Specialty>
            {
                DtoFactoryFake.CreateSpecialty("1", idFaculty),
                DtoFactoryFake.CreateSpecialty("2", idFaculty),
                DtoFactoryFake.CreateSpecialty("3", idFaculty),
                DtoFactoryFake.CreateSpecialty("4", idFaculty)
            };
            return specialtys;
        }

        public List<Courses> GetCourses(string year, string idFilial, string idFaculty, string idsSpecialty)
        {
            List<Courses> courses = new List<Courses>
            {
                DtoFactoryFake.CreateCourses("1", idFaculty, idsSpecialty),
                DtoFactoryFake.CreateCourses("2", idFaculty, idsSpecialty),
                DtoFactoryFake.CreateCourses("3", idFaculty, idsSpecialty),
                DtoFactoryFake.CreateCourses("4", idFaculty, idsSpecialty)
            };
            return courses;
        }

        public List<Group> GetGroups(string year, string idFilial, string idFaculty, string idCourse, string idsSpecialty)
        {
            List<Group> groups = new List<Group>
            {
                DtoFactoryFake.CreateGroup("1", idFaculty, idsSpecialty, idCourse),
                DtoFactoryFake.CreateGroup("2", idFaculty, idsSpecialty, idCourse),
                DtoFactoryFake.CreateGroup("3", idFaculty, idsSpecialty, idCourse),
                DtoFactoryFake.CreateGroup("4", idFaculty, idsSpecialty, idCourse)
            };
            return groups;
        }
    }
}
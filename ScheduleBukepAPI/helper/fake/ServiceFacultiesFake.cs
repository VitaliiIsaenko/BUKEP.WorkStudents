using System;
using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.ShedulerApi
{
    internal class ServiceFacultiesFake : IServiceFaculties
    {
        public List<Faculty> GetFaculties(string year, string idFilial)
        {
            List<Faculty> faculty = new List<Faculty>
            {
                DTOBuilderFake.CreateFaculty("1", "1"),
                DTOBuilderFake.CreateFaculty("2", "2"),
                DTOBuilderFake.CreateFaculty("3", "3"),
                DTOBuilderFake.CreateFaculty("4", "4")
            };
            return faculty;
        }

        public List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty)
        {
            List<Specialty> specialtys = new List<Specialty>
            {
                DTOBuilderFake.CreateSpecialty("1", idFaculty),
                DTOBuilderFake.CreateSpecialty("2", idFaculty),
                DTOBuilderFake.CreateSpecialty("3", idFaculty),
                DTOBuilderFake.CreateSpecialty("4", idFaculty)
            };
            return specialtys;
        }

        public List<Courses> GetCourses(string year, string idFilial, string idFaculty, string idsSpecialty)
        {
            List<Courses> courses = new List<Courses>
            {
                DTOBuilderFake.CreateCourses("1", idFaculty, idsSpecialty),
                DTOBuilderFake.CreateCourses("2", idFaculty, idsSpecialty),
                DTOBuilderFake.CreateCourses("3", idFaculty, idsSpecialty),
                DTOBuilderFake.CreateCourses("4", idFaculty, idsSpecialty)
            };
            return courses;
        }

        public List<Group> GetGroups(string year, string idFilial, string idFaculty, string idCourse, string idsSpecialty)
        {
            List<Group> groups = new List<Group>
            {
                DTOBuilderFake.CreateGroup("1", idFaculty, idsSpecialty, idCourse),
                DTOBuilderFake.CreateGroup("2", idFaculty, idsSpecialty, idCourse),
                DTOBuilderFake.CreateGroup("3", idFaculty, idsSpecialty, idCourse),
                DTOBuilderFake.CreateGroup("4", idFaculty, idsSpecialty, idCourse)
            };
            return groups;
        }
    }
}
using System;
using Bukep.ShedulerApi.apiDTO;
using System.Collections.Generic;

namespace Bukep.ShedulerApi
{
    internal static class DTOBuilderFake
    {
        internal static Courses CreateCourses(string name, string idFaculty, string idsSpecialty)
        {
            return new Courses()
            {
                NameCourse = string.Format("{0} {1} {2}", idFaculty, idsSpecialty, name),
                IdCourse = name
            };
        }

        internal static Faculty CreateFaculty(string name, string id)
        {
            return new Faculty()
            {
                Name = string.Format("{0} {1}", id, name),
                IdFaculty = name
            };
        }

        internal static GroupLesson CreateGroupLesson(string name)
        {
            return new GroupLesson()
            {
                NameLesson = name
            };
        }

        internal static Group CreateGroup(string name, string idFaculty, string idsSpecialty, string idCourse)
        {
            return new Group()
            {
                NameGroup = string.Format("{0} {1} {2} {3}", idFaculty, idsSpecialty, idCourse, name),
            };
        }

        internal static Specialty CreateSpecialty(string name, string id)
        {
            List<int> ids = new List<int>
            {
                int.Parse(name)
            };
            return new Specialty()
            {
                NameSpeciality = string.Format("{0} {1}", id, name),
                IdsSpecialty = ids
            };
        }
    }
}
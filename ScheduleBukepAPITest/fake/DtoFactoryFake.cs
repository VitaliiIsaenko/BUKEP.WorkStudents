﻿using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPITest.fake
{
    internal static class DtoFactoryFake
    {
        internal static Courses CreateCourses(string name, string idFaculty, string idsSpecialty)
        {
            return new Courses()
            {
                NameCourse = $"{idFaculty} {idsSpecialty} {name}",
                IdCourse = name
            };
        }

        internal static Faculty CreateFaculty(string name, string id)
        {
            return new Faculty()
            {
                Name = $"{id} {name}",
                IdFaculty = name
            };
        }

        internal static Lesson CreateGroupLesson(string name)
        {
            return new Lesson()
            {
                NameLesson = name
            };
        }

        internal static Group CreateGroup(string name, string idFaculty, string idsSpecialty, string idCourse)
        {
            return new Group()
            {
                NameGroup = $"{idFaculty} {idsSpecialty} {idCourse} {name}",
            };
        }

        internal static Specialty CreateSpecialty(string name, string id)
        {
            var ids = new List<int>
            {
                int.Parse(name)
            };
            return new Specialty()
            {
                NameSpeciality = $"{id} {name}",
                IdsSpecialty = ids
            };
        }
    }
}
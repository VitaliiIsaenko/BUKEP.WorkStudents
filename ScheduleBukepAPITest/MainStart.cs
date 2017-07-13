using System;
using System.Collections.Generic;
using ScheduleBukepAPI;
using ScheduleBukepAPI.decorators;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPITest
{
    /// <summary>
    /// Класс для работы с API через консоль.
    /// </summary>
    internal class MainStart
    {
        private static readonly FacadeApi Api = new FacadeApi(
            new FilterGroupDecorator(new FacultiesService()),
            new SchedulesService()
        );

        private static void Main(string[] args)
        {
            SelectSchedules();
            //SelectTeacher();
        }

        private static void SelectTeacher()
        {
            var selectPulpit = SelectPulpit();
            var selectTeacher = SelectTeacher(selectPulpit.IdPulpit);

            var dateTime = DateTime.Today.AddDays(1).ToString(FacadeApi.DateTimeFormat);

            var idsString = FacadeApi.ConvertIdsToString(selectTeacher.IdsTeacher);
            var idTeacher = idsString.Substring(1, idsString.Length - 2); //TODO: это костыль обрезание []
            SelectTeacherLessons(idTeacher, dateTime, dateTime);
        }

        private static Teacher SelectTeacher(string idPulpit)
        {
            var teachers = Api.GetTeacher(idPulpit);
            for (var i = 0; i < teachers.Count; i++)
            {
                var teacher = teachers[i];
                Console.WriteLine("{0}. {1} = {2} ", i, teacher.Fio, 
                    FacadeApi.ConvertIdsToString(teacher.IdsTeacher));
            }
            var selectedNumber = AskNumber();

            return teachers[selectedNumber];
        }

        private static Pulpit SelectPulpit()
        {
            var pulpits = Api.GetPulpits();
            for (var i = 0; i < pulpits.Count; i++)
            {
                var pulpit = pulpits[i];
                Console.WriteLine("{0}. {1} = {2} ", i, pulpit.NamePulpit, pulpit.IdPulpit);
            }
            var selectedNumber = AskNumber();

            return pulpits[selectedNumber];
        }

        private static void SelectSchedules()
        {
            var selectedFaculty = SelectFaculty();
            var selectedSpecialty = SelectedSpecialty(selectedFaculty);
            var selectedCourse = SelectedCourse(selectedFaculty, selectedSpecialty);
            var selectedGroup = SelectedGroup(selectedFaculty, selectedSpecialty, selectedCourse);
            var selectedGroupLessons = SelectedLessonsGroup(selectedGroup);
            ShowGroupLessons(selectedGroupLessons);
        }

        private static void SelectTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            var lessons = FacadeApi.GetTeacherLessons(idTeacher, dateFrom, dateTo);
            foreach (var item in lessons)
            {
                Console.WriteLine(item.Discipline.Value);
            }
        }


        private static void ShowGroupLessons(IEnumerable<GroupLesson> selectedGroupLessons)
        {
            foreach (var groupLesson in selectedGroupLessons)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("Discipline = " + groupLesson.Discipline.Value);
            }
        }

        private static IEnumerable<GroupLesson> SelectedLessonsGroup(Group selectedGroup)
        {
            return Api.GetGroupLessons(
                FacadeApi.ConvertIdsToString(selectedGroup.IdsSchedulGroup),
                "2017-05-15",
                "2017-05-15"
            );
        }

        private static Group SelectedGroup(Faculty selectedFaculty, Specialty selectedSpecialty, Courses selectedCourse)
        {
            var groups = Api.GetGroups(
                selectedFaculty.faculty.Key,
                selectedCourse.IdCourse,
                FacadeApi.ConvertIdsToString(selectedSpecialty.IdsSpecialty)
            );
            for (var i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                Console.WriteLine("{0}. {1} {2} = {3} ", i, group.NameGroup, group.NameTypeShedule,
                    FacadeApi.ConvertIdsToString(group.IdsSchedulGroup));
            }

            var numberGroup = AskNumber();

            var selectedGroup = groups[numberGroup];
            return selectedGroup;
        }

        private static Courses SelectedCourse(Faculty selectedFaculty, Specialty selectedSpecialty)
        {
            var idsSpecialty = FacadeApi.ConvertIdsToString(selectedSpecialty.IdsSpecialty);
            Console.WriteLine("IdsSpecialty = " + idsSpecialty);

            var courses = Api.GetCourses(selectedFaculty.faculty.Key, idsSpecialty);
            for (var i = 0; i < courses.Count; i++)
            {
                var course = courses[i];
                Console.WriteLine("{0}. {1} = {2}", i, course.NameCourse, course.IdCourse);
            }
            var numberCourses = AskNumber();

            var selectedCourse = courses[numberCourses];
            return selectedCourse;
        }

        private static Specialty SelectedSpecialty(Faculty selectedFaculty)
        {
            var specialtys = Api.GetSpecialtys(selectedFaculty.faculty.Key);

            for (var i = 0; i < specialtys.Count; i++)
            {
                var specialty = specialtys[i];
                var ids = FacadeApi.ConvertIdsToString(specialty.IdsSpecialty);
                Console.WriteLine("{0}. {1} id = {2}", i, specialty.NameSpeciality, ids);
            }
            var number = AskNumber();
            var selectedSpecialty = specialtys[number];
            return selectedSpecialty;
        }

        private static Faculty SelectFaculty()
        {
            var faculties = Api.GetFaculties();
            for (var i = 0; i < faculties.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, faculties[i].faculty.Value);
            }

            var numberFaculty = AskNumber();

            return faculties[numberFaculty];
        }

        private static int AskNumber()
        {
            Console.Write("Введите число:");
            return System.Convert.ToInt32(Console.ReadLine());
        }
    }
}
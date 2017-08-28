using System;
using System.Collections.Generic;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPITest
{
    /// <summary>
    /// Класс для работы с API через консоль.
    /// </summary>
    public class MainStart
    {
        private static readonly Api Api = new Api(
            new FilteringFacultiesService(new HttpRequstHelper()),
            new SchedulesService(new HttpRequstHelper())
        );

        public static void Main(string[] args)
        {
            SelectSchedules();
            SelectTeacher();
            Console.Read();
        }

        private static void SelectTeacher()
        {
            Pulpit selectPulpit = SelectPulpit();
            var selectTeacher = SelectTeacher(selectPulpit.Info.Key);

            DateTime dateTime = DateTime.Today.AddDays(1);
            IList<int> idTeacher = selectTeacher.IdsTeacher;
            SelectTeacherLessons(idTeacher, dateTime, dateTime);
        }

        private static Teacher SelectTeacher(int idPulpit)
        {
            var teachers = Api.GetTeacher(idPulpit);
            for (var i = 0; i < teachers.Count; i++)
            {
                var teacher = teachers[i];
                Console.WriteLine("{0}. {1} = {2} ", i, teacher.Fio,
                    Api.ConvertIdsToString(teacher.IdsTeacher));
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
                Console.WriteLine("{0}. {1} = {2} ", i, pulpit.Info.Value, pulpit.Info.Key);
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

        private static void SelectTeacherLessons(IList<int> idTeacher, DateTime dateFrom, DateTime dateTo)
        {
            var lessons = Api.GetTeacherLessons(idTeacher, dateFrom, dateTo);
            foreach (var item in lessons)
            {
                Console.WriteLine(item.Discipline.Value);
            }
        }


        private static void ShowGroupLessons(IEnumerable<Lesson> selectedGroupLessons)
        {
            foreach (var groupLesson in selectedGroupLessons)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("Discipline = " + groupLesson.Discipline.Value);
            }
        }

        private static IEnumerable<Lesson> SelectedLessonsGroup(Group selectedGroup)
        {
            return Api.GetGroupLessons(
                selectedGroup.Ids,
                DateTime.Parse("2017-05-15"),
                DateTime.Parse("2017-05-15")
            );
        }

        private static Group SelectedGroup(Faculty selectedFaculty, Specialty selectedSpecialty, Course selectedCourse)
        {
            var groups = Api.GetGroups(
                selectedFaculty.Info.Key,
                selectedCourse.Info.Key,
                selectedSpecialty.Info.Key
            );
            for (var i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                Console.WriteLine("{0}. {1} {2} = {3} ", i, group.Info, group.NameTypeSchedule,
                    Api.ConvertIdsToString(group.Ids));
            }

            var numberGroup = AskNumber();

            var selectedGroup = groups[numberGroup];
            return selectedGroup;
        }

        private static Course SelectedCourse(Faculty selectedFaculty, Specialty selectedSpecialty)
        {
            IList<int> idsSpecialty = selectedSpecialty.Info.Key;
            Console.WriteLine("IdsSpecialty = " + idsSpecialty);

            var courses = Api.GetCourses(selectedFaculty.Info.Key, idsSpecialty);
            for (var i = 0; i < courses.Count; i++)
            {
                var course = courses[i];
                Console.WriteLine("{0}. {1} = {2}", i, course.Info.Value, course.Info.Key);
            }
            var numberCourses = AskNumber();

            var selectedCourse = courses[numberCourses];
            return selectedCourse;
        }

        private static Specialty SelectedSpecialty(Faculty selectedFaculty)
        {
            var specialtys = Api.GetSpecialtys(selectedFaculty.Info.Key);

            for (var i = 0; i < specialtys.Count; i++)
            {
                var specialty = specialtys[i];
                var ids = Api.ConvertIdsToString(specialty.Info.Key);
                Console.WriteLine("{0}. {1} id = {2}", i, specialty.Info.Value, ids);
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
                Console.WriteLine("{0}. {1}", i, faculties[i].Info.Value);
            }

            var numberFaculty = AskNumber();

            return faculties[numberFaculty];
        }

        private static int AskNumber()
        {
            Console.Write("Введите число:");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
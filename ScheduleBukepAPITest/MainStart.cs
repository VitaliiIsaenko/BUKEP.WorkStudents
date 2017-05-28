using System;
using System.Collections.Generic;
using ScheduleBukepAPI;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPITest
{
    /// <summary>
    /// Класс для работы с API через консоль.
    /// </summary>
    internal class MainStart
    {
        private static readonly FacadeApi Api = new FacadeApi();
        private static void Main(string[] args)
        {
            Start();
        }

        private static void ExampleGetDTOFromConsole()
        {
            var jsonConvert = new JsonConvert();;
            var json = Console.ReadLine();
            var group = jsonConvert.ConvertTo<Group>(json);
            Console.WriteLine(group.NameGroup);
            var jsonOfDto = jsonConvert.ConvertToJson<Group>(group);
            Console.WriteLine("jsonOfDTO = " + jsonOfDto);
        }

        private static void Start()
        {
            var selectedFaculty = SelectFaculty();
            var selectedSpecialty = SelectedSpecialty(selectedFaculty);
            var selectedCourse = SelectedCourse(selectedFaculty, selectedSpecialty);
            var selectedGroup = SelectedGroup(selectedFaculty, selectedSpecialty, selectedCourse);

            ShowGroupLessons(selectedGroup);
        }

        private static void ShowGroupLessons(Group selectedGroup)
        {
            var groupLessons = Api.GetGroupLessons(
                            FacadeApi.ConvertIdsToString(selectedGroup.IdsSchedulGroup),
                            "2017-05-15",
                            "2017-05-15"
                            );
            foreach (var groupLesson in groupLessons)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine(
                    "NameTypeShedule - " + groupLesson.NameTypeShedule + "\n" +
                    "NameTypeWeek - " + groupLesson.NameTypeWeek + "\n" +
                    "NameDay - " + groupLesson.NameDay + "\n" +
                    "NameLesson - " + groupLesson.NameLesson + "\n" +
                    "TypeLesson - " + groupLesson.TypeLesson + "\n" +
                    "DateLesson - " + groupLesson.DateLesson + "\n" +
                    "TimeStartLesson - " + groupLesson.TimeStartLesson + "\n" +
                    "TimeEndLesson - " + groupLesson.TimeEndLesson + "\n" +
                    "NameAuditory - " + groupLesson.NameAuditory + "\n" +
                    "IdTeacher - " + groupLesson.IdTeacher + "\n" +
                    "FioTeacher - " + groupLesson.FioTeacher + "\n" +
                    "NameDiscipline - " + groupLesson.NameDiscipline
                );
            }
        }

        private static Group SelectedGroup(Faculty selectedFaculty, Specialty selectedSpecialty, Courses selectedCourse)
        {
            var groups = Api.GetGroups(
                selectedFaculty.IdFaculty,
                selectedCourse.IdCourse,
                FacadeApi.ConvertIdsToString(selectedSpecialty.IdsSpecialty)
                );
            for (var i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                Console.WriteLine("{0}. {1} = {2}", i, group.NameGroup, FacadeApi.ConvertIdsToString(group.IdsSchedulGroup));
            }

            var numberGroup = AskNumber();

            var selectedGroup = groups[numberGroup];
            return selectedGroup;
        }

        private static Courses SelectedCourse(Faculty selectedFaculty, Specialty selectedSpecialty)
        {
            var idsSpecialty = FacadeApi.ConvertIdsToString(selectedSpecialty.IdsSpecialty);
            Console.WriteLine("IdsSpecialty = " + idsSpecialty);

            var courses = Api.GetCourses(selectedFaculty.IdFaculty, idsSpecialty);
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
            var specialtys = Api.GetSpecialtys(selectedFaculty.IdFaculty);

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
                Console.WriteLine("{0}. {1}", i, faculties[i].Name);
            }

            var numberFaculty = AskNumber();

            return faculties[numberFaculty];
        }

        private static int AskNumber()
        {
            Console.WriteLine("Введите число:");
            return System.Convert.ToInt32(Console.ReadLine());
        }
    }
}

using System;
using System.Collections.Generic;
using ScheduleBukepAPI;
using ScheduleBukepAPI.decorators;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
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
            Start();

        }

        private static void Start()
        {
            var selectedFaculty = SelectFaculty();
            var selectedSpecialty = SelectedSpecialty(selectedFaculty);
            var selectedCourse = SelectedCourse(selectedFaculty, selectedSpecialty);
            var selectedGroup = SelectedGroup(selectedFaculty, selectedSpecialty, selectedCourse);
            var selectedGroupLessons = SelectedLessonsGroup(selectedGroup);
            ShowGroupLessons(selectedGroupLessons);
            ShowTeacherLessons(selectedGroupLessons[0]);
        }

        private static void ShowTeacherLessons(GroupLesson groupLessons)
        {
            
            var idTeacher = groupLessons.IdTeacher;
            var lessons=FacadeApi.GetTeacherLessons(idTeacher, "2017-06-12", "2017-06-12");
            Console.WriteLine("Расписание преподавателя " + groupLessons.NameDiscipline);
            foreach (var item in lessons)
            {
                Console.WriteLine(item.NameDiscipline);
            }

        }
        
       

        private static void ShowGroupLessons(IEnumerable<GroupLesson> selectedGroupLessons)
        {

            foreach (var groupLesson in selectedGroupLessons)
            {
                Console.WriteLine("===========================================");
                //TODO: добавить вывод lesson в консоль
            }
        }

        private static IList<GroupLesson> SelectedLessonsGroup(Group selectedGroup)
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
                selectedFaculty.IdFaculty,
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
            Console.Write("Введите число:");
            return System.Convert.ToInt32(Console.ReadLine());
        }
    }
}
using System;
using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPI.decorators
{
    /// <summary>
    /// Нужен для фильтрации Groupe. 
    /// </summary>
    public class FilterGroupDecorator : IFacultiesService
    {
        private readonly IFacultiesService _service;


        public FilterGroupDecorator(IFacultiesService service)
        {
            _service = service;
        }

        public List<Group> GetGroups(string year, string idFilial,
            string idFaculty, string idCourse, string idsSpecialty)
        {
            var groups = _service.GetGroups(year, idFilial, idFaculty, idCourse, idsSpecialty);
            groups.RemoveAll(IsGroupOutInterval);
            return groups;
        }

        /// <summary>
        /// Входит ли сегодня в интервал расписания.
        /// Нужно для фильтрации не начавшегося или прошедшего расписания.
        /// </summary>
        /// <param name="group">Группа для проверки</param>
        /// <returns>true если группа не входит за интервал</returns>
        private static bool IsGroupOutInterval(Group group)
        {
            var startDate = DateTime.Parse(group.ScheduleDateFrom);
            var endDate = DateTime.Parse(group.ScheduleDateTo);

            return TodayIsInRange(startDate, endDate);
        }

        public static bool TodayIsInRange(DateTime startDate, DateTime endDate)
        {
            var today = DateTime.Today;
            var result = !(today >= startDate && today < endDate);
            Console.WriteLine(
                $"TodayIsInRange() Range {startDate} - {endDate} result = {result}"
                );
            return result;
        }

        public List<Courses> GetCourses(string year, string idFilial, string idFaculty,
            string idsSpecialty)
        {
            return _service.GetCourses(year, idFilial, idFaculty, idsSpecialty);
        }

        public List<Faculty> GetFaculties(string year, string idFilial)
        {
            return _service.GetFaculties(year, idFilial);
        }

        public List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty)
        {
            return _service.GetSpecialtys(year, idFilial, idFaculty);
        }

        public List<Pulpit> GetPulpits(string year, string idFilial)
        {
            return _service.GetPulpits(year, idFilial);
        }

        public List<Teacher> GetTeacher(string year, string idPulpit)
        {
            return _service.GetTeacher(year, idPulpit);
        }
    }
}
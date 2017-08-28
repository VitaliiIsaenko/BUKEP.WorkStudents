using System;
using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    //TODO: Добавить UnitTest
    /// <summary>
    /// Используется для фильтрации данных расписания.
    /// </summary>
    public class FilteringFacultiesService : FacultiesService
    {
        public FilteringFacultiesService(HttpRequstHelper httpRequestHelper) : base(httpRequestHelper)
        {
        }

        /// <summary>
        /// Удаляет все не актуальные факультеты.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idFilial"></param>
        /// <returns></returns>
        public override List<Faculty> GetFaculties(int year, int idFilial)
        {
            var faculties = base.GetFaculties(year, idFilial);
            faculties.RemoveAll(faculty => faculty.IsActiveSchedule == false);
            return faculties;
        }

        /// <summary>
        /// Удаляет все не актуальные группы.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idFilial"></param>
        /// <param name="idFaculty"></param>
        /// <param name="idCourse"></param>
        /// <param name="idsSpecialty"></param>
        /// <returns></returns>
        public override List<Group> GetGroups(int year, int idFilial,
            int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            var groups = base.GetGroups(year, idFilial, idFaculty, idCourse, idsSpecialty);
            groups.RemoveAll(IsGroupOutInterval);
            return groups;
        }

        /// <summary>
        /// Выходит ли сегодня за интервал расписания группы.
        /// Нужно для фильтрации не начавшегося или прошедшего расписания.
        /// </summary>
        /// <param name="group">Группа для проверки</param>
        /// <returns>false если расписание группы актуально на сегодня</returns>
        private static bool IsGroupOutInterval(Group group)
        {
            DateTime? startDate = group.ScheduleDateFrom;
            DateTime? endDate = group.ScheduleDateTo;

            return !TodayIsInRange(startDate, endDate);
        }

        /// <summary>
        /// Входит ли сегодняшний день в переданный период.
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>true если входит, также false если startDate или endDate равны null</returns>
        public static bool TodayIsInRange(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return false;
            var today = DateTime.Today;
            var result = (today >= startDate && today < endDate);
            Console.WriteLine(
                $"TodayIsInRange() Range {startDate} - {endDate} result = {result}"
            );
            return result;
        }

        /// <summary>
        /// Удаляет все не актуальные кафедры.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idFilial"></param>
        /// <returns></returns>
        public override List<Pulpit> GetPulpits(int year, int idFilial)
        {
            var pulpits = base.GetPulpits(year, idFilial);
            pulpits.RemoveAll(pulpit => pulpit.IsActiveSchedule == false);
            return pulpits;
        }
    }
}

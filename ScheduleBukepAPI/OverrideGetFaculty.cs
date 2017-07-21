using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;
using System;
using System.Collections.Generic;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI
{
    public class OverrideGetFaculty : FacultiesService
    {
        public OverrideGetFaculty(HttpRequstHelper httpRequestHelper, JsonConvert jsonConvert) : base(httpRequestHelper, jsonConvert)
        {
        }

        public OverrideGetFaculty()
        {
        }

        public override List<Faculty> GetFaculties(int year, int idFilial)
        {
            var Faculties = base.GetFaculties(year, idFilial);
            var faculty = new List<Faculty>(Faculties);
            for (int i = 0; i < Faculties.Count; i++)
            {
                if(Faculties[i].IsActiveSchedule==false)
                {
                    faculty.Remove(Faculties[i]);
                }
            }
            return faculty;
        }

        /// <summary>
        /// Добавлеят дополнительную сортировку по 
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
        /// Входит ли сегодня в интервал расписания.
        /// Нужно для фильтрации не начавшегося или прошедшего расписания.
        /// </summary>
        /// <param name="group">Группа для проверки</param>
        /// <returns>true если расписание группы актуально на сегодня</returns>
        private static bool IsGroupOutInterval(Group group)
        {
            DateTime? startDate = group.ScheduleDateFrom;
            DateTime? endDate = group.ScheduleDateTo;

            return TodayIsInRange(startDate, endDate);
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
            var result = !(today >= startDate && today < endDate);
            Console.WriteLine(
                $"TodayIsInRange() Range {startDate} - {endDate} result = {result}"
            );
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPI
{
    //TODO: raname in AdapterApi
    /// <summary>
    /// Нужен для упрощения работы с IFacultiesService и ISchedulesService.
    /// </summary>
    public class FacadeApi
    {
        private static IFacultiesService _facultiesService;
        private static ISchedulesService _schedulesService;

        /// <summary>
        /// Формат даты для api.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd";

        //TODO: написать метод который бы получал учебный год.
        private const int Year = 2016;

        private const int IdFilial = 1000;

        public FacadeApi(IFacultiesService facultiesService, ISchedulesService schedulesService)
        {
            _facultiesService = facultiesService;
            _schedulesService = schedulesService;
        }

        public FacadeApi() : this(new FacultiesService(), new SchedulesService())
        {
        }

        public List<Pulpit> GetPulpits()
        {
            return _facultiesService.GetPulpits(Year, IdFilial);
        }

        public List<Teacher> GetTeacher(int idPulpit)
        {
            return _facultiesService.GetTeacher(Year, idPulpit);
        }

        public IList<Faculty> GetFaculties()
        {
            return _facultiesService.GetFaculties(Year, IdFilial);
        }

        public IList<Specialty> GetSpecialtys(int idFaculty)
        {
            return _facultiesService.GetSpecialtys(Year, IdFilial, idFaculty);
        }

        public IList<Group> GetGroups(int idFaculty, int idCourse, IList<int> idsSpecialty)
        {
            return _facultiesService.GetGroups(Year, IdFilial, idFaculty, idCourse, idsSpecialty);
        }

        public IList<Course> GetCourses(int idFaculty, IList<int> idsSpecialty)
        {
            return _facultiesService.GetCourses(Year, IdFilial, idFaculty, idsSpecialty);
        }

        public IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup, DateTime dateFrom, DateTime dateTo)
        {
            return _schedulesService.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo);
        }

        public static IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo)
        {
            return _schedulesService.GetTeacherLessons(idsTeacher, dateFrom, dateTo);
        }

        //TODO: Delete
        /// <summary>
        /// Нужен для конвертирования списка id в string.
        /// </summary>
        /// <param name="ids">Список id</param>
        /// <returns>Список ids в формате string разделенный запятыми. Пример: [34,345,60]</returns>
        public static string ConvertIdsToString(IList<int> ids)
        {
            if (ids == null) throw new ArgumentException("Parameter ids cannot be null.");

            var result = string.Join(",", ids.ToArray());
            return $"[{result}]";
        }
    }
}
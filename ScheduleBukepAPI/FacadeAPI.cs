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
        private const string Year = "2016";

        private const string IdFilial = "1000";

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

        public List<Teacher> GetTeacher(string idPulpit)
        {
            return _facultiesService.GetTeacher(Year, idPulpit);
        }

        public IList<Faculty> GetFaculties()
        {
            return _facultiesService.GetFaculties(Year, IdFilial);
        }

        public IList<Specialty> GetSpecialtys(string idFaculty)
        {
            return _facultiesService.GetSpecialtys(Year, IdFilial, idFaculty);
        }

        public IList<Group> GetGroups(string idFaculty, string idCourse, string idsSpecialty)
        {
            return _facultiesService.GetGroups(Year, IdFilial, idFaculty, idCourse, idsSpecialty);
        }

        public IList<Courses> GetCourses(string idFaculty, string idsSpecialty)
        {
            return _facultiesService.GetCourses(Year, IdFilial, idFaculty, idsSpecialty);
        }

        public IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            return _schedulesService.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo);
        }

        public static IList<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            return _schedulesService.GetTeacherLessons(idTeacher, dateFrom, dateTo);
        }

        //TODO: Вынести в ConvertId
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
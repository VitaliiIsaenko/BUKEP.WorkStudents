using System.Collections.Generic;
using System.Linq;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPI
{
    public static class FacadeApi
    {
        private static IServiceFaculties ServiceFaculties { get; set; }
        private static IServiceSchedules ServiceSchedules { get; set; }

        private const string Year = "2016";
        private const string IdFilial = "1000";

        static FacadeApi()
        {
            ServiceFaculties = new ServiceFaculties();
            ServiceSchedules = new ServiceSchedules();
        }

        public static IList<Faculty> GetFaculties()
        {
            return ServiceFaculties.GetFaculties(Year, IdFilial);
        }

        public static IList<Specialty> GetSpecialtys(string idFaculty)
        {
            return ServiceFaculties.GetSpecialtys(Year, IdFilial, idFaculty);
        }

        public static IList<Group> GetGroups(string idFaculty, string idCourse, string idsSpecialty)
        {
            return ServiceFaculties.GetGroups(Year, IdFilial, idFaculty, idCourse, idsSpecialty);
        }

        public static IList<Courses> GetCourses(string idFaculty, string idsSpecialty)
        {
            return ServiceFaculties.GetCourses(Year, IdFilial, idFaculty, idsSpecialty);
        }

        public static IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            return ServiceSchedules.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo);
        }

        public static List<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            return ServiceSchedules.GetTeacherLessons(idTeacher, dateFrom, dateTo);
        }

        public static string ConvertIdsToString(IList<int> ids)
        {
            if (ids == null) return "";
            var result = string.Join(",", ids.ToArray());
            result = "[" + result + "]";
            return result;
        }
    }
}
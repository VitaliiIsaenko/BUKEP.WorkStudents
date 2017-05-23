using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;
using System.Linq;

namespace Bukep.ShedulerApi
{
    public static class FacadeAPI
    {
        private static IServiceFaculties serviceFaculties = new ServiceFaculties();
        private static IServiceSchedules serviceSchedules = new ServiceSchedules();

        private const string year = "2016";
        private const string idFilial = "1000";

        public static List<Faculty> GetFaculties()
        {
            return serviceFaculties.GetFaculties(year, idFilial);
        }

        public static List<Specialty> GetSpecialtys(string idFaculty)
        {
            return serviceFaculties.GetSpecialtys(year, idFilial, idFaculty);
        }

        public static List<Group> GetGroups(string idFaculty, string idCourse, string idsSpecialty)
        {
            return serviceFaculties.GetGroups(year, idFilial, idFaculty, idCourse, idsSpecialty);
        }

        public static List<Courses> GetCourses(string idFaculty, string idsSpecialty)
        {
            return serviceFaculties.GetCourses(year, idFilial, idFaculty, idsSpecialty);
        }

        public static List<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            return serviceSchedules.GetGroupLessons(idsSheduleGroup, dateFrom, dateTo);
        }

        public static string ConvertIdsToString(IList<int> ids)
        {
            if (ids == null) return "";
            string result = string.Join(",", ids.ToArray());
            result = "[" + result + "]";
            return result;
        }

        public static void UseServiceFake()
        {
            serviceFaculties = new ServiceFacultiesFake();
            serviceSchedules = new ServiceSchedulesFake();
    }
    }
}
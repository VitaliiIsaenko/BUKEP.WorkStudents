using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    //TODO: Заменить List на IEnumerable
    internal class ServiceFaculties : IServiceFaculties
    {
        public List<Faculty> GetFaculties(string year, string idFilial)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial }
            };
            var json = HttpRequstHelper.ExecuteGet("GetFaculties", parameters);
            return JsonConvert.ConvertToList<Faculty>(json);
        }

        public List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial },
                { "idFaculty", idFaculty }
            };
            var json = HttpRequstHelper.ExecuteGet("GetSpecialtys", parameters);
            return JsonConvert.ConvertToList<Specialty>(json);
        }

        public List<Courses> GetCourses(string year, string idFilial, string idFaculty, string idsSpecialty)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial },
                { "idFaculty", idFaculty }
            };
            var json = HttpRequstHelper.ExecutePost("GetCourses", parameters, idsSpecialty);
            return JsonConvert.ConvertToList<Courses>(json);
        }

        public List<Group> GetGroups(string year, string idFilial, string idFaculty, string idCourse, string idsSpecialty)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial },
                { "idFaculty", idFaculty },
                { "idCourse", idCourse }
            };
            var json = HttpRequstHelper.ExecutePost("GetGroups", parameters, idsSpecialty);
            return JsonConvert.ConvertToList<Group>(json);
        }
    }

}

using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    //TODO: Заменить List на IEnumerable
    public class FacultiesService : BaseService, IFacultiesService
    {
        public List<Faculty> GetFaculties(string year, string idFilial)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial }
            };
            var json = ExecuteGet("GetFaculties", parameters);
            return ConvertToList<Faculty>(json);
        }

        public List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial },
                { "idFaculty", idFaculty }
            };
            var json = ExecuteGet("GetSpecialtys", parameters);
            return ConvertToList<Specialty>(json);
        }

        public List<Courses> GetCourses(string year, string idFilial, string idFaculty, string idsSpecialty)
        {
            var parameters = new Dictionary<string, string>
            {
                { "year", year },
                { "idFilial", idFilial },
                { "idFaculty", idFaculty }
            };
            var json = ExecutePost("GetCourses", parameters, idsSpecialty);
            return ConvertToList<Courses>(json);
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
            var json = ExecutePost("GetGroups", parameters, idsSpecialty);
            return ConvertToList<Group>(json);
        }
    }

}

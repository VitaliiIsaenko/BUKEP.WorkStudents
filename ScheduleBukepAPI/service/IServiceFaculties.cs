using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.ShedulerApi
{
    interface IServiceFaculties
    {
        List<Courses> GetCourses(string year, string idFilial, string idFaculty, string idsSpecialty);
        List<Faculty> GetFaculties(string year, string idFilial);
        List<Group> GetGroups(string year, string idFilial, string idFaculty, string idCourse, string idsSpecialty);
        List<Specialty> GetSpecialtys(string year, string idFilial, string idFaculty);
    }
}
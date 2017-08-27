using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    /// <summary>
    /// Сервиc для получения Model.
    /// Данные в данном сервисе редко обновляются.
    /// </summary>
    public interface IFacultiesService
    {
        
        List<Faculty> GetFaculties(int year, int idFilial);

        List<Specialty> GetSpecialtys(int year, int idFilial, int idFaculty);

        List<Course> GetCourses(int year, int idFilial, int idFaculty, IList<int> idsSpecialty);

        List<Group> GetGroups(int year, int idFilial, int idFaculty, int idCourse, IList<int> idsSpecialty);

        List<Pulpit> GetPulpits(int year, int idFilial);

        List<Teacher> GetTeacher(int year, int idPulpit);

        List<TimeLesson> GetLessonTime(int idFilial);
    }
}
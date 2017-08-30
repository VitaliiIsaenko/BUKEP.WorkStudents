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
        /// <summary>
        /// Факультеты
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <returns>Факультеты</returns>
        List<Faculty> GetFaculties(int year, int idFilial);

        /// <summary>
        /// Специальности
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <param name="idFaculty">Идентификатор факультета</param>
        /// <returns>Специальности</returns>
        List<Specialty> GetSpecialtys(int year, int idFilial, int idFaculty);

        /// <summary>
        /// Курсы
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <param name="idFaculty">Идентификатор факультета</param>
        /// <param name="idsSpecialty">Список специальностей</param>
        /// <returns>Курсы</returns>
        List<Course> GetCourses(int year, int idFilial, int idFaculty, IList<int> idsSpecialty);

        /// <summary>
        /// Группы
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <param name="idFaculty">Идентификатор факультета</param>
        /// <param name="idsSpecialty">Список специальностей</param>
        /// <param name="idCourse">Идентификатор курса</param>
        /// <returns>Группы</returns>
        List<Group> GetGroups(int year, int idFilial, int idFaculty, int idCourse, IList<int> idsSpecialty);

        /// <summary>
        /// Кафедры
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <returns>Кафедры</returns>
        List<Pulpit> GetPulpits(int year, int idFilial);

        /// <summary>
        /// Преподаватели
        /// </summary>
        /// <param name="year">Учебный год</param>
        /// <param name="idPulpit">Идентификатор кафедры</param>
        /// <returns>Преподаватели</returns>
        List<Teacher> GetTeacher(int year, int idPulpit);

        /// <summary>
        /// Получение расписания звонков
        /// </summary>
        /// <param name="idFilial">Идентификатор филиала</param>
        /// <returns>Расписание звонков</returns>
        List<TimeLesson> GetLessonTime(int idFilial);
    }
}
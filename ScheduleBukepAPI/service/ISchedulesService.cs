using System;
using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    /// <summary>
    /// Сервиc для получения Model.
    /// Данные в данном сервисе часто обновляются.
    /// </summary>
    public interface ISchedulesService
    {
        /// <summary>
        /// Расписание для группы
        /// </summary>
        /// <param name="idsSheduleGroup">Идентификатор группы расписания</param>
        /// <param name="dateFrom">Дата начала. При null DateTime.MinValue</param>
        /// <param name="dateTo">Дата окончания. При null DateTime.MaxValue</param>
        /// <returns>Расписание для группы</returns>
        IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Расписание для преподавателя
        /// </summary>
        /// <param name="idsTeacher">Идентификатор преподавателя</param>
        /// <param name="dateFrom">Дата начала. При null DateTime.MinValue</param>
        /// <param name="dateTo">Дата окончания. При null DateTime.MaxValue</param>
        /// <returns>Расписание для преподавателя</returns>
        IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo);
    }
}
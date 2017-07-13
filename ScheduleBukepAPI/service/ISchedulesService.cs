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
        IList<Lesson> GetGroupLessons(int idsSheduleGroup, DateTime dateFrom, DateTime dateTo);
        IList<Lesson> GetTeacherLessons(int idTeacher, DateTime dateFrom, DateTime dateTo);
    }
}
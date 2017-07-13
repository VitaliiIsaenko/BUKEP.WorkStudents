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
        IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup, DateTime dateFrom, DateTime dateTo);
        IList<Lesson> GetTeacherLessons(IList<int> idsTeacher, DateTime dateFrom, DateTime dateTo);
    }
}
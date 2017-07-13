using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    public interface ISchedulesService
    {
        IList<Lesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo);    
        IList<Lesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo);
    }
}
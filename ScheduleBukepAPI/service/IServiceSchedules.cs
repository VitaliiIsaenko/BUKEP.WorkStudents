using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    public interface IServiceSchedules
    {
        IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo);
        List<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo);
    }
}
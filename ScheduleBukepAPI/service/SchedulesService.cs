using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    internal class SchedulesService : BaseService ,ISchedulesService
    {
        public IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                { "dateFrom", dateFrom },
                { "dateTo", dateTo }
            };
            var json = ExecutePost("GetGroupLessons", parameters, idsSheduleGroup);
            return ConvertToList<GroupLesson>(json);
        }
        public List<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
        {
             {"idTeacher", idTeacher },
             { "dateFrom", dateFrom },
             { "dateTo", dateTo }
        };
            
            var json = ExecuteGet("GetTeacherLessons", parameters);
            return ConvertToList<GroupLesson>(json);
        }
    }
}

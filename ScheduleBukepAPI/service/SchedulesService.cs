using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    public class SchedulesService : BaseService, ISchedulesService
    {
        public IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                {"dateFrom", dateFrom},
                {"dateTo", dateTo}
            };
            var json = ExecutePost(MethodApi.GetGroupLessons, parameters, idsSheduleGroup);
            return ConvertToList<GroupLesson>(json);
        }

        public List<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                {"idTeacher", idTeacher},
                {"dateFrom", dateFrom},
                {"dateTo", dateTo}
            };

            var json = ExecuteGet(MethodApi.GetTeacherLessons, parameters);
            return ConvertToList<GroupLesson>(json);
        }
    }
}
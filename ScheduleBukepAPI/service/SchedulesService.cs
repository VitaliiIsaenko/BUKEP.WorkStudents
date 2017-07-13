using System.Collections.Generic;
using ScheduleBukepAPI.domain;

namespace ScheduleBukepAPI.service
{
    public class SchedulesService : BaseService, ISchedulesService
    {
        public IList<Lesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                {"dateFrom", dateFrom},
                {"dateTo", dateTo}
            };
            var json = ExecutePost(MethodApi.GetGroupLessons, parameters, idsSheduleGroup);
            return ConvertToList<Lesson>(json);
        }

        public IList<Lesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                {"idTeacher", idTeacher},
                {"dateFrom", dateFrom},
                {"dateTo", dateTo}
            };

            var json = ExecuteGet(MethodApi.GetTeacherLessons, parameters);
            return ConvertToList<Lesson>(json);
        }
    }
}
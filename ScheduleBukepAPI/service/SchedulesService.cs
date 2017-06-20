using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    internal class SchedulesService : BaseService ,ISchedulesService
    {
        public IList<Lesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                { "dateFrom", dateFrom },
                { "dateTo", dateTo }
            };
            var json = ExecutePost("GetGroupLessons", parameters, idsSheduleGroup);
            return ConvertToList<Lesson>(json);
        }
    }
}

using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;

namespace ScheduleBukepAPI.service
{
    internal class ServiceSchedules : IServiceSchedules
    {
        public IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var parameters = new Dictionary<string, string>
            {
                { "dateFrom", dateFrom },
                { "dateTo", dateTo }
            };
            var json = HttpRequstHelper.ExecutePost("GetGroupLessons", parameters, idsSheduleGroup);
            return JsonConvert.ConvertToList<GroupLesson>(json);
        }
    }
}

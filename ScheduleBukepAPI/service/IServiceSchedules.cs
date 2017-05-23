using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.ShedulerApi
{
    interface IServiceSchedules
    {
        List<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo);
    }
}
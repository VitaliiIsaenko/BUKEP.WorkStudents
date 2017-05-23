using System;
using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.ShedulerApi
{
    internal class ServiceSchedulesFake : IServiceSchedules
    {
        public List<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            List<GroupLesson> groupLessons = new List<GroupLesson>
            {
                DTOBuilderFake.CreateGroupLesson("GroupLesson 1"),
                DTOBuilderFake.CreateGroupLesson("GroupLesson 2"),
                DTOBuilderFake.CreateGroupLesson("GroupLesson 3"),
                DTOBuilderFake.CreateGroupLesson("GroupLesson 4")
            };
            return groupLessons;
        }
    }
}
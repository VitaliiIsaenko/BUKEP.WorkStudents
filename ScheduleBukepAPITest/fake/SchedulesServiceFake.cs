using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.service;

namespace ScheduleBukepAPITest.fake
{
    internal class SchedulesServiceFake : ISchedulesService
    {
        public IList<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
        {
            var groupLessons = new List<GroupLesson>
            {
                DtoFactoryFake.CreateGroupLesson("Lesson 1"),
                DtoFactoryFake.CreateGroupLesson("Lesson 2"),
                DtoFactoryFake.CreateGroupLesson("Lesson 3"),
                DtoFactoryFake.CreateGroupLesson("Lesson 4")
            };
            return groupLessons;
        }
        public IList<GroupLesson> GetTeacherLessons(string idTeacher, string dateFrom, string dateTo)
        {
            IList<GroupLesson> groupLessons = new List<GroupLesson>
            {
                DtoFactoryFake.CreateGroupLesson("GroupLesson 1"),
                DtoFactoryFake.CreateGroupLesson("GroupLesson 2"),
                DtoFactoryFake.CreateGroupLesson("GroupLesson 3"),
                DtoFactoryFake.CreateGroupLesson("GroupLesson 4")
            };
            return groupLessons;
        }
    }
}
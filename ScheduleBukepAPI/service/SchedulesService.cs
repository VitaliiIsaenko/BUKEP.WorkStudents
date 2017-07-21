using System;
using System.Collections.Generic;
using ScheduleBukepAPI.domain;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service.paremeters;

namespace ScheduleBukepAPI.service
{
    public class SchedulesService : BaseService, ISchedulesService
    {
        private readonly ParameterConstructor _parameterConstructor = new ParameterConstructor();

        public SchedulesService(HttpRequstHelper httpRequestHelper, JsonConvert jsonConvert) : base(httpRequestHelper, jsonConvert)
        {
        }

        public SchedulesService()
        {
        }

        public IList<Lesson> GetGroupLessons(IList<int> idsSheduleGroup,
            DateTime dateFrom, DateTime dateTo)
        {
            IDictionary<string, string> parameters = _parameterConstructor
                .SetParameter(NameParameterForApi.DateFrom, dateFrom)
                .SetParameter(NameParameterForApi.DateTo, dateTo)
                .GetResults();
            string json = ExecutePost(MethodApi.GetGroupLessons, parameters, idsSheduleGroup
            );
            return ConvertToList<Lesson>(json);
        }

        public IList<Lesson> GetTeacherLessons(IList<int> idsTeacher,
            DateTime dateFrom, DateTime dateTo)
        {
            IDictionary<string, string> parameters = _parameterConstructor
                .SetParameter(NameParameterForApi.DateFrom, dateFrom)
                .SetParameter(NameParameterForApi.DateTo, dateTo)
                .GetResults();

            string json = ExecutePost(MethodApi.GetTeacherLessons, parameters, idsTeacher);
            return ConvertToList<Lesson>(json);
        }
    }
}

using System.Collections.Generic;
using Bukep.ShedulerApi.apiDTO;
using Bukep.ShedulerApi;

class ServiceSchedules : IServiceSchedules
{
   

    public List<GroupLesson> GetGroupLessons(string idsSheduleGroup, string dateFrom, string dateTo)
    {
        var parameters = new Dictionary<string, string>
        {
            { "dateFrom", dateFrom },
            { "dateTo", dateTo }
        };
        string json = HttpRequstHelper.ExecutePost("GetGroupLessons", parameters, idsSheduleGroup);
        return JSONConvert.ConvertJSONToListDTO<GroupLesson>(json);
    }
}

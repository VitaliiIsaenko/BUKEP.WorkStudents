using System.Collections.Generic;
using Bukep.ShedulerApi;
using Bukep.ShedulerApi.apiDTO;

namespace Bukep.Sheduler.Controllers
{
    class IdentifySchedule : IController
    {
        private IdentifyScheduleActivity view;

        public IdentifySchedule(IdentifyScheduleActivity view)
        {
            this.view = view;
        }

        public void Update()
        {
            List<Faculty> faculties = FacadeAPI.GetFaculties();           
            view.ShowFaculty(faculties);
        }
    }
}
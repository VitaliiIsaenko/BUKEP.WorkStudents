using ScheduleBukepAPI;
using ScheduleBukepAPI.decorators;
using ScheduleBukepAPI.helpers;
using ScheduleBukepAPI.service;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Controller
    {
        //TODO: добавить FacadeApiFactory
        protected readonly FacadeApi FacadeApi = new FacadeApi(
            new FilterGroupDecorator(new FacultiesService()), 
            new SchedulesService()
            );
        protected readonly JsonConvert JsonConvert = new JsonConvert();

        public abstract void Update();
    }
}
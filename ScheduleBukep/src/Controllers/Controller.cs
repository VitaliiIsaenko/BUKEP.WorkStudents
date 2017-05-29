using ScheduleBukepAPI;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.Controllers
{
    internal abstract class Controller
    {
        protected readonly FacadeApi FacadeApi = new FacadeApi();
        protected readonly JsonConvert JsonConvert = new JsonConvert();

        public abstract void Update();
    }
}
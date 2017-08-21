using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Controller
    {
        private const string Tag = "Controller";

        protected readonly DataProvider DataProvider;
        
        //TODO: вынести view сюда
        protected Controller(BaseActivity activity)
        {
            DataProvider = new DataProviderCache(activity);
        }

        public abstract void Update();
    }
}
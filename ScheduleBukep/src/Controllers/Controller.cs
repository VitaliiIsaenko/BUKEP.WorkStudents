using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Controller
    {
        private const string Tag = "Controller";

        protected readonly DataProvider DataProvider;
        
        protected Controller(BaseActivity activity)
        {
            DataProvider = new DataProvider(activity);
        }

        public abstract void Update();
    }
}
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.Controllers
{
    public abstract class Controller<TView>  where TView: BaseActivity
    {
        /// <summary>
        /// View используется для отображения дынных
        /// </summary>
        public TView View { get; }
        private const string Tag = "Controller";

        protected readonly DataProvider DataProvider;
        
        protected Controller(TView view)
        {
            View = view;
            DataProvider = new DataProviderCache(view);
        }

        public abstract void Update();
    }
}
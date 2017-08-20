using Android.Support.V7.App;
using Android.Util;

namespace Bukep.Sheduler.View
{
    public abstract class BaseActivity : AppCompatActivity
    {
        /// <summary>
        /// Если true то приложение закроется при вызове ShowError
        /// </summary>
        public bool CloseIfHappenedExeption { get; set; }

        private const string Tag = "BaseActivity";

        protected BaseActivity()
        {
            CloseIfHappenedExeption = true;
        }

        /// <summary>
        /// Показать ошибку
        /// </summary>
        /// <param name="mesages">Сообщение ошибки</param>
        public void ShowError(string mesages)
        {
            Log.Error(Tag, "ShowError() mesages = " + mesages);
            var builder = new AlertDialog.Builder(this);
            builder
                .SetTitle(GetString(Resource.String.in_app_happened_error))
                .SetMessage(mesages)
                .SetNegativeButton("Ок", 
                (sender, args) =>
                {
                    if (CloseIfHappenedExeption)
                    {
                        Finish();
                    }
                });
            builder.Create().Show();
        }
    }
}
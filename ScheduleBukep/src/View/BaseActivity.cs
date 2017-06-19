using Android.Support.V7.App;
using Android.Util;

namespace Bukep.Sheduler
{
    public abstract class BaseActivity : AppCompatActivity
    {
        public bool CloseIfHappenedExeption { get; set; }
        private const string Tag = "BaseActivity";

        protected BaseActivity()
        {
            CloseIfHappenedExeption = true;
        }

        public void ShowError(string mesages)
        {
            Log.Error(Tag, "ShowError() mesages = " + mesages);
            var builder = new AlertDialog.Builder(this);
            //TODO: вынести в файл String
            builder.SetTitle("В приложении произошла ошибка!")
                .SetMessage(mesages)
                .SetNegativeButton("Ок", delegate
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
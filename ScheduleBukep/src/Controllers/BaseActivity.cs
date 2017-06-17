using Android.Support.V7.App;
using Android.Util;

namespace Bukep.Sheduler.Controllers
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
            builder.SetTitle("К сожалению, в приложении произошла ошибка!")
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
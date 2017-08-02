using System;
using System.Net;
using Android.Content;
using Android.Net;
using Android.Util;
using Bukep.Sheduler.View;

namespace Bukep.Sheduler.logic
{
    /// <summary>
    /// Нужен для проверки интернета перед выполнением каких либо действий.
    /// </summary>
    public class InternetChecker
    {
        private const string Tag = "InternetChecker";
        private ConnectivityManager _connectivityManager;
        private readonly BaseActivity _activity;

        public InternetChecker(BaseActivity activity)
        {
            _activity = activity;
        }

        /// <summary>
        /// Используется для проверки подключение к интернету перед выполнением функции.
        /// Если нет подключения выводит сообщение об ошибке.
        /// </summary>
        /// <typeparam name="TResult">Результат выполнения функции.</typeparam>
        /// <param name="func">Функция которую нужно выполнить.</param>
        /// <param name="defaultValue">Возвращает в случии неудачи.</param>
        /// <returns></returns>
        public TResult ExecuteOperation<TResult>(Func<TResult> func, TResult defaultValue)
        {
            if (!CheckInternetConnect())
            {
                FailedInternetConnect();
            }

            try
            {
                return func.Invoke();
            }
            catch (WebException e)
            {
                Log.Error(Tag, e.ToString());
                FailedInternetConnect();
                return defaultValue;
            }
        }

        private bool CheckInternetConnect()
        {
            if (_connectivityManager == null)
            {
                _connectivityManager = (ConnectivityManager) _activity.GetSystemService(Context.ConnectivityService);
            }

            NetworkInfo info = _connectivityManager.ActiveNetworkInfo;
            return info != null && info.IsConnected;
        }

        public void FailedInternetConnect()
        {
            //TODO: move in res
            _activity.ShowError("Отсутствует подключение к интернету.");
        }
    }
}
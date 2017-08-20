using System;
using Android.Content;
using Android.Util;
using ScheduleBukepAPI;
using ScheduleBukepAPI.helpers;

namespace Bukep.Sheduler.logic.extension
{
    public static class IntentExtension
    {
        private const string Tag = "IntentExtension";

        /// <summary>
        /// Получение json из Intent по ключу.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJson(this Intent intent, string key)
        {
            var json = intent.GetStringExtra(key);
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception($"Failed get json from intent. Key = {key}");
            }
            Log.Debug(Tag, "GetJsonFromeIntent() json = " + json);
            return json;
        }

        public static T GetObject<T>(this Intent intent, string key)
        {
            string json = intent.GetJson(key);
            return JsonConvert.ConvertTo<T>(json);
        }

        public static void PutObject<T>(this Intent intent, string key, T value)
        {
            intent.PutExtra(key, JsonConvert.ConvertToJson(value));
        }

        public static DateTime GetDateTime(this Intent intent, string key)
        {
            return DateTime.Parse(intent.GetStringExtra(key));
        }

        public static void PutDateTime(this Intent intent, string key, DateTime value)
        {
            intent.PutExtra(key, value.ToString(Api.DateTimeFormat));
        }
    }
}
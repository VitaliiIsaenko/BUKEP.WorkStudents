using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace Bukep.Sheduler.logic
{
    public class CacheHelper
    {
        private const string Tag = "CacheHelper";
        private static readonly IBlobCache Cache = BlobCache.LocalMachine;
        private static readonly IBlobCache UserData = BlobCache.UserAccount;

        /// <summary>
        /// Получаем кэш по ключу, 
        /// если кэша нет взять данные из переданной функции и записать в кэш.
        /// </summary>
        /// <typeparam name="T">Тип данных получаемых из кеша.</typeparam>
        /// <param name="key">Ключ кеша.</param>
        /// <param name="fetchFunc">Функция получения данных.Вывозится в случаи если данных нет в кеше.</param>
        /// <returns>Данные из кеша</returns>
        public static T GetOrPutCached<T>(string key, Func<T> fetchFunc)
        {
            try
            {
                T result = Cache.GetObject<T>(key).Wait();
                if (result == null)
                {
                    return PutCache(key, fetchFunc.Invoke());
                }
                return result;
            }
            catch (KeyNotFoundException)
            {
                return PutCache(key, fetchFunc.Invoke());
            }
        }

        //TODO: такой же как и GetOrPutInCached
        public static T GetOrPutUserData<T>(string key, Func<T> fetchFunc)
        {
            return UserData.GetOrCreateObject(key, fetchFunc, DateTimeOffset.MaxValue).Wait();
        }
        
        public static void ClearAll()
        {
            UserData.InvalidateAll();
            Cache.InvalidateAll();
        }

        /// <summary>
        /// Записываем данные в кэш и возвращаем их.
        /// </summary>
        /// <typeparam name="T">Тип добавляемых данных в кеша.</typeparam>
        /// <param name="key">Ключ кеша.</param>
        /// <param name="value">Данные которые нужно добавить.</param>
        /// <returns>value</returns>
        private static T PutCache<T>(string key, T value)
        {
            Cache.InsertObject(key, value);
            return value;
        }

        public static T PutUserData<T>(string key, T value)
        {
            UserData.InsertObject(key, value);
            return value;
        }

        public static T GetUserData<T>(string key)
        {
            return UserData.GetObject<T>(key).Wait();
        }

        public static void DeleteUserData(string key)
        {
            UserData.Invalidate(key);
        }
    }
}
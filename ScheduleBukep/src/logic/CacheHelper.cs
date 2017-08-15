using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;

namespace Bukep.Sheduler.logic
{
    public class CacheHelper
    {
        private const string Tag = "CacheHelper";
        private static readonly IBlobCache Cache = BlobCache.LocalMachine;

        /// <summary>
        /// Получаем кэш по ключу, 
        /// если кэша нет взять данные из переданной функции и записать в кэш.
        /// </summary>
        /// <typeparam name="T">Тип данных получаемых из кеша.</typeparam>
        /// <param name="key">Ключ кеша.</param>
        /// <param name="fetchFunc">Функция получения данных.Вывозится в случаи если данных нет в кеше.</param>
        /// <returns>Данные из кеша</returns>
        public static T GetAndPutInCached<T>(string key, Func<T> fetchFunc)
        {
            try
            {
                T result = Cache.GetObject<T>(key).Wait();
                if (result == null)
                {
                    return PutInCache(key, fetchFunc.Invoke());
                }
                return result;
            }
            catch (KeyNotFoundException)
            {
                return PutInCache(key, fetchFunc.Invoke());
            }
        }

        /// <summary>
        /// Записываем данные в кэш и возвращаем их.
        /// </summary>
        /// <typeparam name="T">Тип добавляемых данных в кеша.</typeparam>
        /// <param name="key">Ключ кеша.</param>
        /// <param name="value">Данные которые нужно добавить.</param>
        /// <returns>value</returns>
        private static T PutInCache<T>(string key, T value)
        {
            Cache.InsertObject(key, value);
            return value;
        }
    }
}
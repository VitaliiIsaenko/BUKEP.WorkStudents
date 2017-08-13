using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using Android.Util;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.logic
{
    public class CacheHelper
    {
        private const string Tag = "CacheHelper";
        private static readonly IBlobCache Cache = BlobCache.LocalMachine;
        private readonly InternetChecker _internetChecker;

        public CacheHelper(InternetChecker internetChecker)
        {
            _internetChecker = internetChecker;
        }


        public T GetCachedData<T>(string key, Func<T> fetchFunc)
        {
            return CacheIsExists(key) ? CacheExists(key, fetchFunc) : Overwrite(key, fetchFunc);
        }

        /// <summary>
        /// �������� ���������� �� ��� �� �����.
        /// </summary>
        /// <param name="key">True ���� ����������.</param>
        /// <returns></returns>
        private static bool CacheIsExists(string key)
        {
            try
            {
                return Cache.Get(key).Wait() != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private T CacheExists<T>(string key, Func<T> fetchFunc)
        {
            return _internetChecker.CheckInternetConnect() ? 
                GetDataIfHasCacheAndConnectionInternet(key, fetchFunc) :
                GetFromCache<T>(key);
        }

        private T GetDataIfHasCacheAndConnectionInternet<T>(string key, Func<T> fetchFunc)
        {
            Log.Info(Tag, "InternetConnect = true");
            DateTimeOffset? dateTimeOffset = Cache.GetObjectCreatedAt<T>(key).Wait();
            if (dateTimeOffset != null && CheckCacheOutdated(dateTimeOffset.Value))
            {
                Log.Info(Tag, "Cache Outdated");
                return Overwrite(key, fetchFunc);
            }
            return GetFromCache<T>(key);
        }

        /// <summary>
        /// �������� ������� �� ���
        /// </summary>
        /// <param name="dateTimeOffset">True ���� �������</param>
        /// <returns></returns>
        private static bool CheckCacheOutdated(DateTimeOffset dateTimeOffset)
        {
            var now = DateTime.Now;
            TimeSpan span = now - dateTimeOffset;
            return span.Minutes >= 1;
        }

        private static T GetFromCache<T>(string key)
        {
            Log.Info(Tag, "Return cache object");
            return Cache.GetObject<T>(key).Wait(); ;
        }

        private static T Overwrite<T>(string key, Func<T> fetchFunc)
        {
            Log.Info(Tag, "Invoke Overwrite()");
            T data = fetchFunc.Invoke();
            Cache.InsertObject(key, data);
            return data;
        }
    }
}
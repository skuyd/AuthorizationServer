using System;
using System.Collections.Generic;

namespace Pixysoft.Framework.Reflection.Controller
{
    internal class DynamicCacheFactory<T>
    {
        private Dictionary<int, T> caches = new Dictionary<int, T>();

        private static volatile DynamicCacheFactory<T> instance;

        private static object syncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        public static DynamicCacheFactory<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DynamicCacheFactory<T>();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(int key)
        {
            return caches.ContainsKey(key);
        }

        public T GetValue(int key)
        {
            if (!caches.ContainsKey(key))
                return default(T);

            return caches[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddValue(int key, T value)
        {
            lock (syncRoot)
            {
                if (caches.ContainsKey(key))
                {
                    return;
                }
                caches.Add(key, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAll()
        {
            caches.Clear();
        }
    }
}
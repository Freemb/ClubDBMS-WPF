using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;


namespace DataLibrary.Cache
{
    public static class CacheOps
	{
        //TODO Load members list. switch to new thread or async wait to continue loading the user interface
        private static readonly object lockMe = new object();

        public static List<model> GetFromCache<model>(string key, IModelConnector<model> connector = null)
        {
            List<model> data = MemoryCache.Default.Get(key) as List<model>;
            if (data == null)
            {
                lock (lockMe)
                {
                    data = MemoryCache.Default.Get(key) as List<model>;
                    if (data != null) return data;

                    data = connector?.Load(null, true);
                    if (connector != null)
                    {
                        var duration = DateTimeOffset.UtcNow.AddMinutes(30);
                        MemoryCache.Default.AddOrGetExisting(key, data, duration);
                    }
                }
            }
            return data; //returns null if cache has expired
        }



    }
}

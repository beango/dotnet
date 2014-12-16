using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace web.core.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public object Get(string key)
        {
            if (MemoryCache.Default.Contains(key))
            {
                return MemoryCache.Default.Get(key);
            }
            return null;
        }

        public void Set(string key, object data, double expiry)
        {
            MemoryCache.Default.Add(key, data, new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(expiry) });
        }
    }
}
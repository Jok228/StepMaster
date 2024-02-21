using Amazon.Runtime.Internal.Util;
using Infrastructure.MongoDb.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Cache.Implementation
{
    public class My_Cache_Implementate : IMy_Cache
    {
        IMemoryCache _cache;

        public My_Cache_Implementate(IMemoryCache memory)
        {
            _cache = memory;
        }

        public void DeleteObject(string key)
        {
            _cache.Remove(key);
        }

        public object GetObject(string key)
        {
            _cache.TryGetValue(key, out object value);            
            return value;
        }
        public void SetObject(string key, object value, int timeMinute = 0)
        {
            if (timeMinute == 0)
            {
                _cache.Set(key, value);
            }
            _cache.Set(key, value, TimeSpan.FromMinutes(timeMinute));
        }
    }
}

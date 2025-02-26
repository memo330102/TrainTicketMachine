using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace TrainTicketMachine.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return JsonSerializer.Deserialize<T>(value.ToString()!);
            }
            return default;
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            var json = JsonSerializer.Serialize(value);
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            _cache.Set(key, json, cacheEntryOptions);
        }

        public async Task RemoveCacheValueAsync(string key)
        {
            _cache.Remove(key);
        }
    }
}

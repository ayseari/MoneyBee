using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyBee.Common.Caching
{
    public class RedisDistributedCacheService : IRedisDistributedCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisDistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(
                key,
                json);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var json = await _cache.GetStringAsync(key);

            if (json == null)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }

}

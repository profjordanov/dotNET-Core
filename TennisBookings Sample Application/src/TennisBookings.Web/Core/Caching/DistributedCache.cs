using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TennisBookings.Web.Core.Caching
{
    public class DistributedCache<T> : IDistributedCache<T>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<DistributedCache<T>> _logger;

        private readonly string _cacheKeyPrefix;

        public DistributedCache(IDistributedCache distributedCache, ILogger<DistributedCache<T>> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;

            _cacheKeyPrefix = $"{typeof(T).Namespace}_{typeof(T).Name}_";
        }

        public async Task<(bool Found, T Value)> TryGetValueAsync(string key)
        {
            var value = await GetAsync(key);

            return (value != null, value);
        }

        public async Task<T> GetAsync(string key)
        {
            var cachedResult = await _distributedCache.GetStringAsync(CacheKey(key));

            return cachedResult == null ? default : DeserialiseFromString(cachedResult);
        }

        public async Task SetAsync(string key, T item, int minutesToCache)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions
                { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutesToCache) };

            var serialisedItemToCache = SerialiseForCaching(item);

            await _distributedCache.SetStringAsync(CacheKey(key), serialisedItemToCache, cacheEntryOptions);
        }

        public Task RemoveAsync(string key) => _distributedCache.RemoveAsync(CacheKey(key));

        private string CacheKey(string key) => $"{_cacheKeyPrefix}{key}";

        private T DeserialiseFromString(string cachedResult)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(cachedResult, new JsonSerializerSettings
                {
                    MaxDepth = 10
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deserialise from cached string");
                return default;
            }
        }

        private string SerialiseForCaching(T item)
        {
            if (item == null) return null;

            try
            {
                return JsonConvert.SerializeObject(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to serialise type '{Type}' for caching", typeof(T).FullName);
                throw;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using TennisBookings.Web.Core.Caching;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class CachedWeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly IDistributedCache<CurrentWeatherResult> _cache;

        public CachedWeatherForecaster(IWeatherForecaster weatherForecaster, 
            IDistributedCache<CurrentWeatherResult> cache)
        {
            _weatherForecaster = weatherForecaster;
            _cache = cache;
        }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var cacheKey = $"current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

            var (isCached, forecast) = await _cache.TryGetValueAsync(cacheKey);

            if (isCached)
                return forecast;

            var result = await _weatherForecaster.GetCurrentWeatherAsync();

            await _cache.SetAsync(cacheKey, result, 60);

            return result;
        }
    }
}
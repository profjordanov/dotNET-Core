using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WeatherService.Api.Models;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CurrentWeatherController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherResult>> Get(string city)
        {
            var random = new Random();

            // simulate a slow API response
            await Task.Delay(random.Next(100, 300));

            if (_memoryCache.TryGetValue(city, out var weather))
            {
                if (weather is WeatherResult result)
                {
                    return result;
                }
            }

            // create some random weather
            var condition = random.Next(1, 4);

            WeatherResult currentWeather;
            switch (condition)
            {
                case 1:
                    currentWeather = new WeatherResult
                    {
                        City = city,
                        Weather = new WeatherCondition
                        {
                            Description = "Sun",
                            Temperature = new Temperature { Min = 26, Max = 32 },
                            Wind = new Wind { Degrees = 190, Speed = 6 }
                        }
                    };
                    break;
                case 2:
                    currentWeather = new WeatherResult
                    {
                        City = city,
                        Weather = new WeatherCondition
                        {
                            Description = "Rain",
                            Temperature = new Temperature { Min = 8, Max = 14 },
                            Wind = new Wind { Degrees = 80, Speed = 3 }
                        }
                    };
                    break;
                case 3:
                    currentWeather = new WeatherResult
                    {
                        City = city,
                        Weather = new WeatherCondition
                        {
                            Description = "Cloud",
                            Temperature = new Temperature { Min = 12, Max = 18 },
                            Wind = new Wind { Degrees = 10, Speed = 1 }
                        }
                    };
                    break;
                default:
                    currentWeather = new WeatherResult
                    {
                        City = city,
                        Weather = new WeatherCondition
                        {
                            Description = "Snow",
                            Temperature = new Temperature { Min = -2, Max = 1 },
                            Wind = new Wind { Degrees = 240, Speed = 8 }
                        }
                    };
                    break;
            }

            _memoryCache.Set(city, currentWeather, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60 * 12)));

            return currentWeather;
        }
    }
}

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Domain;
using TennisBookings.Web.External;

namespace TennisBookings.Web.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherForecaster(IWeatherApiClient weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var currentWeather = await _weatherApiClient.GetWeatherForecastAsync();

            var result = new CurrentWeatherResult
            {
                Description = currentWeather.Weather.Description
            };

            return result;
        }
    }

    public class LoggingWeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly ILogger<IWeatherForecaster> _logger;

        public LoggingWeatherForecaster(IWeatherForecaster weatherForecaster, ILogger<IWeatherForecaster> logger)
        {
            _weatherForecaster = weatherForecaster;
            _logger = logger;
        }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            _logger.LogInformation("Starting weather service call");

            var result = await _weatherForecaster.GetCurrentWeatherAsync();

            _logger.LogInformation($"Received weather result: {result.Description}");

            return result;
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.External.Models;

namespace TennisBookings.Web.External
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherApiClient> _logger;

        public WeatherApiClient(HttpClient httpClient, IOptions<ExternalServicesConfig> options, ILogger<WeatherApiClient> logger)
        {
            var externalServicesConfig = options.Value;

            httpClient.BaseAddress = new Uri(externalServicesConfig.WeatherApiUrl);

            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherApiResult> GetWeatherForecastAsync()
        {
            const string path = "api/currentWeather/Brighton";

            try
            {
                var response = await _httpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsAsync<WeatherApiResult>();

                return content;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to get weather data from API");
            }

            return null;
        }
    }
}

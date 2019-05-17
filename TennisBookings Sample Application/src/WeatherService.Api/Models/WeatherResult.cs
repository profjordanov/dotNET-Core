namespace WeatherService.Api.Models
{
    public class WeatherResult
    {
        public string City { get; set; }

        public WeatherCondition Weather { get; set; }
    }
}

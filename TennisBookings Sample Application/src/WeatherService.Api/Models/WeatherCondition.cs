﻿namespace WeatherService.Api.Models
{
    public class WeatherCondition
    {
        public string Description { get; set; }
        
        public Wind Wind { get; set; }

        public Temperature Temperature { get; set; }
    }
}

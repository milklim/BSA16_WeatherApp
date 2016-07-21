using System.Collections.Generic;
using Newtonsoft.Json;


namespace weatherForecastApp.Models
{
    // Root object of JSON (request "http://api.openweathermap.org/data/2.5/forecast/daily? . . .")
    public class WeatherForecast7Days
    {
        public City city { get; set; }
        public int cnt { get; set; }
        [JsonProperty("list")]
        public List<Forecast> Forecasts { get; set; }
    }
}
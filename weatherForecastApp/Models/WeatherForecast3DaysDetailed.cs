using System.Collections.Generic;
using Newtonsoft.Json;


namespace weatherForecastApp.Models
{
    // Root object of JSON (request ""http://api.openweathermap.org/data/2.5/forecast? . . .")
    public class WeatherForecast3DaysDetailed
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        [JsonProperty("list")]
        public List<Forecast3HoursDetailed> Forecasts3Hours { get; set; }
    }
}
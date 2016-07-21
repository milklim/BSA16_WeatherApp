using System.Collections.Generic;


namespace weatherForecastApp.Models
{
    // Root object of JSON (request "http://api.openweathermap.org/data/2.5/weather? . . .")
    public class CurrentWeather
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
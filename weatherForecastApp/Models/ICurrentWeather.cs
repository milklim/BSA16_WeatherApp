using System;
namespace weatherForecastApp.Models
{
    interface ICurrentWeather : IForecast
    {
        Clouds clouds { get; set; }
        int cod { get; set; }
        Coord coord { get; set; }
        int dt { get; set; }
        int id { get; set; }
        Main main { get; set; }
        string name { get; set; }
        Sys sys { get; set; }
        System.Collections.Generic.List<Weather> weather { get; set; }
        Wind wind { get; set; }
    }
}

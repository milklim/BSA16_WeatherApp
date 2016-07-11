using System;
namespace weatherForecastApp.Models
{
    interface IWeatherForecast7Days : IForecast
    {
        City city { get; set; }
        int cnt { get; set; }
        System.Collections.Generic.List<Forecast> Forecasts { get; set; }
    }
}

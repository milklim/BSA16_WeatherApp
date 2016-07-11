using System;
namespace weatherForecastApp.Models
{
    interface IWeatherForecast3DaysDetailed : IForecast
    {
        City city { get; set; }
        int cnt { get; set; }
        string cod { get; set; }
        System.Collections.Generic.List<Forecast3HoursDetailed> Forecasts3Hours { get; set; }
        double message { get; set; }
    }
}

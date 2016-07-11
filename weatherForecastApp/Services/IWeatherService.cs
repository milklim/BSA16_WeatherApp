using System;
using weatherForecastApp.Models;
namespace weatherForecastApp.Services
{
    public interface IWeatherService
    {
        IForecast GetForecast(string cityName, TypeOfForecast type);
    }
}

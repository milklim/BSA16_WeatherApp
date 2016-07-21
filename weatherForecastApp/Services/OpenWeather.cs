using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using weatherForecastApp.Models;
using Newtonsoft.Json;

namespace weatherForecastApp.Services
{
    public class OpenWeather : WeatherService
    {
        private static readonly string appId = "a72af90b408ef423db6c2dcc77c4fb55";
        private static readonly Dictionary<Type, string> apiReqParams = new Dictionary<Type, string>()
        {
            { typeof(CurrentWeather), "weather" },
            { typeof(WeatherForecast3DaysDetailed), "forecast" },
            { typeof(WeatherForecast7Days), "forecast/daily" }
        };

        public OpenWeather(IRequestSender reqSenderParam) : base(reqSenderParam)
        {
        }

        protected override string SendApiRequest<T>(string cityName)
        {
            string queryString = String.Format("http://api.openweathermap.org/data/2.5/{0}?q={1}&type=like&units=metric&lang=ru&appid={2}", 
                                                apiReqParams[typeof(T)] , cityName, appId);
            string response = requestSender.SendRequest(queryString);
            return response;
        }

        protected override T DeserializeResponse<T>(string response)
        {
            T forecast = JsonConvert.DeserializeObject<T>(response);
            return forecast;
        }
    }
}
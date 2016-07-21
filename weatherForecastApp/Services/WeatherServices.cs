using Ninject;
using System;
using weatherForecastApp.Models;

namespace weatherForecastApp.Services
{
    public abstract class WeatherService
    {
        protected IRequestSender requestSender;

        public WeatherService(IRequestSender reqSenderParam)
        {
            requestSender = reqSenderParam; 
        }


        public T GetForecast<T>(string cityName)
        {
            string response = SendApiRequest<T>(cityName);
            T forecast = DeserializeResponse<T>(response);
            return forecast;
        }


        protected abstract string SendApiRequest<T>(string cityName);
        protected abstract T DeserializeResponse<T>(string response);

    }
}
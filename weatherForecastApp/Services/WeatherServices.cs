using Ninject;
using System;
using weatherForecastApp.Models;

namespace weatherForecastApp.Services
{
    public class WeatherService : IWeatherService
    {
        private static string appId = System.Web.Configuration.WebConfigurationManager.AppSettings["openWeatherAppId"];
        private IRequestSender requestSender;
        public WeatherService(IRequestSender reqSenderParam)
        {
            requestSender = reqSenderParam; 
        }

        public IForecast GetForecast(string cityName, TypeOfForecast type)
        {
            IKernel ninjectKernel = new StandardKernel();

            string queryParam = string.Empty;
            switch (type)
            {
                case TypeOfForecast.CurrentWeather:
                    queryParam = "weather";
                    ninjectKernel.Bind<IForecast>().To<CurrentWeather>();
                    break;
                case TypeOfForecast.For3Days:
                    queryParam = "forecast";
                    ninjectKernel.Bind<IForecast>().To<WeatherForecast3DaysDetailed>();
                    break;
                case TypeOfForecast.For7Days:
                    queryParam = "forecast/daily";
                    ninjectKernel.Bind<IForecast>().To<WeatherForecast7Days>();
                    break;
            }
            string queryString = String.Format("http://api.openweathermap.org/data/2.5/{0}?q={1}&type=like&units=metric&lang=ru&appid={2}", queryParam, cityName, appId);
            string response = requestSender.SendRequest(queryString);

            IForecast weather = ninjectKernel.Get<IForecast>();
            object ob = Newtonsoft.Json.JsonConvert.DeserializeObject(response, weather.GetType());

            return ob as IForecast; 
        }

    }
}
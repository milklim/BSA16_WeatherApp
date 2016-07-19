using System;
using System.Collections.Generic;
using System.Web.Mvc;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;
using weatherForecastApp.Services;

namespace weatherForecastApp.Controllers
{
    public class HomeController : Controller
    {
        private IWeatherService wService;
        private UserContext db = new UserContext();
        const string defaultCity = "Kiev";
        public HomeController(IWeatherService wServiceParam)
        {
            wService = wServiceParam;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CurrentWeather(string cityName = defaultCity)
        {
            CurrentWeather wView = wService.GetForecast(cityName, TypeOfForecast.CurrentWeather) as CurrentWeather;
            WriteHistory(wView.id);
            return View(wView);
        }


        public ActionResult Forecast3Days(string cityName = defaultCity)
        {
            WeatherForecast3DaysDetailed wView = wService.GetForecast(cityName, TypeOfForecast.For3Days) as WeatherForecast3DaysDetailed;
            return View(wView);
        }


        public ActionResult Forecast7Days(string cityName = defaultCity)
        {
            WeatherForecast7Days wView = wService.GetForecast(cityName, TypeOfForecast.For7Days) as WeatherForecast7Days;
            return View(wView);
        }

        [HttpPost]
        public ActionResult PostRequestHandler(string cityName)
        {
            if (cityName  == string.Empty) return RedirectToAction("Index");
            return RedirectToAction("CurrentWeather", new { cityName = cityName });
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        private void WriteHistory(int cityId)
        {
            User currUser = (Request.Cookies["UserId"] != null) ? db.Users.Find(Guid.Parse(Request.Cookies["UserId"].Value)) : null;
            if (currUser == null)
            {
                currUser = new User();
                db.Users.Add(currUser);
                db.SaveChanges();

                Response.Cookies["UserId"].Value = currUser.UserId.ToString();
                Response.Cookies["UserId"].Expires = DateTime.Now.AddMonths(6);
            }

            var query = "INSERT INTO [Histories] (UserId, CityId, Date) VALUES ({0}, {1}, {2})";
            db.Database.ExecuteSqlCommand(query, currUser.UserId, cityId, DateTime.Now);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
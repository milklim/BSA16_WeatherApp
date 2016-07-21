using System;
using System.Web.Mvc;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;
using weatherForecastApp.Services;


namespace weatherForecastApp.Controllers
{
    public class HomeController : Controller
    {
        private WeatherService wService;
        private UserContext db = new UserContext();
        const string defaultCity = "Kiev";
        public HomeController(WeatherService wServiceParam)
        {
            wService = wServiceParam;
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CurrentWeather(string cityName = defaultCity)
        {
            CurrentWeather wView = wService.GetForecast<CurrentWeather>(cityName);
            WriteHistory(wView);
            return View(wView);
        }


        public ActionResult Forecast3Days(string cityName = defaultCity)
        {
            WeatherForecast3DaysDetailed wView = wService.GetForecast<WeatherForecast3DaysDetailed>(cityName);
            return View(wView);
        }


        public ActionResult Forecast7Days(string cityName = defaultCity)
        {
            WeatherForecast7Days wView = wService.GetForecast<WeatherForecast7Days>(cityName);
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

        private void WriteHistory(CurrentWeather forecast)
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

            City favorCity = db.Cities.Find(forecast.id);
            if (favorCity == null)
            {
                favorCity = new City() { CityId = forecast.id, name = forecast.name, country = forecast.sys.country };
                db.Cities.Add(favorCity);
                db.SaveChanges();
            }

            var query = "INSERT INTO [Histories] (UserId, CityId, Date) VALUES ({0}, {1}, {2})";
            db.Database.ExecuteSqlCommand(query, currUser.UserId, favorCity.CityId, DateTime.Now);
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
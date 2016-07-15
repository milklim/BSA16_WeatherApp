using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;
using weatherForecastApp.Services;

namespace weatherForecastApp.Controllers
{
    public class UsersController : Controller
    {
        [Inject]
        private IRequestSender requestSender { get; set; }
        private UserContext db = new UserContext();



        public ActionResult Favorites(int cityId)
        {
            City favorCity = db.Cities.Find(cityId); 
            if (favorCity == null)
            {
                requestSender = DependencyResolver.Current.GetService<IRequestSender>();
                string queryString = requestSender.SendRequest(string.Format("http://api.openweathermap.org/data/2.5/{0}?id={1}&units=metric&lang=ru&appid={2}",
                                                      "forecast/daily", cityId, System.Web.Configuration.WebConfigurationManager.AppSettings["openWeatherAppId"]));
                queryString = queryString.Substring(8, queryString.IndexOf(",\"population") - 8) + "}";
                favorCity = Newtonsoft.Json.JsonConvert.DeserializeObject<City>(queryString);
                db.Cities.Add(favorCity);
            }


            User currUser = (Request.Cookies["UserId"] != null) ? db.Users.Find(Guid.Parse(Request.Cookies["UserId"].Value)) : null;
            if (currUser != null)
            {
                currUser.FavorCities.Add(favorCity);
            }
            else
            {
                currUser = new User();
                currUser.FavorCities.Add(favorCity);
                db.Users.Add(currUser);
                Response.Cookies["UserId"].Value = currUser.UserId.ToString();
                Response.Cookies["UserId"].Expires = DateTime.Now.AddMonths(6);
            }
            db.SaveChanges();


            List<City> Favorites = new List<City>(db.Users.Find(currUser.UserId).FavorCities);
            return View(Favorites);
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

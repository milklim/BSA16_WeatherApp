using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult ViewFavorites()
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

            List<City> Favorites = new List<City>(db.Users.Find(currUser.UserId).FavorCities);
            return View(Favorites);
        }

        public ActionResult AddToFavorites(int cityId)
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
                db.SaveChanges();
            }
            else
            {
                currUser = new User();
                currUser.FavorCities.Add(favorCity);
                db.Users.Add(currUser);
                db.SaveChanges();

                Response.Cookies["UserId"].Value = currUser.UserId.ToString();
                Response.Cookies["UserId"].Expires = DateTime.Now.AddMonths(6);
            }

            return RedirectToAction("ViewFavorites");
        }

        public ActionResult Stats()
        {
            IEnumerable<History> history;
            if (Request.Cookies["UserId"] != null)
            {
                var guid = Guid.Parse(Request.Cookies["UserId"].Value);
                 history = db.History.Include(o => o.city).Where<History>(c => c.UserId == guid); 
            }
            else
            {
                 history = new List<History>();
            }
            return View(history);
        }

        public ActionResult Delete(int CityId)
        {
            var query = @"DELETE FROM [UserCities] WHERE User_UserId = {0} AND City_CityId = {1}";
            db.Database.ExecuteSqlCommand(query, Guid.Parse(Request.Cookies["UserId"].Value), CityId);
            return RedirectToAction("ViewFavorites");
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

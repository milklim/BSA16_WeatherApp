using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;


namespace weatherForecastApp.Controllers
{
    public class UsersController : Controller
    {
        private UserContext db = new UserContext();


        public ActionResult ViewFavorites()
        {
            User user = GetUser();
            List<City> Favorites = new List<City>(db.Users.Find(user.UserId).FavorCities);
            return View(Favorites.OrderBy(c => c.name));
        }


        public ActionResult AddToFavorites(int cityId)
        {
            City favorCity = db.Cities.Find(cityId); 
            User currUser = GetUser();
            currUser.FavorCities.Add(favorCity);
            db.SaveChanges();
            return RedirectToAction("ViewFavorites");
        }


        public ActionResult Stats()
        {
            User user = GetUser();
            IEnumerable<History> history = db.History.Include(o => o.city).Where<History>(c => c.UserId == user.UserId);
            return View(history);
        }


        public ActionResult DeleteFromFavorites(int CityId)
        {
            var query = @"DELETE FROM [UserCities] WHERE User_UserId = {0} AND City_CityId = {1}";
            db.Database.ExecuteSqlCommand(query, Guid.Parse(Request.Cookies["UserId"].Value), CityId);
            return RedirectToAction("ViewFavorites");
        }


        public ActionResult ClearHistory()
        {
            User user = GetUser(); 
            var query = @"DELETE FROM [Histories] WHERE UserId = {0}";
            db.Database.ExecuteSqlCommand(query, user.UserId);
            return View("Stats", new List<History>());
        }


        private User GetUser()
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
            return currUser;
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

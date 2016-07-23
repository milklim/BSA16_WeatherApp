using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;


namespace weatherForecastApp.Services
{
    public static class DbAsyncHelper
    {
        private static UserContext db = new UserContext();


        public static async Task<User> GetUserAsync()
        {
            User currUser = null;
            if (HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                currUser = await db.Users.FindAsync(Guid.Parse(HttpContext.Current.Request.Cookies["UserId"].Value));
            }
            if (currUser == null)
            {
                currUser = new User();
                db.Users.Add(currUser);
                await db.SaveChangesAsync();
                HttpContext.Current.Response.Cookies["UserId"].Value = currUser.UserId.ToString();
                HttpContext.Current.Response.Cookies["UserId"].Expires = DateTime.Now.AddMonths(6);
            }
            return currUser;
        }


        public static async Task<List<City>> AddToFavoritesAsync(int cityId)
        {
            User currUser = await GetUserAsync();
            currUser.FavorCities.Add(await db.Cities.FindAsync(cityId));
            await db.SaveChangesAsync();
            return currUser.FavorCities.ToList<City>();
        }


        public static async Task<List<City>> DeleteFromFavoritesAsync(int cityId)
        {
            User user = await GetUserAsync();
            user.FavorCities.Remove(await db.Cities.FindAsync(cityId));
            await db.SaveChangesAsync();
            return user.FavorCities.ToList<City>();
        }


        public static async Task WriteHistoryAsync(CurrentWeather cw)
        {
            User user = await GetUserAsync();
            City city = await db.Cities.FindAsync(cw.id);
            if (city == null)
            {
                city = new City() { CityId = cw.id, name = cw.name, country = cw.sys.country };
                db.Cities.Add(city);
            }
            History hist = new History() { UserId = user.UserId, CityId = city.CityId, Date = DateTime.Now };
            db.History.Add(hist);
            await db.SaveChangesAsync();
        }


        public static async Task<List<History>> GetHistoryAsync()
        {
            User user = await GetUserAsync();
            var query = from h in db.History.Include(h => h.city)
                        where h.UserId == user.UserId
                        select h;
            List<History> history = await query.ToListAsync();
            return history;
        }


        public static async Task ClearHistoryAsync()
        {
            User user = await GetUserAsync();
            await db.Database.ExecuteSqlCommandAsync(@"DELETE FROM [Histories] WHERE UserId = {0}",
                                                     user.UserId);
        }
    }
}
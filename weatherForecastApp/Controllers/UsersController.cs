using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using weatherForecastApp.Infrastructure;
using weatherForecastApp.Models;
using weatherForecastApp.Services;

namespace weatherForecastApp.Controllers
{
    public class UsersController : Controller
    {
        private UserContext db = new UserContext();


        public async Task<ActionResult> ViewFavorites()
        {
            User user = await DbAsyncHelper.GetUserAsync();
            return View(user.FavorCities.OrderBy(c => c.name));
        }


        public async Task<ActionResult> AddToFavorites(int cityId)
        {
            List<City> favorCities = await DbAsyncHelper.AddToFavoritesAsync(cityId);
            return View("ViewFavorites", favorCities);
        }

        public async Task<ActionResult> Stats()
        {
            List<History> history = await DbAsyncHelper.GetHistoryAsync();
            return View(history);
        }


        public async  Task<ActionResult> DeleteFromFavorites(int cityId)
        {
            List<City> favorCities = await DbAsyncHelper.DeleteFromFavoritesAsync(cityId);
            return View("ViewFavorites", favorCities);
        }


        public async Task<ActionResult> ClearHistory()
        {
            await DbAsyncHelper.ClearHistoryAsync();
            return View("Stats", new List<History>());
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

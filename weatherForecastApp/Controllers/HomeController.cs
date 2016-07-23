using System.Net.Http;
using System.Threading.Tasks;
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


        public async Task<ActionResult> CurrentWeather(string cityName = defaultCity)
        {
            CurrentWeather wView;
            try
            {
                wView = await wService.GetForecastAsync<CurrentWeather>(cityName);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.err = ex.Message;
                return View("Error");
            }
            await DbAsyncHelper.WriteHistoryAsync(wView);
            return View(wView);
        }


        public async Task<ActionResult> Forecast3Days(string cityName = defaultCity)
        {
            WeatherForecast3DaysDetailed wView;
            try
            {
                wView = await wService.GetForecastAsync<WeatherForecast3DaysDetailed>(cityName);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.err = ex.Message;
                return View("Error");
            }
            return View(wView);
        }


        public async Task<ActionResult> Forecast7Days(string cityName = defaultCity)
        {
            WeatherForecast7Days wView;
            try
            {
                wView = await wService.GetForecastAsync<WeatherForecast7Days>(cityName);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.err = ex.Message;
                return View("Error");
            }
            return View(wView);
        }


        [HttpPost]
        public ActionResult PostRequestHandler(string cityName)
        {
            if (cityName == string.Empty)
            {
                return RedirectToAction("Index");
            }
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
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherHub.Models;
using SimpleWeather;

namespace WeatherHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            WeatherController weatherController = new WeatherController();
            WeatherForecast forecast = await weatherController.GetWeatherForecast(43.13, 24.71);
            double rain = forecast.Current.Rain != null ? forecast.Current.Rain.OneHour : 0;

            ViewBag.Forecast = forecast;
            ViewBag.Rain = rain;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
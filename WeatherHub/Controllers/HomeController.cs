using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherHub.Models;
using SimpleWeather;
using System;
using System.Web;
using Microsoft.AspNetCore.Http.Features;

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

            HttpContext context = HttpContext;

            string ip = GetClientIPAddress(context);
            var ipOtherOption = Request.HttpContext.Connection.RemoteIpAddress;

            ViewBag.Forecast = forecast;
            ViewBag.Rain = rain;

            return View();
        }

        private static string GetClientIPAddress(HttpContext context)
        {
            string ip = string.Empty;

            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }

            return ip;
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
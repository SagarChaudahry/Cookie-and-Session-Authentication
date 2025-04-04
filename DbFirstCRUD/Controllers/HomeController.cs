using System.Diagnostics;
using DbFirstCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace DbFirstCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            if (userId != null && !string.IsNullOrEmpty(userName))
            {
                ViewBag.UserName = userName;
                return View();
            }

            return RedirectToAction("Login", "Authentication");
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

using System.Diagnostics;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    // Home is the controller, The name "Controller" is used in MVC to designate this as a controller.
    // Views of the controller will be in /views/Home.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // IActionResult returns the server rendered view of the page.
        // This method is an action and you can either use view() or view(Privacy)
        // It automatically grabs the method name if it matches the view routing path which is /Home/Index
        public IActionResult Index()
        {
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

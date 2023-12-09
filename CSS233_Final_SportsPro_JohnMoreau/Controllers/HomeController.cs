using john_moreau_MidTerm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

/*
* John Moreau
* CSS233
* 10/28/2023
*
*
*/

namespace john_moreau_MidTerm.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private SportsContext Context { get; set; }

        public HomeController(SportsContext ctx) => Context = ctx;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Registrations()
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
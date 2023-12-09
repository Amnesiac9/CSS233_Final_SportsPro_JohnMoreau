using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

/*
* John Moreau
* CSS233
* 12/9/2023
*
*
*/

namespace john_moreau_MidTerm.Controllers
{
    public class HomeController : Controller
    {

        private SportsContext Context { get; set; }

        public HomeController(SportsContext ctx) => Context = ctx;


        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("Registrations")]
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
using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSS233_Final_SportsPro_JohnMoreau.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsContext Context { get; set; }
        public RegistrationController(SportsContext ctx) => Context = ctx;

        [Route("Registration")]
        public IActionResult GetCustomer()
        {
            var registration = new RegistrationViewModel();
            registration.Customers = Context.Customers.ToList() ;

            return View(registration);
        }

        //[Route("Registration/{id?}")]
        public IActionResult GetItems(RegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                registration = new RegistrationViewModel();
                registration.Customers = Context.Customers.ToList();

                return View("GetCustomer", registration);
            }
            // Get the items and registrations for this customer
            var customer = Context.Customers.Find(registration.CurrentCustomerId);
            var products = Context.Products.ToList();

            registration = new RegistrationViewModel
            {
                CurrentCustomerId = registration.CurrentCustomerId,
                Customer = customer,
                Products = products,
            };

            return View(registration);

            // Debug send back to home
            //return RedirectToAction("GetCustomer");
        }
    }
}

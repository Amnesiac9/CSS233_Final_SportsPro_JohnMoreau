using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var customer = Context.Customers.Include(c => c.Registrations).FirstOrDefault(i => i.Id == registration.CurrentCustomerId);
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

        public IActionResult RegisterItem(RegistrationViewModel model)
        {
            var product = Context.Products.Find(model.CurrentProductId);
            var customer = Context.Customers.Find(model.CurrentCustomerId);
            if (product != null && customer != null)
            {
                // TODO: Check if customer already has the item registered

                var registration = new Registration(customer, product);
                Context.Add(registration);
                Context.SaveChanges();
                TempData["SuccessMessage"] = product?.Name + " has been added to registrations.";
            }

            return RedirectToAction("GetItems", model);
        }

        public IActionResult Delete(RegistrationViewModel model)
        {

            var product = Context.Products.Find(model.CurrentProductId);
            var registration = Context.Registrations.FirstOrDefault(i => i.CustomerId == model.CurrentCustomerId && i.ProductId == model.CurrentProductId); ;

            if (registration != null)
            {
                Context.Registrations.Remove(registration);
                Context.SaveChanges();
                TempData["SuccessMessage"] =  product?.Name+ " has been removed from registrations.";
            }

            return RedirectToAction("GetItems", model);
        }
    }
}

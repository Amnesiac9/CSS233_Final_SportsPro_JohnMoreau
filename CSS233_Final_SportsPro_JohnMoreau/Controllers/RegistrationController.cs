using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSS233_Final_SportsPro_JohnMoreau.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsContext Context { get; set; }
        public RegistrationController(SportsContext ctx) => Context = ctx;

        [Route("Registration")]
        public IActionResult GetCustomer()
        {
            var registration = new RegistrationViewModel { Customers = Context.Customers.ToList() };
            return View(registration);
        }

        [Route("Registration/Products/{Slug?}")]
        public IActionResult GetItems(RegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                registration = new RegistrationViewModel { Customers = Context.Customers.ToList() };
                return View("GetCustomer", registration);
            }

            // Get the items and registrations for this customer, include registrations
            var customer = Context.Customers.Include(c => c.Registrations).FirstOrDefault(i => i.Id == registration.CurrentCustomerId);
            var products = Context.Products.ToList();

            // Create a new registrationview with the customer's registrations sorted by the current sort order
            registration = new RegistrationViewModel(registration.SortBy ?? "", registration.SortOrder ?? "", registration.CurrentCustomerId, products, customer);

            return View("GetItems", registration);

        }

        public IActionResult RegisterItem(RegistrationViewModel model)
        {


            if (!ModelState.IsValid)
            {
                // Get the items and registrations for this customer
                model.Customer = Context.Customers.Include(c => c.Registrations).FirstOrDefault(i => i.Id == model.CurrentCustomerId);
                model.Products = Context.Products.ToList();
                return View("GetItems", model);
            }

            var product = Context.Products.Find(model.CurrentProductId);
            var customer = Context.Customers.Include(c => c.Registrations).FirstOrDefault(i => i.Id == model.CurrentCustomerId);

            if (product != null && customer != null && customer.Registrations != null)
            {

                var registration = Context.Registrations.FirstOrDefault(i => i.CustomerId == model.CurrentCustomerId && i.ProductId == model.CurrentProductId);
                
                if (registration == null)
                {
                    registration = new Registration(customer, product);
                    Context.Add(registration);
                    Context.SaveChanges();
                    TempData["SuccessMessage"] = product?.Name + " has been added to registrations.";
                }
                else
                {
                    TempData["ErrorMessage"] = product.Name + " already registered to " + customer.Name + ".";
                }

            }
            else
            {
                TempData["ErrorMessage"] = "Error registering product, please contact support.";
            }

            return RedirectToAction("GetItems", model);
 
        }

        public IActionResult Delete(RegistrationViewModel model)
        {

            var product = Context.Products.Find(model.CurrentProductId);
            var registration = Context.Registrations.FirstOrDefault(i => i.CustomerId == model.CurrentCustomerId && i.ProductId == model.CurrentProductId);

            if (registration != null)
            {
                Context.Registrations.Remove(registration);
                Context.SaveChanges();
                TempData["SuccessMessage"] =  product?.Name+ " has been removed from registrations.";
            }
            else
            {
                TempData["ErrorMessage"] = "Registration not found.";
            }

            return RedirectToAction("GetItems", model);
        }
    }
}

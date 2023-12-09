//using john_moreau_MidTerm.Migrations;
using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

/*
* John Moreau
* CSS233
* 10/28/2023
*
*
*/

namespace john_moreau_MidTerm.Controllers
{
    public class CustomerController : Controller
    {
        private SportsContext Context { get; set; }
        public CustomerController(SportsContext ctx) => Context = ctx;

        [Route("Customers")]
        public IActionResult List(string sortBy, string sortOrder)
        {
            var customers = Context.Customers;
            //var countries = Context.Countries.ToList();

            switch (sortBy)
            {
                case "Name":
                    ViewData["NameSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(customers.OrderBy(m => m.FirstName).ToList()),
                        "desc" => View(customers.OrderByDescending(m => m.FirstName).ToList()),
                        _ => View(customers.OrderBy(m => m.Id).ToList()),
                    };
                case "Email":
                    ViewData["EmailSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(customers.OrderBy(m => m.Email).ToList()),
                        "desc" => View(customers.OrderByDescending(m => m.Email).ToList()),
                        _ => View(customers.OrderBy(m => m.Id).ToList()),
                    };
                case "City":
                    ViewData["CitySortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(customers.OrderBy(m => m.City).ToList()),
                        "desc" => View(customers.OrderByDescending(m => m.City).ToList()),
                        _ => View(customers.OrderBy(m => m.Id).ToList()),
                    };
                default:
                    return View(customers.OrderBy(m => m.Id).ToList());

            }
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Countries = Context.Countries.ToList();
            ViewBag.Action = "Add";
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = Context.Customers.Find(id);
            ViewBag.Countries = Context.Countries.ToList();
            ViewBag.Action = "Edit";
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Email = customer.Email?.ToLower();

                if (customer.Id == 0)
                {
                    customer.DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt");
                    Context.Customers.Add(customer);

                }
                else
                {
                    Context.Customers.Update(customer);
                }

                Context.SaveChanges();
                return RedirectToAction("List", "Customer");

            }
            else
            {
                ViewBag.Countries = Context.Countries.ToList();
                ViewBag.Action = (customer.Id == 0) ? "Add" : "Edit";
                if (customer.Id == 0)
                {
                    return View("Edit", customer);
                }
                else
                {
                    return View(customer);
                }

            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = Context.Customers.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            Context.Customers.Remove(customer);
            Context.SaveChanges();
            return RedirectToAction("List", "Customer");
        }
    }
}

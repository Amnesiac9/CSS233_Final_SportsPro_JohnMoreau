/*
* John Moreau
* CSS233
* 12/9/2023
* 
*/

using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace john_moreau_MidTerm.Controllers
{
    public class ProductController : Controller
    {
        private SportsContext Context { get; set; }
        public ProductController(SportsContext ctx) => Context = ctx;

        [Route("Products")]
        public IActionResult List(string sortBy, string sortOrder)
        {
            var products = Context.Products;

            switch (sortBy)
            {
                case "Code":
                    ViewData["CodeSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(products.OrderBy(m => m.Code).ToList()),
                        "desc" => View(products.OrderByDescending(m => m.Code).ToList()),
                        _ => View(products.OrderBy(m => m.Id).ToList()),
                    };
                case "Name":
                    ViewData["NameSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(products.OrderBy(m => m.Name).ToList()),
                        "desc" => View(products.OrderByDescending(m => m.Name).ToList()),
                        _ => View(products.OrderBy(m => m.Id).ToList()),
                    };
                case "Price":
                    ViewData["PriceSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(products.OrderBy(m => m.Price).ToList()),
                        "desc" => View(products.OrderByDescending(m => m.Price).ToList()),
                        _ => View(products.OrderBy(m => m.Id).ToList()),
                    };
                case "ReleaseDate":
                    ViewData["ReleaseDateSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(products.OrderBy(m => m.ReleaseDate).ToList()),
                        "desc" => View(products.OrderByDescending(m => m.ReleaseDate).ToList()),
                        _ => View(products.OrderBy(m => m.Id).ToList()),
                    };
                default:
                    return View(products.OrderBy(m => m.Id).ToList());

            }
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = Context.Products.Find(id);
            ViewBag.Action = "Edit";
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    product.DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt");
                    Context.Products.Add(product);

                }
                else
                {
                    Context.Products.Update(product);
                }

                Context.SaveChanges();
                return RedirectToAction("List", "Product");

            }
            else
            {
                ViewBag.Action = (product.Id == 0) ? "Add" : "Edit";
                if (product.Id == 0)
                {
                    return View("Edit", product);
                }
                else
                {
                    return View(product);
                }

            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = Context.Products.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            Context.Products.Remove(product);
            Context.SaveChanges();
            return RedirectToAction("List", "Product");
        }
    }
}

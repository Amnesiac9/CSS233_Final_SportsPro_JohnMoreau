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
    public class IncidentController : Controller
    {
        private SportsContext Context { get; set; }
        public IncidentController(SportsContext ctx) => Context = ctx;

        [Route("Incidents")]
        public IActionResult List(string sortBy, string sortOrder)
        {
            var incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product);

            switch (sortBy)
            {
                case "Title":
                    ViewData["TitleSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(incidents.OrderBy(m => m.Title).ToList()),
                        "desc" => View(incidents.OrderByDescending(m => m.Title).ToList()),
                        _ => View(incidents.OrderBy(m => m.Id).ToList()),
                    };
                case "Customer":
                    ViewData["CustomerSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(incidents.OrderBy(m => m.Customer == null ? "" : m.Customer.FirstName).ToList()),
                        "desc" => View(incidents.OrderByDescending(m => m.Customer == null ? "" : m.Customer.FirstName).ToList()),
                        _ => View(incidents.OrderBy(m => m.Id).ToList()),
                    };
                case "Product":
                    ViewData["ProductSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(incidents.OrderBy(m => m.Product == null ? "" : m.Product.Name).ToList()),
                        "desc" => View(incidents.OrderByDescending(m => m.Product == null ? "" : m.Product.Name).ToList()),
                        _ => View(incidents.OrderBy(m => m.Id).ToList()),
                    };
                case "DateOpened":
                    ViewData["DateOpenedSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(incidents.OrderBy(m => m.DateOpened).ToList()),
                        "desc" => View(incidents.OrderByDescending(m => m.DateOpened).ToList()),
                        _ => View(incidents.OrderBy(m => m.Id).ToList()),
                    };
                default:
                    return View(incidents.OrderBy(m => m.Id).ToList());

            }
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            ViewBag.Action = "Add";
            return View("Edit", new Incident());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var incident = Context.Incidents.Find(id);
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            ViewBag.Action = "Edit";
            return View(incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.Id == 0)
                {
                    incident.DateOpened = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
                    Context.Incidents.Add(incident);

                }
                else
                {
                    Context.Incidents.Update(incident);
                }

                Context.SaveChanges();
                return RedirectToAction("List", "Incident");

            }
            else
            {
                ViewBag.Customers = Context.Customers.ToList();
                ViewBag.Products = Context.Products.ToList();
                ViewBag.Technicians = Context.Technicians.ToList();
                ViewBag.Action = (incident.Id == 0) ? "Add" : "Edit";
                if (incident.Id == 0)
                {
                    return View("Edit", incident);
                }
                else
                {
                    return View(incident);
                }

            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = Context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("List", "Incident");
        }

        public IActionResult Update(string sortBy, string sortOrder, int Id)
        {
            if (Id == 0)
            {
                ViewBag.Technicians = Context.Technicians.ToList();
                return View((new List<Incident>(), new Technician()));
            }
            var technician = Context.Technicians.Find(Id);
            var incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == Id); //.OrderBy(m => m.Id).ToList();
            ViewBag.Technicians = Context.Technicians.ToList();

            switch (sortBy)
            {
                case "Title":
                    ViewData["TitleSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View((incidents.OrderBy(m => m.Title).ToList(), technician)),
                        "desc" => View((incidents.OrderByDescending(m => m.Title).ToList(), technician)),
                        _ => View((incidents.OrderBy(m => m.Id).ToList(), technician)),
                    };
                case "Customer":
                    ViewData["CustomerSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View((incidents.OrderBy(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(), technician)),
                        "desc" => View((incidents.OrderByDescending(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(), technician)),
                        _ => View((incidents.OrderBy(m => m.Id).ToList(), technician)),
                    };
                case "Product":
                    ViewData["ProductSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View((incidents.OrderBy(m => m.Product == null ? "" : m.Product.Name).ToList(), technician)),
                        "desc" => View((incidents.OrderByDescending(m => m.Product == null ? "" : m.Product.Name).ToList(), technician)),
                        _ => View((incidents.OrderBy(m => m.Id).ToList(), technician)),
                    };
                case "DateOpened":
                    ViewData["DateOpenedSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View((incidents.OrderBy(m => m.DateOpened).ToList(), technician)),
                        "desc" => View((incidents.OrderByDescending(m => m.DateOpened).ToList(), technician)),
                        _ => View((incidents.OrderBy(m => m.Id).ToList(), technician)),
                    };
                default:                    
                    return View((incidents.OrderBy(m => m.Id).ToList(), technician));

            }



        }

        [HttpPost]
        public IActionResult Update(List<Incident> incidents, int Id)
        {
            var technician = Context.Technicians.Find(Id);
            incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == Id).OrderBy(m => m.Id).ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            return View((incidents, technician));
        }

        [HttpGet]
        public IActionResult EditTech(int id)
        {
            var incident = Context.Incidents.Find(id);
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            ViewBag.Action = "Edit";
            return View(incident);
        }

        [HttpPost]
        public IActionResult EditTech(Incident incident, int TechId)
        {
            Console.WriteLine("EditTech Post ID: " + TechId);

            if (ModelState.IsValid)
            {
                if (incident.Id == 0)
                {
                    incident.DateOpened = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
                    Context.Incidents.Add(incident);

                }
                else
                {
                    Context.Incidents.Update(incident);
                }

                Context.SaveChanges();
                return RedirectToAction("Update", "Incident", new { sortBy = "", sortOrder = "", Id = TechId });
            }
            else
            {
                ViewBag.Customers = Context.Customers.ToList();
                ViewBag.Products = Context.Products.ToList();
                ViewBag.Technicians = Context.Technicians.ToList();
                ViewBag.Action = (incident.Id == 0) ? "Add" : "Edit";
                if (incident.Id == 0)
                {
                    return View("EditTech", incident);
                }
                else
                {
                    return View(incident);
                }

            }
        }


    }
}

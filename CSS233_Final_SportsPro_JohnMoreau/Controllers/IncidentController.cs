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



namespace CSS233_Final_SportsPro_JohnMoreau.Controllers
{
    public class IncidentController : Controller
    {
        private SportsContext Context { get; set; }
        public IncidentController(SportsContext ctx) => Context = ctx;

        [Route("Incidents")]
        public ActionResult List(string sortBy, string sortOrder)
        {
            

            var incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product);
            var incidentView = new IncidentViewModel(sortBy, sortOrder, incidents);

            return View(incidentView);
        }

        // No Longer Needed
        //[HttpGet]
        //public ActionResult Add()
        //{

        //    ViewBag.Customers = Context.Customers.ToList();
        //    ViewBag.Products = Context.Products.ToList();
        //    ViewBag.Technicians = Context.Technicians.ToList();
        //    ViewBag.Action = "Add";
        //    return View("Edit", new Incident());
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var incidentView = new IncidentViewModel
            {
                CurrentAction = id == 0 ? "Add" : "Edit",
                CurrentIncident = Context.Incidents.Find(id) ?? new Incident(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList(),
                Technicians = Context.Technicians.ToList()
            };

            return View(incidentView);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            if (ModelState.IsValid && model.CurrentIncident != null)
            {
                if (model.CurrentIncident?.Id == 0)
                {
                    // Not sure why the DateOpened is not binding properly to the incident model
                    // Solved, it was because field was disabled.
                    // incident.DateOpened = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");

                    Context.Incidents.Add(model.CurrentIncident ?? new Incident()); // Even though I check if the incident is not null, without the null coalesce here I get a warning of possible null dereference.
                    TempData["SuccessMessage"] = model.CurrentIncident?.Title + " has been added."; // Add TempData for Messages
                }
                else
                {
                    TempData["SuccessMessage"] = model.CurrentIncident?.Title + " has been updated.";
                    Context.Incidents.Update(model.CurrentIncident ?? new Incident()); 
                }

                Context.SaveChanges();
                return RedirectToAction("List", "Incident");

            }
            else
            {
                var incidentView = new IncidentViewModel
                {
                    CurrentAction = model.CurrentIncident?.Id == 0 ? "Add" : "Edit",
                    CurrentIncident = model.CurrentIncident,
                    Customers = Context.Customers.ToList(),
                    Products = Context.Products.ToList(),
                    Technicians = Context.Technicians.ToList()
                };
                return View(incidentView);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var incident = Context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Incident incident)
        {
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            TempData["SuccessMessage"] = incident.Title + " has been deleted.";
            return RedirectToAction("List", "Incident");
        }


        // TECHNICIAN UPDATES
        // This was already built by me for my midterm partially, I implimented it a slightly different way.

        // Seperate Update for Technicians, need to refactor
        [Route("Tech/Incidents")]
        public IActionResult TechIncident(string sortBy, string sortOrder, int Id)
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

        // Seperate Update for Technicians, need to refactor
        [HttpPost]
        public ActionResult Update(List<Incident> incidents, int Id)
        {
            var technician = Context.Technicians.Find(Id);
            incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == Id).OrderBy(m => m.Id).ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            return View((incidents, technician));
        }

        // Seperate Update for Technicians, need to refactor
        [HttpGet]
        public ActionResult EditTech(int id)
        {
            var incident = Context.Incidents.Find(id);
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList();
            ViewBag.Action = "Edit";
            return View(incident);
        }

        // Seperate Update for Technicians, need to refactor
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

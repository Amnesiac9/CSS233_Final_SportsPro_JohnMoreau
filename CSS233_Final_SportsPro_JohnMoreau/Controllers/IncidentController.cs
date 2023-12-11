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

        [Route("Incidents/{category?}")]
        public ActionResult List(string category, string sortBy, string sortOrder)
        {
            category ??= ""; // If null, become empty string

            List<Incident> incidents = category switch
            {
                "unassigned" => Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == 0).ToList(),
                "open" => Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.DateClosed == null).ToList(),
                "closed" => Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.DateClosed != null).ToList(),
                _ => Context.Incidents.Include(c => c.Customer).Include(c => c.Product).ToList(),
            };


            //var incidents = Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.DateClosed == null).ToList();
            var incidentView = new IncidentViewModel(category, sortBy, sortOrder, incidents);
            return View(incidentView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // New View Model
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
                    Context.Incidents.Update(model.CurrentIncident ?? new Incident());
                    TempData["SuccessMessage"] = model.CurrentIncident?.Title + " has been updated.";
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



        [Route("Tech/Incidents/{id?}")]
        public ActionResult TechIncident(IncidentSessionItems? sessionItems)
        {

            var incidentView = new IncidentViewModel
            {
                Technicians = Context.Technicians.ToList()
            };

            if (!ModelState.IsValid || sessionItems?.Id == 0)
            {
                // Our default option needs to be below -1 to allow for a range of -1 to int.maxvalue, so that we can include Unassigned in our select box. (id = -1)
                if(sessionItems?.Id == -2)
                {
                    return View(incidentView);
                }

                sessionItems = HttpContext.Session.GetObject<IncidentSessionItems>("SessionItems");

            }

            if (sessionItems == null)
            {
                return View(incidentView);
            }

            // Set session state
            HttpContext.Session.SetObject("SessionItems", sessionItems);
            incidentView.SortBy = sessionItems?.SortBy;
            incidentView.SortOrder = sessionItems?.SortOrder;
            incidentView.CurrentTechnician = Context.Technicians.Find(sessionItems?.Id);
            incidentView.Incidents = incidentView.GetSortedIncidentList(Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == sessionItems.Id).ToList());

            return View(incidentView);
        }




        [Route("Tech/Incidents/Edit/{id?}")]
        [HttpGet]
        public ActionResult TechEdit(int id)
        {

            var incident = Context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician).FirstOrDefault(i => i.Id == id);
            ViewBag.Action = "Edit";
            return View(incident);
        }

        [Route("Tech/Incidents/Edit/{id?}")]
        [HttpPost]
        public IActionResult TechEdit(Incident incident)
        {

            if (ModelState.IsValid)
            {
                if (incident.Id == 0)
                {
                    incident.DateOpened = DateTime.Parse(incident.DateOpened).ToString("MM/dd/yyyy h:mm:ss tt");
                    Context.Incidents.Add(incident);
                    TempData["SuccessMessage"] = incident.Title + " has been added.";
                }
                else
                {
                    Context.Incidents.Update(incident);
                    TempData["SuccessMessage"] = incident?.Title + " has been updated.";
                }

                Context.SaveChanges();

                // Get and set session
                var sessionItems = HttpContext.Session.GetObject<IncidentSessionItems>("SessionItems");
                if (sessionItems != null && incident?.TechnicianId != null)
                {
                    sessionItems.Id = incident.TechnicianId;
                    HttpContext.Session.SetObject("SessionItems", sessionItems);
                }

                return RedirectToAction("TechIncident", "Incident");
            }
            else
            {
                ViewBag.Action = "Edit";
                return View(incident);

            }
        }


    }
}

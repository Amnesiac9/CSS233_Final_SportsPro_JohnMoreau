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


        [Route("Tech/Incidents")]
        public IActionResult TechIncident(string sortBy, string sortOrder, int id)
        {
            // Get Tech ID from session state.


            var incidentView = new IncidentViewModel
            {
                Technicians = Context.Technicians.ToList()
            };

            if (id != 0)
            {
                incidentView.SortBy = sortBy;
                incidentView.SortOrder = sortOrder;
                incidentView.CurrentTechnician = Context.Technicians.Find(id);
                incidentView.Incidents = incidentView.GetSortedIncidentList(Context.Incidents.Include(c => c.Customer).Include(c => c.Product).Where(i => i.TechnicianId == id).ToList());
            }
            return View(incidentView);
        }





        [Route("Tech/Incidents/Edit")]
        [HttpGet]
        public ActionResult TechEdit(int id)
        {
            var incident = Context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician).FirstOrDefault(i => i.Id == id);
            ViewBag.Action = "Edit";
            return View(incident);
        }

        // Seperate Update for Technicians, need to refactor
        [Route("Tech/Incidents/Edit")]
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
                return RedirectToAction("TechIncident", "Incident", new { sortBy = "", sortOrder = "", Id = incident?.TechnicianId });
            }
            else
            {
                ViewBag.Action = "Edit";
                return View(incident.Id);

            }
        }


    }
}

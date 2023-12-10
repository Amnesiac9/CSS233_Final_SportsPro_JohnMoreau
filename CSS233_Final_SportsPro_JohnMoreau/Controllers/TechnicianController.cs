using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

/*
* John Moreau
* CSS233
* 12/9/2023
*
*
*/

namespace CSS233_Final_SportsPro_JohnMoreau.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsContext Context { get; set; }
        public TechnicianController(SportsContext ctx) => Context = ctx;

        [Route("Technicians")]
        public IActionResult List(string sortBy, string sortOrder)
        {
            var technicians = Context.Technicians;

            switch (sortBy)
            {
                case "Name":
                    ViewData["NameSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(technicians.OrderBy(m => m.Name).ToList()),
                        "desc" => View(technicians.OrderByDescending(m => m.Name).ToList()),
                        _ => View(technicians.OrderBy(m => m.Id).ToList()),
                    };
                case "Email":
                    ViewData["EmailSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(technicians.OrderBy(m => m.Email).ToList()),
                        "desc" => View(technicians.OrderByDescending(m => m.Email).ToList()),
                        _ => View(technicians.OrderBy(m => m.Id).ToList()),
                    };
                case "Phone":
                    ViewData["PhoneSortOrder"] = sortOrder;
                    return sortOrder switch
                    {
                        "asc" => View(technicians.OrderBy(m => m.Phone).ToList()),
                        "desc" => View(technicians.OrderByDescending(m => m.Phone).ToList()),
                        _ => View(technicians.OrderBy(m => m.Id).ToList()),
                    };
                default:
                    return View(technicians.OrderBy(m => m.Id).ToList());

            }
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var technician = Context.Technicians.Find(id);
            ViewBag.Action = "Edit";
            return View(technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                technician.Email = technician.Email != null ? technician.Email.ToLower() : "";
                if (technician.Id == 0)
                {
                    Context.Technicians.Add(technician);
                    TempData["SuccessMessage"] = technician.Name + " has been added.";

                }
                else
                {
                    Context.Technicians.Update(technician);
                    TempData["SuccessMessage"] = technician.Name + " has been updated.";
                }

                Context.SaveChanges();
                return RedirectToAction("List", "Technician");

            }
            else
            {
                ViewBag.Action = (technician.Id == 0) ? "Add" : "Edit";
                if (technician.Id == 0)
                {
                    return View("Edit", technician);
                }
                else
                {
                    return View(technician);
                }

            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contact = Context.Technicians.Find(id);
            return View(contact);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Technician technician)
        {
            Context.Technicians.Remove(technician);
            Context.SaveChanges();
            TempData["SuccessMessage"] = technician.Name + " has been deleted.";
            return RedirectToAction("List", "Technician");
        }
    }
}

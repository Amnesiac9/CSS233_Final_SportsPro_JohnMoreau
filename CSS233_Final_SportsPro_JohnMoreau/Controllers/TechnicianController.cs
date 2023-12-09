//using john_moreau_MidTerm.Migrations;
using john_moreau_MidTerm.Models;
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
    public class TechnicianController : Controller
    {
        private SportsContext Context { get; set; }
        public TechnicianController(SportsContext ctx) => Context = ctx;

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
                    technician.DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt");
                    Context.Technicians.Add(technician);

                }
                else
                {
                    Context.Technicians.Update(technician);
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
        public IActionResult Delete(int id)
        {
            var contact = Context.Technicians.Find(id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            Context.Technicians.Remove(technician);
            Context.SaveChanges();
            return RedirectToAction("List", "Technician");
        }
    }
}

﻿using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSS233_Final_SportsPro_JohnMoreau.Controllers
{
    public class ValidationController : Controller
    {
        private SportsContext Context { get; set; }
        public ValidationController(SportsContext ctx) => Context = ctx;
        public JsonResult CheckEmail(string email) // Not sure how to pull id of the customer as well
        {
            string msg = EmailExists(Context, email);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }

        public static string EmailExists(SportsContext ctx, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.Customers.FirstOrDefault(c => c.Email.ToLower() == email.ToLower()); // && c.Id != id // I want to add ID but not sure how to pull it from the view
                if (customer != null)
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }
    }
}

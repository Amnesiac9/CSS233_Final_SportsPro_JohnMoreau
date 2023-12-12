using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    // Wanted to add a custom validation attribute so that I could pass the email AND Id when checking if an email already exists in the database. Ran out of time to work on this.
    public class RemoteWithIdAttribute : RemoteAttribute
    {
        private int Id { get; set; }
        private string Email { get; set; }
        public RemoteWithIdAttribute(string email, int id) //: base(email)
        { // constructor
            Email = email;
            Id = id;
        }
        //protected override ValidationResult IsValid(object value, ValidationContext ctx)
        //{
        //    if (value is string)
        //    {
        //        // cast value to string
        //        string stringToCheck = (string)value;

        //    }
        //    string msg = base.ErrorMessage ??
        //    ctx.DisplayName + " must be a " + (IsPast ? "past" : "future") +
        //    " date within " + numYears + " years of now.";
        //    return new ValidationResult(msg);
        //}
    }
}

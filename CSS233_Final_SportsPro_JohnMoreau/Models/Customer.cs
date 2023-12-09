using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

/*
* John Moreau
* CSS233
* 10/29/2023
*
*
*/

namespace john_moreau_MidTerm.Models
{
    public class Customer
    {
        // EF Core will configure the database to generate this value
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an Address")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a State")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose a Country")]
        [Range(1, int.MaxValue, ErrorMessage = "Please choose a Country")]
        public int CountryId { get; set; } // Foreign key property

        public Country? Country { get; set; } // Navigation property

        public string Name => FirstName + " " + LastName;

        public string? Email { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? DateAdded { get; set; } = string.Empty;

        public string? Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-').ToLower();
    }
}

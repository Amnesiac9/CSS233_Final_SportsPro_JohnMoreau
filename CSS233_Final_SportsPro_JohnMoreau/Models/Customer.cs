using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

/*
* John Moreau
* CSS233
* 12/10/2023
*
*
*/

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Customer
    {
        // EF Core will configure the database to generate this value
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a First Name")]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter a First Name between 1-51 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Last Name")]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter a Last Name between 1-51 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an Address")]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter an Address between 1-51 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a City")]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter a City between 1-51 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a State")]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter a State between 1-51 characters")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Postal Code")]
        [StringLength(21, MinimumLength = 1, ErrorMessage = "Please enter a Postal Code between 1-21 characters")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose a Country")]
        [Range(1, int.MaxValue, ErrorMessage = "Please choose a Country")]
        public int CountryId { get; set; } // Foreign key property

        public Country? Country { get; set; } // Navigation property

        public string Name => FirstName + " " + LastName;

        [DataType(DataType.EmailAddress)]
        [StringLength(51, MinimumLength = 1, ErrorMessage = "Please enter an Email between 1-51 characters")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address")]

        public string? Email { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format (999) 999-9999")]
        public string? Phone { get; set; } = string.Empty;

        public string? DateAdded { get; set; } = string.Empty;

        public string? Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-').ToLower();

        //public List<Product>? Products { get; set; }

        public ICollection<Registration>? Registrations { get; set; }
    }
}

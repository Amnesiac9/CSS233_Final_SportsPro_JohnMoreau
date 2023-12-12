using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
* John Moreau
* CSS233
* 12/9/2023
*
*
*/

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Technician
    {
        // EF Core will configure the database to generate this value
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format (999) 999-9999")]
        public string Phone { get; set; } = string.Empty;

        public string? DateAdded { get; set; } = string.Empty;

        public string? Slug => Name?.Replace(' ', '-').ToLower();
    }
}

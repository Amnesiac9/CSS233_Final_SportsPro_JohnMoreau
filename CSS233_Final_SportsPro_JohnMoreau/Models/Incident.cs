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

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Incident
    {
        // EF Core will configure the database to generate this value
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Customer")]
        public int CustomerId { get; set; } // Foreign key property
        public Customer? Customer { get; set; } // Navigation property


        [Required(ErrorMessage = "Please select a Product")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Product")]
        public int ProductId { get; set; } // Foreign key property
        public Product? Product { get; set; } // Navigation property


        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select a Technician")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Technician")]
        public int TechnicianId { get; set; } // Foreign key property
        public Technician? Technician { get; set; } // Navigation property


        public string? DateOpened { get; set; } = string.Empty;

        public DateTime? DateClosed { get; set; }

        public string? Slug => Id.ToString();



     
    }
}

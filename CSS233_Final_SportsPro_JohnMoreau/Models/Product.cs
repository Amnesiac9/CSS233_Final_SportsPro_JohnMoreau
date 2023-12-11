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

// Date Times Formatting for FORMS: https://www.mikesdotnetting.com/article/352/working-with-dates-and-times-in-razor-pages-forms#:~:text=Alternatively%2C%20you%20can%20use%20the%20asp-format%20attribute%20on,itself%3A%20DateTime%3A%20%3Cinput%20class%3D%22form-control%22%20asp-for%3D%22DateTime%22%20asp-format%3D%22%20%7B0%3Ayyyy-MM-ddTHH%3Amm%7D%22%20%2F%3E
// I couldn't get any other format to work. I tried using only one character for month "M", and removing/adding the seconds, but that would result in a blank date.

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class Product
    {
        // EF Core will configure the database to generate this value
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Product Code")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a valid Price")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter a valid Price greater than 0.01")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; } = 0.00M;

        [Required(ErrorMessage = "Please enter a Release Date")]
        [Range(typeof(DateTime), "1/1/1900", "12/31/9999", ErrorMessage = "Date must be after 1/1/1900")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss tt}", ApplyFormatInEditMode = true)] // SOURCE: https://www.mikesdotnetting.com/article/352/working-with-dates-and-times-in-razor-pages-forms#:~:text=Alternatively%2C%20you%20can%20use%20the%20asp-format%20attribute%20on,itself%3A%20DateTime%3A%20%3Cinput%20class%3D%22form-control%22%20asp-for%3D%22DateTime%22%20asp-format%3D%22%20%7B0%3Ayyyy-MM-ddTHH%3Amm%7D%22%20%2F%3E
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public string? DateAdded { get; set; } = string.Empty;


        public string? Slug => Name?.Replace(' ', '-').ToLower();

        public ICollection<Registration>? Registrations { get; set; }
    }
}

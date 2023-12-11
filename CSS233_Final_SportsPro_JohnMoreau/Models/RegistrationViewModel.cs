using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

/*
* John Moreau
* CSS233
* 12/9/2023
*
*/

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class RegistrationViewModel
    {
        public List<Customer>? Customers { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a customer")]
        public int? CurrentCustomerId { get; set; }


        public List<Product>? Products { get; set; }

        public int? CurrentProductId { get; set; }


        public string? SortBy { get; set; }

        public string? SortOrder { get; set; }


        // If the sortBy matches our current SortBy, return the sort order, otherwise return "";
        public string GetCurrentSortOrder(string sortBy) => sortBy == SortBy ? SortOrder ?? "" : "";

        // For getting a new sort order to pass to my backend controller so that the URL query matches the actual sort order >:|
        public string GetNewSortOrder(string sortBy) => GetCurrentSortOrder(sortBy) switch
        {
            "asc" => "desc",
            "desc" => "",
            _ => "asc",
        };

        // For getting the sort by value to pass to my backend controller so that the URL query will go away when I'm not sorting by anything
        public string GetSortBy(string sortBy)
        {
            if (GetNewSortOrder(sortBy) == "") return "";
            return sortBy;
        }

        // Get Icon for sort
        public string GetSortIcon(string sortBy) => GetCurrentSortOrder(sortBy) switch
        {
            "asc" => "fa-arrow-up",
            "desc" => "fa-arrow-down",
            _ => "fa-sort",
        };

    }
}

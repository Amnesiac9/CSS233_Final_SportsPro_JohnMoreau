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

        [Required(ErrorMessage = "Please select a customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a customer")]
        public int? CurrentCustomerId { get; set; }


        public List<Product>? Products { get; set; }

        [Required(ErrorMessage = "Please select a product")]
        [Range(0, int.MaxValue, ErrorMessage = "Please select a product")]
        public int? CurrentProductId { get; set; }


        public string? SortBy { get; set; }

        public string? SortOrder { get; set; }


        public RegistrationViewModel () { }

        public RegistrationViewModel(string sortBy, string sortOrder, int? currentCustomerId, List<Product> products, Customer? customer)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
            CurrentCustomerId = currentCustomerId;
            Products = products;
            Customer = GetSortedRegistrations(customer);
        }

        public Customer GetSortedRegistrations (Customer? customer)
        {
            if (customer == null) return new Customer();

            if (customer.Registrations == null) return customer;


            customer.Registrations = SortBy switch
            {
                "Product" => SortOrder switch
                {
                    "asc" => customer.Registrations.OrderBy(m => m.Product?.Name).ToList(),
                    "desc" => customer.Registrations.OrderByDescending(m => m.Product?.Name).ToList(),
                    _ => customer.Registrations.OrderBy(m => m.ProductId).ToList(),
                },
                _ => customer.Registrations.OrderBy(m => m.ProductId).ToList(),
            };

            return customer;

        }

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

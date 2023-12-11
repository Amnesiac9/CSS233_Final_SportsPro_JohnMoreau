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
    public class IncidentViewModel
    {
        public List<Incident>? Incidents { get; set; }

        public List<Product>? Products { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Technician>? Technicians { get; set; }

        
        public Incident? CurrentIncident { get; set; }

        
        public Technician? CurrentTechnician { get; set; }

        public string? IncidentFilter { get; set; }

        public string? CurrentAction { get; set; }

        public string? SortBy { get; set; }

        public string? SortOrder { get; set; }


        public string? Slug => CurrentTechnician?.Name?.Replace(' ', '-').ToLower();

        // Default
        public IncidentViewModel() { }

        // Sort By Constructor
        public IncidentViewModel(string sortBy, string sortOrder, IIncludableQueryable<Incident, Product?> incidents)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
            Incidents = GetSortedIncidentList(incidents);
        }

        public IncidentViewModel(string sortBy, string sortOrder, Technician currentTech, List<Technician> technicians, List<Incident> incidents)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
            CurrentTechnician = currentTech;
            Technicians = technicians;
            Incidents = GetSortedIncidentList(incidents);
        }

        public static Incident NewIncident()
        {
            return new Incident();
        }

        public List<Incident> GetSortedIncidentList(List<Incident> incidents)
        {
            return SortBy switch
            {
                "Title" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Title).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Title).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "Customer" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "Product" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Product == null ? "" : m.Product.Name).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Product == null ? "" : m.Product.Name).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "DateOpened" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.DateOpened).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.DateOpened).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                _ => incidents.OrderBy(m => m.Id).ToList(),
            };
        }

        public List<Incident> GetSortedIncidentList(IIncludableQueryable<Incident, Product?> incidents)
        {
            return SortBy switch
            {
                "Title" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Title).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Title).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "Customer" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Customer == null ? "" : m.Customer.FirstName).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "Product" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.Product == null ? "" : m.Product.Name).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.Product == null ? "" : m.Product.Name).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                "DateOpened" => SortOrder switch
                {
                    "asc" => incidents.OrderBy(m => m.DateOpened).ToList(),
                    "desc" => incidents.OrderByDescending(m => m.DateOpened).ToList(),
                    _ => incidents.OrderBy(m => m.Id).ToList(),
                },
                _ => incidents.OrderBy(m => m.Id).ToList(),
            };
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

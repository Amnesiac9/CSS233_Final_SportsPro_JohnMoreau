using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public class SportsContext : DbContext
    {
        public SportsContext(DbContextOptions<SportsContext> options)
            : base(options)
        { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;

        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Incident> Incidents { get; set; } = null!;

        public DbSet<Registration> Registrations { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Registration>()
            .HasKey(r => new { r.CustomerId, r.ProductId });

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ProductId);


            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Code = "TRNY",
                Name = "Tournament Master 1.0",
                Price = 4.99M,
                ReleaseDate = new DateTime(2018, 12, 1),
                DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt")
            },
            new Product
            {
                Id = 2,
                Code = "LEAG10",
                Name = "League Scheduler 1.0",
                Price = 4.99M,
                ReleaseDate = new DateTime(2019, 05, 1),
                DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt")
            },
            new Product
            {
                Id = 3,
                Code = "LEAGD10",
                Name = "League Scheduler Deluxe 1.0",
                Price = 7.99M,
                ReleaseDate = new DateTime(2019, 08, 1),
                DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt")
            },
            new Product
            {
                Id = 4,
                Code = "PS5",
                Name = "Play Station 5",
                Price = 699.99M,
                ReleaseDate = new DateTime(2020, 11, 12),
                DateAdded = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt")
            });

            modelBuilder.Entity<Technician>().HasData(
                new Technician 
                {
                Id = -1,
                    Name = "Not Assigned",
                    Email = "",
                    Phone = ""
                },
                new Technician 
                {
                    Id = 1,
                    Name = "John Moreau",
                    Email = "email@email.com",
                    Phone = "509-559-5959"
                },
                new Technician
                {
                    Id = 2,
                    Name = "Robin Greene",
                    Email = "robin@wwcc.com",
                    Phone = "509-955-9595"
                },
                new Technician
                {
                    Id = 3,
                    Name = "Alison Diaz",
                    Email = "alison@email.com",
                    Phone = "509-555-0443"
                },
                new Technician
                {
                    Id = 4,
                    Name = "Andrew Wilson",
                    Email = "andrew@email.com",
                    Phone = "509-555-0449"
                },
                new Technician
                {
                    Id = 5,
                    Name = "Gina Fiori",
                    Email = "gina@email.com",
                    Phone = "509-555-0459"
                }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "United States",
                    Code = "US"
                },
                new Country
                {
                    Id = 2,
                    Name = "Canada",
                    Code = "CA"
                }, 
                new Country
                {
                    Id = 3,
                    Name = "Mexico",
                    Code = "MX"
                },
                new Country
                {
                    Id = 4,
                    Name = "Australia",
                    Code = "AU"
                },
                new Country
                {
                    Id = 5,
                    Name = "France",
                    Code = "FR"
                },
                new Country
                {
                    Id = 6,
                    Name = "Finland",
                    Code = "FI"
                },
                new Country
                {
                    Id = 7,
                    Name = "Germany",
                    Code = "DE"
                },
                new Country
                {
                    Id = 8,
                    Name = "Denmark",
                    Code = "DK"
                },
                new Country
                {
                    Id = 9,
                    Name = "Japan",
                    Code = "JP"
                },
                new Country
                {
                    Id = 10,
                    Name = "India",
                    Code = "IN"
                },
                new Country
                {
                    Id = 11,
                    Name = "Italy",
                    Code = "IT"
                },
                new Country
                {
                    Id = 12,
                    Name = "New Zealand",
                    Code = "NZ"
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Kaitlyn",
                    LastName = "Anthoni",
                    Address = "123 Street",
                    City = "San Francisco",
                    State = "California",
                    PostalCode = "94102",
                    CountryId = 1, 
                    Email = "kanthoni@pge.com",
                    Phone = "555-555-5555"
                }, 
                new Customer
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Moreau",
                    Address = "123 Street",
                    City = "Walla Walla",
                    State = "Washington",
                    PostalCode = "99362",
                    CountryId = 1,
                    Email = "john@email.com",
                    Phone = "555-555-5554"
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Robin",
                    LastName = "Greene",
                    Address = "124 Street",
                    City = "Walla Walla",
                    State = "Washington",
                    PostalCode = "99362",
                    CountryId = 1,
                    Email = "robin@wwcc.com",
                    Phone = "555-555-5553"
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "Jackson",
                    LastName = "Greg",
                    Address = "20 Vista Street",
                    City = "Queenstown",
                    State = "Otago",
                    PostalCode = "21658",
                    CountryId = 12,
                    Email = "jacksong@newzealand.com",
                    Phone = "64 222-222-2222"
                }
                );

            modelBuilder.Entity<Incident>().HasData(
                new Incident
                {
                    Id = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    Title = "Error launching program",
                    Description = "Program fails with error code 510, unable to open database.",
                    TechnicianId = 1,
                    DateOpened = new DateTime(2022, 10, 9).ToString("MM/dd/yyyy h:mm:ss tt")
                }, 
                new Incident
                {
                    Id = 2,
                    CustomerId = 2,
                    ProductId = 1,
                    Title = "Error importing data",
                    Description = "Program fails with error code 510, unable to open database.",
                    TechnicianId = 3,
                    DateOpened = new DateTime(2022, 9, 10).ToString("MM/dd/yyyy h:mm:ss tt")
                },
                new Incident
                {
                    Id = 3,
                    CustomerId = 3,
                    ProductId = 2,
                    Title = "Could not install",
                    Description = "Program fails with error code 322, unable to access device.",
                    TechnicianId = 4,
                    DateOpened = new DateTime(2022, 8, 3).ToString("MM/dd/yyyy h:mm:ss tt")
                },
                new Incident
                {
                    Id = 4,
                    CustomerId = 4,
                    ProductId = 4,
                    Title = "Could not install",
                    Description = "Program fails with error 304, unable to access requested files.",
                    TechnicianId = 2,
                    DateOpened = new DateTime(2022, 1, 8).ToString("MM/dd/yyyy h:mm:ss tt")
                }
                ) ;
        }

    }
}

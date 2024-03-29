﻿// <auto-generated />
using System;
using CSS233_Final_SportsPro_JohnMoreau.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSS233_Final_SportsPro_JohnMoreau.Migrations
{
    [DbContext(typeof(SportsContext))]
    partial class SportsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "US",
                            Name = "United States"
                        },
                        new
                        {
                            Id = 2,
                            Code = "CA",
                            Name = "Canada"
                        },
                        new
                        {
                            Id = 3,
                            Code = "MX",
                            Name = "Mexico"
                        },
                        new
                        {
                            Id = 4,
                            Code = "AU",
                            Name = "Australia"
                        },
                        new
                        {
                            Id = 5,
                            Code = "FR",
                            Name = "France"
                        },
                        new
                        {
                            Id = 6,
                            Code = "FI",
                            Name = "Finland"
                        },
                        new
                        {
                            Id = 7,
                            Code = "DE",
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 8,
                            Code = "DK",
                            Name = "Denmark"
                        },
                        new
                        {
                            Id = 9,
                            Code = "JP",
                            Name = "Japan"
                        },
                        new
                        {
                            Id = 10,
                            Code = "IN",
                            Name = "India"
                        },
                        new
                        {
                            Id = 11,
                            Code = "IT",
                            Name = "Italy"
                        },
                        new
                        {
                            Id = 12,
                            Code = "NZ",
                            Name = "New Zealand"
                        });
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("DateAdded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(51)
                        .HasColumnType("nvarchar(51)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Street",
                            City = "San Francisco",
                            CountryId = 1,
                            DateAdded = "",
                            Email = "kanthoni@pge.com",
                            FirstName = "Kaitlyn",
                            LastName = "Anthoni",
                            Phone = "555-555-5555",
                            PostalCode = "94102",
                            State = "California"
                        },
                        new
                        {
                            Id = 2,
                            Address = "123 Street",
                            City = "Walla Walla",
                            CountryId = 1,
                            DateAdded = "",
                            Email = "john@email.com",
                            FirstName = "John",
                            LastName = "Moreau",
                            Phone = "555-555-5554",
                            PostalCode = "99362",
                            State = "Washington"
                        },
                        new
                        {
                            Id = 3,
                            Address = "124 Street",
                            City = "Walla Walla",
                            CountryId = 1,
                            DateAdded = "",
                            Email = "robin@wwcc.com",
                            FirstName = "Robin",
                            LastName = "Greene",
                            Phone = "555-555-5553",
                            PostalCode = "99362",
                            State = "Washington"
                        },
                        new
                        {
                            Id = 4,
                            Address = "20 Vista Street",
                            City = "Queenstown",
                            CountryId = 12,
                            DateAdded = "",
                            Email = "jacksong@newzealand.com",
                            FirstName = "Jackson",
                            LastName = "Greg",
                            Phone = "64 222-222-2222",
                            PostalCode = "21658",
                            State = "Otago"
                        });
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOpened")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TechnicianId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Incidents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            DateOpened = "10/09/2022 12:00:00 AM",
                            Description = "Program fails with error code 510, unable to open database.",
                            ProductId = 1,
                            TechnicianId = 1,
                            Title = "Error launching program"
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            DateOpened = "09/10/2022 12:00:00 AM",
                            Description = "Program fails with error code 510, unable to open database.",
                            ProductId = 1,
                            TechnicianId = 3,
                            Title = "Error importing data"
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 3,
                            DateOpened = "08/03/2022 12:00:00 AM",
                            Description = "Program fails with error code 322, unable to access device.",
                            ProductId = 2,
                            TechnicianId = 4,
                            Title = "Could not install"
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 4,
                            DateOpened = "01/08/2022 12:00:00 AM",
                            Description = "Program fails with error 304, unable to access requested files.",
                            ProductId = 4,
                            TechnicianId = 2,
                            Title = "Could not install"
                        });
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateAdded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "TRNY",
                            DateAdded = "12/11/2023 at 12:26 AM",
                            Name = "Tournament Master 1.0",
                            Price = 4.99m,
                            ReleaseDate = new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Code = "LEAG10",
                            DateAdded = "12/11/2023 at 12:26 AM",
                            Name = "League Scheduler 1.0",
                            Price = 4.99m,
                            ReleaseDate = new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Code = "LEAGD10",
                            DateAdded = "12/11/2023 at 12:26 AM",
                            Name = "League Scheduler Deluxe 1.0",
                            Price = 7.99m,
                            ReleaseDate = new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Code = "PS5",
                            DateAdded = "12/11/2023 at 12:26 AM",
                            Name = "Play Station 5",
                            Price = 699.99m,
                            ReleaseDate = new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Registration", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Technician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DateAdded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Technicians");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            DateAdded = "",
                            Email = "",
                            Name = "Not Assigned",
                            Phone = ""
                        },
                        new
                        {
                            Id = 1,
                            DateAdded = "",
                            Email = "email@email.com",
                            Name = "John Moreau",
                            Phone = "509-559-5959"
                        },
                        new
                        {
                            Id = 2,
                            DateAdded = "",
                            Email = "robin@wwcc.com",
                            Name = "Robin Greene",
                            Phone = "509-955-9595"
                        },
                        new
                        {
                            Id = 3,
                            DateAdded = "",
                            Email = "alison@email.com",
                            Name = "Alison Diaz",
                            Phone = "509-555-0443"
                        },
                        new
                        {
                            Id = 4,
                            DateAdded = "",
                            Email = "andrew@email.com",
                            Name = "Andrew Wilson",
                            Phone = "509-555-0449"
                        },
                        new
                        {
                            Id = 5,
                            DateAdded = "",
                            Email = "gina@email.com",
                            Name = "Gina Fiori",
                            Phone = "509-555-0459"
                        });
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Customer", b =>
                {
                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Incident", b =>
                {
                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Technician", "Technician")
                        .WithMany()
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Registration", b =>
                {
                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Customer", "Customer")
                        .WithMany("Registrations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSS233_Final_SportsPro_JohnMoreau.Models.Product", "Product")
                        .WithMany("Registrations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Customer", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("CSS233_Final_SportsPro_JohnMoreau.Models.Product", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}

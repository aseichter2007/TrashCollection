using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashCollection.Models;

namespace TrashCollection.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {           
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee>  Employees { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<Weekday> Weekdays { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Name = "Customer", NormalizedName = "CUSTOMER"
                }
               
                ) ;
            builder.Entity<Weekday>().HasData(
                new Weekday { Id = -1, day = "Monday" },
                new Weekday { Id = -2, day = "Tuesday" },
                new Weekday { Id = -3, day = "Wednesday" },
                new Weekday { Id = -4, day = "Thursday" },
                new Weekday { Id = -5, day = "Friday" },
                new Weekday { Id = -6, day = "Saturday" },
                new Weekday { Id = -7, day = "Sunday" }); 
        }
    }
}

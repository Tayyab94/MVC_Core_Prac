using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models.ModelExtensions;

namespace VenketCorePracticeCore.Models.DataContext
{
    public class DemoContext : IdentityDbContext<ApplicationUser>
    {
        //[StringLength(100)]
        //public string FirstNameUSer { get; set; }

        public DemoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees
        {
            get; set;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Employee>().HasData(
            //    new Employee
            //    {
            //        ID = 1,
            //        Name = "Tayyab",
            //        Emial = "tayyab@gmail.com",
            //        Departments = Department.HR
            //    }

            //    );
            base.OnModelCreating(builder);
            builder.Seed();
        }
    }
}

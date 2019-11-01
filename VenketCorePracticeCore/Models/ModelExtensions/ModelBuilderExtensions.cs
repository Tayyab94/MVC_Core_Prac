using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models.ModelExtensions
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Employee>().HasData(
               new Employee
               {
                   ID = 1,
                   Name = "Tayyab",
                   Emial = "tayyab@gmail.com",
                   Departments = Department.HR
               }

               );
        }
    }
}

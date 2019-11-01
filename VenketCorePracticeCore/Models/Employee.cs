using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models
{
    public class Employee
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public string Emial { get; set; }

        public Department Departments { get; set; }



        public string imagePath { get; set; }
    }
}

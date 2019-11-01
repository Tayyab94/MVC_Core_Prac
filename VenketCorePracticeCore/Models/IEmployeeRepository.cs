using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models
{
   public interface IEmployeeRepository
    {

        Employee GetEmployeeById(int id);

        IEnumerable<Employee> GetAllEmployeesList();

        Employee AddNewEmployee(Employee obj);

        Employee DeleteEmployee(int id);

        Employee UpdateEmployee(Employee newEmployee);
    }
}

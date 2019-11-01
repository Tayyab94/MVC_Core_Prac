using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Models
{
    public class EmployeeMockRepository : IEmployeeRepository
    {

        public List<Employee> _employeesList;
        public EmployeeMockRepository()
        {
            _employeesList = new List<Employee>()
            {
                new Employee{ ID=1, Name="Tayyab",Emial="tayyab@gmail.com", Departments=Department.HR},    new Employee{ ID=2, Name="Tayyaba",Emial="tayyaba@gmail.com",  Departments=Department.None},
                    new Employee{ ID=3, Name="Aldi",Emial="alidb@gmail.com", Departments=Department.Web},
                        new Employee{ ID=4, Name="ali",Emial="ali@gmail.com", Departments=Department.CEO}
            };
        }

        public Employee AddNewEmployee(Employee obj)
        {
            obj.ID = _employeesList.Max(x => x.ID) + 1;
          _employeesList.Add(obj);
            return obj;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee obj = _employeesList.SingleOrDefault(s => s.ID==id);

            if (obj != null)
                _employeesList.Remove(obj);

            return obj;
        }

        public IEnumerable<Employee> GetAllEmployeesList()
        {
            return _employeesList;
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeesList.FirstOrDefault(s => s.ID == id);
        }

        public Employee UpdateEmployee(Employee newEmployee)
        {
            Employee obj = _employeesList.SingleOrDefault(s => s.ID == newEmployee.ID);

            if (obj != null)
            {
                obj.Name = newEmployee.Name;
                obj.Emial = newEmployee.Emial;
                obj.Departments = newEmployee.Departments;
            }

            return obj;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models.DataContext;

namespace VenketCorePracticeCore.Models
{
    public class SqlEmployeeRepositories : IEmployeeRepository
    {
        private readonly DemoContext _context;

        public SqlEmployeeRepositories(DemoContext context)
        {
            this._context = context;
        }

        public Employee AddNewEmployee(Employee obj)
        {
            _context.Employees.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee obj = _context.Employees.Find(id);

            if(obj!=null)
            {
                _context.Employees.Remove(obj);
                _context.SaveChanges();
            }
            return obj; 
        }

        public IEnumerable<Employee> GetAllEmployeesList()
        {
            return _context.Employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee UpdateEmployee(Employee newEmployee)
        {
            var emp = _context.Employees.Attach(newEmployee);

            emp.State = EntityState.Modified;

            _context.SaveChanges();

            return newEmployee;
        }
    }
}

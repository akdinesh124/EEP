using EEP.Data;
using EEP.Models;
using Microsoft.EntityFrameworkCore;

namespace EEP.Services
{
    public class EmployeeService : IEmployeeService

    {
        private readonly EEPContext _context;
        public EmployeeService(EEPContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return employee;
        }
        public Employee DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return null;
            }
            var employee = _context.Employee.Find(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            _context.SaveChangesAsync();
            return employee;
        }
        public List<Employee> GetAllEmployees()
        {
            return _context.Employee.ToList();
        }
        public List<Employee> GetAllActiveEmployees()
        {
            return _context.Employee.Where(m=>m.EmployeeStatus==true).ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            

            var employee = _context.Employee
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }
    }
}

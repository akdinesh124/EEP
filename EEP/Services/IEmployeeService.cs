using EEP.Models;

namespace EEP.Services
{
    public interface IEmployeeService
    {
        Employee CreateEmployee(Employee employee);
        Employee DeleteEmployee(int id);
        Employee GetEmployeeById(int id);
        List<Employee> GetAllEmployees();
        List<Employee> GetAllActiveEmployees();
    }
}
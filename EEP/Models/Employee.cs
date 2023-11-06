using System.ComponentModel.DataAnnotations;

namespace EEP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Email { get; set; }

        public bool EmployeeStatus { get; set; }
    }
}

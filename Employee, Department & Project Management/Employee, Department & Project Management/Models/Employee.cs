// Models/Employee.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public decimal Salary { get; set; }

        // Foreign Key - links to Department
        [Required]
        public int DepartmentId { get; set; }

        // Navigation property - the related department object
        public Department Department { get; set; }

        // Navigation property - many-to-many through EmployeeProject
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
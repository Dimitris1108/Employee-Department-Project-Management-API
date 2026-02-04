// Models/Department.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string OfficeLocation { get; set; }

        // Navigation property - a department has many employees
        public ICollection<Employee> Employees { get; set; }
    }
}
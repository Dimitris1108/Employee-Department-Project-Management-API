using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Department
    { 
        /// <summary>
        /// Gets or Sets department ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets department name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets department office location.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Gets or Sets department employee.
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}
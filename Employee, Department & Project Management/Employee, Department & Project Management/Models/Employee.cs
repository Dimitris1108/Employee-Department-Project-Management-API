using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Employee
    {
        /// <summary>
        /// Gets or Sets employee IDs. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets employee first name.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets employee last name. 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets employee adress. 
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets employee email. 
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or Sets employee department id.
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or Sets employee department. 
        /// </summary>
        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
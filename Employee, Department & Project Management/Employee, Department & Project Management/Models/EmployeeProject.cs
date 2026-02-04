using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class EmployeeProject
    {
        /// <summary>
        /// Gets or Sets employeeID.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or Sets projectID.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or Sets role.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        /// <summary>
        /// Gets or Sets employee.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or Sets project.
        /// </summary>
        public Project Project { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Project
    {
        /// <summary>
        /// Gets or Sets project ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets project name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets project budget.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        /// <summary>
        /// Gets or Sets project code.
        /// </summary>
        [StringLength(50)]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or Sets employee project.
        /// </summary>
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
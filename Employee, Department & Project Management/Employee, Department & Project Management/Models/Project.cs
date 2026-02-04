// Models/Project.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }

        // Unique project code - generated from external API
        [StringLength(50)]
        public string ProjectCode { get; set; }

        // Navigation property - many-to-many through EmployeeProject
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
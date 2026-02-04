using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.Models
{
    public class EmployeeProject
    {
        // Composite Primary Key (configured in DbContext)
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        // The role this employee has in this project
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}

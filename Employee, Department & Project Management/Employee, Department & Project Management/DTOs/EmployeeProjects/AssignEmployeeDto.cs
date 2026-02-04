using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects
{
    public class AssignEmployeeDto
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; }
    }
}

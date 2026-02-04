using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects
{
    public class UpdateAssignmentDto
    {
        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; }
    }
}

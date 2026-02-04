using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.DTOs.Project
{
    public class UpdateProjectDto
    {
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Budget is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
        public decimal Budget { get; set; }
    }
}

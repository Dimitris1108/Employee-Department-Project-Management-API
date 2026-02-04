using System.ComponentModel.DataAnnotations;

namespace EmployeeDepartmentAndProjectManagement.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Office location is required")]
        [StringLength(200, ErrorMessage = "Office location cannot exceed 200 characters")]
        public string OfficeLocation { get; set; }
    }
}

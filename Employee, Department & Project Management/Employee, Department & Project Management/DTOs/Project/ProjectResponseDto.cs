namespace EmployeeDepartmentAndProjectManagement.DTOs.Project
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public string ProjectCode { get; set; }

        // Count of employees assigned to this project
        public int EmployeeCount { get; set; }
    }
}

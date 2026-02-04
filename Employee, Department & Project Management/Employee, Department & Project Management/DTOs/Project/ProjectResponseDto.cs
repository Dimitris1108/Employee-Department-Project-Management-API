namespace EmployeeDepartmentAndProjectManagement.DTOs.Project
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public string ProjectCode { get; set; }
        public int EmployeeCount { get; set; }
    }
}

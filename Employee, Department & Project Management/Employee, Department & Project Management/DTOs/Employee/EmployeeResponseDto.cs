namespace EmployeeDepartmentAndProjectManagement.DTOs.Employee
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }

        // Department info (instead of just DepartmentId)
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        // Count of projects this employee is assigned to
        public int ProjectCount { get; set; }
    }
}

namespace EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects
{
    public class EmployeeProjectResponseDto
    {
        // Employee info
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEmail { get; set; }

        // Project info
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        // Assignment info
        public string Role { get; set; }
    }
}

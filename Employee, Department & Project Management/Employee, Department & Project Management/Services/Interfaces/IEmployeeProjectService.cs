using EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IEmployeeProjectService
    {
        /// <summary>
        /// Get all employee projects entries.
        /// </summary>
        /// <returns>Employee projects.</returns>
        Task<IEnumerable<EmployeeProjectResponseDto>> GetAllAsync();
        Task<EmployeeProjectResponseDto> GetByIdAsync(int employeeId, int projectId);
        Task<(EmployeeProjectResponseDto Assignment, string ErrorMessage)> AssignAsync(AssignEmployeeDto dto);
        Task<(EmployeeProjectResponseDto Assignment, string ErrorMessage)> UpdateRoleAsync(int employeeId, int projectId, UpdateAssignmentDto dto);
        Task<(bool Success, string ErrorMessage)> RemoveAsync(int employeeId, int projectId);
    }
}

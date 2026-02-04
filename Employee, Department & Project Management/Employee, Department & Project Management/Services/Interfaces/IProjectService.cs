using EmployeeDepartmentAndProjectManagement.DTOs.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
        Task<ProjectResponseDto> GetByIdAsync(int id);
        Task<(ProjectResponseDto Project, string ErrorMessage)> CreateAsync(CreateProjectDto dto);  // CHANGED
        Task<(ProjectResponseDto Project, string ErrorMessage)> UpdateAsync(int id, UpdateProjectDto dto);  // CHANGED
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
    }
}
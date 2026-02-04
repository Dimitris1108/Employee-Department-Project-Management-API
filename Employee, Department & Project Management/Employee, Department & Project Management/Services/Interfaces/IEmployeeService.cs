using EmployeeDepartmentAndProjectManagement.DTOs.Employee;
using EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponseDto>> GetAllAsync();
        Task<EmployeeResponseDto> GetByIdAsync(int id);
        Task<(EmployeeResponseDto Employee, string ErrorMessage)> CreateAsync(CreateEmployeeDto dto);
        Task<(EmployeeResponseDto Employee, string ErrorMessage)> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
        Task<(IEnumerable<EmployeeProjectResponseDto> Projects, string ErrorMessage)> GetEmployeeProjectsAsync(int employeeId);
    }
}

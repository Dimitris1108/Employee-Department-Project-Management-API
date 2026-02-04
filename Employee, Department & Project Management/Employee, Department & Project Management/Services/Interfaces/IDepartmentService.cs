using EmployeeDepartmentAndProjectManagement.DTOs.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponseDto>> GetAllAsync();
        Task<DepartmentResponseDto> GetByIdAsync(int id);
        Task<DepartmentResponseDto> CreateAsync(CreateDepartmentDto dto);
        Task<DepartmentResponseDto> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
    }
}

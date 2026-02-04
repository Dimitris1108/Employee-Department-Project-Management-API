using EmployeeDepartmentAndProjectManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Repositories.Interfaces
{
    public interface IEmployeeProjectRepository
    {
        /// <summary>
        /// Get all employee project entries.
        /// </summary>
        /// <returns>Employee Projects.</returns>
        Task<IEnumerable<EmployeeProject>> GetAllAsync();
        Task<EmployeeProject> GetByIdAsync(int employeeId, int projectId);
        Task<IEnumerable<EmployeeProject>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<EmployeeProject>> GetByProjectIdAsync(int projectId);
        Task<EmployeeProject> CreateAsync(EmployeeProject employeeProject);
        Task<EmployeeProject> UpdateAsync(EmployeeProject employeeProject);
        Task DeleteAsync(EmployeeProject employeeProject);
        Task<bool> ExistsAsync(int employeeId, int projectId);
    }
}

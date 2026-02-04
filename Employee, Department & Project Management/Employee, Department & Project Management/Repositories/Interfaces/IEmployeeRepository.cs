using EmployeeDepartmentAndProjectManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Get all employee entries.
        /// </summary>
        /// <returns>Employees.</returns>
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}

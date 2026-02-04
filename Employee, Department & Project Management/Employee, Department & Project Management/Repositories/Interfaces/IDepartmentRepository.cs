using EmployeeDepartmentAndProjectManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Gets all department entries.
        /// </summary>
        /// <returns>Departments.</returns>
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department department);
        Task<Department> UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<bool> ExistsAsync(int id);
    }
}

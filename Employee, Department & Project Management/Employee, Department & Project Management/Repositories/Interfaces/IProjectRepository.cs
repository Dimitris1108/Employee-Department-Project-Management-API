using EmployeeDepartmentAndProjectManagement.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all project entries.
        /// </summary>
        /// <returns>Projects.</returns>
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        Task<bool> ExistsAsync(int id);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
using EmployeeDepartmentAndProjectManagement.Data;
using EmployeeDepartmentAndProjectManagement.Models;
using EmployeeDepartmentAndProjectManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Repositories
{
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAsync()
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .ToListAsync();
        }

        public async Task<EmployeeProject> GetByIdAsync(int employeeId, int projectId)
        {
            return await _context.EmployeeProjects
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        }

        public async Task<IEnumerable<EmployeeProject>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeProjects
                .Where(ep => ep.EmployeeId == employeeId)
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProject>> GetByProjectIdAsync(int projectId)
        {
            return await _context.EmployeeProjects
                .Where(ep => ep.ProjectId == projectId)
                .Include(ep => ep.Employee)
                .Include(ep => ep.Project)
                .ToListAsync();
        }

        public async Task<EmployeeProject> CreateAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Add(employeeProject);
            await _context.SaveChangesAsync();
            return employeeProject;
        }

        public async Task<EmployeeProject> UpdateAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Update(employeeProject);
            await _context.SaveChangesAsync();
            return employeeProject;
        }

        public async Task DeleteAsync(EmployeeProject employeeProject)
        {
            _context.EmployeeProjects.Remove(employeeProject);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int employeeId, int projectId)
        {
            return await _context.EmployeeProjects
                .AnyAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);
        }
    }
}
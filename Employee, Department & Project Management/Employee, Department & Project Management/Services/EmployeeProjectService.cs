using EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects;
using EmployeeDepartmentAndProjectManagement.Models;
using EmployeeDepartmentAndProjectManagement.Repositories.Interfaces;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services
{
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly IEmployeeProjectRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;

        public EmployeeProjectService(
            IEmployeeProjectRepository repository,
            IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<EmployeeProjectResponseDto>> GetAllAsync()
        {
            var assignments = await _repository.GetAllAsync();
            return assignments.Select(ep => MapToResponseDto(ep));
        }

        public async Task<EmployeeProjectResponseDto> GetByIdAsync(int employeeId, int projectId)
        {
            var assignment = await _repository.GetByIdAsync(employeeId, projectId);

            if (assignment == null)
                return null;

            return MapToResponseDto(assignment);
        }

        public async Task<(EmployeeProjectResponseDto Assignment, string ErrorMessage)> AssignAsync(AssignEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                return (null, $"Employee with ID {dto.EmployeeId} not found");

            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project == null)
                return (null, $"Project with ID {dto.ProjectId} not found");

            if (await _repository.ExistsAsync(dto.EmployeeId, dto.ProjectId))
                return (null, "Employee is already assigned to this project");

            var assignment = new EmployeeProject
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                Role = dto.Role
            };

            await _repository.CreateAsync(assignment);

            assignment = await _repository.GetByIdAsync(dto.EmployeeId, dto.ProjectId);

            return (MapToResponseDto(assignment), null);
        }

        public async Task<(EmployeeProjectResponseDto Assignment, string ErrorMessage)> UpdateRoleAsync(
            int employeeId,
            int projectId,
            UpdateAssignmentDto dto)
        {
            var assignment = await _repository.GetByIdAsync(employeeId, projectId);

            if (assignment == null)
                return (null, "Assignment not found");

            assignment.Role = dto.Role;

            await _repository.UpdateAsync(assignment);

            return (MapToResponseDto(assignment), null);
        }

        public async Task<(bool Success, string ErrorMessage)> RemoveAsync(int employeeId, int projectId)
        {
            var assignment = await _repository.GetByIdAsync(employeeId, projectId);

            if (assignment == null)
                return (false, "Assignment not found");

            await _repository.DeleteAsync(assignment);
            return (true, null);
        }

        private EmployeeProjectResponseDto MapToResponseDto(EmployeeProject ep)
        {
            return new EmployeeProjectResponseDto
            {
                EmployeeId = ep.EmployeeId,
                EmployeeFirstName = ep.Employee.FirstName,
                EmployeeLastName = ep.Employee.LastName,
                EmployeeEmail = ep.Employee.Email,
                ProjectId = ep.ProjectId,
                ProjectName = ep.Project.Name,
                ProjectCode = ep.Project.ProjectCode,
                Role = ep.Role
            };
        }
    }
}
    


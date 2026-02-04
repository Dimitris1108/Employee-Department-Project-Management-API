using EmployeeDepartmentAndProjectManagement.DTOs.Project;
using EmployeeDepartmentAndProjectManagement.Models;
using EmployeeDepartmentAndProjectManagement.Repositories.Interfaces;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IRandomStringGenerator _randomStringGenerator;

        public ProjectService(
            IProjectRepository repository,
            IRandomStringGenerator randomStringGenerator)
        {
            _repository = repository;
            _randomStringGenerator = randomStringGenerator;
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return projects.Select(p => MapToResponseDto(p));
        }

        public async Task<ProjectResponseDto> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return null;

            return MapToResponseDto(project);
        }

        public async Task<(ProjectResponseDto Project, string ErrorMessage)> CreateAsync(CreateProjectDto dto)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var project = new Project
                {
                    Name = dto.Name,
                    Budget = dto.Budget,
                    ProjectCode = null
                };

                await _repository.CreateAsync(project);

                string randomCode = await _randomStringGenerator.GenerateAsync();

                project.ProjectCode = $"{randomCode}{project.Id}";

                await _repository.UpdateAsync(project);

                await transaction.CommitAsync();

                return (MapToResponseDto(project), null);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return (null, $"Failed to create project: {ex.Message}");
            }
        }

        public async Task<(ProjectResponseDto Project, string ErrorMessage)> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return (null, $"Project with ID {id} not found");

            project.Name = dto.Name;
            project.Budget = dto.Budget;

            await _repository.UpdateAsync(project);

            return (MapToResponseDto(project), null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);

            if (project == null)
                return (false, "Project not found");

            await _repository.DeleteAsync(project);
            return (true, null);
        }

        private ProjectResponseDto MapToResponseDto(Project project)
        {
            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Budget = project.Budget,
                ProjectCode = project.ProjectCode,
                EmployeeCount = project.EmployeeProjects?.Count ?? 0
            };
        }
    }
}
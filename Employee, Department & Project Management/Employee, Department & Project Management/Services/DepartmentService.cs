using EmployeeDepartmentAndProjectManagement.DTOs.Department;
using EmployeeDepartmentAndProjectManagement.Models;
using EmployeeDepartmentAndProjectManagement.Repositories.Interfaces;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DepartmentResponseDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();
            return departments.Select(d => MapToResponseDto(d));
        }

        public async Task<DepartmentResponseDto> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return null;

            return MapToResponseDto(department);
        }

        public async Task<DepartmentResponseDto> CreateAsync(CreateDepartmentDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                OfficeLocation = dto.OfficeLocation
            };

            var created = await _repository.CreateAsync(department);
            return MapToResponseDto(created);
        }

        public async Task<DepartmentResponseDto> UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return null;

            department.Name = dto.Name;
            department.OfficeLocation = dto.OfficeLocation;

            var updated = await _repository.UpdateAsync(department);
            return MapToResponseDto(updated);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return (false, "Department not found");

            if (department.Employees != null && department.Employees.Any())
                return (false, "Cannot delete department with employees. Reassign or remove employees first.");

            await _repository.DeleteAsync(department);
            return (true, null);
        }

        private DepartmentResponseDto MapToResponseDto(Department department)
        {
            return new DepartmentResponseDto
            {
                Id = department.Id,
                Name = department.Name,
                OfficeLocation = department.OfficeLocation,
                EmployeeCount = department.Employees?.Count ?? 0
            };
        }
    }
}
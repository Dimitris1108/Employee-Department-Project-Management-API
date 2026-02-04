// Services/EmployeeService.cs

using EmployeeDepartmentAndProjectManagement.DTOs.Employee;
using EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects;
using EmployeeDepartmentAndProjectManagement.Models;
using EmployeeDepartmentAndProjectManagement.Repositories.Interfaces;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => MapToResponseDto(e));
        }

        public async Task<EmployeeResponseDto> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return MapToResponseDto(employee);
        }

        public async Task<(EmployeeResponseDto Employee, string ErrorMessage)> CreateAsync(CreateEmployeeDto dto)
        {
            if (!await _departmentRepository.ExistsAsync(dto.DepartmentId))
                return (null, $"Department with ID {dto.DepartmentId} not found");

            if (await _employeeRepository.EmailExistsAsync(dto.Email))
                return (null, "An employee with this email already exists");

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId
            };

            var created = await _employeeRepository.CreateAsync(employee);

            created = await _employeeRepository.GetByIdAsync(created.Id);

            return (MapToResponseDto(created), null);
        }

        public async Task<(EmployeeResponseDto Employee, string ErrorMessage)> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return (null, $"Employee with ID {id} not found");

            if (!await _departmentRepository.ExistsAsync(dto.DepartmentId))
                return (null, $"Department with ID {dto.DepartmentId} not found");

            if (await _employeeRepository.EmailExistsAsync(dto.Email, id))
                return (null, "An employee with this email already exists");

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;

            await _employeeRepository.UpdateAsync(employee);

            var updated = await _employeeRepository.GetByIdAsync(id);

            return (MapToResponseDto(updated), null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return (false, $"Employee with ID {id} not found");

            await _employeeRepository.DeleteAsync(employee);

            return (true, null);
        }

        public async Task<(IEnumerable<EmployeeProjectResponseDto> Projects, string ErrorMessage)> GetEmployeeProjectsAsync(int employeeId)
        {
            if (!await _employeeRepository.ExistsAsync(employeeId))
                return (null, $"Employee with ID {employeeId} not found");

            var employeeProjects = await _employeeProjectRepository.GetByEmployeeIdAsync(employeeId);

            var result = employeeProjects.Select(ep => new EmployeeProjectResponseDto
            {
                EmployeeId = ep.EmployeeId,
                EmployeeFirstName = ep.Employee.FirstName,
                EmployeeLastName = ep.Employee.LastName,
                EmployeeEmail = ep.Employee.Email,
                ProjectId = ep.ProjectId,
                ProjectName = ep.Project.Name,
                ProjectCode = ep.Project.ProjectCode,
                Role = ep.Role
            });

            return (result, null);
        }

        private EmployeeResponseDto MapToResponseDto(Employee employee)
        {
            return new EmployeeResponseDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name,
                ProjectCount = employee.EmployeeProjects?.Count ?? 0
            };
        }
    }
}
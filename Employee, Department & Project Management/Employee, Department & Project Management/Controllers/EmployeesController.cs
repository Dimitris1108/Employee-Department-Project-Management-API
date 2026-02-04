using EmployeeDepartmentAndProjectManagement.DTOs.Employee;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();
            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found" });

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (employee, errorMessage) = await _service.CreateAsync(dto);

            if (employee == null)
                return BadRequest(new { message = errorMessage });

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (employee, errorMessage) = await _service.UpdateAsync(id, dto);

            if (employee == null)
            {
                if (errorMessage.Contains("not found"))
                    return NotFound(new { message = errorMessage });

                return BadRequest(new { message = errorMessage });
            }

            return Ok(employee);
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, errorMessage) = await _service.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = errorMessage });

            return NoContent();
        }

        // GET: api/employees/5/projects
        [HttpGet("{id}/projects")]
        public async Task<IActionResult> GetEmployeeProjects(int id)
        {
            var (projects, errorMessage) = await _service.GetEmployeeProjectsAsync(id);

            if (projects == null)
                return NotFound(new { message = errorMessage });

            return Ok(projects);
        }
    }
}
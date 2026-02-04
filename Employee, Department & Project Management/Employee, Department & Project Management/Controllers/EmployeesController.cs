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
        /// <summary>
        /// Get all employee info. 
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();
            return Ok(employees);
        }

        // GET: api/employees/5
        /// <summary>
        /// Get employee id 
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} not found" });

            return Ok(employee);
        }

        // POST: api/employees
        /// <summary>
        /// Creates employee entry.
        /// </summary>
        /// <param name="dto">CreateEmployeeDto.</param>
        /// <returns>IActionResult.</returns>
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
        /// <summary>
        /// Update employee entry.
        /// </summary>
        /// <param name="id">Emplyee ID.</param>
        /// <param name="dto">UpdateEmplyeeDto.</param>
        /// <returns>IActionResult.</returns>
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
        /// <summary>
        /// Delete entry.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>IActionResult.</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, errorMessage) = await _service.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = errorMessage });

            return NoContent();
        }

        // GET: api/employees/5/projects
        /// <summary>
        /// Get Employee projects.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>IActionResult.</returns>
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
using EmployeeDepartmentAndProjectManagement.DTOs.Department;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: api/departments
        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllAsync();
            return Ok(departments);
        }

        // GET: api/departments/5
        /// <summary>
        /// Get department by id.
        /// </summary>
        /// <param name="id">Department Id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null)
                return NotFound(new { message = $"Department with ID {id} not found" });

            return Ok(department);
        }

        // POST: api/departments
        /// <summary>
        /// Creates department entry.
        /// </summary>
        /// <param name="dto">CreateDepartmentDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/departments/5
        /// <summary>
        /// Update department entry.
        /// </summary>
        /// <param name="id">DepartmentID.</param>
        /// <param name="dto">UpdateDepartmentDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound(new { message = $"Department with ID {id} not found" });

            return Ok(updated);
        }

        // DELETE: api/departments/5
        /// <summary>
        /// Delete entry.
        /// </summary>
        /// <param name="id">DepartmentID.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, errorMessage) = await _service.DeleteAsync(id);

            if (!success)
            {
                if (errorMessage.Contains("not found"))
                    return NotFound(new { message = errorMessage });

                return BadRequest(new { message = errorMessage });
            }

            return NoContent();
        }

        // GET: api/departments/5/budget
        /// <summary>
        /// Get total project budget for a department.
        /// </summary>
        /// <param name="id">DepartmentID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}/budget")]
        public async Task<IActionResult> GetTotalBudget(int id)
        {
            var result = await _service.GetTotalBudgetAsync(id);

            if (result == null)
                return NotFound(new { message = $"Department with ID {id} not found" });

            return Ok(result);
        }
    }
}
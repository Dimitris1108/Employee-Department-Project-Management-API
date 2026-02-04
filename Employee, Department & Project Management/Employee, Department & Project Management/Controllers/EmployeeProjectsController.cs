using EmployeeDepartmentAndProjectManagement.DTOs.EmployeeProjects;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeProjectsController : ControllerBase
    {
        private readonly IEmployeeProjectService _service;

        public EmployeeProjectsController(IEmployeeProjectService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all employee project info.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _service.GetAllAsync();
            return Ok(assignments);
        }

        /// <summary>
        /// Get employee project ID  
        /// </summary>
        /// <param name="employeeId">employeeId.</param>
        /// <param name="projectId">projectId</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{employeeId}/{projectId}")]
        public async Task<IActionResult> GetById(int employeeId, int projectId)
        {
            var assignment = await _service.GetByIdAsync(employeeId, projectId);

            if (assignment == null)
                return NotFound(new { message = "Assignment not found" });

            return Ok(assignment);
        }

        /// <summary>
        /// Create employee project entry.
        /// </summary>
        /// <param name="dto">AssignEmployeeDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Assign(AssignEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (assignment, errorMessage) = await _service.AssignAsync(dto);

            if (assignment == null)
                return BadRequest(new { message = errorMessage });

            return CreatedAtAction(
                nameof(GetById),
                new { employeeId = assignment.EmployeeId, projectId = assignment.ProjectId },
                assignment
            );
        }

        /// <summary>
        /// Update employee project entry.
        /// </summary>
        /// <param name="employeeId">employeeId.</param>
        /// <param name="projectId">projectId.</param>
        /// <param name="dto">UpdateAssignmentDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{employeeId}/{projectId}")]
        public async Task<IActionResult> UpdateRole(int employeeId, int projectId, UpdateAssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (assignment, errorMessage) = await _service.UpdateRoleAsync(employeeId, projectId, dto);

            if (assignment == null)
                return NotFound(new { message = errorMessage });

            return Ok(assignment);
        }

        /// <summary>
        /// Delete employee project entry
        /// </summary>
        /// <param name="employeeId">employeeId.</param>
        /// <param name="projectId">projectId.</param>
        /// <returns>IActionResult.</returns>

        [HttpDelete("{employeeId}/{projectId}")]
        public async Task<IActionResult> Remove(int employeeId, int projectId)
        {
            var (success, errorMessage) = await _service.RemoveAsync(employeeId, projectId);

            if (!success)
                return NotFound(new { message = errorMessage });

            return NoContent();
        }
    }
}
    


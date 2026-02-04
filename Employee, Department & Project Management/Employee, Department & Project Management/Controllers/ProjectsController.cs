using EmployeeDepartmentAndProjectManagement.DTOs.Project;
using EmployeeDepartmentAndProjectManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        // GET: api/projects
        /// <summary>
        /// Get all project info.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _service.GetAllAsync();
            return Ok(projects);
        }

        // GET: api/projects/5
        /// <summary>
        /// Get project ID.
        /// </summary>
        /// <param name="id">Project ID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _service.GetByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found" });

            return Ok(project);
        }

        // POST: api/projects
        /// <summary>
        /// Create project entry.
        /// </summary>
        /// <param name="dto">CreateProjectDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (project, errorMessage) = await _service.CreateAsync(dto);

            if (project == null)
                return BadRequest(new { message = errorMessage });

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        // PUT: api/projects/5
        /// <summary>
        /// Update project entry.
        /// </summary>
        /// <param name="id">Project ID.</param>
        /// <param name="dto">UpdateProjectDto.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (project, errorMessage) = await _service.UpdateAsync(id, dto);

            if (project == null)
            {
                if (errorMessage.Contains("not found"))
                    return NotFound(new { message = errorMessage });

                return BadRequest(new { message = errorMessage });
            }

            return Ok(project);
        }

        // DELETE: api/projects/5
        /// <summary>
        /// Delete project entry.
        /// </summary>
        /// <param name="id">Project ID.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, errorMessage) = await _service.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = errorMessage });

            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMSAPI.Data;
using PMSAPI.Models;

namespace PMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _context.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectModel project)
        {
            if (project == null)
                return BadRequest();

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectModel updatedProject)
        {
            if (id != updatedProject.Id)
                return BadRequest();

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

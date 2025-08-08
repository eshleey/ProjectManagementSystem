using Microsoft.AspNetCore.Mvc;
using PMSAPI.Data;

namespace PMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("byproject/{projectId}")]
        public IActionResult GetByProjectId(int projectId)
        {
            var tasks = _context.Tasks
                                .Where(t => t.ProjectId == projectId)
                                .ToList();
            return Ok(tasks);
        }
    }
}
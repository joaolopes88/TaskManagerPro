using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskService.Data;
using TaskService.Models;
using Shared; // Import the User class

namespace TaskService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            // Check if the associated user exists
            var userExists = await _context.Users.AnyAsync(u => u.Username == task.Username);
            if (!userExists)
            {
                return BadRequest("The specified user does not exist.");
            }

            // Add the task to the database
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = task.TaskID }, task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return task;
        }
    }
}
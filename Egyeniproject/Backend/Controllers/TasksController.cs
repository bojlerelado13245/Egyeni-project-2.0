using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Backend.Models;
using MyApp.Backend.Data;

namespace MyApp.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        return task;
    }

    // POST: api/tasks/{userId}/tasks
    [HttpPost("{userId}/tasks")]
    public async Task<ActionResult<Tasks>> CreateTask(Guid userId, [FromBody] Tasks task)
    {
        // verify user exists
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound("User not found");

        
        task.OwnerId = userId;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }
// PATCH: api/tasks/{id}/done
    [HttpPatch("{id}/done")]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.IsDone = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
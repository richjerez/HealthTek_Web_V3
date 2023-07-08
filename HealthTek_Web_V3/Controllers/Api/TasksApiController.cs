using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.Api
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TasksApiController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TasksApiController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/TasksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/TasksApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }
        // GET: api/tasks/archived/5
        [HttpGet("{Status}/{id}")]
        public async Task<IActionResult> StatusChange(string Status, int id)
        {
            var Task = await _context.Tasks.FindAsync(id);
            Task.TaskStatus = Status;
            _context.Tasks.Update(Task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/TasksApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            if (id != tasks.TasksId)
            {
                return BadRequest();
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TasksApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.TasksId }, tasks);
        }

        // DELETE: api/TasksApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TasksId == id);
        }

        // Update Assignment --> Task (accepted/rejected)
        // Update Task Status to complete 
        // Create New Task (accepted/rejected)
        // GET: api/tasks/archived/5
        [HttpGet("{assignment}/{taskId}/{taskaction}")]
        public async Task<ActionResult> UpdateTask(string assignment, int taskId, string taskaction)
        {
            var parsed = Int32.Parse(assignment);
            var assignments = _context.Assignments.FirstOrDefault(m => m.AssignmentsId == parsed);
            var tasks = _context.Tasks.Include(m => m.Notes).Include(m => m.FkAssignedBy).FirstOrDefault(m => m.TasksId == taskId);

            var statusArchived = "Archived";
            var statusNew = "New";
            var statusCompleted = "Completed";
            var replyType = "Reply";
            var user = await _userManager.GetUserAsync(User);

            tasks.LastUpdateDate = DateTime.Now;
            tasks.TaskStatus = statusCompleted;

            switch (taskaction)
            {
                case "accepted":
                    // Update Assignment --> Task (accepted)
                    assignments.IsConfirmed = true;
                    assignments.AssignmentEffectiveDate = DateTime.Now;
                    assignments.AssignmentExpirationDate = DateTime.Now;
                    assignments.LastUpdateDate = DateTime.Now;
                    assignments.AssignmentStatus = statusArchived;
                    _context.Assignments.Update(assignments);
                    _context.SaveChanges();

                    // Create NewTask
                    Tasks newTask = new Tasks();
                    newTask.IsCleared = false;
                    newTask.CreationDate = DateTime.Now;
                    newTask.LastUpdateDate = DateTime.Now;
                    newTask.FkAssignedById = user.FkEmployeesId;
                    newTask.FkAssignedToId = tasks.FkAssignedById;
                    newTask.TaskStatus = statusNew;
                    newTask.TaskType = replyType;
                    newTask.TaskNote = tasks.FkAssignedBy.EmployeeLabel + " has accepted the assignment.";
                    newTask.TaskSubject = "New Message";
                    newTask.TaskDescription = "Assignment Reply";
                    _context.Tasks.Add(newTask);
                    _context.SaveChanges();

                    break;
                case "rejected":
                    // Update Assignment --> Task(rejected)
                    assignments.AssignmentEffectiveDate = DateTime.Now;
                    assignments.IsConfirmed = false;
                    assignments.AssignmentPosition = "Rejected";
                    assignments.LastUpdateDate = DateTime.Now;
                    _context.Assignments.Update(assignments);
                    _context.SaveChanges();

                    // Create RejectedNewTask
                    Tasks rejectedNewTask = new Tasks();
                    rejectedNewTask.IsCleared = false;
                    rejectedNewTask.CreationDate = DateTime.Now;
                    rejectedNewTask.LastUpdateDate = DateTime.Now;
                    rejectedNewTask.FkAssignedById = user.FkEmployeesId;
                    rejectedNewTask.FkAssignedToId = tasks.FkAssignedById;
                    rejectedNewTask.TaskStatus = statusNew;
                    rejectedNewTask.TaskType = replyType;
                    rejectedNewTask.TaskNote = tasks.FkAssignedBy.EmployeeLabel + " has rejected the assignment.";
                    rejectedNewTask.TaskSubject = "New Message";
                    rejectedNewTask.TaskDescription = "Assignment Reply";
                    _context.Tasks.Add(rejectedNewTask);
                    _context.SaveChanges();

                    break;
            }
            // Update Task
            _context.Tasks.Update(tasks);
            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}

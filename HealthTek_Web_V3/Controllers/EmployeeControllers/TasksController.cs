using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "TaskViews")]
    public class TasksController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        ExternalLists externalLists = new ExternalLists();

        public TasksController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> ExportTasksData()
        {
            var user = await _userManager.GetUserAsync(User);
            //code to get employee list
            var employeeData = user.FkEmployeesId;
            var fileDownloadName = "EmployeeTasks.csv";
            return new CSVExporter(employeeData, fileDownloadName, _context, "Tasks");
        }

        public async Task<JsonResult> StatusChange(string Status, int id)
        {
            var Task = await _context.Tasks.FindAsync(id);
            Task.TaskStatus = Status;
            _context.Tasks.Update(Task);
            await _context.SaveChangesAsync();
            return Json(new { });
        }
        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var identityContext = await _context.Tasks.Where(m => m.FkAssignedToId == user.FkEmployeesId && m.TaskStatus != "Archived")
                .Include(t => t.FkAssignedBy)
                .Include(t => t.FkAssignedTo)
                .Include(t => t.Notes)
                .OrderByDescending(m => m.CreationDate).ToListAsync();
            ViewData["Types"] = new SelectList(externalLists.TaskTypes);
            ViewData["Status"] = new SelectList(externalLists.TaskStatuses);
            return View(identityContext);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.FkAssignedBy)
                .Include(t => t.Notes)
                .ThenInclude(t => t.FkEmployees)
                .FirstOrDefaultAsync(m => m.TasksId == id);
            if (tasks == null)
            {
                return NotFound();
            }
            tasks.IsCleared = true;
            _context.Update(tasks);
            await _context.SaveChangesAsync();

            var assignmentType = "Assignment";
            var denyqa = "Rejected";
            if (tasks.TaskType == assignmentType || tasks.TaskType == "Reply" && tasks.TaskIdentifier != null && tasks.TaskIdentifier != string.Empty)
            {
                tasks.Assignment = await _context.Assignments.Include(c => c.FkClients).FirstOrDefaultAsync(a => a.AssignmentsId == Int32.Parse(tasks.TaskIdentifier));
            }
            if (tasks.TaskStatus == denyqa)
            {
                tasks.Appointment = await _context.Appointments.Include(c => c.FkClients).FirstOrDefaultAsync(a => a.AppointmentsId == Int32.Parse(tasks.TaskIdentifier));
            }
            return PartialView(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create(string? id)
        {
            ViewData["FkAssignedToId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeeLabel");
            ViewData["Status"] = new SelectList(externalLists.TaskStatuses);
            ViewData["Types"] = new SelectList(externalLists.TaskTypes);
            if (id != null)
            {
                ViewData["FkAssignedToId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeeLabel", id);
            }
            return PartialView();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                tasks.FkAssignedById = user.FkEmployeesId;
                tasks.CreationDate = DateTime.Now;
                tasks.LastUpdateDate = DateTime.Now;
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }

            ViewData["FkAssignedById"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedById);
            ViewData["FkAssignedToId"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedToId);
            ViewData["Status"] = new SelectList(externalLists.TaskStatuses, tasks.TaskStatus);
            ViewData["Types"] = new SelectList(externalLists.TaskTypes, tasks.TaskType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", tasks) });
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["FkAssignedById"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedById);
            ViewData["FkAssignedToId"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedToId);
            ViewData["Status"] = new SelectList(externalLists.TaskStatuses, tasks.TaskStatus);
            ViewData["Types"] = new SelectList(externalLists.TaskTypes, tasks.TaskType);
            return PartialView(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Tasks tasks)
        {
            if (id != tasks.TasksId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.TasksId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkAssignedById"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedById);
            ViewData["FkAssignedToId"] = new SelectList(_context.Employees, "EmployeesId", "FullName", tasks.FkAssignedToId);
            ViewData["Status"] = new SelectList(externalLists.TaskStatuses, tasks.TaskStatus);
            ViewData["Types"] = new SelectList(externalLists.TaskTypes, tasks.TaskType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", tasks) });
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TasksId == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return PartialView(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TasksId == id);
        }

        // Update Assignment --> Task (accepted/rejected)
        // Update Task Status to complete 
        // Create New Task (accepted/rejected)
        public async Task<ActionResult> UpdateTask(string assignment, int taskId, string taskaction)
        {
            var parsed = Int32.Parse(assignment);
            var assignments = _context.Assignments.FirstOrDefault(m => m.AssignmentsId == parsed);
            var tasks = _context.Tasks.Include(m => m.Notes).Include(m => m.FkAssignedBy).FirstOrDefault(m => m.TasksId == taskId);

            var statusNew = "New";
            var statusCompleted = "Completed";
            var replyType = "Reply";
            var user = await _userManager.GetUserAsync(User);

            tasks.LastUpdateDate = DateTime.Now;
            tasks.CompletedDate = DateTime.Now;
            tasks.TaskStatus = statusCompleted;

            switch (taskaction)
            {
                case "accepted":
                    // Update Assignment --> Task (accepted)
                    assignments.IsConfirmed = true;
                    assignments.AssignmentEffectiveDate = DateTime.Now;
                    assignments.AssignmentExpirationDate = DateTime.Now;
                    assignments.LastUpdateDate = DateTime.Now;
                    assignments.AssignmentStatus = "Accepted";
                    _context.Assignments.Update(assignments);
                    _context.SaveChanges();

                    // Create NewTask
                    Tasks newTask = new Tasks();
                    newTask.IsCleared = false;
                    newTask.CreationDate = DateTime.Now;
                    newTask.LastUpdateDate = DateTime.Now;
                    newTask.FkAssignedById = user.FkEmployeesId;
                    newTask.FkAssignedToId = tasks.FkAssignedById;
                    newTask.TaskIdentifier = assignments.AssignmentsId.ToString();
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
                    rejectedNewTask.TaskIdentifier = assignments.AssignmentsId.ToString();
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

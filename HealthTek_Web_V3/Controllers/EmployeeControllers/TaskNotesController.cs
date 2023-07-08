using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class TaskNotesController : Controller
    {
        private readonly IdentityContext _context;

        public TaskNotesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: TaskNotes
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.TaskNotes.Include(t => t.FkAssignments).Include(t => t.FkTasks);
            return View(await identityContext.ToListAsync());
        }

        // GET: TaskNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskNotes = await _context.TaskNotes
                .Include(t => t.FkAssignments)
                .Include(t => t.FkTasks)
                .FirstOrDefaultAsync(m => m.TaskNotesId == id);
            if (taskNotes == null)
            {
                return NotFound();
            }

            return View(taskNotes);
        }

        // GET: TaskNotes/Create
        public IActionResult Create()
        {
            ViewData["FkAssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "AssignmentsId");
            ViewData["FkTasksId"] = new SelectList(_context.Tasks, "TasksId", "TasksId");
            return View();
        }

        // POST: TaskNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskNotes taskNotes)
        {
            if (ModelState.IsValid)
            {
                taskNotes.CreationDate = DateTime.Now;
                taskNotes.LastUpdateDate = DateTime.Now;
                _context.Add(taskNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tasks");
            }
            ViewData["FkAssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "AssignmentsId", taskNotes.FkAssignmentsId);
            ViewData["FkTasksId"] = new SelectList(_context.Tasks, "TasksId", "TasksId", taskNotes.FkTasksId);
            return View(taskNotes);
        }

        // GET: TaskNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskNotes = await _context.TaskNotes.FindAsync(id);
            if (taskNotes == null)
            {
                return NotFound();
            }
            ViewData["FkAssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "AssignmentsId", taskNotes.FkAssignmentsId);
            ViewData["FkTasksId"] = new SelectList(_context.Tasks, "TasksId", "TasksId", taskNotes.FkTasksId);
            return View(taskNotes);
        }

        // POST: TaskNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] TaskNotes taskNotes)
        {
            if (id != taskNotes.TaskNotesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskNotesExists(taskNotes.TaskNotesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAssignmentsId"] = new SelectList(_context.Assignments, "AssignmentsId", "AssignmentsId", taskNotes.FkAssignmentsId);
            ViewData["FkTasksId"] = new SelectList(_context.Tasks, "TasksId", "TasksId", taskNotes.FkTasksId);
            return View(taskNotes);
        }

        // GET: TaskNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskNotes = await _context.TaskNotes
                .Include(t => t.FkAssignments)
                .Include(t => t.FkTasks)
                .FirstOrDefaultAsync(m => m.TaskNotesId == id);
            if (taskNotes == null)
            {
                return NotFound();
            }

            return View(taskNotes);
        }

        // POST: TaskNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskNotes = await _context.TaskNotes.FindAsync(id);
            _context.TaskNotes.Remove(taskNotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskNotesExists(int id)
        {
            return _context.TaskNotes.Any(e => e.TaskNotesId == id);
        }
    }
}

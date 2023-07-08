using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ServiceControllers
{
    [Authorize]
    public class SupportTicketsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalLists = new ExternalLists();
        private readonly UserManager<AppUser> _userManager;

        public SupportTicketsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Policy = "SUPERUSER")]
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.SupportTickets.Include(s => s.FkAssignedBy);
            return View(await identityContext.ToListAsync());
        }

        // GET: SupportTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTickets = await _context.SupportTickets
                .Include(s => s.FkAssignedBy)
                .FirstOrDefaultAsync(m => m.SupportTicketsId == id);
            if (supportTickets == null)
            {
                return NotFound();
            }

            return View(supportTickets);
        }

        // GET: SupportTickets/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            SupportTickets supportTickets = new SupportTickets();
            supportTickets.FkAssignedById = user.FkEmployeesId;
            ViewData["Views"] = new SelectList(_externalLists.ApplicationViews);
            return PartialView(supportTickets);
        }

        // POST: SupportTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SupportTickets supportTickets)
        {
            if (ModelState.IsValid)
            {
                supportTickets.CreationDate = DateTime.Now;
                supportTickets.LastUpdateDate = DateTime.Now;
                _context.Add(supportTickets);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Views"] = new SelectList(_externalLists.ApplicationViews);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", supportTickets) });
        }

        // GET: SupportTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTickets = await _context.SupportTickets.FindAsync(id);
            if (supportTickets == null)
            {
                return NotFound();
            }
            ViewData["Views"] = new SelectList(_externalLists.ApplicationViews);
            return PartialView(supportTickets);
        }

        // POST: SupportTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] SupportTickets supportTickets)
        {
            if (id != supportTickets.SupportTicketsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    supportTickets.LastUpdateDate = DateTime.Now;
                    _context.Update(supportTickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportTicketsExists(supportTickets.SupportTicketsId))
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
            ViewData["Views"] = new SelectList(_externalLists.ApplicationViews);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", supportTickets) });
        }

        // GET: SupportTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTickets = await _context.SupportTickets
                .Include(s => s.FkAssignedBy)
                .FirstOrDefaultAsync(m => m.SupportTicketsId == id);
            if (supportTickets == null)
            {
                return NotFound();
            }

            return PartialView(supportTickets);
        }

        // POST: SupportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supportTickets = await _context.SupportTickets.FindAsync(id);
            _context.SupportTickets.Remove(supportTickets);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool SupportTicketsExists(int id)
        {
            return _context.SupportTickets.Any(e => e.SupportTicketsId == id);
        }
    }
}

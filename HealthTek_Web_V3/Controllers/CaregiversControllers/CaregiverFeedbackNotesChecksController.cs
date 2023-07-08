using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class CaregiverFeedbackNotesChecksController : Controller
    {
        private readonly IdentityContext _context;

        public CaregiverFeedbackNotesChecksController(IdentityContext context)
        {
            _context = context;
        }

        // GET: CaregiverFeedbackNotesChecks
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.CaregiverFeedbackNotesCheck.Include(c => c.FkBaProgressNotes).Include(c => c.FkCaregiverFeedback);
            return View(await identityContext.ToListAsync());
        }

        // GET: CaregiverFeedbackNotesChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedbackNotesCheck = await _context.CaregiverFeedbackNotesCheck
                .Include(c => c.FkBaProgressNotes)
                .Include(c => c.FkCaregiverFeedback)
                .FirstOrDefaultAsync(m => m.CaregiverFeedbackNotesCheckId == id);
            if (caregiverFeedbackNotesCheck == null)
            {
                return NotFound();
            }

            return View(caregiverFeedbackNotesCheck);
        }

        // GET: CaregiverFeedbackNotesChecks/Create
        public IActionResult Create()
        {
            ViewData["FkBaProgressNotesId"] = new SelectList(_context.BaProgressNotes, "BaProgressNotesId", "BaProgressNotesId");
            ViewData["FkCaregiverFeedbackId"] = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "CaregiverFeedbackId");
            return View();
        }

        // POST: CaregiverFeedbackNotesChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverFeedbackNotesCheck caregiverFeedbackNotesCheck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caregiverFeedbackNotesCheck);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkBaProgressNotesId"] = new SelectList(_context.BaProgressNotes, "BaProgressNotesId", "BaProgressNotesId", caregiverFeedbackNotesCheck.FkBaProgressNotesId);
            ViewData["FkCaregiverFeedbackId"] = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "CaregiverFeedbackId", caregiverFeedbackNotesCheck.FkCaregiverFeedbackId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverFeedbackNotesCheck) });
        }

        // GET: CaregiverFeedbackNotesChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedbackNotesCheck = await _context.CaregiverFeedbackNotesCheck.FindAsync(id);
            if (caregiverFeedbackNotesCheck == null)
            {
                return NotFound();
            }
            ViewData["FkBaProgressNotesId"] = new SelectList(_context.BaProgressNotes, "BaProgressNotesId", "BaProgressNotesId", caregiverFeedbackNotesCheck.FkBaProgressNotesId);
            ViewData["FkCaregiverFeedbackId"] = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "CaregiverFeedbackId", caregiverFeedbackNotesCheck.FkCaregiverFeedbackId);
            return View(caregiverFeedbackNotesCheck);
        }

        // POST: CaregiverFeedbackNotesChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CaregiverFeedbackNotesCheck caregiverFeedbackNotesCheck)
        {
            if (id != caregiverFeedbackNotesCheck.CaregiverFeedbackNotesCheckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caregiverFeedbackNotesCheck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverFeedbackNotesCheckExists(caregiverFeedbackNotesCheck.CaregiverFeedbackNotesCheckId))
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
            ViewData["FkBaProgressNotesId"] = new SelectList(_context.BaProgressNotes, "BaProgressNotesId", "BaProgressNotesId", caregiverFeedbackNotesCheck.FkBaProgressNotesId);
            ViewData["FkCaregiverFeedbackId"] = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "CaregiverFeedbackId", caregiverFeedbackNotesCheck.FkCaregiverFeedbackId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverFeedbackNotesCheck) });
        }

        // GET: CaregiverFeedbackNotesChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedbackNotesCheck = await _context.CaregiverFeedbackNotesCheck
                .Include(c => c.FkBaProgressNotes)
                .Include(c => c.FkCaregiverFeedback)
                .FirstOrDefaultAsync(m => m.CaregiverFeedbackNotesCheckId == id);
            if (caregiverFeedbackNotesCheck == null)
            {
                return NotFound();
            }

            return View(caregiverFeedbackNotesCheck);
        }

        // POST: CaregiverFeedbackNotesChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiverFeedbackNotesCheck = await _context.CaregiverFeedbackNotesCheck.FindAsync(id);
            _context.CaregiverFeedbackNotesCheck.Remove(caregiverFeedbackNotesCheck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaregiverFeedbackNotesCheckExists(int id)
        {
            return _context.CaregiverFeedbackNotesCheck.Any(e => e.CaregiverFeedbackNotesCheckId == id);
        }
    }
}

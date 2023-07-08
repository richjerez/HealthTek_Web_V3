using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class CaregiverFeedbackController : Controller
    {
        private readonly IdentityContext _context;

        public CaregiverFeedbackController(IdentityContext context)
        {
            _context = context;
        }

        // GET: CaregiverFeedback
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaregiverFeedback.ToListAsync());
        }

        // GET: CaregiverFeedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedback = await _context.CaregiverFeedback
                .FirstOrDefaultAsync(m => m.CaregiverFeedbackId == id);
            if (caregiverFeedback == null)
            {
                return NotFound();
            }

            return View(caregiverFeedback);
        }

        // GET: CaregiverFeedback/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: CaregiverFeedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverFeedback caregiverFeedback)
        {
            if (ModelState.IsValid)
            {
                caregiverFeedback.CreationDate = DateTime.Now;
                caregiverFeedback.LastUpdateDate = DateTime.Now;
                _context.Add(caregiverFeedback);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverFeedback) });
        }

        // GET: CaregiverFeedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedback = await _context.CaregiverFeedback.FindAsync(id);
            if (caregiverFeedback == null)
            {
                return NotFound();
            }
            return PartialView(caregiverFeedback);
        }

        // POST: CaregiverFeedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CaregiverFeedback caregiverFeedback)
        {
            if (id != caregiverFeedback.CaregiverFeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    caregiverFeedback.LastUpdateDate = DateTime.Now;
                    _context.Update(caregiverFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverFeedbackExists(caregiverFeedback.CaregiverFeedbackId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverFeedback) });
        }

        // GET: CaregiverFeedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverFeedback = await _context.CaregiverFeedback
                .FirstOrDefaultAsync(m => m.CaregiverFeedbackId == id);
            if (caregiverFeedback == null)
            {
                return NotFound();
            }

            return PartialView(caregiverFeedback);
        }

        // POST: CaregiverFeedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiverFeedback = await _context.CaregiverFeedback.FindAsync(id);
            _context.CaregiverFeedback.Remove(caregiverFeedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaregiverFeedbackExists(int id)
        {
            return _context.CaregiverFeedback.Any(e => e.CaregiverFeedbackId == id);
        }
    }
}

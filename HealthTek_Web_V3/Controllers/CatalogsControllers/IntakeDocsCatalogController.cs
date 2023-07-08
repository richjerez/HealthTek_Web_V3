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
    public class IntakeDocsCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public IntakeDocsCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: IntakeDocsCatalog
        public async Task<IActionResult> Index()
        {
            return View(await _context.IntakeDocsCatalog.ToListAsync());
        }

        // GET: IntakeDocsCatalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakeDocsCatalog = await _context.IntakeDocsCatalog
                .FirstOrDefaultAsync(m => m.IntakeDocsCatalogId == id);
            if (intakeDocsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(intakeDocsCatalog);
        }

        // GET: IntakeDocsCatalog/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: IntakeDocsCatalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] IntakeDocsCatalog intakeDocsCatalog)
        {
            if (ModelState.IsValid)
            {
                intakeDocsCatalog.CreationDate = DateTime.Now;
                intakeDocsCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(intakeDocsCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", intakeDocsCatalog) });
        }

        // GET: IntakeDocsCatalog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakeDocsCatalog = await _context.IntakeDocsCatalog.FindAsync(id);
            if (intakeDocsCatalog == null)
            {
                return NotFound();
            }
            return PartialView(intakeDocsCatalog);
        }

        // POST: IntakeDocsCatalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] IntakeDocsCatalog intakeDocsCatalog)
        {
            if (id != intakeDocsCatalog.IntakeDocsCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    intakeDocsCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(intakeDocsCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeDocsCatalogExists(intakeDocsCatalog.IntakeDocsCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", intakeDocsCatalog) });
        }

        // GET: IntakeDocsCatalog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakeDocsCatalog = await _context.IntakeDocsCatalog.FirstOrDefaultAsync(m => m.IntakeDocsCatalogId == id);
            if (intakeDocsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(intakeDocsCatalog);
        }

        // POST: IntakeDocsCatalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intakeDocsCatalog = await _context.IntakeDocsCatalog.FindAsync(id);
            _context.IntakeDocsCatalog.Remove(intakeDocsCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntakeDocsCatalogExists(int id)
        {
            return _context.IntakeDocsCatalog.Any(e => e.IntakeDocsCatalogId == id);
        }
    }
}

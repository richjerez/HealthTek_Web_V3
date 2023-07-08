using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.Catalogs
{
    [Authorize(Policy = "ADMIN")]
    public class CaregiverCompChecksCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public CaregiverCompChecksCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: CaregiverCompChecksCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaregiverCompChecksCatalog.ToListAsync());
        }

        // GET: CaregiverCompChecksCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecksCatalog = await _context.CaregiverCompChecksCatalog
                .FirstOrDefaultAsync(m => m.CaregiverCompChecksCatalogId == id);
            if (caregiverCompChecksCatalog == null)
            {
                return NotFound();
            }

            return View(caregiverCompChecksCatalog);
        }

        // GET: CaregiverCompChecksCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: CaregiverCompChecksCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverCompChecksCatalog caregiverCompChecksCatalog)
        {
            if (ModelState.IsValid)
            {
                caregiverCompChecksCatalog.CreationDate = DateTime.Now;
                caregiverCompChecksCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(caregiverCompChecksCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverCompChecksCatalog) });
        }

        // GET: CaregiverCompChecksCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecksCatalog = await _context.CaregiverCompChecksCatalog.FindAsync(id);
            if (caregiverCompChecksCatalog == null)
            {
                return NotFound();
            }
            return PartialView(caregiverCompChecksCatalog);
        }

        // POST: CaregiverCompChecksCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CaregiverCompChecksCatalog caregiverCompChecksCatalog)
        {
            if (id != caregiverCompChecksCatalog.CaregiverCompChecksCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    caregiverCompChecksCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(caregiverCompChecksCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverCompChecksCatalogExists(caregiverCompChecksCatalog.CaregiverCompChecksCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverCompChecksCatalog) });
        }

        // GET: CaregiverCompChecksCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecksCatalog = await _context.CaregiverCompChecksCatalog
                .FirstOrDefaultAsync(m => m.CaregiverCompChecksCatalogId == id);
            if (caregiverCompChecksCatalog == null)
            {
                return NotFound();
            }

            return PartialView(caregiverCompChecksCatalog);
        }

        // POST: CaregiverCompChecksCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiverCompChecksCatalog = await _context.CaregiverCompChecksCatalog.FindAsync(id);
            _context.CaregiverCompChecksCatalog.Remove(caregiverCompChecksCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaregiverCompChecksCatalogExists(int id)
        {
            return _context.CaregiverCompChecksCatalog.Any(e => e.CaregiverCompChecksCatalogId == id);
        }
    }
}

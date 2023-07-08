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
    public class ReplacementsCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public ReplacementsCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ReplacementsCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReplacementsCatalog.ToListAsync());
        }

        // GET: ReplacementsCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementsCatalog = await _context.ReplacementsCatalog
                .FirstOrDefaultAsync(m => m.ReplacementsCatalogId == id);
            if (replacementsCatalog == null)
            {
                return NotFound();
            }

            return View(replacementsCatalog);
        }

        // GET: ReplacementsCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: ReplacementsCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ReplacementsCatalog replacementsCatalog)
        {
            if (ModelState.IsValid)
            {
                replacementsCatalog.CreationDate = DateTime.Now;
                replacementsCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(replacementsCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", replacementsCatalog) });
        }

        // GET: ReplacementsCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementsCatalog = await _context.ReplacementsCatalog.FindAsync(id);
            if (replacementsCatalog == null)
            {
                return NotFound();
            }
            return PartialView(replacementsCatalog);
        }

        // POST: ReplacementsCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReplacementsCatalogId,Replacement,CreationDate,LastUpdateDate")] ReplacementsCatalog replacementsCatalog)
        {
            if (id != replacementsCatalog.ReplacementsCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    replacementsCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(replacementsCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplacementsCatalogExists(replacementsCatalog.ReplacementsCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", replacementsCatalog) });
        }

        // GET: ReplacementsCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementsCatalog = await _context.ReplacementsCatalog
                .FirstOrDefaultAsync(m => m.ReplacementsCatalogId == id);
            if (replacementsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(replacementsCatalog);
        }

        // POST: ReplacementsCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replacementsCatalog = await _context.ReplacementsCatalog.FindAsync(id);
            _context.ReplacementsCatalog.Remove(replacementsCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplacementsCatalogExists(int id)
        {
            return _context.ReplacementsCatalog.Any(e => e.ReplacementsCatalogId == id);
        }
    }
}

using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.CatalogsControllers
{
    [Authorize(Policy = "ADMIN")]
    public class PreferencesCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public PreferencesCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: PreferencesCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PreferencesCatalog.ToListAsync());
        }

        // GET: PreferencesCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferencesCatalog = await _context.PreferencesCatalog
                .FirstOrDefaultAsync(m => m.PreferencesCatalogId == id);
            if (preferencesCatalog == null)
            {
                return NotFound();
            }

            return View(preferencesCatalog);
        }

        // GET: PreferencesCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: PreferencesCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PreferencesCatalog preferencesCatalog)
        {
            if (ModelState.IsValid)
            {
                preferencesCatalog.CreationDate = DateTime.Now;
                preferencesCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(preferencesCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", preferencesCatalog) });
        }

        // GET: PreferencesCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferencesCatalog = await _context.PreferencesCatalog.FindAsync(id);
            if (preferencesCatalog == null)
            {
                return NotFound();
            }
            return PartialView(preferencesCatalog);
        }

        // POST: PreferencesCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] PreferencesCatalog preferencesCatalog)
        {
            if (id != preferencesCatalog.PreferencesCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    preferencesCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(preferencesCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferencesCatalogExists(preferencesCatalog.PreferencesCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", preferencesCatalog) });
        }

        // GET: PreferencesCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferencesCatalog = await _context.PreferencesCatalog
                .FirstOrDefaultAsync(m => m.PreferencesCatalogId == id);
            if (preferencesCatalog == null)
            {
                return NotFound();
            }

            return PartialView(preferencesCatalog);
        }

        // POST: PreferencesCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preferencesCatalog = await _context.PreferencesCatalog.FindAsync(id);
            _context.PreferencesCatalog.Remove(preferencesCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreferencesCatalogExists(int id)
        {
            return _context.PreferencesCatalog.Any(e => e.PreferencesCatalogId == id);
        }
    }
}

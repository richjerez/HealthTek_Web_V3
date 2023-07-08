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
    public class MaladaptivesCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public MaladaptivesCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: MaladaptivesCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaladaptivesCatalog.ToListAsync());
        }

        // GET: MaladaptivesCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptivesCatalog = await _context.MaladaptivesCatalog
                .FirstOrDefaultAsync(m => m.MaladaptivesCatalogId == id);
            if (maladaptivesCatalog == null)
            {
                return NotFound();
            }

            return View(maladaptivesCatalog);
        }

        // GET: MaladaptivesCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: MaladaptivesCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MaladaptivesCatalog maladaptivesCatalog)
        {
            if (ModelState.IsValid)
            {
                maladaptivesCatalog.CreationDate = DateTime.Now;
                maladaptivesCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(maladaptivesCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", maladaptivesCatalog) });
        }

        // GET: MaladaptivesCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptivesCatalog = await _context.MaladaptivesCatalog.FindAsync(id);
            if (maladaptivesCatalog == null)
            {
                return NotFound();
            }
            return PartialView(maladaptivesCatalog);
        }

        // POST: MaladaptivesCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] MaladaptivesCatalog maladaptivesCatalog)
        {
            if (id != maladaptivesCatalog.MaladaptivesCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    maladaptivesCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(maladaptivesCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaladaptivesCatalogExists(maladaptivesCatalog.MaladaptivesCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", maladaptivesCatalog) });
        }

        // GET: MaladaptivesCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptivesCatalog = await _context.MaladaptivesCatalog
                .FirstOrDefaultAsync(m => m.MaladaptivesCatalogId == id);
            if (maladaptivesCatalog == null)
            {
                return NotFound();
            }

            return PartialView(maladaptivesCatalog);
        }

        // POST: MaladaptivesCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maladaptivesCatalog = await _context.MaladaptivesCatalog.FindAsync(id);
            _context.MaladaptivesCatalog.Remove(maladaptivesCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaladaptivesCatalogExists(int id)
        {
            return _context.MaladaptivesCatalog.Any(e => e.MaladaptivesCatalogId == id);
        }
    }
}

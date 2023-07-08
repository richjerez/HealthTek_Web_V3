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
    public class EnvironmentalsCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public EnvironmentalsCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: EnvironmentalsCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnvironmentalsCatalog.ToListAsync());
        }

        // GET: EnvironmentalsCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalsCatalog = await _context.EnvironmentalsCatalog
                .FirstOrDefaultAsync(m => m.EnvironmentalsCatalogId == id);
            if (environmentalsCatalog == null)
            {
                return NotFound();
            }

            return View(environmentalsCatalog);
        }

        // GET: EnvironmentalsCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: EnvironmentalsCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EnvironmentalsCatalog environmentalsCatalog)
        {
            //ModelState.AddModelError("Description","Error This");
            if (ModelState.IsValid)
            {
                environmentalsCatalog.CreationDate = DateTime.Now;
                environmentalsCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(environmentalsCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", environmentalsCatalog) });
        }

        // GET: EnvironmentalsCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalsCatalog = await _context.EnvironmentalsCatalog.FindAsync(id);
            if (environmentalsCatalog == null)
            {
                return NotFound();
            }
            return PartialView(environmentalsCatalog);
        }

        // POST: EnvironmentalsCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EnvironmentalsCatalog environmentalsCatalog)
        {
            if (id != environmentalsCatalog.EnvironmentalsCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    environmentalsCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(environmentalsCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentalsCatalogExists(environmentalsCatalog.EnvironmentalsCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", environmentalsCatalog) });
        }

        // GET: EnvironmentalsCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalsCatalog = await _context.EnvironmentalsCatalog
                .FirstOrDefaultAsync(m => m.EnvironmentalsCatalogId == id);
            if (environmentalsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(environmentalsCatalog);
        }

        // POST: EnvironmentalsCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environmentalsCatalog = await _context.EnvironmentalsCatalog.FindAsync(id);
            _context.EnvironmentalsCatalog.Remove(environmentalsCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentalsCatalogExists(int id)
        {
            return _context.EnvironmentalsCatalog.Any(e => e.EnvironmentalsCatalogId == id);
        }
    }
}

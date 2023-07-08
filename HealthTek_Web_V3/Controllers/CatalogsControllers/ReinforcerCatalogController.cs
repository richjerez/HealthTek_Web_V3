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
    public class ReinforcerCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public ReinforcerCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ReinforcerCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReinforcerCatalog.ToListAsync());
        }

        // GET: ReinforcerCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reinforcerCatalog = await _context.ReinforcerCatalog
                .FirstOrDefaultAsync(m => m.ReinforcerCatalogId == id);
            if (reinforcerCatalog == null)
            {
                return NotFound();
            }

            return View(reinforcerCatalog);
        }

        // GET: ReinforcerCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: ReinforcerCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ReinforcerCatalog reinforcerCatalog)
        {
            if (ModelState.IsValid)
            {
                reinforcerCatalog.CreationDate = DateTime.Now;
                reinforcerCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(reinforcerCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", reinforcerCatalog) });
        }

        // GET: ReinforcerCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reinforcerCatalog = await _context.ReinforcerCatalog.FindAsync(id);
            if (reinforcerCatalog == null)
            {
                return NotFound();
            }
            return PartialView(reinforcerCatalog);
        }

        // POST: ReinforcerCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ReinforcerCatalog reinforcerCatalog)
        {
            if (id != reinforcerCatalog.ReinforcerCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reinforcerCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(reinforcerCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReinforcerCatalogExists(reinforcerCatalog.ReinforcerCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", reinforcerCatalog) });
        }

        // GET: ReinforcerCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reinforcerCatalog = await _context.ReinforcerCatalog
                .FirstOrDefaultAsync(m => m.ReinforcerCatalogId == id);
            if (reinforcerCatalog == null)
            {
                return NotFound();
            }

            return PartialView(reinforcerCatalog);
        }

        // POST: ReinforcerCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reinforcerCatalog = await _context.ReinforcerCatalog.FindAsync(id);
            _context.ReinforcerCatalog.Remove(reinforcerCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReinforcerCatalogExists(int id)
        {
            return _context.ReinforcerCatalog.Any(e => e.ReinforcerCatalogId == id);
        }
    }
}

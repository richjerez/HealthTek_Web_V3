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
    public class RbtCompTrainingsCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public RbtCompTrainingsCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: RbtCompTrainingsCatalogs
        public async Task<IActionResult> Index()
        {
            var identityContext = await _context.RbtCompTrainingsCatalog.ToListAsync();
            return View(identityContext);
        }

        // GET: RbtCompTrainingsCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainingsCatalog = await _context.RbtCompTrainingsCatalog
                .FirstOrDefaultAsync(m => m.RbtCompTrainingsCatalogId == id);
            if (rbtCompTrainingsCatalog == null)
            {
                return NotFound();
            }

            return View(rbtCompTrainingsCatalog);
        }

        // GET: RbtCompTrainingsCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: RbtCompTrainingsCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RbtCompTrainingsCatalog rbtCompTrainingsCatalog)
        {
            if (ModelState.IsValid)
            {
                rbtCompTrainingsCatalog.CreationDate = DateTime.Now;
                rbtCompTrainingsCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(rbtCompTrainingsCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", rbtCompTrainingsCatalog) });
        }

        // GET: RbtCompTrainingsCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainingsCatalog = await _context.RbtCompTrainingsCatalog.FindAsync(id);
            if (rbtCompTrainingsCatalog == null)
            {
                return NotFound();
            }
            return PartialView(rbtCompTrainingsCatalog);
        }

        // POST: RbtCompTrainingsCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RbtCompTrainingsCatalog rbtCompTrainingsCatalog)
        {
            if (id != rbtCompTrainingsCatalog.RbtCompTrainingsCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rbtCompTrainingsCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(rbtCompTrainingsCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RbtCompTrainingsCatalogExists(rbtCompTrainingsCatalog.RbtCompTrainingsCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", rbtCompTrainingsCatalog) });
        }

        // GET: RbtCompTrainingsCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainingsCatalog = await _context.RbtCompTrainingsCatalog
                .FirstOrDefaultAsync(m => m.RbtCompTrainingsCatalogId == id);
            if (rbtCompTrainingsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(rbtCompTrainingsCatalog);
        }

        // POST: RbtCompTrainingsCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rbtCompTrainingsCatalog = await _context.RbtCompTrainingsCatalog.FindAsync(id);
            _context.RbtCompTrainingsCatalog.Remove(rbtCompTrainingsCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RbtCompTrainingsCatalogExists(int id)
        {
            return _context.RbtCompTrainingsCatalog.Any(e => e.RbtCompTrainingsCatalogId == id);
        }
    }
}

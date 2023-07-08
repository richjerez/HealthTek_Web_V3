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
    public class ClientEventTypesCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public ClientEventTypesCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ClientEventTypesCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientEventTypesCatalog.ToListAsync());
        }

        // GET: ClientEventTypesCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientEventTypesCatalog = await _context.ClientEventTypesCatalog
                .FirstOrDefaultAsync(m => m.ClientEventTypesCatalogId == id);
            if (clientEventTypesCatalog == null)
            {
                return NotFound();
            }

            return View(clientEventTypesCatalog);
        }

        // GET: ClientEventTypesCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: ClientEventTypesCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientEventTypesCatalog clientEventTypesCatalog)
        {
            if (ModelState.IsValid)
            {
                clientEventTypesCatalog.CreationDate = DateTime.Now;
                clientEventTypesCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(clientEventTypesCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", clientEventTypesCatalog) });
        }

        // GET: ClientEventTypesCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientEventTypesCatalog = await _context.ClientEventTypesCatalog.FindAsync(id);
            if (clientEventTypesCatalog == null)
            {
                return NotFound();
            }
            return PartialView(clientEventTypesCatalog);
        }

        // POST: ClientEventTypesCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientEventTypesCatalog clientEventTypesCatalog)
        {
            if (id != clientEventTypesCatalog.ClientEventTypesCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientEventTypesCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(clientEventTypesCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientEventTypesCatalogExists(clientEventTypesCatalog.ClientEventTypesCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", clientEventTypesCatalog) });
        }

        // GET: ClientEventTypesCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientEventTypesCatalog = await _context.ClientEventTypesCatalog
                .FirstOrDefaultAsync(m => m.ClientEventTypesCatalogId == id);
            if (clientEventTypesCatalog == null)
            {
                return NotFound();
            }

            return PartialView(clientEventTypesCatalog);
        }

        // POST: ClientEventTypesCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientEventTypesCatalog = await _context.ClientEventTypesCatalog.FindAsync(id);
            _context.ClientEventTypesCatalog.Remove(clientEventTypesCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientEventTypesCatalogExists(int id)
        {
            return _context.ClientEventTypesCatalog.Any(e => e.ClientEventTypesCatalogId == id);
        }
    }
}

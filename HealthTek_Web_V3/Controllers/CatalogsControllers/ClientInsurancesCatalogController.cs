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
    public class ClientInsurancesCatalogController : Controller
    {
        private readonly IdentityContext _context;

        public ClientInsurancesCatalogController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ClientInsurancesCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientInsurancesCatalog.ToListAsync());
        }

        // GET: ClientInsurancesCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurancesCatalog = await _context.ClientInsurancesCatalog
                .FirstOrDefaultAsync(m => m.ClientInsurancesCatalogId == id);
            if (clientInsurancesCatalog == null)
            {
                return NotFound();
            }

            return View(clientInsurancesCatalog);
        }

        // GET: ClientInsurancesCatalogs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: ClientInsurancesCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientInsurancesCatalog clientInsurancesCatalog)
        {
            if (ModelState.IsValid)
            {
                clientInsurancesCatalog.CreationDate = DateTime.Now;
                clientInsurancesCatalog.LastUpdateDate = DateTime.Now;
                _context.Add(clientInsurancesCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", clientInsurancesCatalog) });
        }

        // GET: ClientInsurancesCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurancesCatalog = await _context.ClientInsurancesCatalog.FindAsync(id);
            if (clientInsurancesCatalog == null)
            {
                return NotFound();
            }
            return PartialView(clientInsurancesCatalog);
        }

        // POST: ClientInsurancesCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientInsurancesCatalog clientInsurancesCatalog)
        {
            if (id != clientInsurancesCatalog.ClientInsurancesCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientInsurancesCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(clientInsurancesCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientInsurancesCatalogExists(clientInsurancesCatalog.ClientInsurancesCatalogId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", clientInsurancesCatalog) });
        }

        // GET: ClientInsurancesCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurancesCatalog = await _context.ClientInsurancesCatalog
                .FirstOrDefaultAsync(m => m.ClientInsurancesCatalogId == id);
            if (clientInsurancesCatalog == null)
            {
                return NotFound();
            }

            return PartialView(clientInsurancesCatalog);
        }

        // POST: ClientInsurancesCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientInsurancesCatalog = await _context.ClientInsurancesCatalog.FindAsync(id);
            _context.ClientInsurancesCatalog.Remove(clientInsurancesCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientInsurancesCatalogExists(int id)
        {
            return _context.ClientInsurancesCatalog.Any(e => e.ClientInsurancesCatalogId == id);
        }
    }
}

using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class PreferencesController : Controller
    {
        private readonly IdentityContext _context;

        public PreferencesController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            ViewData["ClientId"] = id;
            return PartialView();
        }

        // GET: Preferences/Create
        public IActionResult Create(int id)
        {
            ViewData["FkReinforcersCatalogId"] = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName");
            Preferences preferences = new Preferences();
            preferences.FkClientsId = id;
            return PartialView(preferences);
        }

        // POST: Preferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Preferences preferences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preferences);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkReinforcersCatalogId"] = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName", preferences.FkReinforcersCatalogId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", preferences) });
        }

        // GET: Preferences/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var preferences = _context.Preferences.Include(m => m.FkReinforcersCatalog).FirstOrDefault(i => i.PreferencesId == id);
            if (preferences == null)
            {
                return NotFound();
            }
            ViewData["FkReinforcersCatalogId"] = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName", preferences.FkReinforcersCatalogId);
            return PartialView(preferences);
        }

        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Preferences preferences)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preferences);
                    await _context.SaveChangesAsync();
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferencesExists(preferences.PreferencesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["FkReinforcersCatalogId"] = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName", preferences.FkReinforcersCatalogId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", preferences) });
        }

        // GET: Preferences/Delete/5
        public async Task<IActionResult> Delete(int? id, string ClassName, string ActionName, int RouteId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences
                .Include(p => p.FkClients)
                .FirstOrDefaultAsync(m => m.PreferencesId == id);
            if (preferences == null)
            {
                return NotFound();
            }
            return PartialView(preferences);
        }

        // POST: Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] Preferences preferences)
        {
            _context.Preferences.Remove(preferences);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool PreferencesExists(int id)
        {
            return _context.Preferences.Any(e => e.PreferencesId == id);
        }
    }
}

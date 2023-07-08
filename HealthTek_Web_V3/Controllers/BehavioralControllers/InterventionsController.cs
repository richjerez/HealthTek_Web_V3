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
    public class InterventionsController : Controller
    {
        private readonly IdentityContext _context;

        public InterventionsController(IdentityContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> getDescription(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            return Json(new { statusText = intervention.InterventionDescription });
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Interventions.ToListAsync());
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interventions = await _context.Interventions
                .FirstOrDefaultAsync(m => m.InterventionsId == id);
            if (interventions == null)
            {
                return NotFound();
            }

            return View(interventions);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Interventions interventions)
        {
            if (ModelState.IsValid)
            {
                interventions.CreationDate = DateTime.Now;
                interventions.LastUpdateDate = DateTime.Now;
                _context.Add(interventions);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", interventions) });
        }

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interventions = await _context.Interventions.FindAsync(id);
            if (interventions == null)
            {
                return NotFound();
            }
            return PartialView(interventions);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Interventions interventions)
        {
            if (id != interventions.InterventionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    interventions.LastUpdateDate = DateTime.Now;
                    _context.Update(interventions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionsExists(interventions.InterventionsId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", interventions) });
        }

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interventions = await _context.Interventions
                .FirstOrDefaultAsync(m => m.InterventionsId == id);
            if (interventions == null)
            {
                return NotFound();
            }

            return PartialView(interventions);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interventions = await _context.Interventions.FindAsync(id);
            _context.Interventions.Remove(interventions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionsExists(int id)
        {
            return _context.Interventions.Any(e => e.InterventionsId == id);
        }
    }
}

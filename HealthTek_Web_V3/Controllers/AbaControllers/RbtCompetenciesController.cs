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
    public class RbtCompetenciesController : Controller
    {
        private readonly IdentityContext _context;

        public RbtCompetenciesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: RbtCompetencies
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.RbtCompetencies.Include(r => r.FkSupervisorSignatures);
            return View(await identityContext.ToListAsync());
        }

        // GET: RbtCompetencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompetencies = await _context.RbtCompetencies
                .Include(r => r.FkSupervisorSignatures)
                .FirstOrDefaultAsync(m => m.RbtCompetenciesId == id);
            if (rbtCompetencies == null)
            {
                return NotFound();
            }

            return View(rbtCompetencies);
        }

        // GET: RbtCompetencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompetencies = await _context.RbtCompetencies.Include(m => m.RbtCompTrainings).FirstOrDefaultAsync(i => i.RbtCompetenciesId == id);
            if (rbtCompetencies == null)
            {
                return NotFound();
            }
            var catalogs = _context.RbtCompTrainingsCatalog.AsNoTracking().ToList();
            if (rbtCompetencies.RbtCompTrainings.Count == 0)
            {
                foreach (var item in catalogs)
                {
                    RbtCompTrainings trainings = new RbtCompTrainings();
                    trainings.RbtCompTrainingsId = 0;
                    trainings.FkRbtCompetencies = null;
                    trainings.TrainingItem = item.TrainingItem;
                    trainings.FkRbtCompetenciesId = rbtCompetencies.RbtCompetenciesId;
                    _context.RbtCompTrainings.Add(trainings);
                    await _context.SaveChangesAsync();
                }
            }
            return PartialView(rbtCompetencies);
        }

        // POST: RbtCompetencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RbtCompetencies rbtCompetencies)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    rbtCompetencies.RbtCompetenciesId = id;
                    _context.Update(rbtCompetencies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RbtCompetenciesExists(rbtCompetencies.RbtCompetenciesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Populate Toast here";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["TrainingItem"] = new SelectList(_context.RbtCompTrainingsCatalog, "TrainingItem", "TrainingItem");
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", rbtCompetencies) });
        }

        // GET: RbtCompetencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompetencies = await _context.RbtCompetencies
                .Include(r => r.FkSupervisorSignatures)
                .FirstOrDefaultAsync(m => m.RbtCompetenciesId == id);
            if (rbtCompetencies == null)
            {
                return NotFound();
            }

            return View(rbtCompetencies);
        }

        // POST: RbtCompetencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rbtCompetencies = await _context.RbtCompetencies.FindAsync(id);
            _context.RbtCompetencies.Remove(rbtCompetencies);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool RbtCompetenciesExists(int id)
        {
            return _context.RbtCompetencies.Any(e => e.RbtCompetenciesId == id);
        }
    }
}

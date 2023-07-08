using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class ReplacementsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalList = new ExternalLists();

        public ReplacementsController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ArchiveReplacement(int id)
        {
            var replacements = _context.Replacements.FirstOrDefault(m => m.ReplacementsId == id);
            replacements.ArchivedDate = DateTime.Now;
            replacements.LastUpdateDate = DateTime.Now;
            _context.Replacements.Update(replacements);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Replacements
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Replacements.Include(r => r.FkBaAssessments).Include(r => r.FkMaladaptives);
            return View(await identityContext.ToListAsync());
        }

        // GET: Replacements/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: Replacements/Create
        public IActionResult Create(int id)
        {
            var maladaptives = _context.Maladaptives.FirstOrDefault(m => m.MaladaptivesId == id);
            Replacements replacements = new Replacements();
            replacements.FkMaladaptivesId = id;
            replacements.FkBaAssessmentsId = maladaptives.FkBaAssessmentsId;
            ViewData["Replacements"] = new SelectList(_context.Set<ReplacementsCatalog>(), "Replacement", "Replacement");
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod);
            ViewData["TimeFrame"] = new SelectList(_externalList.TimeFrame);
            return PartialView(replacements);
        }

        // POST: Replacements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Replacements replacements)
        {
            if (ModelState.IsValid)
            {
                if (replacements.Baseline1 != null && replacements.Baseline2 != null && replacements.Baseline3 != null)
                {
                    replacements.BaselineAverage = (replacements.Baseline1 + replacements.Baseline2 + replacements.Baseline3) / 3;
                }
                if (replacements.Baseline1 != null && replacements.Baseline2 != null && replacements.Baseline3 == null)
                {
                    replacements.BaselineAverage = (replacements.Baseline1 + replacements.Baseline2) / 2;
                }
                replacements.CreationDate = DateTime.Now;
                replacements.LastUpdateDate = DateTime.Now;

                _context.Add(replacements);
                await _context.SaveChangesAsync();

                var maladaptive = await _context.Maladaptives.Where(x => x.MaladaptivesId == replacements.FkMaladaptivesId).FirstOrDefaultAsync();
                maladaptive.FkReplacementsId = replacements.ReplacementsId;
                _context.Maladaptives.Update(maladaptive);
                await _context.SaveChangesAsync();

                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Replacements"] = new SelectList(_context.Set<ReplacementsCatalog>(), "Replacement", "Replacement");
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", replacements) });
        }

        // GET: Replacements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacements = await _context.Replacements.FindAsync(id);
            if (replacements == null)
            {
                return NotFound();
            }
            ViewData["Replacements"] = new SelectList(_context.Set<ReplacementsCatalog>(), "Replacement", "Replacement", replacements.ReplacementName);
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod, replacements.CollectionMethod);
            ViewData["TimeFrame"] = new SelectList(_externalList.TimeFrame, replacements.BaselineDurationUnit);
            return PartialView(replacements);
        }

        // POST: Replacements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Replacements replacements)
        {
            if (id != replacements.ReplacementsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (replacements.Baseline1 != null && replacements.Baseline2 != null && replacements.Baseline3 != null)
                    {
                        replacements.BaselineAverage = (replacements.Baseline1 + replacements.Baseline2 + replacements.Baseline3) / 3;
                    }
                    if (replacements.Baseline1 != null && replacements.Baseline2 != null && replacements.Baseline3 == null)
                    {
                        replacements.BaselineAverage = (replacements.Baseline1 + replacements.Baseline2) / 2;
                    }
                    replacements.LastUpdateDate = DateTime.Now;
                    _context.Update(replacements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplacementsExists(replacements.ReplacementsId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", replacements) });
        }

        // GET: Replacements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacements = await _context.Replacements
                .Include(r => r.FkMaladaptives)
                .FirstOrDefaultAsync(m => m.ReplacementsId == id);
            if (replacements == null)
            {
                return NotFound();
            }

            return PartialView(replacements);
        }

        // POST: Replacements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replacements = await _context.Replacements.FindAsync(id);
            _context.Replacements.Remove(replacements);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool ReplacementsExists(int id)
        {
            return _context.Replacements.Any(e => e.ReplacementsId == id);
        }
    }
}

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
    public class MaladaptivesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalList = new ExternalLists();

        public MaladaptivesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Maladaptives
        public async Task<IActionResult> ArchiveMaladaptive(int id)
        {
            var maladaptives = _context.Maladaptives.FirstOrDefault(m => m.MaladaptivesId == id);
            maladaptives.ArchivedDate = DateTime.Now;
            maladaptives.LastUpdateDate = DateTime.Now;
            _context.Maladaptives.Update(maladaptives);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Maladaptives.Include(m => m.FkBaAssessments).Include(m => m.FkClients);
            return View(await identityContext.ToListAsync());
        }

        // GET: Maladaptives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptives = await _context.Maladaptives
                .Include(m => m.FkBaAssessments)
                .Include(m => m.FkClients)
                .Include(m => m.FkMaladaptiveDischarges)
                .Include(m => m.FkCaregiverTrainingGoals)
                .FirstOrDefaultAsync(m => m.MaladaptivesId == id);
            if (maladaptives == null)
            {
                return NotFound();
            }

            return View(maladaptives);
        }

        // GET: Maladaptives/Create
        public IActionResult Create(int id)
        {
            ViewData["Maladaptives"] = new SelectList(_context.Set<MaladaptivesCatalog>(), "MaladaptiveName", "MaladaptiveName");
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod);
            ViewData["TimeFrame"] = new SelectList(_externalList.TimeFrame);
            var assesment = _context.BaAssessments.Include(m => m.FkAppointments).FirstOrDefault(m => m.BaAssessmentsId == id);
            Maladaptives maladaptives = new Maladaptives();
            maladaptives.FkClientsId = assesment.FkAppointments.FkClientsId.Value;
            maladaptives.FkBaAssessmentsId = id;
            return PartialView(maladaptives);
        }

        // POST: Maladaptives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Maladaptives maladaptives)
        {
            if (ModelState.IsValid)
            {
                if (maladaptives.Baseline1 != null && maladaptives.Baseline2 != null && maladaptives.Baseline3 != null)
                {
                    maladaptives.BaselineAverage = (maladaptives.Baseline1 + maladaptives.Baseline2 + maladaptives.Baseline3) / 3;
                }
                if (maladaptives.Baseline1 != null && maladaptives.Baseline2 != null && maladaptives.Baseline3 == null)
                {
                    maladaptives.BaselineAverage = (maladaptives.Baseline1 + maladaptives.Baseline2) / 2;
                }
                maladaptives.CreationDate = DateTime.Now;
                maladaptives.LastUpdateDate = DateTime.Now;
                _context.Add(maladaptives);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["TimeFrame"] = new SelectList(_externalList.TimeFrame);
            ViewData["Maladaptives"] = new SelectList(_context.Set<MaladaptivesCatalog>(), "MaladaptiveName", "MaladaptiveName", maladaptives.MaladaptiveName);
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod, maladaptives.CollectionMethod);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", maladaptives) });
        }

        // GET: Maladaptives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptives = await _context.Maladaptives.FindAsync(id);
            if (maladaptives == null)
            {
                return NotFound();
            }
            ViewData["TimeFrame"] = new SelectList(_externalList.TimeFrame);
            ViewData["Maladaptives"] = new SelectList(_context.Set<MaladaptivesCatalog>(), "MaladaptiveName", "MaladaptiveName", maladaptives.MaladaptiveName);
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod, maladaptives.CollectionMethod);
            return PartialView(maladaptives);
        }

        // POST: Maladaptives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Maladaptives maladaptives)
        {
            if (id != maladaptives.MaladaptivesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (maladaptives.Baseline1 != null && maladaptives.Baseline2 != null && maladaptives.Baseline3 != null)
                    {
                        maladaptives.BaselineAverage = (maladaptives.Baseline1 + maladaptives.Baseline2 + maladaptives.Baseline3) / 3;
                    }
                    if (maladaptives.Baseline1 != null && maladaptives.Baseline2 != null && maladaptives.Baseline3 == null)
                    {
                        maladaptives.BaselineAverage = (maladaptives.Baseline1 + maladaptives.Baseline2) / 2;
                    }
                    _context.Update(maladaptives);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaladaptivesExists(maladaptives.MaladaptivesId))
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
            ViewData["Maladaptives"] = new SelectList(_context.Set<MaladaptivesCatalog>(), "MaladaptiveName", "MaladaptiveName", maladaptives.MaladaptiveName);
            ViewData["Collection"] = new SelectList(_externalList.CollectionMethod, maladaptives.CollectionMethod);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", maladaptives) });
        }

        // GET: Maladaptives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptives = await _context.Maladaptives
                .Include(m => m.FkBaAssessments)
                .Include(m => m.FkClients)
                .Include(m => m.FkMaladaptiveDischarges)
                .Include(m => m.FkCaregiverTrainingGoals)
                .FirstOrDefaultAsync(m => m.MaladaptivesId == id);
            if (maladaptives == null)
            {
                return NotFound();
            }

            return PartialView(maladaptives);
        }

        // POST: Maladaptives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maladaptives = await _context.Maladaptives.Include(m => m.LongTermObjectives).Include(m => m.ShortTermObjectives).FirstOrDefaultAsync(m => m.MaladaptivesId == id);
            _context.Maladaptives.Remove(maladaptives);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool MaladaptivesExists(int id)
        {
            return _context.Maladaptives.Any(e => e.MaladaptivesId == id);
        }
    }
}

using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.BehavioralControllers
{
    [Authorize]
    public class BaAssessmentsInterventionsController : Controller
    {
        private readonly IdentityContext _context;

        public BaAssessmentsInterventionsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: BaAssessmentsInterventions
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.BaAssessmentsInterventions.Include(b => b.FkBaAssessments).Include(b => b.FkInterventions);
            return View(await identityContext.ToListAsync());
        }

        // GET: BaAssessmentsInterventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baAssessmentsInterventions = await _context.BaAssessmentsInterventions
                .Include(b => b.FkBaAssessments)
                .Include(b => b.FkInterventions)
                .FirstOrDefaultAsync(m => m.BaAssessmentsInterventionsId == id);
            if (baAssessmentsInterventions == null)
            {
                return NotFound();
            }

            return View(baAssessmentsInterventions);
        }

        // GET: BaAssessmentsInterventions/Create
        public IActionResult Create(int id)
        {
            ViewData["FkInterventionsId"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName");
            BaAssessmentsInterventions interventions = new BaAssessmentsInterventions();
            interventions.FkBaAssessmentsId = id;
            return PartialView(interventions);
        }

        // POST: BaAssessmentsInterventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BaAssessmentsInterventions baAssessmentsInterventions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baAssessmentsInterventions);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkInterventionsId"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName", baAssessmentsInterventions.FkInterventionsId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", baAssessmentsInterventions) });
        }

        // GET: BaAssessmentsInterventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baAssessmentsInterventions = await _context.BaAssessmentsInterventions.FindAsync(id);
            if (baAssessmentsInterventions == null)
            {
                return NotFound();
            }
            ViewData["FkInterventionsId"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName", baAssessmentsInterventions.FkInterventionsId);
            return PartialView(baAssessmentsInterventions);
        }

        // POST: BaAssessmentsInterventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaAssessmentsInterventions baAssessmentsInterventions)
        {
            if (id != baAssessmentsInterventions.BaAssessmentsInterventionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baAssessmentsInterventions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaAssessmentsInterventionsExists(baAssessmentsInterventions.BaAssessmentsInterventionsId))
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
            ViewData["FkBaAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId", baAssessmentsInterventions.FkBaAssessmentsId);
            ViewData["FkInterventionsId"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionsId", baAssessmentsInterventions.FkInterventionsId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", baAssessmentsInterventions) });
        }

        // GET: BaAssessmentsInterventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baAssessmentsInterventions = await _context.BaAssessmentsInterventions
                .Include(b => b.FkBaAssessments)
                .Include(b => b.FkInterventions)
                .FirstOrDefaultAsync(m => m.BaAssessmentsInterventionsId == id);
            if (baAssessmentsInterventions == null)
            {
                return NotFound();
            }

            return PartialView(baAssessmentsInterventions);
        }

        // POST: BaAssessmentsInterventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baAssessmentsInterventions = await _context.BaAssessmentsInterventions.FindAsync(id);
            _context.BaAssessmentsInterventions.Remove(baAssessmentsInterventions);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool BaAssessmentsInterventionsExists(int id)
        {
            return _context.BaAssessmentsInterventions.Any(e => e.BaAssessmentsInterventionsId == id);
        }
    }
}

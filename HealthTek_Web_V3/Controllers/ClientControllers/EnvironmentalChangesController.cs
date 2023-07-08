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
    public class EnvironmentalChangesController : Controller
    {
        private readonly IdentityContext _context;

        public EnvironmentalChangesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: EnvironmentalChanges
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.EnvironmentalChanges.Include(e => e.FkBaMonthlyReports).Include(e => e.FkBaProgressNotes);
            return View(await identityContext.ToListAsync());
        }

        // GET: EnvironmentalChanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalChanges = await _context.EnvironmentalChanges
                .Include(e => e.FkBaMonthlyReports)
                .Include(e => e.FkBaProgressNotes)
                .FirstOrDefaultAsync(m => m.EnvironmentalChangesId == id);
            if (environmentalChanges == null)
            {
                return NotFound();
            }

            return View(environmentalChanges);
        }

        // GET: EnvironmentalChanges/Create
        public IActionResult Create(int? FkBaMonthlyReportsId, int? FkBaProgressNotesId)
        {
            EnvironmentalChanges env = new EnvironmentalChanges();
            env.FkBaMonthlyReportsId = FkBaMonthlyReportsId;
            env.FkBaProgressNotesId = FkBaProgressNotesId;
            ViewData["Environmentals"] = new SelectList(_context.EnvironmentalsCatalog, "Category", "Category");
            return PartialView(env);
        }

        // POST: EnvironmentalChanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EnvironmentalChanges environmentalChanges)
        {
            if (ModelState.IsValid)
            {
                if (environmentalChanges.FkBaMonthlyReportsId == 0)
                {
                    environmentalChanges.FkBaMonthlyReportsId = null;
                }
                _context.Add(environmentalChanges);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkBaMonthlyReportsId"] = environmentalChanges.FkBaMonthlyReportsId;
            ViewData["FkBaProgressNotesId"] = environmentalChanges.FkBaProgressNotesId;
            ViewData["Environmentals"] = new SelectList(_context.EnvironmentalsCatalog, "Category", "Category");
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", environmentalChanges) });
        }

        // GET: EnvironmentalChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalChanges = await _context.EnvironmentalChanges.FindAsync(id);
            if (environmentalChanges == null)
            {
                return NotFound();
            }
            ViewData["FkBaMonthlyReportsId"] = environmentalChanges.FkBaMonthlyReportsId;
            ViewData["FkBaProgressNotesId"] = environmentalChanges.FkBaProgressNotesId;
            ViewData["Environmentals"] = new SelectList(_context.EnvironmentalsCatalog, "Category", "Category");
            return PartialView(environmentalChanges);
        }

        // POST: EnvironmentalChanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EnvironmentalChanges environmentalChanges)
        {
            if (id != environmentalChanges.EnvironmentalChangesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentalChanges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentalChangesExists(environmentalChanges.EnvironmentalChangesId))
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
            ViewData["FkBaMonthlyReportsId"] = environmentalChanges.FkBaMonthlyReportsId;
            ViewData["FkBaProgressNotesId"] = environmentalChanges.FkBaProgressNotesId;
            ViewData["Environmentals"] = new SelectList(_context.EnvironmentalsCatalog, "Category", "Category");
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", environmentalChanges) });
        }

        // GET: EnvironmentalChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalChanges = await _context.EnvironmentalChanges
                .Include(e => e.FkBaMonthlyReports)
                .Include(e => e.FkBaProgressNotes)
                .FirstOrDefaultAsync(m => m.EnvironmentalChangesId == id);
            if (environmentalChanges == null)
            {
                return NotFound();
            }

            return PartialView(environmentalChanges);
        }

        // POST: EnvironmentalChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environmentalChanges = await _context.EnvironmentalChanges.FindAsync(id);
            var newid = environmentalChanges.FkBaProgressNotesId;
            _context.EnvironmentalChanges.Remove(environmentalChanges);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool EnvironmentalChangesExists(int id)
        {
            return _context.EnvironmentalChanges.Any(e => e.EnvironmentalChangesId == id);
        }
    }
}

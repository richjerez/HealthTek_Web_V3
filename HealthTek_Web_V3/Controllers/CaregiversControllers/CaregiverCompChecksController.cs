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
    public class CaregiverCompChecksController : Controller
    {
        private readonly IdentityContext _context;

        public CaregiverCompChecksController(IdentityContext context)
        {
            _context = context;
        }

        // GET: CaregiverCompChecks
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.CaregiverCompChecks.Include(c => c.FkCaregiverCompetencies).Include(c => c.FkClients);
            return View(await identityContext.ToListAsync());
        }

        // GET: CaregiverCompChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecks = await _context.CaregiverCompChecks
                .Include(c => c.FkCaregiverCompetencies)
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.CaregiverCompChecksId == id);
            if (caregiverCompChecks == null)
            {
                return NotFound();
            }

            return View(caregiverCompChecks);
        }

        // GET: CaregiverCompChecks/Create
        public IActionResult Create(int id)
        {
            CaregiverCompChecks checks = new CaregiverCompChecks();
            checks.FkCaregiverCompetenciesId = id;
            return PartialView(checks);
        }

        // POST: CaregiverCompChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverCompChecks caregiverCompChecks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caregiverCompChecks);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverCompChecks) });
        }

        // GET: CaregiverCompChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecks = await _context.CaregiverCompChecks.FindAsync(id);
            if (caregiverCompChecks == null)
            {
                return NotFound();
            }
            ViewData["FkCaregiverCompetenciesId"] = new SelectList(_context.Set<CaregiverCompetencies>(), "CaregiverCompetenciesId", "CaregiverCompetenciesId", caregiverCompChecks.FkCaregiverCompetenciesId);
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "LastName", caregiverCompChecks.FkClientsId);
            return PartialView(caregiverCompChecks);
        }

        // POST: CaregiverCompChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CaregiverCompChecks caregiverCompChecks)
        {
            if (id != caregiverCompChecks.CaregiverCompChecksId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caregiverCompChecks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverCompChecksExists(caregiverCompChecks.CaregiverCompChecksId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverCompChecks) });
        }

        // GET: CaregiverCompChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompChecks = await _context.CaregiverCompChecks
                .Include(c => c.FkCaregiverCompetencies)
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.CaregiverCompChecksId == id);
            if (caregiverCompChecks == null)
            {
                return NotFound();
            }

            return View(caregiverCompChecks);
        }

        // POST: CaregiverCompChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiverCompChecks = await _context.CaregiverCompChecks.FindAsync(id);
            _context.CaregiverCompChecks.Remove(caregiverCompChecks);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool CaregiverCompChecksExists(int id)
        {
            return _context.CaregiverCompChecks.Any(e => e.CaregiverCompChecksId == id);
        }
    }
}

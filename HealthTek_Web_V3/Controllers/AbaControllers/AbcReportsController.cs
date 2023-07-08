using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class AbcReportsController : Controller
    {
        private readonly IdentityContext _context;

        public AbcReportsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: AbcReports
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.AbcReports.Include(a => a.FkBaAssessments);
            return View(await identityContext.ToListAsync());
        }

        // GET: AbcReports/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: AbcReports/Create
        public IActionResult Create(int? id)
        {
            AbcReports abcReports = new AbcReports();
            abcReports.FkBaAssessmentsId = id.Value;
            return PartialView(abcReports);
        }

        // POST: AbcReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AbcReports abcReports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abcReports);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", abcReports) });
        }

        // GET: AbcReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abcReports = await _context.AbcReports.FindAsync(id);
            if (abcReports == null)
            {
                return NotFound();
            }
            return PartialView(abcReports);
        }

        // POST: AbcReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] AbcReports abcReports)
        {
            if (id != abcReports.AbcReportsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abcReports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbcReportsExists(abcReports.AbcReportsId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", abcReports) });
        }

        // GET: AbcReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abcReports = await _context.AbcReports
                .Include(a => a.FkBaAssessments)
                .FirstOrDefaultAsync(m => m.AbcReportsId == id);
            if (abcReports == null)
            {
                return NotFound();
            }

            return PartialView(abcReports);
        }

        // POST: AbcReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abcReports = await _context.AbcReports.FindAsync(id);
            _context.AbcReports.Remove(abcReports);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool AbcReportsExists(int id)
        {
            return _context.AbcReports.Any(e => e.AbcReportsId == id);
        }
    }
}

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
    public class RbtCompTrainingsController : Controller
    {
        private readonly IdentityContext _context;

        public RbtCompTrainingsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: RbtCompTrainings
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.RbtCompTrainings.Include(r => r.FkRbtCompetencies);
            return View(await identityContext.ToListAsync());
        }

        // GET: RbtCompTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainings = await _context.RbtCompTrainings
                .Include(r => r.FkRbtCompetencies)
                .FirstOrDefaultAsync(m => m.RbtCompTrainingsId == id);
            if (rbtCompTrainings == null)
            {
                return NotFound();
            }

            return View(rbtCompTrainings);
        }

        // GET: RbtCompTrainings/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: RbtCompTrainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RbtCompTrainings rbtCompTrainings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rbtCompTrainings);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", rbtCompTrainings) });
        }

        // GET: RbtCompTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainings = await _context.RbtCompTrainings.FindAsync(id);
            if (rbtCompTrainings == null)
            {
                return NotFound();
            }
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId", rbtCompTrainings.FkRbtCompetenciesId);
            return View(rbtCompTrainings);
        }

        // POST: RbtCompTrainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RbtCompTrainings rbtCompTrainings)
        {
            if (id != rbtCompTrainings.RbtCompTrainingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rbtCompTrainings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RbtCompTrainingsExists(rbtCompTrainings.RbtCompTrainingsId))
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
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId", rbtCompTrainings.FkRbtCompetenciesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", rbtCompTrainings) });
        }

        // GET: RbtCompTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rbtCompTrainings = await _context.RbtCompTrainings
                .Include(r => r.FkRbtCompetencies)
                .FirstOrDefaultAsync(m => m.RbtCompTrainingsId == id);
            if (rbtCompTrainings == null)
            {
                return NotFound();
            }

            return View(rbtCompTrainings);
        }

        // POST: RbtCompTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rbtCompTrainings = await _context.RbtCompTrainings.FindAsync(id);
            _context.RbtCompTrainings.Remove(rbtCompTrainings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RbtCompTrainingsExists(int id)
        {
            return _context.RbtCompTrainings.Any(e => e.RbtCompTrainingsId == id);
        }
    }
}

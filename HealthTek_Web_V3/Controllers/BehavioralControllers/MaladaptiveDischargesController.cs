using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.BehavioralControllers
{
    [Authorize]
    public class MaladaptiveDischargesController : Controller
    {
        private readonly IdentityContext _context;

        public MaladaptiveDischargesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: MaladaptiveDischarges
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.MaladaptiveDischarges.Include(m => m.FkMaladaptives);
            return View(await identityContext.ToListAsync());
        }

        // GET: MaladaptiveDischarges/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: MaladaptiveDischarges/Create
        public IActionResult Create(int id)
        {
            MaladaptiveDischarges maladaptive = new MaladaptiveDischarges();
            maladaptive.FkMaladaptivesId = id;
            return PartialView(maladaptive);
        }

        // POST: MaladaptiveDischarges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MaladaptiveDischarges maladaptiveDischarges)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maladaptiveDischarges);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", maladaptiveDischarges) });
        }

        // GET: MaladaptiveDischarges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptiveDischarges = await _context.MaladaptiveDischarges.FindAsync(id);
            if (maladaptiveDischarges == null)
            {
                return NotFound();
            }
            return PartialView(maladaptiveDischarges);
        }

        // POST: MaladaptiveDischarges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] MaladaptiveDischarges maladaptiveDischarges)
        {
            if (id != maladaptiveDischarges.MaladaptiveDischargesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maladaptiveDischarges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaladaptiveDischargesExists(maladaptiveDischarges.MaladaptiveDischargesId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", maladaptiveDischarges) });
        }

        // GET: MaladaptiveDischarges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptiveDischarges = await _context.MaladaptiveDischarges
                .Include(m => m.FkMaladaptives)
                .FirstOrDefaultAsync(m => m.MaladaptiveDischargesId == id);
            if (maladaptiveDischarges == null)
            {
                return NotFound();
            }

            return PartialView(maladaptiveDischarges);
        }

        // POST: MaladaptiveDischarges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maladaptiveDischarges = await _context.MaladaptiveDischarges.FindAsync(id);
            _context.MaladaptiveDischarges.Remove(maladaptiveDischarges);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool MaladaptiveDischargesExists(int id)
        {
            return _context.MaladaptiveDischarges.Any(e => e.MaladaptiveDischargesId == id);
        }
    }
}

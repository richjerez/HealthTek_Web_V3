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
    public class BaCrisisPlansController : Controller
    {
        private readonly IdentityContext _context;

        public BaCrisisPlansController(IdentityContext context)
        {
            _context = context;
        }

        // GET: BaCrisisPlans
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.BaCrisisPlan.Include(b => b.FkBaAssessments);
            return View(await identityContext.ToListAsync());
        }

        // GET: BaCrisisPlans/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: BaCrisisPlans/Create
        public IActionResult Create(int id)
        {
            BaCrisisPlan crisisPlan = new BaCrisisPlan();
            crisisPlan.FkBaAssessmentsId = id;
            return PartialView(crisisPlan);
        }

        // POST: BaCrisisPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BaCrisisPlan baCrisisPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baCrisisPlan);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", baCrisisPlan) });
        }

        // GET: BaCrisisPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baCrisisPlan = await _context.BaCrisisPlan.FindAsync(id);
            if (baCrisisPlan == null)
            {
                return NotFound();
            }
            return PartialView(baCrisisPlan);
        }

        // POST: BaCrisisPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaCrisisPlan baCrisisPlan)
        {
            if (id != baCrisisPlan.BaCrisisPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baCrisisPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaCrisisPlanExists(baCrisisPlan.BaCrisisPlanId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", baCrisisPlan) });
        }

        // GET: BaCrisisPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baCrisisPlan = await _context.BaCrisisPlan
                .Include(b => b.FkBaAssessments)
                .FirstOrDefaultAsync(m => m.BaCrisisPlanId == id);
            if (baCrisisPlan == null)
            {
                return NotFound();
            }

            return PartialView(baCrisisPlan);
        }

        // POST: BaCrisisPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baCrisisPlan = await _context.BaCrisisPlan.FindAsync(id);
            _context.BaCrisisPlan.Remove(baCrisisPlan);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool BaCrisisPlanExists(int id)
        {
            return _context.BaCrisisPlan.Any(e => e.BaCrisisPlanId == id);
        }
    }
}

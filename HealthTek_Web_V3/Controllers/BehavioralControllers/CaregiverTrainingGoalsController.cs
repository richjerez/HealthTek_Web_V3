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
    public class CaregiverTrainingGoalsController : Controller
    {
        private readonly IdentityContext _context;

        public CaregiverTrainingGoalsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: CaregiverTrainingGoals
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.CaregiverTrainingGoals.Include(c => c.FkMaladaptives).Include(c => c.FkReplacements);
            return View(await identityContext.ToListAsync());
        }

        // GET: CaregiverTrainingGoals/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: CaregiverTrainingGoals/Create
        public IActionResult Create(int id, string name)
        {
            CaregiverTrainingGoals caregiverTrainingGoals = new CaregiverTrainingGoals();
            switch (name)
            {
                case "Maladaptives":
                    caregiverTrainingGoals.FkMaladaptivesId = id;
                    break;
                case "Replacements":
                    caregiverTrainingGoals.FkReplacementsId = id;
                    break;
                case "Interventions":
                    caregiverTrainingGoals.FkBaAssessmentsInterventionsId = id;
                    break;
                case "Preferences":
                    caregiverTrainingGoals.FkPreferencesId = id;
                    break;
            }
            return PartialView(caregiverTrainingGoals);
        }

        // POST: CaregiverTrainingGoals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverTrainingGoals caregiverTrainingGoals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caregiverTrainingGoals);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverTrainingGoals) });
        }

        // GET: CaregiverTrainingGoals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverTrainingGoals = await _context.CaregiverTrainingGoals.FindAsync(id);
            if (caregiverTrainingGoals == null)
            {
                return NotFound();
            }
            return PartialView(caregiverTrainingGoals);
        }

        // POST: CaregiverTrainingGoals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] CaregiverTrainingGoals caregiverTrainingGoals)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caregiverTrainingGoals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverTrainingGoalsExists(caregiverTrainingGoals.CaregiverTrainingGoalsId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverTrainingGoals) });
        }

        // GET: CaregiverTrainingGoals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverTrainingGoals = await _context.CaregiverTrainingGoals
                .Include(c => c.FkMaladaptives)
                .Include(c => c.FkBaAssessmentsInterventions)
                .ThenInclude(c => c.FkInterventions)
                .Include(c => c.FkReplacements)
                .FirstOrDefaultAsync(m => m.CaregiverTrainingGoalsId == id);
            if (caregiverTrainingGoals == null)
            {
                return NotFound();
            }
            return PartialView(caregiverTrainingGoals);
        }

        // POST: CaregiverTrainingGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] CaregiverTrainingGoals caregiverTrainingGoals)
        {
            _context.CaregiverTrainingGoals.Remove(caregiverTrainingGoals);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool CaregiverTrainingGoalsExists(int id)
        {
            return _context.CaregiverTrainingGoals.Any(e => e.CaregiverTrainingGoalsId == id);
        }
    }
}

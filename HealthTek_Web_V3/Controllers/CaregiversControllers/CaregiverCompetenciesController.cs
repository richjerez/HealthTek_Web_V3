using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.CaregiversControllers
{
    [Authorize]
    public class CaregiverCompetenciesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CaregiverCompetenciesController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CaregiverCompetencies
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.CaregiverCompetencies.Include(c => c.BaProgressNotes).Include(c => c.FkUserSignatures);
            return View(await identityContext.ToListAsync());
        }

        // GET: CaregiverCompetencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompetencies = await _context.CaregiverCompetencies
                .Include(c => c.BaProgressNotes)
                .Include(c => c.FkUserSignatures)
                .FirstOrDefaultAsync(m => m.CaregiverCompetenciesId == id);
            if (caregiverCompetencies == null)
            {
                return NotFound();
            }

            return View(caregiverCompetencies);
        }

        // GET: CaregiverCompetencies/Create
        public IActionResult Create(int id)
        {
            ViewData["Maladaptives"] = new SelectList(_context.Maladaptives, "MaladaptivesId", "MaladaptiveName");
            ViewData["Replacements"] = new SelectList(_context.Replacements, "ReplacementsId", "ReplacementName");
            ViewData["Reinforcers"] = new SelectList(_context.ReinforcerCatalog, "ReinforcerCatalogId", "ReinforcerName");
            ViewData["Interventions"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName");
            CaregiverCompetencies competencies = new CaregiverCompetencies();
            competencies.FkBaProgressNotesId = id;
            var comps = _context.CaregiverCompChecksCatalog.ToList();
            List<CaregiverCompChecks> cpt = new List<CaregiverCompChecks>();
            foreach (var item in comps)
            {
                CaregiverCompChecks checks = new CaregiverCompChecks();
                checks.FkCaregiverComptChecksCatalog = item;
                checks.CreationDate = DateTime.Now;
                checks.LastUpdateDate = DateTime.Now;
                cpt.Add(checks);
            }
            competencies.CaregiverCompChecks = cpt;
            return PartialView(competencies);
        }

        // POST: CaregiverCompetencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CaregiverCompetencies caregiverCompetencies, bool Sign)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var empSignature = await _context.Employees.Include(m => m.EmployeesRoleNames).ThenInclude(m => m.FkRoleNames).Where(u => u.EmployeesId == user.FkEmployeesId).Include(m => m.FkESignatures).FirstOrDefaultAsync();
                if (!empSignature.EmployeesRoleNames.Any(m => m.FkRoleNames.RoleName.Contains("bcba")) ||
                    !empSignature.EmployeesRoleNames.Any(m => m.FkRoleNames.RoleName.Contains("bcaba")))
                {
                    ModelState.AddModelError("", "You do not have permission to modify this Caregiver Competency!");
                    foreach (var ModelState in ViewData.ModelState.Values)
                    {
                        foreach (var ModelErrors in ModelState.Errors)
                        {
                            string errormessage = ModelErrors.ErrorMessage;
                        }
                    }

                }
                if (Sign)
                {
                    caregiverCompetencies.FkUserSignaturesId = empSignature.FkESignatures.ESignaturesId;
                }


                _context.Add(caregiverCompetencies);
                await _context.SaveChangesAsync();
                TempData["Toast"] = "You have created a Caregiver Competency Check!";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregiverCompetencies) });
        }

        // GET: CaregiverCompetencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompetencies = _context.CaregiverCompetencies
                .Include(m => m.CaregiverCompChecks)
                .ThenInclude(m => m.FkCaregiverComptChecksCatalog)
                .FirstOrDefault(m => m.CaregiverCompetenciesId == id);
            if (caregiverCompetencies == null)
            {
                return NotFound();
            }
            ViewData["Maladaptives"] = new SelectList(_context.Maladaptives, "MaladaptivesId", "MaladaptiveName");
            ViewData["Replacements"] = new SelectList(_context.Replacements, "ReplacementsId", "ReplacementName");
            ViewData["Reinforcers"] = new SelectList(_context.ReinforcerCatalog, "ReinforcerCatalogId", "ReinforcerName");
            ViewData["Interventions"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName");

            return PartialView(caregiverCompetencies);
        }

        // POST: CaregiverCompetencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CaregiverCompetencies caregiverCompetencies, bool Sign)
        {
            if (id != caregiverCompetencies.CaregiverCompetenciesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var empSignature = await _context.Employees.Include(m => m.EmployeesRoleNames).ThenInclude(m => m.FkRoleNames).Where(u => u.EmployeesId == user.FkEmployeesId).Include(m => m.FkESignatures).FirstOrDefaultAsync();
                    if (!empSignature.EmployeesRoleNames.Any(m => m.FkRoleNames.RoleName.Contains("bcba")) ||
                        !empSignature.EmployeesRoleNames.Any(m => m.FkRoleNames.RoleName.Contains("bcaba")))
                    {
                        ModelState.AddModelError("", "You do not have permission to modify this Caregiver Competency!");
                        foreach (var ModelState in ViewData.ModelState.Values)
                        {
                            foreach (var ModelErrors in ModelState.Errors)
                            {
                                string errormessage = ModelErrors.ErrorMessage;
                            }
                        }

                    }
                    if (Sign)
                    {
                        caregiverCompetencies.FkUserSignaturesId = empSignature.FkESignatures.ESignaturesId;
                    }

                    _context.Update(caregiverCompetencies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiverCompetenciesExists(caregiverCompetencies.CaregiverCompetenciesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "You have update a Caregiver Competency Check!";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Maladaptives"] = new SelectList(_context.Maladaptives, "MaladaptivesId", "MaladaptiveName");
            ViewData["Replacements"] = new SelectList(_context.Replacements, "ReplacementsId", "ReplacementName");
            ViewData["Reinforcers"] = new SelectList(_context.ReinforcerCatalog, "ReinforcerCatalogId", "ReinforcerName");
            ViewData["Interventions"] = new SelectList(_context.Interventions, "InterventionsId", "InterventionName");
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregiverCompetencies) });
        }

        // GET: CaregiverCompetencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregiverCompetencies = await _context.CaregiverCompetencies
                .Include(c => c.BaProgressNotes)
                .Include(c => c.FkUserSignatures)
                .FirstOrDefaultAsync(m => m.CaregiverCompetenciesId == id);
            if (caregiverCompetencies == null)
            {
                return NotFound();
            }

            return View(caregiverCompetencies);
        }

        // POST: CaregiverCompetencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregiverCompetencies = await _context.CaregiverCompetencies.FindAsync(id);
            _context.CaregiverCompetencies.Remove(caregiverCompetencies);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool CaregiverCompetenciesExists(int id)
        {
            return _context.CaregiverCompetencies.Any(e => e.CaregiverCompetenciesId == id);
        }
    }
}

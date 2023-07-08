using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class BaReassessmentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalLists = new ExternalLists();

        public BaReassessmentsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: BaReassessments
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.BaReassessments.Include(b => b.InitialAssessment);
            return View(await identityContext.ToListAsync());
        }

        // GET: BaReassessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baReassessments = await _context.BaReassessments
                .Include(b => b.InitialAssessment)
                .FirstOrDefaultAsync(m => m.BaReassessmentsId == id);
            if (baReassessments == null)
            {
                return NotFound();
            }

            return View(baReassessments);
        }

        // GET: BaReassessments/Create
        public IActionResult Create()
        {
            ViewData["FkBaInitialAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId");
            ViewData["Types"] = new SelectList(_externalLists.AssessmentTypes);
            return View();
        }

        // POST: BaReassessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BaReassessments baReassessments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baReassessments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBaInitialAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId", baReassessments.FkBaInitialAssessmentsId);
            return View(baReassessments);
        }

        // GET: BaReassessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baReassessments = await _context.BaReassessments
                .Include(m => m.Reassessments)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.AbcReports)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.BaCrisisPlan)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.Authorizations)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Employee
                .ThenInclude(m => m.FkEmployees)
                .ThenInclude(m => m.EmployeesRoleNames)
                .ThenInclude(m => m.FkRoleNames)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Auth
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.ClientsFacilities)
                .AsSplitQuery()
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Auth
                .ThenInclude(m => m.FkStartLocation)
                .AsSplitQuery()
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Auth
                .ThenInclude(m => m.FkEndLocation)
                .AsSplitQuery()
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Auth
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Authorizations)
                .ThenInclude(m => m.FkServiceCodes)
                .AsSplitQuery()
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Preferences
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkReinforcersCatalog)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Preferences CTG
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .AsSplitQuery() // Replacements
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Preferences
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Client Preferences CTG / STO - LTO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery() // Replacements
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.ShortTermObjectives) // Replacements STO
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.LongTermObjectives) // Replacements LTO
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery() // Maladaptive
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkMaladaptiveDischarges) // Discharges
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Maladaptive Functions
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FunctionsList)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Maladaptive STO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments) // Maladaptive LTO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.LongTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.LongTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.BaAssessmentsInterventions) // Interventions
                .ThenInclude(m => m.FkInterventions)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.BaAssessmentsInterventions) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.InitialAssessment)
                .ThenInclude(m => m.BaAssessmentsInterventions) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .Where(i => i.BaReassessmentsId == id).FirstOrDefaultAsync();
            if (baReassessments == null)
            {
                return NotFound();
            }
            ViewData["STOStatus"] = new SelectList(_externalLists.BehaviorStatuses);
            ViewData["Communication"] = new SelectList(_externalLists.Communication);
            if (baReassessments == null)
            {
                return NotFound();
            }
            return View(baReassessments);
        }

        // POST: BaReassessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaReassessments baReassessments, string Summary)
        {
            if (id != baReassessments.BaReassessmentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baReassessments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaReassessmentsExists(baReassessments.BaReassessmentsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBaInitialAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId", baReassessments.FkBaInitialAssessmentsId);
            return View(baReassessments);
        }

        // GET: BaReassessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baReassessments = await _context.BaReassessments
                .Include(b => b.InitialAssessment)
                .FirstOrDefaultAsync(m => m.BaReassessmentsId == id);
            if (baReassessments == null)
            {
                return NotFound();
            }

            return View(baReassessments);
        }

        // POST: BaReassessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baReassessments = await _context.BaReassessments.FindAsync(id);
            _context.BaReassessments.Remove(baReassessments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaReassessmentsExists(int id)
        {
            return _context.BaReassessments.Any(e => e.BaReassessmentsId == id);
        }
    }
}

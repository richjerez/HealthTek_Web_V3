using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class BaAssessmentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ExternalLists _externalLists = new ExternalLists();

        public BaAssessmentsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index() => PartialView();

        // GET: BaAssessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baAssessments = await _context.BaAssessments
                .Include(m => m.AbcReports)
                .Include(m => m.BaCrisisPlan)
                .Include(m => m.Authorizations)
                .Include(m => m.FkAppointments) // Start Location
                .ThenInclude(m => m.FkStartLocation)
                .AsSplitQuery()
                .Include(m => m.FkAppointments) // End Location
                .ThenInclude(m => m.FkEndLocation)
                .AsSplitQuery()
                .Include(m => m.FkAppointments) // Employee
                .ThenInclude(m => m.FkEmployees)
                .ThenInclude(m => m.EmployeesRoleNames)
                .ThenInclude(m => m.FkRoleNames)
                .Include(m => m.FkAppointments) // Client Auth
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Authorizations)
                .ThenInclude(m => m.FkServiceCodes)
                .AsSplitQuery()
                .Include(m => m.FkAppointments) // Client Preferences
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkReinforcersCatalog)
                .Include(m => m.FkAppointments) // Client Facilities
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.ClientsFacilities)
                .Include(m => m.FkAppointments) // Client Preferences CTG
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .AsSplitQuery() // Replacements
                .Include(m => m.FkAppointments) // Client Preferences
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.FkAppointments) // Client Preferences CTG / STO - LTO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Preferences)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery() // Replacements
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.ShortTermObjectives) // Replacements STO
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.LongTermObjectives) // Replacements LTO
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery() // Maladaptive
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkMaladaptiveDischarges) // Discharges
                .Include(m => m.FkAppointments) // Maladaptive Functions
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FunctionsList)
                .Include(m => m.FkAppointments) // Maladaptive STO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.FkAppointments) // Maladaptive LTO
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.LongTermObjectives)
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.LongTermObjectives)
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .Include(m => m.BaAssessmentsInterventions) // Interventions
                .ThenInclude(m => m.FkInterventions)
                .Include(m => m.BaAssessmentsInterventions) // CaregiverTrainingGoals STO
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .Include(m => m.BaAssessmentsInterventions) // CaregiverTrainingGoals LTO
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .Where(i => i.BaAssessmentsId == id).FirstOrDefaultAsync();
            if (baAssessments == null)
            {
                return NotFound();
            }
            ViewData["STOStatus"] = new SelectList(_externalLists.BehaviorStatuses);
            ViewData["Communication"] = new SelectList(new ExternalLists().Communication);
            return View(baAssessments);
        }

        // POST: BaAssessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaAssessments baAssessments)
        {
            if (id != baAssessments.BaAssessmentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var emp = _context.Employees.Include(m => m.FkESignatures).Where(m => m.EmployeesId == user.FkEmployeesId).FirstOrDefault();
                    baAssessments.LastUpdateDate = DateTime.Now;
                    baAssessments.FkAnalystSignatureId = emp.FkESignatures.ESignaturesId;
                    _context.Update(baAssessments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaAssessmentsExists(baAssessments.BaAssessmentsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "QualityAssurance");
            }
            ViewData["STOStatus"] = new SelectList(_externalLists.BehaviorStatuses);
            ViewData["Communication"] = new SelectList(new ExternalLists().Communication);
            return View(baAssessments);
        }

        private bool BaAssessmentsExists(int id)
        {
            return _context.BaAssessments.Any(e => e.BaAssessmentsId == id);
        }
    }
}

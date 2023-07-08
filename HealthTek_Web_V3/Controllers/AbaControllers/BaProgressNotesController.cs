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
    public class BaProgressNotesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalList = new ExternalLists();
        private readonly UserManager<AppUser> _userManager;

        public BaProgressNotesController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BaProgressNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baProgressNotes = _context.BaProgressNotes
                .Include(f => f.EnvironmentalChanges)
                .Include(f => f.CaregiverFeedbackNotesCheck)
                .Include(f => f.CaregiverCompetencies)
                .ThenInclude(f => f.CaregiverCompChecks)
                .ThenInclude(f => f.FkCaregiverComptChecksCatalog)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.ClientsFacilities)
                .AsSplitQuery() // Maladaptive Measurements
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.MaladaptiveMeasurements)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.ShortTermObjectives)
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.LongTermObjectives)
                .AsSplitQuery() // Replacement Measurements
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.FkReplacements)
                .ThenInclude(f => f.ShortTermObjectives)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.FkReplacements)
                .ThenInclude(f => f.LongTermObjectives)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Maladaptives)
                .ThenInclude(f => f.FkReplacements)
                .ThenInclude(f => f.ReplacementMeasurements)
                .AsSplitQuery() // Reinforcers & Preferences
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Preferences)
                .ThenInclude(f => f.FkReinforcersCatalog)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Preferences)
                .ThenInclude(f => f.FkCaregiverTrainingGoals)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Preferences)
                .ThenInclude(f => f.FkCaregiverTrainingGoals)
                .ThenInclude(f => f.ShortTermObjectives)
                .AsSplitQuery()
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Preferences)
                .ThenInclude(f => f.FkCaregiverTrainingGoals)
                .ThenInclude(f => f.LongTermObjectives)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Medications
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Medications)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Roles
                .ThenInclude(f => f.FkEmployees)
                .ThenInclude(f => f.EmployeesRoleNames)
                .ThenInclude(f => f.FkRoleNames)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Service Codes
                .ThenInclude(f => f.FkServiceCodes)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Location Start
                .ThenInclude(f => f.FkStartLocation)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Location End
                .ThenInclude(f => f.FkEndLocation)
                .FirstOrDefault(m => m.BaProgressNotesId == id);
            if (baProgressNotes == null)
            {
                return NotFound();
            }
            var newid = baProgressNotes.FkAppointments.FkClientsId;
            var reinforcers = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName");
            var feedback = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "Feedback");
            var mal = new SelectList(_context.Maladaptives.Where(f => f.FkClientsId == newid).ToList(), "MaladaptiveName", "MaladaptiveName");
            if (baProgressNotes.ReinforcerListIds != null)
            {
                foreach (var item in reinforcers)
                {
                    if (baProgressNotes.ReinforcerListIds.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }
            if (baProgressNotes.RiskBehaviorMonitored != null)
            {
                foreach (var item in mal)
                {
                    if (baProgressNotes.RiskBehaviorMonitored.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }
            if (baProgressNotes.CaregiverFeedbackNotesCheck.Count != 0)
            {
                foreach (var item in feedback)
                {
                    var newId = Int32.Parse(item.Value);
                    if (baProgressNotes.CaregiverFeedbackNotesCheck.Any(m => m.CaregiverFeedbackNotesCheckId == newId))
                    {
                        item.Selected = true;
                    }
                }

            }
            if (baProgressNotes.ReinforcerListIds != null)
            {
                foreach (var item in reinforcers)
                {
                    if (baProgressNotes.ReinforcerListIds.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }
            ViewData["STOStatus"] = new SelectList(_externalList.BehaviorStatuses);
            ViewData["FkReinforcersCatalogId"] = reinforcers;
            ViewData["CaregiverFeedback"] = feedback;
            ViewData["RiskBehaviors"] = mal;
            ViewData["ClientParticipations"] = new SelectList(_externalList.ClientParticipations);
            return View(baProgressNotes);
        }

        // POST: BaProgressNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaProgressNotes baProgressNotes)
        {
            if (id != baProgressNotes.BaProgressNotesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    baProgressNotes.ReinforcerListIds = String.Join(",", baProgressNotes.Reinforcers.ToArray());
                    var OpCheck = baProgressNotes.CaregiverFeedbackNotesCheck;
                    if (OpCheck != null && OpCheck.Count != 0)
                    {
                        _context.CaregiverFeedbackNotesCheck.RemoveRange(OpCheck);
                        _context.Entry(OpCheck).State = EntityState.Detached;
                    }

                    if (baProgressNotes.CaregiverFeedback != null)
                    {
                        foreach (var item in baProgressNotes.CaregiverFeedback)
                        {
                            CaregiverFeedbackNotesCheck facility = new CaregiverFeedbackNotesCheck();
                            facility.FkBaProgressNotesId = baProgressNotes.BaProgressNotesId.Value;
                            facility.FkCaregiverFeedbackId = item;
                            facility.CreationDate = DateTime.Now;
                            facility.LastUpdateDate = DateTime.Now;
                            _context.CaregiverFeedbackNotesCheck.Add(facility);
                            await _context.SaveChangesAsync();
                            //baProgressNotes.CaregiverFeedbackNotesCheck.Add(facility);
                        }
                    }


                    var user = await _userManager.GetUserAsync(User);
                    var emp = _context.Employees.Include(m => m.FkESignatures).FirstOrDefault(m => m.EmployeesId == user.FkEmployeesId);
                    baProgressNotes.FkEmployeeSignatureId = emp.FkESignatures.ESignaturesId;
                    baProgressNotes.LastUpdateDate = DateTime.Now;
                    _context.Update(baProgressNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaProgressNotesExists(baProgressNotes.BaProgressNotesId.Value))
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
            var newid = baProgressNotes.FkAppointments.FkClientsId;
            var reinforcers = new SelectList(_context.Set<ReinforcerCatalog>(), "ReinforcerCatalogId", "ReinforcerName");
            var feedback = new SelectList(_context.CaregiverFeedback, "CaregiverFeedbackId", "Feedback");
            var mal = new SelectList(_context.Maladaptives.Where(f => f.FkClientsId == newid).ToList(), "MaladaptiveName", "MaladaptiveName");
            ViewData["STOStatus"] = new SelectList(_externalList.BehaviorStatuses);
            ViewData["FkReinforcersCatalogId"] = reinforcers;
            ViewData["CaregiverFeedback"] = feedback;
            ViewData["RiskBehaviors"] = mal;
            ViewData["ClientParticipations"] = new SelectList(_externalList.ClientParticipations);
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments, "AppointmentsId", "AppointmentsId", baProgressNotes.FkAppointmentsId);
            ViewData["FkEmployeeSignatureId"] = new SelectList(_context.Set<ESignatures>(), "ESignaturesId", "ESignaturesId", baProgressNotes.FkEmployeeSignatureId);
            ViewData["FkSupervisorSignatureId"] = new SelectList(_context.Set<ESignatures>(), "ESignaturesId", "ESignaturesId", baProgressNotes.FkSupervisorSignatureId);
            return View(baProgressNotes);
        }

        private bool BaProgressNotesExists(int id)
        {
            return _context.BaProgressNotes.Any(e => e.BaProgressNotesId == id);
        }
    }
}
